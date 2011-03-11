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
using ArcEngine.Storage;
using System.Collections.Generic;

namespace ArcEngine.Graphic
{

	/// <summary>
	/// Base class for texture
	/// </summary>
	public abstract class Texture : IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		public Texture()
		{
			Handle = -1;
			if (InUse == null)
				InUse = new List<Texture>();
			InUse.Add(this);
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~Texture()
		{
			if (!IsDisposed)
				throw new Exception(this + " not disposed, Call Dispose() !!");
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		public abstract void Dispose();


		/// <summary>
		/// Generate mipmap
		/// </summary>
		public void GenerateMipmap()
		{
			Display.Texture = this;
			TK.GL.GenerateMipmap((TK.GenerateMipmapTarget)Target);
		}



		#region Image IO

		/// <summary>
		/// Load an image from bank and convert it to a texture
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="filename">File name to load</param>
		/// <returns>True if success or false if something went wrong</returns>
		protected bool LoadImage(TextureTarget target, string filename)
		{
			using (Stream stream = ResourceManager.Load(filename))
				return FromStream(target, stream);
		}


		/// <summary>
		/// Load a texture from a stream (ie resource files)
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="stream">Stream handle</param>
		/// <returns>True on success</returns>
		protected bool FromStream(TextureTarget target, Stream stream)
		{
			if (stream == null)
				return false;

			Bitmap bm = new Bitmap(stream);
			bool ret = FromBitmap(target, bm);

			if (bm != null)
				bm.Dispose();

			return ret;
		}


		/// <summary>
		/// Loads a texture from a Bitmap
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="bitmap">Bitmap handle</param>
		/// <returns>True on success</returns>
		protected bool FromBitmap(TextureTarget target, Bitmap bitmap)
		{
			if (bitmap == null)
				return false;

			Display.Texture = this;

			SetSize(target, bitmap.Size);

			SetData(target, bitmap, Point.Empty);

			return true;
		}


		/// <summary>
		/// Loads a Png picture from a byte[]
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="data">Binary of a png file</param>
		/// <returns>True if successful, or false</returns>
		protected bool LoadImage(TextureTarget target, byte[] data)
		{
			if (data == null)
				return false;

			MemoryStream stream = new MemoryStream(data);
			bool ret = FromStream(target, stream);
			stream.Dispose();

			return ret;
		}


		/// <summary>
		/// Save the texture to the disk as a PNG file
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="name">Name of the texture on the disk</param>
		/// <returns>True if successful or false if an error occured</returns>
		protected bool SaveToDisk(TextureTarget target, string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;

			Bitmap bm = ToBitmap(target, new Rectangle(Point.Empty, Size));
			if (bm == null)
				return false;

			bm.Save(name, Imaging.ImageFormat.Png);
			bm.Dispose();

			return true;
		}


		/// <summary>
		/// Convert the texture to a Bitmap
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="rectangle">Rectangle bounds</param>
		/// <returns>Bitmap handle or null</returns>
		protected Bitmap ToBitmap(TextureTarget target, Rectangle rectangle)
		{
			if (!Lock(target, ImageLockMode.ReadOnly, rectangle))
				return null;

			Bitmap bm = new Bitmap(rectangle.Width, rectangle.Height);

			System.Drawing.Imaging.BitmapData bmd = bm.LockBits(rectangle,
				System.Drawing.Imaging.ImageLockMode.WriteOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			System.Runtime.InteropServices.Marshal.Copy(Data, 0, bmd.Scan0, Data.Length);
			bm.UnlockBits(bmd);

			Unlock(target);

			return bm;
		}


		/// <summary>
		/// Save the texture as a PNG image in the bank
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="storage">Storage handle</param>
		/// <param name="assetname">Asset name in the bank</param>
		/// <returns>True on success</returns>
		protected bool SaveToStorage(TextureTarget target, StorageBase storage, string assetname)
		{
			if (storage == null || string.IsNullOrEmpty(assetname))
				return false;


			// Create tmp bitmap
			using (Bitmap bm = new Bitmap(Size.Width, Size.Height))
			{

				// Lock texture
				if (!Lock(target, ImageLockMode.ReadOnly))
					return false;

				// Copy texture to the bitmap
				System.Drawing.Imaging.BitmapData bmd = bm.LockBits(new Rectangle(Point.Empty, Size),
					System.Drawing.Imaging.ImageLockMode.WriteOnly,
					System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				System.Runtime.InteropServices.Marshal.Copy(Data, 0, bmd.Scan0, Data.Length);
				bm.UnlockBits(bmd);

				// Unlock texture
				Unlock(target);


				// Save bitmap to a stream
				//using (Stream stream = new MemoryStream())
				using (Stream stream = storage.OpenFile(assetname, FileAccess.Write))
				{
					// Save bitmap to the stream
					bm.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

					// Rewind stream
					//stream.Seek(0, SeekOrigin.Begin);

					// Save to bank
					//ResourceManager.SaveAsset(bankname, assetname, stream);
				}
			}

			return true;
		}


		#endregion


		/// <summary>
		/// Sets the size of the texture
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="size">Desired size</param>
		protected void SetSize(TextureTarget target, Size size)
		{
			Size = size;

		//	Trace.WriteDebugLine("[Texture2D] : Resize() {0}", this);

			Lock(target, ImageLockMode.WriteOnly, new Rectangle(Point.Empty, size));
			Data = null;
			Unlock(target);
		}


		#region Blitting

		/// <summary>
		/// Blits a Bitmap on the texture
		/// </summary>
		/// <param name="target">Reference face</param>
		/// <param name="bitmap">Bitmap handle</param>
		/// <param name="location">Location on the texture</param>
		protected void SetData(TextureTarget target, Bitmap bitmap, Point location)
		{
			if (bitmap == null)
			{
				Trace.WriteDebugLine("[Texture2D] : SetData() failed. bitmap == null - {0}", this);

				return;
			}

			Imaging.BitmapData bmdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				 Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb);


			byte[] data = new byte[bitmap.Size.Width * bitmap.Size.Height * 4];
			Marshal.Copy(bmdata.Scan0, data, 0, bitmap.Size.Width * bitmap.Size.Height * 4);
			bitmap.UnlockBits(bmdata);

			Display.GetLastError("before bind texture");
			Display.Texture = this;
			Display.GetLastError("after bind texture");

			Display.GetLastError("before TexSubImage2D");
			TK.GL.TexSubImage2D<byte>((TK.TextureTarget) target, 0,
				location.X, location.Y,
				bitmap.Width, bitmap.Height,
				(TK.PixelFormat) PixelFormat, TK.PixelType.UnsignedByte,
				data);
			Display.GetLastError("after TexSubImage2D");
		}


		#endregion



		#region Locking / unlocking

				/// <summary>
		/// Locks the bitmap texture to system memory
		/// </summary>
		/// <param name="target">Reference mode</param>
		/// <param name="mode">Access mode</param>
		/// <returns>True if locked, or false if an error occured</returns>
		protected bool Lock(TextureTarget target, ImageLockMode mode)
		{
			return Lock(target, mode, new Rectangle(Point.Empty, Size));
		}


		/// <summary>
		/// Locks the bitmap texture to system memory
		/// </summary>
		/// <param name="target">Reference mode</param>
		/// <param name="mode">Access mode</param>
		/// <param name="rectangle">Zone to lock</param>
		/// <returns>True if locked, or false if an error occured</returns>
		protected bool Lock(TextureTarget target, ImageLockMode mode, Rectangle rectangle)
		{
			// No texture bounds
			if (Handle == -1  || IsLocked)
			{
				Trace.WriteDebugLine("[Texture] : Lock() failed. Lockmode = {0} - {1}", mode.ToString(), this);
				return false;
			}

		//	Trace.WriteDebugLine("[Texture] : Lock() Lockmode = {0} - {1}", mode.ToString(), this);

	
			Data = new byte[rectangle.Width * rectangle.Height * 4];

			LockMode = mode;
			IsLocked = true;

			if (mode == ImageLockMode.WriteOnly)
				return true;


			Display.Texture = this;

			// Get the whole texture
			if (rectangle == new Rectangle(Point.Empty, Size))
			{

				TK.GL.GetTexImage<byte>((TK.TextureTarget)target, 0, (TK.PixelFormat)PixelFormat, TK.PixelType.UnsignedByte, Data);
			}
			else
			{
				//TODO: Use Pixel Buffer Object instead
				TK.GL.ReadPixels(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, (TK.PixelFormat)PixelFormat, TK.PixelType.UnsignedByte, Data);
			}

			LockBound = rectangle;

			return true;
		}


		/// <summary>
		/// Unlocks the texture's bitmap from system memory.
		/// </summary>
		/// <param name="target">Reference face</param>
		protected void Unlock(TextureTarget target)
		{
			if (!IsLocked || LockMode == ImageLockMode.ReadOnly)
			{
				IsLocked = false;
				Trace.WriteDebugLine("[Texture2D] : Lock() failed. Already locked - {0}", this);
				
				return;
			}

			Display.Texture = this;

			// The below is almost OK. The problem is the GL_RGBA. On certain platforms, the GPU prefers that red and blue be swapped (GL_BGRA).
			// If you supply GL_RGBA, then the driver will do the swapping for you which is slow.
			TK.GL.TexImage2D((TK.TextureTarget) target, 0, PixelInternalFormat,
				Size.Width, Size.Height,
				0,
				(TK.PixelFormat) PixelFormat,
				TK.PixelType.UnsignedByte,
				Data);

			//TK.GL.TexSubImage2D<byte>((TK.TextureTarget) target, 0,
			//    LockBound.Left, LockBound.Top,
			//    LockBound.Width, LockBound.Height,
			//    (TK.PixelFormat) PixelFormat, TK.PixelType.UnsignedByte, Data); 


			IsLocked = false;
			Data = null;
			LockBound = Rectangle.Empty;

		//	Trace.WriteDebugLine("[Texture2D] : Unlock() {0}", this);

		}


		/// <summary>
		/// Returns a read only array of Color of the texture
		/// </summary>
		/// <param name="target">Reference mode</param>
		/// <param name="rectangle">Zone to collect</param>
		/// <returns>A bi dimensional array of RGBA colors</returns>
		protected Color[,] DataToColor(TextureTarget target, Rectangle rectangle)
		{
			Lock(target, ImageLockMode.ReadOnly, rectangle);

			Color[,] colors = new Color[rectangle.Width, rectangle.Height];

			for (int y = rectangle.Top ; y < rectangle.Height ; y++)
			{
				for (int x = rectangle.Left ; x < rectangle.Width ; x++)
				{
					int offset = y * rectangle.Width *4 + x * 4;
					colors[x, y] = Color.FromArgb(
						Data[offset + 3],
						Data[offset + 2],
						Data[offset + 1],
						Data[offset + 0]);
				}
			}
			Unlock(target);

			return colors;
		}

		#endregion



		#region Statics


		/// <summary>
		/// Default magnify filter
		/// </summary>
		static public TextureMagFilter DefaultMagFilter = TextureMagFilter.Nearest;


		/// <summary>
		/// Default minify filter
		/// </summary>
		static public TextureMinFilter DefaultMinFilter = TextureMinFilter.Nearest;


		/// <summary>
		/// Default border color
		/// </summary>
		static public Color DefaultBorderColor = Color.Black;


		/// <summary>
		/// Default horizontal wrap filter
		/// </summary>
		static public TextureWrapFilter DefaultHorizontalWrapFilter = TextureWrapFilter.Clamp;


		/// <summary>
		/// Default vertical wrap filter
		/// </summary>
		static public TextureWrapFilter DefaultVerticalWrapFilter = TextureWrapFilter.Clamp;


		/// <summary>
		/// GL_ARB_texture_compression extension supported
		/// </summary>
		static internal bool HasTextureCompression
		{
			get
			{
				return Display.Capabilities.Extensions.Contains("GL_ARB_texture_compression");
			}
		}


		/// <summary>
		/// Number of textre in use
		/// </summary>
		public static List<Texture> InUse
		{
			get;
			protected set;
		}

		#endregion



		#region Properties


		/// <summary>
		/// Does resource is disposed
		/// </summary>
		public bool IsDisposed
		{
			get;
			protected set;
		}


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
		/// Lock rectangle
		/// </summary>
		Rectangle LockBound;


		/// <summary>
		/// Gets the internal RenderDevice ID of the texture
		/// </summary>
		public int Handle
		{
			get;
			protected set;
		}


		/// <summary>
		/// Specifies the number of color components in the texture.
		/// </summary>
		protected TK.PixelInternalFormat PixelInternalFormat;


		/// <summary>
		/// Specifies the format of the pixel data.
		/// </summary>
		public PixelFormat PixelFormat
		{
			get;
			protected set;
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
				TK.GL.GetTexParameter((TK.TextureTarget) Target, TK.GetTextureParameter.TextureBorderColor, color);

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

				TK.GL.TexParameter((TK.TextureTarget) Target, TK.TextureParameterName.TextureBorderColor, color);
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
				TK.GL.GetTexParameter((TK.TextureTarget) Target, TK.GetTextureParameter.TextureMinFilter, out value);

				return (TextureMinFilter) value;
			}
			set
			{
				Display.Texture = this;

				TK.GL.TexParameter((TK.TextureTarget) Target, TK.TextureParameterName.TextureMinFilter, (int) value);
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
				TK.GL.GetTexParameter((TK.TextureTarget) Target, TK.GetTextureParameter.TextureMagFilter, out value);

				return (TextureMagFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter((TK.TextureTarget) Target, TK.TextureParameterName.TextureMagFilter, (int) value);
			}
		}


		/// <summary>
		/// Gets / sets the	texture minifying function 
		/// </summary>
		public float AnisotropicFilter
		{
			get
			{
				if (!Display.Capabilities.HasAnisotropicFiltering)
					return 0.0f;

				Display.Texture = this;

				float value;
				TK.GL.GetTexParameter((TK.TextureTarget) Target, (TK.GetTextureParameter) TK.ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, out value);

				return value;
			}
			set
			{
				if (!Display.Capabilities.HasAnisotropicFiltering)
					return;

				Display.Texture = this;

				TK.GL.TexParameter((TK.TextureTarget) Target, (TK.TextureParameterName) TK.ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, value);
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
			protected set;
		}



		/// <summary>
		/// Bitmap of the texture
		/// </summary>
		public byte[] Data
		{
			get;
			set;
		}


		/// <summary>
		/// Texture target
		/// </summary>
		public TextureTarget Target
		{
			get;
			protected set;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0} (id {1}) ({2}x{3})", Target, Handle, Size.Width, Size.Height);
		}

		
		#endregion
	}



	/// <summary>
	/// Target texture
	/// </summary>
	public enum TextureTarget
	{
		/// <summary>
		/// One dimensional texture
		/// </summary>
		Texture1D = TK.TextureTarget.Texture1D,

	
		/// <summary>
		/// Two dimensional texture
		/// </summary>
		Texture2D = TK.TextureTarget.Texture2D,


		/// <summary>
		/// Three dimensional texture
		/// </summary>
		Texture3D = TK.TextureTarget.Texture3D,


		/// <summary>
		/// Cube map texture
		/// </summary>
		CubeMap = TK.TextureTarget.TextureCubeMap,


		/// <summary>
		/// Rectangle texture
		/// </summary>
		TextureRectangle = TK.TextureTarget.TextureRectangle,

/*
		/// <summary>
		/// 
		/// </summary>
		NegativeX = TK.TextureTarget.TextureCubeMapNegativeX,

		/// <summary>
		/// 
		/// </summary>
		NegativeY = TK.TextureTarget.TextureCubeMapNegativeY,

		/// <summary>
		/// 
		/// </summary>
		NegativeZ = TK.TextureTarget.TextureCubeMapNegativeZ,

		/// <summary>
		/// 
		/// </summary>
		PositiveX = TK.TextureTarget.TextureCubeMapPositiveX,

		/// <summary>
		/// 
		/// </summary>
		PositiveY = TK.TextureTarget.TextureCubeMapPositiveY,

		/// <summary>
		/// 
		/// </summary>
		PositiveZ = TK.TextureTarget.TextureCubeMapPositiveZ,
*/	
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
	/// Sets the wrap parameter for texture coordinate
	/// to either CLAMP or REPEAT
	/// </summary>
	public enum TextureWrapFilter
	{
		/// <summary>
		/// Causes coordinates to be clamped to the range [0,1] and is useful
		/// for preventing wrapping artifacts when mapping a single image onto an object.
		/// </summary>
		Clamp = TK.TextureWrapMode.Clamp,

		/// <summary>
		/// Causes the integer part of the coordinate to be ignored; the GL uses only 
		/// the fractional part, thereby creating a repeating pattern. Border texture elements are 
		/// accessed only if wrapping is set to GL_CLAMP.
		/// </summary>
		Repeat = TK.TextureWrapMode.Repeat,


		/// <summary>
		/// 
		/// </summary>
		ClampToEdge = TK.TextureWrapMode.ClampToEdge,


		/// <summary>
		/// 
		/// </summary>
		ClampToBorder = TK.TextureWrapMode.ClampToBorder,


		/// <summary>
		/// 
		/// </summary>
		MirrorRepeat = TK.TextureWrapMode.MirroredRepeat,
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
	/// Pixel format
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


	/// <summary>
	/// 
	/// </summary>
	public enum TextureEnvMode
	{
		/// <summary>
		/// 
		/// </summary>
		Add = TK.TextureEnvMode.Add,

		/// <summary>
		/// 
		/// </summary>
		Blend = TK.TextureEnvMode.Blend,

		/// <summary>
		/// 
		/// </summary>
		Replace = TK.TextureEnvMode.Replace,

		/// <summary>
		/// 
		/// </summary>
		Modulate = TK.TextureEnvMode.Modulate,

		/// <summary>
		/// 
		/// </summary>
		Decal = TK.TextureEnvMode.Decal,

		/// <summary>
		/// 
		/// </summary>
		Combine = TK.TextureEnvMode.Combine,
	}


	/// <summary>
	/// The texture magnification function is used when the pixel being textured 
	/// maps to an area less than or equal to one texture element. 
	/// </summary>
	public enum TextureMagFilter
	{
		/// <summary>
		/// Returns the value of the texture element that is nearest 
		/// (in Manhattan distance) to the center of the pixel being textured.
		/// </summary>
		Nearest = TK.TextureMagFilter.Nearest,

		/// <summary>
		/// Returns the weighted average of the four texture elements that are closest 
		/// to the center of the pixel being textured. These can include border texture 
		/// elements, depending on the values of GL_TEXTURE_WRAP_S and GL_TEXTURE_WRAP_T, 
		/// and on the exact mapping.
		/// </summary>
		Linear = TK.TextureMagFilter.Linear,
	}


	/// <summary>
	/// The texture minifying function is used whenever the pixel being
	/// textured maps to an area greater than one texture element.
	/// </summary>
	public enum TextureMinFilter
	{

		/// <summary>
		/// Returns the value of the texture element that is nearest (in Manhattan distance) 
		/// to the center of the pixel being textured.
		/// </summary>
		Nearest = TK.TextureMinFilter.Nearest,

		/// <summary>
		/// Returns the weighted average of the four texture elements that are closest to the center of the pixel 
		/// being textured. These can include border texture elements, depending on the values of
		/// GL_TEXTURE_WRAP_S and GL_TEXTURE_WRAP_T, and on the exact mapping.
		/// </summary>
		Linear = TK.TextureMinFilter.Linear,

		/// <summary>
		/// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the
		/// GL_NEAREST criterion (the texture element nearest to the center of the pixel) to produce a texture value.
		/// </summary>
		NearestMipmapNearest = TK.TextureMinFilter.NearestMipmapNearest,

		/// <summary>
		/// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the 
		/// GL_LINEAR criterion (a weighted average of the four texture elements that are closest to the 
		/// center of the pixel) to produce a texture value.
		/// </summary>
		LinearMipmapNearest = TK.TextureMinFilter.LinearMipmapNearest,

		/// <summary>
		/// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses 
		/// the GL_NEAREST criterion (the texture element nearest to the center of the pixel) to produce
		/// a texture value from each mipmap. The final texture value is a weighted average of those two values.
		/// </summary>
		NearestMipmapLinear = TK.TextureMinFilter.NearestMipmapLinear,

		/// <summary>
		/// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses 
		/// the GL_LINEAR criterion (a weighted average of the four texture elements that are closest to 
		/// the center of the pixel) to produce a texture value from each mipmap. 
		/// The final texture value is a weighted average of those two values.
		/// </summary>
		LinearMipmapLinear = TK.TextureMinFilter.LinearMipmapLinear,
	}

}
