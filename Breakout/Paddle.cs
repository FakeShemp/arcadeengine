using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using System.Windows.Forms;
using ArcEngine.Asset;



namespace Breakout
{
	/// <summary>
	/// 
	/// </summary>
	public class Paddle
	{

		/// <summary>
		/// 
		/// </summary>
		public Paddle()
		{
			//
			Location = new Point(0, 550);
			Size = new Size(100, 18);
			Speed = 20;
			Balls = new List<Ball>();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			TileSet = ResourceManager.CreateAsset<TileSet>("Paddle");

			return true;
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{

			// Moves the paddle
			if (Keyboard.IsKeyPress(Keys.Left))
				Location.Offset(new Point(-Speed, 0));

			if (Keyboard.IsKeyPress(Keys.Right))
				Location.Offset(new Point(Speed, 0));

			Location.X = Mouse.Location.X;

			// Check for border collision
			if (Location.X < 160)
				Location.X = 160;

			if (Location.X > 800 - Size.Width)
				Location.X = 800 - Size.Width;


			if (Mouse.Buttons == MouseButtons.Left && Balls.Count > 0)
			{
				foreach (Ball ball in Balls)
				{
					ball.Reset();
				}


				Balls.Clear();
			}

		}



		/// <summary>
		/// Draws the ball
		/// </summary>
		/// <param name="device"></param>
		public void Draw()
		{
			TileSet.Draw(0, Location);

			//device.Texture = TileSet.Texture;

			Tile tile = TileSet.GetTile(1);
			
			Rectangle rect = new Rectangle(Location.X + 16, Location.Y, Size.Width - 32, 18);
			
			TileSet.Draw(1, rect, TextureLayout.Tile);

			TileSet.Draw(2, new Point(Location.X + Size.Width - 16, Location.Y));
		}



		#region Properties


		/// <summary>
		/// Tileset of the ball
		/// </summary>
		TileSet TileSet;


		/// <summary>
		/// Location of the paddle
		/// </summary>
		public Point Location;


		/// <summary>
		/// Size of the paddle
		/// </summary>
		public Size Size;



		/// <summary>
		/// Speed of the paddle
		/// </summary>
		public int Speed;


		/// <summary>
		/// Rectangle of the paddle
		/// </summary>
		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle(Location, Size);
			}
		}



		/// <summary>
		/// Sticked balls
		/// </summary>
		public List<Ball> Balls
		{
			get;
			private set;
		}



		#endregion

	}
}
