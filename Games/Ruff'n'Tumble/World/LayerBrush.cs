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
using ArcEngine;
using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Asset;
using System.Xml;
using System.Drawing;
using ArcEngine.Graphic;


namespace RuffnTumble
{
	/// <summary>
	/// Brush to ease level edition
	/// </summary>
	public class LayerBrush// : ResourceBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name"></param>
		public LayerBrush()//string name) : base(name)
		{
			Size = Size.Empty;
		}




		/// <summary>
		/// Draws the brush on the screen
		/// </summary>
		/// <param name="location">Location in pixel on the screen</param>
		/// <param name="tileset">Tileset to use</param>
		/// <param name="blocksize">Size in pixel of each block</param>
		public void Draw(Point location, TileSet tileset, Size blocksize)
		{
			// Oops !
			if (tileset == null)
			    return;

			// Draw each tiles 
			for(int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					tileset.Draw(Tiles[y][x], new Point(location.X + x * blocksize.Width, location.Y + y * blocksize.Height));
				}

		}


		/// <summary>
		/// Paste the brush in a layer
		/// </summary>
		/// <param name="layer">Layer reference</param>
		/// <param name="location">Location in block in the layer</param>
		public void Paste(Layer layer, Point location)
		{
			// Oops !
			if (layer == null)
				return;


			// Paste each tiles 
			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
					layer.SetTileAtBlock(new Point(location.X + x, location.Y + y), Tiles[y][x]);
		}



		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			xml.WriteStartElement("brush");
	//		xml.WriteAttributeString("name", Name);

	//		base.SaveComment(xml);


			xml.WriteStartElement("size");
			xml.WriteAttributeString("width", Size.Width.ToString());
			xml.WriteAttributeString("height", Size.Height.ToString());
			xml.WriteEndElement();

			int rowid = 0;
			foreach (List<int> row in Tiles)
			{
				xml.WriteStartElement("row");
				xml.WriteAttributeString("BufferID", rowid++.ToString());
				foreach (int id in row)
				{
					xml.WriteString(id.ToString() + " ");
				}
				xml.WriteEndElement();
			}

			xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads brush definition
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
			//		base.LoadComment(node);
					continue;
				}

				switch (node.Name.ToLower())
				{
					case "size":
					{
						Size = new Size(int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value));
					}
					break;

					// Add a row
					case "row":
					{
						int rowid = Int32.Parse(node.Attributes["id"].Value);

						string[] val = node.InnerText.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);
						for(int x = 0; x < Size.Width; x++)
							Tiles[rowid][x] = Int32.Parse(val[x]);
					}
					break;

					default:
					{
						Trace.WriteLine("Brush : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}


			return true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Brush size in block
		/// </summary>
		public Size Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;

				// Oops !
				if (size.IsEmpty)
					size = new Size(1, 1);

				//Tiles = new List<List<int>>();
				//for (int i = 0; i < size.Height; i++)
				//    Tiles.Add(new List<int>(size.Width));


				// Tiles
				Tiles = new List<List<int>>(size.Height);
				for (int y = 0; y < size.Height; y++)
				{
					Tiles.Add(new List<int>());
					for (int x = 0; x < size.Width; x++)
						Tiles[y].Add(-1);
				}

			}
		}
		Size size;



		/// <summary>
		/// Tiles ID
		/// </summary>
		public List<List<int>> Tiles;



		#endregion
	}
}
