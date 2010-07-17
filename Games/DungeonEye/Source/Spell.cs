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


namespace DungeonEye.Source
{
	/// <summary>
	/// Spell class
	/// </summary>
	class Spell : IAsset
	{

		/// <summary>
		/// 
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
			if (xml.Name != "spell")
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
		/// Spell's duration
		/// </summary>
		public TimeSpan Duration
		{
			get;
			set;
		}

		/// <summary>
		/// Action range
		/// </summary>
		/// <remarks>0 means the team only</remarks>
		public int Range
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
}
