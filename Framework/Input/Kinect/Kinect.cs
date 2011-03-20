#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ArcEngine.PInvoke;
using freenect;


namespace ArcEngine.Input
{
	/// <summary>
	/// Kinect controller
	/// </summary>
	public class Kinect : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="deviceid">Device id</param>
		public Kinect(int deviceid)
		{
			freenect.Kinect k = new freenect.Kinect(0);
			
			
			try
			{
				DeviceId = deviceid;
				Motor = CLNUIDevice.CreateMotor(Serial);
				Camera = CLNUIDevice.CreateCamera(Serial);


				SetMotorPosition(0);
			}
			catch (Exception e)
			{
				Trace.WriteLine("[Kinect] : Constructor failure : " + e.Message);
			}
		}



		/// <summary>
		/// Dispose resources
		/// </summary>
		public void Dispose()
		{
			if (Motor != IntPtr.Zero)
				CLNUIDevice.DestroyMotor(Motor);
			Motor = IntPtr.Zero;

			if (Camera != IntPtr.Zero)
				CLNUIDevice.DestroyCamera(Camera);
			Camera = IntPtr.Zero;

			DeviceId = 0;
		}


		#region Motor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="level"></param>
		public void SetMotorPosition(short level)
		{
			CLNUIDevice.SetMotorPosition(Motor, level);
		}

		#endregion


		#region Color Camera


		#endregion


		#region Depth Camera


		#endregion


		#region Led

		/// <summary>
		/// Set
		/// </summary>
		/// <param name="mode"></param>
		public void SetLED(KinectLedBlinkMode mode)
		{
			CLNUIDevice.SetMotorLED(Motor, (byte)mode);
		}

		#endregion


		#region Properties

		/// <summary>
		/// Device id
		/// </summary>
		public int DeviceId
		{
			get;
			private set;
		}


		/// <summary>
		/// Device serial
		/// </summary>
		public string Serial
		{
			get
			{
				try
				{
					return CLNUIDevice.GetDeviceSerial(DeviceId);
				}
				catch
				{
					return string.Empty;
				}
			}
		}


		/// <summary>
		/// Number of available devices
		/// </summary>
		static public int Count
		{
			get
			{
				try
				{
					return CLNUIDevice.GetDeviceCount();
				}
				catch (Exception e)
				{
					return 0;
				}
			}
		}


		/// <summary>
		/// Motor handle
		/// </summary>
		IntPtr Motor;


		/// <summary>
		/// Camera handle
		/// </summary>
		IntPtr Camera;


		/// <summary>
		/// Accelerometer
		/// </summary>
		public Vector3 Accelerometer
		{
			get
			{
				short x = 0;
				short y = 0;
				short z = 0;
				CLNUIDevice.GetMotorAccelerometer(Motor, ref x, ref y, ref z);

				return new Vector3(x, y, z);
			}
		}

		#endregion
	}


	/// <summary>
	/// Blinking mode for the front LED
	/// </summary>
	public enum KinectLedBlinkMode
	{
		Off = 0x0,

		Green = 0x1,

		Red = 0x2,

		Orange = 0x3,

		BlinkGreen = 0x4,

		GreenBlink = 0x5,

		BlinkRedOrange = 0x6,

		BlinkOrangeRed= 0x7,
	}
}
