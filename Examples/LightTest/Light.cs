using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ArcEngine.Graphic;


namespace ArcEngine.Examples.LightTest
{
	public class Light
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Light()
		{
			Intensity = 1.0f;
			Radius = 128.0f;
			Color = Color.White;
			Resolution = 24;
		}


		/// <summary>
		/// Draws the light
		/// </summary>
		public void Render(BatchBuffer buffer)
		{
			if (buffer == null)
			return;

			buffer.AddPoint(Location, Color);

			Point point = Point.Empty;

			for (int i = 0; i <= Resolution; i++)
			{
				float angle = i * 2.0f * (float)Math.PI / Resolution;

				point.X = (int) (Location.X + Math.Cos(angle) * Radius);
				point.Y = (int)(Location.Y + Math.Sin(angle) * Radius);

				buffer.AddPoint(point, Color.FromArgb(0, Color));
			}
			int count = buffer.Update();
			Display.DrawBatch(buffer, PrimitiveType.TriangleFan, 0, count);

		}


		#region Properties

		/// <summary>
		/// Location
		/// </summary>
		public Point Location;


		/// <summary>
		/// Intensity
		/// </summary>
		public float Intensity;


		/// <summary>
		/// Radius
		/// </summary>
		public float Radius;


		/// <summary>
		/// Color
		/// </summary>
		public Color Color;


		/// <summary>
		/// Resolution quality
		/// </summary>
		int Resolution;

		#endregion
	}
}
