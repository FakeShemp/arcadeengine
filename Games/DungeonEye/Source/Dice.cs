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
		/// <param name="throws">Throw count</param>
		/// <param name="faces">Face count</param>
		/// <param name="start">Base value</param>
		public Dice(int throws, int sides, int start)
		{
			Random = new Random((int)DateTime.Now.Ticks);

			Throws = throws;
			Faces = sides;
			Base = start;
		}


		/// <summary>
		/// Returns a dice roll
		/// </summary>
		/// <returns>The value</returns>
		public int Roll()
		{
			return Roll(Throws);
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
				val += Random.Next(1, Faces);


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
		public int Throws
		{
			get;
			set;
		}

		/// <summary>
		/// Number of sides
		/// </summary>
		public int Faces
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


		/// <summary>
		/// Minimum value
		/// </summary>
		public int Minimum
		{
			get
			{
				return Base + Throws;
			}
		}

		/// <summary>
		/// Maximum value
		/// </summary>
		public int Maximum
		{
			get
			{
				return Base + (Faces * Throws);
			}
		}


		#endregion
	}
}
