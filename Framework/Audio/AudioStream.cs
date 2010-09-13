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
using ArcEngine.Audio;
using OpenAL = OpenTK.Audio.OpenAL;


// http://www.marek-knows.com/
// http://www.xiph.org/vorbis/doc/vorbisfile/example.html
// http://loulou.developpez.com/tutoriels/openal/flux-ogg/
	


namespace ArcEngine.Asset
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
			if (Audio.Audio.Context == null)
				throw new InvalidOperationException("Initialize Audio context first");

			// Add to the know list of audio streams
			if (Streams == null)
				Streams = new List<AudioStream>();
			Streams.Add(this);

			Buffers = OpenAL.AL.GenBuffers(2);
			Audio.Audio.Check();

			Source = OpenAL.AL.GenSource();
			Audio.Audio.Check();

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

			IsDisposed = true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			return false;
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


			Vector3 vec = Vector3.Zero;
			OpenAL.AL.Source(Source, OpenAL.ALSource3f.Position, vec.X, vec.Y, vec.Z);
			OpenAL.AL.Source(Source, OpenAL.ALSource3f.Velocity, vec.X, vec.Y, vec.Z);
			OpenAL.AL.Source(Source, OpenAL.ALSource3f.Direction, vec.X, vec.Y, vec.Z);
			OpenAL.AL.Source(Source, OpenAL.ALSourcef.RolloffFactor, 0.0f);
			OpenAL.AL.Source(Source, OpenAL.ALSourceb.SourceRelative, true);

			return true;
		}


		/// <summary>
		/// Play the sound
		/// </summary>
		public void Play()
		{
			if (IsPlaying)
				return;

			if (!Stream(Buffers[0]))
				return;

			if (!Stream(Buffers[1]))
				return;

			OpenAL.AL.SourceQueueBuffers(Source, Buffers.Length, Buffers);
			OpenAL.AL.SourcePlay(Source);

			Status = AudioSourceState.Playing;
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

			OpenAL.AL.GetSource(Source, OpenAL.ALGetSourcei.BuffersProcessed, out processed);

			while (processed-- != 0)
			{
				int buffer;

				buffer = OpenAL.AL.SourceUnqueueBuffer(Source);
				Audio.Audio.Check();

				active = Stream(buffer);

				OpenAL.AL.SourceQueueBuffer(Source, buffer);
				Audio.Audio.Check();
			}

			if (!IsPlaying)
			{
				OpenAL.AL.SourcePlay(Source);
			}

			if (!active && Loop)
			{
				if (!IsPlaying)
				{
					Rewind();
				}
				return true;
			}

			return active;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		bool Stream(int buffer)
		{
			byte[] data = new byte[BUFFER_SIZE];
			int size = 0;
			int result;

			while (size < BUFFER_SIZE)
			{
				result = oggStream.Read(data, size, BUFFER_SIZE - size);
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

			OpenAL.AL.BufferData(buffer, (OpenAL.ALFormat)oggStream.Format, data, data.Length, oggStream.Rate);
			Audio.Audio.Check();

			return true;
		}


		/// <summary>
		/// 
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
		/// 
		/// </summary>
		OggInputStream oggStream;


		/// <summary>
		/// 
		/// </summary>
		string FileName;


		/// <summary>
		/// Loop playing
		/// </summary>
		public bool Loop;


		/// <summary>
		/// Does the source playing 
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				OpenAL.ALSourceState state;

				state = OpenAL.AL.GetSourceState(Source);

				return (state == OpenAL.ALSourceState.Playing);
			}
		}


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
		/// Audio source
		/// </summary>
		int Source;


		/// <summary>
		/// Buffer size
		/// </summary>
		const int BUFFER_SIZE = 4096 * 64;

		/// <summary>
		/// 
		/// </summary>
		public AudioSourceState Status
		{

			get;
			private set;
		}

		#endregion

	}
}
