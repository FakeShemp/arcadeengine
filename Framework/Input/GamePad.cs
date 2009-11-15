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
using ArcEngine.Forms;
using SlimDX.DirectInput;


// http://msdn.microsoft.com/en-us/library/dd757116%28VS.85%29.aspx
// http://msdn.microsoft.com/en-us/library/bb153253%28VS.85%29.aspx

namespace ArcEngine.Input
{
	/// <summary>
	/// Gamepad device management
	/// </summary>
	public static class Gamepad
	{


		/// <summary>
		/// Constructor
		/// </summary>
		static Gamepad()
		{
			AvailableDevices = new List<GamePadState>();
		}



		/// <summary>
		/// Initialization
		/// </summary>
		/// <param name="window">GameWindow handle</param>
		/// <returns></returns>
		static public bool Init(GameWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Device = new DirectInput();

			Window = window;

			return true;
		}


		/// <summary>
		/// Release all resources
		/// </summary>
		static internal void Release()
		{
			ReleaseDevices();

			if (Device != null)
			{
				Device.Dispose();
				Device = null;
			}
		}




		/// <summary>
		/// Check for new devices
		/// </summary>
		static public void CheckForDevices()
		{
			ReleaseDevices();

			if (Window == null)
			{
				Trace.WriteLine("GamePad not initialized. GameWindow is null !!");
				return;
			}

			foreach (DeviceInstance device in Device.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
			{
				try
				{
					Joystick joystick = new Joystick(Device, device.InstanceGuid);
					//joystick.SetCooperativeLevel(form, CooperativeLevel.Nonexclusive | CooperativeLevel.Background);
					joystick.SetCooperativeLevel(Window, CooperativeLevel.Exclusive | CooperativeLevel.Background);
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
		static public GamepadCapabilities GetCapabilities(int id)
		{
			return new GamepadCapabilities(AvailableDevices[id].Joystick);
		}


	
		/// <summary>
		/// Returns the state of a Gamepad
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
		/// http://msdn.microsoft.com/en-us/library/ee417563%28VS.85%29.aspx
		/// http://www.codeproject.com/KB/directx/forcefeedback.aspx
		static public bool SetVibration(int id, int left, int right)
		{
			Joystick joystick = GetDevice(id);
			if (joystick == null)
				return false;

			EffectParameters param = new EffectParameters();
			param.Duration = 10000;
			param.Gain = 10000;
			param.SamplePeriod = 0;
			param.TriggerButton = 0;
			param.TriggerRepeatInterval = 10000;
			param.Flags = EffectFlags.ObjectOffsets| EffectFlags.Cartesian;

			int[] dirs;
			int[] axes;
			param.GetAxes(out axes, out dirs);
			param.SetAxes(axes, dirs);



			Effect effect = new Effect(joystick, EffectGuid.ConstantForce);
			effect.SetParameters(param);
			effect.Start(1);
/*

			int[] dwAxes = new int[2] { JoystickObjects.XAxis, JoystickObjects.YAxis };
			int[] lDirection = new int[] { 18000, 0 };

			ConstantForce diConstantForce = new ConstantForce();
			diConstantForce.Magnitude = 100000;

			SlimDX.DirectInput.
			Effect effect = new Effect(joystick, EffectGuid.ConstantForce);
			

			effect.dwFlags         = DIEFF_POLAR | DIEFF_OBJECTOFFSETS;
			effect.dwDuration = (DWORD)(0.5 * DI_SECONDS);
			effect.dwSamplePeriod = 0;                 // = default 
			effect.dwGain = DI_FFNOMINALMAX;   // No scaling
			effect.dwTriggerButton = DIEB_NOTRIGGER;    // Not a button response
			effect.dwTriggerRepeatInterval = 0;         // Not applicable
			effect.cAxes = 2;
			effect.rgdwAxes = &dwAxes[0];
			effect.rglDirection = &lDirection[0];
			effect.lpEnvelope = NULL;
			effect.cbTypeSpecificParams = sizeof(DICONSTANTFORCE);
			effect.lpvTypeSpecificParams = &diConstantForce;  

			//joystick.CreateEffect(GUID_ConstantForce, &diEffect, &lpdiEffect, NULL);
*/






/*
			Effect diEffect = null;
			int[] diAxes = new int[2];
			int[] diDirection = new int[] { 0, 0};
			PeriodicForce periodic = new PeriodicForce();
			EffectInfo diEffectInfo = new EffectInfo();

			EffectParameters param = new EffectParameters();
			param.Flags = EffectFlags.Polar | EffectFlags.ObjectOffsets;
			param.Duration = 1000;
			param.SamplePeriod = 1000;
			param.Gain = 10000;
			param.TriggerButton = 0;
			param.TriggerRepeatInterval = 0;
			param.SetAxes(diAxes, diDirection);
			param.Envelope = null;


			periodic.Magnitude = 10000;
			periodic.Period = 500;
*/





















/*

			int xAxisOffset = 0;
			int yAxisOffset = 0;
         int nextOffset = 0;
			foreach (DeviceObjectInstance d in joystick.GetObjects())
			{
					 if ((d.ObjectType & ObjectDeviceType.ForceFeedbackActuator) != 0) 
					 {
						  if (nextOffset == 0)
								xAxisOffset = d.Offset;
						  else
								yAxisOffset = d.Offset;
						  nextOffset++;
					 }
			}

         
         int[] offsets = new int[2];
         offsets[0] = xAxisOffset;
         offsets[1] = yAxisOffset;
         int[] coords = {left, right};

         EffectParameters info = new EffectParameters();
         info.Flags = EffectFlags.ObjectOffsets | EffectFlags.Cartesian;
         info.Duration = 1000;
			info.SamplePeriod = joystick.Capabilities.ForceFeedbackSamplePeriod;
			info.Parameters = new ConstantForce(); ;
         info.Gain = 5000;
         info.SetAxes(offsets, coords);
         info.StartDelay = 500;

			Effect eff1 = new Effect(joystick, EffectGuid.ConstantForce);//, info); //This is the line i get the error on.
			eff1.SetParameters(info, EffectParameterFlags.None);
			eff1.Start();
*/
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

	//		IList<EffectInfo> f = joystick.GetEffects(EffectType.ConstantForce);

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
		/// Update Gamepad states
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


		/// <summary>
		/// GameWindow handle
		/// </summary>
		static internal GameWindow Window
		{
			get;
			private set;
		}

		#endregion


	}
}
