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

			// Warning, no decoration defined for this maze !!
			if (maze.Decoration == null)
				MessageBox.Show("No decoration defined for this maze. Please define a decoration first !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

			Maze = maze;
			Alcove = alcove;

			UpdateUI();

		}


		/// <summary>
		/// Update user interface
		/// </summary>
		void UpdateUI()
		{
			if (Alcove == null)
				return;

			DirectionBox.Highlight(CardinalPoint.North, Alcove.GetSideState(CardinalPoint.North));
			DirectionBox.Highlight(CardinalPoint.South, Alcove.GetSideState(CardinalPoint.South));
			DirectionBox.Highlight(CardinalPoint.West, Alcove.GetSideState(CardinalPoint.West));
			DirectionBox.Highlight(CardinalPoint.East, Alcove.GetSideState(CardinalPoint.East));

			HideItemsBox.Checked = Alcove.ItemsHidden(Face);
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
				Decoration deco = Maze.Decoration.GetDecoration(Alcove.GetTile(Face));
				if (deco != null)
				{
					Batch.DrawTile(Maze.Decoration.Tileset, deco.GetTileId(ViewFieldPosition.L), deco.GetLocation(ViewFieldPosition.L));
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

			UpdateUI();

			DecorationBox.Value = Alcove.GetTile(side);

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

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

		}


		#endregion


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemIdBox_ValueChanged(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HideItemsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Alcove == null ||Alcove.ItemsHidden(Face) == HideItemsBox.Checked)
				return;

			Alcove.HideItem(Face, HideItemsBox.Checked);
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
			if (Alcove == null)
				return;

			Alcove.HideItem(Face, false);
			DecorationBox.Value = -1;
			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DecorationBox_ValueChanged(object sender, EventArgs e)
		{
			if (Alcove == null)
				return;

			Alcove.SetSideTile(Face, (int) DecorationBox.Value);
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

		}


		#endregion


		#region Properties

		/// <summary>
		/// Alcove
		/// </summary>
		AlcoveActor Alcove;


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


		#endregion

	}
}
