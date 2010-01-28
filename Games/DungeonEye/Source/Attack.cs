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
	/// This class handle an attack between to entities
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
