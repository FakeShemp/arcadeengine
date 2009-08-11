using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Providers;

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
					typeof(Layer)
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
		public override void Save(Type type, XmlWriter xml)
		{
			foreach (XmlNode node in Levels.Values)
				node.WriteTo(xml);

		}




		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override void Load(XmlNode xml)
		{

			switch (xml.Name.ToLower())
			{
				case "level":
				{

					string name = xml.Attributes["name"].Value;
					Levels.Add(name, xml);
				}
				break;

				default:
				{
					ArcEngine.Log.Send(new ArcEngine.LogEventArgs(ArcEngine.LogLevel.Error, "?" + xml.Name, null));
				}
				break;
			}

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

			Levels[name] = node;
		}

		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <returns>asset's name array</returns>
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
