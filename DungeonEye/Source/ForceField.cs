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
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// Invisible force moving the team
	/// </summary>
	public class ForceField
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public ForceField()
		{
			Type = ForceFieldType.Turning;
			Rotation = CompassRotation.Rotate180;
			Move = CardinalPoint.North;
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

			if (xml.Name.ToLower() != "forcefield")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "type":
					{
						Type = (ForceFieldType)Enum.Parse(typeof(ForceFieldType), node.Attributes["value"].Value);
					}
					break;

					case "rotation":
					{
						Rotation = (CompassRotation)Enum.Parse(typeof(CompassRotation), node.Attributes["value"].Value);
					}
					break;

					case "move":
					{
						Move = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["value"].Value);
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


			writer.WriteStartElement("forcefield");


			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("rotation");
			writer.WriteAttributeString("value", Rotation.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("move");
			writer.WriteAttributeString("value", Move.ToString());
			writer.WriteEndElement();


			writer.WriteEndElement();

			return true;
		}



		#endregion



		#region Properties



		/// <summary>
		/// Type of force field
		/// </summary>
		public ForceFieldType Type
		{
			get;
			set;
		}

		/// <summary>
		/// Type of turn on the team
		/// </summary>
		public CompassRotation Rotation
		{
			get;
			set;
		}


		/// <summary>
		/// Moving value
		/// </summary>
		public CardinalPoint Move
		{
			get;
			set;
		}

		#endregion
	}


	/// <summary>
	/// Type of force field
	/// </summary>
	public enum ForceFieldType
	{
		/// <summary>
		/// Team is turning
		/// </summary>
		Turning,

		/// <summary>
		/// Team is moved to a direction
		/// </summary>
		Moving,

		/// <summary>
		/// Team can go through
		/// </summary>
		Blocking,
	}
}
