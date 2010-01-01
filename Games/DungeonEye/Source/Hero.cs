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
	public class Hero
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="team">Team of the hero</param>
		public Hero(Team team)
		{
			Team = team;
			Inventory = new Item[26];
			Professions = new Profession[2];

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
			Strength = GameBase.Random.Next(9, 17);
			Intelligence = GameBase.Random.Next(4, 17);
			Wisdom = GameBase.Random.Next(4, 17);
			Dexterity = GameBase.Random.Next(4, 17);
			Constitution = GameBase.Random.Next(4, 17);
			Charisma = GameBase.Random.Next(3, 18);
			ArmorClass = GameBase.Random.Next(0, 10);
			MaxHitPoint = GameBase.Random.Next(6, 37);
			HitPoint = GameBase.Random.Next(6, MaxHitPoint);
			Level = 3;
			Food = 75;
			Profession prof = new Profession();
			prof.Experience = GameBase.Random.Next(0, 99999);
			prof.Level = GameBase.Random.Next(1, 18);
			prof.Classe = HeroClass.Fighter;

			Professions[0] = prof;

			Head = GameBase.Random.Next(0, 32);


			Quiver = 10;
			ItemSet itemset = ResourceManager.CreateAsset<ItemSet>("Items");
			if (itemset != null)
			{
				SetInventoryItem(InventoryPosition.Primary, itemset.GetItem("Short Sword"));
				SetInventoryItem(InventoryPosition.Secondary, itemset.GetItem("Short Bow"));
				SetInventoryItem(InventoryPosition.Armor, itemset.GetItem("Leather Armor"));
				SetInventoryItem(InventoryPosition.Inventory_01, itemset.GetItem("Test Item"));
				SetInventoryItem(InventoryPosition.Inventory_02, itemset.GetItem("Spell book"));
				SetInventoryItem(InventoryPosition.Helmet, itemset.GetItem("Helmet"));
				SetInventoryItem(InventoryPosition.Feet, itemset.GetItem("Boots"));
			}
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
		/// <param name="item">Item</param>
		/// <returns>True if enough space, or false if full</returns>
		public bool AddToInventory(Item item)
		{
			if (item == null)
				return false;


			// Arrow
			if (item.Type == ItemType.Ammo)
			{
				Quiver++;
				return true;
			}

			// Neck
			if (item.Slot == BodySlot.Neck && GetInventoryItem(InventoryPosition.Neck) == null)
			{
				SetInventoryItem(InventoryPosition.Wrist, item); 
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
			if ((item.Slot & BodySlot.Primary) == BodySlot.Primary && GetInventoryItem(InventoryPosition.Primary) == null)
			{
				SetInventoryItem(InventoryPosition.Primary, item);
				return true;
			}

			// Secondary
			if ((item.Slot & BodySlot.Secondary) == BodySlot.Secondary && GetInventoryItem(InventoryPosition.Secondary) == null)
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

			for (int i = 0; i < 14; i++)
			{
				if (Inventory[i] == null)
				{
					Inventory[i] = item;
					return true;
				}
			}

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
			//if (item != null && item.Slot == position)
			Inventory[(int)position] = item;
			return true;
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
			attack.Monster = Team.Maze.GetMonster(Team.FrontCoord, Team.GetHeroGroundPosition(this));


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
					Team.Maze.FlyingItems.Add(new FlyingItem(item, loc, TimeSpan.FromSeconds(0.25), int.MaxValue));

					// Empty hand
					Inventory[20 - (int)(hand) * 4] = null;

					if (Quiver > 0)
					{
						Inventory[20 - (int)(hand) * 4] = ResourceManager.CreateAsset<ItemSet>("Items").GetItem("Arrow");
						Quiver--;
					}
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
					if (item.Slot == BodySlot.Waist)
					{
					}

					else if (item.UseQuiver && Quiver > 0)
					{
						Team.Maze.FlyingItems.Add(
							new FlyingItem(ResourceManager.CreateAsset<ItemSet>("Items").GetItem("Arrow"),
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


			HitPoint -= value;
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
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;
		

			ItemSet itemset = ResourceManager.CreateSharedAsset<ItemSet>("Main");

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
						Inventory[int.Parse(node.Attributes["position"].Value)] =  itemset.GetItem(node.Attributes["value"].Value);
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

					case "hp":
					{
						HitPoint = int.Parse(node.Attributes["actual"].Value);
						MaxHitPoint = int.Parse(node.Attributes["max"].Value);
					}
					break;

					case "strength":
					{
						Strength = int.Parse(node.Attributes["actual"].Value);
						MaxStrength = int.Parse(node.Attributes["max"].Value);
					}
					break;

					case "intelligence":
					{
						Intelligence = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "wisdom":
					{
						Wisdom = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "dexterity":
					{
						Dexterity = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "constitution":
					{
						Constitution = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "charisma":
					{
						Charisma = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "armorclass":
					{
						ArmorClass = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "level":
					{
						Level = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "alignment":
					{
						Alignment = (HeroAlignment) Enum.Parse(typeof(HeroAlignment), node.Attributes["value"].Value, true);
					}
					break;

					case "class":
					{
						Class = (HeroClass)Enum.Parse(typeof(HeroClass), node.Attributes["value"].Value, true);
					}
					break;

					case "race":
					{
						Race = (HeroRace)Enum.Parse(typeof(HeroRace), node.Attributes["value"].Value, true);
					}
					break;

					case "profession":
					{
						int id = int.Parse(node.Attributes["id"].Value);
						Professions[id].Experience = int.Parse(node.Attributes["xp"].Value);
						Professions[id].Level = int.Parse(node.Attributes["level"].Value);
						Professions[id].Classe = (HeroClass)Enum.Parse(typeof(HeroClass), node.Attributes["classe"].Value, true);
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
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			// Name
			writer.WriteStartElement("name");
			writer.WriteAttributeString("value", Name);
			writer.WriteEndElement();

		
			// Inventory
			for (int pos = 0; pos < Inventory.Length; pos++)
			{
				if (Inventory[pos] == null)
					continue;

				writer.WriteStartElement("inventory");
				writer.WriteAttributeString("position", pos.ToString());
				writer.WriteAttributeString("value", Inventory[pos].Name);
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

			writer.WriteStartElement("hp");
			writer.WriteAttributeString("actual", HitPoint.ToString());
			writer.WriteAttributeString("max", MaxHitPoint.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("strength");
			writer.WriteAttributeString("actual", Strength.ToString());
			writer.WriteAttributeString("max", MaxStrength.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("intelligence");
			writer.WriteAttributeString("value", Intelligence.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("wisdom");
			writer.WriteAttributeString("value", Wisdom.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("dexterity");
			writer.WriteAttributeString("value", Dexterity.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("constitution");
			writer.WriteAttributeString("value", Constitution.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("charisma");
			writer.WriteAttributeString("value", Charisma.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("armorclass");
			writer.WriteAttributeString("value", ArmorClass.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("level");
			writer.WriteAttributeString("value", Level.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("alignment");
			writer.WriteAttributeString("value", Alignment.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("class");
			writer.WriteAttributeString("value", Class.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("race");
			writer.WriteAttributeString("value", Race.ToString());
			writer.WriteEndElement();

			for(int id = 0; id < Professions.Length; id++)
			{
				writer.WriteStartElement("profession");
				writer.WriteAttributeString("id", id.ToString());
				writer.WriteAttributeString("xp", Professions[id].Experience.ToString());
				writer.WriteAttributeString("level", Professions[id].Level.ToString());
				writer.WriteAttributeString("classe", Professions[id].Classe.ToString());
				writer.WriteEndElement();
			}

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


		/// <summary>
		/// This value represents how much damage a champion can take before dying.
		/// </summary>
		public int HitPoint
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		public int MaxHitPoint
		{
			get;
			set;
		}


		/// <summary>
		/// This value determines the load a hero can carry, how far items can be thrown 
		/// and how much damage is done by melee attacks.
		/// </summary>
		public int Strength
		{
			get;
			set;
		}

		/// <summary>
		/// Maximum strength
		/// </summary>
		public int MaxStrength
		{
			get;
			set;
		}


		/// <summary>
		/// A vital attribute for mages as they learn spells. 
		/// </summary>
		public int Intelligence
		{
			get;
			set;
		}


		/// <summary>
		/// This value is important for spellcasters as it determines their ability to master Magic.
		/// It also determines the speed of Mana recovery.
		/// </summary>
		public int Wisdom
		{
			get;
			set;
		}


		/// <summary>
		/// This value determines the accuracy of missiles and the odds of hitting opponents in combat. 
		/// It also helps the champion to avoid or reduce physical damage improving their AC (armor class).
		/// </summary>
		public int Dexterity
		{
			get;
			set;
		}


		/// <summary>
		/// A character's health and toughness is determined by this. This has other effects outside of simply determining the HP
		/// amount gained after every level, such as a character's resistance to certain physical effects. 
		/// </summary>
		public int Constitution
		{
			get;
			set;
		}


		/// <summary>
		/// This determines how attractive or repulsive a character is to everyone around them.
		/// </summary>
		public int Charisma
		{
			get;
			set;
		}


		/// <summary>
		/// Armor class
		/// </summary>
		public int ArmorClass
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		public int Level
		{
			get;
			set;
		}


		/// <summary>
		/// Hero alignement
		/// </summary>
		public HeroAlignment Alignment
		{
			get;
			set;
		}


		/// <summary>
		/// Hero class
		/// </summary>
		public HeroClass Class
		{
			get;
			set;
		}


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
		public Profession[] Professions;


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
		/// This value determines a hero's resistance to magic attacks.
		/// </summary>
		public byte AntiMagic
		{
			get;
			set;
		}

		/// <summary>
		/// This value determines a hero's resistance to fire damage.
		/// </summary>
		public byte AntiFire
		{
			get;
			set;
		}


		
		#endregion



		#region Properties


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


		/// <summary>
		/// Returns if the hero is unconscious
		/// </summary>
		public bool IsUnconscious
		{
			get
			{
				return HitPoint > -10 && HitPoint <= 0;
			}
		}


		/// <summary>
		/// Returns true if hero is dead
		/// </summary>
		public bool IsDead
		{
			get
			{
				return HitPoint <= -10;
			}
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
	public enum HeroAlignment
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
		Fighter,
		Ranger,
		Paladin,
		Mage,
		Cleric,
		Thief
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
	/// Profession information
	/// </summary>
	public struct Profession
	{
		/// <summary>
		/// Profression
		/// </summary>
		public HeroClass Classe;

		/// <summary>
		/// Experience points
		/// </summary>
		public int Experience;


		/// <summary>
		/// Level
		/// </summary>
		public int Level;
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
		Ring_1 = 17,
		Ring_2 = 18,
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
