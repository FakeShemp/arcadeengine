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


namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// Basic Window object. 
	/// Provides a Client area which new Controls are added to.
	/// </summary>
	public class Window : Container
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title of the window</param>
		public Window(string title)
		{
		}


		/// <summary>
		/// Update the window
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="time">Elapsed time</param>
		public override void Update(GuiManager manager, GameTime time)
		{
			// Update window


			// Update container
			base.Update(manager, time);
		}



		/// <summary>
		/// Draws the window
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="batch">SpriteBatch to use</param>
		public override void Draw(GuiManager manager, SpriteBatch batch)
		{
			if (!Visible)
				return;


			// No skinning
			if (manager.TileSet == null)
			{
				batch.FillRectangle(Rectangle, BgColor);
			}
			else
			{
				// Corner tiles
				Tile tiletl = manager.TileSet.GetTile(0);
				Tile tiletr = manager.TileSet.GetTile(2);
				Tile tilebl = manager.TileSet.GetTile(6);
				Tile tilebr = manager.TileSet.GetTile(8);


				// Draw corners
				batch.DrawTile(manager.TileSet, 0, new Point(Rectangle.Left - tiletl.Size.Width, Rectangle.Top - tiletl.Size.Height), Color);
				batch.DrawTile(manager.TileSet, 2, new Point(Rectangle.Right, Rectangle.Top - tiletr.Size.Height), Color);
				batch.DrawTile(manager.TileSet, 8, new Point(Rectangle.Right, Rectangle.Bottom), Color);
				batch.DrawTile(manager.TileSet, 6, new Point(Rectangle.Left - tiletl.Size.Width, Rectangle.Bottom), Color);

				// Top bar
				Tile tile = manager.TileSet.GetTile(1);
				Rectangle dst = new Rectangle(Rectangle.Left, Rectangle.Top - tile.Size.Height, Rectangle.Width, tile.Size.Height);
				batch.DrawTile(manager.TileSet, 1, dst.Location, dst, Color);


				// Bottom bar
				tile = manager.TileSet.GetTile(7);
				dst = new Rectangle(Rectangle.Left, Rectangle.Bottom, Rectangle.Width, tile.Size.Height);
				batch.DrawTile(manager.TileSet, 7, dst.Location, dst, Color);


				// Right side
				tile = manager.TileSet.GetTile(5);
				dst = new Rectangle(Rectangle.Right, Rectangle.Top, tiletr.Size.Width, Rectangle.Height);
				batch.DrawTile(manager.TileSet, 5, dst.Location, dst, Color);


				// Left side
				tile = manager.TileSet.GetTile(3);
				dst = new Rectangle(Rectangle.Left - tiletl.Size.Height, Rectangle.Top, tiletl.Size.Width, Rectangle.Height);
				batch.DrawTile(manager.TileSet, 3, dst.Location, dst, Color);

				// Background
				batch.DrawTile(manager.TileSet, 4, Rectangle.Location, Rectangle, Color);



				// Draw all controls in the window
				base.Draw(manager, batch);
			}
		}



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
}
