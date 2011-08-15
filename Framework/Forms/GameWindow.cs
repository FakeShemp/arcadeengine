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
using System.Windows.Forms;
using ArcEngine.Graphic;
using ArcEngine.Input;
using TK = OpenTK.Graphics;

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

			TK.GraphicsContextFlags compatible = param.Compatible ? TK.GraphicsContextFlags.Default : TK.GraphicsContextFlags.ForwardCompatible;

			// Adds the control to the form
			Trace.WriteDebugLine("[GameWindow] Requesting a {0}.{1} {2}Opengl context (Color: {3}, Depth: {4}, Stencil:{5}, Sample: {6})",
				param.Major, param.Minor, param.Compatible ? "compatible " : string.Empty, param.Color, param.Depth, param.Stencil, param.Samples);

            RenderControl = new OpenTK.GLControl(new TK.GraphicsMode(param.Color, param.Depth, param.Stencil, param.Samples),
				param.Major, param.Minor, compatible);
			Trace.WriteDebugLine("[GameWindow] Got GraphicsMode {0}", RenderControl.GraphicsMode);
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
            OpenTK.DisplayResolution res = OpenTK.DisplayDevice.Default.SelectResolution(ClientSize.Width, ClientSize.Height, 32, 60);
			SetFullScreen(res);
		}


		/// <summary>
		/// Set in FullScreen mode
		/// </summary>
		/// <param name="resolution">Desired resolution</param>
        void SetFullScreen(OpenTK.DisplayResolution resolution)
		{
			Trace.WriteDebugLine("[GameWindow] SetFullScreen()");

			if (resolution == null)
				return;

            OpenTK.DisplayDevice.Default.ChangeResolution(resolution);

			this.WindowState = FormWindowState.Maximized;
			this.FormBorderStyle = FormBorderStyle.None;


			IsFullScreen = true;
		}


		/// <summary>
		/// Set in FullScreen mode
		/// </summary>
		/// <param name="size">Size</param>
		/// <param name="bpp">Color depth</param>
		void SetFullScreen(Size size, int bpp)
		{
            OpenTK.DisplayResolution res = OpenTK.DisplayDevice.Default.SelectResolution(size.Width, size.Height, bpp, 60);
			SetFullScreen(res);
		}


		/// <summary>
		/// Change to windowed mode
		/// </summary>
		public void SetWindowed()
		{
			Trace.WriteDebugLine("[GameWindow] SetWindowed()");

            OpenTK.DisplayDevice.Default.RestoreResolution();
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
		private void OnLoad(object sender, EventArgs e)
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
			Display.Diagnostic();

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
			Keyboard.OnKeyDown(e);
		}


		/// <summary>
		/// On KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnKeyUp(object sender, KeyEventArgs e)
		{
			Keyboard.OnKeyUp(e);
		}


		/// <summary>
		/// On MouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseUp(object sender, MouseEventArgs e)
		{
			Mouse.OnButtonUp(e);
		}


		/// <summary>
		/// On MouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			Mouse.OnDoubleClick(e);
		}


		/// <summary>
		/// On MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDown(object sender, MouseEventArgs e)
		{
			Mouse.OnButtonDown(e);
		}


		/// <summary>
		/// On MouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseMove(object sender, MouseEventArgs e)
		{
			Mouse.OnMove(e);
		}

		/// <summary>
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			Trace.WriteDebugLine("[GameWindow] Form_Closed()");
            
            GameBase.Exit();
            
            Display.Dispose();

			if (RenderControl != null)
				RenderControl.Dispose();
			RenderControl = null;

		}


	
		#endregion

	
		#region Properties

		/// <summary>
		/// OpenTK control
		/// </summary>
        OpenTK.GLControl RenderControl;


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


/*
		/// <summary>
		/// Gets available displays mode
		/// </summary>
        static List<OpenTK.DisplayDevice> AvailableDisplays
		{
			get
			{
                return new List<OpenTK.DisplayDevice>(OpenTK.DisplayDevice.AvailableDisplays);
			}
		}
*/
        #endregion

	}

}
