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
using ArcEngine.Asset;
using System.Xml;

// http://dmreference.com/SRD/Magic.htm
//
//
//
namespace DungeonEye
{
	/// <summary>
	/// Spell class
	/// </summary>
	class Spell : IAsset
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Spell()
		{
			IsDisposed = false;
		}


		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}

	


		#region Load & Save

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != "spell")
				return false;

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "type":
					{
						//Type = (ItemType)Enum.Parse(typeof(ItemType), node.Attributes["value"].Value, true);
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
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("spell");
			writer.WriteAttributeString("name", Name);





			//writer.WriteStartElement("speed");
			//writer.WriteAttributeString("value", Speed.ToString());
			//writer.WriteEndElement();



			writer.WriteEndElement();

			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// Name of the spell
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Is asset disposed
		/// </summary>
		public bool IsDisposed { get; private set; }



		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "spell";
			}
		}


		///// <summary>
		///// Description of the spell
		///// </summary>
		//public string Description
		//{
		//   get;
		//   set;
		//}



		/// <summary>
		/// How long the magical energy of the spell lasts
		/// </summary>
		public TimeSpan Duration
		{
			get;
			set;
		}


		/// <summary>
		/// Time to cast the spell
		/// </summary>
		public TimeSpan CastingTime
		{
			get;
			set;
		}


		/// <summary>
		/// Action range
		/// </summary>
		public SpellRange Range
		{
			get;
			set;
		}


		/// <summary>
		/// Required level for this spell
		/// </summary>
		public int Level
		{
			get;
			set;
		}


		
		#endregion
	}


	/// <summary>
	/// Indicates how far from the entity a spell can reach
	/// </summary>
	public enum SpellRange
	{
		/// <summary>
		/// The spell affects only you.
		/// </summary>
		Personal,


		/// <summary>
		/// You must touch a creature or object to affect it. 
		/// </summary>
		Touch,


		/// <summary>
		/// The spell reaches as far as 3 blocks away from you. The maximum range increases by 1 block for every two full caster levels.
		/// </summary>
		Close,


		/// <summary>
		/// The spell reaches as far as 5 blocks + 1 block per caster level.
		/// </summary>
		Medium,


		/// <summary>
		/// The spell reaches as far as 10 blocks + 2 blocks per caster level.
		/// </summary>
		Long,


		/// <summary>
		/// The spell reaches anywhere on the same plane of existence.
		/// </summary>
		Unlimited,

	}
}
