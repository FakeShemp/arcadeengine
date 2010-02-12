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
using System.Xml;


namespace DungeonEye
{

	/// <summary>
	/// Experience points
	/// </summary>
	public class Experience
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Experience() : this (0)
		{

		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value">Initial value</param>
		public Experience(int value)
		{
			Points = value;
		}




		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("XP = {0}, Level = {1}", Points, Level);
		}



		/// <summary>
		/// Get level from xp
		/// </summary>
		/// <param name="points">Points</param>
		/// <returns></returns>
		public static int GetLevelFromXP(int points)
		{
			return (int)Math.Floor((1 + Math.Sqrt(points / 125.0f + 1)) / 2);
		}


		#region IO


		/// <summary>
		/// Loads properties
		/// </summary>
		/// <param name="node">Node</param>
		/// <returns></returns>
		public bool Load(XmlNode node)
		{
			if (node == null || node.NodeType != XmlNodeType.Element)
				return false;


			if (node.Attributes["points"] != null)
				Points = int.Parse(node.Attributes["points"].Value);

			return true;
		}



		/// <summary>
		/// Saves properties
		/// </summary>
		/// <param name="name">Name for the node</param>
		/// <param name="writer">XmlWriter</param>
		/// <returns>True if saved</returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("xp");
			writer.WriteAttributeString("points", Points.ToString());
			writer.WriteEndElement();

			return true;
		}

		#endregion



		#region Properties


		/// <summary>
		/// Experiences points
		/// </summary>
		public int Points
		{
			get;
			set;
		}

		/// <summary>
		/// Level
		/// </summary>
		public int Level
		{
			get
			{
				return GetLevelFromXP(Points);
			}
		}

		#endregion
	}
}
