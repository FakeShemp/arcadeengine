using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using ArcEngine.Asset;

namespace TowerDefense
{
	/// <summary>
	/// Tower class
	/// </summary>
	public class Tower
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="location">Game corrdinate</param>
		public Tower(Point location)
		{
			Location = location;
			Rate = TimeSpan.FromSeconds(0.5f);
			Range = 64.0f;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch)
		{
			if (batch == null)
				return;


			Tile tile = Game.TileSet.GetTile(0);
			if (tile != null)
			{
				Vector2 coord = new Vector2(
				Location.X * Game.Scale.X + Game.Scale.X / 2.0f - tile.Size.Width / 2.0f,
				Location.Y * Game.Scale.Y + Game.Scale.Y / 2.0f - tile.Size.Height / 2.0f);

				batch.DrawTile(Game.TileSet, 0, coord, Color.White);
			}			
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
		}


		#region Properties

		/// <summary>
		/// Location in game
		/// </summary>
		public Point Location
		{
			get;
			private set;
		}


		/// <summary>
		/// Attack range
		/// </summary>
		float Range;


		/// <summary>
		/// Fire rate
		/// </summary>
		TimeSpan Rate;


		/// <summary>
		/// Damage
		/// </summary>
		float Damage;


		/// <summary>
		/// Buy price
		/// </summary>
		int Price;


		#endregion
	}
}
