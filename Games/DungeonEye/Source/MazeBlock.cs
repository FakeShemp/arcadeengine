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
using System.ComponentModel;
using System.Drawing;
using ArcEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// Represents a block in a maze
	/// 
	/// 
	/// Events
	///  OnTeamEnter		Team enter in the block
	///  OnTeamStand     Team stand in the block
	///  OnTeamLeave     Team leave the block
	///  OnDropItem      An item is dropped on the ground
	///  OnCollectItem   An item is picked up off the ground
	///  OnClick         The user clicked on the block
	/// 
	/// 
	/// </summary>
	public class MazeBlock
	{


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="maze">Maze</param>
		public MazeBlock(Maze maze)
		{
			if (maze == null)
				throw new ArgumentNullException("maze");

			//Maze = maze;
			Location = new DungeonLocation(maze.Dungeon);
			Location.SetMaze(maze.Name);
			Type = BlockType.Wall;

			WallDecoration = new int[4];
			Alcoves = new bool[4];

			GroundItems = new List<Item>[4];
			for (int i = 0; i < 4; i++)
				GroundItems[i] = new List<Item>();

		}





		#region Events

		/// <summary>
		/// A hero interacted with a side of the block
		/// </summary>
		/// <param name="team">Team</param>
		/// <param name="location">Location of the mouse</param>
		/// <param name="side">Wall side</param>
		/// <returns>True if the event is processed</returns>
		public bool OnClick(Team team, Point location, CardinalPoint side)
		{

			// A door
			if (Door != null && team.ItemInHand == null)
			{
				Door.OnClick(team, location);
				return true;
			}

			// An Alcove 
			else if (HasAlcove(side) && MazeDisplayCoordinates.Alcove.Contains(location))
			{
				if (team.ItemInHand != null)
				{
					DropAlcoveItem(side, team.ItemInHand);
					team.SetItemInHand(null);
				}
				else
				{
					team.SetItemInHand(CollectAlcoveItem(side));
				}

				return true;
			}


			return false;
		}


		/// <summary>
		/// Team stand on the block
		/// </summary>
		/// <param name="team">Team</param>
		public void OnTeamStand(Team team)
		{
		}


		/// <summary>
		/// Action when the team enter the block
		/// </summary>
		/// <param name="team">Team</param>
		public void OnTeamEnter(Team team)
		{
			if (ForceField != null)
			{
				switch (ForceField.Type)
				{
					case ForceFieldType.Turning:
					{
						team.Location.Compass.Rotate(ForceField.Rotation);
					}
					break;

					case ForceFieldType.Moving:
					{
						team.Offset(ForceField.Move, 1);
					}
					break;
				}
			}
			else if (Pit != null)
			{
				if (team.Teleport(Pit.Target))
					team.Damage(Pit.Damage, SavingThrowType.Reflex, Pit.Difficulty);

			}
			else if (Teleporter != null)
			{
				team.Teleport(Teleporter.Target);

			}
			else if (Stair != null)
			{
				if (team.Teleport(Stair.Target))
					team.Direction = Stair.Target.Direction;

			}
			else if (FloorPlate != null)
			{
				FloorPlate.OnTouch(team, this);
			}
		}


		/// <summary>
		/// Action when the team leave the block
		/// </summary>
		/// <param name="team">Team</param>
		public void OnTeamLeave(Team team)
		{
			if (FloorPlate != null)
				FloorPlate.OnLeave(team, this);
		}


		/// <summary>
		/// Item is dropped on the block
		/// </summary>
		/// <param name="item">Handle to the item</param>
		public void OnDroppedItem(Item item)
		{
		}


		/// <summary>
		/// Item is collected from the block
		/// </summary>
		/// <param name="item">Handle to the item</param>
		public void OnCollectedItem(Item item)
		{
		}

		#endregion



		/// <summary>
		/// Returns the wall decoration for a wall
		/// </summary>
		/// <param name="compass"></param>
		/// <returns></returns>
		public WallDecoration GetWallDecoration(CardinalPoint direction)
		{
			return ResourceManager.CreateAsset<WallDecoration>(WallDecoration[(int)direction].ToString());
		}


		/// <summary>
		/// Remove all specials (Stair, Teleporter, Pit...) on this block
		/// </summary>
		public void RemoveSpecials()
		{
			Door = null;
			FloorPlate = null;
			ForceField = null;
			Pit = null;
			Stair = null;
			Teleporter = null;
		}


		/// <summary>
		/// Returns items on the ground at a specific corner
		/// </summary>
		/// <param name="position">Ground position</param>
		/// <returns>List of items</returns>
		public List<Item> GetItemsOnGround(GroundPosition position)
		{
			return GroundItems[(int)position];
		}


		/// <summary>
		/// Returns items on the ground at a specific corner
		/// </summary>
		/// <param name="from">Facing position</param>
		/// <param name="position">Ground position</param>
		/// <returns>List of items</returns>
		public List<Item> GetItemsOnGround(CardinalPoint from, GroundPosition position)
		{
			CardinalPoint[,] tab = new CardinalPoint[,]
			{
				{CardinalPoint.North, CardinalPoint.South, CardinalPoint.West, CardinalPoint.East},
				{CardinalPoint.South, CardinalPoint.North, CardinalPoint.East, CardinalPoint.West},
				{CardinalPoint.West, CardinalPoint.East, CardinalPoint.South, CardinalPoint.North},
				{CardinalPoint.East, CardinalPoint.West, CardinalPoint.North, CardinalPoint.South},
			};

			return GetItemsOnGround((GroundPosition)tab[(int)from, (int)position]);
		}


		#region Alcoves

		/// <summary>
		/// Gets if the wall have an alcove
		/// </summary>
		/// <param name="from">Facing direction</param>
		/// <param name="side">Wall side</param>
		/// <returns>True if an alcove is present<returns>
		public bool HasAlcove(CardinalPoint from, CardinalPoint side)
		{
			CardinalPoint[,] tab = new CardinalPoint[,]
			{
				{CardinalPoint.North, CardinalPoint.South, CardinalPoint.West, CardinalPoint.East},
				{CardinalPoint.South, CardinalPoint.North, CardinalPoint.East, CardinalPoint.West},
				{CardinalPoint.West, CardinalPoint.East, CardinalPoint.South, CardinalPoint.North},
				{CardinalPoint.East, CardinalPoint.West, CardinalPoint.North, CardinalPoint.South},
			};


			return Alcoves[(int)tab[(int)from, (int)side]];
		}

		/// <summary>
		/// Gets an alcove
		/// </summary>
		/// <param name="side">Side of the alcove</param>
		/// <returns>True if an alcove is present<returns>
		public bool HasAlcove(CardinalPoint side)
		{
			return Alcoves[(int)side];
		}


		/// <summary>
		/// Creates or removes an alcove
		/// </summary>
		/// <param name="side">Side of the alcove</param>
		/// <param name="create">Create or remove the alcove</param>
		public void SetAlcove(CardinalPoint side, bool create)
		{
			Alcoves[(int)side] = create;
		}


		/// <summary>
		/// Gets items from an alcove 
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <returns>List of items</returns>
		public List<Item> GetAlcoveItems(CardinalPoint side)
		{
			return GroundItems[(int)side];
		}


		/// <summary>
		/// Gets items from an alcove 
		/// </summary>
		/// <param name="from">View point</param>
		/// <param name="side">Wall side</param>
		/// <returns>List of items</returns>
		public List<Item> GetAlcoveItems(CardinalPoint from, CardinalPoint side)
		{
			CardinalPoint[,] tab = new CardinalPoint[,]
			{
				{CardinalPoint.North, CardinalPoint.South, CardinalPoint.West, CardinalPoint.East},
				{CardinalPoint.South, CardinalPoint.North, CardinalPoint.East, CardinalPoint.West},
				{CardinalPoint.West, CardinalPoint.East, CardinalPoint.South, CardinalPoint.North},
				{CardinalPoint.East, CardinalPoint.West, CardinalPoint.North, CardinalPoint.South},
			};

			return GetAlcoveItems(tab[(int)from, (int)side]);
		}


		/// <summary>
		/// Collects an item in an alcove
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <returns>Item handle or null</returns>
		public Item CollectAlcoveItem(CardinalPoint side)
		{
			return CollectItem((GroundPosition)side);
		}


		/// <summary>
		/// Drops an item in an alcove
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <returns>True if the item can go in the alcove, or false</returns>
		public bool DropAlcoveItem(CardinalPoint side, Item item)
		{
			if (item == null || !HasAlcove(side))
				return false;

			GroundItems[(int)side].Add(item);

			return true;
		}


		#endregion


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");


			writer.WriteStartElement("block");

			// Location
			Location.Save("location", writer);
			//writer.WriteStartElement("location");
			//writer.WriteAttributeString("x", Location.X.ToString());
			//writer.WriteAttributeString("y", Location.Y.ToString());
			//writer.WriteEndElement();


			// Type of wall
			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			if (Door != null)
				Door.Save(writer);

			if (FloorPlate != null)
				FloorPlate.Save(writer);

			if (Pit != null)
				Pit.Save(writer);

			if (Teleporter != null)
				Teleporter.Save(writer);

			if (ForceField != null)
				ForceField.Save(writer);

			if (Stair != null)
				Stair.Save(writer);

			// Wall decoration
			for (int i = 0; i < 4; i++)
			{
				if (WallDecoration[i] != 0)
				{
					writer.WriteStartElement("decoration");
					writer.WriteAttributeString("position", i.ToString());
					writer.WriteAttributeString("id", WallDecoration[i].ToString());
					writer.WriteEndElement();
				}
			}

			// Alcoves
			foreach(CardinalPoint side in Enum.GetValues(typeof(CardinalPoint)))
			{
				if (HasAlcove(side))
				{
					writer.WriteStartElement("alcove");
					writer.WriteAttributeString("side", side.ToString());
					writer.WriteEndElement();
				}
			}


			// Items
			for (int i = 0; i < 4; i++)
			{
				if (GroundItems[i].Count > 0)
				{

					foreach (Item item in GroundItems[i])
					{
						writer.WriteStartElement("item");
						writer.WriteAttributeString("location", ((GroundPosition)i).ToString());
						writer.WriteAttributeString("name", item.Name);
						writer.WriteEndElement();
					}

				}
			}


			writer.WriteEndElement();
		}



		/// <summary>
		/// Loads block defintion
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			//ItemSet itemset = ResourceManager.CreateSharedAsset<ItemSet>("Items");
			//if (itemset == null)
			//{
			//    Trace.WriteLine("MazeBlock::Load() : Failed to open ItemSet.");
			//}
			
			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;



				switch (node.Name.ToLower())
				{
					// Items on ground
					case "item":
					{
						GroundPosition loc = (GroundPosition)Enum.Parse(typeof(GroundPosition), node.Attributes["location"].Value);
						Item item = ResourceManager.CreateAsset<Item>(node.Attributes["name"].Value);
						if (item != null)
							GroundItems[(int)loc].Add(item);

					}
					break;


					case "location":
					{
						Location.Load(node);
						//= new Point(Int32.Parse(node.Attributes["x"].Value), Int32.Parse(node.Attributes["y"].Value));
					}
					break;


					case "type":
					{
						Type = (BlockType)Enum.Parse(typeof(BlockType), node.Attributes["value"].Value, true);
					}
					break;

					case "door":
					{
						Door = new Door();
						Door.Load(node);
					}
					break;

					case "teleporter":
					{
						Teleporter = new Teleporter(this);
						Teleporter.Load(node);
					}
					break;

					case "floorplate":
					{
						FloorPlate = new FloorPlate();
						FloorPlate.Load(node);
					}
					break;

					case "pit":
					{
						Pit = new Pit(this);
						Pit.Load(node);
					}
					break;

					case "forcefield":
					{
						ForceField = new ForceField();
						ForceField.Load(node);
					}
					break;

					case "stair":
					{
						Stair = new Stair(this);
						Stair.Load(node);
					}
					break;


					case "alcove":
					{
						CardinalPoint side = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["side"].Value);
						SetAlcove(side, true);
					}
					break;
				}
			}



			return true;
		}

		#endregion


		#region Ground items

		/// <summary>
		/// Collects an item on the ground
		/// </summary>
		/// <param name="position">Position in the block</param>
		/// <returns>Item</returns>
		public Item CollectItem(GroundPosition position)
		{
			// No item in the middle of a block
			if (position == GroundPosition.Middle)
				throw new ArgumentOutOfRangeException("position", "No items in the middle of a maze block !");

			int count = GroundItems[(int)position].Count;
			if (count == 0)
				return null;

			Item item = GroundItems[(int)position][count - 1];
			GroundItems[(int)position].RemoveAt(count - 1);

			// Call the script
			OnCollectedItem(item);

			return item;
		}


		/// <summary>
		/// Drops an item on the ground
		/// </summary>
		/// <param name="position">Position in the block</param>
		/// <param name="item">Item to drop</param>
		/// <returns>True if the item can be dropped</returns>
		public bool DropItem(GroundPosition position, Item item)
		{
			// No item in the middle of a block
			if (position == GroundPosition.Middle)
				throw new ArgumentOutOfRangeException("position", "No items in the middle of a maze block !");

			// Can drop item in wall
			if (IsWall || Stair != null)
				return false;

			// Add the item to the ground
			GroundItems[(int)position].Add(item);

			// Call the script
			OnCollectedItem(item);

			return true;
		}


		/// <summary>
		/// Returns all objects on the ground depending the view point
		/// </summary>
		/// <param name="location">Facing direction</param>
		/// <returns>Returns an array of items on the ground
		/// Position 0 : Up left
		/// Position 1 : Up Right
		/// Position 2 : Bottom left
		/// Position 3 : Bottom right</returns>
		public List<Item>[] GetGroundItems(CardinalPoint location)
		{
			// List of items
			List<Item>[] items = new List<Item>[4];


			switch (location)
			{
				case CardinalPoint.North:
				{
					items[0] = GroundItems[0];
					items[1] = GroundItems[1];
					items[2] = GroundItems[2];
					items[3] = GroundItems[3];
				}
				break;
				case CardinalPoint.East:
				{
					items[0] = GroundItems[1];
					items[1] = GroundItems[3];
					items[2] = GroundItems[0];
					items[3] = GroundItems[2];
				}
				break;
				case CardinalPoint.South:
				{
					items[0] = GroundItems[3];
					items[1] = GroundItems[2];
					items[2] = GroundItems[1];
					items[3] = GroundItems[0];
				}
				break;
				case CardinalPoint.West:
				{
					items[0] = GroundItems[2];
					items[1] = GroundItems[0];
					items[2] = GroundItems[3];
					items[3] = GroundItems[1];
				}
				break;
			}


			return items;
		}


		#endregion


		#region Properties


		/// <summary>
		/// Gets if the wall have alcoves
		/// </summary>
		public bool HasAlcoves
		{
			get
			{
				return (Alcoves[0] || Alcoves[1] || Alcoves[2] || Alcoves[3]);
			}
		}


		/// <summary>
		/// Returns true if the block is a wall or a trick wall
		/// </summary>
		public bool IsWall
		{
			get
			{
				return Type != BlockType.Ground;
			}
		}


		/// <summary>
		/// Does the block blocks the team
		/// </summary>
		public bool IsBlocking
		{
			get
			{
				if (Type == BlockType.Wall)
					return true;

				if (Door != null && (Door.State != DoorState.Opened))
					return true;

				if (Stair != null)
					return true;

				return false;
			}
		}


		/// <summary>
		/// Is an illusion block
		/// </summary>
		public bool IsIllusion
		{
			get
			{
				return Type == BlockType.Illusion;
			}

		}


		/// <summary>
		/// Decoration on each wall block
		/// </summary>
		public int[] WallDecoration;


		/// <summary>
		/// Items on the ground
		/// </summary>
		[Browsable(false)]
		public List<Item>[] GroundItems
		{
			get;
			set;
		}


		/// <summary>
		/// Number of item on ground
		/// </summary>
		public int GroundItemCount
		{
			get
			{
				int count = 0;
				foreach (List<Item> list in GroundItems)
					count += list.Count;

				return count;

			}
		}


		/// <summary>
		/// Type of the block
		/// </summary>
		public BlockType Type
		{
			get;
			set;
		}


		/// <summary>
		/// Location of the block in the maze
		/// </summary>
		public DungeonLocation Location
		{
			get;
			private set;
		}



		/// <summary>
		/// Door
		/// </summary>
		public Door Door
		{
			get;
			set;
		}


		/// <summary>
		/// Force field
		/// </summary>
		public ForceField ForceField
		{
			get;
			set;
		}


		/// <summary>
		/// Pit
		/// </summary>
		public Pit Pit
		{
			get;
			set;
		}


		/// <summary>
		/// Teleporter
		/// </summary>
		public Teleporter Teleporter
		{
			get;
			set;
		}



		/// <summary>
		/// Floor Plate
		/// </summary>
		public FloorPlate FloorPlate
		{
			get;
			set;
		}


		/// <summary>
		/// Stair
		/// </summary>
		public Stair Stair
		{
			get;
			set;
		}


		/// <summary>
		/// Alcoves
		/// </summary>
		bool[] Alcoves;

		#endregion
	}


	/// <summary>
	/// Type of block in the maze
	/// </summary>
	public enum BlockType
	{
		/// <summary>
		/// Ground block
		/// </summary>
		Ground,

		/// <summary>
		/// Wall block
		/// </summary>
		Wall,
		
		/// <summary>
		/// Illusionary wall
		/// </summary>
		Illusion,

	}


	/// <summary>
	/// Sub position in a block in the maze
	/// 
	/// .-------.
	/// | A | B |
	/// | +---+ |
	/// |-| E |-|
	/// | +---+ |
	/// | C | D |
	/// '-------'
	/// </summary>
	public enum GroundPosition
	{
		/// <summary>
		/// North west
		/// </summary>
		NorthWest = 0,

		/// <summary>
		/// North east
		/// </summary>
		NorthEast = 1,

		/// <summary>
		/// South west
		/// </summary>
		SouthWest = 2,

		/// <summary>
		/// South east
		/// </summary>
		SouthEast = 3,

		/// <summary>
		/// Middle position (not for items !)
		/// </summary>
		Middle = 4
	}

}
