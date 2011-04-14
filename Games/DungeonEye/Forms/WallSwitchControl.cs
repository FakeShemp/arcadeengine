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

namespace DungeonEye
{
	/// <summary>
	/// Wall switch control
	/// </summary>
	public partial class WallSwitchControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="wallswitch"></param>
		/// <param name="maze"></param>
		public WallSwitchControl(WallSwitch wallswitch, Maze maze)
		{
			InitializeComponent();


			ActivatedIdBox.Value = wallswitch.ActivatedDecoration;
			DeactivatedIdBox.Value = wallswitch.DeactivatedDecoration;


			Maze = maze;
			DecorationSet = maze.Decoration;
			WallSwitch = wallswitch;
			ActionScriptBox.Actions = wallswitch.Actions;
			ActionScriptBox.Dungeon = maze.Dungeon;

		}



		/// <summary>
		/// 
		/// </summary>
		void RenderActivated()
		{
			ActivatedGLBox.MakeCurrent();
			Display.ClearBuffers();

			Batch.Begin();

			// Background
			Batch.DrawTile(Maze.WallTileset, 0, Point.Empty);

			// Render the walls
			foreach (TileDrawing tmp in DisplayCoordinates.GetWalls(ViewFieldPosition.L))
				Batch.DrawTile(Maze.WallTileset, tmp.ID, tmp.Location);

			// Draw decoration
			DecorationSet.Draw(Batch, (int)ActivatedIdBox.Value, ViewFieldPosition.L);

			Batch.End();

			ActivatedGLBox.SwapBuffers();
		}


		/// <summary>
		/// 
		/// </summary>
		void RenderDeactivated()
		{
			DeactivatedGlBox.MakeCurrent();
			Display.ClearBuffers();

			Batch.Begin();

			// Background
			Batch.DrawTile(Maze.WallTileset, 0, Point.Empty);

			// Render the walls
			foreach (TileDrawing tmp in DisplayCoordinates.GetWalls(ViewFieldPosition.L))
				Batch.DrawTile(Maze.WallTileset, tmp.ID, tmp.Location);

			// Draw decoration
			DecorationSet.Draw(Batch, (int) DeactivatedIdBox.Value, ViewFieldPosition.L);

			Batch.End();

			DeactivatedGlBox.SwapBuffers();
		}


		#region Control events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="direction"></param>
		private void SideBox_DirectionChanged(object sender, CardinalPoint direction)
		{
			if (WallSwitch == null)
				return;

			WallSwitch.Side = SideBox.Direction;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WallSwitchControl_Load(object sender, EventArgs e)
		{
			ParentForm.FormClosing +=new FormClosingEventHandler(ParentForm_FormClosing);
			
			
			Batch = new SpriteBatch();
		}

		#endregion


		#region Activated

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActivatedGLBox_Load(object sender, EventArgs e)
		{
			ActivatedGLBox.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, ActivatedGLBox.Size);
			Display.RenderState.ClearColor = Color.Black;
			Display.RenderState.Blending = true;
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActivatedIdBox_ValueChanged(object sender, EventArgs e)
		{
			if (WallSwitch == null)
				return;
			WallSwitch.ActivatedDecoration = (int)ActivatedIdBox.Value;

			RenderActivated();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActivatedGLBox_Paint(object sender, PaintEventArgs e)
		{
			RenderActivated();
		}

		#endregion


		#region Deactivated

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeactivatedGlBox_Load(object sender, EventArgs e)
		{
			DeactivatedGlBox.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, DeactivatedGlBox.Size);
			Display.RenderState.ClearColor = Color.Black;
			Display.RenderState.Blending = true;
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeactivatedIdBox_ValueChanged(object sender, EventArgs e)
		{
			if (WallSwitch == null)
				return;

			WallSwitch.DeactivatedDecoration = (int) DeactivatedIdBox.Value;

			RenderDeactivated();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeactivatedGlBox_Paint(object sender, PaintEventArgs e)
		{
			RenderDeactivated();
		}

		#endregion


		#region Script

		#endregion


		#region Properties

		/// <summary>
		/// Wall switch handle
		/// </summary>
		WallSwitch WallSwitch;


		/// <summary>
		/// Spritebatch handle
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Decoration handle
		/// </summary>
		DecorationSet DecorationSet;


		/// <summary>
		/// 
		/// </summary>
		Maze Maze;

		#endregion

	}
}
