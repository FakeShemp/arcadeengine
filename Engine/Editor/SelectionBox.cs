using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Graphic;


namespace ArcEngine.Editor
{

	
	/// <summary>
	/// Selection box with sizing handles
	/// </summary>
	internal class SelectionBox
	{
		/// <summary>
		/// 
		/// </summary>
		public enum MouseTools
		{
			NoTool,
			MoveObject,
			SizeUp,
			SizeUpLeft,
			SizeUpRight,
			SizeDown,
			SizeDownLeft,
			SizeDownRight,
			SizeLeft,
			SizeRight
		};


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
		public Color Color = Color.White;


		/// <summary>
		/// Gets if the mouse is over the control
		/// </summary>
		public bool IsMouseOver
		{
			get
			{
				return isMouseOver;
			}
		}
		bool isMouseOver;


		/// <summary>
		/// Gets the current mouse tool
		/// </summary>
		public MouseTools MouseTool
		{
			get
			{
				return mouseTool;
			}
			set
			{
				mouseTool = value;
			}
		}
		MouseTools mouseTool;



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
			get
			{
				return locationChanged;
			}
		}
		bool locationChanged;



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



		/// <summary>
		/// Checks the kind of action possible
		/// </summary>
		/// <param name="point"></param>
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
				isMouseOver = true;
				if (point.X >= rect.X + 4 && point.X <= rect.X + rect.Width - 4 && point.Y >= rect.Y + 4 && point.Y <= rect.Y + rect.Height - 4)
				{
					Cursor.Current = Cursors.SizeAll;
					mouseTool = MouseTools.MoveObject;
					return true;
				}
				else
				{
					if (point.Y >= rect.Y + rect.Height - 4)
					{
						if (point.X >= rect.X + 4 && point.X <= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNS;
							mouseTool = MouseTools.SizeDown;
							return true;
						}
						else if (point.X <= rect.X + 4)
						{
							Cursor.Current = Cursors.SizeNESW;
							mouseTool = MouseTools.SizeDownLeft;
							return true;
						}
						else if (point.X >= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							mouseTool = MouseTools.SizeDownRight;
							return true;
						}
					}
					if ((point.Y <= rect.Y + 4))
					{
						if (point.X >= rect.X + 4 && point.X <= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNS;
							mouseTool = MouseTools.SizeUp;
							return true;
						}
						else if (point.X <= rect.X + 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							mouseTool = MouseTools.SizeUpLeft;
							return true;
						}
						else if (point.X >= rect.X + rect.Width - 4)
						{
							Cursor.Current = Cursors.SizeNESW;
							mouseTool = MouseTools.SizeUpRight;
							return true;
						}
					}
					if (point.X >= rect.X + rect.Width - 4)
					{
						if (point.Y >= rect.Y + 4 && point.Y <= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeWE;
							mouseTool = MouseTools.SizeRight;
							return true;
						}
						else if (point.Y <= rect.Y + 4)
						{
							Cursor.Current = Cursors.SizeNESW;
							mouseTool = MouseTools.SizeUpRight;
							return true;
						}
						else if (point.Y >= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							mouseTool = MouseTools.SizeDownRight;
							return true;
						}
					}
					if (point.X <= rect.X + 4)
					{
						if (point.Y >= rect.Y + 4 && point.Y <= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeWE;
							mouseTool = MouseTools.SizeLeft;
							return true;
						}
						else if (point.Y <= rect.Y + 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							mouseTool = MouseTools.SizeUpLeft;
							return true;
						}
						else if (point.Y >= rect.Y + rect.Height - 4)
						{
							Cursor.Current = Cursors.SizeNWSE;
							mouseTool = MouseTools.SizeDownRight;
							return true;
						}
					}
				}

			}

			isMouseOver = false;
			mouseTool = MouseTools.NoTool;
			return false;
		}



		/// <summary>
		/// Sets the location of the selection box
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		bool Move(Point location)
		{
			locationChanged = Rectangle.Location != location ;

			Rectangle.X = location.X;
			Rectangle.Y = location.Y;

			return locationChanged;
		}



		/// <summary>
		/// Resizes the Rectangle
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		bool Resize(Size size)
		{
			locationChanged = !size.IsEmpty;


			Point offset = new Point((int) (size.Width / zoom) - Rectangle.X - (int)(Offset.X / Zoom),
									 (int) (size.Height / zoom)- Rectangle.Y - (int)(Offset.Y / Zoom));

			switch (mouseTool)
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
					

			return locationChanged;
		}



		/// <summary>
		/// Draws size handles
		/// </summary>
		void DrawSizeHandles()
		{
			Rectangle rect = Rectangle;
			rect.X = (int) (rect.X * zoom + Offset.X);
			rect.Y = (int) (rect.Y * zoom + Offset.Y);
			rect.Width = (int)(rect.Width * zoom);
			rect.Height = (int)(rect.Height * zoom);

			Display.Rectangle(new Rectangle(rect.X, rect.Y, 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X + (rect.Width / 2) - 2, rect.Y, 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X + rect.Width - 4, rect.Y, 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X, rect.Y + (rect.Height / 2) - 2, 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X + rect.Width - 4, (rect.Y + (rect.Height / 2) - 2), 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X, rect.Y + rect.Height - 4, 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X + (rect.Width / 2) - 2, rect.Y + rect.Height - 4, 4, 4), true);
			Display.Rectangle(new Rectangle(rect.X + rect.Width - 4, rect.Y + rect.Height - 4, 4, 4), true);
		}


		/// <summary>
		/// Renders the selectionbox
		/// </summary>
		public void Draw()
		{
			// No size ? No Draw !
			if (Rectangle.IsEmpty)
				return;


			Color color = Display.Color;
			bool blending = Display.Blending;


			Display.Color = Color;
			Display.Blending = false;

			Rectangle rect = Rectangle;
			rect.X = (int) (rect.X * zoom + Offset.X);
			rect.Y = (int) (rect.Y * zoom + Offset.Y);
			rect.Width = (int ) (rect.Width * zoom);
			rect.Height = (int) (rect.Height * zoom);
			Display.Rectangle(rect, false);

			if (isMouseOver)
				DrawSizeHandles();


			Display.Color = color;
			Display.Blending = blending;
		}


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
				if (mouseTool == MouseTools.MoveObject)
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


	}
}
