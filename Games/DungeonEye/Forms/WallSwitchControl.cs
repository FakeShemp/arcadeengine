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


			DecorationSet = maze.Decoration;


			WallSwitch = wallswitch;
		}


		/// <summary>
		/// 
		/// </summary>
		void RenderActivated()
		{
			ActivatedGLBox.MakeCurrent();
			Display.ClearBuffers();

			Batch.Begin();

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

			DecorationSet.Draw(Batch, (int)DeactivatedIdBox.Value, ViewFieldPosition.L);

			Batch.End();

			DeactivatedGlBox.SwapBuffers();
		}


		#region Control events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WallSwitchControl_Load(object sender, EventArgs e)
		{
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
		/// 
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


		#endregion


	}
}
