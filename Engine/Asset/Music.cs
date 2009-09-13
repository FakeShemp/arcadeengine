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
using System.Xml;


namespace ArcEngine.Asset
{

	/// <summary>
	/// Music class. Music are stream sounds
	/// </summary>
	public class Music : IAsset
	{

		#region	IO operations


		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			/*
						xml.WriteStartElement("tileset");
						xml.WriteAttributeString("name", Name);



						if (!string.IsNullOrEmpty(TextureName))
						{
							xml.WriteStartElement("texture");
							xml.WriteAttributeString("file", TextureName);
							xml.WriteEndElement();
						}

						// Loops throughs tile
						foreach (KeyValuePair<int, Tile> val in tiles)
						{
							xml.WriteStartElement("tile");

							xml.WriteAttributeString("id", val.Key.ToString());

							xml.WriteStartElement("rectangle");
							xml.WriteAttributeString("x", val.Value.Rectangle.X.ToString());
							xml.WriteAttributeString("y", val.Value.Rectangle.Y.ToString());
							xml.WriteAttributeString("width", val.Value.Rectangle.Width.ToString());
							xml.WriteAttributeString("height", val.Value.Rectangle.Height.ToString());
							xml.WriteEndElement();

							xml.WriteStartElement("hotspot");
							xml.WriteAttributeString("x", val.Value.HotSpot.X.ToString());
							xml.WriteAttributeString("y", val.Value.HotSpot.Y.ToString());
							xml.WriteEndElement();

							xml.WriteStartElement("collisionbox");
							xml.WriteAttributeString("x", val.Value.CollisionBox.X.ToString());
							xml.WriteAttributeString("y", val.Value.CollisionBox.Y.ToString());
							xml.WriteAttributeString("width", val.Value.CollisionBox.Width.ToString());
							xml.WriteAttributeString("height", val.Value.CollisionBox.Height.ToString());
							xml.WriteEndElement();

							xml.WriteEndElement();
						}


						xml.WriteEndElement();
			*/
			return true;
		}


		/// <summary>
		/// Loads the collection from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;


			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					// Texture
					case "file":
					{
						//LoadSound(node.Attributes["name"].Value);
					}
					break;

				}
			}

			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// Name of the sound
		/// </summary>
		public string Name
		{
			get;
			set;
		}



		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "music";
			}
		}


		#endregion


	}
}
