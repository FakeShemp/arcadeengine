using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Pixel Buffer
	/// </summary>
	public class PixelBuffer
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public PixelBuffer()
		{
			GL.GenBuffers(1, out Handle);
			//GL.BindBuffer(BufferTarget.PixelUnpackBuffer, Handle);
			//GL.BufferData(BufferTarget.PixelUnpackBuffer, (IntPtr)(size.Width * size.Height * 4), IntPtr.Zero, BufferUsageHint.StreamDraw);
			//GL.BindBuffer(BufferTarget.PixelUnpackBuffer, 0);
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
			GL.MapBuffer(BufferTarget.PixelPackBuffer, access);
			GL.ReadPixels(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

			GL.BindBuffer(BufferTarget.PixelPackBuffer, 0);
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
			if (texture == null || IsLocked || Handle == null)
				return false;

			Texture = texture;
			Rectangle = texture.Rectangle;
			IsLocked = true;

			// Bind PBO
			GL.BindBuffer(BufferTarget.PixelUnpackBuffer, Handle);

			// Copy the texture to the PBO
			GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, Texture.Size.Width, Texture.Size.Height, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

		//	if (Data == null || Data.Length != Size.Width * Size.Height * 4)
		//		Data = new byte[Size.Width * Size.Height * 4];

			// Map PBO to client memory
			IntPtr ptr = GL.MapBuffer(BufferTarget.PixelUnpackBuffer, BufferAccess.ReadWrite);

			

			return false;
		}


		/// <summary>
		/// Unmap the buffer
		/// </summary>
		public void Unmap()
		{
			GL.BindBuffer(BufferTarget.PixelUnpackBuffer, Handle);

			if (Texture != null)
			{

			}
			else
			{
				GL.UnmapBuffer(BufferTarget.PixelPackBuffer);
			}

			IsLocked = false;
			GL.BindBuffer(BufferTarget.PixelPackBuffer, 0);
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


		Texture Texture;


		#endregion
	}
}
