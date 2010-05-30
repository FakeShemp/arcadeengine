using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ArcEngine.Asset;


//
// http://www.spec.org/gwpg/gpc.static/vbo_whitepaper.html
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/

namespace ArcEngine.Graphic
{
	/// <summary>
	/// User buffer 
	/// </summary>
	public class UserBuffer : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public UserBuffer()
		{
			GL.GenBuffers(1, out vertexHandle);
			//GL.GenVertexArrays(1, out vaoHandle);

			UsageMode = BufferUsageHint.StaticDraw;

			Declarations = new List<VertexDeclaration>();
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~UserBuffer()
		{
			if (vertexHandle != -1)
				throw new Exception("UserBuffer : Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			GL.DeleteBuffers(1, ref vertexHandle);
			vertexHandle = -1;

			//GL.DeleteVertexArrays(1, ref vaoHandle);
			//vaoHandle = -1;

			Count = 0;

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

			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(count * sizeof(float)), data, UsageMode);
		}



		/// <summary>
		/// Binds the buffer
		/// </summary>
		/// <param name="shader">Shader to use</param>
		public void Bind(Shader shader)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
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



		#region Properties

		/// <summary>
		/// Buffer internal handles
		/// </summary>
		//int indexHandle;
		public int vertexHandle;
		//int vaoHandle;


		/// <summary>
		/// Usage mode
		/// </summary>
		public BufferUsageHint UsageMode
		{
			get;
			set;
		}



		/// <summary>
		/// Number of indices
		/// </summary>
		public int Count
		{
			get;
			private set;
		}


		/// <summary>
		/// Vertex declarations
		/// </summary>
		List<VertexDeclaration> Declarations;

		#endregion


	}

}
