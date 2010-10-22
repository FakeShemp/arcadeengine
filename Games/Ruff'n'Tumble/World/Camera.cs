using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;

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
		/// <param name="level"></param>
		public Camera(Level level)
		{
			// Hardcoded !!!
			ViewPort = new Rectangle(0, 56, 800, 544);
			Scale = new Vector2(2.0f, 2.0f);
		}


		/// <summary>
		/// Update the camera 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
		}



		/// <summary>
		/// Translate the camera
		/// </summary>
		/// <param name="offset"></param>
		public void Translate(int x, int y)
		{
			Location.Offset(x, y);
		}




		#region Properties


		/// <summary>
		/// Location of the camera
		/// </summary>
		public Point Location;


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
		public Point Offset
		{
			get;
			set;
		}


		/// <summary>
		/// Scale factor
		/// </summary>
		public Vector2 Scale
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


		#endregion
	}
}
