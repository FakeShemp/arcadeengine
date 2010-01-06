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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;


//
//
// http://ddo.mmodb.com/data_edit.php?table_name=ddo_items&ID=244&action=edit_data
//
//
//
//
//
//
//

namespace DungeonEye
{


	/// <summary>
	/// Item class
	/// 
	/// 
	/// http://eob.wikispaces.com/eob.itemtype
	/// http://eob.wikispaces.com/eob.itemdat
	/// </summary>
	public class Item
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Item()
		{
			Classes = HeroClass.Cleric | HeroClass.Fighter | HeroClass.Mage | HeroClass.Paladin | HeroClass.Ranger | HeroClass.Thief;
			Damage = new Dice();
		}



		#region Load & Save

		/// <summary>
		/// Loads an item definition
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns>True if loaded, or false</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != "item")
				return false;

			//ID = int.Parse(xml.Attributes["id"].Value);
			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
						InterfaceName = node.Attributes["interface"].Value;
					}
					break;

					case "type":
					{
						Type = (ItemType)Enum.Parse(typeof(ItemType), node.Attributes["value"].Value, true);
					}
					break;

					case "slot":
					{
						Slot |= (BodySlot)Enum.Parse(typeof(BodySlot), node.Attributes["value"].Value, true);
					}
					break;

					case "weight":
					{
						Weight = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "damage":
					{
						Damage.Load(node);
					}
					break;

					case "critical":
					{
						Critical = new Point(int.Parse(node.Attributes["min"].Value), int.Parse(node.Attributes["max"].Value));
						CriticalMultiplier = int.Parse(node.Attributes["multiplier"].Value);
					}
					break;

					case "description":
					{
						Description = node.InnerText;
					} 
					break;

					case "speed":
					{
						Speed = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "tile":
					{
						TileID = int.Parse(node.Attributes["inventory"].Value);
						GroundTileID = int.Parse(node.Attributes["ground"].Value);
						IncomingTileID = int.Parse(node.Attributes["incoming"].Value);
						ThrowTileID = int.Parse(node.Attributes["moveaway"].Value);
					}
					break;

					case "classes":
					{
						Classes = (HeroClass)Enum.Parse(typeof(HeroClass), node.Attributes["value"].Value);
					}
					break;

					case "usequiver":
					{
						UseQuiver = (bool)Boolean.Parse(node.Attributes["value"].Value);
					}
					break;

				}
			}



			return true;
		}



		/// <summary>
		/// Saves the item definition
		/// </summary>
		/// <param name="writer">Xml writer handle</param>
		/// <returns>True if saved, or false</returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("item");
			writer.WriteAttributeString("name", Name);



			writer.WriteStartElement("tile");
			writer.WriteAttributeString("inventory", TileID.ToString());
			writer.WriteAttributeString("ground", GroundTileID.ToString());
			writer.WriteAttributeString("incoming", IncomingTileID.ToString());
			writer.WriteAttributeString("moveaway", ThrowTileID.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("slot");
			writer.WriteAttributeString("value", Slot.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("classes");
			writer.WriteAttributeString("value", Classes.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("usequiver");
			writer.WriteAttributeString("value", UseQuiver.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("weight");
			writer.WriteAttributeString("value", Weight.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("damage");
			Damage.Save(writer);
			writer.WriteEndElement();

			writer.WriteStartElement("critical");
			writer.WriteAttributeString("min", Critical.X.ToString());
			writer.WriteAttributeString("max", Critical.Y.ToString());
			writer.WriteAttributeString("multiplier", CriticalMultiplier.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("script");
			writer.WriteAttributeString("name", ScriptName);
			writer.WriteAttributeString("interface", InterfaceName);
			writer.WriteEndElement();


			writer.WriteElementString("description", Description);


			writer.WriteStartElement("speed");
			writer.WriteAttributeString("value", Speed.ToString());
			writer.WriteEndElement();

			writer.WriteEndElement();

			return true;
		}

		#endregion



		#region Properties


		/// <summary>
		/// Script name
		/// </summary>
		public string ScriptName
		{
			get;
			set;
		}

		
		/// <summary>
		/// Interface name for the script
		/// </summary>
		public string InterfaceName
		{
			get;
			set;
		}

		/// <summary>
		/// Script of the item
		/// </summary>
		public Script Script
		{
			get;
			private set;
		}


		/// <summary>
		/// Name of the item
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Type of the item
		/// </summary>
		public ItemType Type
		{
			get;
			set;
		}


		/// <summary>
		/// Allowed slot for the item
		/// </summary>
		public BodySlot Slot
		{
			get;
			set;
		}


		/// <summary>
		/// Allowed classes
		/// </summary>
		public HeroClass Classes
		{
			get;
			set;
		}

		/// <summary>
		/// Weight of the item
		/// </summary>
		public int Weight
		{
			get;
			set;
		}


		/// <summary>
		/// Damage
		/// </summary>
		public Dice Damage
		{
			get;
			private set;
		}


		/// <summary>
		/// Critical Hit
		/// </summary>
		/// <remarks>X = minimal, Y = maximal</remarks>
		public Point Critical
		{
			get;
			set;
		}


		/// <summary>
		/// Multiplier for Critical Hit
		/// </summary>
		public int CriticalMultiplier
		{
			get;
			set;
		}


		/// <summary>
		/// Description of the item
		/// </summary>
		public string Description
		{
			get;
			set;
		}


		/// <summary>
		/// Time to wait before next use of the item in ms
		/// </summary>
		public int Speed
		{
			get;
			set;
		}
	
	
		/// <summary>
		/// Tile ID of the item in inventory
		/// </summary>
		public int TileID
		{
			get;
			set;
		}


		/// <summary>
		/// Tile id of the item on the ground
		/// </summary>
		public int GroundTileID
		{
			get;
			set;
		}


		/// <summary>
		/// Tiles when the item is coming toward the team
		/// </summary>
		public int IncomingTileID
		{
			get;
			set;
		}


		/// <summary>
		/// Tiles when the item is thrown away
		/// </summary>
		public int ThrowTileID
		{
			get;
			set;
		}


		/// <summary>
		/// Is the item broken
		/// </summary>
		public bool IsBroken
		{
			get;
			set;
		}

		/// <summary>
		/// Is the item poisoned
		/// </summary>
		public bool IsPoisoned
		{
			get;
			set;
		}


		/// <summary>
		/// Is the item cursed
		/// </summary>
		public bool IsCursed
		{
			get;
			set;
		}


		/// <summary>
		/// The item use quiver
		/// </summary>
		public bool UseQuiver
		{
			get;
			set;
		}
		#endregion
	}


	/// <summary>
	/// Type of items
	/// </summary>
	public enum ItemType
	{
		Adornment,
		Ammo,
		Armor,
		Consumable,
		Miscellaneous,
		Potion,
		Scroll,
		Shield,
		Wand,
		Weapon,
		Key,
	}


	/// <summary>
	/// Body slots
	/// </summary>
	[Flags]
	public enum BodySlot
	{
		/// <summary>
		/// Left hand
		/// </summary>
		Primary = 0x1,

		/// <summary>
		/// Right hand
		/// </summary>
		Secondary = 0x2,

		/// <summary>
		/// 
		/// </summary>
		Ammo = 0x4,

		/// <summary>
		/// 
		/// </summary>
		Body = 0x8,

		/// <summary>
		/// 
		/// </summary>
		Neck = 0x10,

		/// <summary>
		/// 
		/// </summary>
		Ring = 0x20,

		/// <summary>
		/// 
		/// </summary>
		Wrist = 0x40,

		/// <summary>
		/// 
		/// </summary>
		Feet = 0x80,

		/// <summary>
		/// 
		/// </summary>
		Head = 0x100,

		/// <summary>
		/// 
		/// </summary>
		Waist = 0x200,

		/// <summary>
		/// Slotless worn items, items which are not worn
		/// </summary>
		None = 0x400,
	}


	/// <summary>
	/// Type of armor
	/// </summary>
	public enum ArmorType
	{
		Buckler,
		Heavy,
		LargeShield,
		Light,
		Medium,
		SmallShield,
		TowerShield
	}

}
