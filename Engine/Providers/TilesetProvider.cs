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


namespace ArcEngine.Providers
{

	/// <summary>
	/// Tileset provider
	/// </summary>
	public class TileSetProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public TileSetProvider()
		{
			TileSets = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedTileSets = new Dictionary<string, TileSet>(StringComparer.OrdinalIgnoreCase);

			Name = "TileSet";
			Tags = new string[] { "tileset" };
			Assets = new Type[] { typeof(TileSet) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetResource("ArcEngine.Data.Icons.TileSet.png"));

		}




		#region Init & Close


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			return false;
		}



		/// <summary>
		/// Close all opened resources
		/// </summary>
		public override void Close()
		{

		}

		#endregion


		#region IO routines


		/// <summary>
		/// Saves all scripts
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{

			if (typeof(T) == typeof(TileSet))
			{
				foreach (XmlNode node in TileSets.Values)
					node.WriteTo(xml);
			}

/*	
			foreach (KeyValuePair<string, XmlNode> kvp in TileSets)
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
		/// Loads a TileSet
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
				case "tileset":
				{

					TileSets[xml.Attributes["name"].Value] = xml;
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

			if (typeof(T) == typeof(TileSet))
			{
				XmlNode node = null;
				if (TileSets.ContainsKey(name))
					node = TileSets[name];

				form = new TileSetForm(node);
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

			if (typeof(T) == typeof(TileSet))
				TileSets[name] = node;
		}


		/// <summary>
		/// Returns an array of all available Tilesets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Script's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(TileSet))
				foreach (string key in TileSets.Keys)
					list.Add(key);

			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a new TileSet
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>Asset or null</returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(TileSet) && TileSets.ContainsKey(name))
			{
				TileSet tileset = new TileSet();
				tileset.Load(TileSets[name]);
				return (T)(object)tileset;
			}

			return default(T);
		}



		/// <summary>
		/// Returns a <c>Script</c>
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(TileSet) && TileSets.ContainsKey(name))
				return TileSets[name];


			return null;
		}



		/// <summary>
		/// Removes a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(TileSet) && TileSets.ContainsKey(name))
				TileSets.Remove(name);

		}




		/// <summary>
		/// Removes a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
			if (typeof(T) == typeof(TileSet))
				TileSets.Clear();
	}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			TileSets.Clear();
		}

		/// <summary>
		/// Returns the number of known assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Number of available asset</returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(TileSet))
				return TileSets.Count;

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

			if (typeof(T) == typeof(TileSet))
				SharedTileSets[name] = asset as TileSet;	
		}




		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(TileSet))
			{
				if (SharedTileSets.ContainsKey(name))
					return (T)(object)SharedTileSets[name];

				SharedTileSets[name] = new TileSet();
				return (T)(object)SharedTileSets[name];
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

			if (typeof(T) == typeof(TileSet))
				SharedTileSets.Remove(name);
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(StringTable))
			{
				SharedTileSets.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedTileSets.Clear();
		}



		#endregion


		#region Progerties


		/// <summary>
		/// Tilesets
		/// </summary>
		Dictionary<string, XmlNode> TileSets;

		/// <summary>
		/// Shared tilesets
		/// </summary>
		Dictionary<string, TileSet> SharedTileSets;

		#endregion
	}
}
