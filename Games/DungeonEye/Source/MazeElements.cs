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
	static class MazeDisplayCoordinates
	{

		/// <summary>
		/// Default constructor 
		/// </summary>
		static MazeDisplayCoordinates()
		{
			int viewcount = Enum.GetValues(typeof(ViewFieldPosition)).Length;
			Blocks = new BlockRenderInfos[viewcount];


			Walls = new List<TileDrawing>[viewcount];
			for (int i = 0; i < viewcount; i++)
				Walls[i] = new List<TileDrawing>();


			GroundItems = new Point[viewcount, 4];




			// Load file definition
			Stream stream = ResourceManager.LoadResource("data/MazeElements.xml");
			if (stream == null)
			{
				throw new FileNotFoundException("Can not find maze element coordinate file !!!. Aborting");
			}
			XmlDocument doc = new XmlDocument();
			doc.Load(stream);
			Load(doc.DocumentElement);

			stream.Close();
			stream.Dispose();
		}


		/// <summary>
		/// Gets a draw order information
		/// </summary>
		/// <param name="position">Block position in the view field</param>
		/// <returns></returns>
		static public BlockRenderInfos Get(ViewFieldPosition position)
		{
			return Blocks[(int)position];
		}



		/// <summary>
		/// Clear all informations
		/// </summary>
		static public void Clear()
		{
			foreach (BlockRenderInfos block in Blocks)
				block.Clear();

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



		#region IO

		/// <summary>
		/// Loads the maze definition
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		static bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

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

					}
					break;


					case "wall":
					{
						ViewFieldPosition view = (ViewFieldPosition)Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);

						bool swapx = false;
						if (node.Attributes["swapx"] != null)
							swapx = true;

						int id = int.Parse(node.Attributes["tile"].Value);
						Point point = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						Walls[(int)view].Add(new TileDrawing(id, point, swapx, false));
					}
					break;


					case "grounditem":
					{
						ViewFieldPosition view =(ViewFieldPosition) Enum.Parse(typeof(ViewFieldPosition), node.Attributes["position"].Value, true);
						GroundPosition ground = (GroundPosition)Enum.Parse(typeof(GroundPosition), node.Attributes["coordinate"].Value, true);
						if (ground == GroundPosition.Middle)
							throw new ArgumentOutOfRangeException("ground", "No ground item in the middle of a block !");

						GroundItems[(int)view, (int)ground] = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
					}
					break;



				}
			}



			return true;
		}


		#endregion




		/// <summary>
		/// Draw orders
		/// </summary>
		static BlockRenderInfos[] Blocks;










		/// <summary>
		/// Walls
		/// </summary>
		static List<TileDrawing>[] Walls;


		/// <summary>
		/// Ground items
		/// </summary>
		static Point[,] GroundItems;
	}



	
	/// <summary>
	/// Maze block render informations
	/// </summary>
	class BlockRenderInfos
	{
		public BlockRenderInfos()
		{
			Walls = new List<TileDrawing>();
			Decorations = new List<TileDrawing>();
			Stairs = new List<TileDrawing>();
			Pits = new List<TileDrawing>();
			FloorPlates = new List<TileDrawing>();
			Doors = new List<TileDrawing>();
			//Monsters = new List<Point>();
			GroundItems = new Point[4];
		}


		/// <summary>
		/// Clear informations
		/// </summary>
		public void Clear()
		{
			Walls.Clear();
			Decorations.Clear();
			Stairs.Clear();
			Pits.Clear();
			FloorPlates.Clear();
			Doors.Clear();
		//	Monsters.Clear();
		//	GroundItems.Clear();
		}

/*
		/// <summary>
		/// Monsters locations
		/// </summary>
		public List<Point> Monsters;
*/
		
		/// <summary>
		/// Ground items
		/// </summary>
		public Point[] GroundItems;


		/// <summary>
		/// Walls locations
		/// </summary>
		public List<TileDrawing> Walls;


		/// <summary>
		/// Wall decorations locations
		/// </summary>
		public List<TileDrawing> Decorations;


		/// <summary>
		/// Stairs locations
		/// </summary>
		public List<TileDrawing> Stairs;


		/// <summary>
		/// Pits locations
		/// </summary>
		public List<TileDrawing> Pits;


		/// <summary>
		/// Floor plates locations
		/// </summary>
		public List<TileDrawing> FloorPlates;


		/// <summary>
		/// Doord locations
		/// </summary>
		public List<TileDrawing> Doors;


	}



	/// <summary>
	/// Location on the screen of a tile
	/// </summary>
	/// <remarks>This class is used for drawing maze blocks</remarks>
	class TileDrawing
	{
		/// <summary>
		/// 
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
		/// <param name="swapx"></param>
		/// <param name="swapy"></param>
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
