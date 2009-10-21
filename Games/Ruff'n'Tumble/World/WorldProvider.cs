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
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Providers;
using ArcEngine;


namespace RuffnTumble
{

	/// <summary>
	/// Font2D provider
	/// </summary>
	public class WorldProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public WorldProvider()
		{
			Worlds = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedWorlds = new Dictionary<string, Level>(StringComparer.OrdinalIgnoreCase);

			Name = "ArcEngine World";
			Tags = new string[] { "world" };
			Assets = new Type[] { 
					typeof(World),
				};
			Version = new Version(0, 1);
		}


		#region Init & Close


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			return true;
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
		/// Saves all assets
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{
			if (xml == null)
				return false;

			foreach (XmlNode node in Worlds.Values)
				node.WriteTo(xml);


			return true;
		}




		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			switch (xml.Name.ToLower())
			{
				case "world":
				{

					string name = xml.Attributes["name"].Value;
					Worlds[name] = xml;
				}
				break;

				default:
				{
					Trace.WriteLine("?" + xml.Name);
				}
				break;
			}


			return true;
		}



		#endregion


		#region Assets


		/// <summary>
		/// Edits an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override AssetEditor EditAsset<T>(string name)
		{
			AssetEditor form = null;
			XmlNode node = null;

			if (typeof(T) == typeof(World))
			{
				if (Worlds.ContainsKey(name))
					node = Worlds[name];

				form = new Editor.WorldForm(node);
			}

			form.TabText = name;
			return form;
		}


		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public override void Add<T>(string name, XmlNode node)
		{
			CheckValue<T>(name);

			if(typeof(T) == typeof(World))
				Worlds[name] = node;
		}


		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <returns>asset's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(World)) 
				foreach (string key in Worlds.Keys)
					list.Add(key);
	
			list.Sort();
			return list;
		}



		/// <summary>
		/// Creates an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(World) && Worlds.ContainsKey(name))
			{
				World world = new World();
				world.Load(Worlds[name]);

				return (T)(object)world;
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

			if(typeof(T) == typeof(Level) && Worlds.ContainsKey(name))
				return Worlds[name];

			return null;
		}



		/// <summary>
		/// Flush unused assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
			if (typeof(T) == typeof(Level))
				Worlds.Clear();
		}




		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
			if (typeof(T) == typeof(Level) && Worlds.ContainsKey(name))
				Worlds.Remove(name);
		}



		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Worlds.Clear();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(World))
				return Worlds.Count;

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

			if (typeof(T) == typeof(Level))
			{
				SharedWorlds[name] = asset as Level;
			}

		}



		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(Level))
			{
				if (SharedWorlds.ContainsKey(name))
					return (T)(object)SharedWorlds[name];

				Level level = new Level();
				SharedWorlds[name] = level;

				return (T)(object)level;
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
			if (typeof(T) == typeof(Level))
			{
				SharedWorlds[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(Level))
				SharedWorlds.Clear();
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedWorlds.Clear();
		}



		#endregion


		#region Properties


		/// <summary>
		/// Wolrds
		/// </summary>
		Dictionary<string, XmlNode> Worlds;


		/// <summary>
		/// Shared Worlds
		/// </summary>
		Dictionary<string, Level> SharedWorlds;

		#endregion
	}
}
