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
using System.Drawing;
using System.IO;
using System.Xml;
using OpenAL = OpenTK.Audio.OpenAL;
using ArcEngine.Asset;

namespace ArcEngine
{

	/// <summary>
	/// Audio manager
	/// </summary>
	static public class Audio
	{


		/// <summary>
		/// Display diagnostic informations
		/// </summary>
		static void Diagnostic()
		{
			Trace.WriteLine("--- Device related analysis ---");
			Trace.Indent();
			{
				Trace.WriteLine("Default playback device: " + OpenTK.Audio.AudioContext.DefaultDevice);
				Trace.WriteLine("All known playback devices:");
				Trace.Indent();
				{
					foreach (string s in OpenTK.Audio.AudioContext.AvailableDevices)
						Trace.WriteLine(s);
				}
				Trace.Unindent();

				Trace.WriteLine("Default recording device: " + OpenTK.Audio.AudioCapture.DefaultDevice);
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
		/// <param name="count">Number of listener</param>
		/// <returns></returns>
		static public bool Create(int count)
		{
			return Create(DefaultDevice, count);
		}



		/// <summary>
		/// Creates an audio context
		/// </summary>
		/// <param name="device">Name of the device to use</param>
		/// <param name="sourcecount">Number of listener</param>
		/// <returns></returns>
		static public bool Create(string device, int sourcecount)
		{
			if (Context != null)
				Release();

			Trace.WriteLine("[Audio] : Creating a new context on \"{0}\" with {1} source(s)", device, sourcecount);


			try
			{
				Context = new OpenTK.Audio.AudioContext(device);
			}
			catch (Exception)
			{
				Trace.WriteLine("#####################");
				Trace.WriteLine("OpenAL not found !!!!");
				Trace.WriteLine("#####################");

				System.Windows.Forms.MessageBox.Show("Please download and install OpenAL at :" + 
					Environment.NewLine + Environment.NewLine + 
					"http://connect.creativelabs.com/openal/Downloads/Forms/AllItems.aspx" + 
					Environment.NewLine + Environment.NewLine + "and install oalinst", "OpenAL DLL not found !!");

				throw new DllNotFoundException("OpenAL dll not found !!!");
			}


			// Generates sources
			Sources = OpenAL.AL.GenSources(sourcecount);



			Trace.WriteLine("[Audio] : Created on device \"{0}\" with {1} sources.", Context.CurrentDevice, SourceCount);

			Diagnostic();


			//Position = Vector3.Zero;
			//Velocity = Vector3.Zero;

			return true;
		}



		/// <summary>
		/// Release audio context
		/// </summary>
		static public void Release()
		{
			Trace.WriteLine("[Audio] : Release()");

			if (Context != null)
			{
				Context.Dispose();
				Context = null;
			}

			if (Sources != null)
			{
				OpenAL.AL.DeleteSources(Sources);
				Sources = null;
			}

		}


		/// <summary>
		/// Plays an audio sample
		/// </summary>
		/// <param name="id">Channel to ise</param>
		/// <param name="sample">Sample handle</param>
		static public void PlaySample(int id, AudioSample sample)
		{
			if (id < 0 || id > SourceCount || sample == null)
				throw new ArgumentOutOfRangeException("id");



			OpenAL.AL.Source(Sources[id], OpenAL.ALSourcei.Buffer, sample.Buffer);
			OpenAL.AL.Source(Sources[id], OpenAL.ALSourceb.Looping, sample.Loop);
			OpenAL.AL.Source(Sources[id], OpenAL.ALSourcef.Pitch, sample.Pitch);
			OpenAL.AL.Source(Sources[id], OpenAL.ALSourcef.MaxGain, sample.MaxGain);
			OpenAL.AL.Source(Sources[id], OpenAL.ALSourcef.MinGain, sample.MinGain);
			OpenAL.AL.Source(Sources[id], OpenAL.ALSource3f.Position, sample.Position.X, sample.Position.Y, sample.Position.Z);

			OpenAL.AL.SourcePlay(Sources[id]);

		}


		/// <summary>
		/// Pauses a channel
		/// </summary>
		/// <param name="id">Channel to pause</param>
		static public void Pause(int id)
		{
			if (id < 0 || id > SourceCount)
				throw new ArgumentOutOfRangeException("id");

			OpenAL.AL.SourcePause(Sources[id]);
		}


		/// <summary>
		/// Stops a channel
		/// </summary>
		/// <param name="id">Channel to pause</param>
		static public void Stop(int id)
		{
			if (id < 0 || id > SourceCount)
				throw new ArgumentOutOfRangeException("id");

			OpenAL.AL.SourceStop(Sources[id]);
			OpenAL.AL.SourceRewind(Sources[id]);
		}



		/// <summary>
		/// Returns the state of a source
		/// </summary>
		/// <param name="id">Channel id</param>
		/// <returns>State of the audio source</returns>
		static public AudioSourceState GetSourceState(int id)
		{
			if (id < 0 || id > SourceCount)
				throw new ArgumentOutOfRangeException("id");

			return (AudioSourceState)OpenAL.AL.GetSourceState(Sources[id]);
		}



		#region Properties
		

		/// <summary>
		/// Audio context
		/// </summary>
		static OpenTK.Audio.AudioContext Context;


		/// <summary>
		/// Available audio device
		/// </summary>
		public static List<string> AvailableDevices
		{
			get
			{
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
				return OpenTK.Audio.AudioContext.DefaultDevice;
			}
		}


		/// <summary>
		/// Source handles
		/// </summary>
		static int[] Sources;


		/// <summary>
		/// Number of available sources
		/// </summary>
		static int SourceCount
		{
			get
			{
				if (Sources == null)
					return 0;

				return Sources.Length;
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




		#endregion

	}


	/// <summary>
	/// State of an audio channel
	/// </summary>
	public enum AudioSourceState
	{
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
}
