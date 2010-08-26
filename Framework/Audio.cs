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

				Trace.WriteLine("AL Speed of sound: " + OpenAL.AL.Get(OpenAL.ALGetFloat.SpeedOfSound));
				Trace.WriteLine("AL Distance Model: " + OpenAL.AL.GetDistanceModel().ToString());

				Trace.WriteLine("AL Extensions:");
				Trace.Indent();
				{
					foreach (string ext in Extensions)
						Trace.Write(ext + ", ");
				}
				Trace.WriteLine("");
				Trace.Unindent();
			}
			Trace.Unindent();

		}


		/// <summary>
		/// Creates an audio context
		/// </summary>
		/// <returns></returns>
		static public bool Create()
		{
			if (Context != null)
				return true;

			Trace.WriteLine("[Audio] : Create()");


			try
			{
				Context = new OpenTK.Audio.AudioContext();
			}
			catch (Exception e)
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


			string[] AL_Extension_Names = new string[]
				{
				  "AL_EXT_ALAW",
				  "AL_EXT_BFORMAT",
				  "AL_EXT_double",
				  "AL_EXT_EXPONENT_DISTANCE",
				  "AL_EXT_float32",
				  "AL_EXT_FOLDBACK",
				  "AL_EXT_IMA4",
				  "AL_EXT_LINEAR_DISTANCE",
				  "AL_EXT_MCFORMATS",
				  "AL_EXT_mp3",
				  "AL_EXT_MULAW",
				  "AL_EXT_OFFSET",
				  "AL_EXT_vorbis",
				  "AL_LOKI_quadriphonic",
				  "EAX-RAM",
				  "EAX",
				  "EAX1.0",
				  "EAX2.0",
				  "EAX3.0",
				  "EAX3.0EMULATED",
				  "EAX4.0",
				  "EAX4.0EMULATED",
				  "EAX5.0"
				};


			Extensions = new List<string>();
			foreach (string s in AL_Extension_Names)
				if (OpenAL.AL.IsExtensionPresent(s))
					Extensions.Add(s);

			Diagnostic();

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

			Extensions.Clear();
		}



		#region Properties
		

		/// <summary>
		/// Audio context
		/// </summary>
		static OpenTK.Audio.AudioContext Context;


		/// <summary>
		/// Available extensions
		/// </summary>
		static List<string> Extensions;


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


		#endregion

	}
}
