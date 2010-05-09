using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;



//
// http://www.spec.org/gwpg/gpc.static/vbo_whitepaper.html
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/

namespace ArcEngine.Examples
{
	/// <summary>
	/// This type of buffer is used mainly for the element pointer. It contains only indices of elements. 
	/// </summary>
	public class IndexBuffer : IDisposable
	{

		/// <summary>
		/// Cosntructor
		/// </summary>
		public IndexBuffer()
		{
			GL.GenBuffers(1, out indexHandle);
			GL.GenBuffers(1, out vertexHandle);
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~IndexBuffer()
		{
			if (indexHandle != -1)
				throw new Exception("IndexBuffer : Handle != -1, Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			GL.DeleteBuffers(1, ref indexHandle);
			indexHandle = -1;

			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Updates indices
		/// </summary>
		/// <param name="data"></param>
		public void UpdateIndices(uint[] data)
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(data.Length * sizeof(uint)), data, BufferUsageHint.StaticDraw);

			Count = data.Length;
		}


		/// <summary>
		/// Updates vertices
		/// </summary>
		/// <param name="data"></param>
		public void UpdateVertices(float[] data)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * sizeof(float)), data, BufferUsageHint.StaticDraw);
		}



		/// <summary>
		/// Binds the buffer
		/// </summary>
		public void Bind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);
			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
		}



		/// <summary>
		/// Defines vertex attribute data
		/// </summary>
		/// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, or 4.</param>
		/// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. 
		/// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. </param>
		/// <param name="offset">Specifies a pointer to the first component of the first generic vertex attribute in the array. </param>
		public void SetDeclaration(int index, int size, int stride, int offset)
		{
			GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, stride, offset);
		}



		#region Properties

		/// <summary>
		/// Buffer internal handle
		/// </summary>
	//	int Handle;

		int vaoHandle;

		int indexHandle;


		int vertexHandle;


		/// <summary>
		/// Usage mode
		/// </summary>
		public BufferUsageHint UsageMode
		{
			get;
			set;
		}



		/// <summary>
		/// Number of element
		/// </summary>
		public int Count
		{
			get;
			private set;
		}

		#endregion


	}
}
