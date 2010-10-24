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
using OpenAL = OpenTK.Audio.OpenAL;
using System.Windows.Forms;

namespace ArcEngine.Audio
{

	/// <summary>
	/// Audio manager
	/// </summary>
	static public class AudioManager
	{

		/// <summary>
		/// Constructor
		/// </summary>
		static AudioManager()
		{
			if (!string.IsNullOrEmpty(DefaultDevice))
				return;


			Trace.WriteLine("[AudioManager] AudioManager() : OpenAL not found !!!");

			DialogResult result = MessageBox.Show("OpenAL is missing. Would you like to download it now ?", 
				"OpenAL DLL not found !!", 
				MessageBoxButtons.YesNo);

			// Open browser with OpenAL download page
			if (result == DialogResult.Yes)
					System.Diagnostics.Process.Start("http://connect.creativelabs.com/openal/Downloads/Forms/AllItems.aspx");
		}


		/// <summary>
		/// Display diagnostic informations
		/// </summary>
		static void Diagnostic()
		{

			Trace.WriteLine("--- Device related analysis ---");
			Trace.Indent();
			{
				Trace.WriteLine("Default playback device: " + DefaultDevice);
				Trace.WriteLine("All known playback devices:");
				Trace.Indent();
				{
					foreach (string s in OpenTK.Audio.AudioContext.AvailableDevices)
						Trace.WriteLine(s);
				}
				Trace.Unindent();

				Trace.WriteLine("Default recording device: " + DefaultDevice);
				Trace.WriteLine("All known recording devices:");
				Trace.Indent();
				{
					foreach (string s in OpenTK.Audio.AudioCapture.AvailableDevices)
						Trace.WriteLine(s);
				}
				Trace.Unindent();
			}
			Trace.Unindent();


			Trace.WriteLine("--- AL related analysis ---");
			Trace.Indent();
			{
				Trace.WriteLine("Used Device: " + Context.CurrentDevice);
				Trace.WriteLine("AL Renderer: " + OpenAL.AL.Get(OpenAL.ALGetString.Renderer));
				Trace.WriteLine("AL Vendor: " + OpenAL.AL.Get(OpenAL.ALGetString.Vendor));
				Trace.WriteLine("AL Version: " + OpenAL.AL.Get(OpenAL.ALGetString.Version));
				Trace.WriteLine("AL Extensions: " + OpenAL.AL.Get(OpenAL.ALGetString.Extensions));

				Trace.WriteLine("AL Speed of sound: " + OpenAL.AL.Get(OpenAL.ALGetFloat.SpeedOfSound));
				Trace.WriteLine("AL Distance Model: " + OpenAL.AL.GetDistanceModel().ToString());
				Trace.Unindent();
			}
			Trace.Unindent();

		}



		/// <summary>
		/// Creates a default context
		/// </summary>
		/// <returns></returns>
		static public bool Create()
		{
			return Create(DefaultDevice);
		}



		/// <summary>
		/// Creates an audio context
		/// </summary>
		/// <param name="device">Name of the device to use</param>
		/// <returns></returns>
		static public bool Create(string device)
		{
			if (Context != null)
				Release();

			// No context
			if (string.IsNullOrEmpty(device))
			{
				Trace.WriteLine("[AudioManager] Create() : Empty device name.");
				return false;
			}

			Trace.WriteLine("[AudioManager] : Creating a new context on \"{0}\"", device);


			try
			{
				Context = new OpenTK.Audio.AudioContext(device);
			}
			catch (Exception e)
			{
				Trace.WriteLine("[AudioManager] Create() : " + e.Message);
				return false;				
			}


			IsInit = true;
			Trace.WriteLine("[AudioManager] : Created on device \"{0}\"", Context.CurrentDevice);

			Diagnostic();

			return true;
		}



		/// <summary>
		/// Release audio context
		/// </summary>
		static public void Release()
		{
			Trace.WriteLine("[AudioManager] : Release()");

			if (Context != null)
			{
				Context.Dispose();
				Context = null;
			}

			IsInit = false;
		}



		/// <summary>
		/// Check for OpenAL errors
		/// </summary>
		static internal void Check()
		{
			OpenAL.ALError error = OpenAL.AL.GetError();

			if (error != OpenAL.ALError.NoError)
			{
				Trace.WriteLine("[OpenAL] Error : {0}", error.ToString());
			}
		}



		#region Properties
		

		/// <summary>
		/// Audio context
		/// </summary>
		static internal OpenTK.Audio.AudioContext Context
		{
			get;
			private set;
		}


		/// <summary>
		/// Available audio device
		/// </summary>
		public static List<string> AvailableDevices
		{
			get
			{
				Trace.WriteLine("[AudioManager] AvailableDevices");

				if (string.IsNullOrEmpty(DefaultDevice))
					return new List<string>();

				return new List<string>(OpenTK.Audio.AudioContext.AvailableDevices);
			}
		}


		/// <summary>
		/// Default device
		/// </summary>
		public static string DefaultDevice
		{
			get
			{
				Trace.WriteLine("[AudioManager] DefaultDevice");
				try
				{
					return OpenTK.Audio.AudioContext.DefaultDevice;
				}
				catch
				{
					return string.Empty;
				}
			}
		}



		/// <summary>
		/// Listener position
		/// </summary>
		static public Vector3 Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;

				OpenTK.Vector3 vect = new OpenTK.Vector3(value.X, value.Y, value.X);
				OpenAL.AL.Listener(OpenAL.ALListener3f.Position, ref vect);
			}
		}
		static Vector3 position;


		/// <summary>
		/// Listener velocity
		/// </summary>
		static public Vector3 Velocity
		{
			get
			{
				return velocity;
			}
			set
			{
				velocity = value;

				OpenTK.Vector3 vect = new OpenTK.Vector3(value.X, value.Y, value.X);
				OpenAL.AL.Listener(OpenAL.ALListener3f.Velocity, ref vect);
			}
		}
		static Vector3 velocity;


		/// <summary>
		/// Play tunes
		/// </summary>
		static public bool PlayTunes
		{
			get;
			set;
		}


		/// <summary>
		/// Play sounds
		/// </summary>
		static public bool PlaySounds
		{
			get;
			set;
		}


		/// <summary>
		/// Is audio system initialized
		/// </summary>
		static public bool IsInit
		{
			get;
			private set;
		}

		#endregion

	}


	/// <summary>
	/// State of an audio channel
	/// </summary>
	public enum AudioSourceState
	{
		/// <summary>
		/// Initial mode
		/// </summary>
		Initial = OpenAL.ALSourceState.Initial,

		/// <summary>
		/// Channel is playing
		/// </summary>
		Playing = OpenAL.ALSourceState.Playing,

		/// <summary>
		/// Channel is paused
		/// </summary>
		Paused = OpenAL.ALSourceState.Paused,

		/// <summary>
		/// Channel is stopped
		/// </summary>
		Stopped = OpenAL.ALSourceState.Stopped,
	}


	/// <summary>
	/// Audio format
	/// </summary>
	public enum AudioFormat
	{
		/// <summary>
		/// The stereo 16 bits format
		/// </summary>
		Stereo16 = OpenAL.ALFormat.Stereo16,

		/// <summary>
		/// The mono 16 bits format
		/// </summary>
		Mono16 = OpenAL.ALFormat.Mono16,


		/// <summary>
		/// The mono 8 bits format
		/// </summary>
		Mono8 = OpenAL.ALFormat.Mono8,


		/// <summary>
		/// The stereo 8 bits format
		/// </summary>
		Stereo8 = OpenAL.ALFormat.Stereo8,
	}

}
