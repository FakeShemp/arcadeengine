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
	public static class Dice
	{



		/// <summary>
		/// Returns a dice roll
		/// </summary>
		/// <param name="count">Number of roll</param>
		/// <param name="face">Number of face of the dice</param>
		/// <returns></returns>
		static public int Roll(int count, int face)
		{
			int val = 0;

			for (int i = 0; i < count; i++)
				val += Random.Next(1, face);


			return val;
		}



		#region Properties


		/// <summary>
		/// Random generator
		/// </summary>
		static Random Random = new Random((int)DateTime.Now.Ticks);


		#endregion
	}
}
