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
using System.ComponentModel;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Input;
using ArcEngine.Graphic;
using OpenTK.Graphics;


namespace ArcEngine.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class GameWindow : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public GameWindow()
		{
			GraphicsMode mode = new GraphicsMode(new ColorFormat(32), 24, 8);
			RenderControl = new OpenTK.GLControl(mode);
			InitializeComponent();

		}



		/// <summary>
		/// Make the GameWindow rendering context current
		/// </summary>
		public void MakeCurrent()
		{
			if (RenderControl != null)
				RenderControl.MakeCurrent();
		}



		/// <summary>
		/// Swap the render buffers
		/// </summary>
		public void SwapBuffers()
		{
			if (RenderControl != null)
				RenderControl.SwapBuffers();
		}


		#region Events



		/// <summary>
		/// On PreviewKeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			Keyboard.KeyDown(e);
		}


		/// <summary>
		/// On KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_KeyUp(object sender, KeyEventArgs e)
		{
			Keyboard.KeyUp(e);
		}


		/// <summary>
		/// On MouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseUp(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// On MouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Mouse.DoubleClick(e);
		}


		/// <summary>
		/// On MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseDown(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// On MouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseMove(object sender, MouseEventArgs e)
		{

		}

		/// <summary>
		/// Control init
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_Load(object sender, EventArgs e)
		{
			// Initialization
			RenderControl.MakeCurrent();
			Display.Init();
			Display.TraceInfos();
		}


		#endregion

	
		#region Properties

		/// <summary>
		/// Does the game window have focus
		/// </summary>
		public bool HasFocus
		{
			get
			{
				return ContainsFocus;
			}
		}

		/// <summary>
		/// Gets / sets game window resizable
		/// </summary>
		public bool Resizable
		{
			get
			{
				return FormBorderStyle == FormBorderStyle.Sizable;
			}

			set
			{
				if (value)
					FormBorderStyle = FormBorderStyle.Sizable;
				else
					FormBorderStyle = FormBorderStyle.FixedDialog;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether vsync is active.
		/// </summary>
		public bool VSync
		{
			get
			{
				return RenderControl.VSync;
			}

			set
			{
				RenderControl.VSync = value;
			}

		}

		#endregion
	}
}
