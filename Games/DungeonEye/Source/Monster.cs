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
using ArcEngine.Audio;
using ArcEngine.Graphic;
using ArcEngine.Interface;
using ArcEngine.Utility.GameState;
using DungeonEye.Interfaces;
using DungeonEye.MonsterStates;


//
// List of monsters : http://members.tripod.com/~stanislavs/games/eob1mons.htm
// http://dmweb.free.fr/?q=node/1363
//
// http://wiki.themanaworld.org/index.php/User:Crush/Monster_Database
//
// http://wiki.themanaworld.org/index.php/Monster_Database
//
//
//
namespace DungeonEye
{

	/// <summary>
	/// Base class of all monster in the game
	/// </summary>
	public class Monster : Entity, IAsset
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="maze">Maze handle where the monster is</param>
		public Monster(Maze maze)
		{
			if (maze != null)
			{
				Location = new DungeonLocation(maze.Dungeon);
			}

			ItemsInPocket = new List<string>();
			DamageDice = new Dice();
			HitDice = new Dice();
			DefaultBehaviour = MonsterBehaviour.Aggressive;

			DrawOffsetDuration = TimeSpan.FromSeconds(1.0f + GameBase.Random.NextDouble());

			StateManager = new StateManager();

			IsDisposed = false;
		}


		/// <summary>
		/// Initializes the monster
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			Tileset = ResourceManager.CreateSharedAsset<TileSet>(TileSetName, TileSetName);

			//if (!string.IsNullOrEmpty(ScriptName) && !string.IsNullOrEmpty(InterfaceName))
			//{
			//    Script script = ResourceManager.CreateAsset<Script>(ScriptName);
			//    script.Compile();

			//    Interface = script.CreateInstance<IMonster>(InterfaceName);
			//}


			//StateManager.SetState(new IdleState(this));
			return true;
		}


		/// <summary>
		/// Move the monster
		/// </summary>
		/// <param name="offset">Offset</param>
		/// <returns>True if moved, or false</returns>
		public bool Move(Point offset)
		{
			// Can't move
			if (!CanMove)
				return false;

			// Get informations about the destination block
			Point dst = Location.Position;
			dst.Offset(offset);


			// Check all blocking states
			bool canmove = true;

			// The team
			if (Location.Dungeon.Team.Location.Maze == Location.Maze &&
				Location.Dungeon.Team.Location.Position == dst)
				canmove = false;

			// A wall
			Square dstblock = Location.Maze.GetBlock(dst);
			if (dstblock.IsBlocking)
				canmove = false;

			// Stairs
			if (dstblock.Stair != null)
				canmove = false;

			// Monsters
			if (dstblock.MonsterCount > 0)
				canmove = false;

			// blocking door
			if (dstblock.Door != null && dstblock.Door.IsBlocking)
				canmove = false;




			if (canmove)
			{
				// Leave the current block
				Location.Block.OnMonsterLeave(this);


				Location.Position.Offset(offset);
				LastAction = DateTime.Now;

				// Enter the new block
				Location.Block.OnMonsterEnter(this);
			}

			return canmove;
		}


		/// <summary>
		/// Try to attack a location
		/// </summary>
		/// <param name="location">Location to attack</param>
		/// <returns></returns>
		public bool Attack(DungeonLocation location)
		{


			return false;
		}


		/// <summary>
		/// Attack the entity
		/// </summary>
		/// <param name="attack">Attack</param>
		public override void Hit(Attack attack)
		{
			if (attack == null)
				return;

			LastAttack = attack;
			if (LastAttack.IsAMiss)
				return;

			HitPoint.Current -= LastAttack.Hit;

			// Reward the team for having killed the entity
			if (IsDead && attack.Striker is Hero)
			{
				(attack.Striker as Hero).Team.AddExperience(Reward);
			}
		}



		#region Update & Draw

		/// <summary>
		/// Update the monster logic
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public virtual void Update(GameTime time)
		{
			//if (Script.Instance != null)
			//	Script.Instance.OnUpdate(this);

//			return;

			// Draw offset
			if (LastDrawOffset + DrawOffsetDuration < DateTime.Now)
			{
				DrawOffset = new Point(GameBase.Random.Next(-10, 10), GameBase.Random.Next(-10, 10));
				LastDrawOffset = DateTime.Now;
			}



			// Update current state
			//StateManager.Update(time);


			Team team = Location.Dungeon.Team;
			Maze maze = Location.Maze;

			switch (CurrentBehaviour)
			{
				#region Aggressive
				case MonsterBehaviour.Aggressive:
				break;
				#endregion

				#region RangeAttack
				case MonsterBehaviour.RangeAttack:
				break;
				#endregion

				#region Run away
				case MonsterBehaviour.RunAway:
				break;
				#endregion

				#region Guard
				case MonsterBehaviour.Guard:
				{
					// Heal
					if (CanHeal && HitPoint.Ratio < 0.5f)
						Heal();

					// Not in the same maze
					else if (team.Location.Maze != maze)
						break;

					else if (CanGetCloserTo(team.Location))
					{
						GetCloserTo(team.Location);
					}
					else if (CanDoCloseAttack(team.Location))
					{
					}
					else if (IsNear(team.Location))
					{
						if (Location.IsFacing(team.Location))
							Location.FaceTo(team.Location);

						if (!CanUseAmmo)
							CurrentBehaviour = MonsterBehaviour.Aggressive;
					}
				}
				break;
				#endregion

				#region Friendly
				case MonsterBehaviour.Friendly:
				break;
				#endregion

				#region Friendly unmoving
				case MonsterBehaviour.FriendlyUnmoving:
				break;
				#endregion
			}
		}



		/// <summary>
		/// Try to move closer to the target while remaining in the square
		/// </summary>
		/// <param name="target">Target point</param>
		/// <returns>True if can get closer</returns>
		public bool CanGetCloserTo(DungeonLocation target)
		{
			if (target == null)
				throw new ArgumentNullException("target");

			// Monster is alone in the square
			if (Location.SquarePosition == SquarePosition.Center)
				return false;

			// Find the distance
			Point dist = new Point(Location.Position.X - target.Position.X, Location.Position.Y - target.Position.Y);

			// Current maze
			Maze maze = Location.Maze;
			Square square = maze.GetBlock(Location.Position);
			Monster[] monsters = maze.GetBlock(Location.Position).Monsters;

			// Target on the right
			if (dist.X > 0)
			{
			}
			else
			{
			}

			// Target above
			if (dist.Y > 0)
			{
				//if (Location.SquarePosition == SquarePosition.SouthWest && maze.IsLocationFree(Location, SquarePosition.NorthWest))

			}
			else
			{
			}

			return false;
		}


		/// <summary>
		/// Moves closer to the target while remaining in the square
		/// </summary>
		/// <param name="target">Target point</param>
		public void GetCloserTo(DungeonLocation target)
		{
			if (target == null)
				throw new ArgumentNullException("target");

		}


		/// <summary>
		/// Try to move closer to the target while remaining in the square
		/// </summary>
		/// <param name="target">Target point</param>
		/// <returns>True if can get closer</returns>
		public bool IsNear(DungeonLocation target)
		{
			if (target == null)
				throw new ArgumentNullException("target");

			if (Location.Maze != target.Maze)
				return false;

			Point dist = new Point(Location.Position.X - target.Position.X, Location.Position.Y - target.Position.Y);

			return Math.Abs(dist.X) < 5 || Math.Abs(dist.Y) < 5;
		}




		/// <summary>
		/// Draw the monster
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		/// <param name="direction">Team's facing direction</param>
		/// <param name="pos">Position of the monster in the field of view</param>
		public virtual void Draw(SpriteBatch batch, CardinalPoint direction, ViewFieldPosition pos)
		{
			if (Tileset == null)
				return;

			TextureEnvMode mode = Display.TexEnv;



			// Monster was hit, redraw it
			if (LastAttack != null && LastAttack.Time + TimeSpan.FromSeconds(0.25) > DateTime.Now)
			{
				Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
				Display.TexEnv = TextureEnvMode.Add;
			}


			switch (pos)
			{
				case ViewFieldPosition.B:
				Tileset.Scale = new Vector2(0.5f, 0.5f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(0 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4), Color.Gray);
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;
				case ViewFieldPosition.C:
				Tileset.Scale = new Vector2(0.5f, 0.5f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(80 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4), Color.Gray);
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;
				case ViewFieldPosition.D:
				Tileset.Scale = new Vector2(0.5f, 0.5f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(180 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4), Color.Gray);
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;
				case ViewFieldPosition.E:
				Tileset.Scale = new Vector2(0.5f, 0.5f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(270 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4), Color.Gray);
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;
				case ViewFieldPosition.F:
				Tileset.Scale = new Vector2(0.5f, 0.5f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(342 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4), Color.Gray);
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;



				case ViewFieldPosition.I:
				Tileset.Scale = new Vector2(0.75f, 0.75f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(50 + DrawOffset.X / 2, 136 + DrawOffset.Y / 2));
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;
				case ViewFieldPosition.J:
				Tileset.Scale = new Vector2(0.75f, 0.75f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(180 + DrawOffset.X / 2, 136 + DrawOffset.Y / 2));
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;
				case ViewFieldPosition.K:
				Tileset.Scale = new Vector2(0.75f, 0.75f);
				batch.DrawTile(Tileset, GetTileID(direction), new Point(300 + DrawOffset.X / 2, 136 + DrawOffset.Y / 2));
				Tileset.Scale = new Vector2(1.0f, 1.0f);
				break;


				case ViewFieldPosition.M:
				batch.DrawTile(Tileset, GetTileID(direction), new Point(-20 + DrawOffset.X, 190));
				break;
				case ViewFieldPosition.N:
				batch.DrawTile(Tileset, GetTileID(direction), new Point(180 + DrawOffset.X, 190));
				break;
				case ViewFieldPosition.O:
				batch.DrawTile(Tileset, GetTileID(direction), new Point(370 + DrawOffset.X, 190));
				break;
			}




			// finish special mode
			if (LastAttack != null && LastAttack.Time + TimeSpan.FromSeconds(0.25) > DateTime.Now)
			{
				Display.TexEnv = mode;
				Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			}
		}

		#endregion


		/// <summary>
		/// Heal 
		/// </summary>
		public void Heal()
		{
			if (!CanHeal)
				return;

			HitPoint.Current += 2;

		}


		/// <summary>
		/// Checks map between monster and teh team to see if can throw projectiles
		/// </summary>
		/// <param name="target">Target location</param>
		/// <returns>True if possible</returns>
		public bool CanDoRangeAttack(DungeonLocation target)
		{
			// Not in the same maze
			if (target.Maze != Location.Maze)
				return false;

			// x or y lined
			if (target.Position.X == Location.Position.X)
			{
				return true;
			}
			else if (target.Position.Y == Location.Position.Y)
			{
				return true;
			}
			else
				return false;

		}



		/// <summary>
		/// Checks if the monster can do close combat with the target
		/// </summary>
		/// <param name="target">Target location</param>
		/// <returns>True if possible</returns>
		public bool CanDoCloseAttack(DungeonLocation target)
		{
			if (target.Maze != Location.Maze)
				return false;

			if (CanGetCloserTo(target))
				return false;

			Point dist = new Point(target.Position.X - Location.Position.X, target.Position.Y - Location.Position.Y);

			return Math.Abs(dist.X) <= 1 || Math.Abs(dist.Y) <= 1;
		}


		#region Helpers

		/// <summary>
		/// Returns the tile id of the monster depending the view point
		/// </summary>
		/// <param name="from">View direction of the viewer</param>
		/// <returns>ID of the tile to display the monster</returns>
		public int GetTileID(CardinalPoint point)
		{
			int[,] id = new int[4, 4];

			// g	f						LOOKING	  FROM	        VIEW	
			id[0, 0] = 5;			//		N			N			N
			id[0, 1] = 0;			//		N			S			S
			id[0, 2] = 3;			//		N			W			W
			id[0, 3] = 1;			//		N			E			E

			id[1, 0] = 0;			//		S			N			S
			id[1, 1] = 5;			//		S			S			N
			id[1, 2] = 1;			//		S			W			E
			id[1, 3] = 3;			//		S			E			W

			id[2, 0] = 1;			//		W			N			E
			id[2, 1] = 3;			//		W			S			W
			id[2, 2] = 5;			//		W			W			N
			id[2, 3] = 0;			//		W			E			S

			id[3, 0] = 3;			//		E			N			W
			id[3, 1] = 1;			//		E			S			E
			id[3, 2] = 0;			//		E			W			S
			id[3, 3] = 5;			//		E			E			N

			return id[(int)Location.Direction, (int)point] + Tile;
		}



		/// <summary>
		/// Gets if the monster can see the given location
		/// </summary>
		/// <returns>True if the point is in range of sight</returns>
		public bool CanSee(DungeonLocation location)
		{
			if (location == null)
				return false;

			// Not in the same maze
			if (Location.MazeName != location.MazeName)
				return false;

			// Not in sight zone
			if (!SightZone.Contains(location.Position))
				return false;

			// Check in straight line
			Point vector = new Point(Location.Position.X - location.Position.X, Location.Position.Y - location.Position.Y);
			while (!vector.IsEmpty)
			{
				if (vector.X > 0)
					vector.X--;
				else if (vector.X < 0)
					vector.X++;

				if (vector.Y > 0)
					vector.Y--;
				else if (vector.Y < 0)
					vector.Y++;

				Square block = Location.Maze.GetBlock(new Point(location.Position.X + vector.X, Location.Position.Y + vector.Y));
				if (block.IsWall)
					return false;
			}


			// Location is visible
			return true;
		}


		/// <summary>
		/// Can the monster detect a presence near him
		/// </summary>
		/// <param name="location">Location to detect</param>
		/// <returns>True if the monster can fell the location</returns>
		public bool CanDetect(DungeonLocation location)
		{
			if (location == null)
				return false;

			// Not in the same maze
			if (Location.MazeName != location.MazeName)
				return false;

			// Not in sight zone
			if (!DetectionZone.Contains(location.Position))
				return false;


			return true;
		}


		/// <summary>
		/// Does the monster facing a given direction
		/// </summary>
		/// <param name="direction">Direction to check</param>
		/// <returns>True if facing, or false</returns>
		public bool IsFacing(CardinalPoint direction)
		{
			return Location.Compass.IsFacing(direction);
		}



		/// <summary>
		/// Turns the monster to a given direction
		/// </summary>
		/// <param name="direction">Direction to face to</param>
		/// <returns>True if the monster facing the direction</returns>
		public bool TurnTo(CardinalPoint direction)
		{
			if (!CanMove)
				return false;

			Location.Direction = direction;
			LastAction = DateTime.Now;

			return true;
		}


		/// <summary>
		/// Turns the monster to a given location
		/// </summary>
		/// <param name="location">Location to face to</param>
		/// <returns>True if facing the location, or false</returns>
		public bool TurnTo(DungeonLocation location)
		{
			// Still facing
			if (Location.IsFacing(location))
				return true;

			// Face to the location
			Location.Compass.Rotate(CompassRotation.Rotate90);


			// Does the monster face the direction
			return location.IsFacing(location);
		}


		#endregion



		#region I/O


		/// <summary>
		/// Loads monster definition
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name.ToLower() != "monster")
				return false;

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				string value = "";
				if (node.Attributes["value"] != null)
					value = node.Attributes["value"].Value;

				switch (node.Name.ToLower())
				{
					case "location":
					{
						if (Location != null)
							Location.Load(node);
					}
					break;

					case "tiles":
					{
						TileSetName = node.Attributes["name"].Value;
						Tile = int.Parse(node.Attributes["id"].Value);
					}
					break;

					case "pocket":
					{
						ItemsInPocket.Add(node.Attributes["item"].Value);
					}
					break;

					case "script":
					{
						Script = new ScriptInterface<IMonster>();
						Script.Load(node);
					}
					break;

					case "damage":
					{
						DamageDice.Load(node);
					}
					break;

					case "hitdice":
					{
						HitDice.Load(node);
					}
					break;

					case "armorclass":
					{
						ArmorClass = int.Parse(value);
					}
					break;

					case "castinglevel":
					{
						MagicCastingLevel = int.Parse(value);
					}
					break;

					case "stealrate":
					{
						StealRate = float.Parse(value);
					}
					break;

					case "pickuprate":
					{
						PickupRate = float.Parse(value);
					}
					break;

					case "baseattack":
					{
						BaseAttack = int.Parse(value);
					}
					break;

					case "sightrange":
					{
						SightRange = byte.Parse(value);
					}
					break;

					case "behaviour":
					{
						DefaultBehaviour = (MonsterBehaviour) Enum.Parse(typeof(MonsterBehaviour), node.Attributes["default"].Value);
						CurrentBehaviour = (MonsterBehaviour) Enum.Parse(typeof(MonsterBehaviour), node.Attributes["current"].Value);
					}
					break;

					case "nonmaterial":
					{
						NonMaterial = bool.Parse(value);
					}
					break;

					case "poisonimmunity":
					{
						PoisonImmunity = bool.Parse(value);
					}
					break;

					case "cansseinvisible":
					{
						CanSeeInvisible = bool.Parse(value);
					}
					break;

					case "backrowattack":
					{
						BackRowAttack = bool.Parse(value);
					}
					break;

					case "teleports":
					{
						Teleports = bool.Parse(value);
					}
					break;

					case "usestairs":
					{
						UseStairs = bool.Parse(value);
					}
					break;

					case "fillsquare":
					{
						FillSquare = bool.Parse(value);
					}
					break;

					case "flees":
					{
						FleesAfterAttack = bool.Parse(value);
					}
					break;

					case "drains":
					{
						HasDrainMagic = bool.Parse(value);
					}
					break;

					case "heals":
					{
						HasHealMagic = bool.Parse(value);
					}
					break;

					case "throwweapons":
					{
						ThrowWeapons = bool.Parse(value);
					}
					break;

					case "flying":
					{
						Flying = bool.Parse(value);
					}
					break;

					case "smartai":
					{
						SmartAI = bool.Parse(value);
					}
					break;

					case "reward":
					{
						Reward = int.Parse(value);
					}
					break;

					default:
					{
						base.Load(node);
					}
					break;
				}

			}

			return true;
		}


		/// <summary>
		/// Saves monster definition
		/// </summary>
		/// <param name="writer">XmlWriter handle</param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("monster");
			writer.WriteAttributeString("name", Name);
			writer.WriteAttributeString("position", Location.SquarePosition.ToString());

			base.Save(writer);

			if (Script != null)
				Script.Save("script", writer);

			if (Location != null)
				Location.Save("location", writer);

			DamageDice.Save("damage", writer);
			HitDice.Save("hitdice", writer);

			writer.WriteStartElement("tiles");
			writer.WriteAttributeString("name", TileSetName);
			writer.WriteAttributeString("id", Tile.ToString());
			writer.WriteEndElement();

			foreach (string name in ItemsInPocket)
			{
				writer.WriteStartElement("pocket");
				writer.WriteAttributeString("item", name);
				writer.WriteEndElement();
			}

			writer.WriteStartElement("armorclass");
			writer.WriteAttributeString("value", ArmorClass.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("baseattack");
			writer.WriteAttributeString("value", BaseAttack.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("sightrange");
			writer.WriteAttributeString("value", SightRange.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("reward");
			writer.WriteAttributeString("value", Reward.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("sound");
			writer.WriteAttributeString("event", "hit");
			writer.WriteAttributeString("name", AttackSoundName);
			writer.WriteEndElement();

			writer.WriteStartElement("sound");
			writer.WriteAttributeString("event", "hurt");
			writer.WriteAttributeString("name", HurtSoundName);
			writer.WriteEndElement();

			writer.WriteStartElement("sound");
			writer.WriteAttributeString("event", "walk");
			writer.WriteAttributeString("name", MoveSoundName);
			writer.WriteEndElement();

			writer.WriteStartElement("sound");
			writer.WriteAttributeString("event", "die");
			writer.WriteAttributeString("name", DieSoundName);
			writer.WriteEndElement();

			writer.WriteStartElement("behaviour");
			writer.WriteAttributeString("default", DefaultBehaviour.ToString());
			writer.WriteAttributeString("current", CurrentBehaviour.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("fillsquare");
			writer.WriteAttributeString("value", FillSquare.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("flees");
			writer.WriteAttributeString("value", FleesAfterAttack.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("drains");
			writer.WriteAttributeString("value", HasDrainMagic.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("heals");
			writer.WriteAttributeString("value", HasHealMagic.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("castinglevel");
			writer.WriteAttributeString("value", MagicCastingLevel.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("throwweapons");
			writer.WriteAttributeString("value", ThrowWeapons.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("usestairs");
			writer.WriteAttributeString("value", UseStairs.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("flying");
			writer.WriteAttributeString("value", Flying.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("smartai");
			writer.WriteAttributeString("value", SmartAI.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("teleports");
			writer.WriteAttributeString("value", Teleports.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("pickuprate");
			writer.WriteAttributeString("value", PickupRate.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("stealrate");
			writer.WriteAttributeString("value", StealRate.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("backrowattack");
			writer.WriteAttributeString("value", BackRowAttack.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("cansseinvisible");
			writer.WriteAttributeString("value", CanSeeInvisible.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("poisonimmunity");
			writer.WriteAttributeString("value", PoisonImmunity.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("nonmaterial");
			writer.WriteAttributeString("value", NonMaterial.ToString());
			writer.WriteEndElement();


			writer.WriteEndElement();

			return true;
		}



		#endregion


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(Name);
		}


		#region Properties


		/// <summary>
		/// Can use ammo
		/// </summary>
		public bool CanUseAmmo
		{
			get
			{
				return false;
			}
		}


		/// <summary>
		/// Does the monster can cast Heal magic
		/// </summary>
		public bool CanHeal
		{
			get
			{
				if (!HasHealMagic)
					return false;

				return true;
			}
		}



		/// <summary>
		/// Monster default behaviour
		/// </summary>
		public MonsterBehaviour DefaultBehaviour
		{
			get;
			set;
		}


		/// <summary>
		/// Monster behaviour
		/// </summary>
		public MonsterBehaviour CurrentBehaviour
		{
			get;
			private set;
		}




		/// <summary>
		/// Gets or sets a monster in the middle of a square
		/// </summary>
		public bool FillSquare
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the monster run away after an attack 
		/// </summary>
		public bool FleesAfterAttack
		{
			get;
			set;
		}


		/// <summary>
		/// State manager
		/// </summary>
		public StateManager StateManager
		{
			get;
			private set;
		}


		/// <summary>
		/// Is asset disposed
		/// </summary>
		public bool IsDisposed { get; private set; }


		/// <summary>
		/// Base attack bonus
		/// </summary>
		public int BaseAttack
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the monster uses of the drain spell
		/// </summary>
		public bool HasDrainMagic
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the monster uses of the heal spell
		/// </summary>
		public bool HasHealMagic
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the maximum power level the monster can use. 
		/// </summary>
		public int MagicCastingLevel
		{
			get;
			set;
		}


		/// <summary>
		/// Base save bonus
		/// </summary>
		public override int BaseSaveBonus
		{
			get
			{
				// HACK
				return 2; // +Experience.GetLevelFromXP(Reward) / 2;
			}
		}


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "monster";
			}
		}


		/// <summary>
		/// Armor class
		/// </summary>
		public override int ArmorClass
		{
			get;
			set;
		}


		/// <summary>
		/// Dice rolled to generate hit points
		/// </summary>
		public Dice HitDice
		{
			get;
			set;
		}


		/// <summary>
		/// Location of the monster
		/// </summary>
		public DungeonLocation Location
		{
			get;
			set;
		}


		/*
				/// <summary>
				/// Target location of the monster
				/// </summary>
				public DungeonLocation TargetLocation
				{
					get
					{
						DungeonLocation loc = new DungeonLocation(Location);

						//switch (TargetDirection)
						//{
						//    case CardinalPoint.North:
						//    loc.Position.Y -= TargetRange;
						//    break;
						//    case CardinalPoint.South:
						//    loc.Position.Y += TargetRange;
						//    break;
						//    case CardinalPoint.West:
						//    loc.Position.X -= TargetRange;
						//    break;
						//    case CardinalPoint.East:
						//    loc.Position.X += TargetRange;
						//    break;
						//}

						return loc;
					}
				}
		*/


		/// <summary>
		/// Name of the monster
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// TileSet of the monster
		/// </summary>
		[Browsable(false)]
		public TileSet Tileset
		{
			get;
			protected set;
		}


		/// <summary>
		/// Damage dice
		/// </summary>
		public Dice DamageDice
		{
			get;
			private set;
		}


		/// <summary>
		/// Holded items droped if killed
		/// </summary>
		public List<string> ItemsInPocket
		{
			get;
			set;
		}


		/// <summary>
		/// Offset when drawing the monster
		/// </summary>
		Point DrawOffset;


		/// <summary>
		/// Last time a draw offset occured
		/// </summary>
		DateTime LastDrawOffset;


		/// <summary>
		/// Duration of a draw offset.
		/// Smaller the duration is, more the feeling of a over excited monster is
		/// </summary>
		TimeSpan DrawOffsetDuration;


		/// <summary>
		/// Gets or sets to throw all unequipped weapons 
		/// </summary>
		public bool ThrowWeapons
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets if the monster can go up and down stairs.
		/// </summary>
		public bool UseStairs
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets if the monster is flying.
		/// </summary>
		public bool Flying
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets if the monster receives a variety of AI boosts
		/// (opening doors, using switch, cast spell...)
		/// </summary>
		public bool SmartAI
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets if the monster can teleport at will to a new square 
		/// on the same dungeon level within a 5 square radius. 
		/// </summary>
		public bool Teleports
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets how likely a monster is to pick up an item on the ground
		/// </summary>
		public float PickupRate
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets how often a monster steals from the party instead of attacking.
		/// </summary>
		public float StealRate
		{
			get;
			set;
		}


		/// <summary>
		/// Tile id for this monster
		/// </summary>
		[Category("TileSet")]
		public int Tile
		{
			get;
			set;
		}


		/// <summary>
		/// TileSet name to use for this monster
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string TileSetName
		{
			get;
			set;
		}


		/// <summary>
		/// The creature will tend to stay in the back row while other creatures 
		/// will step up to the front row when the party is near and they want to attack
		/// </summary>
		public bool BackRowAttack
		{
			get;
			set;
		}



		/// <summary>
		/// When sets, the creature can see the party even if it is under the effect of the 'Invisibility' spell. 
		/// </summary>
		public bool CanSeeInvisible
		{
			get;
			set;
		}


		/// <summary>
		/// Last time the monster made an action
		/// </summary>
		DateTime LastAction
		{
			get;
			set;
		}


		/// <summary>
		/// Does the monster can move
		/// </summary>
		public bool CanMove
		{
			get
			{
				if (DefaultBehaviour == MonsterBehaviour.FriendlyUnmoving || DefaultBehaviour == MonsterBehaviour.Guard)
					return false;

				if (LastAction + Speed > DateTime.Now)
					return false;

				return true;
			}
		}



		/// <summary>
		/// Experience gained by killing the entity
		/// </summary>
		public int Reward
		{
			get;
			set;
		}


		/// <summary>
		/// Defines the attack speed of the creature. 
		/// This is the minimum amount of time required between two attacks. 
		/// </summary>
		public TimeSpan AttackSpeed
		{
			get;
			set;
		}


		/// <summary>
		/// Maximum number of tiles between creature and party needed to see the party. 
		/// This applies only if the creature is facing the party.
		/// </summary>
		public int SightRange
		{
			get;
			set;
		}


		/// <summary>
		/// Sight zone
		/// </summary>
		public Rectangle SightZone
		{
			get
			{
				Rectangle zone = Rectangle.Empty;

				// Calculates the area view
				switch (Location.Direction)
				{
					case CardinalPoint.North:
					zone = new Rectangle(
						Location.Position.X - 1, Location.Position.Y - SightRange,
						3, SightRange);
					break;
					case CardinalPoint.South:
					zone = new Rectangle(
						Location.Position.X - 1, Location.Position.Y + 1,
						3, SightRange);
					break;
					case CardinalPoint.West:
					zone = new Rectangle(
						Location.Position.X - SightRange, Location.Position.Y - 1,
						SightRange, 3);
					break;
					case CardinalPoint.East:
					zone = new Rectangle(
						Location.Position.X + 1, Location.Position.Y - 1,
						SightRange, 3);
					break;
				}

				return zone;

			}
		}



		/// <summary>
		/// Detection zone
		/// </summary>
		public Rectangle DetectionZone
		{
			get
			{
				return new Rectangle(
				Location.Position.X - DetectionRange / 2,
				Location.Position.Y - DetectionRange / 2,
				DetectionRange, DetectionRange);
			}
		}


		/// <summary>
		/// Maximum number of tiles between creature and party needed to detect 
		/// and "turn" towards the party, perhaps to shoot a projectile. 
		/// This applies even if the creature is not facing the party.
		/// </summary>
		public byte DetectionRange
		{
			get;
			set;
		}


/*
		/// <summary>
		/// Resistance to magical spells like Fireball.
		/// Values range from 0 to 15. Value 15 means the creature is immune.
		/// </summary>
		public byte FireResistance
		{
			get;
			set;
		}
*/


		/// <summary>
		/// Resistance to magical spells involving poison.
		/// </summary>
		public bool PoisonImmunity
		{
			get;
			set;
		}



		/// <summary>
		/// The monster is non material. These creatures ignore normal attacks but take damage from some spells.
		/// These creatures can pass through all doors of any type. 
		/// </summary>
		public bool NonMaterial
		{
			get;
			set;
		}


		/// <summary>
		/// The amount of time while the attack graphic is displayed. 
		/// </summary>
		public TimeSpan AttackDisplayDuration;


		/// <summary>
		/// Script
		/// </summary>
		public ScriptInterface<IMonster> Script
		{
			get;
			private set;
		}


		#region Sounds

		/// <summary>
		/// Name of the sound when the monster attacks
		/// </summary>
		public string AttackSoundName
		{
			get;
			set;
		}

		/// <summary>
		/// Name of the sound when is hit by an attack
		/// </summary>
		public string HurtSoundName
		{
			get;
			set;
		}

		/// <summary>
		/// Name of the sound when the monster dies
		/// </summary>
		public string DieSoundName
		{
			get;
			set;
		}

		/// <summary>
		/// Name of the sound when the monster move
		/// </summary>
		public string MoveSoundName
		{
			get;
			set;
		}

		AudioSample AttackSound;
		AudioSample HurtSound;
		AudioSample DieSound;
		AudioSample MoveSound;

		#endregion

		#endregion
	}

/*
	/// <summary>
	/// A size modifier applies to the creature’s Armor Class (AC) and attack bonus,
	/// as well as to certain skills. A creature’s size also determines how far 
	/// it can reach to make a melee attack and how much space it occupies in a block.
	/// </summary>
	public enum MonsterSize
	{
		/// <summary>
		/// means there can be 4 creatures per tile
		/// </summary>
		Small,


		/// <summary>
		/// means there can be 2 creatures per tile
		/// </summary>
		Normal,

		/// <summary>
		/// means there can be only one creature per tile
		/// </summary>
		Big
	}


	/// <summary>
	/// The bigger an opponent is, the easier it is to hit in combat. 
	/// The smaller it is, the harder it is to hit.
	/// </summary>
	public enum MonsterSizeModifier
	{
		/// <summary>
		/// Blue whale
		/// </summary>
		Colossal = -8,

		/// <summary>
		/// Gray whale
		/// </summary>
		Gargantuan = -4,

		/// <summary>
		/// Elephant
		/// </summary>
		Huge = -2,

		/// <summary>
		/// Lion
		/// </summary>
		Large = -1,

		/// <summary>
		/// Human
		/// </summary>
		Medium = 0,

		/// <summary>
		/// German shepherd
		/// </summary>
		Small = 1,

		/// <summary>
		/// House cat
		/// </summary>
		Tiny = 2,

		/// <summary>
		/// Rat
		/// </summary>
		Diminutive = 4,

		/// <summary>
		/// Horsefly
		/// </summary>
		Fine = 8,
	}




	/// <summary>
	/// This value defines what kind of attack the creature uses. 
	/// This value affects how the inflicted damage is computed.
	/// </summary>
	public enum AttackType
	{
		/// <summary>
		/// The creature does not attack champions.
		/// </summary>
		None,


		/// <summary>
		///  the attacked champion's 'Anti-Fire' characteristic is used to compute damage. 
		/// </summary>
		Fire,


		/// <summary>
		/// The 'Armor Strength' value of the attacked champion's armor is used to compute damage.
		/// </summary>
		Critical,


		/// <summary>
		/// The 'Armor Strength' value of the attacked champion's armor is used to compute damage. 
		/// </summary>
		Normal,

		/// <summary>
		/// The 'Sharp resistance' value of the attacked champion's armor is used to compute damage. 
		/// </summary>
		Sharp,


		/// <summary>
		///  the attacked champion's 'Anti-Magic' characteristic is used to compute damage. 
		/// </summary>
		Magic,


		/// <summary>
		///  the attacked champion's 'Wisdom' characteristic is used to compute damage. 
		/// </summary>
		Psychic,

	}

*/


	/// <summary>
	/// Monster artificial intelligence
	/// </summary>
	public enum MonsterBehaviour
	{
		/// <summary>
		/// Go right up to the party and attack
		/// </summary>
		Aggressive,

		/// <summary>
		/// Try to keep distance from party and use ranged attacks (spells and thrown items)
		/// </summary>
		RangeAttack,

		/// <summary>
		/// Avoid the party, only attacking when cornered
		/// </summary>
		RunAway,

		/// <summary>
		/// Stay in one square, unmoving 
		/// </summary>
		Guard,

		/// <summary>
		/// Invincible, never attacks, wanders around randomly
		/// </summary>
		Friendly,

		/// <summary>
		/// Invincible, never attacks or moves
		/// </summary>
		FriendlyUnmoving,
	}
}

