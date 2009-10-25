#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Imaging = System.Drawing.Imaging;


//
//
//
//
//
//
//
//
//

namespace ArcEngine.Graphic
{

	/// <summary>
	/// Spécifie la position de l'image sur le contrôle.
	/// </summary>
	public enum TextureLayout
	{
		/// <summary>
		/// Image is top left aligned
		/// </summary>
		None = 0,

		/// <summary>
		/// The image is tiled
		/// </summary>
		Tile = 1,

		/// <summary>
		/// The image is centered
		/// </summary>
		Center = 2,

		/// <summary>
		/// The image is stretched
		/// </summary>
		Stretch = 3,

		/// <summary>
		/// The image is zoomed (proportions keeped)
		/// </summary>
		Zoom = 4,
	}



	/// <summary>
	/// Texture definition
	/// </summary>
	public class Texture : IDisposable
	{

		#region ctor / dtor

		/// <summary>
		/// Constructor
		/// </summary>
		public Texture()
		{
			Handle = GL.GenTexture();
		}

		/// <summary>
		/// Creates an empty texture
		/// </summary>
		/// <param name="size">Size of the texture to create</param>
		public Texture(Size size) : this()
		{
			Display.Texture = this;


			// The below is almost OK. The problem is the GL_RGBA. On certain platforms, the GPU prefers that red and blue be swapped (GL_BGRA).
			// If you supply GL_RGBA, then the driver will do the swapping for you which is slow.
			// http://www.opengl.org/wiki/Common_Mistakes#Unsupported_formats_.234
			//GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Size.Width, Size.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
			//GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, Size.Width, Size.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
			//LockTextureBits(ImageLockMode.WriteOnly);
			//Data = null;
			//UnlockTextureBits();
			SetSize(size);

			MinFilter = TextureMinFilter.Linear;
			MagFilter = TextureMagFilter.Linear;
			VerticalWrap = VerticalWrapFilter.Clamp;
			HorizontalWrap = HorizontalWrapFilter.Clamp;
		}

		/// <summary>
		/// Loads an image from the disk
		/// </summary>
		/// <param name="filename">Image's name</param>
		public Texture(string filename) : this()
		{
			LoadImage(filename);
		}


		/// <summary>
		/// Creates a texture from a Stream
		/// </summary>
		/// <param name="stream">Stream handle</param>
		/// <remarks>The Stream is closed automatically</remarks>
		public Texture(Stream stream) : this()
		{
			if (stream == null)
				return;

			LoadImage(stream);

			stream.Close();
		}


		/// <summary>
		/// Destructor
		/// TODO
		/// </summary>
		~Texture()
		{
		//	Dispose(false);
		}


		#endregion


		/// <summary>
		/// Sets the size of the texture
		/// </summary>
		/// <param name="size"></param>
		public void SetSize(Size size)
		{
			Size = size;

			LockTextureBits(ImageLockMode.WriteOnly);
			Data = null;
			UnlockTextureBits();
		}

		#region Blitting



		/// <summary>
		/// Draws a Texture on the screen and resize it
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="tex">Rectangle in the texture</param>
		/// <param name="mode">Rendering mode</param>
		public void Blit(Rectangle rect, Rectangle tex, TextureLayout mode)
		{
			Display.Texture = this;

			// Display mode
			switch (mode)
			{
				default:
				{
					Blit(tex, tex);
				}
				break;


				case TextureLayout.Stretch:
				{
					Blit(rect, tex);
				}
				break;


				case TextureLayout.Center:
				{
					Point pos = new Point(
						(rect.Width - Size.Width) / 2,
						(rect.Height - Size.Height) / 2
						);
					pos.Offset(rect.Location);
					Blit(pos);
				}
				break;


				case TextureLayout.Tile:
				{
					//device.ScissorZone = rect;
					//device.Scissor = true;

					GL.Begin(BeginMode.Quads);
					for (int y = rect.Location.Y; y <= rect.Location.Y + rect.Height; y += tex.Size.Height)
						for (int x = rect.Location.X; x <= rect.Location.X + rect.Width; x += tex.Size.Width)
						{
							//	Blit(Texture, new Point(x, y));

							Display.RawBlit(new Rectangle(x, y, tex.Width, tex.Height), tex);


						}
					GL.End();

					//device.Scissor = false; 
 
				}
				break;

				case TextureLayout.Zoom:
				{
					int value = Math.Min(rect.Width - Size.Width, rect.Height - Size.Height);

					Rectangle final = new Rectangle(
						0, 0,
						Size.Width + value, Size.Height + value);

					final.Location = new Point((rect.Width - final.Width) / 2,
												(rect.Height - final.Height) / 2);

					Blit(final, Rectangle);

				}
				break;
			}
		}


		/// <summary>
		/// Blits a texture to the screen.
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="tex">Rectangle in the texture</param>
		public void Blit(Rectangle rect, Rectangle tex)
		{
			Display.Texture = this;
			Display.RawBlit(rect, tex);
		}


		/// <summary>
		/// Blits a texture on the screen
		/// </summary>
		/// <param name="pos"></param>
		public void Blit(Point pos)
		{
			Display.Texture = this;
			Display.RawBlit(new Rectangle(pos, Size), Rectangle);
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="angle"></param>
        /// <param name="origin"></param>
        public void Blit(Point pos, float angle, Point origin)
        {
            Display.Translate(pos.X + origin.X, pos.Y + origin.Y);
            Display.Rotate(angle);

            Blit(new Point(-origin.X, -origin.Y));

            Display.DefaultMatrix();
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="mode"></param>
		public void Blit(Rectangle rect, TextureLayout mode)
		{
			Blit(rect, Rectangle, mode);
			//Blit(device, rect, Rectangle, mode);
		}


		/// <summary>
		/// Blit a Bitmap on the texture
		/// </summary>
		/// <param name="bitmap">Bitmap handle</param>
		/// <param name="location">Location on the texture</param>
		public void Blit(Bitmap bitmap, Point location)
		{
			if (bitmap == null)
			{
				Trace.WriteLine("Bitmap == null");
				return;
			}

			//Display.Texture = this;
			ErrorCode error = GL.GetError();

			Imaging.BitmapData bmdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				 Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb);





			byte[] data = new byte[bitmap.Size.Width * bitmap.Size.Height * 4];
			Marshal.Copy(bmdata.Scan0, data, 0, bitmap.Size.Width * bitmap.Size.Height * 4);
			bitmap.UnlockBits(bmdata);

			Display.Texture = this;

			GL.TexSubImage2D<byte>(TextureTarget.Texture2D, 0, 
				location.X, location.Y, 
				bitmap.Width, bitmap.Height,
				PixelFormat.Bgra, PixelType.UnsignedByte,
				data);
			error = GL.GetError();

			// Update texture content
		//	if (LockTextureBits(ImageLockMode.WriteOnly))
		//	{
				//Data = data;

/*
				for (int y = 0; y < bitmap.Height; y++)
				{
					Array.Copy(
						data, bitmap.Width * y * 4,
						Data, Size.Width * y * 4 
						+ location.X * 4 + location.Y * Size.Width * 4, 
						bitmap.Width * 4);
				}
*/
				
		//		UnlockTextureBits();
		//	}



			
			

		}


		#endregion


		#region Helper


		/// <summary>
		/// Checks if the texture size is valid (power of two)
		/// </summary>
		/// <param name="size">Size to check</param>
		/// <returns></returns>
		public static bool CheckTextureSize(Size size)
		{
		//	if (GL.SupportsExtension("ARB_texture_non_power_of_two"))
		//		return true;

			if (size.Width != NextPowerOfTwo(size.Width) || size.Height != NextPowerOfTwo(size.Height))
				return false;

			return true;
		}


		/// <summary>
		/// Give the next power of two
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected static int NextPowerOfTwo(int input)
		{
			int value = 1;

			while (value < input) value <<= 1;

			return value;
		}


		/// <summary>
		/// Returns the next Power Of Two size
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Size GetNextPOT(Size size)
		{
			return new Size(NextPowerOfTwo(size.Width), NextPowerOfTwo(size.Height));
		}

		#endregion


		#region Image IO

		/// <summary>
		/// Load an image from bank and convert it to a texture
		/// </summary>
		/// <param name="filename"></param>
		/// <returns>True if success or false if something went wrong</returns>
		public bool LoadImage(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return false;

			return LoadImage(ResourceManager.LoadResource(filename));
		}


		/// <summary>
		/// Load a texture from a stream (ie resource files)
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public bool LoadImage(Stream stream)
		{
			if (stream == null)
				return false;

			Display.Texture = this;

			Bitmap bitmap = new Bitmap(stream);

			SetSize(new Size(bitmap.Width, bitmap.Height));

			Blit(bitmap, Point.Empty);
/*
			System.Drawing.Imaging.BitmapData bmdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				 System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);




			byte[] data = new byte[bitmap.Size.Width * bitmap.Size.Height * 4];
			System.Runtime.InteropServices.Marshal.Copy(bmdata.Scan0, data, 0, bitmap.Size.Width * bitmap.Size.Height * 4);

			// Update texture content
			if (LockTextureBits(ImageLockMode.WriteOnly))
			{
				Data = data;
				UnlockTextureBits();
			}

			bitmap.UnlockBits(bmdata);
*/

			//MinFilter = MinifyFilter.Linear;
			//MagFilter = MagnifyFilter.Linear;

			return true;	
		}

		/// <summary>
		/// Loads a Png picture from a byte[]
		/// </summary>
		/// <param name="data">Binary of a png file</param>
		/// <returns>True if successful, or false</returns>
		public bool LoadImage(byte[] data)
		{
			if (data == null)
				return false;

			return LoadImage(new MemoryStream(data));
		}

		/// <summary>
		/// Save the texture to the disk as a PNG file
		/// </summary>
		/// <param name="name">Name of the texture on the disk</param>
		/// <returns>True if successful or false if an error occured</returns>
		public bool SaveToDisk(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;


			Bitmap bm = new Bitmap(Size.Width, Size.Height);

			if (!LockTextureBits(ImageLockMode.ReadOnly))
				return false;

			System.Drawing.Imaging.BitmapData bmd = bm.LockBits(new Rectangle(Point.Empty, Size),
				System.Drawing.Imaging.ImageLockMode.WriteOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			System.Runtime.InteropServices.Marshal.Copy(Data, 0, bmd.Scan0, Data.Length);
			bm.UnlockBits(bmd);
			
			UnlockTextureBits();

			bm.Save(name);

			return true;
		}

		/// <summary>
		/// Save the texture as a PNG image in the bank
		/// </summary>
		/// <param name="name">Name</param>
		/// <returns></returns>
		public bool SaveToBank(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;


			Bitmap bm = new Bitmap(Size.Width, Size.Height);

			if (!LockTextureBits(ImageLockMode.ReadOnly))
				return false;

			System.Drawing.Imaging.BitmapData bmd = bm.LockBits(new Rectangle(Point.Empty, Size),
				System.Drawing.Imaging.ImageLockMode.WriteOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			System.Runtime.InteropServices.Marshal.Copy(Data, 0, bmd.Scan0, Data.Length);
			bm.UnlockBits(bmd);

			UnlockTextureBits();

			Stream stream = new MemoryStream();
			bm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
			stream.Seek(0, SeekOrigin.Begin);

			return ResourceManager.LoadBinary(name, stream);
		}


		#endregion


		#region Locking

		/// <summary>
		/// Locks the bitmap texture to system memory
		/// </summary>
		/// <param name="mode">Access mode</param>
		/// <returns></returns>
		public bool LockTextureBits(ImageLockMode mode)
		{
			// No texture bounds
			if (Handle == 0 || IsLocked)
				return false;

			Data = new byte[Size.Width * Size.Height * 4];

			LockMode = mode;
			IsLocked = true;

			if (mode == ImageLockMode.WriteOnly)
				return true;


			Display.Texture = this;
			//GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, Data);
			GL.GetTexImage<byte>(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, Data);
			return true;
		}


		/// <summary>
		/// Unlocks the texture's bitmap from system memory.
		/// </summary>
		public void UnlockTextureBits()
		{
			if (!IsLocked || LockMode == ImageLockMode.ReadOnly)
			{
				IsLocked = false;
				return;
			}

			Display.Texture = this;

			// The below is almost OK. The problem is the GL_RGBA. On certain platforms, the GPU prefers that red and blue be swapped (GL_BGRA).
			// If you supply GL_RGBA, then the driver will do the swapping for you which is slow.
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8,
				Size.Width, Size.Height,
				0,
				PixelFormat.Bgra,
				PixelType.UnsignedByte,
				Data);

			MinFilter = TextureMinFilter.Nearest;
			MagFilter = TextureMagFilter.Nearest;

			//GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			//GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);


			IsLocked = false;
			Data = null;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Returns the lock status of the texture
		/// </summary>
		public bool IsLocked
		{
			get;
			protected set;
		}


		/// <summary>
		/// Lock mode
		/// </summary>
		protected ImageLockMode LockMode;



		/// <summary>
		/// Gets the internal RenderDevice ID of the texture
		/// </summary>
		public int Handle
		{
			get;
			private set;
		}



		/// <summary>
		/// Horizontal wrapping method
		/// </summary>
		public HorizontalWrapFilter HorizontalWrap
		{
			get
			{
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				int value;
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureWrapS, out value);

				return (HorizontalWrapFilter)value;
			}
			set
			{
				GL.BindTexture(TextureTarget.Texture2D, Handle);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)value);
			}
		}


		/// <summary>
		/// Vertical wrapping method
		/// </summary>
		public VerticalWrapFilter VerticalWrap
		{
			get
			{
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				int value;

				//GL.GetInteger(GL.GL_TEXTURE_WRAP_T, out value[0]);
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureWrapT, out value);

				return (VerticalWrapFilter)value;
			}
			set
			{
				GL.BindTexture(TextureTarget.Texture2D, Handle);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)value);
			}
		}



		/// <summary>
		/// Gets / sets a border color.
		/// </summary>
		public Color BorderColor
		{
			get
			{
				GL.PushAttrib(AttribMask.TextureBit);
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				int[] color = new int[4];

				//GL.GetInteger(GL.GL_TEXTURE_BORDER_COLOR, out color[0]);
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureBorderColor, color);

				GL.PopAttrib();

				return Color.FromArgb(color[0], color[1], color[2], color[3]);

			}
			set
			{
				GL.PushAttrib(AttribMask.TextureBit);
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				int[] color = new int[4];
				color[0] = value.A;
				color[1] = value.R;
				color[2] = value.G;
				color[3] = value.B;

				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, color);
				GL.PopAttrib();
			}
		}


		/// <summary>
		/// Gets / sets the	texture minifying function 
		/// </summary>
		public TextureMinFilter MinFilter
		{
			get
			{
				//GL.PushAttrib(AttribMask.TextureBit);
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				int value;
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureMinFilter, out value);

				//GL.PopAttrib();

				return (TextureMinFilter)value;
			}
			set
			{
				//GL.PushAttrib(AttribMask.TextureBit);
				GL.BindTexture(TextureTarget.Texture2D, Handle);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)value);
				//GL.PopAttrib();
			}
		}


		/// <summary>
		/// Gets / sets the	texture	magnification function 
		/// </summary>
		public TextureMagFilter MagFilter
		{
			get
			{
				//GL.PushAttrib(AttribMask.TextureBit);
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				int value;
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureMagFilter, out value);
				//GL.PopAttrib();

				return (TextureMagFilter)value;
			}
			set
			{
				//GL.PushAttrib(AttribMask.TextureBit);
				GL.BindTexture(TextureTarget.Texture2D, Handle);

				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)value);

				//GL.PopAttrib();
			}
		}

		/// <summary>
		/// Gets / sets the size of the texture
		/// </summary>
		[Description("Texture size")]
		[Category("Dimension")]
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets a rectangle that covers the texture
		/// </summary>
		[Browsable(false)]
		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle(Point.Empty, Size);
			}
		}



		/// <summary>
		/// Bitmap of the texture
		/// </summary>
		public byte[] Data
		{
			get;
			set;
		}


		#endregion


		#region Dispose

		/// <summary>
		/// Implement IDisposable.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose(bool disposing) executes in two distinct scenarios.
		/// If disposing equals true, the method has been called directly
		/// or indirectly by a user's code. Managed and unmanaged resources
		/// can be disposed.
		/// If disposing equals false, the method has been called by the
		/// runtime from inside the finalizer and you should not reference
		/// other objects. Only unmanaged resources can be disposed.
		/// </summary>
		/// <param name="disposing"></param>
		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.
				}

				// Call the appropriate methods to clean up
				// unmanaged resources here.
				// If disposing is false,
				// only the following code is executed.
				GL.DeleteTexture(Handle);

				// Note disposing has been done.
				disposed = true;

			}
		}


		private bool disposed = false;

		#endregion


	}


	/// <summary>
	/// Sets the wrap parameter for texture coordinate s
	/// to either CLAMP or REPEAT
	/// </summary>
	public enum HorizontalWrapFilter
	{
		/// <summary>
		/// Causes s coordinates to be clamped	to the range [0,1] and is useful
		/// for preventing wrapping artifacts	when mapping a single image onto	an object.
		/// </summary>
		Clamp = 0,
		/// <summary>
		/// Causes the integer part of the s coordinate	to be ignored; the GL uses only 
		/// the fractional part, thereby creating a repeating pattern. Border texture elements are 
		/// accessed only if wrapping is set to GL_CLAMP.
		/// </summary>
		Repeat = 1
	}


	/// <summary>
	/// Sets the wrap parameter for texture coordinate t
	/// to either CLAMP or REPEAT
	/// </summary>
	public enum VerticalWrapFilter
	{
		/// <summary>
		/// Causes s coordinates to be clamped	to the range [0,1] and is useful
		/// for preventing wrapping artifacts	when mapping a single image onto	an object.
		/// </summary>
		Clamp = 0,
		/// <summary>
		/// Causes the integer part of the s coordinate	to be ignored; the GL uses only 
		/// the fractional part, thereby creating a repeating pattern. Border texture elements are 
		/// accessed only if wrapping is set to GL_CLAMP.
		/// </summary>
		Repeat = 1
	}



	/// <summary>
	/// Specifies flags that are passed to the flags parameter of the Overload:System.Drawing.Bitmap.LockBits
	///method. The Overload:System.Drawing.Bitmap.LockBits method locks a portion
	///of an image so that you can read or write the pixel data.
	/// </summary>
	public enum ImageLockMode
	{

		/// <summary>
		/// Specifies that a portion of the image is locked for reading.
		/// </summary>
		ReadOnly = 1,

	
		
		/// <summary>
		/// Specifies that a portion of the image is locked for writing.
		/// </summary>
		WriteOnly = 2,

		/// <summary>
		/// Specifies that a portion of the image is locked for reading or writing.
		/// </summary>
		ReadWrite = 3,
	}


}
