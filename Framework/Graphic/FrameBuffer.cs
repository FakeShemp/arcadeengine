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
using System.Drawing;
using TK = OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Redirect the rendering output to a frame buffer
	/// </summary>
	// http://www.gamedev.net/reference/articles/article2331.asp
	// http://troylawlor.com/tutorial-fbo.html
	// http://www.gamedev.net/community/forums/topic.asp?topic_id=364174
	// 
	// http://www.seas.upenn.edu/~cis665/fbo.htm#error2
	// 
	// 
	// http://www.opengl.org/registry/specs/ARB/wgl_render_texture.txt
	// http://oss.sgi.com/projects/ogl-sample/registry/EXT/framebuffer_object.txt
	// 
	// 
	// http://www.coder-studio.com/index.php?page=tutoriaux_aff&code=c_13
	// 
	// 
	// http://ogltotd.blogspot.com/2006/12/render-to-texture.html
	// http://www.opentk.com/doc/graphics/frame-buffer-objects
	// 
	// http://www.songho.ca/opengl/gl_fbo.html
	// http://www.songho.ca/opengl/gl_pbo.html
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
			ColorTexture = new Texture2D(size);


			// Create Depth Tex
			DepthTexture = new Texture2D(size);
            TK.GL.TexImage2D(TK.TextureTarget.Texture2D, 0, (TK.PixelInternalFormat)TK.All.DepthComponent32, size.Width, size.Height, 0, TK.PixelFormat.DepthComponent, TK.PixelType.UnsignedInt, IntPtr.Zero);

			// Create Stencil Tex
		//	StencilTexture = new Texture(size);
		//	TK.GL.TexImage2D(TextureTarget.Texture2D, 0, (PixelInternalFormat)All.StencilIndex, size.Width, size.Height, 0, PixelFormat.StencilIndex, PixelType.UnsignedByte, IntPtr.Zero);


			

			// Create a FBO and attach the textures
			TK.GL.GenFramebuffers(1, out Handle);
            TK.GL.BindFramebuffer(TK.FramebufferTarget.Framebuffer, Handle);
            TK.GL.FramebufferTexture2D(TK.FramebufferTarget.Framebuffer, TK.FramebufferAttachment.ColorAttachment0, TK.TextureTarget.Texture2D, ColorTexture.Handle, 0);
            TK.GL.FramebufferTexture2D(TK.FramebufferTarget.Framebuffer, TK.FramebufferAttachment.DepthAttachment, TK.TextureTarget.Texture2D, DepthTexture.Handle, 0);
		//	TK.GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.StencilAttachment, TextureTarget.Texture2D, StencilTexture.Handle, 0);

			End();
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~FrameBuffer()
		{
			System.Windows.Forms.MessageBox.Show("[FrameBuffer] : Call Dispose() !!");
			//throw new Exception("FrameBuffer : Call Dispose() !!");
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
			//    TK.GL.Ext.DeleteFramebuffers(1, ref FBOHandle);

			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Make the Render buffer current 
		/// </summary>
		public void Bind()
		{
            TK.GL.Ext.BindFramebuffer(TK.FramebufferTarget.FramebufferExt, Handle);

            TK.GL.PushAttrib(TK.AttribMask.ViewportBit);
			TK.GL.Viewport(0, 0, Size.Width, Size.Height);

            TK.GL.MatrixMode(TK.MatrixMode.Projection);
            TK.GL.PushMatrix();
            TK.GL.LoadIdentity();
            TK.GL.Ortho(0, Size.Width, 0, Size.Height, -1, 1);
		}



		/// <summary>
		/// 
		/// </summary>
		public void End()
		{
            TK.GL.MatrixMode(TK.MatrixMode.Projection);
			TK.GL.PopMatrix();

			TK.GL.PopAttrib();
            TK.GL.Ext.BindFramebuffer(TK.FramebufferTarget.FramebufferExt, 0);
		}



		#region Properties


		/// <summary>
		/// FBO handle
		/// </summary>
		int Handle;


/*
		/// <summary>
		/// State of the frame buffer
		/// </summary>
		public TK.FramebufferErrorCode Status
		{
			get
			{

				switch (TK.GL.Ext.CheckFramebufferStatus(TK.FramebufferTarget.FramebufferExt))
				{
                    case FramebufferTarget.FramebufferCompleteExt:
					{
						Trace.WriteLine("FBO: The framebuffer is complete and valid for rendering.");
						break;
					}
                    case TK.FramebufferTarget.FramebufferIncompleteAttachmentExt:
					{
						Trace.WriteLine("FBO: One or more attachment points are not framebuffer attachment complete. This could mean there’s no texture attached or the format isn’t renderable. For color textures this means the base format must be RGB or RGBA and for depth textures it must be a DEPTH_COMPONENT format. Other causes of this error are that the width or height is zero or the z-offset is out of range in case of render to volume.");
						break;
					}
                    case TK.FramebufferTarget.FramebufferIncompleteMissingAttachmentExt:
					{
						Trace.WriteLine("FBO: There are no attachments.");
						break;
					}
                    case TK.FramebufferTarget.FramebufferIncompleteDimensionsExt:
					{
						Trace.WriteLine("FBO: Attachments are of different size. All attachments must have the same width and height.");
						break;
					}
                    case TK.FramebufferTarget.FramebufferIncompleteFormatsExt:
					{
						Trace.WriteLine("FBO: The color attachments have different format. All color attachments must have the same format.");
						break;
					}
                    case TK.FramebufferTarget.FramebufferIncompleteDrawBufferExt:
					{
						Trace.WriteLine("FBO: An attachment point referenced by GL.DrawBuffers() doesn’t have an attachment.");
						break;
					}
                    case TK.FramebufferTarget.FramebufferIncompleteReadBufferExt:
					{
						Trace.WriteLine("FBO: The attachment point referenced by GL.ReadBuffers() doesn’t have an attachment.");
						break;
					}
                    case FramebufferTarget.FramebufferUnsupportedExt:
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

                return TK.GL.Ext.CheckFramebufferStatus(TK.FramebufferTarget.FramebufferExt);

			}
		}
*/

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
		public Texture2D ColorTexture
		{
			get;
			private set;
		}


		/// <summary>
		/// Depth texture
		/// </summary>
		public Texture2D DepthTexture
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

                TK.GL.GetRenderbufferParameter(TK.RenderbufferTarget.Renderbuffer, TK.RenderbufferParameterName.RenderbufferWidthExt, out width);
                TK.GL.GetRenderbufferParameter(TK.RenderbufferTarget.Renderbuffer, TK.RenderbufferParameterName.RenderbufferHeight, out height);

				return new Size(width, height);
			}
		}


		#endregion

	}
}
