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
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// An efficient way to render a batch of geometry
	/// </summary>
	/// http://songho.ca/opengl/gl_vbo.html
	public class Batch : IDisposable
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Batch() : this(1, 0)
		{

		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="texturecount">Number of texture buffer</param>
		/// <param name="attribcount">Number of attrib buffer</param>
		public Batch(int texturecount, int attribcount)
		{
			int[] id = new int[1];
			GL.GenBuffers(1, id);
			Handle = id[0];
			Buffer = new List<float>();
		}


		/// <summary>
		/// Clear the batch
		/// </summary>
		public void Clear()
		{
			Buffer.Clear();
		}




		/// <summary>
		/// Apply updates
		/// </summary>
		public void Apply()
		{
			// Update buffer
			GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Buffer.Count * sizeof(float)), Buffer.ToArray(), BufferUsageHint.StaticDraw);
		}


		#region No multitexturing

		/// <summary>
		/// Adds a rectangle
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="color">Drawing color</param>
		/// <param name="tex">Texture coordinate</param>
		public void AddRectangle(Rectangle rect, Color color, Rectangle tex)
		{
			AddPoint(rect.Location, color, tex.Location);
			AddPoint(new Point(rect.Right, rect.Top), color, new Point(tex.Right, tex.Top));
			AddPoint(new Point(rect.Right, rect.Bottom), color, new Point(tex.Right, tex.Bottom));
			AddPoint(new Point(rect.X, rect.Bottom), color, new Point(tex.X, tex.Bottom));
		}


		/// <summary>
		/// Adds a point
		/// </summary>
		/// <param name="point">Location on the screen</param>
		/// <param name="color">Color of the point</param>
		/// <param name="texture">Texture coordinate</param>
		public void AddPoint(Point point, Color color, Point texture)
		{
			// Vertex
			Buffer.Add(point.X);
			Buffer.Add(point.Y);


			return;

			// Color
			Buffer.Add(color.R / 255);
			Buffer.Add(color.G / 255);
			Buffer.Add(color.B / 255);
			Buffer.Add(color.A / 255);

			// Texture
			Buffer.Add(texture.X);
			Buffer.Add(texture.Y);
		}


		/// <summary>
		/// Adds a point
		/// </summary>
		/// <param name="point">Location on the screen</param>
		/// <param name="color">Color of the point</param>
		public void AddPoint(Point point, Color color)
		{
			AddPoint(point, color, Point.Empty);
		}


		/// <summary>
		/// Adds a line
		/// </summary>
		/// <param name="from">Start point</param>
		/// <param name="to">Ending point</param>
		/// <param name="color">Color of the line</param>
		public void AddLine(Point from, Point to, Color color)
		{
			AddPoint(from, color);
			AddPoint(to, color);
		}

		#endregion



/*
		#region Multitexturing


		/// <summary>
		/// Adds a point
		/// </summary>
		/// <param name="point">Location on the screen</param>
		/// <param name="color">Color of the point</param>
		/// <param name="texture">Texture coordinates</param>
		public void AddPoint(Point point, Color color, Point[] texture)
		{
			VertexBuffer.Add(point);
			for (int id = 0; id < TextureBufferCount; id++)
			{
				if (id <= texture.Length)
					TextureBuffer[id].Add(texture[id]);
			}

			ColorBuffer.Add((color.A << 24) + (color.B << 16) + (color.G << 8) + (color.R));
		}


		/// <summary>
		/// Adds a rectangle
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="color">Drawing color</param>
		/// <param name="tex">Texture coordinates</param>
		public void AddRectangle(Rectangle rect, Color color, Rectangle[] tex)
		{
			if (tex.Length < TextureBufferCount * 4)
				throw new ArgumentOutOfRangeException("Not enough texture coordinates. Waiting for " + TextureBufferCount * 4 +", got " + tex.Length);
			Point[] points = new Point[TextureBufferCount];

			//AddPoint(rect.Location, color, tex.Location);
			for (int id = 0; id < TextureBufferCount; id++)
				points[id] = tex[id].Location;
			AddPoint(rect.Location, color, points);


			//AddPoint(new Point(rect.Right, rect.Top), color, new Point(tex.Right, tex.Top));
			for (int id = 0; id < TextureBufferCount; id++)
				points[id] = new Point(tex[id].Right, tex[id].Top);
			AddPoint(new Point(rect.Right, rect.Top), color, points);


			//AddPoint(new Point(rect.Right, rect.Bottom), color, new Point(tex.Right, tex.Bottom));
			for (int id = 0; id < TextureBufferCount; id++)
				points[id] = new Point(tex[id].Right, tex[id].Bottom);
			AddPoint(new Point(rect.Right, rect.Bottom), color, points);


			//AddPoint(new Point(rect.X, rect.Bottom), color, new Point(tex.X, tex.Bottom));
			for (int id = 0; id < TextureBufferCount; id++)
				points[id] = new Point(tex[id].X, tex[id].Bottom);
			AddPoint(new Point(rect.X, rect.Bottom), color, points);
		}



		#endregion
*/



		#region Disposing
		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			int[] id = new int[]{Handle};
			GL.DeleteBuffers(1, id);
			Handle = -1;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Gets the size of the buffer
		/// </summary>
		public int Size
		{
			get
			{
				return Buffer.Count;
			}
		}


		/// <summary>
		/// Buffer ID
		/// </summary>
		public int Handle
		{
			get;
			private set;
		}
		


		/// <summary>
		/// 
		/// </summary>
		internal List<float> Buffer
		{
			get;
			private set;
		}


		#endregion

	}
}
