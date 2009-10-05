using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine;
using ArcEngine.Asset;


namespace ProjectT
{
	/// <summary>
	/// 
	/// </summary>
	public class Shape
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Shape()
		{
			Size = new Size(32, 32);
		}


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			// Load tiles
			Tiles = ResourceManager.CreateSharedAsset<TileSet>("Shapes");
			if (Tiles == null)
				return false;

			// Size of one piece
			Size = Tiles.GetTile(0).Size;

			return true;
		}


		/// <summary>
		/// Display a shape
		/// </summary>
		/// <param name="location"></param>
		/// <param name="id"></param>
		/// <param name="rotation"></param>
		public void Draw(Point location, byte id, byte rotation)
		{
			if (id < 0 || id > 4)
				return;

			if (rotation < 0 || rotation > 4)
				return;

			for(int y = 0; y < 5; y++)
				for (int x = 0; x < 5; x++)
				{
					if (Pieces[id, rotation, x, y] == 0)
						continue;

					Tiles.Draw(0, new Point(location.X + x * Size.Width, location.Y + y * Size.Height));
				}
		}



		#region Properties

		/// <summary>
		/// Shapes definition 
		/// </summary>
		byte[, , ,] Pieces = new byte[7 /*kind */, 4 /* rotation */, 5 /* horizontal blocks */, 5 /* vertical blocks */ ]  
		{  
			#region Square
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 0, 0, 0}  
				}  
			},  
			#endregion

			#region I
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 1, 2, 1, 1},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 2, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 1, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{1, 1, 2, 1, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 1, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 2, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				}  
			},  
			#endregion

			#region L
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 2, 0, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 1, 2, 1, 0},  
					{0, 1, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 1, 1, 0, 0},  
					{0, 0, 2, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 1, 0},  
					{0, 1, 2, 1, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				}  
			}, 
			#endregion

			#region L mirrored  
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 2, 0, 0},  
					{0, 1, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 1, 0, 0, 0},  
					{0, 1, 2, 1, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 2, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 1, 2, 1, 0},  
					{0, 0, 0, 1, 0},  
					{0, 0, 0, 0, 0}  
				}  
			},  
			#endregion

			#region N  
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 1, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 1, 2, 0, 0},  
					{0, 0, 1, 1, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 1, 2, 0, 0},  
					{0, 1, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 1, 1, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				}  
			},  
			#endregion

			#region N mirrored
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 0, 1, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 1, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 1, 0, 0, 0},  
					{0, 1, 2, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 1, 0},  
					{0, 1, 2, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				}  
			},  
			#endregion

			#region T  
			{  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 2, 1, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0},  
					{0, 1, 2, 1, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 1, 2, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 0, 0, 0, 0}  
				},  
				{  
					{0, 0, 0, 0, 0},  
					{0, 0, 1, 0, 0},  
					{0, 1, 2, 1, 0},  
					{0, 0, 0, 0, 0},  
					{0, 0, 0, 0, 0}  
				}  
				} 
		#endregion
		};


		/// <summary>
		/// Size of one brick
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Tileset 
		/// </summary>
		TileSet Tiles;

		
		#endregion
	}
}
