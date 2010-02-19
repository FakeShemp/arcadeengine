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

namespace ArcEngine
{
	/// <summary>
	/// Generates a random enum
	/// </summary>
	public static class RandomEnum
	{

		/// <summary>
		/// Constructor
		/// </summary>
		static RandomEnum()
		{
			Random = new Random(DateTime.Now.Millisecond);
		}



		/// <summary>
		/// Returns a random enum
		/// </summary>
		/// <typeparam name="T">Type of the enum</typeparam>
		/// <returns></returns>
		public static T Get<T>()
		{
			T[] values = (T[])Enum.GetValues(typeof(T));
			return values[Random.Next(0, values.Length)];
		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		static Random Random;

		#endregion
	}
}