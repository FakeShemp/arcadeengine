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


		#region Load & Save

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml.Name != "item")
				return false;


			//ID = int.Parse(xml.Attributes["id"].Value);
			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "type":
					{
						Type = (ItemType)Enum.Parse(typeof(ItemType), node.Attributes["value"].Value, true);
					}
					break;

					case "slot":
					{
						Slot |= (ItemSlot)Enum.Parse(typeof(ItemSlot), node.Attributes["value"].Value, true);
					}
					break;

					case "weight":
					{
						Weight = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "damage":
					{
						DiceThrow = int.Parse(node.Attributes["throw"].Value);
						DiceFace = int.Parse(node.Attributes["face"].Value);
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
						MoveAwayTileID = int.Parse(node.Attributes["moveaway"].Value);
					}
					break;

					default:
					{

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

			writer.WriteStartElement("item");
			writer.WriteAttributeString("name", Name);



			writer.WriteStartElement("tile");
			writer.WriteAttributeString("inventory", TileID.ToString());
			writer.WriteAttributeString("ground", GroundTileID.ToString());
			writer.WriteAttributeString("incoming", IncomingTileID.ToString());
			writer.WriteAttributeString("moveaway", MoveAwayTileID.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("slot");
			writer.WriteAttributeString("value", Slot.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("weight");
			writer.WriteAttributeString("value", Weight.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("damage");
			writer.WriteAttributeString("throw", DiceThrow.ToString());
			writer.WriteAttributeString("face", DiceFace.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("critical");
			writer.WriteAttributeString("min", Critical.X.ToString());
			writer.WriteAttributeString("max", Critical.Y.ToString());
			writer.WriteAttributeString("multiplier", CriticalMultiplier.ToString());
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
		public ItemSlot Slot
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
		/// 
		/// </summary>
		public int GroundTileID
		{
			get;
			set;
		}


		/// <summary>
		/// Number of face of the dice
		/// </summary>
		public int DiceFace
		{
			get;
			set;
		}


		/// <summary>
		/// Number od dice throw
		/// </summary>
		public int DiceThrow
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
		public int MoveAwayTileID
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
		Food,
		Miscellaneous,
		Potion,
	//	Reagent,
		Scroll,
		Shield,
		Wand,
		Weapon,
		Key
	}


	/// <summary>
	/// Items slots
	/// </summary>
	[Flags]
	public enum ItemSlot
	{
		/// <summary>
		/// 
		/// </summary>
		Primary = 0x1,

		/// <summary>
		/// 
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


	/// <summary>
	/// Type of weapon
	/// </summary>
	public enum WeaponType
	{
		Axe,
		BastardSword, 
		BattleAxe,
		Club,
		Dagger, 
		Dart,
		DwarvenWarAxe, 
		Falchion,
		GreatAxe,
		GreatClub,
		GreatCrossbow,
		GreatSword,
		HeavyCrossbow,
		HeavyMace,
		HeavyPick,
		Kama,
		Khopesh,
		Kukri,
		LightCrossbow,
		LightHammer,
		LightMace,
		LightPick,
		LongBow, 
		LongSword,
		Maul,
		Morningstar,
		Quarterstaff,
		Rapier,
		RepeatingHeavyCrossbow,
		RepeatingLightCrossbow,
		Scimitar,
		ShortBow,
		ShortSword,
		Shuriken,
		Sickle,
		ThrowingAxe,
		ThrowingDagger,
		WarHammer
	}
}
