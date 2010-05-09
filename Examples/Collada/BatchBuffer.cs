using System;
using System.Collections.Generic;
using System.Text;

using ArcEngine.Graphic;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace ArcEngine.Examples
{

	/// <summary>
	/// Batch buffer
	/// </summary>
	public class BatchBuffer : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public BatchBuffer()
		{
			ElementBuffer = new IndexBuffer();

			GL.GenVertexArrays(1, out Handle);
			GL.BindVertexArray(Handle);
		}


		/// <summary>
		/// 
		/// </summary>
		public void Bind()
		{
			GL.BindVertexArray(Handle);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		public void Draw(BeginMode mode)
		{
			GL.BindVertexArray(Handle);
			GL.DrawElements(mode, ElementBuffer.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		public void SetIndices(uint[] buffer)
		{
			ElementBuffer.UpdateIndices(buffer);
		}



		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Handle != -1)
				GL.DeleteVertexArrays(1, ref Handle);
			Handle = -1;

			if (ElementBuffer != null)
				ElementBuffer.Dispose();
			ElementBuffer = null;

		}



		#region Properties

		/// <summary>
		/// VAO handle
		/// </summary>
		int Handle;


		/// <summary>
		/// 
		/// </summary>
		IndexBuffer ElementBuffer;


		#endregion
	}
}
