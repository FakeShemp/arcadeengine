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
