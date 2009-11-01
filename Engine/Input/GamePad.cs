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
//using ArcEngine.PInvoke;
using System.Windows.Forms;

using ArcEngine.Forms;
using SlimDX;
using SlimDX.DirectInput;


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
			AvailableDevices = new List<GamePadState>();
			Device = new DirectInput();
		}



		/// <summary>
		/// Check for new devices
		/// </summary>
		/// <param name="form">GameWindow handle</param>
		static public void CheckForDevices(GameWindow form)
		{
			ReleaseDevices();

			if (form == null)
				return;

			foreach (DeviceInstance device in Device.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
			{
				try
				{
					Joystick joystick = new Joystick(Device, device.InstanceGuid);
					joystick.SetCooperativeLevel(form, CooperativeLevel.Nonexclusive | CooperativeLevel.Background);
					joystick.Acquire();

					AvailableDevices.Add(new GamePadState(joystick));
				}
				catch (DirectInputException ex)
				{
					Trace.WriteLine("Error while acquiring joystick : \"{0}\"", ex.Message);
				}
			}


			// Sets axis ranges
			for (int id = 0; id < Count; id++)
			{
				SetAxisRange(id, -1000, 1000);
				SetDeadZone(id, 100);
			}
		}


		/// <summary>
		/// Sets axis ranges
		/// </summary>
		/// <param name="id">Joytisck id</param>
		/// <param name="min">Minimum value</param>
		/// <param name="max">Maximum value</param>
		static public void SetAxisRange(int id, int min, int max)
		{
			Joystick joystick = GetDevice(id);
			if (joystick == null)
				return;

			foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
			{
				if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
					joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(min, max);
			}
		}



		/// <summary>
		/// Sets the dead zone
		/// </summary>
		/// <param name="id">Joystick id</param>
		/// <param name="value">Dead zone value</param>
		static public void SetDeadZone(int id, int value)
		{
			Joystick joystick = GetDevice(id);
			if (joystick == null)
				return;

			foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
			{
				if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
					joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).DeadZone = value;
			}
		}



		/// <summary>
		/// Gets the capabilities of a gamepad.
		/// </summary>
		/// <param name="id"></param>
		static public GamePadCapabilities GetCapabilities(int id)
		{
			return new GamePadCapabilities(AvailableDevices[id].Joystick);
		}


	
		/// <summary>
		/// Returns the state of a GamePad
		/// </summary>
		/// <param name="id">ID of the gamepad</param>
		/// <returns></returns>
		static public GamePadState GetState(int id)
		{
			if (id < 0 || id >= AvailableDevices.Count)
			return null;

			return AvailableDevices[id];
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		static public bool SetVibration(int id, float left, float right)
		{
			Joystick joystick = GetDevice(id);
			if (joystick == null)
				return false;


/*
			EffectParameters param = new EffectParameters();
			param.Duration = 300;
			param.SamplePeriod = 0;
			param.SetAxes(new int[] { 1 }, new int[] { 1 });


			
			Effect effect = new Effect(joystick, EffectGuid.RampForce);

			//param = effect.GetParameters();

			//effect.SetParameters(param);
			effect.Start();
*/

			IList<EffectInfo> f = joystick.GetEffects(EffectType.ConstantForce);

			return true;
		}


		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		public delegate void UnpluggedDevice(int id);


		/// <summary>
		/// 
		/// </summary>
		static public event UnpluggedDevice OnUnplug;

		#endregion



		#region Privates & internals



		/// <summary>
		/// Unplug a device
		/// </summary>
		/// <param name="joystick"></param>
		static internal void UnplugDevice(Joystick joystick)
		{
			for (int id = 0; id < Count; id++)
			{
				if (AvailableDevices[id].Joystick == joystick)
				{
					if (OnUnplug != null)
						OnUnplug(id);

					return;
				}
			}

		}


		/// <summary>
		/// Update GamePad states
		/// </summary>
		static internal void Update()
		{
			foreach (GamePadState pad in AvailableDevices)
				pad.Update();
		}


		/// <summary>
		/// Gets a Joystick device
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		static Joystick GetDevice(int id)
		{
			if (id < 0 || id > AvailableDevices.Count - 1)
				return null;

			return AvailableDevices[id].Joystick;
		}



		/// <summary>
		/// Release all acquired devices
		/// </summary>
		static void ReleaseDevices()
		{
			foreach (GamePadState state in AvailableDevices)
			{
				if (state.Joystick == null)
					continue;

				state.Joystick.Unacquire();
				state.Joystick.Dispose();
			}

			AvailableDevices.Clear();
		}


		#endregion


		#region Properties

		/// <summary>
		/// DirectInput device
		/// </summary>
		public static DirectInput Device;


		/// <summary>
		/// Available joystick devices
		/// </summary>
		static List<GamePadState> AvailableDevices;



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


		#endregion


	}
}
