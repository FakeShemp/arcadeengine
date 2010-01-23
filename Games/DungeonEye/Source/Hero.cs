﻿#region Licence
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
using ArcEngine;
using ArcEngine.Input;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// Represents a hero in the team
	/// 
	/// 
	/// 
	/// 
	/// http://uaf.wiki.sourceforge.net/Player%27s+Guide
	/// </summary>
	public class Hero : Entity
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="team">Team of the hero</param>
		public Hero(Team team)
		{
			Team = team;
			Inventory = new Item[26];
			Professions = new List<Profession>();

			Attacks = new AttackResult[2];
			Attacks[0] = new AttackResult();
			Attacks[1] = new AttackResult();
		}


		/// <summary>
		/// Generate a new hero with random values
		/// <see cref="http://www.aidedd.org/regles-f97/creation-de-perso-t1456.html"/>
		/// <see cref="http://christiansarda.free.fr/anc_jeux/eob1_intro.html"/>
		/// </summary>
		public void Generate()
		{
			ReRollAbilities();
			HitPoint = new HitPoint(GameBase.Random.Next(6, 37), GameBase.Random.Next(6, 37));
			Food = 75;

			Professions.Add(new Profession(GameBase.Random.Next(0, 999999), HeroClass.Cleric));
			Professions.Add(new Profession(GameBase.Random.Next(0, 999999), HeroClass.Fighter));


			Head = GameBase.Random.Next(0, 32);


			Quiver = 10;
			SetInventoryItem(InventoryPosition.Primary, ResourceManager.CreateAsset<Item>("Short Bow"));
			SetInventoryItem(InventoryPosition.Inventory_09, ResourceManager.CreateAsset<Item>("Short Bow"));
			SetInventoryItem(InventoryPosition.Armor, ResourceManager.CreateAsset<Item>("Leather Armor"));
			SetInventoryItem(InventoryPosition.Inventory_01, ResourceManager.CreateAsset<Item>("Test Item"));
			SetInventoryItem(InventoryPosition.Inventory_02, ResourceManager.CreateAsset<Item>("Spell book"));
			SetInventoryItem(InventoryPosition.Helmet, ResourceManager.CreateAsset<Item>("Helmet"));
			SetInventoryItem(InventoryPosition.Feet, ResourceManager.CreateAsset<Item>("Boots"));
		}


		/// <summary>
		/// Updates hero
		/// </summary>
		/// <param name="time">Elapsed gametime</param>
		public void Update(GameTime time)
		{
			Point mousePos = Mouse.Location;


			// Remove olds attacks
			//Attacks.RemoveAll(
			//   delegate(AttackResult attack)
			//   {
			//      return attack.Date + attack.Hold < DateTime.Now;
			//   });
		}


		#region Inventory


		/// <summary>
		/// Adds an item to the first free slot in the inventory
		/// </summary>
		/// <param name="item">Item handle</param>
		/// <returns>True if enough space, or false if full</returns>
		public bool AddToInventory(Item item)
		{
			if (item == null)
				return false;


			// Arrow
			if ((item.Slot & BodySlot.Quiver) == BodySlot.Quiver)
			{
				Quiver++;
				return true;
			}

			// Neck
			if (item.Slot == BodySlot.Neck && GetInventoryItem(InventoryPosition.Neck) == null)
			{
				SetInventoryItem(InventoryPosition.Neck, item); 
				return true;
			}

			// Armor
			if (item.Slot == BodySlot.Body && GetInventoryItem(InventoryPosition.Armor) == null)
			{
				SetInventoryItem(InventoryPosition.Armor, item);
				return true;
			}


			// Wrist
			if (item.Slot == BodySlot.Wrist && GetInventoryItem(InventoryPosition.Wrist) == null)
			{
				SetInventoryItem(InventoryPosition.Wrist, item);
				return true;
			}

			// Helmet
			if (item.Slot == BodySlot.Head && GetInventoryItem(InventoryPosition.Helmet) == null)
			{
				SetInventoryItem(InventoryPosition.Helmet, item);
				return true;
			}

			// Primary
			if ((item.Slot & BodySlot.Primary) == BodySlot.Primary && 
				(item.Type == ItemType.Weapon || item.Type == ItemType.Shield || item.Type == ItemType.Wand) &&
				GetInventoryItem(InventoryPosition.Primary) == null)
			{
				SetInventoryItem(InventoryPosition.Primary, item);
				return true;
			}

			// Secondary
			if ((item.Slot & BodySlot.Secondary) == BodySlot.Secondary &&
				(item.Type == ItemType.Weapon || item.Type == ItemType.Shield || item.Type == ItemType.Wand) &&
				GetInventoryItem(InventoryPosition.Secondary) == null)
			{
				SetInventoryItem(InventoryPosition.Secondary, item);
				return true;
			}

			// Boots
			if (item.Slot == BodySlot.Feet && GetInventoryItem(InventoryPosition.Feet) == null)
			{
				SetInventoryItem(InventoryPosition.Feet, item);
				return true;
			}

			// Ring
			if (item.Slot == BodySlot.Ring)
			{
				if (GetInventoryItem(InventoryPosition.Ring_Left) == null)
				{
					SetInventoryItem(InventoryPosition.Ring_Right, item);
					return true;
				}
				else if (GetInventoryItem(InventoryPosition.Ring_Right) == null)
				{
					SetInventoryItem(InventoryPosition.Ring_Left, item);
					return true;
				}
			}

			// Waist
			if ((item.Slot & BodySlot.Belt) == BodySlot.Belt)
			{
				for(int i = 0; i < 3; i++)
				{
					if (GetInventoryItem(InventoryPosition.Belt_1 + i) == null)
					{
						SetInventoryItem(InventoryPosition.Belt_1 + i, item);
						return true;
					}
				}
			}

			// Else anywhere in the bag...
			for (int i = 0; i < 14; i++)
			{
				if (Inventory[i] == null)
				{
					Inventory[i] = item;
					return true;
				}
			}

			// Sorry no room !
			Team.AddMessage("Bag is full !");
			return false;
		}


		/// <summary>
		/// Returns the item at a given inventory location
		/// </summary>
		/// <param name="position">Inventory position</param>
		/// <returns>Item or null</returns>
		public Item GetInventoryItem(InventoryPosition position)
		{
			return Inventory[(int)position];
		}



		/// <summary>
		/// Sets the item at a given inventory position
		/// </summary>
		/// <param name="position">Position in the inventory</param>
		/// <param name="item">Item to set</param>
		/// <returns>True if the item can be set at the given inventory location</returns>
		public bool SetInventoryItem(InventoryPosition position, Item item)
		{
			if (item == null)
			{
				Inventory[(int)position] = item;
				return true;
			}


			bool res = false;
			switch (position)
			{
				case InventoryPosition.Inventory_01:
				case InventoryPosition.Inventory_02:
				case InventoryPosition.Inventory_03:
				case InventoryPosition.Inventory_04:
				case InventoryPosition.Inventory_05:
				case InventoryPosition.Inventory_06:
				case InventoryPosition.Inventory_07:
				case InventoryPosition.Inventory_08:
				case InventoryPosition.Inventory_09:
				case InventoryPosition.Inventory_10:
				case InventoryPosition.Inventory_11:
				case InventoryPosition.Inventory_12:
				case InventoryPosition.Inventory_13:
				case InventoryPosition.Inventory_14:
					res = true;
				break;

				case InventoryPosition.Armor:
				if ((item.Slot & BodySlot.Body) == BodySlot.Body)
					res = true;
				break;

				case InventoryPosition.Wrist:
				if ((item.Slot & BodySlot.Wrist) == BodySlot.Wrist)
					res = true;
				break;

				case InventoryPosition.Secondary:
				if ((item.Slot & BodySlot.Secondary) == BodySlot.Secondary)
					res = true;
				break;

				case InventoryPosition.Ring_Left:
				case InventoryPosition.Ring_Right:
				if ((item.Slot & BodySlot.Ring) == BodySlot.Ring)
					res = true;
				break;

				case InventoryPosition.Feet:
				if ((item.Slot & BodySlot.Feet) == BodySlot.Feet)
					res = true;
				break;

				case InventoryPosition.Primary:
				if ((item.Slot & BodySlot.Primary) == BodySlot.Primary)
					res = true;
				break;

				case InventoryPosition.Belt_1:
				case InventoryPosition.Belt_2:
				case InventoryPosition.Belt_3:
				if ((item.Slot & BodySlot.Belt) == BodySlot.Belt)
					res = true;
				break;

				case InventoryPosition.Neck:
				if ((item.Slot & BodySlot.Neck) == BodySlot.Neck)
					res = true;
				break;

				case InventoryPosition.Helmet:
				if ((item.Slot & BodySlot.Head) == BodySlot.Head)
					res = true;
				break;
			}

			if (res)
				Inventory[(int)position] = item;

			return res;
		}


		/// <summary>
		/// Gets the next item in the waist bag
		/// </summary>
		/// <returns>Item handle or null if empty</returns>
		public Item PopWaistItem()
		{
			return null;

		}

		#endregion


		#region Attacks & Damages

		/// <summary>
		/// Hero attack with his hands
		/// </summary>
		/// <param name="hand">Attacking hand</param>
		public void UseHand(HeroHand hand)
		{
			if (IsUnconscious || IsDead)
				return;

			AttackResult attack = Attacks[(int)hand];


			Item item = null;
			if (hand == HeroHand.Primary)
				item = GetInventoryItem(InventoryPosition.Primary);
			else
				item = GetInventoryItem(InventoryPosition.Secondary);



			// Trace this attack
			attack.Date = DateTime.Now;
			attack.Result = (short)GameBase.Random.Next(0, 10);
		//	attack.Monster = Team.Location.Maze.GetMonster(Team.FrontCoord, Team.GetHeroGroundPosition(this));


			// Hand attack
			if (item == null)
			{
				attack.OnHold = TimeSpan.FromMilliseconds(500);
				return;
			}


			DungeonLocation loc = new DungeonLocation(Team.Location);
			loc.GroundPosition = Team.GetHeroGroundPosition(this);
			switch (item.Type)
			{

				// Throw the ammo
				case ItemType.Ammo:
				{
					// throw ammo
					Team.Location.Maze.FlyingItems.Add(new FlyingItem(item, loc, TimeSpan.FromSeconds(0.25), int.MaxValue));

					// Empty hand
					InventoryPosition pos = hand == HeroHand.Primary ? InventoryPosition.Primary : InventoryPosition.Secondary;
					SetInventoryItem(pos, null);

					//if (Quiver > 0 && item.UseQuiver)
					//{
					//    SetInventoryItem(pos, ResourceManager.CreateAsset<ItemSet>("Items").GetItem("Arrow"));
					//    Quiver--;
					//}
				}
				break;


				// Cast the spell
				case ItemType.Scroll:
				break;


				// Use the wand
				case ItemType.Wand:
				break;


				// Use the weapon
				case ItemType.Weapon:
				{
					if (item.Slot == BodySlot.Belt)
					{
					}

					else if (item.UseQuiver && Quiver > 0)
					{
						Team.Location.Maze.FlyingItems.Add(
							new FlyingItem(ResourceManager.CreateAsset<Item>("Arrow"),
							loc, TimeSpan.FromSeconds(0.25), int.MaxValue));
						Quiver--;
					}

					attack.OnHold = TimeSpan.FromMilliseconds(item.Speed + 3000);
				}
				break;

			}


			if (attack.Monster != null)
			{
				attack.Monster.Attack(attack.Result);
			}
		}


		/// <summary>
		/// An item hit the hero
		/// </summary>
		/// <param name="item">Item hitting the hero</param>
		/// <param name="value">Amount of damage</param>
		public void Hit(Item item, int value)
		{
			LastHitTime = DateTime.Now;
			LastHit = value;


			HitPoint.Current -= value;
		}



		#endregion


		#region Helpers

		/// <summary>
		/// Does the hero can attack ?
		/// </summary>
		/// <param name="hand">Hand to attack</param>
		/// <returns>True if the specified hand can attack</returns>
		public bool CanAttack(HeroHand hand)
		{
			if (IsDead || IsUnconscious)
				return false;

			// Check the item in the other hand
			Item item = null;
			if (hand == HeroHand.Primary)
			{
				item = GetInventoryItem(InventoryPosition.Secondary);
				if (item != null && item.TwoHanded)
					return false;
			}
			else
			{
				item = GetInventoryItem(InventoryPosition.Primary);
				if (item != null && item.TwoHanded)
					return false;
			}

			return Attacks[(int)hand].Date + Attacks[(int)hand].OnHold < DateTime.Now;
		}


		/// <summary>
		/// Returns the last attack
		/// </summary>
		/// <param name="hand">Hand of the attack</param>
		/// <returns>Attack result</returns>
		public AttackResult GetLastAttack(HeroHand hand)
		{
			return Attacks[(int)hand];
		}


		#endregion


		#region IO


		/// <summary>
		/// Loads a hero definition
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns>True if loaded</returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;
		
			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "name":
					{
						Name = node.Attributes["value"].Value;
					}
					break;

					case "inventory":
					{
						SetInventoryItem(
							(InventoryPosition)Enum.Parse(typeof(InventoryPosition), node.Attributes["position"].Value),
							ResourceManager.CreateAsset<Item>(node.Attributes["value"].Value));
					}
					break;

					case "quiver":
					{
						Quiver = int.Parse(node.Attributes["count"].Value);
					}
					break;

					case "head":
					{
						Head = int.Parse(node.Attributes["id"].Value);
					}
					break;

					case "food":
					{
						Food = byte.Parse(node.Attributes["value"].Value);
					}
					break;

					//case "class":
					//{
					//   Class = (HeroClass)Enum.Parse(typeof(HeroClass), node.Attributes["value"].Value, true);
					//}
					//break;

					case "race":
					{
						Race = (HeroRace)Enum.Parse(typeof(HeroRace), node.Attributes["value"].Value, true);
					}
					break;

					case "profession":
					{
						Profession prof = new Profession();
						prof.Load(node);
						Professions.Add(prof);
					}
					break;

					default:
					{
						base.Load(node);
					}
					break;
				}
			}

			return true;
		}



		/// <summary>
		/// Saves a hero definition
		/// </summary>
		/// <param name="writer">Xml writer handle</param>
		/// <returns>True if saved</returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("hero");
			base.Save(writer);

			// Name
			writer.WriteStartElement("name");
			writer.WriteAttributeString("value", Name);
			writer.WriteEndElement();

		
			// Inventory
			//for (int pos = 0; pos < Inventory.Length; pos++)
			foreach(InventoryPosition pos in Enum.GetValues(typeof(InventoryPosition)))
			{
				Item item = GetInventoryItem(pos);
				//if (Inventory[pos] == null)
				if (item == null)
					continue;

				writer.WriteStartElement("inventory");
				writer.WriteAttributeString("position", pos.ToString());
				writer.WriteAttributeString("value", item.Name);
				writer.WriteEndElement();
			}

			writer.WriteStartElement("quiver");
			writer.WriteAttributeString("count", Quiver.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("head");
			writer.WriteAttributeString("id", Head.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("food");
			writer.WriteAttributeString("value", Food.ToString());
			writer.WriteEndElement();

			//writer.WriteStartElement("class");
			//writer.WriteAttributeString("value", Class.ToString());
			//writer.WriteEndElement();

			writer.WriteStartElement("race");
			writer.WriteAttributeString("value", Race.ToString());
			writer.WriteEndElement();

			foreach (Profession prof in Professions)
				prof.Save(writer);

			writer.WriteEndElement();
			return true;
		}

		#endregion


		#region Hero properties


		/// <summary>
		///  Name of the hero
		/// </summary>
		public string Name
		{
			get;
			set;
		}

/*
		/// <summary>
		/// Hero class
		/// </summary>
		public HeroClass Class
		{
			get;
			set;
		}
*/

		/// <summary>
		/// Hero race
		/// </summary>
		public HeroRace Race
		{
			get;
			set;
		}

		
		/// <summary>
		/// Profression of the Hero
		/// </summary>
		public List<Profession> Professions;

		/// <summary>
		/// ID of head tile
		/// </summary>
		public int Head;


		/// <summary>
		/// Number of arrows in the quiver
		/// </summary>
		public byte Arrows
		{
			get;
			set;
		}


		/// <summary>
		/// These value represent how hungry and thursty a champion is.
		/// Food value is decreased to regenerate Stamina and Health. 
		/// When these value reach zero, the hero is starving: his Stamina and health decrease until he eats, drinks or dies.
		/// </summary>
		/// <remarks>Max food Level is 100</remarks>
		public byte Food
		{
			get
			{
				return food;
			}
			set
			{
				food = value;
				if (food > 100)
					food = 100;
			}
		}
		byte food;



		

		/// <summary>
		/// Team of the hero
		/// </summary>
		public Team Team
		{
			get;
			private set;
		}


		/// <summary>
		/// Sums of last attacks
		/// </summary>
		AttackResult[] Attacks;


		/// <summary>
		/// Items in the bag
		/// </summary>
		Item[] Inventory;


		/// <summary>
		/// Number of arrow in the quiver
		/// </summary>
		public int Quiver;



		/// <summary>
		/// Last time the hero was hit by a monster
		/// </summary>
		public DateTime LastHitTime
		{
			get;
			private set;
		}


		/// <summary>
		/// How many HP hero lost by the last attack
		/// </summary>
		public int LastHit
		{
			get;
			private set;
		}


		#endregion
	}


	#region Enums & Structures


	/// <summary>
	/// Result of the attack of a hero
	/// </summary>
	public class AttackResult
	{
		/// <summary>
		/// Time of the attack
		/// </summary>
		public DateTime Date;


		/// <summary>
		/// Result of the attack.
		/// </summary>
		/// <remarks>If Result == 0 the attack missed</remarks>
		public short Result;


		/// <summary>
		/// Monster involved in the fight.
		/// </summary>
		public Monster Monster;


		/// <summary>
		/// Hom many time the hero have to wait before attacking again with this hand
		/// </summary>
		public TimeSpan OnHold;

	}


	/// <summary>
	/// Available hero alignements
	/// </summary>
	public enum EntityAlignment
	{
		LawfulGood,
		NeutralGood,
		ChaoticGood,
		LawfulNeutral,
		TrueNeutral,
		ChoaticNeutral,
		LawfulEvil,
		NeutralEvil,
		ChaoticEvil
	}

	/// <summary>
	/// Class of the Hero
	/// </summary>
	[Flags]
	public enum HeroClass
	{
		/// <summary>
		/// 
		/// </summary>
		Undefined = 0x0,
	
		/// <summary>
		/// 
		/// </summary>
		Fighter = 0x1,

		/// <summary>
		/// 
		/// </summary>
		Ranger = 0x2,

		/// <summary>
		/// 
		/// </summary>
		Paladin = 0x4,

		/// <summary>
		/// 
		/// </summary>
		Mage = 0x8,

		/// <summary>
		/// 
		/// </summary>
		Cleric = 0x10,

		/// <summary>
		/// 
		/// </summary>
		Thief = 0x20,

	}


	/// <summary>
	/// Race of the Hero
	/// </summary>
	public enum HeroRace
	{
		HumanMale,
		HumanFemale,
		ElfMale,
		ElfFemale,
		HalfElfMale,
		HalfElfFemale,
		DwarfMale,
		DwarfFemale,
		GnomeMale,
		GnomeFemale,
		HalflingMale,
		HalflingFemale
	}


	/// <summary>
	/// Hand of Hero
	/// </summary>
	public enum HeroHand
	{
		/// <summary>
		/// Right hand
		/// </summary>
		Primary = 0,

		/// <summary>
		/// Left hand
		/// </summary>
		Secondary = 1

	}



	/// <summary>
	/// Position in the inventory of a Hero
	/// </summary>
	public enum InventoryPosition
	{
		Inventory_01 = 0,
		Inventory_02 = 1,
		Inventory_03 = 2,
		Inventory_04 = 3,
		Inventory_05 = 4,
		Inventory_06 = 5,
		Inventory_07 = 6,
		Inventory_08 = 7,
		Inventory_09 = 8,
		Inventory_10 = 9,
		Inventory_11 = 10,
		Inventory_12 = 11,
		Inventory_13 = 12,
		Inventory_14 = 13,
		Armor = 14,
		Wrist = 15,
		Secondary = 16,
		Ring_Left = 17,
		Ring_Right = 18,
		Feet = 19,
		Primary = 20,
		Belt_1 = 21,
		Belt_2 = 22,
		Belt_3 = 23,
		Neck = 24,
		Helmet = 25,
	//	Quiver,
	}




	#endregion

}
