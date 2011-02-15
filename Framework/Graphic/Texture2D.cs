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

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ArcEngine.Storage;
using TK = OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// 2D Texture
	/// </summary>
	public class Texture2D : Texture
	{

		#region ctor / dtor

		/// <summary>
		/// Constructor
		/// </summary>
		public Texture2D()
		{
			// Create handle
			Handle = TK.GL.GenTexture();

			// Target type
			Target = TextureTarget.Texture2D;

			// Bind the texture
			Display.Texture = this;

            PixelInternalFormat = TK.PixelInternalFormat.Rgba8;
			PixelFormat = PixelFormat.Bgra;

			MagFilter = DefaultMagFilter;
			MinFilter = DefaultMinFilter;
			BorderColor = DefaultBorderColor;
			HorizontalWrap = DefaultHorizontalWrapFilter;
			VerticalWrap = DefaultVerticalWrapFilter;
			
		}


		/// <summary>
		/// Creates an empty texture
		/// </summary>
		/// <param name="size">Size of the texture to create</param>
		public Texture2D(Size size) : this()
		{
			SetSize(Target, size);
		}


		/// <summary>
		/// Loads an image from the disk
		/// </summary>
		/// <param name="filename">Image's name</param>
		public Texture2D(string filename) : this()
		{
			base.LoadImage(Target, filename);
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

			base.FromStream(Target, stream);

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
			SetSize(Target, size);
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
		/// Implement IDisposable.
		/// </summary>
		public override void Dispose()
		{
            TK.GL.DeleteTexture(Handle);
			Handle = -1;

			IsDisposed = true;
			InUse--;
		}

		#endregion


		#region Statics

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


		#region Shared textures


		/// <summary>
		/// Creates a shared texture
		/// </summary>
		/// <param name="name">Name of the texture</param>
		/// <returns>Texture handle</returns>
		public static Texture2D CreateShared(string name)
		{
			if (SharedTextures == null)
				SharedTextures = new Dictionary<string, Texture2D>();


			// Texture already exist, so return it
			if (SharedTextures.ContainsKey(name))
				return SharedTextures[name];

			// Else create the texture
			SharedTextures[name] = new Texture2D();
			return SharedTextures[name];
		}



		/// <summary>
		/// Deletes a shared texture
		/// </summary>
		/// <param name="name">Name of the texture</param>
		public static void DeleteShared(string name)
		{
			SharedTextures[name] = null;
		}


		/// <summary>
		/// Removes all shared textures
		/// </summary>
		public static void DeleteShared()
		{
			SharedTextures.Clear();
		}



		/// <summary>
		/// Shared textures
		/// </summary>
		static Dictionary<string, Texture2D> SharedTextures = new Dictionary<string, Texture2D>();


		#endregion


		#region Loading and saving

		/// <summary>
		/// Load an image from bank and convert it to a texture
		/// </summary>
		/// <param name="filename">File name to load</param>
		/// <returns>True if success or false if something went wrong</returns>
		public bool LoadImage(string filename)
		{
			return base.LoadImage(Target, filename);
		}


		/// <summary>
		/// Load a texture from a stream (ie resource files)
		/// </summary>
		/// <param name="stream">Stream handle</param>
		/// <returns></returns>
		public bool FromStream(Stream stream)
		{
			return base.FromStream(Target, stream);
		}


		/// <summary>
		/// Loads a texture from a Bitmap
		/// </summary>
		/// <param name="bitmap">Bitmap handle</param>
		/// <returns></returns>
		public bool FromBitmap(Bitmap bitmap)
		{
			return base.FromBitmap(Target, bitmap);
		}

		/// <summary>
		/// Loads a Png picture from a byte[]
		/// </summary>
		/// <param name="data">Binary of a png file</param>
		/// <returns>True if successful, or false</returns>
		public bool LoadImage(byte[] data)
		{
			return base.LoadImage(Target, data);
		}


		/// <summary>
		/// Save the texture to the disk as a PNG file
		/// </summary>
		/// <param name="name">Name of the texture on the disk</param>
		/// <returns>True if successful or false if an error occured</returns>
		public bool SaveToDisk(string name)
		{
			return base.SaveToDisk(Target, name);
		}



		/// <summary>
		/// Save the texture as a PNG image in the bank
		/// </summary>
		/// <param name="storage">Bank's name</param>
		/// <param name="assetname">Asset name in the bank</param>
		/// <returns>True on success</returns>
		public bool SaveToBank(StorageBase storage, string assetname)
		{
			return base.SaveToStorage(TextureTarget.Texture2D, storage, assetname);
		}


		/// <summary>
		/// Blits a Bitmap on the texture
		/// </summary>
		/// <param name="bitmap">Bitmap handle</param>
		/// <param name="location">Location on the texture</param>
		public void SetData(Bitmap bitmap, Point location)
		{
			base.SetData(Target, bitmap, location);
		}

		#endregion


		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		public bool Lock(ImageLockMode mode)
		{
			return Lock(TextureTarget.Texture2D, mode);
		}


		/// <summary>
		/// 
		/// </summary>
		public void Unlock()
		{
			Unlock(TextureTarget.Texture2D);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Color[,] GetColors()
		{
			return DataToColor(TextureTarget.Texture2D);
		}

		/// <summary>
		/// Sets the size of the texture
		/// </summary>
		/// <param name="size">Desired size</param>
		public void SetSize(Size size)
		{
			base.SetSize(Target, size);
		}




		#region Properties

		/// <summary>
		/// Gets a rectangle that covers the texture
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(Point.Empty, Size);
			}
		}


		/// <summary>
		/// Horizontal wrapping method
		/// </summary>
		public TextureWrapFilter HorizontalWrap
		{
			get
			{
				Display.Texture = this;

				int value;
				TK.GL.GetTexParameter((TK.TextureTarget) Target, TK.GetTextureParameter.TextureWrapS, out value);

				return (TextureWrapFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter((TK.TextureTarget) Target, TK.TextureParameterName.TextureWrapS, (int) value);
			}
		}


		/// <summary>
		/// Vertical wrapping method
		/// </summary>
		public TextureWrapFilter VerticalWrap
		{
			get
			{
				Display.Texture = this;

				int value;
				TK.GL.GetTexParameter((TK.TextureTarget) Target, TK.GetTextureParameter.TextureWrapT, out value);

				return (TextureWrapFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter((TK.TextureTarget) Target, TK.TextureParameterName.TextureWrapT, (int) value);
			}
		}



		#endregion
	}

}
