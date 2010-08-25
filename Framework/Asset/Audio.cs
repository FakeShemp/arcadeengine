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
using System.IO;
using System.Xml;
using OpenAL = OpenTK.Audio.OpenAL;

namespace ArcEngine.Asset
{
	/// <summary>
	/// http://www.games-creators.org/wiki/Utiliser_FMOD_en_C_sharp
	/// http://www.devmaster.net/articles.php?catID=6
	/// http://www.gamedev.net/reference/articles/article2008.asp
	/// </summary>
	public class Audio : IDisposable, IAsset
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public Audio()
		{
			Buffer = OpenAL.AL.GenBuffer();
			Source = OpenAL.AL.GenSource();

			Pitch = 1.0f;
			MaxGain = 1.0f;
			Position = Point.Empty;
			Velocity = Point.Empty;

			//Player = new SoundPlayer();
			//Player.LoadCompleted += new AsyncCompletedEventHandler(Player_LoadCompleted);

			IsDisposed = false;
		}


		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}



		/// <summary>
		/// Destructor
		/// </summary>
		~Audio()
		{
			//throw new Exception("Audio : Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			OpenAL.AL.DeleteSource(Source);
			Source = 0;
			OpenAL.AL.DeleteBuffer(Buffer);
			Buffer = 0;

			//if (Player != null)
			//    Player.Dispose();
			//Player = null;

		}


		#region Statics


		/// <summary>
		/// Display diagnostic informations
		/// </summary>
		internal static void Diagnostic()
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
			//	Trace.WriteLine("AL Maximum simultanous Sources: " + MaxSources);

			//	Trace.WriteLine("AL Extension string: " + ExtensionString);
				Trace.WriteLine("Confirmed AL Extensions:");
				Trace.Indent();
				{
			//		foreach (KeyValuePair<string, bool> pair in Extensions)
			//			Trace.WriteLine(pair.Key + ": " + pair.Value);
				}
				Trace.Unindent();
			}
			Trace.Unindent();

		}


		/// <summary>
		/// Creates an audio context
		/// </summary>
		/// <returns></returns>
		internal static bool Create()
		{
			if (Context != null)
				return true;

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

			Diagnostic();

			return true;
		}


		/// <summary>
		/// Release audio context
		/// </summary>
		internal static void Release()
		{
			if (Context != null)
			{
				Context.Dispose();
				Context = null;
			}
		}

		static OpenTK.Audio.AudioContext Context;


		#endregion


		#region Events

		/// <summary>
		/// On load completed event handler
		/// </summary>
		/// <param name="audio"></param>
		public delegate void OnLoadCompletedEventHandler(Audio audio);

		/// <summary>
		/// OnLoadCompleted event
		/// </summary>
		public event OnLoadCompletedEventHandler OnLoadCompleted;

		#endregion



		/// <summary>
		/// Loads a sound
		/// </summary>
		/// <param name="filename">File to load</param>
		public void LoadSound(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return;

			//Player.Stream = ResourceManager.LoadResource(filename);
			//if (Player.Stream == null)
			//    return;


			int channels, bits_per_sample, sample_rate;
			Stream stream = ResourceManager.LoadResource(filename);
			byte[] sound_data = LoadWave(stream, out channels, out bits_per_sample, out sample_rate);

			OpenAL.AL.BufferData(Buffer, GetSoundFormat(channels, bits_per_sample), sound_data, sound_data.Length, sample_rate);

			OpenAL.AL.Source(Source, OpenAL.ALSourcei.Buffer, Buffer);

			IsLoaded = true;
		}



		#region Loaders

		/// <summary>
		/// Loads a wave/riff audio file.
		/// </summary>
		/// <param name="stream">Stream of the file to load</param>
		/// <param name="channels">Number of channel</param>
		/// <param name="bits">Bits per samples</param>
		/// <param name="rate">Sample rate</param>
		/// <returns></returns>
		/// <remarks>Thanks to OpenTK example for the code !!!</remarks>
		private byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
		{
			channels = 0;
			bits = 0;
			rate = 0;

			if (stream == null)
				return null;

			using (BinaryReader reader = new BinaryReader(stream))
			{
				// RIFF header
				string signature = new string(reader.ReadChars(4));
				if (signature != "RIFF")
					throw new NotSupportedException("Specified stream is not a wave file.");

				int riff_chunck_size = reader.ReadInt32();

				string format = new string(reader.ReadChars(4));
				if (format != "WAVE")
					throw new NotSupportedException("Specified stream is not a wave file.");

				// WAVE header
				string format_signature = new string(reader.ReadChars(4));
				if (format_signature != "fmt ")
					throw new NotSupportedException("Specified wave file is not supported.");

				int format_chunk_size = reader.ReadInt32();
				int audio_format = reader.ReadInt16();
				int num_channels = reader.ReadInt16();
				int sample_rate = reader.ReadInt32();
				int byte_rate = reader.ReadInt32();
				int block_align = reader.ReadInt16();
				int bits_per_sample = reader.ReadInt16();

				string data_signature = new string(reader.ReadChars(4));
		//		if (data_signature != "data")
		//			throw new NotSupportedException("Specified wave file is not supported.");

				int data_chunk_size = reader.ReadInt32();

				channels = num_channels;
				bits = bits_per_sample;
				rate = sample_rate;

				return reader.ReadBytes((int) reader.BaseStream.Length);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="channels"></param>
		/// <param name="bits"></param>
		/// <returns></returns>
		private OpenAL.ALFormat GetSoundFormat(int channels, int bits)
		{
			switch (channels)
			{
				case 1:
				return bits == 8 ? OpenAL.ALFormat.Mono8 : OpenAL.ALFormat.Mono16;
				case 2:
				return bits == 8 ? OpenAL.ALFormat.Stereo8 : OpenAL.ALFormat.Stereo16;
				default:
				throw new NotSupportedException("The specified sound format is not supported.");
			}
		}


		#endregion

		/// <summary>
		/// Plays the sound
		/// </summary>
		public void Play()
		{
			//if (!IsLoaded)
			//    return;

			OpenAL.AL.SourcePlay(Source);

			IsPlaying = true;
		}


		/// <summary>
		/// Pauses the sound
		/// </summary>
		public void Pause()
		{
			OpenAL.AL.SourcePause(Source);
		}


		/// <summary>
		/// Stops the sound
		/// </summary>
		public void Stop()
		{
			IsPlaying = false;
			OpenAL.AL.SourceStop(Source);
			OpenAL.AL.SourceRewind(Source);
		}


		#region	IO operations


		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

/*
			xml.WriteStartElement("tileset");
			xml.WriteAttributeString("name", Name);



			if (!string.IsNullOrEmpty(TextureName))
			{
				xml.WriteStartElement("texture");
				xml.WriteAttributeString("file", TextureName);
				xml.WriteEndElement();
			}

			// Loops throughs tile
			foreach (KeyValuePair<int, Tile> val in tiles)
			{
				xml.WriteStartElement("tile");

				xml.WriteAttributeString("id", val.Key.ToString());

				xml.WriteStartElement("rectangle");
				xml.WriteAttributeString("x", val.Value.Rectangle.X.ToString());
				xml.WriteAttributeString("y", val.Value.Rectangle.Y.ToString());
				xml.WriteAttributeString("width", val.Value.Rectangle.Width.ToString());
				xml.WriteAttributeString("height", val.Value.Rectangle.Height.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("hotspot");
				xml.WriteAttributeString("x", val.Value.HotSpot.X.ToString());
				xml.WriteAttributeString("y", val.Value.HotSpot.Y.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("collisionbox");
				xml.WriteAttributeString("x", val.Value.CollisionBox.X.ToString());
				xml.WriteAttributeString("y", val.Value.CollisionBox.Y.ToString());
				xml.WriteAttributeString("width", val.Value.CollisionBox.Width.ToString());
				xml.WriteAttributeString("height", val.Value.CollisionBox.Height.ToString());
				xml.WriteEndElement();

				xml.WriteEndElement();
			}


			xml.WriteEndElement();
*/
			return false;
		}


		/// <summary>
		/// Loads the collection from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;


			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
					//base.LoadComment(node);
					continue;
				}


				switch (node.Name.ToLower())
				{
					// file name
					case "file":
					{
						LoadSound(node.Attributes["name"].Value);
					}
					break;

				}
			}

			return true;
		}

		#endregion
	

		#region Listener Properties

/*
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

*/


		#endregion


		#region Properties

		/// <summary>
		/// Name of the sound
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Is asset disposed
		/// </summary>
		public bool IsDisposed { get; private set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "audio";
			}
		}


		/// <summary>
		/// Audio type
		/// </summary>
		public AudioType Type
		{
			get;
			set;
		}


		/// <summary>
		/// ID of the sound buffer
		/// </summary>
		int Buffer;


		/// <summary>
		/// Source ID
		/// </summary>
		int Source;


		/// <summary>
		/// Turns sound looping on or off
		/// </summary>
		public bool Loop
		{
			get
			{
				bool ret;
				OpenAL.AL.GetSource(Source, OpenAL.ALSourceb.Looping, out ret);

				return ret;
			}

			set
			{
				OpenAL.AL.Source(Source, OpenAL.ALSourceb.Looping, value);
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
		public float MaxGain
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
		/// Gain of the sound
		/// </summary>
		public float MinGain
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


		/// <summary>
		/// Is sound loaded
		/// </summary>
		public bool IsLoaded
		{
			get;
			private set;
		}


		/// <summary>
		/// Is sound is playing
		/// </summary>
		public bool IsPlaying
		{
			get;
			private set;
		}


		/// <summary>
		/// Sound player
		/// </summary>
	//	SoundPlayer Player;

		#endregion
	}


	/// <summary>
	/// Type of Audio sound
	/// </summary>
	public enum AudioType
	{
		/// <summary>
		/// Audio data are fully loaded in memory
		/// </summary>
		Sound,

		/// <summary>
		/// Audio data are streamed
		/// </summary>
		Music
	}
}
