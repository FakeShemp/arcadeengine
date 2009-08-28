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

namespace ArcEngine.Input
{
	/// <summary>
	/// GamePad
	/// </summary>
	public static class GamePad
	{


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal static bool Init()
		{


			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		static public GamePadCapabilities GetCapabilities(int id)
		{
			return new GamePadCapabilities(id);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		static public void GetState(int id)
		{
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		static public void SetVibration(int id, float left, float right)
		{
		}

		#region Properties



		/// <summary>
		/// Returns the number of gamepads connected
		/// </summary>
		public static int Count
		{
			get
			{
				int count = 0; // Winmm.joyGetNumDevs();
				//if (count == Winmm.JOYERR_UNPLUGGED)
				//{
				//   return 0;
				//}


				return count;
			}
		}

		#endregion
	}
}
