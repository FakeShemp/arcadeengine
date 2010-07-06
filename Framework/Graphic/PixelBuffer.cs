#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using TK = OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Pixel Buffer
	/// </summary>
	public class PixelBuffer : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public PixelBuffer()
		{
            TK.GL.GenBuffers(1, out Handle);
			//GL.BindBuffer(BufferTarget.PixelUnpackBuffer, Handle);
			//GL.BufferData(BufferTarget.PixelUnpackBuffer, (IntPtr)(size.Width * size.Height * 4), IntPtr.Zero, BufferUsageHint.StreamDraw);
			//GL.BindBuffer(BufferTarget.PixelUnpackBuffer, 0);
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~PixelBuffer()
		{
			throw new Exception("PixelBuffer : Handle (id=" + Handle.ToString() + ") != -1, Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
            TK.GL.DeleteBuffers(1, ref Handle);
			Handle = -1;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;

			GC.SuppressFinalize(this);
		}



		/// <summary>
		/// Maps the current frame buffer
		/// </summary>
		/// <param name="rectangle">Rectangle</param>
		/// <param name="access">Access mode</param>
		/// <returns></returns>
        public bool MapFrameBuffer(Rectangle rectangle, BufferAccess access)
		{
			Texture = null;
			Rectangle = Rectangle.Empty;

			// No buffer available
			if (Handle == 0 || IsLocked)
				return false;

			// Reserve enough space
			Rectangle = rectangle;
			if (Data == null || Data.Length != Size.Width * Size.Height * 4)
				Data = new byte[Size.Width * Size.Height * 4];

			Access = access;
			IsLocked = true;

			// Write only, no need to get the content
			if (Access == BufferAccess.WriteOnly)
				return true;

			// Read the frame buffer
            TK.GL.MapBuffer(TK.BufferTarget.PixelPackBuffer, (TK.BufferAccess)access);
            TK.GL.ReadPixels(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, TK.PixelFormat.Bgra, TK.PixelType.UnsignedByte, IntPtr.Zero);

            TK.GL.BindBuffer(TK.BufferTarget.PixelPackBuffer, 0);
			return true;

			

		}



		/// <summary>
		/// Maps a texture
		/// </summary>
		/// <param name="texture">Texture to map</param>
		/// <param name="access">Access mode</param>
		/// <returns></returns>
		public bool MapTexture(Texture texture, BufferAccess access)
		{
			if (texture == null || IsLocked || Handle == 0)
				return false;

			Texture = texture;
			Rectangle = texture.Rectangle;
			IsLocked = true;

			// Bind PBO
            TK.GL.BindBuffer(TK.BufferTarget.PixelUnpackBuffer, Handle);

			// Copy the texture to the PBO
            TK.GL.TexSubImage2D(TK.TextureTarget.Texture2D, 0, 0, 0, Texture.Size.Width, Texture.Size.Height, TK.PixelFormat.Bgra, TK.PixelType.UnsignedByte, IntPtr.Zero);

		//	if (Data == null || Data.Length != Size.Width * Size.Height * 4)
		//		Data = new byte[Size.Width * Size.Height * 4];

			// Map PBO to client memory
            IntPtr ptr = TK.GL.MapBuffer(TK.BufferTarget.PixelUnpackBuffer, (TK.BufferAccess)BufferAccess.ReadWrite);

			

			return false;
		}


		/// <summary>
		/// Unmap the buffer
		/// </summary>
		public void Unmap()
		{
            TK.GL.BindBuffer(TK.BufferTarget.PixelUnpackBuffer, Handle);

			if (Texture != null)
			{

			}
			else
			{
                TK.GL.UnmapBuffer(TK.BufferTarget.PixelPackBuffer);
			}

			IsLocked = false;
            TK.GL.BindBuffer(TK.BufferTarget.PixelPackBuffer, 0);
		}




		#region Properties


		/// <summary>
		/// Buffer handle
		/// </summary>
		int Handle;

		/// <summary>
		/// Data of the buffer
		/// </summary>
		public byte[] Data
		{
			get;
			set;
		}


		/// <summary>
		/// Access mode
		/// </summary>
        BufferAccess Access;


		/// <summary>
		/// Returns the lock status of the texture
		/// </summary>
		public bool IsLocked
		{
			get;
			protected set;
		}


		/// <summary>
		/// Size of the buffer
		/// </summary>
		public Size Size
		{
			get
			{
				return Rectangle.Size;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		Rectangle Rectangle;



		/// <summary>
		/// 
		/// </summary>
		Texture Texture;


		#endregion
	}



    /// <summary>
    /// 
    /// </summary>
    public enum BufferAccess
    {
        /// <summary>
        /// 
        /// </summary>
        ReadOnly = TK.BufferAccess.ReadOnly,

        /// <summary>
        /// 
        /// </summary>
        WriteOnly = TK.BufferAccess.WriteOnly,

        /// <summary>
        /// 
        /// </summary>
        ReadWrite = TK.BufferAccess.ReadWrite,
    }

}
