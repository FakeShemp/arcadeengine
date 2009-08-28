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

using System.Drawing;
using OpenTK.Audio;
using OpenTK.Math;
using OpenTK;


using AlContext = System.IntPtr;
using AlDevice = System.IntPtr;


namespace ArcEngine.Audio
{
	/// <summary>
	/// http://www.games-creators.org/wiki/Utiliser_FMOD_en_C_sharp
	/// http://www.devmaster.net/articles.php?catID=6
	/// http://www.gamedev.net/reference/articles/article2008.asp
	/// </summary>
	public class Sound : IDisposable
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public Sound()
		{
			Buffer = AL.GenBuffer();
			Source = AL.GenSource();

			Pitch = 1.0f;
			Gain = 1.0f;
			Position = Point.Empty;
			Velocity = Point.Empty;
		}

		/// <summary>
		/// Destructor
		/// </summary>
		~Sound()
		{
			Dispose();
		}


		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			AL.DeleteSource(Source);
			AL.DeleteBuffer(Buffer);

			Source = 0;
			Buffer = 0;
		}


		/// <summary>
		/// Loads a sound
		/// </summary>
		/// <param name="filename">File to load</param>
		/// <returns></returns>
		public bool LoadSound(string filename)
		{

			using (AudioReader sound = new AudioReader(filename))
			{
				AL.BufferData(Buffer, sound.ReadToEnd());
				AL.Source(Source, ALSourcei.Buffer, Buffer);
			}

			return AL.GetError() == ALError.NoError;
		}



		/// <summary>
		/// Plays the sound
		/// </summary>
		public void Play()
		{
			AL.SourcePlay(Source);
		}


		/// <summary>
		/// Pauses the sound
		/// </summary>
		public void Pause()
		{
			AL.SourcePause(Source);
		}


		/// <summary>
		/// Stops the sound
		/// </summary>
		public void Stop()
		{
			AL.SourceStop(Source);
			AL.SourceRewind(Source);
		}



		#region Initialization


		/// <summary>
		/// Initialize the sound system
		/// </summary>
		/// <returns></returns>
		static internal bool Init()
		{
			Trace.WriteLine("Init sound system... ");
			Trace.Indent();
			if (IntPtr.Size == 8)
			{
				Trace.WriteLine("No sound system on x64 systems for the moment, sorry...");
				Trace.Unindent();
				return false;
			}



			// Make sure we have a sound device available.  If not, do not allow playing of sounds :)
			if (AudioContext.AvailableDevices.Length > 0)
			{
				if (!string.IsNullOrEmpty(AudioContext.AvailableDevices[0]))
				{
					Context = new AudioContext();
				}
			}

		//	Context = new AudioContext();


       //  ContextHandle handle = new ContextHandle();

//         AlContext MyContext;

/*
         AlDevice MyDevice = Alc.OpenDevice(null);
         if (MyDevice != null)
         {
            Trace.WriteLine("Device allocation succeeded.");
				//MyContext = Alc.CreateContext(MyDevice, OpenTK.Audio.); // create context



				//if (MyContext != IntPtr.Zero)
				//{
				//   Trace.WriteLine("Context allocation succeeded.");
				//}
         }
*/
/*			
			Alut.Init();

			ListenerPosition = new Point(0, 0);
			ListenerVelocity = new Point(0, 0);
*/

			Trace.Unindent();
			Trace.WriteLine("OK");
			return true;
		}



		/// <summary>
		/// Close audio context
		/// </summary>
		static internal void Close()
		{
			if (Context != null)
			{
				Context.Dispose();
			}


	//		Alut.Exit();
		}
		#endregion



		#region Listener Properties


		/// <summary>
		/// Listener position
		/// </summary>
		static public Point ListenerPosition
		{
			get
			{
				Vector3 pos;

				AL.GetListener(ALListener3f.Position, out pos);

				return new Point((int)pos.X, (int)pos.Y); ;
			}

			set
			{
				AL.Listener(ALListener3f.Position, value.X, value.Y, 0);
			}
		}

		/// <summary>
		/// Listener velocity
		/// </summary>
		static public Point ListenerVelocity
		{
			get
			{
				int[] pos = new int[3];

				//AL.GetListeneriv(AL.AL_VELOCITY, pos);

				return new Point(pos[0], pos[1]); ;
			}

			set
			{
				//AL.Listener3i(AL.AL_VELOCITY, value.X, value.Y, 0);
			}
		}

		/// <summary>
		/// Listener gain
		/// </summary>
		static public float ListenerGain
		{
			get
			{
				float val = 0;
				//AL.GetListenerf(AL.AL_GAIN, out val);

				return val;
			}
			set
			{
				//AL.Listenerf(AL.AL_GAIN, value);
			}
		}




		#endregion


		#region Properties


		/// <summary>
		/// ID of the sound buffer
		/// </summary>
		int Buffer;

		/// <summary>
		/// Source ID
		/// </summary>
		int Source;



		/// <summary>
		/// Audio Context
		/// </summary>
		static public AudioContext Context
		{
			protected set;
			get;
		}



		/// <summary>
		/// Turns sound looping on or off
		/// </summary>
		public bool Loop
		{
			get
			{
				int ret = 0;
				//AL.GetSourcei(SourceID, AL.AL_LOOPING, out ret);

				return  false;
			}

			set
			{
				//if (value)
				//   AL.Sourcei(SourceID, AL.AL_LOOPING, AL.AL_TRUE);
				//else
				//   AL.Sourcei(SourceID, AL.AL_LOOPING, AL.AL_FALSE);

			}
		}


		/// <summary>
		/// Pitch of the sound
		/// </summary>
		public float Pitch
		{
			get
			{
				float val = 0;

				//AL.GetSourcef(SourceID, AL.AL_PITCH, out val);

				return val;
			}
			set
			{
				//AL.Sourcef(SourceID, AL.AL_PITCH, value);
			}
		}

		/// <summary>
		/// Gain of the sound
		/// </summary>
		public float Gain
		{
			get
			{
				float val = 0;

				//AL.GetSourcef(SourceID, AL.AL_GAIN, out val);

				return val;
			}
			set
			{
				//AL.Sourcef(SourceID, AL.AL_GAIN, value);
			}
		}


		/// <summary>
		/// Position of the sound
		/// </summary>
		public Point Position
		{
			get
			{
				int[] pos = new int[3];

				//AL.GetSourceiv(SourceID, AL.AL_POSITION, pos);

				return new Point(pos[0], pos[1]);
			}
			set
			{
				//AL.Source3i(SourceID, AL.AL_POSITION, value.X, value.Y, 0);
			}
		}



		/// <summary>
		/// Velocity of the sound
		/// </summary>
		public Point Velocity
		{
			get
			{
				int[] pos = new int[3];

				//AL.GetSourceiv(SourceID, AL.AL_VELOCITY, pos);

				return new Point(pos[0], pos[1]);
			}
			set
			{
				//AL.Source3i(SourceID, AL.AL_VELOCITY, value.X, value.Y, 0);
			}
		}


		#endregion
	}
}
