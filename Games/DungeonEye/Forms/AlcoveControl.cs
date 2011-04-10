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
		/// 
		/// </summary>
		/// <param name="alcove">Alcove handle</param>
		/// <param name="maze">Maze handle</param>
		public AlcoveControl(Alcove alcove, Maze maze)
		{
			if (alcove == null || maze == null)
				throw new ArgumentNullException("[AlcoveControl] : Alcove handle or Maze handle is null !!!");

			InitializeComponent();

			Buttons = new Button[]
			{
				NorthBox,
				SouthBox,
				WestBox,
				EastBox,
			};

			// Warning, no decoration defined for this maze !!
			if (maze.Decoration == null)
				MessageBox.Show("No decoration defined for this maze. Please define a decoration first !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);


			UpdateUI();

			Maze = maze;
			Alcove = alcove;
		}


		/// <summary>
		/// 
		/// </summary>
		void UpdateUI()
		{
			if (Alcove == null)
				return;

			for (int i = 0; i < Buttons.Length; i++)
			{
				// Marks face buttons
				if (Alcove.GetSideState((CardinalPoint)i))
					Buttons[i].ForeColor = Color.Red;
				else
					Buttons[i].ForeColor = Color.Black;
			}
		}


		/// <summary>
		/// Marks faces button with an alcove
		/// </summary>
		void UpdateFaceButtons()
		{
	
			for (int i = 0; i < Buttons.Length; i++)
			{

				// Marks face buttons
				if (Alcove != null && Alcove.GetSideState((CardinalPoint)i))
				{
					Buttons[i].ForeColor = Color.Red;
				}
				else
				{
					Buttons[i].ForeColor = Color.Black;
				}
			}
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


		#endregion


		#region Events


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

			DecorationBox.Value = -1;
			UpdateFaceButtons();
			RenderScene();
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

			Alcove.SetSideTile(Face, (int)DecorationBox.Value);
			RenderScene();
			UpdateFaceButtons();
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NorthBox_Click(object sender, EventArgs e)
		{
			ChangeSide(CardinalPoint.North);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SouthBox_Click(object sender, EventArgs e)
		{
			ChangeSide(CardinalPoint.South);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WestBox_Click(object sender, EventArgs e)
		{
			ChangeSide(CardinalPoint.West);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EastBox_Click(object sender, EventArgs e)
		{
			ChangeSide(CardinalPoint.East);
		}

	
		#endregion


		#region Properties

		/// <summary>
		/// Alcove
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
		/// 
		/// </summary>
		Button[] Buttons;


		#endregion


	}
}
