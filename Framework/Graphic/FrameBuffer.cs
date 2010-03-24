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
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ArcEngine.Graphic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace ArcEngine.Graphic
{
	/// <summary>
	/// Redirect the rendering output to a frame buffer
	/// </summary>
	/// http://www.gamedev.net/reference/articles/article2331.asp
	/// http://troylawlor.com/tutorial-fbo.html
	/// http://www.gamedev.net/community/forums/topic.asp?topic_id=364174
	/// 
	/// http://www.seas.upenn.edu/~cis665/fbo.htm#error2
	/// 
	/// 
	/// http://www.opengl.org/registry/specs/ARB/wgl_render_texture.txt
	/// http://oss.sgi.com/projects/ogl-sample/registry/EXT/framebuffer_object.txt
	/// 
	/// 
	/// http://www.coder-studio.com/index.php?page=tutoriaux_aff&code=c_13
	/// 
	/// 
	/// http://ogltotd.blogspot.com/2006/12/render-to-texture.html
	/// http://www.opentk.com/doc/graphics/frame-buffer-objects
	/// 
	/// http://www.songho.ca/opengl/gl_fbo.html
	/// http://www.songho.ca/opengl/gl_pbo.html
	public class FrameBuffer : IDisposable
	{



		/// <summary>
		/// Create the RenderBuffer
		/// </summary>
		/// <param name="size">Desired size</param>
		public FrameBuffer(Size size)
		{
			Size = size;

			// Create Color Tex
			ColorTexture = new Texture(size);


			// Create Depth Tex
			DepthTexture = new Texture(size);
			GL.TexImage2D(TextureTarget.Texture2D, 0, (PixelInternalFormat)All.DepthComponent32, size.Width, size.Height, 0, PixelFormat.DepthComponent, PixelType.UnsignedInt, IntPtr.Zero);

			// Create Stencil Tex
		//	StencilTexture = new Texture(size);
		//	GL.TexImage2D(TextureTarget.Texture2D, 0, (PixelInternalFormat)All.StencilIndex, size.Width, size.Height, 0, PixelFormat.StencilIndex, PixelType.UnsignedByte, IntPtr.Zero);


			

			// Create a FBO and attach the textures
			GL.GenFramebuffers(1, out Handle);
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
			GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, ColorTexture.Handle, 0);
			GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, DepthTexture.Handle, 0);
		//	GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.StencilAttachment, TextureTarget.Texture2D, StencilTexture.Handle, 0);

			End();
		}


		
		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			if (ColorTexture != null)
				ColorTexture.Dispose();

			if (DepthTexture != null)
				DepthTexture.Dispose();

			//if (FBOHandle != 0)
			//    GL.Ext.DeleteFramebuffers(1, ref FBOHandle);
		}


		/// <summary>
		/// Make the Render buffer current 
		/// </summary>
		public void Bind()
		{
			GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, Handle);

			GL.PushAttrib(AttribMask.ViewportBit);
			GL.Viewport(0, 0, Size.Width, Size.Height);

			GL.MatrixMode(MatrixMode.Projection);
			GL.PushMatrix();
			GL.LoadIdentity();
			GL.Ortho(0, Size.Width, 0, Size.Height, -1, 1);
		}



		/// <summary>
		/// 
		/// </summary>
		public void End()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.PopMatrix();

			GL.PopAttrib();
			GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
		}



		#region Properties


		/// <summary>
		/// FBO handle
		/// </summary>
		int Handle;


		/// <summary>
		/// State of the frame buffer
		/// </summary>
		public FramebufferErrorCode Status
		{
			get
			{

				switch (GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt))
				{
					case FramebufferErrorCode.FramebufferCompleteExt:
					{
						Trace.WriteLine("FBO: The framebuffer is complete and valid for rendering.");
						break;
					}
					case FramebufferErrorCode.FramebufferIncompleteAttachmentExt:
					{
						Trace.WriteLine("FBO: One or more attachment points are not framebuffer attachment complete. This could mean there’s no texture attached or the format isn’t renderable. For color textures this means the base format must be RGB or RGBA and for depth textures it must be a DEPTH_COMPONENT format. Other causes of this error are that the width or height is zero or the z-offset is out of range in case of render to volume.");
						break;
					}
					case FramebufferErrorCode.FramebufferIncompleteMissingAttachmentExt:
					{
						Trace.WriteLine("FBO: There are no attachments.");
						break;
					}
					case FramebufferErrorCode.FramebufferIncompleteDimensionsExt:
					{
						Trace.WriteLine("FBO: Attachments are of different size. All attachments must have the same width and height.");
						break;
					}
					case FramebufferErrorCode.FramebufferIncompleteFormatsExt:
					{
						Trace.WriteLine("FBO: The color attachments have different format. All color attachments must have the same format.");
						break;
					}
					case FramebufferErrorCode.FramebufferIncompleteDrawBufferExt:
					{
						Trace.WriteLine("FBO: An attachment point referenced by GL.DrawBuffers() doesn’t have an attachment.");
						break;
					}
					case FramebufferErrorCode.FramebufferIncompleteReadBufferExt:
					{
						Trace.WriteLine("FBO: The attachment point referenced by GL.ReadBuffers() doesn’t have an attachment.");
						break;
					}
					case FramebufferErrorCode.FramebufferUnsupportedExt:
					{
						Trace.WriteLine("FBO: This particular FBO configuration is not supported by the implementation.");
						break;
					}
					default:
					{
						Trace.WriteLine("FBO: Status unknown. (yes, this is really bad.)");
						break;
					}
				}

				return GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);

			}
		}


		/// <summary>
		/// Size of the FrameBuffer
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Color texture
		/// </summary>
		public Texture ColorTexture
		{
			get;
			private set;
		}


		/// <summary>
		/// Depth texture
		/// </summary>
		public Texture DepthTexture
		{
			get;
			private set;
		}

/*
		/// <summary>
		/// Stencil texture
		/// </summary>
		public Texture StencilTexture
		{
			get;
			private set;
		}
*/

		/// <summary>
		/// Maximum allowed size
		/// </summary>
		static public Size MaxSize
		{
			get
			{
				int width;
				int height;

				GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferWidthExt, out width);
				GL.GetRenderbufferParameter(RenderbufferTarget.Renderbuffer, RenderbufferParameterName.RenderbufferHeight, out height);

				return new Size(width, height);
			}
		}


		#endregion

	}
}
