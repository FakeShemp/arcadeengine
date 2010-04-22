using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace ArcEngine.Examples
{

	/// <summary>
	/// 
	/// </summary>
	public class DrawBatch : IDisposable
	{

		/// <summary>
		/// 
		/// </summary>
		public DrawBatch()
		{
			ElementBuffer = new ElementBuffer();

			GL.GenVertexArrays(1, out Handle);
			GL.BindVertexArray(Handle);
		}


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
			ElementBuffer.Update(buffer);
		}


		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			if (Handle != -1)
			{
				GL.DeleteVertexArrays(1, ref Handle);
				Handle = -1;
			}
		}



		#region Properties

		/// <summary>
		/// VAO handle
		/// </summary>
		int Handle;


		/// <summary>
		/// 
		/// </summary>
		ElementBuffer ElementBuffer;


		#endregion
	}
}
