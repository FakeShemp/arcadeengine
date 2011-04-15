#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Interface;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Alcove control
	/// </summary>
	public partial class AlcoveControl : UserControl
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="alcove">Alcove handle</param>
		/// <param name="maze">Maze handle</param>
		public AlcoveControl(AlcoveActor alcove, Maze maze)
		{
			if (alcove == null || maze == null)
				throw new ArgumentNullException("[AlcoveControl] : Alcove handle or Maze handle is null !!!");

			InitializeComponent();

			LastMousePosition = Control.MousePosition;

			// Warning, no decoration defined for this maze !!
			if (maze.Decoration == null)
				MessageBox.Show("No decoration defined for this maze. Please define a decoration first !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);


			ItemsBox.Items.Add("");
			ItemsBox.Items.AddRange(ResourceManager.GetAssets<Item>().ToArray());

			Maze = maze;
			Actor = alcove;

			DirectionBox.Direction = CardinalPoint.North;
			UpdateUI();

		}


		/// <summary>
		/// Update user interface
		/// </summary>
		void UpdateUI()
		{
			if (Actor == null || Alcove == null)
				return;

			DirectionBox.Highlight(CardinalPoint.North, Actor.GetAlcove(CardinalPoint.North).Decoration != -1);
			DirectionBox.Highlight(CardinalPoint.South, Actor.GetAlcove(CardinalPoint.South).Decoration != -1);
			DirectionBox.Highlight(CardinalPoint.West, Actor.GetAlcove(CardinalPoint.West).Decoration != -1);
			DirectionBox.Highlight(CardinalPoint.East, Actor.GetAlcove(CardinalPoint.East).Decoration != -1);

			HideItemsBox.Checked = Alcove.HideItems;
			AcceptBigItemsBox.Checked = Alcove.AcceptBigItems;
			DecorationBox.Value = Alcove.Decoration;
			ItemLocationBox.Text = Alcove.ItemLocation.ToString();
		}




		/// <summary>
		/// Draws the scene
		/// </summary>
		void RenderScene()
		{
			GLControl.MakeCurrent();

			Display.ClearBuffers();


			Batch.Begin();

			// Background
			Batch.DrawTile(Maze.WallTileset, 0, Point.Empty);

			// Draw the wall
			foreach (TileDrawing tmp in DisplayCoordinates.GetWalls(ViewFieldPosition.L))
				Batch.DrawTile(Maze.WallTileset, tmp.ID, tmp.Location, Color.White, 0.0f, tmp.Effect, 0.0f);


			// Draw the tile
			if (Maze.Decoration != null)
			{
				Decoration deco = Maze.Decoration.GetDecoration(Alcove.Decoration);
				if (deco != null)
				{
					Point loc = deco.GetLocation(ViewFieldPosition.L);
					Batch.DrawTile(Maze.Decoration.Tileset, deco.GetTileId(ViewFieldPosition.L), loc);


					// Draw preview item
					if (Maze.Dungeon.ItemTileSet != null && PreviewItem != null)
					{
						// Draw the item
						loc.Offset(Alcove.ItemLocation);
				//		Batch.DrawTile(Maze.Dungeon.ItemTileSet, PreviewItem.GroundTileID, loc);


						Batch.DrawTile(Maze.Dungeon.ItemTileSet, PreviewItem.GroundTileID, loc,
							DisplayCoordinates.GetDistantColor(ViewFieldPosition.L), 0.0f,
							DisplayCoordinates.GetItemScaleFactor(ViewFieldPosition.L), SpriteEffects.None, 0.0f);


					}
				}
			}


			Batch.End();
			GLControl.SwapBuffers();
		}



		/// <summary>
		/// Release all resources
		/// </summary>
		void ReleaseResources()
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="side"></param>
		void ChangeSide(CardinalPoint side)
		{
			Face = side;
			Alcove = Actor.GetAlcove(side);

			UpdateUI();
			RenderScene();
		}


		#region GLControl


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLControl_Load(object sender, EventArgs e)
		{
			GLControl.MakeCurrent();
			Display.Init();


			Batch = new SpriteBatch();


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLControl_Paint(object sender, PaintEventArgs e)
		{
			RenderScene();
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (PreviewItem != null)
				{
					Point offset = new Point(e.Location.X - LastMousePosition.X, e.Location.Y - LastMousePosition.Y);
					Point location = Alcove.ItemLocation;
					location.Offset(offset);
					Alcove.ItemLocation = location;

					RenderScene();
					UpdateUI();
				}
				Cursor = Cursors.SizeAll;
			}
			else
			{
				Cursor = Cursors.Default;
			}

			// Update last mouse position
			LastMousePosition = e.Location;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

			if (PreviewItem != null)
			{

				Point location = Alcove.ItemLocation;

				switch (e.KeyCode)
				{
					case Keys.Up:
					{
						location.Y--;
						e.IsInputKey = true;
					}
					break;

					case Keys.Down:
					{
						location.Y++;
						e.IsInputKey = true;
					}
					break;

					case Keys.Left:
					{
						location.X--;
						e.IsInputKey = true;
					}
					break;

					case Keys.Right:
					{
						location.X++;
						e.IsInputKey = true;
					}
					break;
				}

				Alcove.ItemLocation = location;
				RenderScene();
				UpdateUI();
			}

		}


		#endregion


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			PreviewItem = ResourceManager.CreateAsset<Item>((string) ItemsBox.SelectedItem);
			RenderScene();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HideItemsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Actor == null || Alcove.HideItems == HideItemsBox.Checked)
				return;

			Alcove.HideItems = HideItemsBox.Checked;
			RenderScene();
			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AlcoveControl_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			if (ParentForm != null)
				ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ReleaseResources();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearBox_Click(object sender, EventArgs e)
		{
			if (Actor == null)
				return;

			Alcove.HideItems = false;
			DecorationBox.Value = -1;
			ItemsBox.SelectedIndex = -1;
			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DecorationBox_ValueChanged(object sender, EventArgs e)
		{
			if (Actor == null || Alcove.Decoration == (int) DecorationBox.Value)
				return;

			Alcove.Decoration = (int) DecorationBox.Value;
			RenderScene();
			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="direction"></param>
		private void DirectionBox_DirectionChanged(object sender, CardinalPoint direction)
		{
			ChangeSide(direction);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AcceptBigItemsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Actor == null || Alcove.AcceptBigItems ==  AcceptBigItemsBox.Checked)
				return;

			Alcove.AcceptBigItems = AcceptBigItemsBox.Checked;
			UpdateUI();
		}


		#endregion


		#region Properties

		/// <summary>
		/// Alcove
		/// </summary>
		AlcoveActor Actor;


		/// <summary>
		/// Current alcove handle
		/// </summary>
		Alcove Alcove;


		/// <summary>
		/// Current face
		/// </summary>
		CardinalPoint Face;


		/// <summary>
		/// Sprite batch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Current maze
		/// </summary>
		Maze Maze;


		/// <summary>
		/// Preview item
		/// </summary>
		Item PreviewItem;

		/// <summary>
		/// 
		/// </summary>
		Point LastMousePosition;

		#endregion


	}
}
