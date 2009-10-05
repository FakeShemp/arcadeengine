using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine;

namespace ProjectT
{
	/// <summary>
	/// 
	/// </summary>
	public class Board
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="size">Size of the board</param>
		public Board(Size size)
		{
			Size = size;
			Slot = new byte[size.Height, size.Width];
		}


		/// <summary>
		/// Initializing the board
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
					Slot[y, x] = 0;

	
			// Load tiles
			Tiles = ResourceManager.CreateSharedAsset<TileSet>("Shapes");
			if (Tiles == null)
				return false;

			return true;
		}



		/// <summary>
		/// Update the board logic
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
		}


		/// <summary>
		/// Draw the board
		/// </summary>
		public void Draw()
		{
			// Background
			Display.Color = Color.LightGray;
			Display.Rectangle(new Rectangle(Location.X, Location.Y, 340, 680), true);

		}


		#region Properties


		/// <summary>
		/// Next shape to use
		/// </summary>
		public byte NextShape
		{
			get;
			private set;
		}

		/// <summary>
		/// Slots
		/// </summary>
		byte[,] Slot;

		/// <summary>
		/// Size of the board
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Location of the board in the screen
		/// </summary>
		public Point Location
		{
			get;
			set;
		}




		TileSet Tiles;

		#endregion


	}
}
