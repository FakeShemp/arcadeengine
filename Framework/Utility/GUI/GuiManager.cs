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
using System.Text;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Drawing;
using ArcEngine.Input;


namespace ArcEngine.Utility.GUI
{

	/// <summary>
	/// Graphical User Interface manager
	/// </summary>
	/// <remarks>The GUIManager handles resource deallocation of all Control's it contains. 
	/// Once you add a Control you don't own anymore the pointer. Deleting the Control outside of the GUIManager will result in crashes.</remarks>
	public class GuiManager : IDisposable
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public GuiManager()
		{
			Controls = new List<Control>();

			Batch = new SpriteBatch();
		}




		/// <summary>
		/// Disposes resources
		/// </summary>
		public void Dispose()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;


			IsDisposed= true;
		}




		#region Update and Draw

		/// <summary>
		/// Updates elements
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{

			// Find the control under the mouse
			Control previouscontrol = ControlUnderMouse;
			ControlUnderMouse = ControlFromPoint(Mouse.Location);


			// MouseLeave/MouseEnter event
			if (previouscontrol != ControlUnderMouse)
			{
				if (previouscontrol != null)
					previouscontrol.ProcessMessage(Message.Create(ControlMessage.MouseLeave, null, null));

				if (ControlUnderMouse != null)
					ControlUnderMouse.ProcessMessage(Message.Create(ControlMessage.MouseEnter, null, null));

			}
		}



		/// <summary>
		/// Draws elements
		/// </summary>
		public void Draw()
		{
			Message message = Message.Create(ControlMessage.Paint, this, null);

			Batch.Begin();

			foreach (Control element in Controls)
			{
				DrawChild(element, message);
			}

			Batch.End();
		}



		/// <summary>
		/// Draw the control and children
		/// </summary>
		/// <param name="control">Parent control</param>
		/// <param name="msg">Message</param>
		private void DrawChild(Control control, Message msg)
		{
			control.ProcessMessage(msg);

			foreach (Control ctrl in control.Controls)
			{
				ctrl.ProcessMessage(msg);
				
				DrawChild(ctrl, msg);
			}
		}

		#endregion



		/// <summary>
		/// Retrieves a handle to the control that contains the specified point. 
		/// </summary>
		/// <param name="loc">The point to be checked</param>
		/// <returns>The return value is a handle to the window that contains the point.
		/// If no window exists at the given point, the return value is NULL</returns>
		public Control ControlFromPoint(Point loc)
		{
			foreach (Control control in Controls)
			{
				// Point outside the control
				if (!control.Rectangle.Contains(loc))
					continue;


				Point point = new Point(loc.X - control.Location.X, loc.Y - control.Location.Y);
				return control.GetChildAtPoint(point);
			}


			return null;
		}

		
		#region Element management

		/// <summary>
		/// Removes all gui elements
		/// </summary>
		public void Clear()
		{
			Controls.Clear();
		}


		/// <summary>
		/// Adds an element
		/// </summary>
		/// <param name="element"></param>
		public void Add(Control element)
		{
			if (element == null)
				return;


			Controls.Add(element);
		}


		/// <summary>
		/// Removes an element
		/// </summary>
		/// <param name="element"></param>
		public void Remove(Control element)
		{
			if (element == null)
				return;

			Controls.Remove(element);
		}


		#endregion


		#region Properties


		/// <summary>
		/// Control under the mouse
		/// </summary>
		public Control ControlUnderMouse
		{
			get;
			private set;
		}


		/// <summary>
		/// List of all gui elements
		/// </summary>
		List<Control> Controls;



		/// <summary>
		/// Font to draw text
		/// </summary>
		public BitmapFont Font;


		/// <summary>
		/// Tileset to use
		/// </summary>
		public TileSet TileSet;


		/// <summary>
		/// Resource disposed
		/// </summary>
		public bool IsDisposed
		{
			get;
			private set;
		}


		/// <summary>
		/// SpriteBatch
		/// </summary>
		internal SpriteBatch Batch
		{
			get;
			private set;
		}

		#endregion

	}
}
