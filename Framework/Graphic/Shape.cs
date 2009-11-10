using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;


namespace ArcEngine.Graphic
{
	/// <summary>
	/// A drawable convex shape
	/// </summary>
	public class Shape
	{

/*
		/// <summary>
		/// Begins a new shape
		/// </summary>
		/// <param name="x">X start point</param>
		/// <param name="y">Y start point</param>
		/// <param name="mode">Shape drawing mode</param>
		public void Begin(int x, int y, ShapeMode mode)
		{
			Begin(mode);
			GL.Vertex2(x, y);

			StartPoint = new Point(x, y);
		}

*/

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
				GL.Begin(BeginMode.Polygon);
			else
				GL.Begin(BeginMode.LineStrip);

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

			GL.End();
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
			GL.Vertex2(StartPoint.X, StartPoint.Y);
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
			GL.Vertex2(x, y);
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
			GL.Vertex2(x, y);
			LastPoint.X = x;
			LastPoint.Y = y;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <param name="control"></param>
		public void QuadraticCurveTo(Point point, Point control)
		{
			Point control1 = new Point(
				(int)(point.X + 2.0f / 3.0f * (control.X - point.X)),
				(int)(point.Y + 2.0f / 3.0f * (control.Y - point.Y)));

			Point control2 = new Point(
				(int)(control1.X + (LastPoint.X - point.X) / 3.0f),
				(int)(control1.Y + (LastPoint.Y - point.Y) / 3.0f));

			BezierCurveTo(point, control1, control2);
		}



		/// <summary>
		/// Draws a Bezier curve
		/// </summary>
		/// <param name="point"></param>
		/// <param name="control1"></param>
		/// <param name="control2"></param>
		public void BezierCurveTo(Point point, Point control1, Point control2)
		{
			Vector2[] points = new Vector2[]
			{
				new Vector2(LastPoint.X, LastPoint.Y),
				new Vector2(control1.X, control1.Y),
				new Vector2(control2.X, control2.Y),
				new Vector2(point.X, point.Y),
			};
			BezierCurve curve = new BezierCurve(points);


			//Display.Color = Color.White;

			Vector2 pos = Vector2.One;
			for (int p = 0; p <= Display.CircleResolution; p++)
			{
				pos = curve.CalculatePoint((float)p / (float)Display.CircleResolution);
				GL.Vertex2(pos.X, pos.Y);
			}


			Display.PointSize = 2;
			Display.DrawPoint(LastPoint, Color.Red);
			Display.DrawPoint(point, Color.Red);
			Display.DrawPoint(control1, Color.Green);
			Display.DrawPoint(control2, Color.Green);
			Display.PointSize = 1;


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

			int real_segments = (int)(Math.Abs(angle) / (2 * Math.PI) * (float)Display.CircleResolution) + 1;

			float theta = angle / (float)(real_segments);
			float tangetial_factor = (float)Math.Tan(theta);
			float radial_factor = (float)(1 - Math.Cos(theta));

			float xx = (float)(x + radius * Math.Cos(start));
			float yy = (float)(y + radius * Math.Sin(start));

			for (int ii = 0; ii < real_segments + 1; ii++)
			{
				GL.Vertex2(xx, yy);

				float tx = -(yy - y);
				float ty = xx - x;

				xx += tx * tangetial_factor;
				yy += ty * tangetial_factor;

				float rx = x - xx;
				float ry = y - yy;

				xx += rx * radial_factor;
				yy += ry * radial_factor;
			}

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
		/// <param name="rectangle"></param>
		/// <param name="radius"></param>
		public void RoundedRectangle(Rectangle rectangle, int radius)
		{

			//function roundedRect(ctx, x, y, width, height, radius)
			//ctx.beginPath();
			//ctx.moveTo(x,y+radius);
			
			//ctx.lineTo(x,y+height-radius);
			//ctx.quadraticCurveTo(x,y+height,x+radius,y+height);
			
			//ctx.lineTo(x+width-radius,y+height);
			//ctx.quadraticCurveTo(x+width,y+height,x+width,y+height-radius);
			
			//ctx.lineTo(x+width,y+radius);
			//ctx.quadraticCurveTo(x+width,y,x+width-radius,y);
			
			//ctx.lineTo(x+radius,y);
			//ctx.quadraticCurveTo(x,y,x,y+radius);
			//ctx.stroke();

			MoveTo(rectangle.X, rectangle.Y + radius);
			
			LineTo(rectangle.X, rectangle.Bottom - radius);
			QuadraticCurveTo(new Point(rectangle.X + radius, rectangle.Bottom), new Point(rectangle.X, rectangle.Bottom));

			LineTo(rectangle.Right - radius, rectangle.Bottom);
			QuadraticCurveTo(new Point(rectangle.Right, rectangle.Bottom), new Point(rectangle.Right, rectangle.Bottom - radius));
			
			//LineTo(rectangle.Right, rectangle.Y + radius);
			//QuadraticCurveTo(new Point(rectangle.Right, rectangle.Y), new Point(rectangle.Right - radius, rectangle.Y));
			
			//LineTo(rectangle.X + radius, rectangle.Y);
			//QuadraticCurveTo(new Point(rectangle.X, rectangle.Y), new Point(rectangle.X, rectangle.Y + radius));

		}




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
