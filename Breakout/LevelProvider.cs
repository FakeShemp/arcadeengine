using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Providers;
using System.Xml;


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
		public override List<string> GetAssets(Type type)
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
		public override void Save(Type type, XmlWriter xml)
		{
			foreach (XmlNode node in Levels.Values)
				node.WriteTo(xml);

		}




		/// <summary>
		/// Loads a level
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override void Load(XmlNode xml)
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

		}



		#endregion


		#region Properties


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
