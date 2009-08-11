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
