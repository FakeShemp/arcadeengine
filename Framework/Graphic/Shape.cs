using System;
using System.Drawing;
using TK = OpenTK.Graphics.OpenGL;


namespace ArcEngine.Graphic
{
	/// <summary>
	/// A drawable convex shape
	/// </summary>
	public class Shape
	{


		/// <summary>
		/// Begins a new shape
		/// </summary>
		/// <param name="mode">Shape drawing mode</param>
		public void Begin(ShapeMode mode)
		{
			if (IsOpen)
			{
				throw new InvalidOperationException("Shape opened. Close the Shape first !!!");
			}

			Display.Texturing = false;
			if (mode == ShapeMode.Fill)
                TK.GL.Begin(TK.BeginMode.Polygon);
			else
                TK.GL.Begin(TK.BeginMode.LineStrip);

			Mode = mode;
			IsOpen = true;
		}


		/// <summary>
		/// End of the shape
		/// </summary>
		public void End()
		{
			if (!IsOpen)
				return;

            TK.GL.End();
			Display.Texturing = true;

			IsOpen = false;
			StartPoint = Point.Empty;
			LastPoint = Point.Empty;
		}


		/// <summary>
		/// Closes the shape
		/// </summary>
		public void ClosePath()
		{
            TK.GL.Vertex2(StartPoint.X, StartPoint.Y);
			LastPoint = StartPoint;

			End();
		}


		/// <summary>
		/// Creates a new subpath with the specified point as its first point.
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		public void MoveTo(int x, int y)
		{
			End();
			Begin(Mode);
            TK.GL.Vertex2(x, y);
			StartPoint.X = x;
			StartPoint.Y = y;
			LastPoint = StartPoint;
		}


		/// <summary>
		/// Connects the last point to the given point using a straight line,
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		public void LineTo(int x, int y)
		{
            TK.GL.Vertex2(x, y);
			LastPoint.X = x;
			LastPoint.Y = y;
		}


		/// <summary>
		/// Draws a quadratic curve
		/// </summary>
		/// <param name="point">End point</param>
		/// <param name="control">Control point</param>
		public void QuadraticCurveTo(Point point, Point control)
		{
			//Point control1 = new Point(
			//   (int)(point.X + 2.0f / 3.0f * (control.X - point.X)),
			//   (int)(point.Y + 2.0f / 3.0f * (control.Y - point.Y)));

			//Point control2 = new Point(
			//   (int)(control1.X + (LastPoint.X - point.X) / 3.0f),
			//   (int)(control1.Y + (LastPoint.Y - point.Y) / 3.0f));

			Point control1 = new Point(
				(int)(LastPoint.X + 2.0f / 3.0f * (control.X - LastPoint.X)),
				(int)(LastPoint.Y + 2.0f / 3.0f * (control.Y - LastPoint.Y)));

			Point control2 = new Point(
				(int)(control1.X + (point.X - LastPoint.X) / 3.0f),
				(int)(control1.Y + (point.Y - LastPoint.Y) / 3.0f));

			BezierCurveTo(point, control1, control2);
		}



		/// <summary>
		/// Draws a Bezier curve
		/// </summary>
		/// <param name="point">End point</param>
		/// <param name="control1">Control point 1</param>
		/// <param name="control2">Control point 2</param>
		public void BezierCurveTo(Point point, Point control1, Point control2)
		{
			OpenTK.Vector2[] points = new OpenTK.Vector2[]
			{
				new OpenTK.Vector2(LastPoint.X, LastPoint.Y),
				new OpenTK.Vector2(control1.X, control1.Y),
				new OpenTK.Vector2(control2.X, control2.Y),
				new OpenTK.Vector2(point.X, point.Y),
			};
			OpenTK.BezierCurve curve = new OpenTK.BezierCurve(points);


			OpenTK.Vector2 pos = OpenTK.Vector2.One;
			for (int p = 0; p <= 50; p++)
			{
				pos = curve.CalculatePoint((float)p / 50.0f);
                TK.GL.Vertex2(pos.X, pos.Y);
			}

			LastPoint.X = (int)pos.X;
			LastPoint.Y = (int)pos.Y;
		}



		/// <summary>
		/// Draws an arc
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radius">Radius</param>
		/// <param name="start">Start angle</param>
		/// <param name="angle">Angle length</param>
		public void ArcTo(int x, int y, int radius, float start, float angle)
		{

			int real_segments = (int)(Math.Abs(angle) / (2.0f * Math.PI) * 50.0f) + 1;


			float theta = angle / (float)(real_segments);
			float tangetial_factor = (float)Math.Tan(theta);
			float radial_factor = (float)(1 - Math.Cos(theta));

			float xx = (float)(x + radius * Math.Cos(start));
			float yy = (float)(y + radius * Math.Sin(start));

			for (int ii = 0; ii < real_segments + 1; ii++)
			{
                TK.GL.Vertex2(xx, yy);

				float tx = -(yy - y);
				float ty = xx - x;

				xx += tx * tangetial_factor;
				yy += ty * tangetial_factor;

				float rx = x - xx;
				float ry = y - yy;

				xx += rx * radial_factor;
				yy += ry * radial_factor;
			}

			LastPoint.X = (int)xx;
			LastPoint.Y = (int)yy;

		}


		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle</param>
		public void Rectangle(Rectangle rectangle)
		{
		}


		/// <summary>
		/// Draws a rounded rectangle
		/// </summary>
		/// <param name="rectangle">Rectangle</param>
		/// <param name="radius">Round radius</param>
		public void RoundedRectangle(Rectangle rectangle, int radius)
		{
			// Bottom left
			LineTo(rectangle.X, rectangle.Bottom - radius);
			QuadraticCurveTo(
			   new Point(rectangle.X + radius, rectangle.Bottom),
			   new Point(rectangle.X, rectangle.Bottom));

			// Bottom right
			LineTo(rectangle.Right - radius, rectangle.Bottom);
			QuadraticCurveTo(
			   new Point(rectangle.Right, rectangle.Bottom - radius),
			   new Point(rectangle.Right, rectangle.Bottom));

			// Top right
			LineTo(rectangle.Right, rectangle.Y + radius);
			QuadraticCurveTo(
				new Point(rectangle.Right - radius, rectangle.Y),
				new Point(rectangle.Right, rectangle.Y));

			// Top left
			LineTo(rectangle.X + radius, rectangle.Y);
			QuadraticCurveTo(
				new Point(rectangle.X, rectangle.Y + radius),
				new Point(rectangle.X, rectangle.Y));

			// Close the path
			LineTo(rectangle.X, rectangle.Bottom - radius);
		}



		/// <summary>
		/// 
		/// </summary>
		public void Clip()
		{
		}





		#region Properties

		/// <summary>
		/// Drawing mode
		/// </summary>
		ShapeMode Mode;


		/// <summary>
		/// Gets if the path is opened
		/// </summary>
		public bool IsOpen
		{
			get;
			private set;
		}


		/// <summary>
		/// Starting point
		/// </summary>
		Point StartPoint;


		/// <summary>
		/// Last draw point
		/// </summary>
		Point LastPoint;

		#endregion
	}


	/// <summary>
	/// Shape mode drawing
	/// </summary>
	public enum ShapeMode
	{
		/// <summary>
		/// Fill shape
		/// </summary>
		Fill,

		/// <summary>
		/// Stroke shape
		/// </summary>
		Stroke
	}
}
