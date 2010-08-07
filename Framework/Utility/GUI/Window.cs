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
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine;

namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// Basic Window object. 
	/// Provides a client area which new controls are added to.
	/// </summary>
	public class Window : ContainerControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title of the window</param>
		public Window(string title)
		{
			Title = title;
		}


		/// <summary>
		/// Gets the part of the window at a given location
		/// </summary>
		/// <param name="location">Location relative to the top left of the window</param>
		/// <returns>Part of the window</returns>
		public HitTest GetHitTest(Point location)
		{


			return HitTest.Error;
		}

		#region OnEvent

		/// <summary>
		/// Draws the window
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Draw all controls in the window
			base.OnPaint(e);

	
			if (!Visible)
				return;


			// No skinning
			if (e.Manager.TileSet == null)
			{
				e.Batch.FillRectangle(Rectangle, BgColor);
			}
			else
			{
				// Corner tiles
				Tile tiletl = e.Manager.TileSet.GetTile(0);
				Tile tiletr = e.Manager.TileSet.GetTile(2);
				Tile tilebl = e.Manager.TileSet.GetTile(6);
				Tile tilebr = e.Manager.TileSet.GetTile(8);


				// Draw corners
				e.Batch.DrawTile(e.Manager.TileSet, 0, new Point(Rectangle.Left - tiletl.Size.Width, Rectangle.Top - tiletl.Size.Height), Color);
				e.Batch.DrawTile(e.Manager.TileSet, 2, new Point(Rectangle.Right, Rectangle.Top - tiletr.Size.Height), Color);
				e.Batch.DrawTile(e.Manager.TileSet, 8, new Point(Rectangle.Right, Rectangle.Bottom), Color);
				e.Batch.DrawTile(e.Manager.TileSet, 6, new Point(Rectangle.Left - tiletl.Size.Width, Rectangle.Bottom), Color);

				// Top bar
				Tile tile = e.Manager.TileSet.GetTile(1);
				Rectangle dst = new Rectangle(Rectangle.Left, Rectangle.Top - tile.Size.Height, Rectangle.Width, tile.Size.Height);
				e.Batch.DrawTile(e.Manager.TileSet, 1, dst.Location, dst, Color);


				// Bottom bar
				tile = e.Manager.TileSet.GetTile(7);
				dst = new Rectangle(Rectangle.Left, Rectangle.Bottom, Rectangle.Width, tile.Size.Height);
				e.Batch.DrawTile(e.Manager.TileSet, 7, dst.Location, dst, Color);


				// Right side
				tile = e.Manager.TileSet.GetTile(5);
				dst = new Rectangle(Rectangle.Right, Rectangle.Top, tiletr.Size.Width, Rectangle.Height);
				e.Batch.DrawTile(e.Manager.TileSet, 5, dst.Location, dst, Color);


				// Left side
				tile = e.Manager.TileSet.GetTile(3);
				dst = new Rectangle(Rectangle.Left - tiletl.Size.Height, Rectangle.Top, tiletl.Size.Width, Rectangle.Height);
				e.Batch.DrawTile(e.Manager.TileSet, 3, dst.Location, dst, Color);

				// Background
				e.Batch.DrawTile(e.Manager.TileSet, 4, Rectangle.Location, Rectangle, Color);

			}
		}

		#endregion


		#region IO routines


		/// <summary>
		/// Save the window
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("rectangle");
			writer.WriteAttributeString("x", Rectangle.X.ToString());
			writer.WriteAttributeString("y", Rectangle.Y.ToString());
			writer.WriteAttributeString("width", Rectangle.Width.ToString());
			writer.WriteAttributeString("height", Rectangle.Height.ToString());
			writer.WriteEndElement();


			writer.WriteStartElement("color");
			writer.WriteAttributeString("r", Color.R.ToString());
			writer.WriteAttributeString("g", Color.G.ToString());
			writer.WriteAttributeString("b", Color.B.ToString());
			writer.WriteAttributeString("a", Color.A.ToString());
			writer.WriteEndElement();



			writer.WriteStartElement("bgcolor");
			writer.WriteAttributeString("r", BgColor.R.ToString());
			writer.WriteAttributeString("g", BgColor.G.ToString());
			writer.WriteAttributeString("b", BgColor.B.ToString());
			writer.WriteAttributeString("a", BgColor.A.ToString());
			writer.WriteEndElement();


			return true;
		}


		/// <summary>
		/// Loads window definition
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public override bool Load(XmlNode node)
		{
			if (node == null)
				return false;

			switch (node.Name.ToLower())
			{

				// Main color
				case "color":
				{
					Color = Color.FromArgb(Int32.Parse(node.Attributes["a"].Value),
					Int32.Parse(node.Attributes["r"].Value),
					Int32.Parse(node.Attributes["g"].Value),
					Int32.Parse(node.Attributes["b"].Value));
				}
				break;

				// Background color
				case "bgcolor":
				{
					BgColor = Color.FromArgb(Int32.Parse(node.Attributes["a"].Value),
					Int32.Parse(node.Attributes["r"].Value),
					Int32.Parse(node.Attributes["g"].Value),
					Int32.Parse(node.Attributes["b"].Value));
				}
				break;


				// Rectangle of the element
				case "rectangle":
				{

					Rectangle rect = Rectangle.Empty;
					rect.X = Int32.Parse(node.Attributes["x"].Value);
					rect.Y = Int32.Parse(node.Attributes["y"].Value);
					rect.Width = Int32.Parse(node.Attributes["width"].Value);
					rect.Height = Int32.Parse(node.Attributes["height"].Value);

					Rectangle = rect;
				}
				break;


				// Unknown...
				default:
				{
					return false;
				}
			}

			return true;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Tile of the window
		/// </summary>
		public string Title;

		#endregion

	}


	/// <summary>
	/// Part of the window corresponds to a particular screen coordinate
	/// </summary>
	public enum HitTest
	{
		/// <summary>
		/// In the border of a window that does not have a sizing border.
		/// </summary>
		Border,

		/// <summary>
		/// In the lower-horizontal border of a resizable window
		/// (the user can click the mouse to resize the window vertically).
		/// </summary>
		Bottom,

		/// <summary>
		/// In the lower-left corner of a border of a resizable window 
		/// (the user can click the mouse to resize the window diagonally).
		/// </summary>
		BottomLeft,

		/// <summary>
		/// In the lower-right corner of a border of a resizable window 
		/// (the user can click the mouse to resize the window diagonally).
		/// </summary>
		BottomRight,

		/// <summary>
		/// In a title bar.
		/// </summary>
		Caption,

		/// <summary>
		/// In a client area.
		/// </summary>
		ClientArea,

		/// <summary>
		/// In a Close button.
		/// </summary>
		Close,

		/// <summary>
		/// On the screen background or on a dividing line between windows 
		/// (same as HTNOWHERE, except that the DefWindowProc function 
		/// produces a system beep to indicate an error).
		/// </summary>
		Error,

		/// <summary>
		/// In a Help button.
		/// </summary>
		Help,

		/// <summary>
		/// In a horizontal scroll bar.
		/// </summary>
		HScroll,

		/// <summary>
		/// In the left border of a resizable window 
		/// (the user can click the mouse to resize the window horizontally).
		/// </summary>
		Left,

		/// <summary>
		/// In a menu.
		/// </summary>
		Menu,

		/// <summary>
		/// In a Maximize button.
		/// </summary>
		MaxButton,

		/// <summary>
		/// In a Minimize button.
		/// </summary>
		MinButton,

		/// <summary>
		/// On the screen background or on a dividing line between windows.
		/// </summary>
		NoWhere,

		/// <summary>
		/// In a Minimize button.
		/// </summary>
		Reduce,

		/// <summary>
		/// In the right border of a resizable window 
		/// (the user can click the mouse to resize the window horizontally).
		/// </summary>
		Right,

		/// <summary>
		/// In a size box
		/// </summary>
		Size,

		/// <summary>
		/// In a window menu or in a Close button in a child window.
		/// </summary>
		SystemMenu,

		/// <summary>
		/// In the upper-horizontal border of a window
		/// </summary>
		Top,

		/// <summary>
		/// In the upper-left corner of a window border.
		/// </summary>
		TopLeft,

		/// <summary>
		/// In the upper-right corner of a windows border
		/// </summary>
		TopRight,


	}
}
