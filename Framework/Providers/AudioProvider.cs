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

using ArcEngine.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Editor;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace ArcEngine.Providers
{

	/// <summary>
	/// Audio provider
	/// </summary>
	public class AudioProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public AudioProvider()
		{
			Audios = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedAudios = new Dictionary<string, Audio>(StringComparer.OrdinalIgnoreCase);

			Name = "Audio";
			Tags = new string[] { "audio" };
			Assets = new Type[] { typeof(Audio) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetResource("ArcEngine.Data.Icons.TileSet.png"));

		}



		/// <summary>
		/// Trace audio informations
		/// </summary>
		public void TraceInfos()
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
				Trace.WriteLine("AL Renderer: " + AL.Get(ALGetString.Renderer));
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


		#region Init & Close

		/// <summary>
		/// Init sound manager
		/// </summary>
		public override bool Init()
		{
			return true;

			if (IsInit)
			{
				Trace.WriteLine("AudioContext already initialized !");
				return true;
			}

			Trace.Write("Init sound system... ");
			Trace.Indent();

            try
            {
                Context = new AudioContext();
                if (Context == null)
                {
                    Trace.WriteLine("Failed to initialize AudioContext <!");
                    Trace.Unindent();
                    return false;
                }

                Trace.WriteLine("OK !");
                Trace.Unindent();
                return true;
            }
            catch (Exception)
            {
                Trace.WriteLine("Exception thrown when creating AudioContext !");
                Trace.Unindent();
            }

            return false;
		}



		/// <summary>
		/// Close audio context
		/// </summary>
		public override void Close()
		{
			if (Context != null)
				Context.Dispose();
			Context = null;

			Trace.WriteLine("Audio closed.");

		}
		#endregion


		#region IO routines


		/// <summary>
		/// Saves all audios
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{

			if (typeof(T) == typeof(Audio))
			{
				foreach (XmlNode node in Audios.Values)
					node.WriteTo(xml);
			}

			/*	
						foreach (KeyValuePair<string, XmlNode> kvp in Sound)
						{
							xml.WriteStartElement("tileset");
							xml.WriteAttributeString("name", kvp.Key);

							kvp.Value.WriteContentTo(xml);

							xml.WriteEndElement();

						}
			*/

			return true;
		}




		/// <summary>
		/// Loads audio
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			string name = xml.Name.ToLower();

			switch (name)
			{
				case "audio":
				{

					Audios[xml.Attributes["name"].Value] = xml;
					//string name = xml.Attributes["name"].Value;
					//TileSet tileset = Create<TileSet>(name);
					//if (tileset != null)
					//   tileset.Load(xml);
				}
				break;
			}


			return true;
		}


		#endregion


		#region Editor


		/// <summary>
		/// Edits an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override AssetEditor EditAsset<T>(string name)
		{
			AssetEditor form = null;

			if (typeof(T) == typeof(Audio))
			{
				XmlNode node = null;
				if (Audios.ContainsKey(name))
					node = Audios[name];

				form = new AudioForm(node);
				form.TabText = name;
			}

			return form;
		}


		#endregion


		#region Assets

		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public override void Add<T>(string name, XmlNode node)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Audio))
				Audios[name] = node;
		}


		/// <summary>
		/// Returns an array of all available sound
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Sound's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(Audio))
				foreach (string key in Audios.Keys)
					list.Add(key);


			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a new Sound
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>Asset or null</returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Audio) && Audios.ContainsKey(name))
			{
				Audio sound = new Audio();
				sound.Load(Audios[name]);

				return (T)(object)sound;
			}


			return default(T);
		}



		/// <summary>
		/// Returns an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Audio) && Audios.ContainsKey(name))
					return Audios[name];

			return null;
		}



		/// <summary>
		/// Removes a sound
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Audio) && Audios.ContainsKey(name))
				Audios.Remove(name);
		}




		/// <summary>
		/// Removes a Sound
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
			if (typeof(T) == typeof(Audio))
				Audios.Clear();
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Audios.Clear();
		}

		/// <summary>
		/// Returns the number of known assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Number of available asset</returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(Audio))
				return Audios.Count;

			return 0;
		}

		#endregion


		#region Shared assets

		/// <summary>
		/// Adds an asset as Shared
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset to register</param>
		/// <param name="asset">Asset's handle</param>
		public override void AddShared<T>(string name, IAsset asset)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (typeof(T) == typeof(Audio))
				SharedAudios[name] = asset as Audio;
	
		}

		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(Audio))
			{
				if (SharedAudios.ContainsKey(name))
					return (T)(object)SharedAudios[name];

				Audio sound = new Audio();
				sound.Load(Audios[name]);
				SharedAudios[name] = sound;

				return (T)(object)sound;
			}

			return default(T);
		}



		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public override void RemoveShared<T>(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (typeof(T) == typeof(Audio))
				SharedAudios.Remove(name);
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(Audio))
			{
				SharedAudios.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedAudios.Clear();
		}



		#endregion


		#region Properties


		/// <summary>
		/// Is Audio Context is initialized
		/// </summary>
		public bool IsInit
		{
			get
			{
				return Context != null;
			}
		}



		/// <summary>
		/// Audio Context
		/// </summary>
		public AudioContext Context
		{
			private set;
			get;
		}


		/// <summary>
		/// Sound
		/// </summary>
		Dictionary<string, XmlNode> Audios;

		/// <summary>
		/// Shared Sounds
		/// </summary>
		Dictionary<string, Audio> SharedAudios;

		#endregion
	}
}
