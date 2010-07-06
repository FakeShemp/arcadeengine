using System;
using TK = OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// 
	/// </summary>
	public class IndexBuffer : IDisposable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public IndexBuffer()
		{
			Usage = BufferUsage.StaticDraw;
			TK.GL.GenBuffers(1, out Handle);
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		public void Dispose()
		{
			if (Handle != -1)
                TK.GL.DeleteBuffers(1, ref Handle);
			Handle = -1;

			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Destructor
		/// </summary>
		~IndexBuffer()
		{
			throw new Exception("IndexBuffer : Call Dispose() !!");
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		public void Update(int[] buffer)
		{
			if (Handle == -1 || buffer == null)
				return;

			// Set the index buffer
			Bind();

			// Update data
            TK.GL.BufferData<int>(TK.BufferTarget.ElementArrayBuffer, (IntPtr)(buffer.Length * sizeof(int)), buffer, (TK.BufferUsageHint) Usage);

			Count = buffer.Length;
		}


		/// <summary>
		/// Binds the buffer
		/// </summary>
		/// <returns></returns>
		internal bool Bind()
		{
			if (Handle == -1)
				return false;

            TK.GL.BindBuffer(TK.BufferTarget.ElementArrayBuffer, Handle);

			return true;
		}



		#region Properties

		/// <summary>
		/// Handle of the buffer
		/// </summary>
		int Handle;


		/// <summary>
		/// Usage of the buffer
		/// </summary>
		public BufferUsage Usage
		{
			get;
			set;
		}


		/// <summary>
		/// Number of element in the buffer
		/// </summary>
		public int Count
		{
			get;
			private set;
		}


		#endregion
	}
}
