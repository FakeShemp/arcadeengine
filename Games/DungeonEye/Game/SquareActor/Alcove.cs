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
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using System.ComponentModel;
using System;
using ArcEngine.Asset;
using ArcEngine.Audio;
using System.Text;

namespace DungeonEye
{

	/// <summary>
	/// Alcove on walls
	/// </summary>
	public class Alcove : SquareActor
	{

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="square">Parent square handle</param>
		public Alcove(Square square) : base(square)
		{
			Decorations = new int[4] {-1, -1, -1, -1};
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Alcove (x)");

			return sb.ToString();
		}

		/// <summary>
		/// Gets an alcove side state
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <returns>True if an alcove is on this side, or false if no alcove</returns>
		public bool GetSideState(CardinalPoint side)
		{
			return Decorations[(int)side] != -1;
		}


		/// <summary>
		/// Enables or disables an alcove on a side
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <param name="state">Tile id of the decoration</param>
		public void SetSideTile(CardinalPoint side, int tileid)
		{
			Decorations[(int)side] = tileid;
		}


		/// <summary>
		/// Gets an alcove side state
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <returns>Tile id of the decoration, or -1 if no decoration</returns>
		public int GetTile(CardinalPoint side)
		{
			return Decorations[(int)side];
		}


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != Tag)
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					default:
					{
						Trace.WriteLine("[Alcove] Load() : Unknown node \"" + node.Name + "\" found.");
					}
					break;
				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement(Tag);

			for (int i = 0; i < 4; i++)
			{
				writer.WriteStartElement("side");
				writer.WriteAttributeString("name", ((CardinalPoint)i).ToString());
				writer.WriteAttributeString("tile", (Decorations[i]).ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string Tag = "alcove";


		/// <summary>
		/// Decoration id
		/// </summary>
		int[] Decorations;

		#endregion
	}
}
