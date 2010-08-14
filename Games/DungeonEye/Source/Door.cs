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

namespace DungeonEye
{

	/// <summary>
	/// Door in a maze
	/// </summary>
	public class Door : IDisposable
	{


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (TileSet != null)
				TileSet.Dispose();

			if (OpenSound != null)
				OpenSound.Dispose();

			if (CloseSound != null)
				CloseSound.Dispose();
		}


		/// <summary>
		/// Initializes doors
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			// If no tileset, then load it
			TileSet = ResourceManager.CreateSharedAsset<TileSet>("Doors", "Doors");
			if (TileSet == null)
			{
				Trace.WriteLine("Unable to load TileSet for door");
				return false;
			}
            TileSet.Scale = new Vector2(2.0f, 2.0f);


			// Zone of the button to open/close the door
			Button = new Rectangle(254, 70, 20, 28);

			// Sounds
			OpenSound = ResourceManager.CreateSharedAsset<Audio>("door open");
			CloseSound = ResourceManager.CreateSharedAsset<Audio>("door close");

			return true;
		}



		/// <summary>
		/// Draw the door
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="location">Offset location for drawing</param>
		/// <param name="distance">Distance of the door from the view point</param>
		/// <param name="direction">View point of the team</param>
		public virtual void Draw(SpriteBatch batch, Point location, ViewFieldPosition distance, CardinalPoint direction)
		{

			switch (Type)
			{
				case DoorType.Grid:
					DrawGridDoor(batch, location, distance);
				break;
				case DoorType.Iron:
				DrawIronDoor(batch, location, distance);
				break;
				case DoorType.Monster:
				DrawMonsterDoor(batch, location, distance);
				break;
				case DoorType.Spider:
				DrawSpiderDoor(batch, location, distance);
				break;
				case DoorType.Stone:
				DrawStoneDoor(batch, location, distance);
				break;
				case DoorType.Eye:
				DrawEyeDoor(batch, location, distance);
				break;
			}


		//	Display.Rectangle(Button, false);

		}


		/// <summary>
		/// Update the door state
		/// </summary>
		/// <param name="time">Game time</param>
		public virtual void Update(GameTime time)
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
		/// <param name="team">Team handle</param>
		/// <param name="location">Location of the click</param>
		public virtual void OnClick(Team team, Point location)
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

		}


		/// <summary>
		/// Opens the door
		/// </summary>
		public void Open()
		{
			State = DoorState.Opening;
			OpenSound.Play();
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
			CloseSound.Play();
		}


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
                    batch.End();

					Display.PopScissor();

                    batch.Begin();

					if (HasButton)
						batch.DrawTile(TileSet, 33, new Point(254, 72));
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

					Display.RenderState.Scissor = true;
					Display.PushScissor(new Rectangle(location.X + 14, location.Y, 100, 94));

                    batch.Begin();
					batch.DrawTile(TileSet, 7, new Point(location.X + VPosition * 2, location.Y));
					batch.DrawTile(TileSet, 7, new Point(location.X - VPosition * 2 + 64, location.Y), Color.White, 0.0f, SpriteEffects.FlipHorizontally, 0.0f);
                    batch.End();

					Display.PopScissor();

                    batch.Begin();
					if (HasButton)
						batch.DrawTile(TileSet, 34, new Point(234, 80));
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

					case "button":
					{
						HasButton = true;
					}
					break;

					case "toforce":
					{
						ToForce = true;
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
		public bool Save(XmlWriter writer)
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

			// Has to be forced
			if (ToForce)
			{
				writer.WriteStartElement("toforce");
				writer.WriteEndElement();
			}

			if (HasButton)
			{
				writer.WriteStartElement("button");
				writer.WriteEndElement();
			}

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
		[Browsable(false)]
		public TileSet TileSet
		{
			get;
			private set;
		}


		/// <summary>
		/// Is the door opened
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
		public bool IsBlocking
		{
			get
			{
				return State != DoorState.Opened;		
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
			get;
			set;
		}

		/// <summary>
		/// Bashable by chopping
		/// </summary>
		public bool IsBashable
		{
			get;
			set;
		}


		/// <summary>
		/// Bashable by chopping
		/// </summary>
		public bool DestroyableByFireball
		{
			get;
			set;
		}

		/// <summary>
		/// Does the door has to be forced to be opened
		/// </summary>
		public bool ToForce
		{
			get;
			set;
		}

		/// <summary>
		/// Vertical position of the door in the animation
		/// </summary>
		int VPosition;

		/// <summary>
		/// Location of the button on the door
		/// </summary>
	//	Point ButtonPoint;


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
		public bool ThrownItemsPassThrough 
		{
			get;
			set;
		}



		/// <summary>
		/// Creatures can see the party through the door
		/// </summary>
		public bool CreaturesCanSeeThrough
		{
			get;
			set;
		}


		/// <summary>
		/// Zone of the button
		/// </summary>
		Rectangle Button;

		/// <summary>
		/// 
		/// </summary>
		Audio OpenSound;


		/// <summary>
		/// 
		/// </summary>
		Audio CloseSound;

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

}
