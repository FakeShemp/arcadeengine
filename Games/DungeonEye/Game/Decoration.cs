#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Interface;


namespace DungeonEye
{
	/// <summary>
	/// Information about a specific decoration
	/// </summary>
	public class Decoration
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Decoration()
		{
			TileId= new int[18];
			Location = new Point[18];
			for (int i = 0 ; i < TileId.Length ; i++)
				TileId[i] = -1;
			Swap = new bool[18];
		}




		/// <summary>
		/// Gets the tile id for a given view point
		/// </summary>
		/// <param name="position">View point position</param>
		/// <returns>Tile id or -1 if no tile</returns>
		public int GetTileId(ViewFieldPosition position)
		{
			return TileId[(int) position];
		}



		/// <summary>
		/// Sets the tile id for a given view point
		/// </summary>
		/// <param name="position">View point position</param>
		/// <param name="id">Id of the tile</param>
		public void SetTileId(ViewFieldPosition position, int id)
		{
			TileId[(int) position] = id;
		}


		/// <summary>
		/// Gets the on screen location for a given view point
		/// </summary>
		/// <param name="position">View point position</param>
		/// <returns>Screen location</returns>
		public Point GetLocation(ViewFieldPosition position)
		{
			return Location[(int) position];
		}


		/// <summary>
		/// Gets the on screen location for a given view point
		/// </summary>
		/// <param name="position">View point position</param>
		/// <param name="location">Screen location</param>
		public void SetLocation(ViewFieldPosition position, Point location)
		{
			Location[(int) position] = location;
		}


		/// <summary>
		/// Gets if the tile need to be swapped
		/// </summary>
		/// <param name="position">View field position</param>
		/// <returns>True on horizontal swap</returns>
		public bool GetSwap(ViewFieldPosition position)
		{
			return Swap[(int)position];
		}


		/// <summary>
		/// Sets if the tile need to be swapped
		/// </summary>
		/// <param name="position">View field position</param>
		public void SetSwap(ViewFieldPosition position, bool swap)
		{
			Swap[(int)position] = swap;
		}


		/// <summary>
		/// Clear definition
		/// </summary>
		public void Clear()
		{
			foreach (ViewFieldPosition pos in Enum.GetValues(typeof(ViewFieldPosition)))
			{
				SetTileId(pos, -1);
				SetLocation(pos, Point.Empty);
				SetSwap(pos, false);
			}

			IsBlocking = false;
		}


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{

			if (xml == null || xml.Name != "decoration")
				return false;

			IsBlocking = bool.Parse(xml.Attributes["isblocking"].Value);

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;

				try
				{
					ViewFieldPosition pos = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Name);
					Location[(int) pos].X = int.Parse(node.Attributes["x"].Value);
					Location[(int) pos].Y = int.Parse(node.Attributes["y"].Value);
					TileId[(int) pos] = int.Parse(node.Attributes["id"].Value);
					Swap[(int)pos] = bool.Parse(node.Attributes["swap"].Value);

				}
				catch (Exception e)
				{
					Trace.WriteLine("[Decoration]Load : Error while loading : " + e.Message);
				}
			}



			return true;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer">XmlWriter handle</param>
		/// <param name="id">Decoration Id</param>
		/// <returns></returns>
		public bool Save(XmlWriter writer, int id)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("decoration");
			writer.WriteAttributeString("id", id.ToString());
			writer.WriteAttributeString("isblocking", IsBlocking.ToString());

			foreach (ViewFieldPosition vfp in Enum.GetValues(typeof(ViewFieldPosition)))
			{
				writer.WriteStartElement(vfp.ToString());
				writer.WriteAttributeString("id", TileId[(int) vfp].ToString());
				writer.WriteAttributeString("x", Location[(int) vfp].X.ToString());
				writer.WriteAttributeString("y", Location[(int)vfp].Y.ToString());
				writer.WriteAttributeString("swap", Swap[(int)vfp].ToString());
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			return true;
		}


		#endregion



		#region properties


		/// <summary>
		/// Tile Id
		/// </summary>
		int[] TileId;


		/// <summary>
		/// Display location on the screen
		/// </summary>
		Point[] Location;


		/// <summary>
		/// Horizontal swap
		/// </summary>
		bool[] Swap;


		/// <summary>
		/// Does the decoration is blocking
		/// </summary>
		public bool IsBlocking
		{
			get;
			set;
		}

		#endregion

	}
}
