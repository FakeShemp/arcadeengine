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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Graphic;
using WeifenLuo.WinFormsUI.Docking;
using OpenTK.Audio;

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
			Sounds = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedSounds = new Dictionary<string, Sound>(StringComparer.OrdinalIgnoreCase);

			Name = "Audio";
			Tags = new string[] { "audio" };
			Assets = new Type[] { typeof(Sound) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetResource("ArcEngine.Data.Icons.TileSet.png"));

		}



		#region IO routines


		/// <summary>
		/// Saves all audios
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{

			if (type == typeof(Sound))
			{
				foreach (XmlNode node in Sounds.Values)
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

					Sounds[xml.Attributes["name"].Value] = xml;
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

			if (typeof(T) == typeof(Sound))
			{
				XmlNode node = null;
				if (Sounds.ContainsKey(name))
					node = Sounds[name];

			//	form = new SoundForm(node);
			//	form.TabText = name;
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

			if (typeof(T) == typeof(Sound))
				Sounds[name] = node;

		}


		/// <summary>
		/// Returns an array of all available sound
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Sound's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(Sound))
			{
				foreach (string key in Sounds.Keys)
					list.Add(key);
			}

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

			if (typeof(T) == typeof(Sound))
			{
				if (!Sounds.ContainsKey(name))
					return default(T);

				Sound sound = new Sound();
				sound.Load(Sounds[name]);

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

			if (typeof(T) == typeof(Sound))
			{
				if (Sounds.ContainsKey(name))
					return Sounds[name];
			}

			return null;
		}



		/// <summary>
		/// Removes a sound
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
		}




		/// <summary>
		/// Removes a Sound
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Sounds.Clear();
		}

		#endregion


		#region Shared assets


		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(Sound))
			{
				if (SharedSounds.ContainsKey(name))
					return (T)(object)SharedSounds[name];

				Sound sound = new Sound();
				sound.Load(Sounds[name]);
				SharedSounds[name] = sound;

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
			if (typeof(T) == typeof(Sound))
			{
				SharedSounds[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(StringTable))
			{
				SharedSounds.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedSounds.Clear();
		}



		#endregion



		#region Progerties


		/// <summary>
		/// Sound
		/// </summary>
		Dictionary<string, XmlNode> Sounds;

		/// <summary>
		/// Shared Sounds
		/// </summary>
		Dictionary<string, Sound> SharedSounds;

		#endregion
	}
}
