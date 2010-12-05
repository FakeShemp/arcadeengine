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
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;


//
//
// http://dmweb.free.fr/?q=node/1394
//
//
// Type of coordinates
//		lighting bolts
//		clouds
//		creatures
//		items
//		stack of items
//		ThrownItems
//		Doors
//		Floor decorations (foot steps)
//		Wall decorations
//		Unreadable texts
//		Wall texts
//		Door buttons
//		Door decorations
//
//

namespace DungeonEye
{
	/// <summary>
	/// Visual display informations
	/// </summary>
	public static class DisplayCoordinates
	{

		/// <summary>
		/// Default constructor 
		/// </summary>
		static DisplayCoordinates()
		{
			int viewcount = Enum.GetValues(typeof(ViewFieldPosition)).Length;

			Doors = new TileDrawing[viewcount];
			FloorPlates = new TileDrawing[viewcount];
			
			Ground = new Point[viewcount, 5];
			for (int i = 0 ; i < viewcount ; i++)
				for (int j = 0 ; j < 5 ; j++)
					Ground[i,j] = new Point(-999, -999);

			Decorations = new TileDrawing[viewcount, 3];
			FlyingItems = new Point[viewcount, 5];
			Walls = new List<TileDrawing>[viewcount];
			for (int i = 0; i < viewcount; i++)
				Walls[i] = new List<TileDrawing>();

			Pits = new TileDrawing[viewcount];
			CeilingPits = new TileDrawing[viewcount];
			Stairs = new List<TileDrawing>[viewcount];
			for (int i = 0; i < viewcount; i++)
				Stairs[i] = new List<TileDrawing>();


			ThrowRight =  new Rectangle(176, 0, 176, 144);
			ThrowLeft = new Rectangle(0, 0, 176, 144);
			CampButton = new Rectangle(578, 354, 62, 42);
			FrontSquare = new Rectangle(48, 14, 256, 192);
			Alcove = new Rectangle(130, 64, 128, 44);
			LeftFeetTeam = new Rectangle(0, 202, 176, 38);
			LeftFrontTeamGround = new Rectangle(0, 144, 176, 58);
			RightFeetTeam = new Rectangle(176, 202, 176, 38);
			RightFrontTeamGround = new Rectangle(176, 144, 176, 58);

			ScriptedDialog = new Rectangle(0, 242, 640, 158);
		}


		#region Getters

		/// <summary>
		/// Gets a draw order information
		/// </summary>
		/// <param name="position">Block position in the view field</param>
		/// <returns></returns>
		static public List<TileDrawing> GetWalls(ViewFieldPosition position)
		{
			return Walls[(int)position];
		}


		/// <summary>
		/// Gets a ground item coordinate
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <param name="position">ground position</param>
		/// <returns></returns>
		static public Point GetGroundPosition(ViewFieldPosition view, SquarePosition position)
		{
			return Ground[(int)view, (int)position];
		}


		/// <summary>
		/// Gets a flying item coordinate
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <param name="ground">ground position</param>
		/// <returns></returns>
		static public Point GetFlyingItem(ViewFieldPosition view, SquarePosition ground)
		{
			return FlyingItems[(int)view, (int)ground];
		}


		/// <summary>
		/// Gets a decoration coordinate
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <param name="point">Wall side</param>
		/// <returns></returns>
		static public TileDrawing GetDecoration(ViewFieldPosition view, CardinalPoint point)
		{
			if (point == CardinalPoint.North)
				return null;

			return Decorations[(int)view, (int)point - 1];
		}


		/// <summary>
		/// Get stair
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <returns></returns>
		static public List<TileDrawing> GetStairs(ViewFieldPosition view)
		{
			return Stairs[(int)view];
		}


		/// <summary>
		/// Get pit
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <returns></returns>
		static public TileDrawing GetPit(ViewFieldPosition view)
		{
			return Pits[(int)view];
		}


		/// <summary>
		/// Get ceiling pit
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <returns></returns>
		static public TileDrawing GetCeilingPit(ViewFieldPosition view)
		{
			return CeilingPits[(int)view];
		}


		/// <summary>
		/// Gets floor plate
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <returns></returns>
		static public TileDrawing GetFloorPlate(ViewFieldPosition view)
		{
			return FloorPlates[(int)view];
		}


		/// <summary>
		/// Gets door
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <returns></returns>
		static public TileDrawing GetDoor(ViewFieldPosition view)
		{
			return Doors[(int)view];
		}

		#endregion


		#region IO

		/// <summary>
		/// Loads the maze definition
		/// </summary>
		/// <returns></returns>
		static public bool Load()
		{
			
			// Load file definition
			using (Stream stream = ResourceManager.Load("MazeElements.xml"))
			{
				if (stream == null)
					throw new FileNotFoundException("Can not find maze element coordinate file !!! Aborting.");

				XmlDocument doc = new XmlDocument();
				doc.Load(stream);
				XmlNode xml = doc.DocumentElement;
				if (xml.Name != "displaycoordinate")
				{
					Trace.Write("Wrong header for MazeElements file");
					return false;
				}


				foreach (XmlNode node in xml)
				{
					if (node.NodeType == XmlNodeType.Comment)
						continue;


					switch (node.Name.ToLower())
					{
						case "decoration":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							CardinalPoint side = (CardinalPoint) Enum.Parse(typeof(CardinalPoint), node.Attributes["side"].Value, true);
							if (side == CardinalPoint.North)
								throw new ArgumentOutOfRangeException("side", "No north wall side decoration !");

							Decorations[(int) view, (int) side - 1] = GetTileDrawing(node);
							//new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						}
						break;


						case "wall":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Walls[(int) view].Add(GetTileDrawing(node));
						}
						break;


						case "stair":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Stairs[(int) view].Add(GetTileDrawing(node));
						}
						break;


						case "ground":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							SquarePosition ground = (SquarePosition) Enum.Parse(typeof(SquarePosition), node.Attributes["coordinate"].Value, true);

							Ground[(int) view, (int) ground] = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						}
						break;


						case "flyingitem":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							SquarePosition ground = (SquarePosition) Enum.Parse(typeof(SquarePosition), node.Attributes["coordinate"].Value, true);

							FlyingItems[(int) view, (int) ground] = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						}
						break;


						case "pit":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Pits[(int) view] = GetTileDrawing(node);
						}
						break;


						case "ceilingpit":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							CeilingPits[(int) view] = GetTileDrawing(node);
						}
						break;


						case "floorplate":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							FloorPlates[(int) view] = GetTileDrawing(node);
						}
						break;

						case "door":
						{
							ViewFieldPosition view = (ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Doors[(int) view] = GetTileDrawing(node);
						}
						break;

						default:
						{
							Trace.WriteLine("[MazeDisplayCoordinates] Load() : Unknown element \"" + node.Name + "\".");
						}
						break;

					}
				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		static TileDrawing GetTileDrawing(XmlNode node)
		{
			if (node == null)
				return null;

			// Tile id
			int id = int.Parse(node.Attributes["tile"].Value);

			// Location
			Point location = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));

			// effect
			SpriteEffects effect = SpriteEffects.None;
			if (node.Attributes["effect"] != null)
				effect = (SpriteEffects)Enum.Parse(typeof(SpriteEffects), node.Attributes["effect"].Value);

			return new TileDrawing(id, location, effect);
		}

		#endregion


		#region Properties

		/// <summary>
		/// Pits
		/// </summary>
		static TileDrawing[] Pits;

		/// <summary>
		/// Ceiling pits
		/// </summary>
		static TileDrawing[] CeilingPits;


		/// <summary>
		/// Floorplates
		/// </summary>
		static TileDrawing[] FloorPlates;


		/// <summary>
		/// Doors
		/// </summary>
		static TileDrawing[] Doors;


		/// <summary>
		/// Walls
		/// </summary>
		static List<TileDrawing>[] Walls;


		/// <summary>
		/// Stairs
		/// </summary>
		static List<TileDrawing>[] Stairs;


		/// <summary>
		/// Ground items
		/// </summary>
		static Point[,] Ground;


		/// <summary>
		/// Decorations
		/// </summary>
		static TileDrawing[,] Decorations;


		/// <summary>
		/// Flying items
		/// </summary>
		static Point[,] FlyingItems;


		/// <summary>
		/// Alcoves zone
		/// </summary>
		static public Rectangle Alcove
		{
			get;
			private set;
		}


		/// <summary>
		/// Front block zone
		/// </summary>
		static public Rectangle FrontSquare
		{
			get;
			private set;
		}


		/// <summary>
		/// Camp button zone
		/// </summary>
		static public Rectangle CampButton
		{
			get;
			private set;
		}


		/// <summary>
		/// Throw left zone
		/// </summary>
		static public Rectangle ThrowLeft
		{
			get;
			private set;
		}


		/// <summary>
		/// Scripted dialog rectangle
		/// </summary>
		static public Rectangle ScriptedDialog
		{
			get;
			private set;
		}


		/// <summary>
		/// Throw right zone
		/// </summary>
		static public Rectangle ThrowRight
		{
			get;
			private set;
		}


		/// <summary>
		/// Left team ground item zone
		/// </summary>
		static public Rectangle LeftFeetTeam
		{
			get;
			private set;
		}


		/// <summary>
		/// Right team ground item zone
		/// </summary>
		static public Rectangle RightFeetTeam
		{
			get;
			private set;
		}


		/// <summary>
		/// Left front team ground item zone
		/// </summary>
		static public Rectangle LeftFrontTeamGround
		{
			get;
			private set;
		}


		/// <summary>
		/// Right front team ground item zone
		/// </summary>
		static public Rectangle RightFrontTeamGround
		{
			get;
			private set;
		}



		#endregion

	}




	/// <summary>
	/// Location on the screen of a tile
	/// </summary>
	/// <remarks>This class is used for drawing maze blocks</remarks>
	public class TileDrawing
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id">ID of the tile</param>
		/// <param name="location">Location</param>
		public TileDrawing(int id, Point location) : this(id, location, SpriteEffects.None)
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id">ID of the tile</param>
		/// <param name="location">Location</param>
		/// <param name="effect">Display effect</param>
		public TileDrawing(int id, Point location, SpriteEffects effect)
		{
			ID = id;
			Location = location;
			Effect = effect;
		}

		/// <summary>
		/// Tile id
		/// </summary>
		public int ID
		{
			get;
			private set;
		}

		/// <summary>
		/// Location on the screen
		/// </summary>
		public Point Location
		{
			get;
			private set;
		}

		/// <summary>
		/// Display effect
		/// </summary>
		public SpriteEffects Effect
		{
			get;
			private set;
		}

	}



}
