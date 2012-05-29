using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Interface;

namespace RuffnTumble
{
	/// <summary>
	/// World asset definition
	/// </summary>
	public class World : IAsset
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public World()
		{
			Levels = new Dictionary<string, Level>();
		}



		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			foreach (Level level in Levels.Values)
				level.Dispose();
		}

		/// <summary>
		/// Update the world
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
			if (CurrentLevel != null)
				CurrentLevel.Update(time);
		}


		/// <summary>
		/// Draw the world
		/// </summary>
		public void Draw(SpriteBatch batch)
		{
			if (CurrentLevel != null)
				CurrentLevel.Draw(batch);
		}



		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;


			xml.WriteStartElement("world");
			xml.WriteAttributeString("name", Name);


			foreach (Level level in Levels.Values)
				level.Save(xml);

			xml.WriteEndElement();

			return true;
		}

		/// <summary>
		/// Loads the level from a bank
		/// </summary>
		/// <param name="xml">xml node</param>
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

				switch (node.Name.ToLower())
				{
					case "level":
					{
						string name = node.Attributes["name"].Value;
						Level level = new Level();
						level.Load(node);

						AddLevel(name, level);
					}
					break;

					default:
					{
						Trace.WriteLine("World '{0}' : Unknown node element found (\"{1}\")", Name, node.Name);
					}
					break;
				}
			}

			return true;
		}

		#endregion


		#region Level management

		/// <summary>
		/// Sets the level to use
		/// </summary>
		/// <param name="name">Name of the new level</param>
		/// <returns>true if level found</returns>
		public bool SetLevel(string name)
		{
			if (!Levels.ContainsKey(name))
				return false;

			CurrentLevel = Levels[name];
			CurrentLevel.Init();

			return true;
		}


		/// <summary>
		/// Returns a list of all available levels
		/// </summary>
		/// <returns>A list of level's name</returns>
		public List<string> GetLevels()
		{
			List<string> list = new List<string>();

			foreach (string name in Levels.Keys)
				list.Add(name);

			return list;
		}


		/// <summary>
		/// Gets a level by its name
		/// </summary>
		/// <param name="name">Name of the level to find</param>
		/// <returns>Handle to the level or null</returns>
		public Level GetLevel(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			if (Levels.ContainsKey(name))
				return Levels[name];

			return null;
		}


		/// <summary>
		/// Adds a level to the world
		/// </summary>
		/// <param name="name"></param>
		/// <param name="level"></param>
		public void AddLevel(string name, Level level)
		{
			if (string.IsNullOrEmpty(name) || level == null)
				return;

			Levels[name] = level;
		}


		/// <summary>
		/// Removes a level 
		/// </summary>
		/// <param name="name"></param>
		public void RemoveLevel(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

			Levels.Remove(name);
		}

		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public bool IsDisposed
		{
			get;
			private set;
		}


		/// <summary>
		/// Name of the world
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "world";
			}
		}


		/// <summary>
		/// The level in action
		/// </summary>
		public Level CurrentLevel
		{
			get;
			private set;
		}


		/// <summary>
		/// Number of level in this world
		/// </summary>
		public int LevelCount
		{
			get
			{
				return Levels.Count;
			}
		}

		/// <summary>
		/// All levels in the world
		/// </summary>
		Dictionary<string, Level> Levels;

		#endregion
	}
}
