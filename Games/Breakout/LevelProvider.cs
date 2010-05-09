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
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Forms;


namespace Breakout
{
	/// <summary>
	/// 
	/// </summary>
	public class LevelProvider : Provider
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public LevelProvider()
		{
			Levels = new Dictionary<string, XmlNode>();
			SharedLevels = new Dictionary<string, Level>();

			Name = "ArcEngine Breakout";
			Tags = new string[] { "level" };
			Assets = new Type[] { typeof(Level) };
			Version = new Version(0, 1);

		}


		/// <summary>
		/// 
		/// </summary>
		public override void Dispose()
		{
			

		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			return true;
		}



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

			Levels[name] = node;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (!Levels.ContainsKey(name))
				return default(T);

			Level level = new Level(name);
			level.Load(Levels[name]);

			return (T)(object)level;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (!Levels.ContainsKey(name))
				return null;

			return Levels[name];
		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			if (typeof(T) == typeof(Level))
			{
				foreach (string key in Levels.Keys)
				{
					list.Add(key);
				}
			}

			list.Sort();
			return list;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{

		}



		/// <summary>
		/// 
		/// </summary>
		public override void Clear()
		{
			Levels.Clear();
		}

		#endregion


		#region Editor
		
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override AssetEditor EditAsset<T>(string name)
		{


			return null;
		}

		#endregion


		#region Shared assets



		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="asset"></param>
		public override void AddShared<T>(string name, ArcEngine.Asset.IAsset asset)
		{
			
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
				if (SharedLevels.ContainsKey(name))
					return (T)(object)SharedLevels[name];

				Level level = new Level("");
				SharedLevels[name] = level;

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
				SharedLevels[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(Level))
				SharedLevels.Clear();
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedLevels.Clear();
		}



		#endregion


		#region IO routines


		/// <summary>
		/// Saves all scripts
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{
			foreach (XmlNode node in Levels.Values)
				node.WriteTo(xml);


			return true;
		}




		/// <summary>
		/// Loads a level
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{

			switch (xml.Name.ToLower())
			{
				case "level":
				{
					Add<Level>(xml.Attributes["name"].Value, xml);
				}
				break;

				default:
				{

				}
				break;
			}


			return true;
		}



		#endregion


		#region Properties

		/// <summary>
		/// Returns the number of known assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Number of available asset</returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(Level))
				return Levels.Count;

			return 0;
		}


		/// <summary>
		/// Levels
		/// </summary>
		Dictionary<string, XmlNode> Levels;


		/// <summary>
		/// Shared levels
		/// </summary>
		Dictionary<string, Level> SharedLevels;

		#endregion
	}
}
