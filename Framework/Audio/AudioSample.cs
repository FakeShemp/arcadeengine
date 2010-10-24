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
using System.IO;
using System.Xml;
using ArcEngine.Asset;
using OpenAL = OpenTK.Audio.OpenAL;
using ArcEngine.Interface;

/// http://www.games-creators.org/wiki/Utiliser_FMOD_en_C_sharp
/// http://www.devmaster.net/articles.php?catID=6
/// http://www.gamedev.net/reference/articles/article2008.asp

namespace ArcEngine.Audio
{
	/// <summary>
	/// Audio sample loaded in memory
	/// </summary>
	public class AudioSample : IDisposable, IAsset
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public AudioSample()
		{
			if (!AudioManager.IsInit)
			{
				Trace.WriteLine("[AudioSample] AudioSample() : No audio context available.");
				return;
			}

			Buffer = OpenAL.AL.GenBuffer();

			Pitch = 1.0f;
			MaxGain = 1.0f;
			Position = Vector3.Zero;
			Velocity = Vector3.Zero;

			IsDisposed = false;
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~AudioSample()
		{
			if (!IsDisposed)
				throw new Exception("[AudioSample] : Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Buffer != 0)
				OpenAL.AL.DeleteBuffer(Buffer);
			Buffer = 0;

			IsDisposed = true;
		}




		#region Loaders


		/// <summary>
		/// Loads a WAV sound
		/// </summary>
		/// <param name="filename">File to load</param>
		/// <returns>True if successful</returns>
		public bool LoadSound(string filename)
		{
			if (!AudioManager.IsInit)
			{
				Trace.WriteLine("[AudioSample] LoadSound() : Failed to load sample, audio system not initialized.");
				return false;
			}

			using (Stream stream = ResourceManager.LoadAsset(filename))
			{
				if (stream == null)
					return false;

				int channels, bits_per_sample, sample_rate;
				byte[] sound_data = LoadWave(stream, out channels, out bits_per_sample, out sample_rate);

				// Transfert data
				OpenAL.AL.BufferData(Buffer, (OpenAL.ALFormat)GetSoundFormat(channels, bits_per_sample), sound_data, sound_data.Length, sample_rate);

				return true;
			}
		}


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
		/// Returns sound format
		/// </summary>
		/// <param name="channels"></param>
		/// <param name="bits"></param>
		/// <returns></returns>
		private AudioFormat GetSoundFormat(int channels, int bits)
		{
			switch (channels)
			{
				case 1:
				return bits == 8 ? AudioFormat.Mono8 : AudioFormat.Mono16;
				case 2:
				return bits == 8 ? AudioFormat.Stereo8 : AudioFormat.Stereo16;
				default:
				throw new NotSupportedException("The specified sound format is not supported.");
			}
		}


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
						LoadSound(node.Attributes["name"].Value);
					}
					break;

				}
			}

			return true;
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
				return "audiosample";
			}
		}


		/// <summary>
		/// ID of the sound buffer
		/// </summary>
		internal int Buffer
		{
			get;
			private set;
		}


		/// <summary>
		/// Turns sound looping on or off
		/// </summary>
		public bool Loop;


		/// <summary>
		/// Pitch of the sound
		/// </summary>
		public float Pitch;


		/// <summary>
		/// Gain of the sound
		/// </summary>
		public float MaxGain;


		/// <summary>
		/// Gain of the sound
		/// </summary>
		public float MinGain;


		/// <summary>
		/// Position of the sound
		/// </summary>
		public Vector3 Position;


		/// <summary>
		/// Velocity of the sound
		/// </summary>
		public Vector3 Velocity;


		#endregion
	}

}
