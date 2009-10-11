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
			Scale = new PointF(1.0f, 1.0f);
		}


		/// <summary>
		/// Update the camera 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
		}


		#region Properties


		/// <summary>
		/// Location of the camera
		/// </summary>
		public Point Location
		{
			get;
			set;
		}


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
		public PointF Scale
		{
			get;
			set;
		}

		#endregion
	}
}
