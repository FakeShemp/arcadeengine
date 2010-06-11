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

			int count = Buffer.Count / Stride;
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

			if (shader == null)
				return;

			int offset = 0;
			foreach (VertexDeclaration dec in Declarations)
			{
				int id = shader.GetAttribute(dec.Name);

				Display.EnableBufferIndex(id);
				Display.SetBufferDeclaration(id, dec.Size, Stride * sizeof(float), offset);

				offset += dec.Size * sizeof(float);
			}

		}


		/// <summary>
		/// Adds a vertex declation to the buffer
		/// </summary>
		/// <param name="name">Specifies the name of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute.</param>
		public void AddDeclaration(string name, int size)
		{
			Declarations.Add(new VertexDeclaration(name, size));

			Stride += size;
		}



		/// <summary>
		/// Clear vertex declarations
		/// </summary>
		public void ClearDeclaration()
		{
			Declarations.Clear();
			Stride = 0;
		}



		/// <summary>
		/// Creates a defaut buffer containing position, color and texture data
		/// </summary>
		/// <returns></returns>
		public static BatchBuffer CreatePositionColorTextureBuffer()
		{
			BatchBuffer buffer = new BatchBuffer();
			buffer.AddDeclaration("in_position", 2);
			buffer.AddDeclaration("in_color", 4);
			buffer.AddDeclaration("in_texture", 2);

			return buffer;
		}



		/// <summary>
		/// Creates a defaut buffer containing position, color and texture data
		/// </summary>
		/// <returns></returns>
		public static BatchBuffer CreatePositionColorBuffer()
		{
			BatchBuffer buffer = new BatchBuffer();
			buffer.AddDeclaration("in_position", 2);
			buffer.AddDeclaration("in_color", 4);

			return buffer;
		}


		/// <summary>
		/// Clear the buffer
		/// </summary>
		public void Clear()
		{
			Buffer.Clear();
		}

		#region Helpers



		/// <summary>
		/// Adds a colored rectangle
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="color">Drawing color</param>
		public void AddRectangle(Rectangle rect, Color color)
		{
			AddPoint(new Point(rect.X, rect.Bottom), color);				// D
			AddPoint(rect.Location, color);										// A
			AddPoint(new Point(rect.Right, rect.Bottom), color);			// C

			AddPoint(rect.Location, color);										// A
			AddPoint(new Point(rect.Right, rect.Bottom), color);			// C
			AddPoint(new Point(rect.Right, rect.Top), color);				// B
		}


		/// <summary>
		/// Adds a textured rectangle
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
		/// Adds a textured rectangle
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
		/// Adds a textured point
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
		/// Adds a colored point
		/// </summary>
		/// <param name="point">Location on the screen</param>
		/// <param name="color">Color of the point</param>
		public void AddPoint(Point point, Color color)
		{
			// Vertex
			Buffer.Add(point.X);
			Buffer.Add(point.Y);

			// Color
			Buffer.Add(color.R / 255.0f);
			Buffer.Add(color.G / 255.0f);
			Buffer.Add(color.B / 255.0f);
			Buffer.Add(color.A / 255.0f);
		}


		/// <summary>
		/// Adds a colored line
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


		/// <summary>
		/// Stride
		/// </summary>
		public int Stride
		{
			get;
			private set;
		}

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
		public VertexDeclaration(string name, int size)
		{
			Name = name;
			Size = size;
		}

		/// <summary>
		/// Name of the attribute
		/// </summary>
		public string Name;


		/// <summary>
		/// Size of the elements
		/// </summary>
		public int Size;

	}

}
