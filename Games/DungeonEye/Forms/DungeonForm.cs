#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Dungeon form editor
	/// </summary>
	public partial class DungeonForm : AssetEditorBase
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="node">Dungeon node definition</param>
		public DungeonForm(XmlNode node)
		{
			InitializeComponent();



			// Create the dungeon
			Dungeon = new Dungeon();
			Dungeon.Load(node);
			Dungeon.Init();

			PreviewLoc = new DungeonLocation(Dungeon.StartLocation);


			MazePropertyBox.Tag = Dungeon;
			RebuildMazeList();
			DungeonNoteBox.Text = Dungeon.Note;

			

			KeyboardScheme = ResourceManager.CreateAsset<InputScheme>(Game.InputSchemeName);
			if (KeyboardScheme == null)
			{
				KeyboardScheme = new InputScheme();
				KeyboardScheme["MoveForward"] = Keys.Z;
				KeyboardScheme["MoveBackward"] = Keys.S;
				KeyboardScheme["StrafeLeft"] = Keys.Q;
				KeyboardScheme["StrafeRight"] = Keys.D;
				KeyboardScheme["TurnLeft"] = Keys.A;
				KeyboardScheme["TurnRight"] = Keys.E;
				KeyboardScheme["Inventory"] = Keys.I;
				KeyboardScheme["SelectHero1"] = Keys.D1;
				KeyboardScheme["SelectHero2"] = Keys.D2;
				KeyboardScheme["SelectHero3"] = Keys.D3;
				KeyboardScheme["SelectHero4"] = Keys.D4;
				KeyboardScheme["SelectHero5"] = Keys.D5;
				KeyboardScheme["SelectHero6"] = Keys.D6;
			}

		}



		/// <summary>
		/// Move the team
		/// </summary>
		/// <param name="front"></param>
		/// <param name="strafe"></param>
		/// <returns>True if move allowed, otherwise false</rereturns>
		public void PreviewMove(int front, int strafe)
		{

			// Destination point
		//	Point dst = Point.Empty;
			Point offset = Point.Empty;


			switch (PreviewLoc.Direction)
			{
				case CardinalPoint.North:
				{
				//	dst = new Point(PreviewLoc.Position.X + front, PreviewLoc.Position.Y + strafe);
					offset = new Point(front, strafe);
				}
				break;

				case CardinalPoint.South:
				{
				//	dst = new Point(PreviewLoc.Position.X - front, PreviewLoc.Position.Y - strafe);
					offset = new Point(-front, -strafe);
				}
				break;

				case CardinalPoint.East:
				{
				//	dst = new Point(PreviewLoc.Position.X - strafe, PreviewLoc.Position.Y + front);
					offset = new Point(-strafe, front);
				}
				break;

				case CardinalPoint.West:
				{
				//	dst = new Point(PreviewLoc.Position.X + strafe, PreviewLoc.Position.Y - front);
					offset = new Point(strafe, -front);
				}
				break;
			}



			PreviewLoc.Position.X += offset.X;
			PreviewLoc.Position.Y += offset.Y;

			PreviewBox.Text = "Preview pos : " + PreviewLoc.Position.ToString();

		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			ResourceManager.AddAsset<Dungeon>(Dungeon.Name, ResourceManager.ConvertAsset(Dungeon));
		}


		/// <summary>
		/// Refresh zones
		/// </summary>
		public void RebuildZones()
		{
			MazeZonesBox.BeginUpdate();
			MazeZonesBox.Items.Clear();
			foreach (MazeZone zone in Maze.Zones)
			{
				MazeZonesBox.Items.Add(zone.Name);
			}

			MazeZonesBox.EndUpdate();
		}

		#region Events


		/// <summary>
		/// On form loading
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DungeonForm_Load(object sender, EventArgs e)
		{

			glControl.MakeCurrent();
			Display.Init();

			SpriteBatch = new SpriteBatch();

			// Preload texture resources
			Icons = new TileSet();
			Icons.Texture = new Texture2D(ResourceManager.GetInternalResource("DungeonEye.Forms.data.editor.png"));

			// Preload background texture resource
			CheckerBoard = new Texture2D(ResourceManager.GetInternalResource("ArcEngine.Resources.checkerboard.png"));
			CheckerBoard.HorizontalWrap = TextureWrapFilter.Repeat;
			CheckerBoard.VerticalWrap = TextureWrapFilter.Repeat;

			int id = 0;
			for (int y = 0; y < Icons.Texture.Size.Height - 50; y += 25)
			{
				for (int x = 0; x < Icons.Texture.Size.Width; x += 25)
				{
					Tile tile = Icons.AddTile(id++);
					tile.Rectangle = new Rectangle(x, y, 25, 25);
				}
			}
			Icons.AddTile(100).Rectangle = new Rectangle(0, 245, 6, 11); // alcoves
			Icons.AddTile(101).Rectangle = new Rectangle(6, 248, 11, 6); // alcoves

			DrawTimer.Start();
		
		}


		/// <summary>
		/// On mouse down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_MouseDown(object sender, MouseEventArgs e)
		{
			LastMousePos = e.Location;

			if (Maze == null)
				return;

			Point coord = new Point((e.Location.X - Offset.X) / 25, (e.Location.Y - Offset.Y) / 25);
			if (!Maze.Contains(coord))
				return;

			MazeBlock block = Maze.GetBlock(coord);

			if (e.Button == MouseButtons.Middle)
			{
				LastMousePos = e.Location;
				Cursor = Cursors.SizeAll;

				return;
			}
			else if (e.Button == MouseButtons.Left)
			{
				BlockCoord = coord;


				#region Mazeblock changing
				// Add wall
				if (EditWallButton.Checked)
				{
					if (block.Type == BlockType.Ground)
						block.Type = BlockType.Wall;
					else if (block.Type == BlockType.Wall)
						block.Type = BlockType.Illusion;
					else if (block.Type == BlockType.Illusion)
						block.Type = BlockType.Wall;
				}
				#endregion

				#region Zone

				else if (CreateNewZoneBox.Checked)
				{
					CurrentZone = new MazeZone();
					CurrentZone.Rectangle = new Rectangle(coord, new Size(1, 1));
				}

				#endregion


				// Select object
				else
				{
					if (PreviewLoc.Position == BlockCoord)
						DragPreview = true;
				}
			}

			else if (e.Button == MouseButtons.Right)
			{

				if (EditWallButton.Checked)
				{
					//block.IsWall = false;
					block.Type = BlockType.Ground;
				}
			}


		}


		/// <summary>
		/// On double click edit block informations
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void glControl_DoubleClick(object sender, EventArgs e)
		{
			// No maze or not a double left mouse click
			if (Maze == null || ((MouseEventArgs)e).Button != MouseButtons.Left)
				return;

			// Not while editing walls
			if (EditWallButton.Checked)
				return;

			Point pos = glControl.PointToClient(MousePosition);
			pos = new Point((pos.X - Offset.X) / 25, (pos.Y - Offset.Y) / 25);
			if (!Maze.Contains(pos))
				return;
			

			new MazeBlockForm(Maze, Maze.GetBlock(pos)).ShowDialog();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_MouseMove(object sender, MouseEventArgs e)
		{

			if (Maze == null)
				return;


			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle)
			{
				// Smooth the value
				Offset.X -= (LastMousePos.X - e.X) * 2;
				Offset.Y -= (LastMousePos.Y - e.Y) * 2;


				// Store last mouse location
				LastMousePos = e.Location;
	
				return;
			}

			else if (e.Button == MouseButtons.Left)
			{
				BlockCoord = new Point((e.Location.X - Offset.X) / 25, (e.Location.Y - Offset.Y) / 25);
				MazeBlock block = Maze.GetBlock(BlockCoord);

				if (EditWallButton.Checked)
				{
					block.Type = BlockType.Wall;
					return;
				}
				else if (CreateNewZoneBox.Checked)
				{
					CurrentZone.Rectangle = new Rectangle(CurrentZone.Rectangle.Location,
						new Size(BlockCoord.X - CurrentZone.Rectangle.Left + 1, BlockCoord.Y - CurrentZone.Rectangle.Top + 1));
				}


				if (DragPreview)
				{
					PreviewLoc.Position = BlockCoord;
				}
			}

			else if (e.Button == MouseButtons.Right)
			{
				BlockCoord = new Point((e.Location.X - Offset.X) / 25, (e.Location.Y - Offset.Y) / 25);
				MazeBlock block = Maze.GetBlock(BlockCoord);

				if (EditWallButton.Checked)
				{
					block.Type = BlockType.Ground;
				}
			}


			// Debug
			LabelBox.Text = new Point((e.Location.X - Offset.X) / 25, (e.Location.Y - Offset.Y) / 25).ToString();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Middle)
			{
				Cursor = Cursors.Default;
				return;
			}

			// Entity no more selected
			if (e.Button == MouseButtons.Left)
			{
				if (CreateNewZoneBox.Checked)
				{
					DungeonEye.Forms.Wizards.NewNameWizard wizard = new DungeonEye.Forms.Wizards.NewNameWizard(string.Empty);
					if (wizard.ShowDialog() == DialogResult.OK)
					{
						CurrentZone.Name = wizard.NewName;
						Maze.Zones.Add(CurrentZone);
						RebuildZones();
					}
					else
					{
						CurrentZone = null;
					}

					CreateNewZoneBox.Checked = false;
				}

				DragPreview = false;
				return;
			}


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			if (SpriteBatch == null)
				return;

			glControl.MakeCurrent();
			Display.ClearBuffers();

			SpriteBatch.Begin();

            // Background texture
            Rectangle dst = new Rectangle(Point.Empty, glControl.Size);
			SpriteBatch.Draw(CheckerBoard, dst, dst, Color.White);


			if (Maze == null)
			{
				SpriteBatch.End();
				glControl.SwapBuffers();
				return;
			}


			// Blocks
			for (int y = 0; y < Maze.Size.Height; y++)
			{
				for (int x = 0; x < Maze.Size.Width; x++)
				{
					MazeBlock block = Maze.GetBlock(new Point(x, y));
					int tileid = block.Type == BlockType.Ground ? 1 : 0;

					// Location of the block on the screen
					Point location = new Point(Offset.X + x * 25, Offset.Y + y * 25);


					Color color = Color.White;
					if (block.Type == BlockType.Illusion)
						color = Color.LightGreen; 

					SpriteBatch.DrawTile(Icons, tileid, location, color);

					if (block.GroundItemCount > 0)
					{
						SpriteBatch.DrawTile(Icons, 19, location);
					}



					// Doors
					if (block.Door != null)
					{
						if (Maze.IsDoorNorthSouth(block.Location))
							tileid = 3;
						else
							tileid = 2;


						// Door opened or closed
						if (block.Door.State == DoorState.Broken || block.Door.State == DoorState.Opened || block.Door.State == DoorState.Opening)
							tileid += 2;

						SpriteBatch.DrawTile(Icons, tileid, location);
					}


					if (block.FloorPlate != null)
					{
						SpriteBatch.DrawTile(Icons, 18, location);
					}

					if (block.Pit != null)
					{
						SpriteBatch.DrawTile(Icons, 9, location);
					}

					if (block.Teleporter != null)
					{
						SpriteBatch.DrawTile(Icons, 11, location);
					}

					if (block.ForceField != null)
					{
						if (block.ForceField.Type == ForceFieldType.Turning)
							tileid = 12;
						else if (block.ForceField.Type == ForceFieldType.Moving)
						{
							tileid = 13 + (int)block.ForceField.Move;
						}
						else
							tileid = 17;

						SpriteBatch.DrawTile(Icons, tileid, location);
					}

					if (block.Stair != null)
					{
						tileid = block.Stair.Type == StairType.Up ? 6 : 7;
						SpriteBatch.DrawTile(Icons, tileid, location);
					}

					// Alcoves
					if (block.HasAlcoves)
					{
						// Alcoves coords
						Point[] alcoves = new Point[]
						{
							new Point(7, 0),
							new Point(7, 19),
							new Point(0, 7),
							new Point(19, 7),
						};


						foreach (CardinalPoint side in Enum.GetValues(typeof(CardinalPoint)))
						{
							if (block.HasAlcove(side))
							{
								tileid = (int)side > 1 ? 100: 101;
								SpriteBatch.DrawTile(Icons, tileid, new Point(
									Offset.X + x * 25 + alcoves[(int)side].X, 
									Offset.Y + y * 25 + alcoves[(int)side].Y));

							}
						}
					}

				}
			}

			// Draw monsters
			foreach (Monster monster in Maze.Monsters)
				SpriteBatch.DrawTile(Icons, 8, new Point(Offset.X + monster.Location.Position.X * 25, Offset.Y + monster.Location.Position.Y * 25));

	

			// Preview pos
			SpriteBatch.DrawTile(Icons, 22 + (int)PreviewLoc.Direction, new Point(Offset.X + PreviewLoc.Position.X * 25, Offset.Y + PreviewLoc.Position.Y * 25));

			// Starting point
			if (Dungeon.StartLocation.MazeName == Maze.Name)
			{
				SpriteBatch.DrawTile(Icons, 20,
					new Point(Offset.X + Dungeon.StartLocation.Position.X * 25, Offset.Y + Dungeon.StartLocation.Position.Y * 25));
			}



			// Surround the selected object
			if (MazePropertyBox.SelectedObject != null)
				SpriteBatch.DrawRectangle(new Rectangle(BlockCoord.X * 25 + Offset.X, BlockCoord.Y * 25 + Offset.Y, 25, 25), Color.White);


			if (DisplayZonesBox.Checked)
			{

				foreach (MazeZone zone in Maze.Zones)
				{
					Rectangle rect = new Rectangle(zone.Rectangle.X * 25, zone.Rectangle.Y * 25, zone.Rectangle.Width * 25, zone.Rectangle.Height * 25);
					Color color = Color.FromArgb(100, Color.Red);

					if (CurrentZone == zone)
					{
						color = Color.FromArgb(100, Color.Red);
						SpriteBatch.DrawRectangle(rect, Color.White);
					}

					SpriteBatch.FillRectangle(rect, color);
				}
			}

			if (CurrentZone != null)
			{
				Rectangle rect = new Rectangle(CurrentZone.Rectangle.X * 25, CurrentZone.Rectangle.Y * 25, CurrentZone.Rectangle.Width * 25, CurrentZone.Rectangle.Height * 25);
				SpriteBatch.FillRectangle(rect, Color.FromArgb(128, Color.Blue));
			}


			SpriteBatch.End();
			glControl.SwapBuffers();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				EditWallButton.Checked = false;
				//ObjectPropertyBox.SelectedObject = null;
			}


			//else if (e.KeyCode == Keys.Delete)
			//{
			//   if (ObjectPropertyBox.SelectedObject == null)
			//      return;

			//   //if (ObjectPropertyBox.SelectedObject is Monster)
			//   //{
			//   //   Monster monster = ObjectPropertyBox.SelectedObject as Monster;
			//   //   Maze.Monsters.Remove(monster);
			//   //}

			//   ObjectPropertyBox.SelectedObject = null;
			//}
			else if (e.KeyCode == KeyboardScheme["TurnLeft"])
			{
				PreviewLoc.Compass.Rotate(CompassRotation.Rotate270);
			}

			// Turn right
			else if (e.KeyCode == KeyboardScheme["TurnRight"])
				PreviewLoc.Compass.Rotate(CompassRotation.Rotate90);


			// Move forward
			else if (e.KeyCode == KeyboardScheme["MoveForward"])
				PreviewMove(0, -1);


			// Move backward
			else if (e.KeyCode == KeyboardScheme["MoveBackward"])
				PreviewMove(0, 1);


			// Strafe left
			else if (e.KeyCode == KeyboardScheme["StrafeLeft"])
				PreviewMove(-1, 0);

			// Strafe right
			else if (e.KeyCode == KeyboardScheme["StrafeRight"])
				PreviewMove(1, 0);
		}



		/// <summary>
		/// Resize OpenGL controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			glControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), glControl.Size);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DungeonNoteBox_TextChanged(object sender, EventArgs e)
		{
			if (Dungeon == null)
				return;
			Dungeon.Note = DungeonNoteBox.Text;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MazeListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Maze = Dungeon.GetMaze(MazeListBox.SelectedItem.ToString());
			MazePropertyBox.SelectedObject = Maze;
			PreviewLoc.SetMaze(Maze.Name);
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DungeonForm_FormClosing(object sender, FormClosingEventArgs e)
		{

			DrawTimer.Stop();
			DialogResult result = MessageBox.Show("Save modifications ?", "Dungeon Editor", MessageBoxButtons.YesNoCancel);


			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
				DrawTimer.Start();
				return;
			}


			if (Dungeon != null)
				Dungeon.Dispose();
			Dungeon = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;

			if (Icons != null)
				Icons.Dispose();
			Icons = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;
		}


		/// <summary>
		/// Adds a maze
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddMazeButton_Click(object sender, EventArgs e)
		{
			new Wizards.NewMazeWizard(Dungeon).ShowDialog();

			RebuildMazeList();
		}


		/// <summary>
		///  Removes a maze
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveMazeButton_Click(object sender, EventArgs e)
		{
			Dungeon.RemoveMaze(MazeListBox.SelectedItem.ToString());
			RebuildMazeList();


			if (MazeListBox.Items.Count > 0)
				MazeListBox.SelectedIndex = 0;
			else
				Maze = null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResetOffsetBox_Click(object sender, EventArgs e)
		{
			Offset = Point.Empty;
		}




		/// <summary>
		/// Draw timer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawTimer_Tick(object sender, EventArgs e)
		{
			DrawTimer.Stop();
	

			GlControl_Paint(null, null);

			if (GlPreviewControl.Created)		
				GlPreviewControl_Paint(null, null);

			DrawTimer.Start();
		}


		/// <summary>
		/// Set the starting point
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StartLocationMenu_Click(object sender, EventArgs e)
		{
			Dungeon.StartLocation = new DungeonLocation(PreviewLoc);
		}


		#endregion


		#region Preview control events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_Load(object sender, EventArgs e)
		{
			GlPreviewControl.MakeCurrent();
			Display.Init();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_Paint(object sender, PaintEventArgs e)
		{
			if (SpriteBatch == null )
				return;

			GlPreviewControl.MakeCurrent();

			Display.ClearBuffers();

			if (Maze == null)
			{
				GlPreviewControl.SwapBuffers();
				return;
			}

			SpriteBatch.Begin();
			Maze.Draw(SpriteBatch, PreviewLoc);
			SpriteBatch.End();

			GlPreviewControl.SwapBuffers();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_Resize(object sender, EventArgs e)
		{

			GlPreviewControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlPreviewControl.Size);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ForwardBox_Click(object sender, EventArgs e)
		{
			PreviewMove(0, -1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TurnLeftBox_Click(object sender, EventArgs e)
		{
			PreviewLoc.Compass.Rotate(CompassRotation.Rotate270);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StrafeLeftBox_Click(object sender, EventArgs e)
		{
			PreviewMove(-1, 0);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BackwardBox_Click(object sender, EventArgs e)
		{
			PreviewMove(0, 1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TurnRightBox_Click(object sender, EventArgs e)
		{
			PreviewLoc.Compass.Rotate(CompassRotation.Rotate90);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StrafeRightBox_Click(object sender, EventArgs e)
		{
			PreviewMove(1, 0);
		}


		#endregion


		#region Misc

		/// <summary>
		/// Rebuild maze list
		/// </summary>
		void RebuildMazeList()
		{
			MazeListBox.BeginUpdate();
			MazeListBox.Items.Clear();

			foreach (Maze maze in Dungeon.MazeList)
				MazeListBox.Items.Add(maze.Name);

			MazeListBox.EndUpdate();

			if (Maze == null)
				return;

			MazeListBox.SelectedItem = Maze.Name;
		}

		#endregion


		#region Maze zone region



		
		#endregion


		#region Properties

		/// <summary>
		/// Asset
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Dungeon;
			}
		}

		/// <summary>
		/// Dungeon to edit
		/// </summary>
		Dungeon Dungeon;


		/// <summary>
		/// Current maze
		/// </summary>
		Maze Maze;


		/// <summary>
		/// Preview location
		/// </summary>
		DungeonLocation PreviewLoc;


		/// <summary>
		/// Maze icons
		/// </summary>
		TileSet Icons;


		/// <summary>
		/// Spritebtach
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Draw offset of the map
		/// </summary>
		Point Offset;



		/// <summary>
		/// Last location of the mouse
		/// </summary>
		Point LastMousePos;


		/// <summary>
		/// Background texture
		/// </summary>
		Texture2D CheckerBoard;


		/// <summary>
		/// Location of the mazeblock
		/// </summary>
		Point BlockCoord;


		/// <summary>
		/// Dragging preview position
		/// </summary>
		bool DragPreview;


		/// <summary>
		/// Allow the player to personalize keyboard input shceme
		/// </summary>
		InputScheme KeyboardScheme;


		/// <summary>
		/// Current maze zone
		/// </summary>
		MazeZone CurrentZone;

		#endregion

	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditWallButton_Click(object sender, EventArgs e)
		{
			if (Maze == null)
			{
				EditWallButton.Checked = false;
				return;
			}

			CreateNewZoneBox.Checked = false;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateNewZoneBox_Click(object sender, EventArgs e)
		{
			if (Maze == null)
			{
				CreateNewZoneBox.Checked = false;
				return;
			}

			EditWallButton.Checked = false;
		}
	}





}
