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
using System.Xml;
using ArcEngine.Asset;
using OpenAL = OpenTK.Audio.OpenAL;


// http://www.marek-knows.com/
// http://www.xiph.org/vorbis/doc/vorbisfile/example.html
// http://loulou.developpez.com/tutoriels/openal/flux-ogg/
	


namespace ArcEngine.Audio
{
	/// <summary>
	///Audio streaming class
	/// </summary>
	public class AudioStream : IDisposable, IAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public AudioStream()
		{
			if (AudioManager.Context == null)
				throw new InvalidOperationException("Initialize Audio context first");

			// Add to the known list of audio streams
			if (Streams == null)
				Streams = new List<AudioStream>();
			Streams.Add(this);

			Buffers = OpenAL.AL.GenBuffers(2);
			AudioManager.Check();

			Source = OpenAL.AL.GenSource();
			AudioManager.Check();

			BufferData = new Dictionary<int, byte[]>();
			BufferData[Buffers[0]] = new byte[44100];
			BufferData[Buffers[1]] = new byte[44100];
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		public void Dispose()
		{
			Streams.Remove(this);

			if (oggStream != null)
				oggStream.Dispose();
			oggStream = null;

			// Generates buffers
			if (Buffers != null)
				OpenAL.AL.DeleteBuffers(Buffers);
			Buffers = null;

			// Generate source
			if (Source != -1)
				OpenAL.AL.DeleteSource(Source);
			Source = -1;

			FileName = string.Empty;
			BufferData = null;

			IsDisposed = true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			return true;
		}


		/// <summary>
		/// Loads an Ogg Vorbis file
		/// </summary>
		/// <param name="filename">File to open</param>
		/// <returns></returns>
		public bool LoadOgg(string filename)
		{
			AssetHandle handle = ResourceManager.LoadResource(filename);
			if (handle == null)
				return false;

			oggStream = new OggInputStream(handle.Stream);
			FileName = filename;

			Position = Vector3.Zero;
			Direction = Vector3.Zero;
			Velocity = Vector3.Zero;
			RolloffFactor = 0.0f;
			SourceRelative = true;

			return true;
		}

	
		/// <summary>
		/// Play the sound
		/// </summary>
		public void Play()
		{
			if (State == AudioSourceState.Playing)
				return;

			// Fill first buffer
			if (!Stream(Buffers[0], BufferData[Buffers[0]]))
				return;

			// Fill second buffer
			if (!Stream(Buffers[1], BufferData[Buffers[1]]))
				return;

			// Add buffers to the queue
			OpenAL.AL.SourceQueueBuffers(Source, Buffers.Length, Buffers);

			// Play source
			OpenAL.AL.SourcePlay(Source);
		}


		/// <summary>
		/// Stop the sound
		/// </summary>
		public void Stop()
		{
			OpenAL.AL.SourceStop(Source);
		}


		/// <summary>
		/// Pause sound
		/// </summary>
		public void Pause()
		{
			OpenAL.AL.SourcePause(Source);
		}


		/// <summary>
		/// Update a sound buffer
		/// </summary>
		internal bool Process()
		{
			int processed = 0;
			bool active = true;

			// Get free buffers
			OpenAL.AL.GetSource(Source, OpenAL.ALGetSourcei.BuffersProcessed, out processed);
			while (processed-- != 0)
			{
				// Remove buffer from the queue
				int buffer = OpenAL.AL.SourceUnqueueBuffer(Source);
				AudioManager.Check();

				// Fill buffer
				active = Stream(buffer, BufferData[buffer]);

				// Enqueue buffer
				OpenAL.AL.SourceQueueBuffer(Source, buffer);
				AudioManager.Check();
			}

			// If not playing, play
			if (State != AudioSourceState.Playing)
			{
				OpenAL.AL.SourcePlay(Source);
			}

			// Loop mode ?
			if (!active && Loop)
			{
				if (State != AudioSourceState.Playing)
					Rewind();

				return true;
			}

			return active;
		}


		/// <summary>
		/// Fill a buffer with audio data
		/// </summary>
		/// <param name="bufferid">Buffer id</param>
		/// <param name="data">Buffer to fill</param>
		/// <returns>True if no errors</returns>
		bool Stream(int bufferid, byte[] data)
		{
			if (data == null | data.Length == 0)
				return false;

			int size = 0;
			while (size < data.Length)
			{
				int result = oggStream.Read(data, size, data.Length - size);
				if (result > 0)
				{
					size += result;
				}
				else
				{
					if (result < 0)
					{
						Trace.WriteLine("[AudioStream] : Stream Error : {0}", result);
						throw new Exception("Stream Error: " + result);
					}
					else
					{
						break;
					}
				}
			}

			if (size == 0)
			{
				return false;
			}

			// Fill OpenAL buffer
			OpenAL.AL.BufferData(bufferid, (OpenAL.ALFormat)oggStream.Format, data, data.Length, oggStream.Rate);
			AudioManager.Check();

			return true;
		}


		/// <summary>
		/// Rewind the stream
		/// </summary>
		public void Rewind()
		{
			string filename = FileName;
			Dispose();
			LoadOgg(filename);
		}


		#region statics

		/// <summary>
		/// Update 
		/// </summary>
		static internal void Update()
		{
			if (Streams == null)
				return;

			foreach (AudioStream audio in Streams)
			{
				audio.Process();
			}
		}


		/// <summary>
		/// Registred streams
		/// </summary>
		static List<AudioStream> Streams;

		#endregion



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
						//LoadSound(node.Attributes["name"].Value);
					}
					break;

				}
			}

			return true;
		}

		#endregion
	


		#region Properties


		/// <summary>
		/// Sound position
		/// </summary>
		public Vector3 Position
		{
			get
			{
				Vector3 v = Vector3.Zero;
				OpenAL.AL.GetSource(Source, OpenAL.ALSource3f.Position, out v.X, out v.Y, out v.Z);
				return v;
			}
			set
			{
				OpenAL.AL.Source(Source, OpenAL.ALSource3f.Position, value.X, value.Y, value.Z);
			}
		}


		/// <summary>
		/// Sound direction
		/// </summary>
		public Vector3 Direction
		{
			get
			{
				Vector3 v = Vector3.Zero;
				OpenAL.AL.GetSource(Source, OpenAL.ALSource3f.Direction, out v.X, out v.Y, out v.Z);
				return v;
			}
			set
			{
				OpenAL.AL.Source(Source, OpenAL.ALSource3f.Direction, value.X, value.Y, value.Z);
			}
		}


		/// <summary>
		/// Sound velocity
		/// </summary>
		public Vector3 Velocity
		{
			get
			{
				Vector3 v = Vector3.Zero;
				OpenAL.AL.GetSource(Source, OpenAL.ALSource3f.Velocity, out v.X, out v.Y, out v.Z);
				return v;
			}
			set
			{
				OpenAL.AL.Source(Source, OpenAL.ALSource3f.Velocity, value.X, value.Y, value.Z);
			}
		}


		/// <summary>
		/// Rolloff factor
		/// </summary>
		public float RolloffFactor
		{
			get
			{
				float f;
				OpenAL.AL.GetSource(Source, OpenAL.ALSourcef.RolloffFactor, out f);
				return f;
			}
			set
			{
				OpenAL.AL.Source(Source, OpenAL.ALSourcef.RolloffFactor, value);
			}
		}


		/// <summary>
		/// Rolloff factor
		/// </summary>
		public bool SourceRelative
		{
			get
			{
				bool b;
				OpenAL.AL.GetSource(Source, OpenAL.ALSourceb.SourceRelative, out b);
				return b;
			}
			set
			{
				OpenAL.AL.Source(Source, OpenAL.ALSourceb.SourceRelative, value);
			}
		}


		/// <summary>
		/// Ogg stream
		/// </summary>
		OggInputStream oggStream;


		/// <summary>
		/// File name of the audio stream
		/// </summary>
		string FileName;


		/// <summary>
		/// Loop playing
		/// </summary>
		public bool Loop;


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
		public bool IsDisposed
		{
			get;
			private set;
		}


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "audiostream";
			}
		}


		/// <summary>
		/// Audio buffers
		/// </summary>
		int[] Buffers;


		/// <summary>
		/// Buffer data
		/// </summary>
		Dictionary<int, byte[]> BufferData;
		

		/// <summary>
		/// Audio source
		/// </summary>
		int Source;


		/// <summary>
		/// State of the stream
		/// </summary>
		public AudioSourceState State
		{
			get
			{
				return (AudioSourceState) OpenAL.AL.GetSourceState(Source);
			}
		}

		#endregion

	}
}
