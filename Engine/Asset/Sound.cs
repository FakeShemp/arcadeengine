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


using System.IO;
using System;
using System.Drawing;
using System.Xml;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio;
//using OpenTK.Math;


namespace ArcEngine.Asset
{
	/// <summary>
	/// http://www.games-creators.org/wiki/Utiliser_FMOD_en_C_sharp
	/// http://www.devmaster.net/articles.php?catID=6
	/// http://www.gamedev.net/reference/articles/article2008.asp
	/// </summary>
	public class Sound : IDisposable, IAsset
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
			if (string.IsNullOrEmpty(filename))
				return false;

			Stream stream = ResourceManager.LoadResource(filename);
			if (stream == null)
				return false;

			//using (AudioReader sound = new AudioReader(stream))
			//{
			//   AL.BufferData(Buffer, sound.ReadToEnd());
			//   AL.Source(Source, ALSourcei.Buffer, Buffer);
			//}

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
			return true;
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
					// Texture
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
	


		#region Initialization



		/// <summary>
		/// Init sound manager
		/// </summary>
		static internal bool Init()
		{
			if (IsInit)
			{
				Trace.WriteLine("AudioContext already initialized !");
				return true;
			}

			Trace.WriteLine("Init sound system... ");
			Trace.Indent();

			Context = new AudioContext();
			if (Context == null)
			{
				Trace.WriteLine("Failed to initialize AudioContext!");
				Trace.Unindent();
				return false;
			}


			Trace.Unindent();
			return true;
		}


		/// <summary>
		/// Trace audio informations
		/// </summary>
		static public void TraceInfos()
		{
			if (!IsInit)
			{
				Trace.WriteLine("Audion Context not initialized !");
				return;
			}


			Trace.WriteLine("--- Audio ---");
			Trace.Indent();
			{
				//Trace.WriteLine("Used Device: " + Context.);
				Trace.WriteLine("AL Renderer: " +  AL.Get(ALGetString.Renderer));
				Trace.WriteLine("AL Vendor: " + AL.Get(ALGetString.Vendor));
				Trace.WriteLine("AL Version: " + AL.Get(ALGetString.Version));

				Trace.WriteLine("AL Speed of sound: " + AL.Get(ALGetFloat.SpeedOfSound));
				Trace.WriteLine("AL Distance Model: " + AL.GetDistanceModel().ToString());
			//	Trace.WriteLine("AL Maximum simultanous Sources: " + MaxSources);

		//		Trace.WriteLine("AL Extension string: " + ExtensionString);
		//		Trace.WriteLine("Confirmed AL Extensions:");
				//Trace.Indent();
				//{
				//   foreach (KeyValuePair<string, bool> pair in Extensions)
				//      Trace.WriteLine(pair.Key + ": " + pair.Value);
				//}
				//Trace.Unindent();
			}
			Trace.Unindent();

		}


		/// <summary>
		/// Close audio context
		/// </summary>
		static internal void Close()
		{
			if (Context != null)
				Context.Dispose();
			Context = null;


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
		/// Name of the sound
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "sound";
			}
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
		/// Audio Context
		/// </summary>
		static public AudioContext Context
		{
			private set;
			get;
		}


		/// <summary>
		/// Is Audio Context is initialized
		/// </summary>
		static public bool IsInit
		{
			get
			{
				return Context != null;
			}
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
