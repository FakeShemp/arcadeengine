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
	public class Door
	{


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
			TileSet.Scale = new SizeF(2.0f, 2.0f);


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
		/// <param name="location">Offset location for drawing</param>
		/// <param name="distance">Distance of the door from the view point</param>
		/// <param name="direction">View point of the team</param>
		public virtual void Draw(Point location, ViewFieldPosition distance, CardinalPoint direction)
		{

			switch (Type)
			{
				case DoorType.Grid:
					DrawGridDoor(location, distance);
				break;
				case DoorType.Iron:
					DrawIronDoor(location, distance);
				break;
				case DoorType.Monster:
					DrawMonsterDoor(location, distance);
				break;
				case DoorType.Spider:
					DrawSpiderDoor(location, distance);
				break;
				case DoorType.Stone:
					DrawStoneDoor(location, distance);
				break;
				case DoorType.Eye:
					DrawEyeDoor(location, distance);
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
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawEyeDoor(Point location, ViewFieldPosition distance)
		{

			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					Display.Scissor = true;
					location.Offset(56, 14);
					Display.ScissorZone = new Rectangle(location, new Size(144, 150));
					TileSet.Draw(15, new Point(location.X, location.Y + VPosition * 5));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(41, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					Display.Scissor = true;
					location.Offset(32, 12);
					Display.ScissorZone = new Rectangle(location, new Size(102, 96));
					TileSet.Draw(16, new Point(location.X, location.Y + VPosition * 3));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(42, new Point(234, 80));
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
					Display.Scissor = true;
					location.Offset(14, 4);
					Display.ScissorZone = new Rectangle(location, new Size(64, 58));
					TileSet.Draw(17, new Point(location.X, location.Y + VPosition * 2));
					Display.Scissor = false;
				}
				break;

			}
		}


		/// <summary>
		/// Draws the grid door
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawGridDoor(Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					Display.Scissor = true;
					location.Offset(54, 16);
					Display.ScissorZone = new Rectangle(location, new Size(148, 142));
					location.Offset(0, VPosition * 5);
					TileSet.Draw(9, location);
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(30, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					Display.Scissor = true;
					location.Offset(28, 8);
					Display.ScissorZone = new Rectangle(location, new Size(104, 86));
					location.Offset(0, VPosition * 3);
					TileSet.Draw(10, location);
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(31, new Point(234, 80));
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
					Display.Scissor = true;
					location.Offset(16, 4);
					Display.ScissorZone = new Rectangle(location, new Size(64, 58));
					location.Offset(0, VPosition * 2);
					TileSet.Draw(11, location);
					Display.Scissor = false;
				}
				break;

			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawIronDoor(Point location, ViewFieldPosition distance)
		{

			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					Display.Scissor = true;
					location.Offset(54, 16);
					if (State != DoorState.Opened)
					{
						Display.ScissorZone = new Rectangle(location, new Size(148, 144));
						Display.Scissor = true;
						TileSet.Draw(18, new Point(location.X, location.Y + VPosition * 5));
					}
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(30, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					Display.Scissor = true;
					location.Offset(28, 8);
					Display.ScissorZone = new Rectangle(location, new Size(104, 96));
					Display.Scissor = true;
					TileSet.Draw(19, new Point(location.X, location.Y + VPosition * 3));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(31, new Point(234, 80));
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
					Display.Scissor = true;
					location.Offset(14, 4);
					Display.ScissorZone = new Rectangle(location, new Size(64, 58));
					Display.Scissor = true;
					TileSet.Draw(20, new Point(location.X, location.Y + VPosition * 2));
					Display.Scissor = false;
				}
				break;

			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawMonsterDoor(Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					location.Offset(56, 14);
					Display.Scissor = true;
					Display.ScissorZone = new Rectangle(location, new Size(144, 142));
					TileSet.Draw(0, new Point(location.X, location.Y + VPosition * 5));
					TileSet.Draw(1, new Point(location.X, location.Y + 86 + VPosition * -2));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(36, new Point(260, 72));


				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					location.Offset(28, 8);
					Display.Scissor = true;
					Display.ScissorZone = new Rectangle(location, new Size(104, 96));
					TileSet.Draw(2, new Point(location.X, location.Y + VPosition * 3));
					TileSet.Draw(3, new Point(location.X, location.Y + 56 + VPosition * -1));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(37, new Point(234, 80));

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
					Display.Scissor = true;
					Display.ScissorZone = new Rectangle(location, new Size(68, 60));
					TileSet.Draw(4, new Point(location.X, location.Y + VPosition * 2));
					TileSet.Draw(5, new Point(location.X, location.Y + 36 + VPosition * -1));
					Display.Scissor = false;
				}
				break;

			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawSpiderDoor(Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					location.Offset(32, 24);
					Display.Scissor = true;
					Display.ScissorZone = new Rectangle(location, new Size(192, 142));
					TileSet.Draw(12, new Point(location.X, location.Y + VPosition * 5));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(39, new Point(260, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					location.Offset(32, 18);
					Display.Scissor = true;
					Display.ScissorZone = new Rectangle(location, new Size(96, 82));
					TileSet.Draw(13, new Point(location.X, location.Y + VPosition * 3));
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(40, new Point(234, 80));
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
					Display.ScissorZone = new Rectangle(location, new Size(64, 54));
					Display.Scissor = true;
					//					TileSet.Draw(14, location);
					TileSet.Draw(14, new Point(location.X, location.Y + VPosition * 2));
					Display.Scissor = false;
				}
				break;

			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="distance"></param>
		void DrawStoneDoor(Point location, ViewFieldPosition distance)
		{
			switch (distance)
			{
				case ViewFieldPosition.M:
				case ViewFieldPosition.N:
				case ViewFieldPosition.O:
				{
					Display.Scissor = true;
					location.Offset(32, 16);
					Display.ScissorZone = new Rectangle(location.X + 26 , location.Y, 140, 142);
					TileSet.Draw(6, new Point(location.X + VPosition * 3, location.Y));
					TileSet.Draw(6, new Point(location.X - VPosition * 3 + 96, location.Y), true, false);
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(33, new Point(254, 72));
				}
				break;

				case ViewFieldPosition.H:
				case ViewFieldPosition.I:
				case ViewFieldPosition.J:
				case ViewFieldPosition.K:
				case ViewFieldPosition.L:
				{
					Display.Scissor = true;
					location.Offset(16, 8);
					Display.ScissorZone = new Rectangle(location.X + 14, location.Y, 100, 94);
					TileSet.Draw(7, new Point(location.X + VPosition * 2, location.Y));
					TileSet.Draw(7, new Point(location.X - VPosition * 2 + 64, location.Y), true, false);
					Display.Scissor = false;

					if (HasButton)
						TileSet.Draw(34, new Point(234, 80));
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
					Display.Scissor = true;
					location.Offset(8, 4);
					Display.ScissorZone = new Rectangle(location.X + 10, location.Y, 60, 58);
					TileSet.Draw(8, new Point(location.X + VPosition, location.Y));
					TileSet.Draw(8, new Point(location.X - VPosition + 40, location.Y), true, false);
					Display.Scissor = false;
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
		Grid,

		Iron,

		Monster,

		Spider,

		Stone,

		Eye
	}

}
