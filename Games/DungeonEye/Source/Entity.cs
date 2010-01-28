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
using System.Xml;
using ArcEngine;


namespace DungeonEye
{
	/// <summary>
	/// Base class for every entity in the game
	/// </summary>
	public abstract class Entity
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Entity()
		{
			Charisma = new Ability();
			Strength = new Ability();
			Constitution = new Ability();
			Dexterity = new Ability();
			HitPoint = new HitPoint();
			Intelligence = new Ability();
			Wisdom = new Ability();

			ReRollAbilities();
		}



		/// <summary>
		/// Reroll entity abilities
		/// </summary>
		public void ReRollAbilities()
		{
			Charisma.Value = RollForAbility();
			Strength.Value = RollForAbility();
			Constitution.Value = RollForAbility();
			Dexterity.Value = RollForAbility();
			Intelligence.Value = RollForAbility();
			Wisdom.Value = RollForAbility();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		int RollForAbility()
		{
			Dice dice = new Dice(1, 6, 0);

			List<int> list = new List<int>();
			list.Add(dice.Roll());
			list.Add(dice.Roll());
			list.Add(dice.Roll());
			list.Add(dice.Roll());
			list.Sort();


			return list[1] + list[2] + list[3];
		}


		/// <summary>
		/// Attack the entity
		/// </summary>
		/// <param name="attack">Attack</param>
		public void Hit(Attack attack)
		{
			if (attack == null)
				return;

			LastAttack = attack;
			if (LastAttack.IsAMiss)
				return;

			HitPoint.Current -= LastAttack.Hit;

			// Reward the team for having killed the entity
			if (IsDead && attack.Striker is Hero)
			{
				(attack.Striker as Hero).Team.AddExperience(Experience);
			}
		}


		#region IO



		/// <summary>
		/// Saves properties
		/// </summary>
		/// <param name="writer">XmlWriter</param>
		/// <returns>True if saved</returns>
		public virtual bool Save(XmlWriter writer)
		{
			if (writer == null || writer.WriteState != WriteState.Element)
				return false;

			HitPoint.Save(writer);
			Strength.Save("strength", writer);
			Intelligence.Save("intelligence", writer);
			Wisdom.Save("wisdom", writer);
			Dexterity.Save("dexterity", writer);
			Constitution.Save("constitution", writer);
			Charisma.Save("charisma", writer);

			writer.WriteStartElement("alignment");
			writer.WriteAttributeString("value", Alignment.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("experience");
			writer.WriteAttributeString("value", Experience.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("speed");
			writer.WriteAttributeString("value", Speed.ToString());
			writer.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		public virtual bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			if (xml.NodeType == XmlNodeType.Comment)
				return false;

			switch (xml.Name.ToLower())
			{

				case "strength":
				{
					Strength.Load(xml);
				}
				break;

				case "intelligence":
				{
					Intelligence.Load(xml);
				}
				break;

				case "wisdom":
				{
					Wisdom.Load(xml);
				}
				break;

				case "dexterity":
				{
					Dexterity.Load(xml);
				}
				break;

				case "constitution":
				{
					Constitution.Load(xml);
				}
				break;

				case "charisma":
				{
					Charisma.Load(xml);
				}
				break;

				case "alignment":
				{
					Alignment = (EntityAlignment)Enum.Parse(typeof(EntityAlignment), xml.Attributes["value"].Value, true);
				}
				break;

				case "hitpoint":
				{
					HitPoint.Load(xml);
				}
				break;

				case "speed":
				{
					Speed = int.Parse(xml.Attributes["value"].Value);
				}
				break;

				case "experience":
				{
					Experience = int.Parse(xml.Attributes["value"].Value);
				}
				break;
			}


			return true;
		}


		#endregion



		#region Properties


		/// <summary>
		/// Experience gained by killing the entity
		/// </summary>
		public int Experience
		{
			get;
			set;
		}

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

		
		/// <summary>
		/// Armor class
		/// </summary>
		public abstract int ArmorClass
		{
			get;
			set;
		}


		/// <summary>
		/// Speed
		/// </summary>
		public int Speed
		{
			get;
			set;
		}



		/// <summary>
		/// Hit Point
		/// </summary>
		public HitPoint HitPoint
		{
			get;
			set;
		}


		/// <summary>
		/// Last attack suffered
		/// </summary>
		public Attack LastAttack
		{
			get;
			private set;
		}


		/// <summary>
		/// Returns if is unconscious
		/// </summary>
		public bool IsUnconscious
		{
			get
			{
				return HitPoint.Current > -10 && HitPoint.Current <= 0;
			}
		}


		/// <summary>
		/// Returns true is dead
		/// </summary>
		public bool IsDead
		{
			get
			{
				return HitPoint.Current <= -10;
			}
		}



		/// <summary>
		/// This value determines the load a hero can carry, how far items can be thrown 
		/// and how much damage is done by melee attacks. Strength also limits the amount 
		/// of equipment your character can carry. 
		/// </summary>
		public Ability Strength
		{
			get;
			private set;
		}


		/// <summary>
		/// A vital attribute for mages as they learn spells. 
		/// </summary>
		public Ability Intelligence
		{
			get;
			private set;
		}


		/// <summary>
		/// This value is important for spellcasters as it determines their ability to master Magic.
		/// It also determines the speed of Mana recovery.
		/// </summary>
		public Ability Wisdom
		{
			get;
			private set;
		}


		/// <summary>
		/// This value determines the accuracy of missiles and the odds of hitting opponents in combat. 
		/// It also helps the champion to avoid or reduce physical damage improving their AC (armor class).
		/// </summary>
		public Ability Dexterity
		{
			get;
			private set;
		}


		/// <summary>
		/// A character's health and toughness is determined by this. This has other effects outside of simply determining the HP
		/// amount gained after every level, such as a character's resistance to certain physical effects. 
		/// </summary>
		public Ability Constitution
		{
			get;
			private set;
		}


		/// <summary>
		/// This determines how attractive or repulsive a character is to everyone around them.
		/// </summary>
		public Ability Charisma
		{
			get;
			private set;
		}



		/// <summary>
		/// Hero alignement
		/// </summary>
		public EntityAlignment Alignment
		{
			get;
			set;
		}


		/// <summary>
		/// Number of arrow in the quiver
		/// </summary>
		public int Quiver;


		#endregion
	}


	/// <summary>
	/// Hand of Hero
	/// </summary>
	public enum EntityHand
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


}
