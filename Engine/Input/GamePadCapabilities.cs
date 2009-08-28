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

namespace ArcEngine.Input
{

	/// <summary>
	/// Describes the capabilities of a gamepad.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct GamePadCapabilities
	{

		// <summary>
		// The JOYCAPS structure contains information about the joystick capabilities.
		// </summary>
	//	Winmm.JOYCAPS Caps;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		internal GamePadCapabilities(int id)
		{
			//Winmm.joyGetDevCaps(ref id, ref Caps, sizeof(Winmm.JOYCAPS));
			//Caps = new Winmm.JOYCAPS();
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsConnected
		{
			get
			{
				return false;
			}
		}


	}
	

}
