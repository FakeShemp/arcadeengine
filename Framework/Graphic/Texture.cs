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
	/// Base class for texture
	/// </summary>
	public abstract class Texture : IDisposable
	{

		/// <summary>
		/// 
		/// </summary>
		public abstract void Dispose();


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
		static public HorizontalWrapFilter DefaultHorizontalWrapFilter = HorizontalWrapFilter.Clamp;


		/// <summary>
		/// Default vertical wrap filter
		/// </summary>
		static public VerticalWrapFilter DefaultVerticalWrapFilter = VerticalWrapFilter.Clamp;


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
		/// Horizontal wrapping method
		/// </summary>
		public HorizontalWrapFilter HorizontalWrap
		{
			get
			{
				Display.Texture = this;

				int value;
				TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, TK.GetTextureParameter.TextureWrapS, out value);

				return (HorizontalWrapFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureWrapS, (int) value);
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

				return (VerticalWrapFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureWrapT, (int) value);
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

				return (TextureMinFilter) value;
			}
			set
			{
				Display.Texture = this;

				TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureMinFilter, (int) value);
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

				return (TextureMagFilter) value;
			}
			set
			{
				Display.Texture = this;
				TK.GL.TexParameter(TK.TextureTarget.Texture2D, TK.TextureParameterName.TextureMagFilter, (int) value);
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
				TK.GL.GetTexParameter(TK.TextureTarget.Texture2D, (TK.GetTextureParameter) TK.ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, out value);

				return value;
			}
			set
			{
				if (!Display.Capabilities.HasAnisotropicFiltering)
					return;

				Display.Texture = this;

				TK.GL.TexParameter(TK.TextureTarget.Texture2D, (TK.TextureParameterName) TK.ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, value);
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
