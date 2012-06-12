using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.Input;

namespace RuffnTumble
{
	/// <summary>
	/// Controls the scrolling and panning of the level
	/// </summary>
	public class Camera
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="level">Level handle</param>
		public Camera(Level level)
		{
			//HACK: Hardcoded !!!
			ViewPort = new Rectangle(0, 56, 1024, 712);
			Speed = 250.0f;
			ClampToEdges = true;
			Level = level;
		}


		/// <summary>
		/// Update the camera 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			speed = Speed * time.ElapsedGameTime.Milliseconds / 1000.0f;

			if (Keyboard.IsKeyPress(Keys.Right))
			{
				Location.X += speed;
			}
			if (Keyboard.IsKeyPress(Keys.Left))
			{
				Location.X -= speed;
			}
			if (Keyboard.IsKeyPress(Keys.Up))
			{
				Location.Y -= speed;
			}
			if (Keyboard.IsKeyPress(Keys.Down))
			{
				Location.Y += speed;
			}


			if (ClampToEdges)
			{
				Location.X = Math.Max(0.0f, Location.X);
				Location.Y = Math.Max(0.0f, Location.Y);

				if (Location.X + ViewPort.Width> Level.SizeInPixel.Width)
					Location.X = Level.SizeInPixel.Width - ViewPort.Width;

				if (Location.Y + ViewPort.Height > Level.SizeInPixel.Height)
					Location.Y = Level.SizeInPixel.Height - ViewPort.Height;

			}

		}

		public float speed;

		/// <summary>
		/// Translate the camera
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void Translate(float x, float y)
		{
			Location.X += x;
			Location.Y += y;
		}




		#region Properties


		/// <summary>
		/// Location of the camera in the level
		/// </summary>
		public Vector2 Location;


		/// <summary>
		/// Follow this entity
		/// </summary>
		public Entity Target
		{
			get;
			set;
		}

		/// <summary>
		/// Rendering zone of the camera on the screen
		/// </summary>
		public Rectangle ViewPort
		{
			get;
			set;
		}


		/// <summary>
		/// Offset of the target
		/// </summary>
		public Vector2 TargetOffset
		{
			get;
			set;
		}


		/// <summary>
		/// Limit camera to the level borders
		/// </summary>
		public bool ClampToEdges
		{
			get;
			set;
		}


		/// <summary>
		/// Movment speed
		/// </summary>
		public float Speed
		{
			get;
			set;
		}


		/// <summary>
		/// Level handle
		/// </summary>
		Level Level;

		#endregion
	}
}
