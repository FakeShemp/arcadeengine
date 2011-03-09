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
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using System.ComponentModel;
using System;
using ArcEngine.Asset;
using ArcEngine.Audio;
using System.Text;

namespace DungeonEye
{

	/// <summary>
	/// Door in a square 
	/// </summary>
	public class Door : SquareActor
	{
		/// <summary>
		/// Initializes doors
		/// </summary>
		/// <returns></returns>
		public Door(Square square) : base(square)
		{
			// Zone of the button to open/close the door
			Button = new Rectangle(254, 70, 20, 28);

			// Sounds
			OpenSound = ResourceManager.LockSharedAsset<AudioSample>("door open");
			CloseSound = ResourceManager.LockSharedAsset<AudioSample>("door close");


			AcceptItems = false;
			Speed = TimeSpan.FromSeconds(1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(State + " " + Type + " door ");
			if (IsBreakable)
				sb.Append("(breakable) ");
			sb.Append("(key ....) ");
			


			return sb.ToString();
		}


		/// <summary>
		/// Draw the door
		/// </summary>
		/// <param name="batch">Spritebatch to use</param>
		/// <param name="field">View field</param>
		/// <param name="position">Position in the view filed</param>
		/// <param name="view">Looking direction of the team</param>
		public override void Draw(SpriteBatch batch, ViewField field, ViewFieldPosition position, CardinalPoint view)
		{
			if (TileSet == null)
				return;

			TileDrawing td = null;
			TileSet overlay = Square.Maze.OverlayTileset;
			TileSet wall = Square.Maze.WallTileset;


			// Under the door, draw sides
			if (field.GetBlock(ViewFieldPosition.N).IsWall && position == ViewFieldPosition.Team)
			{
				td = DisplayCoordinates.GetDoor(ViewFieldPosition.Team);
				if (td != null)
					batch.DrawTile(overlay, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
			}

			// Draw the door
			else if (((field.Maze.IsDoorNorthSouth(Square.Location) && (view == CardinalPoint.North || view == CardinalPoint.South)) ||
					(!field.Maze.IsDoorNorthSouth(Square.Location) && (view == CardinalPoint.East || view == CardinalPoint.West))) &&
					position != ViewFieldPosition.Team)
			{
				td = DisplayCoordinates.GetDoor(position);
				if (td != null)
				{
					batch.DrawTile(wall, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
					//block.Door.Draw(batch, td.Location, position, view);


					switch (Type)
					{
						case DoorType.Grid:
							DrawGridDoor(batch, td.Location, position);
						break;
						case DoorType.Iron:
							DrawIronDoor(batch, td.Location, position);
						break;
						case DoorType.Monster:
							DrawMonsterDoor(batch, td.Location, position);
						break;
						case DoorType.Spider:
							DrawSpiderDoor(batch, td.Location, position);
						break;
						case DoorType.Stone:
							DrawStoneDoor(batch, td.Location, position);
						break;
						case DoorType.Eye:
							DrawEyeDoor(batch, td.Location, position);
						break;
					}
				}
			}


		}


		/// <summary>
		/// Update the door state
		/// </summary>
		/// <param name="time">Game time</param>
		public override void Update(GameTime time)
		{
			// Opening
			if (State == DoorState.Opening)
			{
				VPosition--;

				if (VPosition <= -30)
				{
					State = DoorState.Opened;
				}
			}


			// Closing
			else if (State == DoorState.Closing)
			{
				VPosition++;

				if (VPosition >= 0)
				{
					State = DoorState.Closed;
				}
			}

		}


		/// <summary>
		/// Mouse click on the door
		/// </summary>
		/// <param name="location">Location of the click</param>
		/// <param name="side">Wall side</param>
		public override bool OnClick(Point location, CardinalPoint side)
		{
			// Button
			if (HasButton && Button.Contains(location))
			{
				if (State == DoorState.Closed || State == DoorState.Closing)
					Open();
				else if (State == DoorState.Opened || State == DoorState.Opening)
					Close();
			}


			// Try to force the door


			return true;
		}


		/// <summary>
		/// Opens the door
		/// </summary>
		public void Open()
		{
			State = DoorState.Opening;
		//	Audio.PlaySample(0, OpenSound);
		}


		/// <summary>
		/// Closes the door
		/// </summary>
		public void Close()
		{
			// Check if a monster or the team is under the door
			//if (Maze.GetMonsterAt(Location).Count > 0 || (DungeonEye.Team.Maze == Maze && DungeonEye.Team.Location == Location))
			//	return;

			State = DoorState.Closing;
		//	Audio.PlaySample(0, CloseSound);
		}


		#region Events


		/// <summary>
		/// Opens the door
		/// </summary>
		public override void Activate()
		{
			Open();
		}


		/// <summary>
		/// Closes the door
		/// </summary>
		public override void Deactivate()
		{
			Close();
		} 


		#endregion


		#region Door type


		/// <summary>
        /// Draw the door with a scissor test
        /// </summary>
        /// <param name="batch">Spritebatch to use</param>
        /// <param name="tileset">Tileset to use</param>
        /// <param name="id">ID of the tile</param>
        /// <param name="location">Location of the tile on the screen</param>
        /// <param name="scissor">Scissor zone</param>
        void InternalDraw(SpriteBatch batch, TileSet tileset, int id, Point location, Rectangle scissor)
        {
            if (batch == null)
                return;

            batch.End();

			Display.PushScissor(scissor);

            batch.Begin();
            batch.DrawTile(TileSet, id, location);
            batch.End();

			Display.PopScissor();

            batch.Begin();
        }


		/// <summary>
		/// 
		/// </summary>
        /// <param name="batch"></param>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawEyeDoor(SpriteBatch batch, Point location, ViewFieldPosition distance)
		{

			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					location.Offset(56, 14);
                    InternalDraw(batch, TileSet, 15, 
                        new Point(location.X, location.Y + VPosition * 5), 
                        new Rectangle(location, new Size(144, 150)));

					if (HasButton)
						batch.DrawTile(TileSet, 41, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					location.Offset(32, 12);
                    InternalDraw(batch, TileSet, 16, 
                        new Point(location.X, location.Y + VPosition * 3), 
                        new Rectangle(location, new Size(102, 96)));

					if (HasButton)
						batch.DrawTile(TileSet, 42, new Point(234, 80));
				}
				break;

				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
				{
					location.Offset(14, 4);
                    InternalDraw(batch, TileSet, 17,
                         new Point(location.X, location.Y + VPosition * 2),
                         new Rectangle(location, new Size(64, 58)));
				}
				break;

			}
		}


		/// <summary>
		/// Draws the grid door
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawGridDoor(SpriteBatch batch, Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
                    //Display.RenderState.Scissor = true;
					location.Offset(54, 16);
                    //Display.ScissorZone = new Rectangle(location, new Size(148, 142));
                    //location.Offset(0, VPosition * 5);
                    //batch.DrawTile(TileSet, 9, location);
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 9,
                         new Point(location.X, location.Y + VPosition * 5),
                         new Rectangle(location, new Size(148, 142)));

					if (HasButton)
						batch.DrawTile(TileSet, 30, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
                    //Display.RenderState.Scissor = true;
                    location.Offset(28, 8);
                    //Display.ScissorZone = new Rectangle(location, new Size(104, 86));
                    //location.Offset(0, VPosition * 3);
                    //batch.DrawTile(TileSet, 10, location);
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 10,
                         new Point(location.X, location.Y + VPosition * 3),
                         new Rectangle(location, new Size(104, 86)));

					if (HasButton)
						batch.DrawTile(TileSet, 31, new Point(234, 80));
				}
				break;

				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
				{
                    //Display.RenderState.Scissor = true;
					location.Offset(16, 4);
                    //Display.ScissorZone = new Rectangle(location, new Size(64, 58));
                    //location.Offset(0, VPosition * 2);
                    //batch.DrawTile(TileSet, 11, location);
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 11,
                         new Point(location.X, location.Y + VPosition * 2),
                         new Rectangle(location, new Size(64, 58)));

				}
				break;

			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawIronDoor(SpriteBatch batch, Point location, ViewFieldPosition distance)
		{

			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					//Display.RenderState.Scissor = true;
					location.Offset(54, 16);
                    //if (State != DoorState.Opened)
                    //{
                    //    Display.ScissorZone = new Rectangle(location, new Size(148, 144));
                    //    Display.RenderState.Scissor = true;
                    //    batch.DrawTile(TileSet, 18, new Point(location.X, location.Y + VPosition * 5));
                    //}
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 18,
                     new Point(location.X, location.Y + VPosition * 5),
                     new Rectangle(location, new Size(148, 144)));

					if (HasButton)
						batch.DrawTile(TileSet, 30, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
                    //Display.RenderState.Scissor = true;
					location.Offset(28, 8);
                    //Display.ScissorZone = new Rectangle(location, new Size(104, 96));
                    //Display.RenderState.Scissor = true;
                    //batch.DrawTile(TileSet, 19, new Point(location.X, location.Y + VPosition * 3));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 19,
                        new Point(location.X, location.Y + VPosition * 3),
                        new Rectangle(location, new Size(104, 96)));

					if (HasButton)
						batch.DrawTile(TileSet, 31, new Point(234, 80));
				}
				break;

				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
				{
                    //Display.RenderState.Scissor = true;
					location.Offset(14, 4);
                    //Display.ScissorZone = new Rectangle(location, new Size(64, 58));
                    //Display.RenderState.Scissor = true;
                    //batch.DrawTile(TileSet, 20, new Point(location.X, location.Y + VPosition * 2));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 20,
                         new Point(location.X, location.Y + VPosition * 2),
                         new Rectangle(location, new Size(64, 58)));

				}
				break;

			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawMonsterDoor(SpriteBatch batch, Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					location.Offset(56, 14);
                    //Display.RenderState.Scissor = true;
                    //Display.ScissorZone = new Rectangle(location, new Size(144, 142));
                    //batch.DrawTile(TileSet, 0, new Point(location.X, location.Y + VPosition * 5));
                    //batch.DrawTile(TileSet, 1, new Point(location.X, location.Y + 86 + VPosition * -2));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 0,
                         new Point(location.X, location.Y + VPosition * 5),
                         new Rectangle(location, new Size(144, 142)));

                    InternalDraw(batch, TileSet, 1,
                         new Point(location.X, location.Y + 86 + VPosition * -2),
                         new Rectangle(location, new Size(144, 142)));

					if (HasButton)
						batch.DrawTile(TileSet, 36, new Point(260, 72));


				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					location.Offset(28, 8);
                    //Display.RenderState.Scissor = true;
                    //Display.ScissorZone = new Rectangle(location, new Size(104, 96));
                    //batch.DrawTile(TileSet, 2, new Point(location.X, location.Y + VPosition * 3));
                    //batch.DrawTile(TileSet, 3, new Point(location.X, location.Y + 56 + VPosition * -1));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 2,
                         new Point(location.X, location.Y + VPosition * 3),
                         new Rectangle(location, new Size(104, 96)));

                    InternalDraw(batch, TileSet, 3,
                         new Point(location.X, location.Y + 56 - VPosition),
                         new Rectangle(location, new Size(104, 96)));


					if (HasButton)
						batch.DrawTile(TileSet, 37, new Point(234, 80));

				}
				break;

				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
				{
					location.Offset(14, 4);
                    //Display.RenderState.Scissor = true;
                    //Display.ScissorZone = new Rectangle(location, new Size(68, 60));
                    //batch.DrawTile(TileSet, 4, new Point(location.X, location.Y + VPosition * 2));
                    //batch.DrawTile(TileSet, 5, new Point(location.X, location.Y + 36 + VPosition * -1));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 4,
                         new Point(location.X, location.Y + VPosition * 2),
                         new Rectangle(location, new Size(68, 60)));

                    InternalDraw(batch, TileSet, 5,
                         new Point(location.X, location.Y + 36 - VPosition),
                         new Rectangle(location, new Size(68, 60)));


				}
				break;

			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawSpiderDoor(SpriteBatch batch, Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					location.Offset(32, 24);
                    //Display.RenderState.Scissor = true;
                    //Display.ScissorZone = new Rectangle(location, new Size(192, 142));
                    //batch.DrawTile(TileSet, 12, new Point(location.X, location.Y + VPosition * 5));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 12,
                         new Point(location.X, location.Y + VPosition * 5),
                         new Rectangle(location, new Size(192, 142)));

					if (HasButton)
						batch.DrawTile(TileSet, 39, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					location.Offset(32, 18);
                    //Display.RenderState.Scissor = true;
                    //Display.ScissorZone = new Rectangle(location, new Size(96, 82));
                    //batch.DrawTile(TileSet, 13, new Point(location.X, location.Y + VPosition * 3));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 13,
                         new Point(location.X, location.Y + VPosition * 3),
                         new Rectangle(location, new Size(96, 82)));


					if (HasButton)
						batch.DrawTile(TileSet, 40, new Point(234, 80));
				}
				break;

				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
				{
					location.Offset(16, 10);
                    //Display.ScissorZone = new Rectangle(location, new Size(64, 54));
                    //Display.RenderState.Scissor = true;
                    //batch.DrawTile(TileSet, 14, new Point(location.X, location.Y + VPosition * 2));
                    //Display.RenderState.Scissor = false;

                    InternalDraw(batch, TileSet, 14,
                        new Point(location.X, location.Y + VPosition * 2),
                        new Rectangle(location, new Size(64, 54)));

				}
				break;

			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawStoneDoor(SpriteBatch batch, Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
                    batch.End();

					location.Offset(32, 16);

                    Display.PushScissor(new Rectangle(location.X + 26, location.Y, 140, 142));

                    batch.Begin();
                    batch.DrawTile(TileSet, 6, new Point(location.X + VPosition * 3, location.Y));
                    batch.DrawTile(TileSet, 6, new Point(location.X - VPosition * 3 + 96, location.Y), Color.White, 0.0f, SpriteEffects.FlipHorizontally, 0.0f);
					
					if (HasButton)
						batch.DrawTile(TileSet, 33, new Point(location.X + 102 - VPosition * 3, 72 + 20));
                    
					batch.End();

					Display.PopScissor();

                    batch.Begin();

				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
                    batch.End();


					location.Offset(16, 8);

				//	Display.RenderState.Scissor = true;
					Display.PushScissor(new Rectangle(location.X + 14, location.Y, 100, 94));

                    batch.Begin();
					batch.DrawTile(TileSet, 7, new Point(location.X + VPosition * 2, location.Y));
					batch.DrawTile(TileSet, 7, new Point(location.X - VPosition * 2 + 64, location.Y), Color.White, 0.0f, SpriteEffects.FlipHorizontally, 0.0f);

					if (HasButton)
						batch.DrawTile(TileSet, 34, new Point(234, 80));
        
					batch.End();

					Display.PopScissor();

                    batch.Begin();
				}
				break;

				case ViewFieldPosition.A:
				case ViewFieldPosition.B:
				case ViewFieldPosition.C:
				case ViewFieldPosition.D:
				case ViewFieldPosition.E:
				case ViewFieldPosition.F:
				case ViewFieldPosition.G:
				{
                    batch.End();

					location.Offset(8, 4);

					Display.PushScissor(new Rectangle(location.X + 10, location.Y, 60, 58));

                    batch.Begin();
					batch.DrawTile(TileSet, 8, new Point(location.X + VPosition, location.Y));
					batch.DrawTile(TileSet, 8, new Point(location.X - VPosition + 40, location.Y), Color.White, 0.0f, SpriteEffects.FlipHorizontally, 0.0f);
                    batch.End();

					Display.PopScissor();

                    batch.Begin();
				}
				break;
			}

		}


		#endregion


		#region IO

		/// <summary>
		/// Loads the door's definition from a bank
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;
			

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "type":
					{
						Type = (DoorType)Enum.Parse(typeof(DoorType), node.Attributes["value"].Value, true);
					}
					break;

					case "state":
					{
						State = (DoorState)Enum.Parse(typeof(DoorState), node.Attributes["value"].Value, true);
						if (State == DoorState.Opened)
							VPosition = -30;

					}
					break;

					case "isbreakable":
					{
						IsBreakable = Boolean.Parse(node.Attributes["value"].Value);
					}
					break;

					case "consumeitem":
					{
						ConsumeItem = Boolean.Parse(node.Attributes["value"].Value);
					}
					break;

					case "openitem":
					{
						OpenItemName = node.Attributes["value"].Value;
					}
					break;

					case "opentype":
					{
						OpenType = (DoorOpenType)Enum.Parse(typeof(DoorOpenType), node.Attributes["value"].Value, true);
					}
					break;

					case "picklock":
					{
						PickLock = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "speed":
					{
						Speed = TimeSpan.FromSeconds(int.Parse(node.Attributes["value"].Value));
					}
					break;

					case "strength":
					{
						Strength = int.Parse(node.Attributes["value"].Value);
					}
					break;



					default:
					{
						Trace.WriteLine("[Door] Load() : Unknown node \"" + node.Name + "\" found.");
					}
					break;
				}
			}



			return true;
		}


		/// <summary>
		/// Saves the door definition
		/// </summary>
		/// <param name="writer">XML writer handle</param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("door");

			// Type of door
			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			// State
			writer.WriteStartElement("state");
			writer.WriteAttributeString("value", State.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("isbreakable");
			writer.WriteAttributeString("value", IsBreakable.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("consumeitem");
			writer.WriteAttributeString("value", ConsumeItem.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("openitem");
			writer.WriteAttributeString("value", OpenItemName);
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("opentype");
			writer.WriteAttributeString("value", OpenType.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("picklock");
			writer.WriteAttributeString("value", PickLock.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("speed");
			writer.WriteAttributeString("value", Speed.TotalSeconds.ToString());
			writer.WriteEndElement();


			// 
			writer.WriteStartElement("strength");
			writer.WriteAttributeString("value", Strength.ToString());
			writer.WriteEndElement();


			writer.WriteEndElement();

			return true;
		}

		#endregion


		#region Helpers


		/// <summary>
		/// Returns true if the door can be displayed in the maze
		/// </summary>
		/// <param name="point">Looking direction</param>
		/// <returns>True if drawable</returns>
		public bool IsDrawable(CardinalPoint point)
		{

			//switch (point)
			//{
			//   case CardinalPoint.North:
			//   return IsNorthSouth;
			//   case CardinalPoint.South:
			//   return IsNorthSouth;
			//   case CardinalPoint.West:
			//   return! IsNorthSouth;
			//   case CardinalPoint.East:
			//   return !IsNorthSouth;
			//}

			return true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Tileset for the drawing
		/// </summary>
		TileSet TileSet
		{
			get
			{
				if (Square == null)
					return null;

				return Square.Maze.DoorTileset;
			}
		}


		/// <summary>
		/// The way the door opens
		/// </summary>
		public DoorOpenType OpenType
		{
			get;
			set;
		}


		/// <summary>
		/// Item needed to opens the door
		/// </summary>
		public string OpenItemName
		{
			get;
			set;
		}


		/// <summary>
		/// Door state
		/// </summary>
		public DoorState State
		{
			get;
			set;
		}


		/// <summary>
		/// Type of door
		/// </summary>
		public DoorType Type
		{
			get;
			set;
		}


		/// <summary>
		/// Gets if the door blocking the path
		/// </summary>
		[Browsable(false)]
		public override bool IsBlocking
		{
			get
			{
				return State != DoorState.Opened;		
			}
		}


		/// <summary>
		/// Picklock value
		/// </summary>
		public int PickLock
		{
			get;
			set;
		}


		/// <summary>
		/// Does items can pass though
		/// </summary>
		public override bool CanPassThrough
		{
			get
			{
				return !IsBlocking;
			}
		}


		/// <summary>
		/// Gets if the door is open
		/// </summary>
		public bool IsOpen
		{
			get
			{
				return State == DoorState.Opened;	
			}
		}


		/// <summary>
		/// Has the door a button to open it
		/// </summary>
		public bool HasButton
		{
			get
			{
				return OpenType == DoorOpenType.Button;
			}
		}


		/// <summary>
		/// Bashable by chopping
		/// </summary>
		public bool IsBreakable
		{
			get;
			set;
		}


		/// <summary>
		/// Vertical position of the door in the animation
		/// </summary>
		int VPosition;


		/// <summary>
		/// Opening / closing speed
		/// </summary>
		public TimeSpan Speed
		{
			get;
			set;
		}

		/// <summary>
		/// Thrown items can pass through
		/// </summary>
		public bool CanItemsPassThrough 
		{
			get
			{
				return State == DoorState.Broken || State == DoorState.Opened || Type == DoorType.Grid;
			}
		}



		/// <summary>
		/// Can see through the door
		/// </summary>
		public bool CanSeeThrough
		{
			get
			{
				if (CanItemsPassThrough)
					return true;

				if (Type == DoorType.Grid || Type == DoorType.Iron)
					return true;

				return false;
			}
		}


		/// <summary>
		/// Zone of the button
		/// </summary>
		Rectangle Button;

		/// <summary>
		/// 
		/// </summary>
		AudioSample OpenSound;


		/// <summary>
		/// 
		/// </summary>
		AudioSample CloseSound;


		/// <summary>
		/// Door strength. 
		/// This damage must be done in a single blow.
		/// Multiple weaker blows will have no effect.
		/// </summary>
		public int Strength
		{
			get;
			set;
		}


		/// <summary>
		/// Item disappears upon use
		/// </summary>
		public bool ConsumeItem
		{
			get;
			set;
		}
		#endregion
	}


	/// <summary>
	/// State of a door
	/// </summary>
	public enum DoorState
	{
		/// <summary>
		/// Door is closed
		/// </summary>
		Closed,

		/// <summary>
		/// Door is closing
		/// </summary>
		Closing,

		/// <summary>
		/// Door is opening
		/// </summary>
		Opening,

		/// <summary>
		/// Door is opened
		/// </summary>
		Opened,

		/// <summary>
		/// Door is broken
		/// </summary>
		Broken,

		/// <summary>
		/// Door got stucked
		/// </summary>
		Stuck,
	}


	/// <summary>
	/// Visual type of door
	/// </summary>
	public enum DoorType
	{
		/// <summary>
		/// 
		/// </summary>
		Grid,

		/// <summary>
		/// 
		/// </summary>
		Iron,

		/// <summary>
		/// 
		/// </summary>
		Monster,

		/// <summary>
		/// 
		/// </summary>
		Spider,

		/// <summary>
		/// 
		/// </summary>
		Stone,

		/// <summary>
		/// 
		/// </summary>
		Eye
	}


	/// <summary>
	/// The way a door can be opened
	/// </summary>
	public enum DoorOpenType
	{
		/// <summary>
		/// The door has a button
		/// </summary>
		Button,

		/// <summary>
		/// The door opens using an item
		/// </summary>
		Item,


		/// <summary>
		/// The door opens by an event
		/// </summary>
		Event,
	}
}
