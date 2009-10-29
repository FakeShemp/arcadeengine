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
using System.Runtime.InteropServices;
using ArcEngine.PInvoke;

// http://msdn.microsoft.com/en-us/library/dd757116%28VS.85%29.aspx
// http://msdn.microsoft.com/en-us/library/bb153253%28VS.85%29.aspx

namespace ArcEngine.Input
{
	/// <summary>
	/// GamePad device management
	/// </summary>
	public static class GamePad
	{


		/// <summary>
		/// Constructor
		/// </summary>
		static GamePad()
		{
			AvailableDevices = new List<int>();
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





		/// <summary>
		/// Check for new devices
		/// </summary>
		public static void CheckForDevices()
		{
			AvailableDevices.Clear();

			int max = Winmm.joyGetNumDevs();

			for (int id = 0; id < max; id++)
			{
				if (IsDeviceConnected(id))		
					AvailableDevices.Add(id);					
			}

		}



		/// <summary>
		/// Checks if a device is connected 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool IsDeviceConnected(int id)
		{
			Winmm.JOYINFO info = new Winmm.JOYINFO();
			return Winmm.joyGetPos(Winmm.JOYSTICKID1 + id, ref info) == Winmm.JOYERR_NOERROR;
		}



		#region Properties

		/// <summary>
		/// Number of available devices
		/// </summary>
		static public int Count
		{
			get
			{
				return AvailableDevices.Count;
			}
		}


		/// <summary>
		/// Returns a list of available id device
		/// </summary>
		static public List<int> AvailableDevices
		{
			get;
			private set;
		}


		#endregion





	}
}
