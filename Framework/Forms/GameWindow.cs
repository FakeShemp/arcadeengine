﻿#region Licence
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
		public GameWindow(GameWindowParams param)
		{
			Trace.WriteDebugLine("[GameWindow] Constructor()");

			if (param == null)
			{
				Trace.WriteDebugLine("[GameWindow] param == null !");
				throw new ArgumentNullException("param");
			}

			InitializeComponent();


			// Adds the control to the form
			RenderControl = new GLControl(new GraphicsMode(param.Color, param.Depth, param.Stencil, param.Samples),
				param.Major, param.Minor,
				GraphicsContextFlags.Default);
			RenderControl.Dock = DockStyle.Fill;
			Controls.Add(RenderControl);

			// Resize the window
			ClientSize = param.Size;

			VSync = true;
			if (param.FullScreen)
				SetFullScreen(param.Size, param.Color);

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


		#region FullScreen / Windowed mode

		/// <summary>
		/// Set in FullScreen mode
		/// </summary>
		public void SetFullScreen()
		{
			DisplayResolution res = DisplayDevice.Default.SelectResolution(ClientSize.Width, ClientSize.Height, 32, 60);
			SetFullScreen(res);
		}


		/// <summary>
		/// Set in FullScreen mode
		/// </summary>
		/// <param name="resolution">Desired resolution</param>
		public void SetFullScreen(DisplayResolution resolution)
		{
			Trace.WriteDebugLine("[GameWindow] SetFullScreen()");

			if (resolution == null)
				return;

			DisplayDevice.Default.ChangeResolution(resolution);

			this.WindowState = FormWindowState.Maximized;
			this.FormBorderStyle = FormBorderStyle.None;


			IsFullScreen = true;
		}

		/// <summary>
		/// Set in FullScreen mode
		/// </summary>
		/// <param name="size">Size</param>
		/// <param name="bpp">Color depth</param>
		public void SetFullScreen(Size size, int bpp)
		{
			DisplayResolution res = DisplayDevice.Default.SelectResolution(size.Width, size.Height, bpp, 60);
			SetFullScreen(res);
		}


		/// <summary>
		/// Change to windowed mode
		/// </summary>
		public void SetWindowed()
		{
			Trace.WriteDebugLine("[GameWindow] SetWindowed()");

			DisplayDevice.Default.RestoreResolution();
			WindowState = FormWindowState.Normal;
			Resizable = resizable;

			IsFullScreen = false;
		} 

		#endregion


		#region Events


		/// <summary>
		/// On resize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnResize(object sender, EventArgs e)
		{
			Trace.WriteDebugLine("[GameWindow] OnResize()");
			
			if (RenderControl == null)
			{
				Trace.WriteLine("[GameWindow] OnResize() : RenderControl is null !!");
				return;
			}

			if (RenderControl.Context == null)
			{
				Trace.WriteLine("[GameWindow] OnResize() : RenderControl.Context is null !!");
				return;
			}

			RenderControl.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, RenderControl.Size);
		}


		/// <summary>
		/// Control init
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_Load(object sender, EventArgs e)
		{
			Trace.WriteDebugLine("[GameWindow] Form_Load()");

			if (RenderControl == null)
			{
				Trace.WriteLine("[GameWindow] Form_Load() : RenderControl is null !!");
				return;
			}
			
			if (RenderControl.Context == null)
			{
				Trace.WriteDebugLine("[GameWindow] Form_Load() : RenderControl.Context == null");
				return;
			}

			// Initialization
			RenderControl.MakeCurrent();
			Display.Init();
			Display.TraceInfos();

			// Force the resize of the GLControl
			OnResize(null, null);
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
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			Trace.WriteDebugLine("[GameWindow] Form_Closed()");
			
			if (RenderControl != null)
			{
				RenderControl.Dispose();
				RenderControl = null;
			}
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
				return resizable;
			}

			set
			{
				if (value)
				{
					FormBorderStyle = FormBorderStyle.Sizable;
					MaximizeBox = true;
				}
				else
				{
					FormBorderStyle = FormBorderStyle.FixedDialog;
					MaximizeBox = false;
				}

				resizable = value;
			}
		}
		bool resizable;


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


		/// <summary>
		/// Gets if the window is in FullScreen mode
		/// </summary>
		public bool IsFullScreen
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets available displays mode
		/// </summary>
		static public List<DisplayDevice> AvailableDisplays
		{
			get
			{
				return new List<DisplayDevice>(DisplayDevice.AvailableDisplays);
			}
		}
		#endregion

	}

}
