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
using Imaging = System.Drawing.Imaging;
using TK = OpenTK.Graphics.OpenGL;

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
	/// Texture definition
	/// </summary>
	public class Texture2D : IDisposable
	{

		#region ctor / dtor

		/// <summary>
		/// Constructor
		/// </summary>
		public Texture2D()
		{
			Handle = TK.GL.GenTexture();
			//ErrorCode code = GL.GetError();
			//if (code != ErrorCode.NoError)
			//    Trace.WriteLine("Failed to create a new texture ({0})", code.ToString());

			MagFilter = Display.TextureParameters.MagFilter;
			MinFilter = Display.TextureParameters.MinFilter;
			BorderColor = Display.TextureParameters.BorderColor;
			HorizontalWrap = Display.TextureParameters.HorizontalWrapFilter;
			VerticalWrap = Display.TextureParameters.VerticalWrapFilter;

            PixelInternalFormat = TK.PixelInternalFormat.Rgba8;
			PixelFormat = PixelFormat.Bgra;
		}

		/// <summary>
		/// Creates an empty texture
		/// </summary>
		/// <param name="size">Size of the texture to create</param>
		public Texture2D(Size size) : this()
		{
			SetSize(size);
		}


		/// <summary>
		/// Loads an image from the disk
		/// </summary>
		/// <param name="filename">Image's name</param>
		public Texture2D(string filename) : this()
		{
			LoadImage(filename);
		}


		/// <summary>
		/// Creates a texture from a Stream
		/// </summary>
		/// <param name="stream">Stream handle</param>
		/// <remarks>The Stream is closed automatically</remarks>
		public Texture2D(Stream stream) : this()
		{
			if (stream == null)
				return;

			LoadImage(stream);

			stream.Close();
		}


		/// <summary>
		/// Creates a new texture with a speicific size and pixel format
		/// </summary>
		/// <param name="size">Desired size</param>
		/// <param name="format">Desired pixel format</param>
		public Texture2D(Size size, PixelFormat format) : this()
		{
			PixelFormat = format;
			SetSize(size);
		}


		/// <summary>
		/// Creates a texture with a specified pixel format
		/// </summary>
		/// <param name="format">Desired pixel format</param>
		public Texture2D(PixelFormat format) : this()
		{
			PixelFormat = format;
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~Texture2D()
		{
		//	throw new Exception("Texture : Handle (id=" + Handle.ToString() + ") != -1, Call Dispose() !!");
		}


		/// <summary>
		/// Implement IDisposable.
		/// </summary>
		public void Dispose()
		{
            TK.GL.DeleteTexture(Handle);
			Handle = -1;

			GC.SuppressFinalize(this);
		}

		#endregion


		/// <summary>
		/// Sets the size of the texture
		/// </summary>
		/// <param name="size">Desired size</param>
		public void SetSize(Size size)
		{
			Size = size;

			LockTextureBits(ImageLockMode.WriteOnly);
			Data = null;
			UnlockTextureBits();
		}


		#region Blitting

		/// <summary>
		/// Blits a Bitmap on the texture
		/// </summary>
		/// <param name="bitmap">Bitmap handle</param>
		/// <param name="location">Location on the texture</param>
        public void SetData(Bitmap bitmap, Point location)
		{
			if (bitmap == null)
				return;


			Imaging.BitmapData bmdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				 Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb);


			byte[] data = new byte[bitmap.Size.Width * bitmap.Size.Height * 4];
			Marshal.Copy(bmdata.Scan0, data, 0, bitmap.Size.Width * bitmap.Size.Height * 4);
			bitmap.UnlockBits(bmdata);

			Display.Texture = this;

            TK.GL.TexSubImage2D<byte>(TK.TextureTarget.Texture2D, 0, 
				location.X, location.Y, 
				bitmap.Width, bitmap.Height,
                TK.PixelFormat.Bgra, TK.PixelType.UnsignedByte,
				data);
		}


		#endregion


		#region Helper

        /// <summary>
        /// Create a 1x1 white texture. Useful for drawing uniform filled element
        /// </summary>
        /// <returns>A 1x1 white texture</returns>
        public static Texture2D CreateWhite1x1()
        {
            Texture2D texture = new Texture2D(new Size(1, 1));

            Bitmap bm = new Bitmap(1, 1);
            bm.SetPixel(0, 0, Color.White);
            texture.SetData(bm, Point.Empty);
            bm.Dispose();


            return texture;
        }


		/// <summary>
		/// Checks if the texture size is valid (power of two)
		/// </summary>
		/// <param name="size">Size to check</param>
		/// <returns>True if the texture is a power of two</returns>
		public static bool CheckTextureSize(Size size)
		{
		//	if (GL.SupportsExtension("ARB_texture_non_power_of_two"))
		//		return true;

			if (size.Width != NextPowerOfTwo(size.Width) || size.Height != NextPowerOfTwo(size.Height))
				return false;

			return true;
		}


		/// <summary>
		/// Gets if the value is a power of two
		/// </summary>
		/// <param name="value">Value to check</param>
		/// <returns>True if power of two</returns>
		protected static bool IsPowerOfTwo(int value)
		{
			return (value & (value - 1)) == 0;
		}



		/// <summary>
		/// Give the next power of two
		/// </summary>
		/// <param name="input">Value</param>
		/// <returns>Newt power of two value</returns>
		protected static int NextPowerOfTwo(int input)
		{
			int value = 1;

			while (value < input) 
				value <<= 1;

			return value;
		}


		/// <summary>
		/// Returns the next Power Of Two size
		/// </summary>
		/// <param name="size">Size</param>
		/// <returns>Next power of two size</returns>
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
			using (Stream stream = ResourceManager.LoadResource(filename))
				return LoadImage(stream);
		}


		/// <summary>
		/// Load a texture from a stream (ie resource files)
		/// </summary>
		/// <param name="stream">Stream</param>
		/// <returns></returns>
		public bool LoadImage(Stream stream)
		{
			if (stream == null)
				return false;

			return LoadImage(new Bitmap(stream));
		}


		/// <summary>
		/// Loads a texture from a Bitmap
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		public bool LoadImage(Bitmap bitmap)
		{
			if (bitmap == null)
				return false;

			Display.Texture = this;

			SetSize(new Size(bitmap.Width, bitmap.Height));

            SetData(bitmap, Point.Empty);

			
			// Update texture matrix
	//		Display.TextureMatrix = Matrix4.Scale(1.0f / Size.Width, 1.0f / Size.Height, 1.0f);


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
		/// <returns>True if locked, or false if an error occured</returns>
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
            TK.GL.GetTexImage<byte>(TK.TextureTarget.Texture2D, 0, TK.PixelFormat.Bgra, TK.PixelType.UnsignedByte, Data);
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
            TK.GL.TexImage2D(TK.TextureTarget.Texture2D, 0, TK.PixelInternalFormat.Rgba8,
				Size.Width, Size.Height,
				0,
				(TK.PixelFormat)PixelFormat,
                TK.PixelType.UnsignedByte,
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
		/// Specifies the number of color components in the texture.
		/// </summary>
        TK.PixelInternalFormat PixelInternalFormat;


		/// <summary>
		/// Specifies the format of the pixel data.
		/// </summary>
		public PixelFormat PixelFormat
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
				Display.Texture = this;

				int value;
                TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, TK.GetTextureParameter.TextureWrapS, out value);

				return (HorizontalWrapFilter)value;
			}
			set
			{
				Display.Texture = this;
                TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureWrapS, (int)value);
			}
		}


		/// <summary>
		/// Vertical wrapping method
		/// </summary>
		public VerticalWrapFilter VerticalWrap
		{
			get
			{
				Display.Texture = this;

				int value;
                TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, TK.GetTextureParameter.TextureWrapT, out value);

				return (VerticalWrapFilter)value;
			}
			set
			{
				Display.Texture = this;
                TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureWrapT, (int)value);
			}
		}



		/// <summary>
		/// Gets / sets a border color.
		/// </summary>
		public Color BorderColor
		{
			get
			{
				Display.Texture = this;

				int[] color = new int[4];
                TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, TK.GetTextureParameter.TextureBorderColor, color);

				return Color.FromArgb(color[0], color[1], color[2], color[3]);

			}
			set
			{
				Display.Texture = this;

				int[] color = new int[4];
				color[0] = value.A;
				color[1] = value.R;
				color[2] = value.G;
				color[3] = value.B;

                TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureBorderColor, color);
			}
		}


		/// <summary>
		/// Gets / sets the	texture minifying function 
		/// </summary>
		public TextureMinFilter MinFilter
		{
			get
			{
				Display.Texture = this;

				int value;
                TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, TK.GetTextureParameter.TextureMinFilter, out value);

				return (TextureMinFilter)value;
			}
			set
			{
				Display.Texture = this;

                TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureMinFilter, (int)value);
			}
		}


		/// <summary>
		/// Gets / sets the	texture	magnification function 
		/// </summary>
		public TextureMagFilter MagFilter
		{
			get
			{
				Display.Texture = this;

				int value;
                TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, TK.GetTextureParameter.TextureMagFilter, out value);

				return (TextureMagFilter)value;
			}
			set
			{
				Display.Texture = this;
                TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureMagFilter, (int)value);
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


		public override string ToString()
		{
			return String.Format("Texture {0}", Handle);
		}

	}



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
	/// Sets the wrap parameter for texture coordinate s
	/// to either CLAMP or REPEAT
	/// </summary>
	public enum HorizontalWrapFilter
	{
		/// <summary>
		/// Causes s coordinates to be clamped	to the range [0,1] and is useful
		/// for preventing wrapping artifacts	when mapping a single image onto	an object.
		/// </summary>
		Clamp = 0x812F,

		/// <summary>
		/// Causes the integer part of the s coordinate	to be ignored; the GL uses only 
		/// the fractional part, thereby creating a repeating pattern. Border texture elements are 
		/// accessed only if wrapping is set to GL_CLAMP.
		/// </summary>
		Repeat = 0x2901
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
		Clamp = 0x812F,


		/// <summary>
		/// Causes the integer part of the s coordinate	to be ignored; the GL uses only 
		/// the fractional part, thereby creating a repeating pattern. Border texture elements are 
		/// accessed only if wrapping is set to GL_CLAMP.
		/// </summary>
		Repeat = 0x2901
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


    /// <summary>
    /// 
    /// </summary>
    public enum PixelFormat
    {
        /// <summary>
        /// 
        /// </summary>
        DepthComponent = TK.PixelFormat.DepthComponent,

        /// <summary>
        /// 
        /// </summary>
        Red = TK.PixelFormat.Red,

        /// <summary>
        /// 
        /// </summary>
        Green = TK.PixelFormat.Green,

        /// <summary>
        /// 
        /// </summary>
        Blue = TK.PixelFormat.Blue,

        /// <summary>
        /// 
        /// </summary>
        Alpha = TK.PixelFormat.Alpha,

        /// <summary>
        /// 
        /// </summary>
        Rgb = TK.PixelFormat.Rgb,

        /// <summary>
        /// 
        /// </summary>
        Rgba = TK.PixelFormat.Rgba,

        /// <summary>
        /// 
        /// </summary>
        Bgr = TK.PixelFormat.Bgr,

        /// <summary>
        /// 
        /// </summary>
        Bgra = TK.PixelFormat.Bgra,

        /// <summary>
        /// 
        /// </summary>
        DepthStencil = TK.PixelFormat.DepthStencil,
    }

}
