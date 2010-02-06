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
			Professions = new List<Profession>();

			Inventory = new Item[26];
			BackPack = new Item[14];
			WaistPack = new Item[3];
			Attacks = new Attack[2];
			HandActions = new HandAction[2];
			HandActions[0] = new HandAction(ActionResult.Ok);
			HandActions[1] = new HandAction(ActionResult.Ok);
			HandPenality = new DateTime[2];
			HandPenality[0] = DateTime.Now;
			HandPenality[1] = DateTime.Now;
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

			Professions.Add(new Profession(0, HeroClass.Cleric));
			Professions.Add(new Profession(0, HeroClass.Fighter));


			Head = GameBase.Random.Next(0, 32);


			Quiver = 30;
			SetInventoryItem(InventoryPosition.Primary, ResourceManager.CreateAsset<Item>("Short Sword"));
			SetInventoryItem(InventoryPosition.Armor, ResourceManager.CreateAsset<Item>("Leather Armor"));
			SetInventoryItem(InventoryPosition.Helmet, ResourceManager.CreateAsset<Item>("Helmet"));
			SetInventoryItem(InventoryPosition.Feet, ResourceManager.CreateAsset<Item>("Boots"));
			SetBackPackItem(9, ResourceManager.CreateAsset<Item>("Short Bow"));
			SetBackPackItem(1, ResourceManager.CreateAsset<Item>("Test Item"));
			SetBackPackItem(2, ResourceManager.CreateAsset<Item>("Spell book"));
		}


		/// <summary>
		/// Updates hero
		/// </summary>
		/// <param name="time">Elapsed gametime</param>
		public void Update(GameTime time)
		{
			Point mousePos = Mouse.Location;


			if (Food > 100)
				Food = 100;

			// Remove olds attacks
			//Attacks.RemoveAll(
			//   delegate(AttackResult attack)
			//   {
			//      return attack.Date + attack.Hold < DateTime.Now;
			//   });
		}



		/// <summary>
		/// Add experience to the hero
		/// </summary>
		/// <param name="amount">XP to add</param>
		public void AddExperience(int amount)
		{
			if (amount == 0)
				return;

			foreach (Profession prof in Professions)
			{
				if (prof.AddXP(amount / ProfessionCount))
				{

					// New level gained
					Team.AddMessage(Name + " gained a level in " + prof.Class.ToString() + " !");
				}
			}


		}


		#region Inventory



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

				//case InventoryPosition.Belt_1:
				//case InventoryPosition.Belt_2:
				//case InventoryPosition.Belt_3:
				//if ((item.Slot & BodySlot.Belt) == BodySlot.Belt)
				//   res = true;
				//break;

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


		#endregion


		#region Items

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
				GetInventoryItem(InventoryPosition.Primary) == null &&
				CanUseHand(HeroHand.Primary))
			{
				SetInventoryItem(InventoryPosition.Primary, item);
				return true;
			}

			// Secondary
			if ((item.Slot & BodySlot.Secondary) == BodySlot.Secondary &&
				(item.Type == ItemType.Weapon || item.Type == ItemType.Shield || item.Type == ItemType.Wand) &&
				GetInventoryItem(InventoryPosition.Secondary) == null &&
				CanUseHand(HeroHand.Secondary))
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
			if ((item.Slot & BodySlot.Waist) == BodySlot.Waist)
			{
				for (int i = 0; i < 3; i++)
				{
					if (GetWaistPackItem(i) == null)
					{
						SetWaistPackItem(i, item);
						return true;
					}
				}
			}


			// Else anywhere in the bag...
			for (int i = 0; i < 14; i++)
			{
				if (BackPack[i] == null)
				{
					BackPack[i] = item;
					return true;
				}
			}
			// Sorry no room !
			return false;
		}

		
		/// <summary>
		/// Sets an item in the back pack
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="item">Item handle</param>
		public void SetBackPackItem(int position, Item item)
		{
			if (position < 0 || position > 14)
				return;

			BackPack[position] = item;
		}


		/// <summary>
		/// Gets an item from the backpack
		/// </summary>
		/// <param name="position">Position</param>
		/// <returns>Item handle</returns>
		public Item GetBackPackItem(int position)
		{
			if (position < 0 || position > 13)
				return null;

			return BackPack[position];
		}


		/// <summary>
		/// Sets an item in the waist pack
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="item">Item handle</param>
		/// <returns></returns>
		public bool SetWaistPackItem(int position, Item item)
		{
			if (position < 0 || position > 2)
				return false;

			if (item == null)
			{
				WaistPack[position] = null;
				return true;
			}

			if ((item.Slot & BodySlot.Waist) != BodySlot.Waist)
				return false;
			
			WaistPack[position] = item;
			return true;
		}


		/// <summary>
		/// Gets an item from the waistpack
		/// </summary>
		/// <param name="position">Position</param>
		/// <returns>Item handle</returns>
		public Item GetWaistPackItem(int position)
		{
			if (position < 0 || position > 3)
				return null;

			return WaistPack[position];
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
		/// Make damage to the hero
		/// </summary>
		/// <param name="damage">Attack roll</param>
		/// <param name="type">Type of saving throw</param>
		/// <param name="difficulty">Difficulty</param>
		public void Damage(Dice damage, SavingThrowType type, int difficulty)
		{
			if (damage == null)
				return;

			int save = Dice.GetD20(1);

			// No damage
			if (save == 20 || save + SavingThrow(type) > difficulty)
				return;

			HitPoint.Current -= damage.Roll();

		}
	

		/// <summary>
		/// Returns the result of a saving throw
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public int SavingThrow(SavingThrowType type)
		{
			int ret = BaseSaveBonus;

			switch (type)
			{
				case SavingThrowType.Fortitude:
				ret += Constitution.Modifier;
				break;
				case SavingThrowType.Reflex:
				ret += Dexterity.Modifier;
				break;
				case SavingThrowType.Will:
				ret += Wisdom.Modifier;
				break;
			}


			return ret;
		}


		/// <summary>
		/// Add a time penality to a hand
		/// </summary>
		/// <param name="hand">Hand</param>
		/// <param name="duration">Duration</param>
		public void AddHandPenality(HeroHand hand, TimeSpan duration)
		{

			HandPenality[(int)hand] = DateTime.Now + duration;
		}


		/// <summary>
		/// Hero attack with his hands
		/// </summary>
		/// <param name="hand">Attacking hand</param>
		public void UseHand(HeroHand hand)
		{
			// No action possible
			if (!CanUseHand(hand))
				return;

			// Attacked entity
			Entity target = Team.Location.Maze.GetMonster(Team.FrontLocation, Team.GetHeroGroundPosition(this));


			// Which item is used for the attack
			Item item = GetInventoryItem(hand == HeroHand.Primary ? InventoryPosition.Primary : InventoryPosition.Secondary);


			// Hand attack
			if (item == null)
			{
				if (Team.IsHeroInFront(this))
				{
					Attacks[(int)hand] = new Attack(this, target, null);
				}
				else
					HandActions[(int)hand] = new HandAction(ActionResult.CantReach);

				AddHandPenality(hand, TimeSpan.FromMilliseconds(250));
				return;
			}



			// 
			DungeonLocation loc = new DungeonLocation(Team.Location);
			loc.GroundPosition = Team.GetHeroGroundPosition(this);
			switch (item.Type)
			{

				// Throw the ammo
				case ItemType.Ammo:
				{
					// throw ammo
					Team.Location.Maze.FlyingItems.Add(new FlyingItem(this, item, loc, TimeSpan.FromSeconds(0.25), int.MaxValue));

					// Empty hand
					SetInventoryItem(hand == HeroHand.Primary ? InventoryPosition.Primary : InventoryPosition.Secondary, null);
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

					// Weapon use quiver
					else if (item.UseQuiver)
					{
						if (Quiver > 0)
						{
							Team.Location.Maze.FlyingItems.Add(
								new FlyingItem(this, ResourceManager.CreateAsset<Item>("Arrow"),
								loc, TimeSpan.FromSeconds(0.25), int.MaxValue));
							Quiver--;
						}
						else
							HandActions[(int)hand] = new HandAction(ActionResult.NoAmmo);
						
						AddHandPenality(hand, TimeSpan.FromMilliseconds(500));
					}

					else
					{
						// Check is the weapon can reach the target
						if (!Team.IsHeroInFront(this) && item.Range == 0)
						{
							HandActions[(int)hand] = new HandAction(ActionResult.CantReach);
						}
						else
							Attacks[(int)hand] = new Attack(this, target, item);
	
						AddHandPenality(hand, item.AttackSpeed);
					}
				}
				break;

			}

		}

		#endregion


		#region Helpers


		/// <summary>
		/// Checks if the hero belgons to a class
		/// </summary>
		/// <param name="classe">Class</param>
		/// <returns>True if the Hero belgons to this class</returns>
		public bool CheckClass(HeroClass classe)
		{
			foreach(Profession prof in Professions)
				if (prof != null)
				{
					if (prof.Class == classe)
						return true;
				}

			return false;
		}


		/// <summary>
		/// Can use the hand
		/// </summary>
		/// <param name="hand">Hand to attack</param>
		/// <returns>True if the specified hand can be used</returns>
		public bool CanUseHand(HeroHand hand)
		{
			if (IsDead || IsUnconscious)
				return false;

			// Check the item in the other hand
			Item item = GetInventoryItem(hand == HeroHand.Primary ? InventoryPosition.Secondary : InventoryPosition.Primary);
			if (item != null && item.TwoHanded)
				return false;

			return HandPenality[(int)hand] < DateTime.Now;
		}


		/// <summary>
		/// Returns the last attack
		/// </summary>
		/// <param name="hand">Hand of the attack</param>
		/// <returns>Attack result</returns>
		public Attack GetLastAttack(HeroHand hand)
		{
			return Attacks[(int)hand];
		}



		/// <summary>
		/// Gets the last action result
		/// </summary>
		/// <param name="hand">Hand</param>
		/// <returns></returns>
		public HandAction GetLastActionResult(HeroHand hand)
		{
			return HandActions[(int)hand];
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
					case "inventory":
					{
						SetInventoryItem(
							(InventoryPosition)Enum.Parse(typeof(InventoryPosition), node.Attributes["position"].Value),
							ResourceManager.CreateAsset<Item>(node.Attributes["name"].Value));
					}
					break;

					case "quiver":
					{
						Quiver = int.Parse(node.Attributes["count"].Value);
					}
					break;
					
					case "name":
					{
						Name = node.Attributes["value"].Value;
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

			writer.WriteStartElement("quiver");
			writer.WriteAttributeString("count", Quiver.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("head");
			writer.WriteAttributeString("id", Head.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("food");
			writer.WriteAttributeString("value", Food.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("race");
			writer.WriteAttributeString("value", Race.ToString());
			writer.WriteEndElement();

			foreach (Profession prof in Professions)
				prof.Save(writer);

			// Inventory
			foreach (InventoryPosition pos in Enum.GetValues(typeof(InventoryPosition)))
			{
				Item item = GetInventoryItem(pos);
				if (item == null)
					continue;

				writer.WriteStartElement("inventory");
				writer.WriteAttributeString("position", pos.ToString());
				writer.WriteAttributeString("name", item.Name);
				writer.WriteEndElement();
			}

			for (int id = 0; id < 3; id++)
				if (WaistPack[id] != null)
				{
					writer.WriteStartElement("waist");
					writer.WriteAttributeString("position", id.ToString());
					writer.WriteAttributeString("name", WaistPack[id].Name);
					writer.WriteEndElement();
				}


			for (int id = 0; id < 14; id++)
				if (BackPack[id] != null)
				{
					writer.WriteStartElement("waist");
					writer.WriteAttributeString("position", id.ToString());
					writer.WriteAttributeString("name", BackPack[id].Name);
					writer.WriteEndElement();
				}


			writer.WriteEndElement();
			return true;
		}

		#endregion


		#region Hero properties


		/// <summary>
		/// Base save bonus
		/// </summary>
		public int BaseSaveBonus
		{
			get
			{
				int value = 2;

				foreach (Profession prof in Professions)
				{
					if (prof == null)
						continue;
					value += prof.Experience.Level / 2;
				}
				return value;
			}
		}


		/// <summary>
		/// Base attack bonus
		/// </summary>
		public int BaseAttackBonus
		{
			get
			{
				int value = 0;

				foreach (Profession prof in Professions)
				{
					if (prof == null)
						continue;

					if (prof.Class == HeroClass.Fighter || prof.Class == HeroClass.Ranger || prof.Class == HeroClass.Paladin)
						value += prof.Experience.Level;

					if (prof.Class == HeroClass.Cleric || prof.Class == HeroClass.Mage || prof.Class == HeroClass.Thief)
						value += (prof.Experience.Level * 4) / 3;
				}

				return value;
			}
		}
	
		
		/// <summary>
		/// Number of arrow in the quiver
		/// </summary>
		public int Quiver;


		/// <summary>
		/// Armor class
		/// </summary>
		public override int ArmorClass
		{
			get
			{
				return 10 + ArmorBonus + ShieldBonus + DodgeBonus + NaturalArmorBonus;
			}
			set
			{
			}
		}

		
		/// <summary>
		/// Armor bonus
		/// Provided by armor slot, head slot and bracers slot 
		/// </summary>
		public int ArmorBonus
		{
			get
			{
				byte value = 0;

				Item item = GetInventoryItem(InventoryPosition.Helmet);
				if (item != null)
					value += item.ArmorClass;

				item = GetInventoryItem(InventoryPosition.Armor);
				if (item != null)
					value += item.ArmorClass;

				item = GetInventoryItem(InventoryPosition.Wrist);
				if (item != null)
					value += item.ArmorClass;

				return value;
			}
		}


		/// <summary>
		/// Shield bonus
		/// Provided by shield slot
		/// </summary>
		public int ShieldBonus
		{
			get
			{
				Item item = GetInventoryItem(InventoryPosition.Secondary);
				if (item == null)
					return 0;

				return item.ArmorClass;
			}
		}


		/// <summary>
		/// Dodge bonus
		/// Provided by boots slot
		/// </summary>
		public int DodgeBonus
		{
			get
			{
				Item item = GetInventoryItem(InventoryPosition.Feet);
				if (item == null)
					return 0;

				return item.ArmorClass;
			}
		}


		/// <summary>
		/// Natural armor bonus
		/// provided by amulet slot
		/// </summary>
		public int NaturalArmorBonus
		{
			get
			{
				Item item = GetInventoryItem(InventoryPosition.Neck);
				if (item == null)
					return 0;

				return item.ArmorClass;
			}
		}


		/// <summary>
		///  Name of the hero
		/// </summary>
		public string Name
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
		/// Items in the bag
		/// </summary>
		Item[] Inventory;

		
		/// <summary>
		/// Profressions of the Hero
		/// </summary>
		public List<Profession> Professions;


		/// <summary>
		/// Number of profession
		/// </summary>
		public int ProfessionCount
		{
			get
			{
				if (Professions[0] != null && Professions[1] != null)
					return 2;

				return 1;
			}
		}


		/// <summary>
		/// ID of head tile
		/// </summary>
		public int Head;


		/// <summary>
		/// Back pack items
		/// </summary>
		public Item[] BackPack
		{
			get;
			private set;
		}


		/// <summary>
		/// Waist items
		/// </summary>
		public Item[] WaistPack
		{
			get;
			private set;
		}


		/// <summary>
		/// These value represent how hungry and thursty a champion is.
		/// Food value is decreased to regenerate Stamina and Health. 
		/// When these value reach zero, the hero is starving: his Stamina and health decrease until he eats, drinks or dies.
		/// </summary>
		/// <remarks>Max food Level is 100</remarks>
		public byte Food
		{
			get;
			set;
		}


		/// <summary>
		/// Team of the hero
		/// </summary>
		public Team Team
		{
			get;
			private set;
		}


		/// <summary>
		/// Summary of last attacks
		/// </summary>
		Attack[] Attacks;

		/// <summary>
		/// Time penality on hands
		/// </summary>
		public DateTime[] HandPenality
		{
			get;
			private set;
		}

		/// <summary>
		/// Action result for each hands
		/// </summary>
		HandAction[] HandActions;
		
		#endregion

	}


	#region Enums & Structures


	/// <summary>
	/// Position in the inventory of a Hero
	/// </summary>
	public enum InventoryPosition
	{
		Armor,
		Wrist,
		Secondary,
		Ring_Left,
		Ring_Right,
		Feet,
		Primary,
		Neck,
		Helmet,
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




	#endregion

}
