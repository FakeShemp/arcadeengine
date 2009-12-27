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

namespace DungeonEye
{
	/// <summary>
	/// Dice
	/// </summary>
	public class Dice
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public Dice() : this(0, 0, 0)
		{
		}

	
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="rolls"></param>
		/// <param name="faces"></param>
		/// <param name="start"></param>
		public Dice(int rolls, int sides, int start)
		{
			Random = new Random((int)DateTime.Now.Ticks);

			Rolls = rolls;
			Sides = sides;
			Base = start;
		}


		/// <summary>
		/// Returns a dice roll
		/// </summary>
		/// <returns>The value</returns>
		public int Roll()
		{
			return Roll(Rolls);
		}



		/// <summary>
		/// Returns a dice roll
		/// </summary>
		/// <param name="rolls>Number of roll</param>
		/// <returns>The value</returns>
		public int Roll(int rolls)
		{
			int val = 0;

			for (int i = 0; i < rolls; i++)
				val += Random.Next(1, Sides);


			return val + Base;
		}



		#region Properties


		/// <summary>
		/// Random generator
		/// </summary>
		Random Random;


		/// <summary>
		/// Number of throw
		/// </summary>
		public int Rolls
		{
			get;
			set;
		}

		/// <summary>
		/// Number of sides
		/// </summary>
		public int Sides
		{
			get;
			set;
		}

		/// <summary>
		/// Base value
		/// </summary>
		public int Base
		{
			get;
			set;
		}



		#endregion
	}
}
