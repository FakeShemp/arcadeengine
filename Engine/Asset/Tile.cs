using System.Drawing;

namespace ArcEngine.Asset
{

	/// <summary>
	/// Definition of a tile
	/// </summary>
	public class Tile
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="hotspot"></param>
		public Tile(Rectangle rect, Point hotspot)
		{
			Rectangle = rect;
			HotSpot = hotspot;
		}

		/// <summary>
		/// 
		/// </summary>
		public Tile()
		{
			Rectangle = Rectangle.Empty;
			HotSpot = Point.Empty;
		}



		#region Properties

		/// <summary>
		/// Gets / sets the rectangle of the tile in the texture
		/// </summary>
		public Rectangle Rectangle
		{
			get;
			set;
		}


		/// <summary>
		/// Size of the tile
		/// </summary>
		public Size Size
		{
			get
			{
				return Rectangle.Size;
			}
		}

									


		/// <summary>
		/// HotSpot of the tile
		/// </summary>
		public Point HotSpot
		{
			get;
			set;
		}


		/// <summary>
		/// Gets/sets the collision box of the tile relative to the hotspot
		/// (Usefull for sprites)
		/// </summary>
		public Rectangle CollisionBox
		{
			get;
			set;
		}


		/// <summary>
		/// Get tile colors
		/// </summary>
		public Color[,] Data
		{
			get;
			internal set;
		}


/*
		/// <summary>
		/// Collision mask
		/// </summary>
		public bool[,] CollisionMask
		{
			get;
			set;
		}
*/
		#endregion

	}

}
