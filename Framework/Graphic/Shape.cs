using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// A drawable convex shape
	/// </summary>
	public class Shape
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Shape()
		{
			Batch = new Batch();
			Scale = new SizeF(1, 1);
		}


		/// <summary>
		/// Reset
		/// </summary>
		public void Begin()
		{

			
		}


		public void Close()
		{
		}


		public void MoveTo(Point point)
		{
		}


		public void LineTo(Point point)
		{
		}


		public void QuadraticCurveTo(float cpx, float cpy, float x, float y)
		{
		}


		public void BezierCurveTo(Point from, Point to, float radius)
		{
		}


		public void ArcTo(Point from, Point to, float radius)
		{
		}


		public void Rectangle(Rectangle rectangle)
		{
		}


		public void Fill()
		{
		}


		public void Stroke()
		{
		}


		public void Clip()
		{
		}





		#region Properties

		/// <summary>
		/// Batch
		/// </summary>
		Batch Batch;


		/// <summary>
		/// Location of the Shape
		/// </summary>
		public Point Location
		{
			get;
			set;
		}


		/// <summary>
		/// Rotation
		/// </summary>
		public float Rotation
		{
			get;
			set;
		}


		/// <summary>
		/// Scale
		/// </summary>
		public SizeF Scale
		{
			get;
			set;
		}


		#endregion
	}
}
