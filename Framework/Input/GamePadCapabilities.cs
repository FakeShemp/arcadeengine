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
using OpenTK.Input;

namespace ArcEngine.Input
{

	/// <summary>
	/// Describes the capabilities of a gamepad.
	/// </summary>
	public class GamepadCapabilities
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="joystick"></param>
		internal GamepadCapabilities(Joystick joystick)
		{
			// Joystick unplugged ?
			if (joystick == null)
				return; // throw new ArgumentNullException("joystick");

			Joystick = joystick;

		}


		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Joystick Joystick;

		/// <summary>
		/// Gets the number of axes available on the device. 
		/// </summary>
		public int AxeCount
		{
			get
			{
				//return Joystick.GetCapabilities().AxisCount;
				return -1;
			}
		}

		/// <summary>
		/// Gets the number of buttons available on the device. 
		/// </summary>
		public int ButtonCount
		{
			get
			{
				//return Joystick.Capabilities.ButtonCount;
				return -1;
			}
		}


		/// <summary>
		/// Gets the friendly instance name for the device.
		/// </summary>
		public string InstanceName
		{
			get
			{
				if (Joystick == null)
					return string.Empty;

				//return Joystick.Information.InstanceName;
				return string.Empty;
			}
		}

		/// <summary>
		/// Gets the friendly product name for the device.
		/// </summary>
		public string ProductName
		{
			get
			{
				if (Joystick == null)
					return string.Empty;

				//return Joystick.Information.ProductName;
				return string.Empty;
			}
		}

		/*
						/// <summary>
						/// The device is physically attached to the user's computer. 
						/// </summary>
						public bool IsConnected
						{
							get
							{
								return (Joystick.Capabilities.Flags & DeviceFlags.Attached) == DeviceFlags.Attached;
							}
						}
				*/


		/// <summary>
		/// The device supports force-feedback. 
		/// </summary>
		public bool ForceFeedback
		{
			get
			{
				if (Joystick == null)
					return false;

				//return (Joystick.Capabilities.Flags & DeviceFlags.ForceFeedback) == DeviceFlags.ForceFeedback;
				return false;
			}
		}


		/// <summary>
		/// Gets the number of Point-Of-View controllers available on the device. 
		/// </summary>
		public int PovCount
		{
			get
			{
				if (Joystick == null)
					return 0;

				//return Joystick.Capabilities.PovCount;
				return -1;
			}
		}
		#endregion
	}
	

}
