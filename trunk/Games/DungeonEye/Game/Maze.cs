﻿#region Licence
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
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using DungeonEye.MonsterStates;

namespace DungeonEye
{

	/// <summary>
	///  A maze in a Dungeon
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
		/// <param name="dungeon">Dungeon handle</param>
		public Maze(Dungeon dungeon)
		{
			Name = "No name";
			Dungeon = dungeon;

			Blocks = new List<List<Square>>();
			ThrownItems = new List<ThrownItem>();
			Zones = new List<MazeZone>();

			IsDisposed = false;
		}



		/// <summary>
		/// Initialize the maze
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			WallTileset = ResourceManager.CreateSharedAsset<TileSet>(WallTilesetName, WallTilesetName);
			if (WallTileset == null)
				Trace.WriteLine("[Maze] Init() : Failed to load wall tileset for the maze \"" + Name + "\".");

			Decoration = ResourceManager.CreateSharedAsset<DecorationSet>(DecorationName, DecorationName);
			if (Decoration == null)
				Trace.WriteLine("[Maze] Init() : Failed to load decoration for the maze \"" + Name + "\".");

			ItemsTileset = ResourceManager.CreateSharedAsset<TileSet>(ItemsTilesetName, ItemsTilesetName);
			if (ItemsTileset == null)
				Trace.WriteLine("[Maze] Init() : Failed to load items tileset for the maze \"" + Name + "\".");

			DoorTileset = ResourceManager.CreateSharedAsset<TileSet>("Doors", "Doors");
			if (DoorTileset == null)
				Trace.WriteLine("[Maze] Init() : Failed to load items tileset for doors.");


			foreach (List<Square> list in Blocks)
				foreach (Square square in list)
				{
					square.Init();

					#region Pits
					// Find pit destination squares
					Pit pit = square.Actor as Pit;

					if (pit != null && pit.Target != null)
					{
						// Maze exists
						Maze maze = Dungeon.GetMaze(pit.Target.Maze);
						if (maze == null)
							continue;

						// Destination exists
						Square blk = maze.GetSquare(pit.Target.Coordinate);
						if (blk == null)
							continue;

						blk.IsPitTarget = true;
					}
					#endregion
				}

			return true;
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{

			// Dispose each sqaure
			foreach (List<Square> list in Blocks)
				foreach (Square square in list)
					square.Dispose();


			ResourceManager.UnlockSharedAsset<TileSet>(DoorTileset);
			DoorTileset = null;

			ResourceManager.UnlockSharedAsset<TileSet>(ItemsTileset);
			ItemsTileset = null;

			ResourceManager.UnlockSharedAsset<DecorationSet>(Decoration);
			Decoration = null;

			ResourceManager.UnlockSharedAsset<TileSet>(WallTileset);
			WallTileset = null;

			Blocks.Clear();
			Description = null;
			Dungeon = null;
			ThrownItems.Clear();
			ItemsTilesetName = null;
			DecorationName = null;
			WallTilesetName = null;
			size = Size.Empty;
			Zones.Clear();
			Name = "";

			IsDisposed = true;
		}


		/// <summary>
		/// Update all elements in the maze
		/// </summary>
		public void Update(GameTime time)
		{
			// Update monsters
			foreach (List<Square> list in Blocks)
				foreach (Square square in list)
				{
					square.Update(time);
				}

			/*
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
									Square block = GetBlock(monster.Location.Position);

									//ItemSet itemset = ResourceManager.CreateSharedAsset<ItemSet>("Main");
									foreach (string name in monster.ItemsInPocket)
										block.DropItem(monster.Location.Position, ResourceManager.CreateAsset<Item>(name));
								}

								return true;
							});


			*/

			#region Flying items

			// Update flying items
			foreach (ThrownItem item in ThrownItems)
				item.Update(time, this);

			// Remove all blocked flying items
			ThrownItems.RemoveAll(fi =>
				{
					// Item can fly
					if (fi.Distance > 0)
						return false;


					// Make the item falling on the ground
					SquarePosition pos = fi.Location.Position;
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

					GetSquare(fi.Location.Coordinate).DropItem(pos, fi.Item);


					return true;
				});
			#endregion
		}


		#region Monsters

		/*

		/// <summary>
		/// Adds a monster in the maze
		/// </summary>
		/// <param name="monster">Monster handle</param>
		/// <param name="location">Location in the maze</param>
		/// <param name="position"></param>
		public void SetMonster(Monster monster, Point location, SquarePosition position)
		{
			Square square = GetBlock(location);
			if (square != null)
				square.SetMonster(monster, position);
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
					list[(int)monster.Location.Position] = monster;

			return list;
		}


		/// <summary>
		/// Returns the monster at a given location
		/// </summary>
		/// <param name="location">Location in the maze</param>
		/// <param name="position">Ground location</param>
		/// <returns>Monster handle or null</returns>
		public Monster GetMonster(DungeonLocation location, SquarePosition position)
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

*/

		#endregion


		#region  Helper



		/// <summary>
		/// Checks if a maze location is valid
		/// </summary>
		/// <param name="pos">Point in the maze</param>
		/// <returns>True if the point is in the maze, false if the point is outside the maze</returns>
		public bool Contains(Point pos)
		{
			return new Rectangle(Point.Empty, Size).Contains(pos);
		}


		/*
				/// <summary>
				/// Gets if a given location is free for a monster
				/// </summary>
				/// <param name="Location"></param>
				/// <param name="position"></param>
				/// <returns></returns>
				public bool IsLocationFree(DungeonLocation location, SquarePosition position)
				{
					if (location == null)
						return false;

					Square square = GetBlock(location.Position);
					if (square.IsBlocking)
						return true;
			

					return false;
				}
		*/

		/// <summary>
		/// Gets if a door is North-South aligned
		/// </summary>
		/// <param name="location">Door location in the maze</param>
		/// <returns>True if the door is pointing north or south</returns>
		public bool IsDoorNorthSouth(DungeonLocation location)
		{
			if (location == null)
				throw new ArgumentNullException("location");

			Point left = new Point(location.Coordinate.X - 1, location.Coordinate.Y);
			if (!Contains(left))
				return false;

			Square block = GetSquare(left);
			return (block.IsWall);
		}

		/// <summary>
		/// Returns informations about a block in the maze
		/// </summary>
		/// <param name="location">Location of the block</param>
		/// <returns>Square handle</returns>
		public Square GetSquare(Point location)
		{
			if (!Rectangle.Contains(location))
				return new Square(this);

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
		public List<ThrownItem>[] GetFlyingItems(DungeonLocation location, CardinalPoint direction)
		{
			List<ThrownItem>[] tmp = new List<ThrownItem>[5];
			tmp[0] = new List<ThrownItem>();
			tmp[1] = new List<ThrownItem>();
			tmp[2] = new List<ThrownItem>();
			tmp[3] = new List<ThrownItem>();
			tmp[4] = new List<ThrownItem>();

			foreach (ThrownItem item in ThrownItems)
			{
				// Not in the same maze
				if (item.Location.Maze != location.Maze)
					continue;

				// Same coordinate
				if (item.Location.Coordinate == location.Coordinate)
					tmp[(int)item.Location.Position].Add(item);
			}


			// Swap according to view point direction
			List<ThrownItem>[] items = new List<ThrownItem>[5];
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
		/// <param name="batch">SpriteBatch to use</param>
		/// <param name="location">Location to display from</param>
		/// <see cref="http://eob.wikispaces.com/eob.vmp"/>
		public void Draw(SpriteBatch batch, DungeonLocation location)
		{
			if (WallTileset == null)
				return;

			// Clear the spritebatch
			batch.End();
			Display.PushScissor(new Rectangle(0, 0, 352, 240));
			batch.Begin();


			//
			// 
			//
			ViewField pov = new ViewField(this, location);


			// Backdrop
			// The background is assumed to be x-flipped when party.x & party.y & party.direction = 1.
			// I.e. all kind of moves and rotations from the current position will result in the background being x-flipped.
			//bool flipbackdrop = ((location.Position.X + location.Position.Y + (int)location.Direction) & 1) == 0;
			SpriteEffects effect = ((location.Coordinate.X + location.Coordinate.Y + (int)location.Direction) & 1) == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			batch.DrawTile(WallTileset, 0, Point.Empty, Color.White, 0.0f, effect, 0.0f);


			// alternate the wall
			int swap = (location.Coordinate.Y % 2) * 9;


			// maze block draw order
			// A G B F C E D
			// H L I K J
			// M O N
			// P Team Q

			#region row -3
			DrawSquare(batch, pov, ViewFieldPosition.A, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.G, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.B, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.F, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.C, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.E, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.D, location.Direction);
			#endregion

			#region row -2
			DrawSquare(batch, pov, ViewFieldPosition.H, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.L, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.I, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.K, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.J, location.Direction);
			#endregion

			#region row -1
			DrawSquare(batch, pov, ViewFieldPosition.M, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.O, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.N, location.Direction);
			#endregion

			#region row 0
			DrawSquare(batch, pov, ViewFieldPosition.P, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.Team, location.Direction);
			DrawSquare(batch, pov, ViewFieldPosition.Q, location.Direction);
			#endregion


			// Clear the spritebatch
			batch.End();
			Display.PopScissor();
			batch.Begin();

		}



		/// <summary>
		/// Draws a square
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		/// <param name="field">View field</param>
		/// <param name="position">Position in the view field</param>
		/// <param name="view">Looking direction of the team</param>
		void DrawSquare(SpriteBatch batch, ViewField field, ViewFieldPosition position, CardinalPoint view)
		{
			if (field == null)
				return;

			Square block = field.Blocks[(int)position];
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
				//TODO
				td = DisplayCoordinates.GetCeilingPit(position);
				//if (td != null)
				//	batch.DrawTile(OverlayTileset, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
				//***batch.DrawTile(ItemsTileset, td.ID, td.Location, td.SwapX, td.SwapY);
			}

			#endregion

			#region Items on ground before a door

			// Zoom factors for items
			float[] ItemZoom = new float[] { 1.0f, 0.66f, 0.5f, 0.33f };
			Vector2[] scale = new Vector2[]
			{
				new Vector2(1.0f, 1.0f),
				new Vector2(0.66f, 0.66f),
				new Vector2(0.5f, 0.5f),
				new Vector2(0.33f, 0.33f)
			};
			Color[] colors = new Color[]
			{
				Color.White,
				Color.White,
				Color.FromArgb(255, 128, 128, 128),
				Color.FromArgb(128, 128, 128, 128),
			};

			List<Item>[] list = block.GetItems(view);
			if (!block.IsWall)
			{
				for (int i = 0; i < 2; i++)
				{
					if (list[i].Count == 0)
						continue;

					foreach (Item item in list[i])
					{
						point = DisplayCoordinates.GetGroundPosition(position, (SquarePosition)i);
						if (!point.IsEmpty)
						{
							Tile tile = ItemsTileset.GetTile(item.GroundTileID);
							if (tile != null)
							{
								// Get the screen location
								Rectangle rect = new Rectangle(
									point.X - tile.Origin.X, point.Y - tile.Origin.Y,
									tile.Rectangle.Width / (offset + 1), tile.Rectangle.Height / (offset + 1));

								batch.DrawTile(ItemsTileset, item.GroundTileID, point, colors[offset], 0.0f, scale[offset], SpriteEffects.None, 0.0f);
							}
						}
					}
				}
			}
			#endregion

			#region Actor
			if (block.Actor != null)
			{
				block.Actor.Draw(batch, field, position, view);
			}
			#endregion

			#region Walls
			if (block.IsWall)
			{
				// Walls
				foreach (TileDrawing tmp in DisplayCoordinates.GetWalls(position))
					batch.DrawTile(WallTileset, tmp.ID, tmp.Location, Color.White, 0.0f, tmp.Effect, 0.0f);


				// Alcoves
				if (block.HasAlcoves)
				{
					// Draw alcoves
					foreach (CardinalPoint side in Enum.GetValues(typeof(CardinalPoint)))
					{
						td = DisplayCoordinates.GetDecoration(position, side);
						//TODO
						//if (td != null && block.HasAlcove(view, side))
						//    batch.DrawTile(OverlayTileset, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
					}

					// Draw items in the alcove in front of the team
					td = DisplayCoordinates.GetDecoration(position, CardinalPoint.South);
					if (td != null)
					{
						Point[] offsets = new Point[]
						{
							new Point(10, 10),		// A
							new Point(10, 10),		// B
							new Point(10, 10),		// C
							new Point(10, 10),		// D
							new Point(10, 10),		// E
							new Point(10, 10),		// F
							new Point(10, 10),		// G
							new Point(20, 20),		// H
							new Point(20, 20),		// I
							new Point(20, 20),		// J
							new Point(20, 20),		// K
							new Point(16, 16),		// L
							new Point(32, 32),		// M
							new Point(32, 32),		// N
							new Point(32, 32),		// O
						};

						foreach (Item item in block.GetAlcoveItems(view, CardinalPoint.South))
						{
							Point loc = td.Location;
							loc.Offset(offsets[(int)position]);
							batch.DrawTile(ItemsTileset, item.GroundTileID + offset, loc);
						}
					}
				}

			}
			#endregion

			#region Items on ground after a door
			if (!block.IsWall)
			{
				for (int i = 2; i < 4; i++)
				{
					if (list[i].Count == 0)
						continue;

					foreach (Item item in list[i])
					{
						// Get screen coordinate
						point = DisplayCoordinates.GetGroundPosition(position, (SquarePosition)i);
						if (!point.IsEmpty)
						{
							Tile tile = ItemsTileset.GetTile(item.GroundTileID);
							if (tile != null)
							{
								Rectangle rect = tile.Rectangle;
								rect.Inflate(offset, offset);

								//batch.DrawTile(ItemsTileset, item.GroundTileID, point, rect, Color.Red);
							//	batch.DrawTile(ItemsTileset, item.GroundTileID + offset, point);
								batch.DrawTile(ItemsTileset, item.GroundTileID, point, colors[offset], 0.0f, scale[offset], SpriteEffects.None, 0.0f);
							}
						}
					}
				}
			}
			#endregion

			#region Monsters
			if (block.MonsterCount > 0)
			{
				int[][] order = new int[][]
				{
					new int[] {0, 1, 2, 3},	// North
					new int[] {3, 2, 1, 0},	// South
					new int[] {2, 0, 3, 1},	// West
					new int[] {1, 3, 0, 2},	// East
				};

				for (int i = 0; i < 4; i++)
				{
					Monster monster = block.Monsters[order[(int)view][i]];
					if (monster != null)
						monster.Draw(batch, view, position);
				}
			}
			#endregion

			#region Flying items
			List<ThrownItem>[] flyings = GetFlyingItems(block.Location, view);
			foreach (SquarePosition pos in Enum.GetValues(typeof(SquarePosition)))
			{
				point = DisplayCoordinates.GetFlyingItem(position, pos);
				//	if (point == Point.Empty)
				//		continue;

				// Swap the tile if throwing on the right side
				SpriteEffects fx = SpriteEffects.None;
				if (pos == SquarePosition.NorthEast || pos == SquarePosition.SouthEast)
					fx = SpriteEffects.FlipHorizontally;

				foreach (ThrownItem fi in flyings[(int)pos])
					batch.DrawTile(ItemsTileset, fi.Item.ThrowTileID + offset, point, Color.White, 0.0f, fx, 0.0f);

			}
			#endregion


		}


		/// <summary>
		/// Draws the minimap
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		/// <param name="location">Location on the screen</param>
		public void DrawMiniMap(SpriteBatch batch, Point location)
		{
			if (batch == null)
				return;

			Team team = GameScreen.Team;

			Color color;

			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					Square block = GetSquare(new Point(x, y));

					switch (block.Type)
					{
						case SquareType.Wall:
						color = Color.Black;
						break;
						case SquareType.Illusion:
						color = Color.Gray;
						break;
						default:
						color = Color.White;
						break;
					}



					if (block.MonsterCount > 0)
						color = Color.Red;
					if (block.Actor is Door)
						color = Color.Yellow;
					if (block.Actor is Pit)
						color = Color.DarkBlue;
					if (block.Actor is Stair)
						color = Color.LightGreen;
					if (block.MonsterCount > 0)
						color = Color.Red;


					if (team.Location.Coordinate.X == x && team.Location.Coordinate.Y == y && team.Maze == this)
						color = Color.Blue;

					batch.FillRectangle(new Rectangle(location.X + x * 4, location.Y + y * 4, 4, 4), color);

				}
		}


		#endregion


		#region IO

		/// <summary>
		/// Loads the maze definition
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		/// <returns>True on success</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;

			Square block = null;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						WallTilesetName = node.Attributes["wall"].Value;
						DecorationName = node.Attributes["decoration"].Value;
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
							ThrownItem item = new ThrownItem(null);
							item.Load(subnode);
							ThrownItems.Add(item);
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

					case "doors":
					{
						DefaultDoorType = (DoorType)Enum.Parse(typeof(DoorType), node.Attributes["type"].Value);
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
									block = new Square(this);
									block.Load(subnode);

									Blocks[block.Location.Coordinate.Y][block.Location.Coordinate.X] = block;
								}
								break;
							}
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
		/// <param name="writer">XmlWriter handle</param>
		/// <returns>True on success</returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("maze");
			writer.WriteAttributeString("name", Name);

			// Tilesets
			writer.WriteStartElement("tileset");
			writer.WriteAttributeString("wall", WallTilesetName);
			writer.WriteAttributeString("decoration", DecorationName);
			writer.WriteAttributeString("items", ItemsTilesetName);
			writer.WriteEndElement();

			writer.WriteStartElement("description");
			writer.WriteString(Description);
			writer.WriteEndElement();

			writer.WriteStartElement("doors");
			writer.WriteAttributeString("type", DefaultDoorType.ToString());
			writer.WriteEndElement();


			// Blocks
			writer.WriteStartElement("blocks");
			writer.WriteAttributeString("width", Size.Width.ToString());
			writer.WriteAttributeString("height", Size.Height.ToString());


			foreach (List<Square> list in Blocks)
				foreach (Square block in list)
					block.Save(writer);

			writer.WriteEndElement();

			// Zones
			foreach (MazeZone zone in Zones)
				zone.Save(writer);

			/*

						// Monsters
						if (Monsters.Count > 0)
						{
							writer.WriteStartElement("monsters");
							foreach (Monster monster in Monsters)
								monster.Save(writer);
							writer.WriteEndElement();
						}
			*/

			// flying items
			if (ThrownItems.Count > 0)
			{
				writer.WriteStartElement("flyingitems");
				foreach (ThrownItem item in ThrownItems)
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
					Blocks[y][x].Location.Coordinate = new Point(x, y);
				}
		}


		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="rowid">Position of insert</param>
		public void InsertRow(int rowid)
		{
			// Build the row
			List<Square> row = new List<Square>(Size.Width);
			for (int x = 0; x < Size.Width; x++)
				row.Add(new Square(this));

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
			foreach (List<Square> row in Blocks)
			{
				if (columnid >= row.Count)
				{
					row.Add(new Square(this));
				}
				else
				{
					row.Insert(columnid, new Square(this));

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
			foreach (List<Square> row in Blocks)
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
		/// Is asset disposed
		/// </summary>
		[Browsable(false)]
		public bool IsDisposed { get; private set; }


		#region Tilesets

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
		/// Decoration name
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string DecorationName
		{
			get;
			set;
		}

		/// <summary>
		/// Decoration handle
		/// </summary>
		[Browsable(false)]
		public DecorationSet Decoration
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
		/// Doors tileset
		/// </summary>
		[Browsable(false)]
		public TileSet DoorTileset
		{
			get;
			private set;
		}

		#endregion


		/// <summary>
		/// Blocks in the maze
		/// </summary>
		List<List<Square>> Blocks;


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
		public List<ThrownItem> ThrownItems
		{
			get;
			private set;
		}


		/// <summary>
		/// Default type of door in this maze
		/// </summary>
		public DoorType DefaultDoorType
		{
			get;
			set;
		}


		/// <summary>
		/// Level Experience Multiplier when Heroes gain experience
		/// </summary>
		public float ExperienceMultiplier;


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
	/// Get all the blocks viewed from a given point of view
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
			Blocks = new Square[19];




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
					Blocks[0] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y - 3));
					Blocks[1] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y - 3));
					Blocks[2] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y - 3));
					Blocks[3] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y - 3));
					Blocks[4] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y - 3));
					Blocks[5] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y - 3));
					Blocks[6] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y - 3));

					Blocks[7] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y - 2));
					Blocks[8] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y - 2));
					Blocks[9] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y - 2));
					Blocks[10] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y - 2));
					Blocks[11] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y - 2));

					Blocks[12] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y - 1));
					Blocks[13] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y - 1));
					Blocks[14] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y - 1));

					Blocks[15] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y));
					Blocks[17] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y));

				}
				break;
				#endregion

				#region South
				case CardinalPoint.South:
				{
					Blocks[0] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y + 3));
					Blocks[1] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y + 3));
					Blocks[2] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y + 3));
					Blocks[3] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y + 3));
					Blocks[4] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y + 3));
					Blocks[5] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y + 3));
					Blocks[6] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y + 3));

					Blocks[7] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y + 2));
					Blocks[8] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y + 2));
					Blocks[9] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y + 2));
					Blocks[10] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y + 2));
					Blocks[11] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y + 2));

					Blocks[12] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y + 1));
					Blocks[13] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y + 1));
					Blocks[14] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y + 1));

					Blocks[15] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y));
					Blocks[17] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y));
				}
				break;
				#endregion

				#region East
				case CardinalPoint.East:
				{
					Blocks[0] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y - 3));
					Blocks[1] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y - 2));
					Blocks[2] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y - 1));
					Blocks[3] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y));
					Blocks[4] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y + 1));
					Blocks[5] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y + 2));
					Blocks[6] = maze.GetSquare(new Point(location.Coordinate.X + 3, location.Coordinate.Y + 3));

					Blocks[7] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y - 2));
					Blocks[8] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y - 1));
					Blocks[9] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y));
					Blocks[10] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y + 1));
					Blocks[11] = maze.GetSquare(new Point(location.Coordinate.X + 2, location.Coordinate.Y + 2));

					Blocks[12] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y - 1));
					Blocks[13] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y));
					Blocks[14] = maze.GetSquare(new Point(location.Coordinate.X + 1, location.Coordinate.Y + 1));

					Blocks[15] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y - 1));
					Blocks[17] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y + 1));
				}
				break;
				#endregion

				#region West
				case CardinalPoint.West:
				{
					Blocks[0] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y + 3));
					Blocks[1] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y + 2));
					Blocks[2] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y + 1));
					Blocks[3] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y));
					Blocks[4] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y - 1));
					Blocks[5] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y - 2));
					Blocks[6] = maze.GetSquare(new Point(location.Coordinate.X - 3, location.Coordinate.Y - 3));

					Blocks[7] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y + 2));
					Blocks[8] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y + 1));
					Blocks[9] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y));
					Blocks[10] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y - 1));
					Blocks[11] = maze.GetSquare(new Point(location.Coordinate.X - 2, location.Coordinate.Y - 2));

					Blocks[12] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y + 1));
					Blocks[13] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y));
					Blocks[14] = maze.GetSquare(new Point(location.Coordinate.X - 1, location.Coordinate.Y - 1));

					Blocks[15] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y + 1));
					Blocks[17] = maze.GetSquare(new Point(location.Coordinate.X, location.Coordinate.Y - 1));
				}
				break;
				#endregion
			}

			// Team's position
			Blocks[16] = maze.GetSquare(location.Coordinate);
		}


		/// <summary>
		/// Gets a block in the view field
		/// </summary>
		/// <param name="position">Block position</param>
		/// <returns>Block handle</returns>
		public Square GetBlock(ViewFieldPosition position)
		{
			return Blocks[(int)position];
		}


		#region Properties


		/// <summary>
		/// Blocks in the maze
		/// </summary>
		public Square[] Blocks
		{
			get;
			private set;
		}


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
			list.Add("id");
			list.Insert(0, "");


			return new StandardValuesCollection(list);
		}

	}



}