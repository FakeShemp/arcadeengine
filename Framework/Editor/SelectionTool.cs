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
using System.Text;
using System.Windows.Forms;
using ArcEngine.Graphic;


namespace ArcEngine.Editor
{

	
	/// <summary>
	/// Selection tool with sizing handles
	/// </summary>
	internal class SelectionTool
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public SelectionTool()
		{
			Color = Color.FromArgb(128, Color.White);
		}

		/// <summary>
		/// Checks the kind of action possible
		/// </summary>
		/// <param name="point">Point to test</param>
		/// <returns>True if the point hit the rectangle box</returns>
		bool IsHit(Point point)
		{
			// If mouse down, then no check
			if (MouseDown)
				return false;


			Rectangle rect = Rectangle;
			rect.X = (int) (rect.X * zoom + Offset.X);
			rect.Y = (int) (rect.Y * zoom + Offset.Y);
			rect.Width = (int) (rect.Width * zoom);
			rect.Height = (int)(rect.Height * zoom);


			if (rect.Contains(point))
			{
				IsMouseOver = true;
				if (point.X >= rect.X + 4 && point.X <= rect.X + rect.Width - 4 && point.Y >= rect.Y + 4 && point.Y <= rect.Y + rect.Height - 4)
				{
					Cursor.Current = Cursors.SizeAll;
					MouseTool = MouseTools.MoveObject;
					return true;
				}
				else
				{
					if (point.Y >= rect.Y + rect.Height - 4)
					{
						if (point.X >= rect.X + 4 && point.X <= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNS;
							MouseTool = MouseTools.SizeDown;
							return true;
						}
						else if (point.X <= rect.X + 4)
						{
							Cursor.Current = Cursors.SizeNESW;
							MouseTool = MouseTools.SizeDownLeft;
							return true;
						}
						else if (point.X >= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							MouseTool = MouseTools.SizeDownRight;
							return true;
						}
					}
					if ((point.Y <= rect.Y + 4))
					{
						if (point.X >= rect.X + 4 && point.X <= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNS;
							MouseTool = MouseTools.SizeUp;
							return true;
						}
						else if (point.X <= rect.X + 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							MouseTool = MouseTools.SizeUpLeft;
							return true;
						}
						else if (point.X >= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNESW;
							MouseTool = MouseTools.SizeUpRight;
							return true;
						}
					}
					if (point.X >= rect.X + rect.Width - 4)
					{
						if (point.Y >= rect.Y + 4 && point.Y <= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeWE;
							MouseTool = MouseTools.SizeRight;
							return true;
						}
						else if (point.Y <= rect.Y + 4)
						{
							Cursor.Current = Cursors.SizeNESW;
							MouseTool = MouseTools.SizeUpRight;
							return true;
						}
						else if (point.Y >= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							MouseTool = MouseTools.SizeDownRight;
							return true;
						}
					}
					if (point.X <= rect.X + 4)
					{
						if (point.Y >= rect.Y + 4 && point.Y <= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeWE;
							MouseTool = MouseTools.SizeLeft;
							return true;
						}
						else if (point.Y <= rect.Y + 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							MouseTool = MouseTools.SizeUpLeft;
							return true;
						}
						else if (point.Y >= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							MouseTool = MouseTools.SizeDownRight;
							return true;
						}
					}
				}

			}

			IsMouseOver = false;
			MouseTool = MouseTools.NoTool;
			return false;
		}



		/// <summary>
		/// Sets the location of the selection box
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		bool Move(Point location)
		{
			LocationChanged = Rectangle.Location != location ;

			Rectangle.X = location.X;
			Rectangle.Y = location.Y;

			return LocationChanged;
		}



		/// <summary>
		/// Resizes the Rectangle
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		bool Resize(Size size)
		{
			LocationChanged = !size.IsEmpty;


			Point offset = new Point((int) (size.Width / zoom) - Rectangle.X - (int)(Offset.X / Zoom),
									 (int) (size.Height / zoom)- Rectangle.Y - (int)(Offset.Y / Zoom));

			switch (MouseTool)
			{
				case MouseTools.SizeUp:
					Rectangle.Y += offset.Y;
					Rectangle.Height -= offset.Y;
				break;
				case MouseTools.SizeUpLeft:
					Rectangle.Y += offset.Y;
					Rectangle.Height -= offset.Y;
					Rectangle.X += offset.X;
					Rectangle.Width -= offset.X;
				break;
				case MouseTools.SizeUpRight:
					Rectangle.Y += offset.Y;
					Rectangle.Height -= offset.Y;
					Rectangle.Width = offset.X;
				break;
				case MouseTools.SizeDown:
					Rectangle.Height = offset.Y;
				break;
				case MouseTools.SizeDownLeft:
					Rectangle.Height = offset.Y;
					Rectangle.X += offset.X;
					Rectangle.Width -= offset.X;
				break;
				case MouseTools.SizeDownRight:
					Rectangle.Height = offset.Y;
					Rectangle.Width = offset.X;
				break;
				case MouseTools.SizeLeft:
					Rectangle.X += offset.X;
					Rectangle.Width -= offset.X;
				break;
				case MouseTools.SizeRight:
					Rectangle.Width = offset.X;
				break;
			}

			if (Rectangle.Height < 0)
				Rectangle.Height = 0;

			if (Rectangle.Width < 0)
				Rectangle.Width = 0;
					

			return LocationChanged;
		}



		/// <summary>
		/// Draws size handles
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		void DrawSizeHandles(SpriteBatch batch)
		{
			if (batch == null)
				return;

			Rectangle rect = Rectangle;
			rect.X = (int) (rect.X * zoom + Offset.X);
			rect.Y = (int) (rect.Y * zoom + Offset.Y);
			rect.Width = (int)(rect.Width * zoom);
			rect.Height = (int)(rect.Height * zoom);

			batch.FillRectangle(new Rectangle(rect.X, rect.Y, 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X + (rect.Width / 2) - 2, rect.Y, 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X + rect.Width - 4, rect.Y, 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X, rect.Y + (rect.Height / 2) - 2, 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X + rect.Width - 4, (rect.Y + (rect.Height / 2) - 2), 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X, rect.Y + rect.Height - 4, 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X + (rect.Width / 2) - 2, rect.Y + rect.Height - 4, 4, 4), Color);
			batch.FillRectangle(new Rectangle(rect.X + rect.Width - 4, rect.Y + rect.Height - 4, 4, 4), Color);
		}


		/// <summary>
		/// Renders the selectionbox
		/// </summary>
		public void Draw(SpriteBatch batch)
		{
			// No size ? No Draw !
			if (Rectangle.IsEmpty || batch == null)
				return;

			Rectangle rect = Rectangle;
			rect.X = (int) (rect.X * zoom + Offset.X);
			rect.Y = (int) (rect.Y * zoom + Offset.Y);
			rect.Width = (int ) (rect.Width * zoom);
			rect.Height = (int) (rect.Height * zoom);
			batch.FillRectangle(rect, Color);

			batch.DrawRectangle(rect, Color);

			if (IsMouseOver)
				DrawSizeHandles(batch);

		}



		#region Events

		/// <summary>
		/// Update the selectionbox
		/// </summary>
		/// <param name="e"></param>
		/// <returns>True if the control location size has changed</returns>
		public bool OnMouseMove(MouseEventArgs e)
		{
			// Update tool state
			IsHit(e.Location);
				//return false;

			Point pos = new Point((int) (e.Location.X / zoom), (int) (e.Location.Y / zoom));
			pos.X += Offset.X;
			pos.Y += Offset.Y;

			if (MouseDown)
			{
				if (MouseTool == MouseTools.MoveObject)
				{
					Move(new Point(pos.X - ClickDelta.X, pos.Y - ClickDelta.Y));
					return true;
				}
				else
				{
					return Resize(new Size(e.Location.X, e.Location.Y));
				}
			}

			return false;
		}



		/// <summary>
		/// OnMouseDown event
		/// </summary>
		/// <param name="e">MouseEventArgs argument</param>
		public void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ClickDelta.X = (int)(e.Location.X / zoom) - Rectangle.X + Offset.X;
				ClickDelta.Y = (int)(e.Location.Y / zoom)- Rectangle.Y + Offset.Y;

				MouseDown = true;
			}
		}


		/// <summary>
		/// OnMouseUpEvent
		/// </summary>
		/// <param name="e"></param>
		public void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ClickDelta = Point.Empty;
				MouseDown = false;
			}
		}


		#endregion


		#region Properties

		/// <summary>
		/// The selection Rectangle
		/// </summary>
		public Rectangle Rectangle = Rectangle.Empty;


		/// <summary>
		/// Size of the rectangle
		/// </summary>
		public Size Size
		{
			get
			{
				return Rectangle.Size;
			}
		}

		/// <summary>
		/// Color of the selection box
		/// </summary>
		public Color Color;


		/// <summary>
		/// Gets if the mouse is over the control
		/// </summary>
		public bool IsMouseOver
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets the current mouse tool
		/// </summary>
		public MouseTools MouseTool
		{
			get;
			set;
		}



		/// <summary>
		/// Delta of the top left corner with the mouse on an OnMouseDown event
		/// </summary>
		Point ClickDelta = Point.Empty;


		/// <summary>
		/// True if left mouse button down
		/// </summary>
		bool MouseDown = false;


		/// <summary>
		/// Gets if location changed
		/// </summary>
		public bool LocationChanged
		{
			get;
			private set;
		}



		/// <summary>
		/// Zoom value
		/// </summary>
		public float Zoom
		{
			get
			{
				return zoom;
			}
			set
			{
				zoom = value;
				if (zoom == 0.0f)
					zoom = 1.0f;
			}
		}
		float zoom = 1.0f;



		/// <summary>
		/// Translation value
		/// </summary>
		public Point Offset;

	
		#endregion

	}
	
	/// <summary>
	/// 
	/// </summary>
	public enum MouseTools
	{

		/// <summary>
		/// 
		/// </summary>
		NoTool,

		/// <summary>
		/// 
		/// </summary>
		MoveObject,

		/// <summary>
		/// 
		/// </summary>
		SizeUp,

		/// <summary>
		/// 
		/// </summary>
		SizeUpLeft,

		/// <summary>
		/// 
		/// </summary>
		SizeUpRight,

		/// <summary>
		/// 
		/// </summary>
		SizeDown,

		/// <summary>
		/// 
		/// </summary>
		SizeDownLeft,

		/// <summary>
		/// 
		/// </summary>
		SizeDownRight,

		/// <summary>
		/// 
		/// </summary>
		SizeLeft,

		/// <summary>
		/// 
		/// </summary>
		SizeRight
	};


}
