using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ArcEngine.Asset;
using System.Drawing;

//
// http://www.spec.org/gwpg/gpc.static/vbo_whitepaper.html
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Vertex buffer
	/// </summary>
	public class BatchBuffer : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public BatchBuffer()
		{
			int id = 0;
			GL.GenBuffers(1, out id);
			Handle = id;

			UsageMode = BufferUsageHint.StaticDraw;

			Declarations = new List<VertexDeclaration>();

			Buffer = new List<float>();
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~BatchBuffer()
		{
			if (Handle != -1)
				throw new Exception("BatchBuffer : Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			int id = Handle;
			GL.DeleteBuffers(1, ref id);
			Handle = -1;

			//GL.DeleteVertexArrays(1, ref vaoHandle);
			//vaoHandle = -1;


			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Sets vertices
		/// </summary>
		/// <param name="data">Data to send to the buffer</param>
		public void SetVertices(float[] data)
		{
			int count = 0;
			if (data != null)
				count = data.Length;

			GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(count * sizeof(float)), data, UsageMode);
		}


		/// <summary>
		/// Updates the buffer with the internal vertex data
		/// </summary>
		/// <returns>Number of primitives</returns>
		public int Update()
		{
			SetVertices(Buffer.ToArray());

			int count = Buffer.Count / 8;
			Buffer.Clear();

			return count;
		}


		/// <summary>
		/// Binds the buffer
		/// </summary>
		/// <param name="shader">Shader to use</param>
		public void Bind(Shader shader)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
			//GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);

			if (shader == null)
				return;

			foreach (VertexDeclaration dec in Declarations)
			{
				int id = shader.GetAttribute(dec.Name);

				Display.EnableBufferIndex(id);
				Display.SetBufferDeclaration(id, dec.Size, dec.Stride, dec.Offset);
			}

		}


		/// <summary>
		/// Adds a vertex declation to the buffer
		/// </summary>
		/// <param name="name">Specifies the name of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute.</param>
		/// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. 
		/// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. </param>
		/// <param name="offset">Specifies a pointer to the first component of the first generic vertex attribute in the array. </param>
		public void AddDeclaration(string name, int size, int stride, int offset)
		{
			Declarations.Add(new VertexDeclaration(name, size, stride, offset));
		}


		/// <summary>
		/// Clear vertex declarations
		/// </summary>
		public void ClearDeclaration()
		{
			Declarations.Clear();
		}



		/// <summary>
		/// Creates a defaut buffer containing position, color and texture data
		/// </summary>
		/// <returns></returns>
		public static BatchBuffer CreatePositionColorTextureBuffer()
		{
			BatchBuffer buffer = new BatchBuffer();
			buffer.AddDeclaration("in_position", 2, sizeof(float) * 8, 0);
			buffer.AddDeclaration("in_color", 4, sizeof(float) * 8, sizeof(float) * 2);
			buffer.AddDeclaration("in_texture", 2, sizeof(float) * 8, sizeof(float) * 6);

			return buffer;
		}



		#region Vertex PositionColorTexture helpers

		/// <summary>
		/// Clear the buffer
		/// </summary>
		public void Clear()
		{
			Buffer.Clear();
		}


		/// <summary>
		/// Adds a rectangle
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="color">Drawing color</param>
		public void AddRectangle(Rectangle rect, Color color)
		{
			AddRectangle(rect, color, Rectangle.Empty);
		}


		/// <summary>
		/// Adds a rectangle
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="color">Drawing color</param>
		/// <param name="tex">Texture coordinate</param>
		public void AddRectangle(Rectangle rect, Color color, Rectangle tex)
		{
			AddPoint(new Point(rect.X, rect.Bottom), color, new Point(tex.X, tex.Bottom));				// D
			AddPoint(rect.Location, color, tex.Location);															// A
			AddPoint(new Point(rect.Right, rect.Bottom), color, new Point(tex.Right, tex.Bottom));		// C

			AddPoint(rect.Location, color, tex.Location);															// A
			AddPoint(new Point(rect.Right, rect.Bottom), color, new Point(tex.Right, tex.Bottom));		// C
			AddPoint(new Point(rect.Right, rect.Top), color, new Point(tex.Right, tex.Top));				// B

		}


		/// <summary>
		/// Adds a rectangle
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="color">Drawing color for each corner</param>
		/// <param name="tex">Texture coordinate</param>
		public void AddRectangle(Rectangle rect, Color[] color, Rectangle tex)
		{
			if (color.Length != 4)
				return;

			AddPoint(rect.Location, color[0], tex.Location);
			AddPoint(new Point(rect.Right, rect.Top), color[1], new Point(tex.Right, tex.Top));
			AddPoint(new Point(rect.X, rect.Bottom), color[2], new Point(tex.X, tex.Bottom));
			AddPoint(new Point(rect.Right, rect.Bottom), color[3], new Point(tex.Right, tex.Bottom));
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

			// Color
			Buffer.Add(color.R / 255.0f);
			Buffer.Add(color.G / 255.0f);
			Buffer.Add(color.B / 255.0f);
			Buffer.Add(color.A / 255.0f);

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





		#region Properties

		/// <summary>
		/// Buffer internal handles
		/// </summary>
		internal int Handle
		{
			get;
			private set;
		}


		/// <summary>
		/// Usage mode
		/// </summary>
		public BufferUsageHint UsageMode
		{
			get;
			set;
		}



		/// <summary>
		/// Vertex declarations
		/// </summary>
		List<VertexDeclaration> Declarations;



		/// <summary>
		/// Vertex buffer data
		/// </summary>
		List<float> Buffer;


		#endregion


	}

	/// <summary>
	/// Declares the format of a set of vertex inputs
	/// </summary>
	struct VertexDeclaration
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Specifies the name of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute.</param>
		/// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. 
		/// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. </param>
		/// <param name="offset">Specifies a pointer to the first component of the first generic vertex attribute in the array. </param>
		public VertexDeclaration(string name, int size, int stride, int offset)
		{
			Name = name;
			Size = size;
			Stride = stride;
			Offset = offset;
		}

		/// <summary>
		/// Name of the attribute
		/// </summary>
		public string Name;


		/// <summary>
		/// Size of the elements
		/// </summary>
		public int Size;

		/// <summary>
		/// Stride between elements
		/// </summary>
		public int Stride;


		/// <summary>
		/// Offset from the start
		/// </summary>
		public int Offset;
	}

}
