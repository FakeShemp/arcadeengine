#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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


namespace DungeonEye
{
	/// <summary>
	/// 
	/// </summary>
	public class Stair
	{

		/// <summary>
		/// 
		/// </summary>
		public Stair(Square block)
		{
			if (block == null)
				throw new ArgumentNullException("block");

			Block = block;

			Target = new DungeonLocation(Block.Location);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Stairs going " + Type + " (target " + Target + ")");

			return sb.ToString();
		}


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name.ToLower() != "stair")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "target":
					{
						Target.Load(node);
					}
					break;


					case "type":
					{
						Type = (StairType)Enum.Parse(typeof(StairType), node.Attributes["value"].Value);
					}
					break;
				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("stair");

			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			Target.Save("target", writer);

			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		Square Block;

	
		/// <summary>
		/// Target of the stair
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}



		/// <summary>
		/// Type of stair
		/// </summary>
		public StairType Type
		{
			get;
			set;
		}


		#endregion
	}


	/// <summary>
	/// Stair type
	/// </summary>
	public enum StairType
	{
		/// <summary>
		/// Going up
		/// </summary>
		Up,


		/// <summary>
		/// Going down
		/// </summary>
		Down
	}
}
