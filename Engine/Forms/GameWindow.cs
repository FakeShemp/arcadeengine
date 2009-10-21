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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK;
using OpenTK.Graphics;

namespace ArcEngine.Forms
{
	/// <summary>
	/// GameWindow
	/// </summary>
	public partial class GameWindow : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GameWindow(Size size, int major, int minor)
		{
			InitializeComponent();

			// ADds the control to the form
			RenderControl = new GLControl(new GraphicsMode(32, 24, 8),
				major, minor,
				GraphicsContextFlags.Default);
			RenderControl.Dock = DockStyle.Fill;
			Controls.Add(RenderControl);

			// Resize the window
			ClientSize = size;

			VSync = true;
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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnResize(object sender, EventArgs e)
		{
			if (RenderControl == null)
				return;

			RenderControl.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, RenderControl.Size);
		}


		/// <summary>
		/// On PreviewKeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			Keyboard.KeyDown(e);
		}


		/// <summary>
		/// On KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnKeyUp(object sender, KeyEventArgs e)
		{
			Keyboard.KeyUp(e);
		}


		/// <summary>
		/// On MouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseUp(object sender, MouseEventArgs e)
		{
			Mouse.ButtonUp(e);
		}


		/// <summary>
		/// On MouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			Mouse.DoubleClick(e);
		}


		/// <summary>
		/// On MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDown(object sender, MouseEventArgs e)
		{
			Mouse.ButtonDown(e);
		}


		/// <summary>
		/// On MouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseMove(object sender, MouseEventArgs e)
		{
			Mouse.Move(e);
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
		/// OpenTK control
		/// </summary>
		GLControl RenderControl;


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
