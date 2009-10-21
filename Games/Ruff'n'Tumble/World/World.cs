using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;


namespace RuffnTumble
{
	/// <summary>
	/// 
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
		/// Init the world
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			return true;

		}


		/// <summary>
		/// Update the world
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			if (CurrentLevel != null)
				CurrentLevel.Update(time);
		}


		/// <summary>
		/// Draw the world
		/// </summary>
		public void Draw()
		{
			if (CurrentLevel != null)
				CurrentLevel.Draw();
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
			CurrentLevel = null;

			if (Levels.ContainsKey(name))
				CurrentLevel = Levels[name];

			if (CurrentLevel == null)
				return false;

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
		/// All levels in the world
		/// </summary>
		Dictionary<string, Level> Levels;

		#endregion
	}
}
