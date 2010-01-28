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
	/// Profession information
	/// </summary>
	public class Profession
	{
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public Profession() : this(0, HeroClass.Undefined)
		{

		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="xp">XP points</param>
		/// <param name="classe">Class</param>
		public Profession(int xp, HeroClass classe)
		{
			Experience = new Experience(xp);
			Class = classe;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}, level {1}", Class.ToString(), Experience.Level.ToString());
		}


		/// <summary>
		/// Add xp
		/// </summary>
		/// <param name="amount">Amount of points to add</param>
		/// <returns>True if level up</returns>
		public bool AddXP(int amount)
		{
			int level = Experience.Level;

			Experience.Points += amount;

			return level != Experience.Level;
		}



		#region IO


		/// <summary>
		/// Loads properties
		/// </summary>
		/// <param name="node">Node</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.NodeType != XmlNodeType.Element)
				return false;



			foreach (XmlNode node in xml.ChildNodes)
			{
				switch (node.Name.ToLower())
				{
					case "class":
					{
						Class = (HeroClass)Enum.Parse(typeof(HeroClass), node.Attributes["name"].Value);
					}
					break;

					case "xp":
					{
						Experience.Load(node);
					}
					break;
				}
			}

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

			writer.WriteStartElement("profession");
			
			writer.WriteStartElement("class");
			writer.WriteAttributeString("name", Class.ToString());
			writer.WriteEndElement();

			Experience.Save(writer);
	
			writer.WriteEndElement();

			return true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Profression
		/// </summary>
		public HeroClass Class
		{
			get;
			private set;
		}


		/// <summary>
		/// Experience
		/// </summary>
		public Experience Experience
		{
			get;
			private set;
		}


		#endregion
	}

}
