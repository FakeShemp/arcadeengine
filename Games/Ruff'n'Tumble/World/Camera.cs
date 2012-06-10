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
			ViewPort = new Rectangle(0, 56, 800, 44);
			Speed = 250.0f;
			ClampToEdges = true;
		}


		/// <summary>
		/// Update the camera 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			Console.WriteLine(time.ElapsedGameTime);

			float speed = Speed * time.ElapsedGameTime.Milliseconds / 1000;

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
			}
		}



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
		public PointF Location;


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
		public PointF TargetOffset
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

		#endregion
	}
}
