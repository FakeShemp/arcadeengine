#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using WeifenLuo.WinFormsUI.Docking;


namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class DungeonForm : AssetEditor
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="node">Dungeon node definition</param>
		public DungeonForm(XmlNode node)
		{
			InitializeComponent();
			PreviewLoc = new DungeonLocation();



			// Create the dungeon
			Dungeon = new Dungeon();
			Dungeon.Load(node);
			Dungeon.Init();

			MazePropertyBox.Tag = Dungeon;
			RebuildMazeList();

			

			KeyboardScheme = ResourceManager.CreateAsset<InputScheme>(DungeonEye.InputSchemeName);
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
			//StringBuilder sb = new StringBuilder();
			//using (XmlWriter writer = XmlWriter.Create(sb))
			//    Dungeon.Save(writer);

			//string xml = sb.ToString();
			//XmlDocument doc = new XmlDocument();
			//doc.LoadXml(xml);

			//ResourceManager.AddAsset<Dungeon>(Dungeon.Name, doc.DocumentElement);

			ResourceManager.AddAsset<Dungeon>(Dungeon.Name, ResourceManager.ConvertAsset(Dungeon));

		}





		#region Events


		/// <summary>
		/// On form loading
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DungeonForm_Load(object sender, EventArgs e)
		{
			GlPreviewControl.MakeCurrent();
			Display.Init();

			glControl.MakeCurrent();
			Display.Init();


			Batch = new Batch();

			// Preload texture resources
			Icons = new TileSet();
			//Icons.Texture = new Texture(Assembly.GetExecutingAssembly().GetManifestResourceStream("DungeonEye.Forms.data.editor.png"));
			Icons.Texture = new Texture(ResourceManager.GetResource("DungeonEye.Forms.data.editor.png"));

			// Preload background texture resource
			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


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
			if (!Maze.Contains(coord) || Maze == null)
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


				#region Adding

				// Add wall
				if (EditWallButton.Checked)
				{
					block.Type = BlockType.Wall;
				}



				#endregion


				// Select object
				else
				{
			//		ObjectPropertyBox.SelectedObject = null;

/*
					// Select monster
					foreach (Monster monster in Maze.Monsters)
					{
						if (monster.Location.Position == BlockCoord)
						{
							ObjectPropertyBox.SelectedObject = monster;
							DragObjectByMouse = true;
							return;
						}
					}
*/



					if (PreviewLoc.Position == BlockCoord)
						DragPreview = true;

					// Maze block
				//	ObjectPropertyBox.SelectedObject = block;

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
			if (Maze == null)
				return;

			//	LabelBox.Text = new Point((e.Location.X - Offset.X) / 25, (e.Location.Y - Offset.Y) / 25).ToString();
			Point pos = glControl.PointToClient(MousePosition);
		//	pos = MousePosition;
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


				//if (DragObjectByMouse)
				//{
				//   // Find the delta mouse movement
				//   Point offset = e.Location;
				//   offset.X -= LastMousePos.X;
				//   offset.Y -= LastMousePos.Y;

				//   if (ObjectPropertyBox.SelectedObject is Monster)
				//   {
				//      Monster monster = ObjectPropertyBox.SelectedObject as Monster;
				//      monster.Location.Position = BlockCoord;
				//   }

				//   return;
				//}
				//else
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
			if (e.Button == MouseButtons.Left)// && DragObjectByMouse)
			{
		//		DragObjectByMouse = false;
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
			glControl.MakeCurrent();
			Display.ClearBuffers();


			// Background texture
			CheckerBoard.Blit(new Rectangle(Point.Empty, glControl.Size), TextureLayout.Tile);

			if (Maze == null)
			{
				glControl.SwapBuffers();
				return;
			}


			// Draw maze background
			Display.Texture = Icons.Texture;
			Batch.Clear();

			Tile tile = null;

			// Blocks
			for (int y = 0; y < Maze.Size.Height; y++)
			{
				for (int x = 0; x < Maze.Size.Width; x++)
				{
					MazeBlock block = Maze.GetBlock(new Point(x, y));
					tile = Icons.GetTile(block.Type == BlockType.Ground ? 1 : 0);
					

					Color color = Color.White;
					if (block.Type == BlockType.Trick)
						color = Color.LightGreen; //Color.FromArgb(200, Color.Green);

					Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), color, tile.Rectangle);

					if (block.GroundItemCount > 0)
					{
						tile = Icons.GetTile(19);
						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), color, tile.Rectangle);
					}



					// Doors
					if (block.Door != null)
					{
						int tileid = 0;
						
						if (Maze.IsDoorNorthSouth(new Point(x, y)))
							tileid = 3;
						else
							tileid = 2;


						// Door opened or closed
						if (block.Door.State == DoorState.Broken || block.Door.State == DoorState.Opened || block.Door.State == DoorState.Opening)
							tileid += 2;

						tile = Icons.GetTile(tileid);

						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), Color.White, tile.Rectangle);
					}


					if (block.FloorPlate != null)
					{
						tile = Icons.GetTile(18);
						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), Color.White, tile.Rectangle);
					}

					if (block.Pit != null)
					{
						tile = Icons.GetTile(9);
						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), Color.White, tile.Rectangle);
					}

					if (block.Teleporter != null)
					{
						tile = Icons.GetTile(11);
						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), Color.White, tile.Rectangle);
					}

					if (block.ForceField != null)
					{
						int id;
						if (block.ForceField.Type == ForceFieldType.Turning)
							id = 12;
						else if (block.ForceField.Type == ForceFieldType.Moving)
						{
							id = 13 + (int)block.ForceField.Move;
						}
						else
							id = 17;

						tile = Icons.GetTile(id);

						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), Color.White, tile.Rectangle);

					}

					if (block.Stair != null)
					{
						tile = Icons.GetTile(block.Stair.Type == StairType.Up ? 6 : 7);
						Batch.AddRectangle(new Rectangle(Offset.X + x * 25, Offset.Y + y * 25, 25, 25), Color.White, tile.Rectangle);
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
								tile = Icons.GetTile(100 + ((int)side > 1 ? 0: 1));
								Batch.AddRectangle(new Rectangle(
									Offset.X + x * 25 + alcoves[(int)side].X, 
									Offset.Y + y * 25 + alcoves[(int)side].Y,
									tile.Size.Width, tile.Size.Height), Color.White, tile.Rectangle);
							}
						}
					}

				}
			}

			// Draw monsters
			tile = Icons.GetTile(8);
			foreach (Monster monster in Maze.Monsters)
				Batch.AddRectangle(new Rectangle(Offset.X + monster.Location.Position.X * 25, Offset.Y + monster.Location.Position.Y * 25, 25, 25), Color.White, tile.Rectangle);

	

			// Preview pos
			tile = Icons.GetTile(22 + (int)PreviewLoc.Direction);
			Batch.AddRectangle(new Rectangle(Offset.X + PreviewLoc.Position.X * 25, Offset.Y + PreviewLoc.Position.Y * 25, 25, 25), Color.White, tile.Rectangle);

			// Starting point
			if (Dungeon.StartLocation.Maze == Maze.Name)
			{
				tile = Icons.GetTile(20);
				Batch.AddRectangle(new Rectangle(Offset.X + Dungeon.StartLocation.Position.X * 25, Offset.Y + Dungeon.StartLocation.Position.Y * 25, 25, 25), Color.White, tile.Rectangle);
			}




			Batch.Apply();
			Display.DrawBatch(Batch, BeginMode.Quads);



			// Surround the selected object
			if (MazePropertyBox.SelectedObject != null)
				Display.DrawRectangle(new Rectangle(BlockCoord.X * 25 + Offset.X, BlockCoord.Y * 25 + Offset.Y, 25, 25), Color.White);




			glControl.SwapBuffers();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_Paint(object sender, PaintEventArgs e)
		{

			GlPreviewControl.MakeCurrent();
			Display.ClearBuffers();

			if (Maze == null)
			{
				GlPreviewControl.SwapBuffers();
				return;
			}


			Maze.Draw(PreviewLoc);

			GlPreviewControl.SwapBuffers();
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


			GlPreviewControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlPreviewControl.Size);
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
			PreviewLoc.Maze = Maze.Name;
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
			}

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
		/// Rendergin batch
		/// </summary>
		Batch Batch;



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
		Texture CheckerBoard;


		/// <summary>
		/// Location of the mazeblock
		/// </summary>
		Point BlockCoord;


		/// <summary>
		/// Dragging an object (monster, door)
		/// </summary>
	//	bool DragObjectByMouse;



		/// <summary>
		/// Dragging preview position
		/// </summary>
		bool DragPreview;


		/// <summary>
		/// Allow the player to personalize keyboard input shceme
		/// </summary>
		InputScheme KeyboardScheme;


		#endregion

	}





}
