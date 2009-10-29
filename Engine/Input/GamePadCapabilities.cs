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


namespace ArcEngine.Input
{

	/// <summary>
	/// Describes the capabilities of a gamepad.
	/// </summary>
	public class GamePadCapabilities
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		public GamePadCapabilities(int id)
		{
			Caps = new Winmm.JOYCAPS();
			int error = Winmm.joyGetDevCaps(id, ref Caps, Marshal.SizeOf(Caps));
			if (error != Winmm.JOYERR_NOERROR)
			{
				Trace.WriteLine("Error while getting GamePad capabilities (id={0})", id);
				IsConnected = false;
				return;
			}

			//Winmm.JOYINFOEX ex = new Winmm.JOYINFOEX();
			//ex.dwSize = Marshal.SizeOf(ex);
			//Winmm.joyGetPosEx(id, ref ex);


			IsConnected = true;
		}


		#region Properties

		/// <summary>
		/// The JOYCAPS structure contains information about the joystick capabilities.
		/// </summary>
		Winmm.JOYCAPS Caps;

		/// <summary>
		/// Number of axes currently in use
		/// </summary>
		public int AxeCount
		{
			get
			{
				return Caps.wNumAxes;
			}
		}

		/// <summary>
		/// Number of button
		/// </summary>
		public int ButtonCount
		{
			get
			{
				return Caps.wNumButtons;
			}
		}

		/// <summary>
		/// Name of the device
		/// </summary>
		public string Name
		{
			get
			{
				return Caps.szPname;
			}
		}

		/// <summary>
		/// Is connected
		/// </summary>
		public bool IsConnected
		{
			get;
			private set;
		}


		#endregion
	}
	

}
