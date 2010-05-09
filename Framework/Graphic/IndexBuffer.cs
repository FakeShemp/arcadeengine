using System;
using OpenTK.Graphics.OpenGL;



//
// http://www.spec.org/gwpg/gpc.static/vbo_whitepaper.html
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/

namespace ArcEngine.Graphic
{
	/// <summary>
	/// This type of buffer is used mainly for the element pointer. It contains only indices of elements. 
	/// </summary>
	public class IndexBuffer : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public IndexBuffer()
		{
			GL.GenBuffers(1, out indexHandle);
			GL.GenBuffers(1, out vertexHandle);
			//GL.GenVertexArrays(1, out vaoHandle);

			UsageMode = BufferUsageHint.StaticDraw;
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~IndexBuffer()
		{
			if (indexHandle != -1)
				throw new Exception("IndexBuffer : Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			GL.DeleteBuffers(1, ref indexHandle);
			indexHandle = -1;

			GL.DeleteBuffers(1, ref vertexHandle);
			vertexHandle = -1;

			//GL.DeleteVertexArrays(1, ref vaoHandle);
			//vaoHandle = -1;

			Count = 0;

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
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * sizeof(float)), data, UsageMode);
		}



		/// <summary>
		/// Binds the buffer
		/// </summary>
		public void Bind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);
		}





		#region Properties

		/// <summary>
		/// Buffer internal handles
		/// </summary>
		int indexHandle;
		int vertexHandle;
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
