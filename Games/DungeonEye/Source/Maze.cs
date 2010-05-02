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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using DungeonEye.MonsterStates;

namespace DungeonEye
{

	/// <summary>
	/// 
	/// 
	/// 
	/// 
	/// http://dmweb.free.fr/?q=node/217
	/// </summary>
	public class Maze : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Maze(Dungeon dungeon)
		{
			Name = "No name";
			Dungeon = dungeon;

			Blocks = new List<List<MazeBlock>>();
			Monsters = new List<Monster>();
			Doors = new List<Door>();
			FlyingItems = new List<FlyingItem>();
			Zones = new List<MazeZone>();


		}



		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Doors != null)
				foreach (Door door in Doors)
					door.Dispose();
			Doors = null;

			if (Monsters != null)
				foreach (Monster monster in Monsters)
					monster.Dispose();
			Monsters = null;

			if (ItemsTileset != null)
				ItemsTileset.Dispose();
			ItemsTileset = null;

			if (OverlayTileset != null)
				OverlayTileset.Dispose();
			OverlayTileset = null;

			if (WallTileset != null)
				WallTileset.Dispose();
			WallTileset = null;

			Blocks = null;
			Description = null;
			Dungeon = null;
			FlyingItems = null;
			ItemsTilesetName = null;
			OverlayTilesetName = null;
			WallTilesetName = null;
			size = Size.Empty;
			Zones = null;
			Name = "";

		}


		/// <summary>
		/// Initialize the maze
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			// Loads maze display coordinates
			MazeDisplayCoordinates.Load();


			WallTileset = ResourceManager.CreateSharedAsset<TileSet>(WallTilesetName, WallTilesetName);
			if (WallTileset == null)
			{
				Trace.WriteLine("Failed to load wall tileset for the maze \"" + Name + "\".");
				return false;
			}
			WallTileset.Scale = new SizeF(2.0f, 2.0f);

			OverlayTileset = ResourceManager.CreateSharedAsset<TileSet>(OverlayTilesetName, OverlayTilesetName);
			if (OverlayTileset == null)
			{
				Trace.WriteLine("Failed to load overlay tileset for the maze \"" + Name + "\".");
				return false;
			}
			OverlayTileset.Scale = new SizeF(2.0f, 2.0f);

			ItemsTileset = ResourceManager.CreateSharedAsset<TileSet>(ItemsTilesetName, ItemsTilesetName);
			if (ItemsTileset == null)
			{
				Trace.WriteLine("Failed to load items tileset for the maze \"" + Name + "\".");
				return false;
			}
			ItemsTileset.Scale = new SizeF(2.0f, 2.0f);

			foreach (Monster monster in Monsters)
				monster.Init();

			foreach (Door door in Doors)
				door.Init();

			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					MazeBlock block = GetBlock(new Point(x, y));

					#region Pits
					if (block.Pit != null && block.Pit.Target != null)
					{
						Maze maze = Dungeon.GetMaze(block.Pit.Target.MazeName);
						if (maze == null)
							continue;

						MazeBlock blk = maze.GetBlock(block.Pit.Target.Position);
						if (blk == null)
							continue;

						blk.IsPitTarget = true;
					}
					#endregion
				}
		
			return true;
		}


		/// <summary>
		/// Update all elements in the maze
		/// </summary>
		public void Update(GameTime time)
		{
			// Update monsters
			foreach (Monster monster in Monsters)
			{
				monster.Update(time);
			}


			// Remove dead monsters
			Monsters.RemoveAll(
				delegate(Monster monster)
				{
					// Monster alive
					if (!monster.IsDead)
						return false;

					// Drop the content of his pocket
					if (monster.ItemsInPocket.Count > 0)
					{
						MazeBlock block = GetBlock(monster.Location.Position);

						//ItemSet itemset = ResourceManager.CreateSharedAsset<ItemSet>("Main");
						foreach (string name in monster.ItemsInPocket)
							block.DropItem(monster.Location.GroundPosition, ResourceManager.CreateAsset<Item>(name));
					}

					return true;
				});



			#region Doors
			foreach (Door door in Doors)
				door.Update(time);
			#endregion


			#region Flying items

			// Update flying items
			foreach (FlyingItem item in FlyingItems)
				item.Update(time, this);

			// Remove all blocked flying items
			FlyingItems.RemoveAll(
				delegate(FlyingItem fi)
				{
					// Item can fly
					if (fi.Distance > 0)
						return false;


					// Make the item falling on the ground
					GroundPosition pos = fi.Location.GroundPosition;
/*
					switch (fi.Location.Direction)
					{
						case CardinalPoint.North:
						if (fi.Location.GroundPosition == GroundPosition.SouthWest)
							pos = GroundPosition.NorthWest;
						if (fi.Location.GroundPosition == GroundPosition.SouthEast)
							pos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.East:
						if (fi.Location.GroundPosition == GroundPosition.NorthWest)
							pos = GroundPosition.NorthEast;
						if (fi.Location.GroundPosition == GroundPosition.SouthWest)
							pos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.South:
						if (fi.Location.GroundPosition == GroundPosition.NorthEast)
							pos = GroundPosition.SouthEast;
						if (fi.Location.GroundPosition == GroundPosition.NorthWest)
							pos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.West:
						if (fi.Location.GroundPosition == GroundPosition.SouthEast)
							pos = GroundPosition.SouthWest;
						if (fi.Location.GroundPosition == GroundPosition.NorthEast)
							pos = GroundPosition.NorthWest;
						break;
					}
*/

					GetBlock(fi.Location.Position).DropItem(pos, fi.Item);


					return true;
				});
			#endregion
		}


		#region Monsters


		/// <summary>
		/// Adds a monster in the maze
		/// </summary>
		/// <param name="monster">Monster handle</param>
		/// <param name="location">Location in the maze</param>
		/// <returns>True if the monster can spawn, else false if monster is still there</returns>
		public bool SetMonster(Monster monster, DungeonLocation location)
		{
			if (location == null)
				return false;

			// Check if a monster is present
			if (GetMonsters(location.Position)[(int)location.GroundPosition] != null)
				return false;

			Monsters.Add(monster);

			
			return true;
		}


		/// <summary>
		/// Returns the number of monster in the block
		/// </summary>
		/// <param name="location">Block position</param>
		/// <returns>Number of monster in the block</returns>
		public int GetMonsterCount(Point location)
		{
			Monster[] monsters = GetMonsters(location);

			int count = 0;
			if (monsters[0] != null) count++;
			if (monsters[1] != null) count++;
			if (monsters[2] != null) count++;
			if (monsters[3] != null) count++;

			return count;
		}



		/// <summary>
		/// Returns monster at a given location
		/// </summary>
		/// <param name="location">Location to check</param>
		/// <param name="view">View point</param>
		/// <returns>An array containing monsters</returns>
		public Monster[] GetMonsters(Point location)//, CardinalPoint view)
		{
			Monster[] list = new Monster[4];

			// Out of the maze
			if (!Rectangle.Contains(location))
				return list;


			foreach (Monster monster in Monsters)
				if (monster.Location.Position == location)
					list[(int)monster.Location.GroundPosition] = monster;

			return list;
		}



		/// <summary>
		/// Returns the monster at a given location
		/// </summary>
		/// <param name="location">Location in the maze</param>
		/// <param name="position">Ground location</param>
		/// <returns>Monster handle or null</returns>
		public Monster GetMonster(DungeonLocation location, GroundPosition position)
		{
			// Out of the maze
			if (!Rectangle.Contains(location.Position))
				return null;

			// All monsters in the block
			Monster[] monsters = GetMonsters(location.Position);

			// Count monsters
			int count = 0;
			for (int i = 0; i < 4; i++)
				if (monsters[i] != null)
					count++;

			if (count == 0)
				return null;

			if (count == 1)
			{
				for (int i = 0; i < 4; i++)
					if (monsters[i] != null)
						return monsters[i];
			}

			return monsters[(int)position];
		}


		#endregion


		#region  Helper

		/// <summary>
		/// Checks if a maze location is valid
		/// </summary>
		/// <param name="pos">Point in the maze</param>
		/// <returns>True if the point is in the maze, false if the point is outside the maze</returns>
		public bool Contains(Point pos)
		{
			Rectangle rect = new Rectangle(Point.Empty, Size);

			return rect.Contains(pos);
		}


		


		/// <summary>
		/// Gets if a door is North-South aligned
		/// </summary>
		/// <param name="location">Door location in the maze</param>
		/// <returns>True if the door is pointing north or south</returns>
		public bool IsDoorNorthSouth(DungeonLocation location)
		{
			if (location == null)
				throw new ArgumentNullException("location");

			Point left = new Point(location.Position.X - 1, location.Position.Y);	
			if (!Contains(left))
				return false;

			MazeBlock block = GetBlock(left);
			return (block.IsWall);
		}

		/// <summary>
		/// Returns informations about a block in the maze
		/// </summary>
		/// <param name="location">Location of the block</param>
		/// <returns>Block handle</returns>
		public MazeBlock GetBlock(Point location)
		{
			if (!Rectangle.Contains(location))
				return new MazeBlock(this);

			return Blocks[location.Y][location.X];

		}


		/// <summary>
		/// Checks if a point is visible from another point
		/// ie: is a monster visible from the current point of view of the team ?
		/// </summary>
		/// <param name="from">The point of view</param>
		/// <param name="to">The destination point to check</param>
		/// <param name="dir">The direction to look</param>
		/// <param name="distance">See distance</param>
		/// <returns>true if visible, false if not visible</returns>
		public bool IsVisible(Point from, Point to, Compass dir, int distance)
		{

			return false;
		}

		#endregion


		#region Flying items

		/// <summary>
		/// Returns all objects flying depending the view point
		/// </summary>
		/// <param name="direction">Looking direction</param>
		/// <returns>Returns an array of flying items
		/// Position 0 : North West
		/// Position 1 : North East
		/// Position 2 : South West
		/// Position 3 : South East</returns>
		public List<FlyingItem>[] GetFlyingItems(DungeonLocation location, CardinalPoint direction)
		{
			List<FlyingItem>[] tmp = new List<FlyingItem>[5];
			tmp[0] = new List<FlyingItem>();
			tmp[1] = new List<FlyingItem>();
			tmp[2] = new List<FlyingItem>();
			tmp[3] = new List<FlyingItem>();
			tmp[4] = new List<FlyingItem>();

			foreach (FlyingItem item in FlyingItems)
			{
				if (item.Location.Position == location.Position)
					tmp[(int)item.Location.GroundPosition].Add(item);
			}

	
			List<FlyingItem>[] items = new List<FlyingItem>[5];
			switch (direction)
			{
				case CardinalPoint.North:
				{
					items[0] = tmp[0];
					items[1] = tmp[1];
					items[2] = tmp[2];
					items[3] = tmp[3];
				}
				break;
				case CardinalPoint.East:
				{
					items[0] = tmp[1];
					items[1] = tmp[3];
					items[2] = tmp[0];
					items[3] = tmp[2];
				}
				break;
				case CardinalPoint.South:
				{
					items[0] = tmp[3];
					items[1] = tmp[2];
					items[2] = tmp[1];
					items[3] = tmp[0];
				}
				break;
				case CardinalPoint.West:
				{
					items[0] = tmp[2];
					items[1] = tmp[0];
					items[2] = tmp[3];
					items[3] = tmp[1];
				}
				break;
			}

			items[4] = tmp[4];

			return items;
		}


		#endregion


		#region Draws

		/// <summary>
		/// Draw the maze
		/// </summary>
		/// <param name="location">Location to display from</param>
		/// <see cref="http://eob.wikispaces.com/eob.vmp"/>
		public void Draw(DungeonLocation location)
		{


			if (WallTileset == null)
				return;

			//
			// 
			//
			ViewField pov = new ViewField(this, location);


			// Backdrop
			// The background is assumed to be x-flipped when party.x & party.y & party.direction = 1.
			// I.e. all kind of moves and rotations from the current position will result in the background being x-flipped.
			bool flipbackdrop = ((location.Position.X + location.Position.Y + (int)location.Direction) & 1) == 0;

			Display.Color = Color.White;
			WallTileset.Draw(0, Point.Empty, flipbackdrop, false);


			// alternate the wall
			int swap = (location.Position.Y % 2) * 9;


			// maze block draw order
			// A G B F C E D
			// H L I K J
			// M O N
			// P Team Q

			#region row -3
			DrawBlock(pov, ViewFieldPosition.A, location.Direction);
			DrawBlock(pov, ViewFieldPosition.G, location.Direction);
			DrawBlock(pov, ViewFieldPosition.B, location.Direction);
			DrawBlock(pov, ViewFieldPosition.F, location.Direction);
			DrawBlock(pov, ViewFieldPosition.C, location.Direction);
			DrawBlock(pov, ViewFieldPosition.E, location.Direction);
			DrawBlock(pov, ViewFieldPosition.D, location.Direction);
			#endregion

			#region row -2
			DrawBlock(pov, ViewFieldPosition.H, location.Direction);
			DrawBlock(pov, ViewFieldPosition.L, location.Direction);
			DrawBlock(pov, ViewFieldPosition.I, location.Direction);
			DrawBlock(pov, ViewFieldPosition.K, location.Direction);
			DrawBlock(pov, ViewFieldPosition.J, location.Direction);
			#endregion

			#region row -1
			DrawBlock(pov, ViewFieldPosition.M, location.Direction);
			DrawBlock(pov, ViewFieldPosition.O, location.Direction);
			DrawBlock(pov, ViewFieldPosition.N, location.Direction);
			#endregion

			#region row 0
			DrawBlock(pov, ViewFieldPosition.P, location.Direction);
			DrawBlock(pov, ViewFieldPosition.Team, location.Direction);
			DrawBlock(pov, ViewFieldPosition.Q, location.Direction);
			#endregion


		}



		/// <summary>
		/// Draws a block
		/// </summary>
		/// <param name="field">View field</param>
		/// <param name="position">Position in the view filed</param>
		/// <param name="view">Looking direction of the team</param>
		void DrawBlock(ViewField field, ViewFieldPosition position, CardinalPoint view)
		{
			if (field == null)
				return;

			MazeBlock block = field.Blocks[(int)position];
			Point point;
			TileDrawing td = null;

			#region Drawing offset
			int offset = 1;
			switch (position)
			{
				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
					offset = 3;
				break;
				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
					offset = 2;
				break;
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
					offset = 1;
				break;
				case ViewFieldPosition.P:
				case ViewFieldPosition.Team:
				case ViewFieldPosition.Q:
					offset = 0;
				break;
			}
			#endregion


			#region ceiling pit
			if (block.IsPitTarget)
			{
				td = MazeDisplayCoordinates.GetCeilingPit(position);
				if (td != null)
					OverlayTileset.Draw(td.ID, td.Location, td.SwapX, td.SwapY);
			}

			#endregion

			#region Items on ground
			List<Item>[] list = block.GetGroundItems(view);
			if (!block.IsWall)
			{
				for (int i = 0; i < 2; i++)
				{
					if (list[i].Count == 0)
						continue;

					foreach (Item item in list[i])
					{
						point = MazeDisplayCoordinates.GetGroundItem(position, (GroundPosition)i);
						if (!point.IsEmpty)
							ItemsTileset.Draw(item.GroundTileID + offset, point);
					}
				}
			}
			#endregion

			#region Pit
			if (block.Pit != null)
			{
				td = MazeDisplayCoordinates.GetPit(position);
				if (td != null && !block.Pit.IsHidden)
					OverlayTileset.Draw(td.ID, td.Location, td.SwapX, td.SwapY);
			}
			#endregion

			#region Stair
			else if (block.Stair != null)
			{
				// Upstair or downstair ?
				int delta = block.Stair.Type == StairType.Up ? 0 : 13;
				foreach (TileDrawing tmp in MazeDisplayCoordinates.GetStairs(position))
					WallTileset.Draw(tmp.ID + delta, tmp.Location, tmp.SwapX, tmp.SwapY);
			}
			#endregion

			#region Door
			else if (block.Door != null)
			{
				// Under the door
				if (field.GetBlock(ViewFieldPosition.N).IsWall && position == ViewFieldPosition.Team)
				{
					td = MazeDisplayCoordinates.GetDoor(ViewFieldPosition.Team);
					if (td != null)
						OverlayTileset.Draw(td.ID, td.Location, td.SwapX, td.SwapY);
				}
				else if (((field.Maze.IsDoorNorthSouth(block.Location) && (view == CardinalPoint.North || view == CardinalPoint.South)) ||
					(!field.Maze.IsDoorNorthSouth(block.Location) && (view == CardinalPoint.East || view == CardinalPoint.West))) &&
					position != ViewFieldPosition.Team)
				{
					td = MazeDisplayCoordinates.GetDoor(position);
					if (td != null)
					{
						WallTileset.Draw(td.ID, td.Location, td.SwapX, td.SwapY);
						block.Door.Draw(td.Location, position, view);
					}
				}
			}
			#endregion

			#region Floor plate 
			else if (block.FloorPlate != null && !block.FloorPlate.Invisible)
			{
				td = MazeDisplayCoordinates.GetFloorPlate(position);
				if (td != null)
					OverlayTileset.Draw(td.ID, td.Location, td.SwapX, td.SwapY);
			}
			#endregion

			#region Walls
			else if (block.IsWall)
			{
				// Walls
				foreach (TileDrawing tmp in MazeDisplayCoordinates.GetWalls(position))
					WallTileset.Draw(tmp.ID, tmp.Location, tmp.SwapX, tmp.SwapY);


				// Alcoves
				if (block.HasAlcoves)
				{
					// Draw alcoves
					foreach (CardinalPoint side in Enum.GetValues(typeof(CardinalPoint)))
					{
						td = MazeDisplayCoordinates.GetDecoration(position, side);
						if (td != null && block.HasAlcove(view, side))
							OverlayTileset.Draw(td.ID, td.Location, td.SwapX, td.SwapY);
					}

					// Draw items in the alcove in front of the team
					td = MazeDisplayCoordinates.GetDecoration(position, CardinalPoint.South);
					if (td != null)
					{
						foreach (Item item in block.GetAlcoveItems(view, CardinalPoint.South))
							ItemsTileset.Draw(item.GroundTileID + offset, td.Location);
					}
				}

			}
			#endregion

			#region Monsters
			//if (GetMonsterCount(block.Location.Position) != 0)
			{
				foreach (Monster monster in field.GetMonsters(position))
				{
					if (monster != null)
						monster.Draw(view, position);
				}
			}
			#endregion


			#region Items on ground
			if (!block.IsWall)
			{
				for (int i = 2; i < 4; i++)
				{
					if (list[i].Count == 0)
						continue;

					foreach (Item item in list[i])
					{
						point = MazeDisplayCoordinates.GetGroundItem(position, (GroundPosition)i);
						if (!point.IsEmpty)
							ItemsTileset.Draw(item.GroundTileID + offset, point);
					}
				}
			}
			#endregion

			#region Flying items
			List<FlyingItem>[] flyings = GetFlyingItems(block.Location, view);
			foreach(GroundPosition pos in Enum.GetValues(typeof(GroundPosition)))
			{
				if (MazeDisplayCoordinates.GetFlyingItem(position, pos) == Point.Empty)
					continue;

				// Swap the tile if throwing on the right side
				bool swap = false;
				if (pos == GroundPosition.NorthEast || pos == GroundPosition.SouthEast)
					swap = true;

				foreach (FlyingItem fi in flyings[(int)pos])
					ItemsTileset.Draw(fi.Item.ThrowTileID + offset, MazeDisplayCoordinates.GetFlyingItem(position, pos), swap, false);

			}
			#endregion


		}


		/// <summary>
		/// Draws the minimap
		/// </summary>
		/// <param name="team">Team handle</param>
		/// <param name="location">Location on the screen</param>
		public void DrawMiniMap(Team team, Point location)
		{

			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					MazeBlock block = GetBlock(new Point(x, y));

					switch (block.Type)
					{
						case BlockType.Wall:
							Display.Color = Color.Black;
						break;
						case BlockType.Illusion:
							Display.Color = Color.Gray;
						break;
						default:
							Display.Color = Color.White;
						break;
					}


					
					if (GetMonsterCount(block.Location.Position) > 0)
						Display.Color = Color.Red;
					if (block.Door != null)
						Display.Color = Color.Yellow;
					if (block.Stair != null)
						Display.Color = Color.LightGreen;

					if (team.Location.Position.X == x && team.Location.Position.Y == y && team.Location.Maze == this)
						Display.Color = Color.Blue;

					Display.FillRectangle(new Rectangle(location.X + x * 4, location.Y + y * 4, 4, 4), Display.Color);
				}


			// Draw monster target
			foreach (Monster monster in Monsters)
			{
				Point start = monster.Location.Position;
				start.X *= 4;
				start.Y *= 4;
				start.Offset(location);
				start.Offset(2, 2);

				// Sight zone
				Rectangle zone = new Rectangle(monster.SightZone.X * 4 + location.X, monster.SightZone.Y * 4 + location.Y,
					monster.SightZone.Width * 4, monster.SightZone.Height * 4);
				Display.FillRectangle(zone, Color.FromArgb(128, Color.Red));

	
				//TODO a deplacer en tant que propriete de Monster
				if (monster.StateManager.CurrentState is MoveState)
				{
					// Direction
					MoveState state = monster.StateManager.CurrentState as MoveState;
					Point end = state.TargetLocation.Position;
					end.X *= 4;
					end.Y *= 4;
					end.Offset(location);
					end.Offset(2, 2);

					Display.DrawLine(start, end, Color.Blue);
				}
			}
		}


		#endregion


		#region IO

		/// <summary>
		/// Loads the maze definition
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;

			MazeBlock block = null;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						WallTilesetName = node.Attributes["wall"].Value;
						OverlayTilesetName = node.Attributes["overlay"].Value;
						ItemsTilesetName = node.Attributes["items"].Value;

					}
					break;

					case "description":
					{
						Description = node.InnerText;
					}
					break;

					case "flyingitems":
					{
						foreach (XmlNode subnode in node)
						{
							FlyingItem item = new FlyingItem(null);
							item.Load(subnode);
							FlyingItems.Add(item);
						}
					}
					break;

					case "zone":
					{
						MazeZone zone = new MazeZone();
						zone.Load(node);
						Zones.Add(zone);
					}
					break;

					#region Blocks

					case "blocks":
					{
						// Resize maze
						Size = new Size(int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value));

						foreach (XmlNode subnode in node)
						{
							switch (subnode.Name.ToLower())
							{
								// Add a row
								case "block":
								{
									block = new MazeBlock(this);
									block.Load(subnode);

									Blocks[block.Location.Position.Y][block.Location.Position.X] = block;

									//
									// Collect block informations
									//

									// Add door to the list
									if (block.Door != null)
										Doors.Add(block.Door);
								}
								break;
							}
						}

					}
					break;

					#endregion


					#region Monsters

					case "monsters":
					{
						foreach (XmlNode subnode in node)
						{
							Monster monster = new Monster(this);
							Monsters.Add(monster);
							monster.Load(subnode);
							monster.Location.SetMaze(Name);
						}

					}
					break;
					#endregion

				}
			}



			return true;
		}



		/// <summary>
		/// Saves the maze definition
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("maze");
			writer.WriteAttributeString("name", Name);

			// Tilesets
			writer.WriteStartElement("tileset");
			writer.WriteAttributeString("wall", WallTilesetName);
			writer.WriteAttributeString("overlay", OverlayTilesetName);
			writer.WriteAttributeString("items", ItemsTilesetName);
			writer.WriteEndElement();

			writer.WriteStartElement("description");
			writer.WriteString(Description);
			writer.WriteEndElement();


			// Blocks
			writer.WriteStartElement("blocks");
			writer.WriteAttributeString("width", Size.Width.ToString());
			writer.WriteAttributeString("height", Size.Height.ToString());


			foreach (List<MazeBlock> list in Blocks)
				foreach(MazeBlock block in list)
					block.Save(writer);

			writer.WriteEndElement();

			// Zones
			foreach (MazeZone zone in Zones)
				zone.Save(writer);



			// Monsters
			if (Monsters.Count > 0)
			{
				writer.WriteStartElement("monsters");
				foreach (Monster monster in Monsters)
					monster.Save(writer);
				writer.WriteEndElement();
			}

			// flying items
			if (FlyingItems.Count > 0)
			{
				writer.WriteStartElement("flyingitems");
				foreach (FlyingItem item in FlyingItems)
					item.Save(writer);
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			return true;
		}

		#endregion


		#region Resize

		/// <summary>
		/// Resizes the maze
		/// </summary>
		/// <param name="newsize">new size</param>
		public void Resize(Size newsize)
		{
			// Rows
			if (newsize.Height > Size.Height)
			{
				for (int y = Size.Height; y < newsize.Height; y++)
					InsertRow(y);
			}
			else if (newsize.Height < Size.Height)
			{
				for (int y = Size.Height - 1; y >= newsize.Height; y--)
					RemoveRow(y);
			}


			// Columns
			if (newsize.Width > Size.Width)
			{
				for (int x = Size.Width; x < newsize.Width; x++)
					InsertColumn(x);
			}
			else if (newsize.Width < Size.Width)
			{
				for (int x = Size.Width - 1; x >= newsize.Width; x--)
					RemoveColumn(x);
			}


			size = newsize;

			for (int y = 0; y < size.Height; y++)
				for (int x = 0; x < size.Width; x++)
				{
					Blocks[y][x].Location.Position = new Point(x, y);
				}
		}


		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="rowid">Position of insert</param>
		public void InsertRow(int rowid)
		{
			// Build the row
			List<MazeBlock> row = new List<MazeBlock>(Size.Width);
			for (int x = 0; x < Size.Width; x++)
				row.Add(new MazeBlock(this));

			// Adds the row at the end
			if (rowid >= Blocks.Count)
			{
				Blocks.Add(row);
			}

			// Or insert the row
			else
			{
				Blocks.Insert(rowid, row);

				// Offset objects
			//	Rectangle zone = new Rectangle(0, rowid * level.BlockDimension.Height, level.Dimension.Width, level.Dimension.Height);
			//	OffsetObjects(zone, new Point(0, level.BlockDimension.Height));
			}
		}


		/// <summary>
		/// Removes a row
		/// </summary>
		/// <param name="rowid">Position of remove</param>
		public void RemoveRow(int rowid)
		{
			// Removes the row
			if (rowid >= Blocks.Count)
				return;

			Blocks.RemoveAt(rowid);

			// Offset objects
		//	Rectangle zone = new Rectangle(0, rowid * level.BlockDimension.Height, level.Dimension.Width, level.Dimension.Height);
		//	OffsetObjects(zone, new Point(0, -level.BlockDimension.Height));
		}


		/// <summary>
		/// Inserts a column
		/// </summary>
		/// <param name="columnid">Position of remove</param>
		public void InsertColumn(int columnid)
		{
			// Insert the column
			foreach (List<MazeBlock> row in Blocks)
			{
				if (columnid >= row.Count)
				{
					row.Add(new MazeBlock(this));
				}
				else
				{
					row.Insert(columnid, new MazeBlock(this));

					// Offset objects
				//	Rectangle zone = new Rectangle(columnid * level.BlockDimension.Width, 0, level.Dimension.Width, level.Dimension.Height);
				//	OffsetObjects(zone, new Point(level.BlockDimension.Width, 0));
				}
			}
		}


		/// <summary>
		/// Removes a column
		/// </summary>
		/// <param name="columnid">Position of remove</param>
		public void RemoveColumn(int columnid)
		{
			// Remove the column
			foreach (List<MazeBlock> row in Blocks)
			{
				row.RemoveAt(columnid);
			}

			// Offset objects
		//	Rectangle zone = new Rectangle(columnid * level.BlockDimension.Width, 0, level.Dimension.Width, level.Dimension.Height);
		//	OffsetObjects(zone, new Point(-level.BlockDimension.Width, 0));
		}


		/// <summary>
		/// Offsets EVERY objects (monsters, items...) in the maze
		/// </summary>
		/// <param name="zone">Each object in this rectangle</param>
		/// <param name="offset">Offset to move</param>
		void OffsetObjects(Rectangle zone, Point offset)
		{
/*	
			// Move entities
			foreach (Entity entity in Entities.Values)
			{
				if (zone.Contains(entity.Location))
				{
					entity.Location = new Point(
						entity.Location.X + offset.X,
						entity.Location.Y + offset.Y);
				}
			}


			// Mode SpawnPoints
			foreach (SpawnPoint spawn in SpawnPoints.Values)
			{
				if (zone.Contains(spawn.Location))
				{
					spawn.Location = new Point(
						spawn.Location.X + offset.X,
						spawn.Location.Y + offset.Y);
				}
			}
*/
		}

		#endregion


		#region Properties

		
		/// <summary>
		/// Wall TileSet to use
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string WallTilesetName
		{
			get;
			set;
		}


		/// <summary>
		/// Wall tileset
		/// </summary>
		[Browsable(false)]
		public TileSet WallTileset
		{
			get;
			private set;
		}


		/// <summary>
		/// Layout TileSet
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string OverlayTilesetName
		{
			get;
			set;
		}

		/// <summary>
		/// Overlay tileset
		/// </summary>
		[Browsable(false)]
		public TileSet OverlayTileset
		{
			get;
			private set;
		}


		/// <summary>
		/// Ground items tilesets
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string ItemsTilesetName
		{
			get;
			set;
		}


		/// <summary>
		/// Items tileset
		/// </summary>
		[Browsable(false)]
		public TileSet ItemsTileset
		{
			get;
			private set;
		}




		/// <summary>
		/// Blocks in the maze
		/// </summary>
		List<List<MazeBlock>> Blocks;

		/// <summary>
		/// Gets the size of the maze
		/// </summary>
		public Size Size
		{
			get
			{
				return size;
			}
			set
			{
				Resize(value);
			}
		}
		Size size;


		/// <summary>
		/// Rectangle (size) of the maze
		/// </summary>
		[Browsable(false)]
		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle(Point.Empty, Size);
			}
		}


		/// <summary>
		/// Monsters in the maze
		/// </summary>
		[Browsable(false)]
		public List<Monster> Monsters
		{
			get;
			private set;
		}



		/// <summary>
		/// Private list of doors in the maze
		/// </summary>
		[Category("Blocks")]
		List<Door> Doors;



		/// <summary>
		/// Name of the maze
		/// </summary>
		public string Name
		{
			get;
			set;
		}



		/// <summary>
		/// Description of the maze
		/// </summary>
		public string Description
		{
			get;
			set;
		}



		/// <summary>
		/// Dungeon the maze belongs to
		/// </summary>
		[Browsable(false)]
		public Dungeon Dungeon
		{
			get;
			private set;
		}



		/// <summary>
		/// Flying items in the maze
		/// </summary>
		[Browsable(false)]
		public List<FlyingItem> FlyingItems
		{
			get;
			private set;
		}



		/// <summary>
		/// Level Experience Multiplier when Heroes gain experience
		/// </summary>
		public byte ExperienceMultiplier;



		/// <summary>
		/// List of available zones
		/// </summary>
		public List<MazeZone> Zones
		{
			get;
			private set;
		}

		#endregion

	}


	/// <summary>
	/// Get the walls and the monsters viewed from a point of view
	/// </summary>
	public class ViewField
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="maze">Maze</param>
		/// <param name="location">Team's location</param>
		public ViewField(Maze maze, DungeonLocation location)
		{
			Maze = maze;
			Blocks = new MazeBlock[19];
			Monsters = new Monster[19][];
			for (int i = 0; i < Monsters.Length; i++)
				Monsters[i] = new Monster[4];




			// Cone of vision : 18 blocks + 1 block for the Point of View
			//
			//   ABCDEFG
			//    HIJKL
			//     MNO
			//     P^Q
			//
			// ^ => Point of view
			switch (location.Direction)
			{
				#region North
				case CardinalPoint.North:
				{
					Blocks[0] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y - 3));
					Blocks[1] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y - 3));
					Blocks[2] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y - 3));
					Blocks[3] = maze.GetBlock(new Point(location.Position.X, location.Position.Y - 3));
					Blocks[4] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y - 3));
					Blocks[5] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y - 3));
					Blocks[6] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y - 3));

					Blocks[7] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y - 2));
					Blocks[8] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y - 2));
					Blocks[9] = maze.GetBlock(new Point(location.Position.X, location.Position.Y - 2));
					Blocks[10] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y - 2));
					Blocks[11] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y - 2));

					Blocks[12] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y - 1));
					Blocks[13] = maze.GetBlock(new Point(location.Position.X, location.Position.Y - 1));
					Blocks[14] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y - 1));

					Blocks[15] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y));
					Blocks[17] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y));


					
					
					Monsters[0] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y - 3));
					Monsters[1] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y - 3));
					Monsters[2] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y - 3));
					Monsters[3] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y - 3));
					Monsters[4] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y - 3));
					Monsters[5] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y - 3));
					Monsters[6] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y - 3));

					Monsters[7] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y - 2));
					Monsters[8] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y - 2));
					Monsters[9] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y - 2));
					Monsters[10] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y - 2));
					Monsters[11] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y - 2));

					Monsters[12] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y - 1));
					Monsters[13] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y - 1));
					Monsters[14] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y - 1));

					Monsters[15] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y));
					Monsters[17] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y));


				}
				break;
				#endregion

				#region South
				case CardinalPoint.South:
				{
					Blocks[0] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y + 3));
					Blocks[1] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y + 3));
					Blocks[2] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y + 3));
					Blocks[3] = maze.GetBlock(new Point(location.Position.X, location.Position.Y + 3));
					Blocks[4] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y + 3));
					Blocks[5] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y + 3));
					Blocks[6] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y + 3));

					Blocks[7] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y + 2));
					Blocks[8] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y + 2));
					Blocks[9] = maze.GetBlock(new Point(location.Position.X, location.Position.Y + 2));
					Blocks[10] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y + 2));
					Blocks[11] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y + 2));

					Blocks[12] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y + 1));
					Blocks[13] = maze.GetBlock(new Point(location.Position.X, location.Position.Y + 1));
					Blocks[14] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y + 1));

					Blocks[15] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y));
					Blocks[17] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y));


					Monsters[0] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y + 3));
					Monsters[1] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y + 3));
					Monsters[2] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y + 3));
					Monsters[3] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y + 3));
					Monsters[4] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y + 3));
					Monsters[5] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y + 3));
					Monsters[6] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y + 3));

					Monsters[7] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y + 2));
					Monsters[8] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y + 2));
					Monsters[9] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y + 2));
					Monsters[10] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y + 2));
					Monsters[11] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y + 2));

					Monsters[12] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y + 1));
					Monsters[13] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y + 1));
					Monsters[14] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y + 1));

					Monsters[15] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y));
					Monsters[17] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y));

				}
				break;
				#endregion

				#region East
				case CardinalPoint.East:
				{
					Blocks[0] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y - 3));
					Blocks[1] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y - 2));
					Blocks[2] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y - 1));
					Blocks[3] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y));
					Blocks[4] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y + 1));
					Blocks[5] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y + 2));
					Blocks[6] = maze.GetBlock(new Point(location.Position.X + 3, location.Position.Y + 3));

					Blocks[7] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y - 2));
					Blocks[8] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y - 1));
					Blocks[9] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y));
					Blocks[10] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y + 1));
					Blocks[11] = maze.GetBlock(new Point(location.Position.X + 2, location.Position.Y + 2));

					Blocks[12] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y - 1));
					Blocks[13] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y));
					Blocks[14] = maze.GetBlock(new Point(location.Position.X + 1, location.Position.Y + 1));

					Blocks[15] = maze.GetBlock(new Point(location.Position.X, location.Position.Y - 1));
					Blocks[17] = maze.GetBlock(new Point(location.Position.X, location.Position.Y + 1));


					Monsters[0] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y - 3));
					Monsters[1] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y - 2));
					Monsters[2] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y - 1));
					Monsters[3] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y));
					Monsters[4] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y + 1));
					Monsters[5] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y + 2));
					Monsters[6] = maze.GetMonsters(new Point(location.Position.X + 3, location.Position.Y + 3));

					Monsters[7] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y - 2));
					Monsters[8] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y - 1));
					Monsters[9] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y));
					Monsters[10] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y + 1));
					Monsters[11] = maze.GetMonsters(new Point(location.Position.X + 2, location.Position.Y + 2));

					Monsters[12] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y - 1));
					Monsters[13] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y));
					Monsters[14] = maze.GetMonsters(new Point(location.Position.X + 1, location.Position.Y + 1));

					Monsters[15] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y - 1));
					Monsters[17] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y + 1));

				
				}
				break;
				#endregion

				#region West
				case CardinalPoint.West:
				{
					Blocks[0] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y + 3));
					Blocks[1] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y + 2));
					Blocks[2] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y + 1));
					Blocks[3] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y));
					Blocks[4] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y - 1));
					Blocks[5] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y - 2));
					Blocks[6] = maze.GetBlock(new Point(location.Position.X - 3, location.Position.Y - 3));

					Blocks[7] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y + 2));
					Blocks[8] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y + 1));
					Blocks[9] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y));
					Blocks[10] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y - 1));
					Blocks[11] = maze.GetBlock(new Point(location.Position.X - 2, location.Position.Y - 2));

					Blocks[12] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y + 1));
					Blocks[13] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y));
					Blocks[14] = maze.GetBlock(new Point(location.Position.X - 1, location.Position.Y - 1));

					Blocks[15] = maze.GetBlock(new Point(location.Position.X, location.Position.Y + 1));
					Blocks[17] = maze.GetBlock(new Point(location.Position.X, location.Position.Y - 1));


					Monsters[0] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y + 3));
					Monsters[1] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y + 2));
					Monsters[2] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y + 1));
					Monsters[3] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y));
					Monsters[4] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y - 1));
					Monsters[5] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y - 2));
					Monsters[6] = maze.GetMonsters(new Point(location.Position.X - 3, location.Position.Y - 3));

					Monsters[7] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y + 2));
					Monsters[8] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y + 1));
					Monsters[9] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y));
					Monsters[10] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y - 1));
					Monsters[11] = maze.GetMonsters(new Point(location.Position.X - 2, location.Position.Y - 2));

					Monsters[12] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y + 1));
					Monsters[13] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y));
					Monsters[14] = maze.GetMonsters(new Point(location.Position.X - 1, location.Position.Y - 1));

					Monsters[15] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y + 1));
					Monsters[17] = maze.GetMonsters(new Point(location.Position.X, location.Position.Y - 1));
				}
				break;
				#endregion

			}

			// Team's position
			Blocks[16] = maze.GetBlock(new Point(location.Position.X, location.Position.Y));


		}

		/// <summary>
		/// Returns a list of all monster
		/// </summary>
		/// <param name="pos">Position in the view field</param>
		/// <returns></returns>
		public Monster[] GetMonsters(ViewFieldPosition pos)
		{
			switch (pos)
			{
				case ViewFieldPosition.B:
				return Monsters[1];

				case ViewFieldPosition.C:
				return Monsters[2];

				case ViewFieldPosition.D:
				return Monsters[3];

				case ViewFieldPosition.E:
				return Monsters[4];

				case ViewFieldPosition.F:
				return Monsters[5];

				case ViewFieldPosition.I:
				return Monsters[8];

				case ViewFieldPosition.J:
				return Monsters[9];

				case ViewFieldPosition.K:
				return Monsters[10];

				case ViewFieldPosition.M:
				return Monsters[12];

				case ViewFieldPosition.N:
				return Monsters[13];

				case ViewFieldPosition.O:
				return Monsters[14];

			}

			return new Monster[4];
		}


		/// <summary>
		/// Gets a block in the view field
		/// </summary>
		/// <param name="position">Block position</param>
		/// <returns>Block handle</returns>
		public MazeBlock GetBlock(ViewFieldPosition position)
		{
			return Blocks[(int)position];
		}


		#region Properties


		/// <summary>
		/// Blocks in the maze
		/// </summary>
		public MazeBlock[] Blocks
		{
			get;
			private set;
		}



		/// <summary>
		/// Monster list
		/// </summary>
		Monster[][] Monsters;


		/// <summary>
		/// Is the team visible, no wall between the Team and the Point Of View 
		/// </summary>
		public bool IsTeamVisible
		{
			get;
			private set;
		}


		/// <summary>
		/// Current maze
		/// </summary>
		public Maze Maze
		{
			get;
			private set;
		}

		#endregion
	}



	/// <summary>
	/// Block position in the view field
	/// Cone of vision : 18 blocks + 1 block for the Point of View
	///
	///   ABCDEFG
	///    HIJKL
	///     MNO
	///     P^Q
	///
	/// ^ => Point of view of the team
	/// </summary>
	public enum ViewFieldPosition
	{
		A = 0,
		B = 1,
		C = 2,
		D = 3,
		E = 4,
		F = 5,
		G = 6,

		H = 7,
		I = 8,
		J = 9,
		K = 10,
		L = 11,
	
		M = 12,
		N = 13,
		O = 14,

		P = 15,
		Team = 16,
		Q = 17
	}




	/// <summary>
	/// Maze Enumerator for PropertyGrids
	/// </summary>
	public class MazeEnumerator : StringConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> list = new List<string>();

			list.Add("a");
			list.Add("z");
			list.Add("e");
			list.Add("rt");
			list.Add("y");
			list.Add("u");
			list.Add("i");
			list.Insert(0, "");


			return new StandardValuesCollection(list);
		}

	}



}
