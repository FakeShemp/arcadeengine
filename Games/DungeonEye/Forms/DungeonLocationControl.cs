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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using OpenTK.Graphics.OpenGL;

namespace DungeonEye.Forms
{
	public partial class DungeonLocationControl : UserControl
	{

		/// <summary>
		/// 
		/// </summary>
		public DungeonLocationControl()
		{
			InitializeComponent();

		}



		/// <summary>
		/// Gets mazeblock location from a coordinate in the control
		/// </summary>
		/// <param name="point">Coordinate in the control</param>
		/// <returns></returns>
		public DungeonLocation GetLocation(Point point)
		{
			DungeonLocation loc = new DungeonLocation(Dungeon);
			loc.SetMaze(Maze.Name);

			loc.Position = point;


			return loc;
		}



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DrawTimer.Stop();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_Load(object sender, EventArgs e)
		{
			GlControlBox.MakeCurrent();
			Display.Init();

			if (DesignMode)
				return;

			// Preload background texture resource
			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


			Batch = new Batch();

			// Preload texture resources
			Icons = new TileSet();
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


			ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
			DrawTimer.Start();
	
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControlBox.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControlBox.Size);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			if (GlControlBox.Context == null)
				return;

			GlControlBox.MakeCurrent();

			try
			{
				Display.ClearBuffers();


				if (DesignMode)
					return;


				// Background texture
				if (CheckerBoard != null)
					CheckerBoard.Blit(new Rectangle(Point.Empty, GlControlBox.Size), TextureLayout.Tile);



				if (Maze == null)
					return;


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
						if (block.Type == BlockType.Illusion)
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

							if (Maze.IsDoorNorthSouth(block.Location))
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
									tile = Icons.GetTile(100 + ((int)side > 1 ? 0 : 1));
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

				Batch.Apply();
				Display.DrawBatch(Batch, BeginMode.Quads);



				// Target
				if (Target.MazeName == Maze.Name)
					Display.DrawRectangle(new Rectangle(Offset.X + Target.Position.X * 25, Offset.Y + Target.Position.Y * 25, 25, 25), Color.White);

			
			}
			finally
			{
				GlControlBox.SwapBuffers();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_DoubleClick(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_MouseUp(object sender, MouseEventArgs e)
		{

		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawTimer_Tick(object sender, EventArgs e)
		{
			DrawTimer.Stop();


			GlControl_Paint(null, null);

			DrawTimer.Start();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_MouseMove(object sender, MouseEventArgs e)
		{
			BlockUnderMouse = new Point((e.Location.X - Offset.X) / 25, (e.Location.Y - Offset.Y) / 25);
		}



		#endregion



		#region Properties


		/// <summary>
		/// Dungeon
		/// </summary>
		public Dungeon Dungeon
		{
			get;
			set;
		}


		/// <summary>
		/// Maze to display
		/// </summary>
		public Maze Maze
		{
			get;
			set;
		}


		/// <summary>
		/// Target location
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}


		/// <summary>
		/// Checkerboard background texture
		/// </summary>
		Texture CheckerBoard;


		/// <summary>
		/// Maze icons
		/// </summary>
		TileSet Icons;


		/// <summary>
		/// Rendering batch
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
		/// Gets the block coordinate under the mouse
		/// </summary>
		public Point BlockUnderMouse
		{
			get;
			private set;
		}

		#endregion



	}
}
