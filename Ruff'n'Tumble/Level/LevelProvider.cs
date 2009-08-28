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


namespace RuffnTumble.Asset
{

	/// <summary>
	/// Font2D provider
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

			Name = "ArcEngine Level";
			Tags = new string[] { "level", "model" };
			Assets = new Type[] { 
					typeof(Level),
					typeof(Model),  
			//		typeof(Layer)
				};
			Version = new Version(0, 1);
		}



		#region IO routines


		/// <summary>
		/// Saves all assets
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{
			foreach (XmlNode node in Levels.Values)
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

			switch (xml.Name.ToLower())
			{
				case "level":
				{

					string name = xml.Attributes["name"].Value;
					Levels[name] = xml;
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

			if (typeof(T) == typeof(Level))
			{
				if (Levels.ContainsKey(name))
					node = Levels[name];

				form = new Editor.LevelForm(node);
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

			Levels[name] = node;
		}


		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <returns>asset's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			foreach (string key in Levels.Keys)
			{
				list.Add(key);
			}

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

			if (!Levels.ContainsKey(name))
				return default(T);

			// Creates a Texture
			Level level = new Level();
			level.Load(Levels[name]);

			return (T)(object)level;
		}



		/// <summary>
		/// Returns a <c>StringTable</c>
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (!Levels.ContainsKey(name))
				return null;

			return Levels[name];
		}



		/// <summary>
		/// Flush unused scripts
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
		}




		/// <summary>
		/// Removes a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
		}



		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{

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
			if (typeof(T) == typeof(Level))
			{
				if (SharedLevels.ContainsKey(name))
					return (T)(object)SharedLevels[name];

				Level level = new Level();
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


		#region Progerties


		/// <summary>
		/// Scripts
		/// </summary>
		Dictionary<string, XmlNode> Levels;


		/// <summary>
		/// Shared levels
		/// </summary>
		Dictionary<string, Level> SharedLevels;

		#endregion
	}
}
