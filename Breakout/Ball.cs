#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Text;
using System.Drawing;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;


namespace Breakout
{
	/// <summary>
	/// 
	/// </summary>
	public class Ball
	{
		
		/// <summary>
		/// Constructor
		/// </summary>
		public Ball(Level level)
		{
			Level = level;
			TileSet = ResourceManager.CreateAsset<TileSet>("Balls");
		}



		/// <summary>
		/// Updates the ball status
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			if (Lost)
				return;


			if (Velocity.IsEmpty)
			{
				// Stick the ball to the paddle
				Location = new Point(Level.Paddle.Location.X + Level.Paddle.Rectangle.Width / 2 - Size.Width / 2, Level.Paddle.Location.Y - this.Size.Height);
				return;
			}


			// Move the ball
			Location.Offset(Velocity);


			// Check for ball collision
			if (Location.X < 160 || Location.X + Size.Width > 800)
				Velocity.X = -Velocity.X;

			if (Location.Y < 0)
				Velocity.Y = -Velocity.Y;


			// Paddle collision
			if (Rectangle.IntersectsWith(Level.Paddle.Rectangle))
			{
				// Change velocity
				Velocity.Y = -Velocity.Y;

				// hit the left side of the paddle and coming from the left
				if (Location.X < Level.Paddle.Rectangle.Left + Level.Paddle.Rectangle.Width / 2 && Velocity.X > 0)
					Velocity.X = -Velocity.X;

			}



				// Ball out 
				if (Location.Y > 600)
					Lost = true;
		}



		/// <summary>
		/// Draws the ball
		/// </summary>
		/// <param name="device"></param>
		public void Draw()
		{
			if (Lost)
				return;

			TileSet.Draw(0, Location);
		}



		/// <summary>
		/// Resets the status of the ball
		/// </summary>
		public void Reset()
		{
		//	Location = new Point(512, 384);
			Velocity = new Point(Game.Random.Next(-10, 10), Game.Random.Next(-10, 10));

			Lost = false;
		}



		#region Properties


		/// <summary>
		/// Tileset of the ball
		/// </summary>
		TileSet TileSet;


		/// <summary>
		/// Location of the ball
		/// </summary>
		public Point Location;


		/// <summary>
		/// Size of the ball
		/// </summary>
		public Size Size
		{
			get
			{
				return TileSet.GetTile(0).Size;
			}
		}


		/// <summary>
		/// Velocity of the ball
		/// </summary>
		public Point Velocity;


		/// <summary>
		/// Rectangle of the ball
		/// </summary>
		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle(Location, Size);
			}
		}


		/// <summary>
		/// Reference to the level
		/// </summary>
		Level Level;


		/// <summary>
		/// Gets if the ball is lost
		/// </summary>
		public bool Lost
		{
			get;
			protected set;
		}

		#endregion
	}
}
