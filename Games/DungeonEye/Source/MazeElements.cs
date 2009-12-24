using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using ArcEngine;


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
//		FlyingItems
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
	/// Block drawing informations
	/// </summary>
	public static class MazeDisplayCoordinates
	{

		/// <summary>
		/// Default constructor 
		/// </summary>
		static MazeDisplayCoordinates()
		{
			int viewcount = Enum.GetValues(typeof(ViewFieldPosition)).Length;

			Doors = new TileDrawing[viewcount];
			FloorPlates = new TileDrawing[viewcount];
			GroundItems = new Point[viewcount, 4];
			Decorations = new TileDrawing[viewcount, 3];
			FlyingItems = new Point[viewcount, 5];
			Walls = new List<TileDrawing>[viewcount];
			for (int i = 0; i < viewcount; i++)
				Walls[i] = new List<TileDrawing>();

			Pits = new TileDrawing[viewcount];
			Stairs = new List<TileDrawing>[viewcount];
			for (int i = 0; i < viewcount; i++)
				Stairs[i] = new List<TileDrawing>();
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
		/// <param name="ground">ground position</param>
		/// <returns></returns>
		static public Point GetGroundItem(ViewFieldPosition view, GroundPosition ground)
		{
			if (ground == GroundPosition.Middle)
				throw new ArgumentOutOfRangeException("ground", "No ground item in the middle of a block !");

			return GroundItems[(int)view, (int)ground];
		}


		/// <summary>
		/// Gets a flying item coordinate
		/// </summary>
		/// <param name="view">Block position in the view field</param>
		/// <param name="ground">ground position</param>
		/// <returns></returns>
		static public Point GetFlyingItem(ViewFieldPosition view, GroundPosition ground)
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
			Stream stream = ResourceManager.LoadResource("MazeElements.xml");
			if (stream == null)
				throw new FileNotFoundException("Can not find maze element coordinate file !!! Aborting.");

			try
			{
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
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							CardinalPoint side = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["side"].Value, true);
							if (side == CardinalPoint.North)
								throw new ArgumentOutOfRangeException("side", "No north wall side decoration !");

							Decorations[(int)view, (int)side - 1] = GetTileDrawing(node);
								//new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						}
						break;


						case "wall":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Walls[(int)view].Add(GetTileDrawing(node));
						}
						break;


						case "stair":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Stairs[(int)view].Add(GetTileDrawing(node));
						}
						break;


						case "grounditem":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							GroundPosition ground = (GroundPosition)Enum.Parse(typeof(GroundPosition), node.Attributes["coordinate"].Value, true);
							if (ground == GroundPosition.Middle)
								throw new ArgumentOutOfRangeException("ground", "No ground item in the middle of a block !");

							GroundItems[(int)view, (int)ground] = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						}
						break;


						case "flyingitem":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							GroundPosition ground = (GroundPosition)Enum.Parse(typeof(GroundPosition), node.Attributes["coordinate"].Value, true);

							FlyingItems[(int)view, (int)ground] = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						}
						break;


						case "pit":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Pits[(int)view] = GetTileDrawing(node);
						}
						break;


						case "floorplate":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							FloorPlates[(int)view] = GetTileDrawing(node);
						}
						break;

						case "door":
						{
							ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
							Doors[(int)view] = GetTileDrawing(node);
						}
						break;

					}
				}
			}
			finally
			{
				stream.Close();
				stream.Dispose();
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


			bool swapx = false;
			if (node.Attributes["swapx"] != null)
				swapx = true;

			bool swapy = false;
			if (node.Attributes["swapy"] != null)
				swapy = true;

			int id = int.Parse(node.Attributes["tile"].Value);
			Point point = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
			return new TileDrawing(id, point, swapx, swapy);

		}

		#endregion


		#region Properties

		/// <summary>
		/// Pits
		/// </summary>
		static TileDrawing[] Pits;


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
		static Point[,] GroundItems;


		/// <summary>
		/// Decorations
		/// </summary>
		static TileDrawing[,] Decorations;


		/// <summary>
		/// Flying items
		/// </summary>
		static Point[,] FlyingItems;


		/// <summary>
		/// Rectangle zone for alcoves
		/// </summary>
		static public Rectangle AlcoveZone
		{
			get
			{
				return new Rectangle(130, 64, 128, 44);
			}
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
		public TileDrawing(int id, Point location) : this(id, location, false, false)
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id">ID of the tile</param>
		/// <param name="location">Location</param>
		/// <param name="swapx">Vertical flip</param>
		/// <param name="swapy">Horizontal flip</param>
		public TileDrawing(int id, Point location, bool swapx, bool swapy)
		{
			ID = id;
			Location = location;
			SwapX = swapx;
			SwapY = swapy;
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
		/// Swap horizontally
		/// </summary>
		public bool SwapX
		{
			get;
			private set;
		}

		/// <summary>
		/// Swap vertically
		/// </summary>
		public bool SwapY
		{
			get;
			private set;
		}

	}



}
