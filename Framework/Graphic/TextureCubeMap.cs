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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using ArcEngine.Asset;
using Imaging = System.Drawing.Imaging;
using TK = OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// CubeMap texture
	/// </summary>
	public class TextureCubeMap : Texture
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TextureCubeMap()
		{
			Target = TextureTarget.CubeMap;

			// Create handle
			Handle = TK.GL.GenTexture();

			// Bind the texture
			Display.Texture = this;

			PixelInternalFormat = TK.PixelInternalFormat.Rgba8;
			PixelFormat = PixelFormat.Bgra;

			MagFilter = DefaultMagFilter;
			MinFilter = DefaultMinFilter;
			BorderColor = DefaultBorderColor;
		}


		/// <summary>
		/// Implement IDisposable.
		/// </summary>
		public override void Dispose()
		{
			TK.GL.DeleteTexture(Handle);
			Handle = -1;

			GC.SuppressFinalize(this);
		}



		#region Loading

		/// <summary>
		/// Load an image from bank and convert it to a texture
		/// </summary>
		/// <param name="face">Reference face</param>
		/// <param name="filename">File name to load</param>
		/// <returns>True if success or false if something went wrong</returns>
		public bool LoadImage(TextureCubeFace face, string filename)
		{
			return base.LoadImage((TextureTarget) face, filename);
		}


		/// <summary>
		/// Load a texture from a stream (ie resource files)
		/// </summary>
		/// <param name="face">Reference face</param>
		/// <param name="stream">Stream handle</param>
		/// <returns></returns>
		public bool FromStream(TextureCubeFace face, Stream stream)
		{
			return base.FromStream((TextureTarget) face, stream);
		}


		/// <summary>
		/// Loads a texture from a Bitmap
		/// </summary>
		/// <param name="face">Reference face</param>
		/// <param name="bitmap">Bitmap handle</param>
		/// <returns></returns>
		public bool FromBitmap(TextureCubeFace face, Bitmap bitmap)
		{
			return base.FromBitmap((TextureTarget) face, bitmap);
		}


		/// <summary>
		/// Loads a Png picture from a byte[]
		/// </summary>
		/// <param name="face">Reference face</param>
		/// <param name="data">Binary of a png file</param>
		/// <returns>True if successful, or false</returns>
		public bool LoadImage(TextureCubeFace face, byte[] data)
		{
			return base.LoadImage((TextureTarget) face, data);
		}


		/// <summary>
		/// Blits a Bitmap on the texture
		/// </summary>
		/// <param name="face">Reference face</param>
		/// <param name="bitmap">Bitmap handle</param>
		/// <param name="location">Location on the texture</param>
		public void SetData(TextureCubeFace face, Bitmap bitmap, Point location)
		{
			base.SetData((TextureTarget) face, bitmap, location);
		}


		/// <summary>
		/// Sets the size of the texture
		/// </summary>
		/// <param name="face">Reference face</param>
		/// <param name="size">Desired size</param>
		public void SetSize(TextureCubeFace face, Size size)
		{
			base.SetSize((TextureTarget) face, size);
		}



		#endregion


		#region Properties



		/// <summary>
		/// Sets the wrap parameter for texture coordinate R
		/// </summary>
		public TextureWrapFilter WrapR
		{
			get
			{
				Display.Texture = this;

				int value;
				TK.GL.GetTexParameter((TK.TextureTarget) Target, TK.GetTextureParameter.TextureWrapR, out value);

				return (TextureWrapFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter((TK.TextureTarget) Target, TK.TextureParameterName.TextureWrapR, (int) value);
			}
		}


		/// <summary>
		/// Sets the wrap parameter for texture coordinate S
		/// </summary>
		public TextureWrapFilter WrapS
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
		/// Sets the wrap parameter for texture coordinate T
		/// </summary>
		public TextureWrapFilter WrapT
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

	/// <summary>
	/// 
	/// </summary>
	public enum TextureCubeFace
	{
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
	}
}
