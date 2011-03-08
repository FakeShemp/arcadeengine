#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.ComponentModel;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Interface;

namespace DungeonEye
{

	/// <summary>
	/// Represent a dungeon which contains severals mazes
	/// </summary>
	public class Dungeon : IAsset, IDisposable
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public Dungeon()
		{
			Mazes = new Dictionary<string, Maze>();
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			foreach (Maze maze in Mazes.Values)
				maze.Dispose();
			Mazes.Clear();

			// Remove shared asset
			ResourceManager.RemoveSharedAsset<TileSet>("Doors");


			StartLocation = null;
			Note = "";

			IsDisposed = true;
		}


		/// <summary>
		/// Initialize the dungeon
		/// </summary>
		/// <returns>True if all is OK</returns>
		public bool Init()
		{
			Trace.WriteLine("[Dungeon] : Init()");

			// Loads maze display coordinates
			DisplayCoordinates.Load();

			StartLocation.Maze = StartLocation.Maze;


			// Generate shared asset
			ResourceManager.CreateSharedAsset<TileSet>("Doors", "Doors");


			foreach (Maze maze in Mazes.Values)
				maze.Init();

			return true;
		}



		/// <summary>
		/// Clears the dungeon
		/// </summary>
		public void Clear()
		{
			foreach (Maze maze in Mazes.Values)
				maze.Dispose();
			Mazes.Clear();

			StartLocation = new DungeonLocation(StartLocation);
		}



		/// <summary>
		/// Update all elements in the dungeon
		/// </summary>
		/// <param name="time">Elapsed game time</param>
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

			if (xml.Name != XmlTag)
			{
				Trace.WriteLine("Expecting \"" + XmlTag + "\" in node header, found \"" + xml.Name + "\" when loading Dungeon.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "maze":
					{
						string name = node.Attributes["name"].Value;
						Maze maze = new Maze(this);
						maze.Name = name;
						Mazes[name] = maze;
						maze.Load(node);
					}
					break;

					case "start":
					{
						StartLocation = new DungeonLocation(node);
					}
					break;


					case "note":
					{
						Note = node.InnerText;
					}
					break;
				}


			}

			return true;
		}



		/// <summary>
		/// Saves the dungeon
		/// </summary>
		/// <param name="writer">XmlWriter handle</param>
		/// <returns>True if saved</returns>
		public bool Save(XmlWriter writer)
		{

			if (writer == null)
				return false;

			writer.WriteStartElement("dungeon");
			writer.WriteAttributeString("name", Name);

	
			foreach (Maze maze in MazeList)
				maze.Save(writer);

			StartLocation.Save("start", writer);

			writer.WriteStartElement("note");
			writer.WriteString(Note);
			writer.WriteEndElement();

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
			if (maze == null)
				return;

			Mazes[maze.Name] = maze;
		}


		/// <summary>
		/// Removes a maze by its name
		/// </summary>
		/// <param name="name">Name of the maze</param>
		public void RemoveMaze(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

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
		/// Is asset disposed
		/// </summary>
		[Browsable(false)]
		public bool IsDisposed { get; private set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "dungeon";
			}
		}


		/// <summary>
		/// Comments about the dungeon
		/// </summary>
		public string Note
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


		///// <summary>
		///// Handle to the team
		///// </summary>
		//public Team Team
		//{
		//    get;
		//    set;
		//}
		
		
		#endregion


	}
}
