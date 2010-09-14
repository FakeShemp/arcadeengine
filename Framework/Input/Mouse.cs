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
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Graphic;
using ArcEngine.PInvoke;

namespace ArcEngine.Input
{

	/// <summary>
	/// Mouse 
	/// </summary>
	public static class Mouse
	{
		/// <summary>
		/// Initialize the mouse handler
		/// </summary>
		/// <param name="form">GameWindow handle</param>
		static internal void Init(Form form)
		{
			Trace.WriteDebugLine("[Mouse] Init()");
			if (form == null)
			{
				Trace.WriteDebugLine("[Mouse] form = null !");
				throw new ArgumentNullException("form");
			}

			PreviousLocation = Point.Empty;

			Form = form;
			Form.MouseWheel += new MouseEventHandler(OnMouseWheel);

		}


		/// <summary>
		/// Dispose
		/// </summary>
		static internal void Dispose()
		{
			Trace.WriteDebugLine("[Mouse] Dispose");

			if (Form != null)
				Form.MouseWheel -= new MouseEventHandler(OnMouseWheel);
			Form = null;
		}


		/// <summary>
		/// Updates the mouse state
		/// </summary>
		static internal void Update()
		{
			PreviousState = Buttons;
			Buttons = Form.MouseButtons;

			MoveDelta = new Point(Location.X - PreviousLocation.X, Location.Y - PreviousLocation.Y);
			PreviousLocation = Location;
		}


		/// <summary>
		/// http://www.switchonthecode.com/tutorials/csharp-tutorial-how-to-use-custom-cursors
		/// </summary>
		static public void ChangeCursor()
		{
			string Text = "Cursor Test";

			Bitmap bitmap = new Bitmap(140, 25);
			Graphics g = Graphics.FromImage(bitmap);
			using (Font f = new Font(FontFamily.GenericSansSerif, 10))
				g.DrawString("{ } Switch On The Code", f, Brushes.Green, 0, 0);

			Cursor = CreateCursor(bitmap, 3, 3);

			bitmap.Dispose();
		}

		static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
		{
			User32.IconInfo tmp = new User32.IconInfo();
			User32.GetIconInfo(bmp.GetHicon(), ref tmp);
			tmp.xHotspot = xHotSpot;
			tmp.yHotspot = yHotSpot;
			tmp.fIcon = false;
			return new Cursor(CreateIconIndirect(ref tmp));
		}

		#region Buttons

		/// <summary>
		/// New mouse button Up
		/// </summary>
		/// <param name="button">Mouse button</param>
		/// <returns>True if button up</returns>
		static public bool IsNewButtonUp(MouseButtons button)
		{

			if (PreviousState == Buttons)
				return false;


			return 
				( (PreviousState & button) == button) && 
				( (Buttons & button) != button);

		}


		/// <summary>
		/// New mouse button Down
		/// </summary>
		/// <param name="button">Mouse button</param>
		/// <returns>True if button down</returns>
		static public bool IsNewButtonDown(MouseButtons button)
		{
			return (PreviousState != button) && (Buttons == button);
		}



		/// <summary>
		/// Mouse button Up
		/// </summary>
		/// <param name="button">Mouse button</param>
		/// <returns>True if button up</returns>
		static public bool IsButtonUp(MouseButtons button)
		{
			return (Buttons & button) != button;
		}


		/// <summary>
		/// Mouse button Down
		/// </summary>
		/// <param name="button">Mouse button</param>
		/// <returns>True if button down</returns>
		static public bool IsButtonDown(MouseButtons button)
		{
			return (Buttons & button) == button;
		}


		/// <summary>
		/// Gets a list of all pressed buttons
		/// </summary>
		/// <returns></returns>
		public static List<MouseButtons> GetPressedButtons()
		{
			List<MouseButtons> list = new List<MouseButtons>();

			foreach(MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
			{
				if (IsButtonDown(button))
					list.Add(button);
			}
			return list;
		}


		/// <summary>
		/// Gets a list of all released buttons
		/// </summary>
		/// <returns></returns>
		public static List<MouseButtons> GetReleasedButtons()
		{
			List<MouseButtons> list = new List<MouseButtons>();

			foreach (MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
			{
				if (IsButtonUp(button))
					list.Add(button);
			}
			return list;
		}

		#endregion


		#region OnEvents

		/// <summary>
		/// Mouse wheel event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal static void OnMouseWheel(object sender, MouseEventArgs e)
		{
			if (MouseWheel != null)
				MouseWheel(null, e);
		}
		
		/// <summary>
		/// ButtonDown event
		/// </summary>
		/// <param name="e"></param>
		internal static void OnButtonDown(MouseEventArgs e)
		{
			if (ButtonDown != null)
				ButtonDown(null, e);
		}


		/// <summary>
		/// ButtonUp event
		/// </summary>
		/// <param name="e"></param>
		internal static void OnButtonUp(MouseEventArgs e)
		{
			if (ButtonUp != null)
				ButtonUp(null, e);
		}


		/// <summary>
		/// Move event
		/// </summary>
		/// <param name="e"></param>
		internal static void OnMove(MouseEventArgs e)
		{
			PreviousLocation = e.Location;

			if (Move != null)
				Move(null, e);
		}


		/// <summary>
		/// DoubleClick event
		/// </summary>
		/// <param name="e"></param>
		internal static void OnDoubleClick(MouseEventArgs e)
		{
			if (DoubleClick != null)
				DoubleClick(null, e);
		}

		#endregion


		#region Properties


		/// <summary>
		/// Form capturing mouse input
		/// </summary>
		static Form Form;


		/// <summary>
		/// Gets/sets the mouse Offset
		/// </summary>
		public static Point Location
		{
			get
			{
				if (Form == null)
					throw new ArgumentNullException("Form", "You must initialize the class first !");
				return Form.PointToClient(Cursor.Position);
			}
			set
			{
				Cursor.Position = Form.PointToScreen(value);
			}
		}


		/// <summary>
		/// Previous location
		/// </summary>
		public static Point PreviousLocation
		{
			get;
			private set;
		}


		/// <summary>
		/// Mouse move delta
		/// </summary>
		public static Point MoveDelta
		{
			get;
			private set;

		}


		/// <summary>
		///  Gets/sets the cursor visibility
		/// </summary>
		public static bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
				if (value)
					Cursor.Show();
				else
					Cursor.Hide();
			}
		}
		static bool visible;
 

		/// <summary>
		/// Gets and sets whether or not the mouse cursor is visible. 
		/// </summary>
		public static Cursor Cursor
		{
			get
			{
				if (Form == null)
					throw new ArgumentNullException("Form", "You must initialize the class first !");
				
				return Form.Cursor;
			}
			set
			{
				if (Form == null)
					throw new ArgumentNullException("Form", "You must initialize the class first !");
				Form.Cursor = value;
			}
		}


		/// <summary>
		/// Gets the mouse button states
		/// </summary>
		static public MouseButtons Buttons
		{
			get;
			private set;
		}


		/// <summary>
		/// Previous mouse buttons state
		/// </summary>
		static MouseButtons PreviousState;

		#endregion


		#region Events
		/// <summary>
		///  Event fired when a ButtonDown occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> ButtonDown;


		/// <summary>
		/// Event fired when a ButtonUp occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> ButtonUp;

		/// <summary>
		/// Event fired when a mouse move occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> Move;

		/// <summary>
		/// Event fired when a double click occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> DoubleClick;

		/// <summary>
		/// Occurs when the mouse wheel moves
		/// </summary>
		public static event EventHandler<MouseEventArgs> MouseWheel;


		#endregion

	}



}
