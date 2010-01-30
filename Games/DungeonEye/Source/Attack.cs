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
	/// This class handle an attack between to entities.
	/// There is a difference between Attacking and damaging. If you attack a monster, it is an attempt to do damage to it.
	/// It is not guaranteed that the attack is successful. In case of damage the attack was successful. 
	/// </summary>
	public class Attack
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="striker">Striker entity</param>
		/// <param name="Attacked">Attacked entity</param>
		/// <param name="item">Item used as a weapon. Use null for a hand attack</param>
		public Attack(Entity striker, Entity target, Item item)
		{
			Time = DateTime.Now;
			Striker = striker;
			Target = target;
			Item = item;

			if (striker == null || target == null)
				return;

			// Ranged attack ?
			DungeonLocation from = null;
			DungeonLocation to = null;
			if (striker is Hero)
				from = ((Hero)striker).Team.Location;
			else
				from = ((Monster)striker).Location;

			if (target is Hero)
				to = ((Hero)target).Team.Location;
			else
				to = ((Monster)target).Location;

			Range = (int)Math.Sqrt((from.Position.Y - to.Position.Y) * (from.Position.Y - to.Position.Y) +
						(from.Position.X - to.Position.X) * (from.Position.X - to.Position.X));

			// Attack roll
			int attackdie = Dice.GetD20(1);
			if (attackdie == 1)
				attackdie = -100000;
			if (attackdie == 20)
				attackdie = 100000;


			// Base attack bonus
			int baseattackbonus = 0;
			int modifier = 0;					// modifier
			int sizemodifier = 0;			// Size modifier
			int rangepenality = 0;			// Range penality


			if (striker is Hero)
			{
				Hero hero = striker as Hero;
				foreach (Profession prof in hero.Professions)
				{
					if (prof == null)
						continue;

					if (prof.Class == HeroClass.Fighter || prof.Class == HeroClass.Ranger || prof.Class == HeroClass.Paladin)
						baseattackbonus += prof.Experience.Level;

					if (prof.Class == HeroClass.Cleric || prof.Class == HeroClass.Mage || prof.Class == HeroClass.Thief)
						baseattackbonus += (prof.Experience.Level * 4) / 3;
				}
			}
			else
			{
				Monster monster = striker as Monster;
				sizemodifier = (int)monster.Size;
			}


			// Range penality
			if (RangedAttack)
			{
				modifier = striker.Dexterity.Modifier;

				//TODO : Add range penality
			}
			else
				modifier = striker.Strength.Modifier;

			// Attack bonus
			int attackbonus = baseattackbonus + modifier + sizemodifier + rangepenality;
			if (target.ArmorClass > attackdie + attackbonus)
				return;



			Dice dice = new Dice(1, 8, 0);
			Hit = dice.Roll();
			if (IsAHit)
				Target.Hit(this);

		}



		#region Properties

		/// <summary>
		/// Time of the attack
		/// </summary>
		public DateTime Time
		{
			get;
			private set;
		}

		/// <summary>
		/// Striker entity
		/// </summary>
		public Entity Striker
		{
			get;
			private set;
		}


		/// <summary>
		/// Target entity
		/// </summary>
		public Entity Target
		{
			get;
			private set;
		}


		/// <summary>
		/// Attacking item
		/// </summary>
		public Item Item
		{
			get;
			private set;
		}



		/// <summary>
		/// Is the attack a success
		/// </summary>
		public bool IsAHit
		{
			get
			{
				return Hit > 0;
			}
		}


		/// <summary>
		/// Is the attack a success
		/// </summary>
		public bool IsAMiss
		{
			get
			{
				return Hit == 0;
			}
		}


		/// <summary>
		/// Ranged attack
		/// </summary>
		public bool RangedAttack
		{
			get
			{
				return Range > 1;
			}
		}


		/// <summary>
		/// Distance of the attack
		/// </summary>
		public int Range
		{
			get;
			private set;
		}


		/// <summary>
		/// Damage inflicted
		/// </summary>
		public int Hit
		{
			get;
			private set;
		}

		#endregion
	}

}
