using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;
using OpenTK;



//
// http://www.spec.org/gwpg/gpc.static/vbo_whitepaper.html
//
namespace ArcEngine.Examples
{
	/// <summary>
	/// This type of buffer is used mainly for the element pointer. It contains only indices of elements. 
	/// </summary>
	public class ElementBuffer
	{

		/// <summary>
		/// 
		/// </summary>
		public ElementBuffer()
		{
			GL.GenBuffers(1, out Handle);

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public void Update(int[] data)
		{
			//GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
			//GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * indicesVboData.Length), indicesVboData, BufferUsageHint.StaticDraw);


			try
			{
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
				GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(data.Length * sizeof(uint)), data, BufferUsageHint.StaticDraw);
			}
			catch (Exception e)
			{
				bool er = GL.IsBuffer(Handle);
				Trace.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
			}


		}


		/// <summary>
		/// 
		/// </summary>
		public void Bind()
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
		}



		#region Properties

		/// <summary>
		/// Buffer internal handle
		/// </summary>
		int Handle;



		/// <summary>
		/// Size of an element
		/// </summary>
		int ElementSize;



		/// <summary>
		/// Enable index
		/// </summary>
		public int Index
		{
			get;
			private set;
		}


		#endregion

	}
}
