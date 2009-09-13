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
using ArcEngine.Asset;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine;


namespace DungeonEye
{
	/// <summary>
	/// 
	/// </summary>
	public class ItemSet : IAsset
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public ItemSet()
		{
			Items = new Dictionary<string, Item>();
		}



		#region Load & Save

		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml">Asset definition</param>
		/// <returns>True if successful, or false if an error occured</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name != "itemset")
			{
				Trace.WriteLine("Expecting \"itemset\" in node header, found \"" + xml.Name + "\" when loading ItemSet.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						TileSetName = node.Attributes["name"].Value;
					}
					break;

					case "item":
					{
						Item item = new Item();
						item.Load(node);

						Items[item.Name] = item;
					}
					break;

					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
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
				return false;

			writer.WriteStartElement("itemset");
			writer.WriteAttributeString("name", TileSetName);

			writer.WriteStartElement("tileset");
			writer.WriteAttributeString("name", TileSetName);
			writer.WriteEndElement();

			writer.WriteStartElement("script");
			writer.WriteAttributeString("name", ScriptName);
			writer.WriteEndElement();
		
			foreach (KeyValuePair<string, Item> kvp in Items)
				kvp.Value.Save(writer);

			writer.WriteEndElement();


			return true;
		}

		#endregion



		#region Item managemnt

/*
		/// <summary>
		/// Creates a new item
		/// </summary>
		/// <param name="name"></param>
		/// <returns>Null if item name already in use</returns>
		public Item CreateItem(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			//foreach (Item itm in Items)
			foreach (KeyValuePair<string, Item> kvp in Items)
			{
				if (itm.Name == name)
					return null;
			}


			Item item = new Item();
			item.Name = name;
			Items.Add(item);


			return item;
		}


		/// <summary>
		/// Returns an item by its ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Item GetItem(int id)
		{
			foreach (Item item in Items)
			{
				if (item.ID == id)
					return item;
			}

			return null;
		}
*/

		/// <summary>
		/// Returns an item by its name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Item GetItem(string name)
		{
			if (!Items.ContainsKey(name))
				return null;


			return Items[name];
		}

		#endregion



		#region Properties

		/// <summary>
		/// Name of the asset
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
				return "itemset";
			}
		}


		/// <summary>
		/// All items
		/// </summary>
		[Browsable(false)]
		public Dictionary<string, Item> Items
		{
			get;
			private set;
		}

		/// <summary>
		/// Name of the Tileset
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[DescriptionAttribute("TileSet name to use")]
		public string TileSetName
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the script to use
		/// </summary>
		[TypeConverter(typeof(ScriptEnumerator))]
		[DescriptionAttribute("Script name to use")]
		public string ScriptName
		{
			get;
			set;
		}

		#endregion

	}
}
