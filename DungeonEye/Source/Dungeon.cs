using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;

namespace DungeonEye
{

	/// <summary>
	/// Represent a dungeon which contains severals mazes
	/// </summary>
	public class Dungeon : IAsset
	{
		public Dungeon()
		{
			Mazes = new Dictionary<string, Maze>();
			StartLocation = new DungeonLocation();
		}



		/// <summary>
		/// Initialize the dungeon
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			foreach (Maze maze in Mazes.Values)
				maze.Init();

			return true;
		}



		/// <summary>
		/// Clears the dungeon
		/// </summary>
		public void Clear()
		{
			//StartDirection = CardinalPoint.North;
			StartLocation = new DungeonLocation();
			//StartMazeName = string.Empty;
			Mazes.Clear();
		}



		/// <summary>
		/// Update all elements in the dungeon
		/// </summary>
		public void Update(GameTime time)
		{
			foreach (Maze maze in MazeList)
			{
				maze.Update(time);
			}
		}



		#region IO

		/// <summary>
		/// Loads a <see cref="Dungeon"/>
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name != "dungeon")
			{
				Trace.WriteLine("Expecting \"dungeon\" in node header, found \"" + xml.Name + "\" when loading Dungeon.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					// Level
					case "maze":
					{
						string name = node.Attributes["name"].Value;
						Maze maze = new Maze(this);
						maze.Name = name;
						maze.Load(node);
						Mazes[name] = maze;
					}
					break;

					// Start point
					case "start":
					{
						StartLocation.Maze = node.Attributes["name"].Value;
						StartLocation.Position = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						StartLocation.Direction = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["direction"].Value, true);
					}
					break;

				}


			}

			return true;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{

			if (writer == null)
				return false;

			writer.WriteStartElement("dungeon");
			writer.WriteAttributeString("name", Name);

			writer.WriteStartElement("start");
			writer.WriteAttributeString("name", StartLocation.Maze);
			writer.WriteAttributeString("x", StartLocation.Position.X.ToString());
			writer.WriteAttributeString("y", StartLocation.Position.Y.ToString());
			writer.WriteAttributeString("direction", StartLocation.Direction.ToString());
			writer.WriteEndElement();

	
			foreach (Maze maze in MazeList)
				maze.Save(writer);

			writer.WriteEndElement();

			return true;

		}



		#endregion



		#region Mazes


		/// <summary>
		/// Returns a maze by its name
		/// </summary>
		/// <param name="name">Maze name</param>
		/// <returns>Maze or null</returns>
		public Maze GetMaze(string name)
		{
			if (string.IsNullOrEmpty(name) || !Mazes.ContainsKey(name))
				return null;

			return Mazes[name];
		}

/*
		/// <summary>
		/// Adds a maze to the dungeon
		/// </summary>
		/// <param name="name"></param>
		/// <param name="node"></param>
		public void SetMaze(Maze maze)
		{
			if (maze == null)
				return;

			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				maze.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

		//	Mazes[maze.Name] = doc.DocumentElement;

		}
*/

		/// <summary>
		/// Adss a maze to the dungeon
		/// </summary>
		/// <param name="maze">Maze to add</param>
		public void AddMaze(Maze maze)
		{
			Mazes[maze.Name] = maze;
		}


		/// <summary>
		/// Removes a maze by its name
		/// </summary>
		/// <param name="name">Name of the maze</param>
		public void RemoveMaze(string name)
		{
			Mazes.Remove(name);
		}



		#endregion



		#region Properties

		/// <summary>
		/// Name of the dungeon
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Gets a list of all levels
		/// </summary>
		[Browsable(false)]
		public List<Maze> MazeList
		{
			get
			{
				List<Maze> list = new List<Maze>();

				foreach (Maze level in Mazes.Values)
					list.Add(level);

				return list;
			}
		}


		/// <summary>
		/// Available mazes
		/// </summary>
		 Dictionary<string, Maze> Mazes;


		/// <summary>
		/// Starting location in the dungeon
		/// </summary>
		public DungeonLocation StartLocation
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the Tileset
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[DescriptionAttribute("TileSet name to use for items")]
		public string ItemSetName
		{
			get;
			set;
		}

		#endregion


	}







}
