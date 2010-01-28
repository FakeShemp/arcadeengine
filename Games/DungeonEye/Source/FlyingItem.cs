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
using System.Text;
using System.Drawing;
using ArcEngine;
using System.Xml;

namespace DungeonEye
{

	/// <summary>
	/// All flying objects in the maze (item, fireball, acid cloud....)
	/// 
	/// http://eob.wikispaces.com/eob.thrownitem
	/// </summary>
	public class FlyingItem
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="block">Location</param>
		public FlyingItem(MazeBlock block)
		{
			//if (block == null)
			//    throw new ArgumentNullException("block");

		//	Block = block;
			if (block != null)
				Location = new DungeonLocation(block.Location);
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="owner">Entity who thrown the item</param>
		/// <param name="item">Item</param>
		/// <param name="location">Start location</param>
		/// <param name="speed">Time in ms taken to cross a block</param>
		/// <param name="distance">How many block the item have to fly before falling on the ground</param>
		public FlyingItem(Entity owner, Item item, DungeonLocation location, TimeSpan speed, int distance)
		{
			Owner = owner;
			Item = item;
			Location = new DungeonLocation(location);
			Speed = speed;
			Distance = distance;
		}



		/// <summary>
		/// Update the flying item
		/// </summary>
		/// <param name="time">Game time</param>
		/// <param name="maze">Maze where the flying item is</param>
		/// <returns>True if the blocked or false if nothing happened</rereturns>
		public bool Update(GameTime time, Maze maze)
		{
			// Item can't move any more
			if (Distance == 0)
				return true;
			
			LastUpdate += time.ElapsedGameTime;

			if (LastUpdate > Speed)
			{
				LastUpdate -= Speed;

				// Find the next block according to the direction
				Point dst = Point.Empty;
				switch (Location.Direction)
				{
					case CardinalPoint.North:
					dst = new Point(Location.Position.X, Location.Position.Y - 1);
					break;
					case CardinalPoint.East:
					dst = new Point(Location.Position.X + 1, Location.Position.Y);
					break;
					case CardinalPoint.South:
					dst = new Point(Location.Position.X, Location.Position.Y + 1);
					break;
					case CardinalPoint.West:
					dst = new Point(Location.Position.X - 1, Location.Position.Y);
					break;
				}


				// Blocked by a wall, fall before the block
				MazeBlock blockinfo =  maze.GetBlock(dst);
				if (blockinfo.IsBlocking)
				{
					Distance = 0;
				}


				// Block by an obstacle, but fall on the block
				int monstercount = maze.GetMonsterCount(dst);
				if ((blockinfo.Door != null && blockinfo.Door.State != DoorState.Opened) || monstercount > 0)
				{
					Distance = 0;
					Location.Position = dst;

					GroundPosition gp = Location.GroundPosition;
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.SouthEast)
							Location.GroundPosition = GroundPosition.SouthEast;
						else
							Location.GroundPosition = GroundPosition.SouthWest;
						break;
						case CardinalPoint.South:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.SouthEast)
							Location.GroundPosition = GroundPosition.NorthEast;
						else
							Location.GroundPosition = GroundPosition.NorthWest;
						break;
						case CardinalPoint.West:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.NorthWest)
							Location.GroundPosition = GroundPosition.NorthEast;
						else
							Location.GroundPosition = GroundPosition.SouthEast;
						break;
						case CardinalPoint.East:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.NorthWest)
							Location.GroundPosition = GroundPosition.NorthWest;
						else
							Location.GroundPosition = GroundPosition.SouthWest;
						break;
					}


					// Get monster and hit them
					if (monstercount > 0)
					{
						Monster[] monsters = maze.GetMonsters(Location.Position);
						foreach(Monster monster in monsters)
							if (monster != null)
							{
								Attack attack = new Attack(Owner, monster, Item);
							//	monster.Hit(attack);
							}
					}
					return true;
				}


				//// Hit a monster
				//else if (maze.GetMonsterCount(dst) > 0)
				//{
				//   Distance = 0;
				//   Location.Position = dst;
				//}



				// Drop the item at good ground position
				if (Distance == 0)
				{
					GroundPosition gp = Location.GroundPosition;
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.SouthEast)
							Location.GroundPosition = GroundPosition.NorthEast;
						else
							Location.GroundPosition = GroundPosition.NorthWest;
						break;
						case CardinalPoint.South:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.SouthEast)
							Location.GroundPosition = GroundPosition.SouthEast;
						else
							Location.GroundPosition = GroundPosition.SouthWest;
						break;
						case CardinalPoint.West:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.NorthWest)
							Location.GroundPosition = GroundPosition.NorthWest;
						else
							Location.GroundPosition = GroundPosition.SouthWest;
						break;
						case CardinalPoint.East:
						if (gp == GroundPosition.NorthEast || gp == GroundPosition.NorthWest)
							Location.GroundPosition = GroundPosition.NorthEast;
						else
							Location.GroundPosition = GroundPosition.SouthEast;
						break;
					}

					return true;
				}
				else
				{
					Distance--;
					Location.Position = dst;
				}
			}

			return false;
		}




		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("flyingitem");

			Location.Save("location", writer);

			writer.WriteStartElement("item");
			writer.WriteAttributeString("name", Item.Name);
			writer.WriteEndElement();

			writer.WriteStartElement("speed");
			writer.WriteAttributeString("value", Speed.TotalMilliseconds.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("distance");
			writer.WriteAttributeString("value", Distance.ToString());
			writer.WriteEndElement();

			writer.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;

				switch (node.Name.ToLower())
				{
					case "location":
					{
						Location.Load(node);
					}
					break;

					case "item":
					{
						Item item = ResourceManager.CreateAsset<Item>("Main");
						Item = ResourceManager.CreateAsset<Item>(node.Attributes["name"].Value);
					}
					break;

					case "distance":
					{
						Distance = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "speed":
					{
						Speed = TimeSpan.FromMilliseconds(int.Parse(node.Attributes["value"].Value));
					}
					break;
				}


			}

			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// Owner of the flying item
		/// </summary>
		public Entity Owner
		{
			get;
			private set;
		}

		/// <summary>
		/// Speed of the object in ms to cross a block
		/// </summary>
		public TimeSpan Speed
		{
			get;
			set;
		}


		/// <summary>
		/// How many blocks the item have to fly
		/// </summary>
		public int Distance;


		/// <summary>
		/// Location of the object
		/// </summary>
		public DungeonLocation Location
		{
			get;
			set;
		}

		/// <summary>
		/// Handle to the item
		/// </summary>
		public Item Item
		{
			get;
			set;
		}

		/// <summary>
		/// Last time the update occured
		/// </summary>
		TimeSpan LastUpdate;
		
		#endregion

	}
}
