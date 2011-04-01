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
using TK = OpenTK.Graphics.OpenGL;

//
//
// http://gamasutra.com/features/20060804/boutros_01.shtml
// http://www.opengl.org/wiki/Common_Mistakes
//
namespace ArcEngine.Graphic
{
	/// <summary>
	/// Handles the configuration and management of the video device.
	/// </summary>
	public static class Display
	{

		/// <summary>
		/// Static constructor
		/// </summary>
		static Display()
		{
			Trace.WriteDebugLine("[Display] Constructor()");

			Statistics = new RenderStats();
			RenderState = new RenderState();
			Scissors = new Stack<Rectangle>();

			textures = new Texture[32];
		}


		/// <summary>
		/// Resets default state
		/// </summary>
		public static void Init()
		{
			Trace.WriteDebugLine("[Display] Init()");
			if (Capabilities == null)
				Capabilities = new RenderDeviceCapabilities();


			RenderState.Blending = true;
			RenderState.ClearColor = Color.Black;

			TK.GL.Hint(TK.HintTarget.PolygonSmoothHint, TK.HintMode.Nicest);
			TK.GL.Hint(TK.HintTarget.LineSmoothHint, TK.HintMode.Nicest);
			TK.GL.Hint(TK.HintTarget.PointSmoothHint, TK.HintMode.Nicest);

			// Cause visual glitches with SpriteBatch !!!!
		//	TK.GL.Enable(TK.EnableCap.PolygonSmooth);

			if (Texture2D.HasTextureCompression)
				TK.GL.Hint(TK.HintTarget.TextureCompressionHint, TK.HintMode.Nicest);

			BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			RenderState.StencilClearValue = 0;
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		internal static void Dispose()
		{
			Trace.WriteDebugLine("[Display] : Dispose()");

			if (Texture.InUse.Count > 0)
			{
				Trace.WriteLine("{0} texture(s) remaining !", Texture.InUse.Count);
				foreach (Texture tex in Texture.InUse)
					Trace.WriteLine("Texture handle \"" + tex.Handle + "\"");
			}


			Shader = null;
			Texture = null;
		}


		/// <summary>
		/// Display Graphic device informations
		/// </summary>
		public static void Diagnostic()
		{
			Trace.WriteLine("Video informations :");
			Trace.Indent();


			// Dll version
			Trace.WriteLine("System informations :");
			string keybase = @"SYSTEM\ControlSet001\Control\Class\{4D36E968-E325-11CE-BFC1-08002BE10318}\";
			for (int i = 0; i < 32; i++)
			{
				string key = String.Format("{0:0000}", i);
				Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keybase + key);
				
				if (hklm == null)
					break;

				Trace.WriteLine("Video card {0} - {1} :", i, (string)hklm.GetValue("DriverDesc"));
				Trace.Indent();
				object obj = hklm.GetValue("OpenGLDriverName");
				if (obj != null)
					Trace.WriteLine("Driver name : {0}", ((string[])obj)[0]);
				Trace.WriteLine("Driver version : {0}", (string)hklm.GetValue("DriverVersion"));
				
				string[] str = ((string) hklm.GetValue("DriverDate")).Split('-');
				DateTime date = DateTime.Parse(str[1] + "-" + str[0] + "-" + str[2]);
				Trace.WriteLine("Driver date : {0}", date.ToLongDateString());
				Trace.Unindent();
			}

			Trace.WriteLine("");
			Trace.WriteLine("OpenGL informations :");
			Trace.WriteLine("Graphics card vendor : {0}", Capabilities.VideoVendor);
			Trace.WriteLine("Renderer : {0}", Capabilities.VideoRenderer);
			Trace.WriteLine("OpenGL Version : {0}", Capabilities.VideoVersion);
			Trace.WriteLine("GLSL Version : {0}", Capabilities.ShadingLanguageVersion);

			Trace.WriteLine("Display modes");
			Trace.Indent();
			foreach (OpenTK.DisplayDevice device in OpenTK.DisplayDevice.AvailableDisplays)
				Trace.WriteLine(device.ToString());
			Trace.Unindent();


			if (Capabilities.MajorVersion <= 2)
			{
				Trace.Write("Supported extensions ({0}) : ", Capabilities.Extensions.Count);
				foreach(string name in Capabilities.Extensions)
					Trace.Write("{0} ", name);
			}
			else
			{
				int count = 0;
				TK.GL.GetInteger(TK.GetPName.NumExtensions, out count);
				Trace.Write("Supported extensions ({0}) : ", count);
				for (int i = 0; i < count; i++)
					Trace.Write("{0}, ", TK.GL.GetString(TK.StringName.Extensions, i));
			}
			Trace.WriteLine("");

			Trace.Unindent();
		}


		/// <summary>
		/// Binds a texture to a texture image unit
		/// </summary>
		/// <param name="unit">Texture image unit id</param>
		/// <param name="texture">Texture handle</param>
		public static void SetTexture(int unit, Texture texture)
		{
			TextureUnit = unit;
			Texture = texture;
		}


		#region Scissor


		/// <summary>
		/// Pushes a new scissor zone and activate scissor if required
		/// </summary>
		/// <param name="rectangle">Clipped rectangle</param>
		public static void PushScissor(Rectangle rectangle)
		{
			Scissors.Push(rectangle);

			ScissorZone = rectangle;
			RenderState.Scissor = true;
		}


		/// <summary>
		/// Pops a scissor rectangle and deactivate scissor if stack is empty
		/// </summary>
		public static void PopScissor()
		{
			if (Scissors.Count == 0)
				return;

			Scissors.Pop();

			if (Scissors.Count > 0)
				ScissorZone = Scissors.Peek();

			if (Scissors.Count == 0)
				RenderState.Scissor = false;
		}


		/// <summary>
		/// Clears and deactivates the scissor zone
		/// </summary>
		public static void ClearScissor()
		{
			Scissors.Clear();
			RenderState.Scissor = false;
		}

		#endregion


		#region OpenGL

		/// <summary>
		/// Clears all buffers
		/// </summary>
		public static void ClearBuffers()
		{
			TK.GL.Clear(TK.ClearBufferMask.ColorBufferBit | TK.ClearBufferMask.DepthBufferBit | TK.ClearBufferMask.StencilBufferBit);
		}


		/// <summary>
		/// Clears the stencil buffer
		/// </summary>
		public static void ClearStencilBuffer()
		{
			TK.GL.Clear(TK.ClearBufferMask.StencilBufferBit);
		}


		/// <summary>
		/// Clears the color buffer
		/// </summary>
		public static void ClearColorBuffer()
		{
			TK.GL.Clear(TK.ClearBufferMask.ColorBufferBit);
		}


		/// <summary>
		/// Clears the depth buffer
		/// </summary>
		public static void ClearDepthBuffer()
		{
			TK.GL.Clear(TK.ClearBufferMask.DepthBufferBit);
		}


		/// <summary>
		/// Gets Opengl errors
		/// </summary>
		/// <param name="message">Message to display with the error</param>
		/// <returns>True on error</returns>
		public static bool GetLastError(string message)
		{
			TK.ErrorCode error = TK.GL.GetError();
			if (error == TK.ErrorCode.NoError)
				return false;


			System.Diagnostics.StackFrame stack = new System.Diagnostics.StackFrame(1, true);

			string msg = message + " => " + error.ToString();


			Trace.WriteLine("\"" + stack.GetFileName() + ":" + stack.GetFileLineNumber() + "\" => GL error : " + msg + " (" + error + ")");

			return true;
		}


		/// <summary>
		/// Specifies blending arithmetic
		/// </summary>
		/// <param name="source">Source factor</param>
		/// <param name="dest">Destination factor</param>
		public static void BlendingFunction(BlendingFactorSource source, BlendingFactorDest dest)
		{
			TK.GL.BlendFunc((TK.BlendingFactorSrc)source, (TK.BlendingFactorDest)dest);
		}


		/// <summary>
		/// Alpha test function
		/// </summary>
		/// <param name="function">Comparison function</param>
		/// <param name="reference">Reference value</param>
		public static void AlphaFunction(AlphaFunction function, float reference)
		{
			TK.GL.AlphaFunc((TK.AlphaFunction)function, reference);
		}


		/// <summary>
		/// enable and disable writing of frame buffer color components
		/// </summary>
		/// <param name="red">Red</param>
		/// <param name="green">Green</param>
		/// <param name="blue">Blue</param>
		/// <param name="alpha">Alpha</param>
		public static void ColorMask(bool red, bool green, bool blue, bool alpha)
		{
			TK.GL.ColorMask(red, green, blue, alpha);
		}

		#endregion


		#region Batchs


		/// <summary>
		/// Defines vertex attribute data
		/// </summary>
		/// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, or 4.</param>
		/// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. 
		/// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. </param>
		/// <param name="offset">Specifies a pointer to the first component of the first generic vertex attribute in the array. </param>
		public static void SetBufferDeclaration(int index, int size, int stride, int offset)
		{
			if (index < 0)
				return;

			TK.GL.VertexAttribPointer(index, size, TK.VertexAttribPointerType.Float, false, stride, offset);
		}



		/// <summary>
		/// Binds and draws an IndexBuffer
		/// </summary>
		/// <param name="buffer">Buffer handle</param>
		/// <param name="mode">Drawing mode</param>
		/// <param name="index">Index buffer</param>
		public static void DrawIndexBuffer(BatchBuffer buffer, PrimitiveType mode, IndexBuffer index)
		{
			if (buffer == null || index == null)
				return;

			// Set the index buffer
			index.Bind();

			// Bind shader
			buffer.Bind();

			// Draw
			TK.GL.DrawElements((TK.BeginMode)mode, index.Count, TK.DrawElementsType.UnsignedInt, IntPtr.Zero);

			Statistics.BatchCall++;
		}



		/// <summary>
		/// Draws a batch
		/// </summary>
		/// <param name="batch">Batch to draw</param>
		/// <param name="first">Specifies the starting index in the enabled arrays.</param>
		/// <param name="count">Specifies the number of indices to be rendered.</param>
		public static void DrawBatch(BatchBuffer batch, int first, int count)
		{
			DrawBatch(batch, PrimitiveType.Triangles, first, count);
		}



		/// <summary>
		/// Draws a user batch
		/// </summary>
		/// <param name="batch">Batch to draw</param>
		/// <param name="mode">Drawing mode</param>
		/// <param name="first">Specifies the starting index in the enabled arrays.</param>
		/// <param name="count">Specifies the number of indices to be rendered.</param>
		public static void DrawBatch(BatchBuffer batch, PrimitiveType mode, int first, int count)
		{
			// No batch, or empty batch
			if (batch == null)
				return;

			// Bind buffer
			batch.Bind();

			TK.GL.DrawArrays((TK.BeginMode)mode, first, count);

			Statistics.BatchCall++;

			return;
		}


		/// <summary>
		/// Enables a buffer index 
		/// </summary>
		/// <param name="id">ID of the buffer</param>
		public static void EnableBufferIndex(int id)
		{
			if (id < 0)
				return;

			TK.GL.EnableVertexAttribArray(id);
		}


		/// <summary>
		/// Disables a buffer index
		/// </summary>
		/// <param name="id">ID of the buffer</param>
		public static void DisableBufferIndex(int id)
		{
			TK.GL.DisableVertexAttribArray(id);
		}

		#endregion


		#region Stencil

		/// <summary>
		/// Stencil test action
		/// </summary>
		/// <param name="fail">Specifies the action to take when the stencil test fails</param>
		/// <param name="zfail">Specifies the action when the stencil test passes, but the depth test fails</param>
		/// <param name="zpass">Specifies the action when both the stencil test and the depth test pass, or when the 
		/// stencil test passes and either there is no depth buffer or depth testing is not enabled</param>
		public static void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass)
		{
			TK.GL.StencilOp((TK.StencilOp)fail, (TK.StencilOp)zfail, (TK.StencilOp)zpass);
		}


		/// <summary>
		/// Stencil test function
		/// </summary>
		/// <param name="function">Test function</param>
		/// <param name="reference">Reference value</param>
		/// <param name="mask">Mask</param>
		public static void StencilFunction(StencilFunction function, int reference, int mask)
		{
			TK.GL.StencilFunc((TK.StencilFunction)function, reference, mask);
		}

		#endregion


		#region Polygon mode


		/// <summary>
		/// Select a polygon rasterization mode
		/// </summary>
		/// <param name="face">Specifies the polygons that mode applies to</param>
		/// <param name="mode">Specifies how polygons will be rasterized</param>
		public static void PolygonMode(MaterialFace face, PolygonMode mode)
		{
			TK.GL.PolygonMode((TK.MaterialFace)face, (TK.PolygonMode)mode);
		}



		/// <summary>
		/// Set the scale and units used to calculate depth values
		/// </summary>
		/// <param name="factor">Specifies a scale factor that is used to create a variable
		/// depth offset for each polygon</param>
		/// <param name="unit">value tocreate a constant depth offset</param>
		public static void PolygonOffset(float factor, float unit)
		{
			TK.GL.PolygonOffset(factor, unit);
		}

		#endregion


		#region Properties


		/// <summary>
		/// Defines the render state of a graphics device.
		/// </summary>
		public static RenderState RenderState
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets or sets the current Shader
		/// </summary>
		public static Shader Shader
		{
			get
			{
				return shader;
			}
			set
			{
				shader = value;
				TK.GL.UseProgram(shader == null ? 0 : shader.ProgramID);

				Statistics.ShaderBinding++;
			}
		}
		static Shader shader;


		/// <summary>
		/// Current texture
		/// </summary>
		static public Texture Texture
		{
			set
			{
				if (value == null)
				{
					//Trace.WriteDebugLine("[Display] : Bind null texture on TU {0}", TextureUnit);

					//TK.GL.BindTexture((TK.TextureTarget)value.Target, 0);
					textures[TextureUnit] = null;
					return;
				}


	//			if (textures[TextureUnit] != value)
				{					
					textures[TextureUnit] = value;
					TK.GL.BindTexture((TK.TextureTarget)value.Target, value.Handle);

					Statistics.TextureBinding++;
				}
			}
			get
			{
				return textures[TextureUnit];
			}
		}
		static Texture[] textures;


		/// <summary>
		/// Gets or sets the texture unit
		/// </summary>
		static public int TextureUnit
		{
			get
			{
				return textureUnit;
			}

			set
			{
				//if (value > Capabilities.MaxTextureUnits || value < 0)
				//	return;

				TK.GL.ActiveTexture(TK.TextureUnit.Texture0 + value);
				textureUnit = value;
			}
		}
		static int textureUnit;


		/// <summary>
		/// Sets a texture environment 
		/// </summary>
		static public TextureEnvMode TexEnv
		{
			get
			{
				float[] mode = new float[1];

				TK.GL.GetTexEnv(TK.TextureEnvTarget.TextureEnv, TK.TextureEnvParameter.TextureEnvMode, mode);

				return (TextureEnvMode)mode[0];
			}

			set
			{
				TK.GL.TexEnv(TK.TextureEnvTarget.TextureEnv, TK.TextureEnvParameter.TextureEnvMode, (float)value);
			}
		}


		/// <summary>
		/// Gets/sets the viewport
		/// </summary>
		public static Rectangle ViewPort
		{
			get
			{
				int[] tab = new int[4];

				TK.GL.GetInteger(TK.GetPName.Viewport, tab);

				return new Rectangle(tab[0], tab[1], tab[2], tab[3]);
			}
			set
			{
				Rectangle rect = value;
				if (rect.Width == 0)
					rect.Width = 1;

				TK.GL.Viewport(0, 0, rect.Width, rect.Height);
			}
		}

/*
		/// <summary>
		/// Enables/disables 2d texture
		/// </summary>
		public static bool Texturing
		{
			get
			{
				return TK.GL.IsEnabled(TK.EnableCap.Texture2D);
			}
			set
			{
				if (value)
					TK.GL.Enable(TK.EnableCap.Texture2D);
				else
					TK.GL.Disable(TK.EnableCap.Texture2D);

			}
		}
*/

		/// <summary>
		/// Gets/sets the scissor zone
		/// </summary>
		public static Rectangle ScissorZone
		{
			get
			{
				int[] rect = new int[4];
				TK.GL.GetInteger(TK.GetPName.ScissorBox, rect);

				return new Rectangle(rect[0], rect[1], rect[2], rect[3]);
			}
			private set
			{
				//GL.Scissor(scissorZone.X,scissorZone.Bottom, scissorZone.Width,  scissorZone.Top - scissorZone.Bottom);
				TK.GL.Scissor(value.X, ViewPort.Height - value.Top - value.Height, value.Width, value.Height);
			}
		}


		/// <summary>
		/// Scissors stack
		/// </summary>
		static Stack<Rectangle> Scissors;


		/// <summary>
		/// Render device capabilities
		/// </summary>
		public static RenderDeviceCapabilities Capabilities
		{
			get;
			private set;
		}

		
		/// <summary>
		/// Rendering stats
		/// </summary>
		public static RenderStats Statistics
		{
			get;
			private set;
		}


		#endregion
	}


	/// <summary>
	/// Interpretation of polygons for rasterization
	/// </summary>
	public enum MaterialFace
	{
		/// <summary>
		/// Front-facing polygons 
		/// </summary>
		Front = TK.MaterialFace.Front,

		/// <summary>
		/// Back-facing polygons 
		/// </summary>
		Back = TK.MaterialFace.Back,

		/// <summary>
		/// Both
		/// </summary>
		FrontAndBack = TK.MaterialFace.FrontAndBack,
	}


	/// <summary>
	/// Final rasterization of polygons
	/// </summary>
	public enum PolygonMode
	{
		/// <summary>
		/// Polygon vertices that are marked as the start of a boundary edge are drawn as points
		/// </summary>
		Point = TK.PolygonMode.Point,

		/// <summary>
		/// Boundary edges of the polygon are drawn as line segments
		/// </summary>
		Line = TK.PolygonMode.Line,

		/// <summary>
		/// The interior of the polygon is filled
		/// </summary>
		Fill = TK.PolygonMode.Fill,
	}


	/// <summary>
	/// Supported device capabilities
	/// </summary>
	public class RenderDeviceCapabilities
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public RenderDeviceCapabilities()
		{
			int integer = 0;

			string version = TK.GL.GetString(TK.StringName.Version);
			if (version[0] - '0' >= 3)
			{
				int value = 0;
				TK.GL.GetInteger(TK.GetPName.MajorVersion, out value);
				MajorVersion = value;
				TK.GL.GetInteger(TK.GetPName.MinorVersion, out value);
				MinorVersion = value;


				Extensions = new List<string>();
				int count = 0;
				TK.GL.GetInteger(TK.GetPName.NumExtensions, out count);
				for (int id = 0; id < count; id++)
				{
					Extensions.Add(TK.GL.GetString(TK.StringName.Extensions, id));
				}
			}
			else
			{
				MajorVersion = version[0] - '0';
				MinorVersion = version[2] - '0';

				Extensions = new List<string>(TK.GL.GetString(TK.StringName.Extensions).Split(new char[] { ' ' }));
		
			}


			if (Extensions.Contains("GL_ARB_texture_non_power_of_two"))
				HasNonPowerOf2Textures = true;

			if (Extensions.Contains("GL_ARB_framebuffer_object"))
				HasFBO = true;

			if (Extensions.Contains("GL_EXT_texture_filter_anisotropic"))
			{
				HasAnisotropicFiltering = true;

				float val;
				TK.GL.GetFloat((TK.GetPName)TK.ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out val);
				MaxAnisotropicFilter = val;
			}

			if (Extensions.Contains("GL_ARB_texture_compression"))
			{
				HasTextureCompression = true;
				TK.GL.Hint(TK.HintTarget.TextureCompressionHint, TK.HintMode.Nicest);
			}

			if (Extensions.Contains("GL_ARB_pixel_buffer_object"))
				HasPBO = true;

			if (Extensions.Contains("GL_ARB_vertex_buffer_object"))
				HasVBO = true;

			if (Extensions.Contains("GL_ARB_multisample"))
			{
				HasMultiSample = true;
				TK.GL.GetInteger(TK.GetPName.SampleBuffers, out integer);
				MaxMultiSample = integer;
			}

			TK.GL.GetInteger(TK.GetPName.MaxVertexTextureImageUnits, out integer);
			MaxVertexTextureImageUnits = integer;

			TK.GL.GetInteger(TK.GetPName.MaxTextureImageUnits, out integer);
			MaxTextureImageUnits = integer;

			TK.GL.GetInteger(TK.GetPName.MaxCombinedTextureImageUnits, out integer);
			MaxCombinedTextureImageUnits = integer;

			TK.GL.GetInteger(TK.GetPName.MaxColorAttachments, out integer);
			MaxColorAttachments = integer;

			TK.GL.GetInteger(TK.GetPName.MaxTextureCoords, out integer);
			MaxTextureCoords = integer;

			TK.GL.GetInteger(TK.GetPName.MaxDrawBuffers, out integer);
			MaxDrawBuffers = integer;


			#region GL_NVX_gpu_memory_info
			if (Extensions.Contains("GL_NVX_gpu_memory_info"))
			{
				Trace.Indent();
				int val = 0;

				const int GPU_MEMORY_INFO_DEDICATED_VIDMEM_NVX = 0x9047;
				const int GPU_MEMORY_INFO_TOTAL_AVAILABLE_MEMORY_NVX = 0x9048;
				const int GPU_MEMORY_INFO_CURRENT_AVAILABLE_VIDMEM_NVX = 0x9049;
				const int GPU_MEMORY_INFO_EVICTION_COUNT_NVX = 0x904A;
				const int GPU_MEMORY_INFO_EVICTED_MEMORY_NVX = 0x904B;

				TK.GL.GetInteger((TK.GetPName)GPU_MEMORY_INFO_DEDICATED_VIDMEM_NVX, out val);
				Trace.WriteLine("Dedicated video memory : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)GPU_MEMORY_INFO_TOTAL_AVAILABLE_MEMORY_NVX, out val);
				Trace.WriteLine("Total available memory : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)GPU_MEMORY_INFO_CURRENT_AVAILABLE_VIDMEM_NVX, out val);
				Trace.WriteLine("Current available dedicated video memory : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)GPU_MEMORY_INFO_EVICTION_COUNT_NVX, out val);
				Trace.WriteLine("Total evictions : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)GPU_MEMORY_INFO_EVICTED_MEMORY_NVX, out val);
				Trace.WriteLine("Total video memory evicted : {0} Kb", val);
				Trace.Unindent();
			}
			#endregion


			#region GL_ATI_meminfo
			if (Extensions.Contains("GL_ATI_meminfo"))
			{
				Trace.Indent();
				int[] val = new int[4];

				const int VBO_FREE_MEMORY_ATI = 0x87FB;
				const int TEXTURE_FREE_MEMORY_ATI = 0x87FC;
				const int RENDERBUFFER_FREE_MEMORY_ATI = 0x87FD;

				TK.GL.GetInteger((TK.GetPName)VBO_FREE_MEMORY_ATI, val);
				Trace.WriteLine("VBO_FREE_MEMORY_ATI");
				Trace.WriteLine("total memory free in the pool : {0} Kb", val[0]);
				Trace.WriteLine("largest available free block in the pool : {0} Kb", val[1]);
				Trace.WriteLine("total auxiliary memory free : {0} Kb", val[2]);
				Trace.WriteLine("largest auxiliary free block : {0} Kb", val[3]);

				val = new int[4];
				TK.GL.GetInteger((TK.GetPName)TEXTURE_FREE_MEMORY_ATI, val);
				Trace.WriteLine("TEXTURE_FREE_MEMORY_ATI");
				Trace.WriteLine("total memory free in the pool : {0} Kb", val[0]);
				Trace.WriteLine("largest available free block in the pool : {0} Kb", val[1]);
				Trace.WriteLine("total auxiliary memory free : {0} Kb", val[2]);
				Trace.WriteLine("largest auxiliary free block : {0} Kb", val[3]);

				val = new int[4];
				TK.GL.GetInteger((TK.GetPName)RENDERBUFFER_FREE_MEMORY_ATI, val);
				Trace.WriteLine("RENDERBUFFER_FREE_MEMORY_ATI");
				Trace.WriteLine("total memory free in the pool : {0} Kb", val[0]);
				Trace.WriteLine("largest available free block in the pool : {0} Kb", val[1]);
				Trace.WriteLine("total auxiliary memory free : {0} Kb", val[2]);
				Trace.WriteLine("largest auxiliary free block : {0} Kb", val[3]);

				Trace.Unindent();

			}
			#endregion


			#region Shader version
			string[] tmp = ShadingLanguageVersion.Split(new char[] { ' ' });
			if (tmp[0].StartsWith("1.20"))
				ShaderVersion = ShaderVersion.GLSL_1_20;
			else if (tmp[0].StartsWith("1.30"))
				ShaderVersion = ShaderVersion.GLSL_1_30;
			else if (tmp[0].StartsWith("1.40"))
				ShaderVersion = ShaderVersion.GLSL_1_40;
			else if (tmp[0].StartsWith("1.50"))
				ShaderVersion = ShaderVersion.GLSL_1_50;
			else if (tmp[0].StartsWith("3.30"))
				ShaderVersion = ShaderVersion.GLSL_3_30;
			else if (tmp[0].StartsWith("4.00"))
				ShaderVersion = ShaderVersion.GLSL_4_00;
			else if (tmp[0].StartsWith("4.10"))
				ShaderVersion = ShaderVersion.GLSL_4_10;
			else
				ShaderVersion = ShaderVersion.Unsuported;
			#endregion
		}


		/// <summary>
		/// Has multi sample support
		/// </summary>
		public bool HasMultiSample
		{
			get;
			internal set;
		}


		/// <summary>
		/// The maximum number of texture coordinate sets available to vertex and fragment shaders.
		/// </summary>
		public int MaxMultiSample
		{
			get;
			private set;
		}


		/// <summary>
		/// The maximum number of draw buffers supported.
		/// </summary>
		public int MaxDrawBuffers
		{
			get;
			private set;
		}

		/// <summary>
		/// The maximum number of color atachments.
		/// </summary>
		public int MaxColorAttachments
		{
			get;
			private set;
		}

/*
		/// <summary>
		/// Total number of texture image units from the fragment 
		/// and vertex processor can access combined.
		/// </summary>
		public int MaxTextureUnits
		{
			get;
			private set;
		}
*/

		/// <summary>
		/// Maximum number of available texture image units 
		/// </summary>
		public int MaxVertexTextureImageUnits
		{
			get;
			private set;
		}


		/// <summary>
		/// Maximum number of supported texture image units 
		/// </summary>
		public int MaxTextureImageUnits
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public int MaxCombinedTextureImageUnits
		{
			get;
			private set;
		}


		/// <summary>
		/// Maximum number of supported texture coordinate sets 
		/// </summary>
		public int MaxTextureCoords
		{
			get;
			private set;
		}


		/// <summary>
		/// Has non power of two texture support
		/// </summary>
		public bool HasNonPowerOf2Textures
		{
			get;
			private set;
		}


		/// <summary>
		/// Has Frame Buffer Objects support
		/// </summary>
		public bool HasFBO
		{
			get;
			private set;
		}


		/// <summary>
		/// Has texture compression
		/// </summary>
		public bool HasTextureCompression
		{
			get;
			private set;
		}


		/// <summary>
		/// Has Vertex Buffer Objects support
		/// </summary>
		public bool HasVBO
		{
			get;
			private set;
		}

	
		/// <summary>
		/// Has Anisotropic filtering support
		/// </summary>
		public bool HasAnisotropicFiltering
		{
			get;
			private set;
		}


		/// <summary>
		/// Maximum anisotropic filter value
		/// </summary>
		public float MaxAnisotropicFilter
		{
			get;
			private set;
		}


		/// <summary>
		/// Has Pixel Buffer Objects support
		/// </summary>
		public bool HasPBO
		{
			get;
			private set;
		}


		/// <summary>
		/// Returns the name of the graphic card vendor. 
		/// </summary>
		public string VideoVendor
		{
			get
			{
				return TK.GL.GetString(TK.StringName.Vendor);
			}
		}


		/// <summary>
		/// Returns the name of the graphic card. 
		/// </summary>
		public string VideoRenderer
		{
			get
			{
				return TK.GL.GetString(TK.StringName.Renderer);
			}
		}


		/// <summary>
		/// Returns OpenGL version
		/// </summary>
		public string VideoVersion
		{
			get
			{
				return TK.GL.GetString(TK.StringName.Version);
			}
		}


		/// <summary>
		/// This function returns the OpenGL Shading Language version that is supported by the engine
		/// </summary>
		public string ShadingLanguageVersion
		{
			get
			{
				return TK.GL.GetString(TK.StringName.ShadingLanguageVersion);
			}
		}


		/// <summary>
		/// Major version of the Context
		/// </summary>
		public int MajorVersion
		{
			get;
			private set;
		}


		/// <summary>
		/// Minor version of the Context
		/// </summary>
		public int MinorVersion
		{
			get;
			private set;
		}


		/// <summary>
		/// Shader language version
		/// </summary>
		public ShaderVersion ShaderVersion
		{
			get;
			private set;
		}

		#region Properties

		
		/// <summary>
		/// List of extensions
		/// </summary>
		public List<string> Extensions
		{
			get;
			private set;
		}


		#endregion

	}


	/// <summary>
	/// Statistics for a rendered scene
	/// </summary>
	public class RenderStats
	{


		/// <summary>
		/// Reset counters
		/// </summary>
		internal void Reset()
		{
			BatchCall = 0;
			TextureBinding = 0;
			ShaderBinding = 0;
		}


		#region Properties

		/// <summary>
		/// Number of shader binding
		/// </summary>
		public int ShaderBinding
		{
			get;
			internal set;
		}

		/// <summary>
		/// Number of batch render call
		/// </summary>
		public int BatchCall
		{
			get;
			internal set;
		}


		/// <summary>
		/// Number of textures used when drawing this frame.
		/// </summary>
		public int TextureBinding
		{
			get;
			internal set;
		}




		#endregion
	}


	/// <summary>
	/// GameWindow creation parameters
	/// </summary>
	public class GameWindowParams
	{

		/// <summary>
		/// Game window size
		/// </summary>
		public Size Size = new Size(1024, 768);

		/// <summary>
		/// Major version
		/// </summary>
		public int Major = 3;

		/// <summary>
		/// Minor version
		/// </summary>
		public int Minor = 0;

		/// <summary>
		/// FSAA buffer 
		/// </summary>
		public int Samples = 0;

		/// <summary>
		/// Color buffer depth
		/// </summary>
		public int Color = 32;

		/// <summary>
		/// Depth buffer depth
		/// </summary>
		public int Depth = 24;

		/// <summary>
		/// Stencil color depth
		/// </summary>
		public int Stencil = 8;


		/// <summary>
		/// Full screen mode
		/// </summary>
		public bool FullScreen = false;


		/// <summary>
		/// Compatible with old mode (previous OpenGL 3.0)
		/// </summary>
		public bool Compatible = false;
	}


	/// <summary>
	/// Defines how data in a vertex stream is interpreted during a draw call
	/// </summary>
	public enum PrimitiveType
	{
		/// <summary>
		/// Renders the specified vertices as a sequence of isolated triangles.
		/// Each group of three vertices defines a separate triangle. 
		/// </summary>
		Triangles = TK.BeginMode.Triangles,

		/// <summary>
		/// Renders the vertices as a triangle fan.
		/// </summary>
		TriangleFan = TK.BeginMode.TriangleFan,

		/// <summary>
		/// Renders the vertices as a triangle strip
		/// </summary>
		TriangleStrip = TK.BeginMode.TriangleStrip,

		/// <summary>
		/// Renders the vertices as a collection of isolated points.
		/// </summary>
		Points = TK.BeginMode.Points,

		/// <summary>
		/// Renders the vertices as a list of isolated straight line segments
		/// </summary>
		Lines = TK.BeginMode.Lines,

		/// <summary>
		/// Renders the vertices as a single polyline
		/// </summary>
		LineStrip = TK.BeginMode.LineLoop,
	}


	/// <summary>
	/// Stencil operation
	/// </summary>
	public enum StencilOp
	{
		/// <summary>
		/// Set Stencil Buffer to 0.
		/// </summary>
		Zero = TK.StencilOp.Zero,

		/// <summary>
		/// Bitwise invert
		/// </summary>
		Invert = TK.StencilOp.Invert,

		/// <summary>
		/// Do not modify the Stencil Buffer
		/// </summary>
		Keep = TK.StencilOp.Keep,

		/// <summary>
		/// set Stencil Buffer to ref value as specified by last GL.StencilFunc() call
		/// </summary>
		Replace = TK.StencilOp.Replace,

		/// <summary>
		/// increment Stencil Buffer by 1. It is clamped at 255
		/// </summary>
		Incr = TK.StencilOp.Incr,

		/// <summary>
		/// decrement Stencil Buffer by 1. It is clamped at 0.
		/// </summary>
		Decr = TK.StencilOp.Decr,

		/// <summary>
		/// increment Stencil Buffer by 1. If the result is greater than 255, it becomes 0.
		/// </summary>
		IncrWrap = TK.StencilOp.IncrWrap,

		/// <summary>
		/// decrement Stencil Buffer by 1. If the result is less than 0, it becomes 255.
		/// </summary>
		DecrWrap = TK.StencilOp.DecrWrap,
	}


	/// <summary>
	/// Stencil function
	/// </summary>
	public enum StencilFunction
	{
		/// <summary>
		/// Test will never succeed
		/// </summary>
		Never = TK.StencilFunction.Never,

		/// <summary>
		/// Test will succeed if ( ref & mask ) < ( pixel & mask )
		/// </summary>
		Less = TK.StencilFunction.Less,

		/// <summary>
		/// Test will succeed if ( ref & mask ) == ( pixel & mask )
		/// </summary>
		Equal = TK.StencilFunction.Equal,

		/// <summary>
		/// Test will succeed if ( ref & mask ) <= ( pixel & mask )
		/// </summary>
		Lequal = TK.StencilFunction.Lequal,

		/// <summary>
		/// Test will succeed if ( ref & mask ) > ( pixel & mask )
		/// </summary>
		Greater = TK.StencilFunction.Greater,

		/// <summary>
		/// Test will succeed if ( ref & mask ) != ( pixel & mask )
		/// </summary>
		Notequal = TK.StencilFunction.Notequal,

		/// <summary>
		/// Test will succeed if ( ref & mask ) >= ( pixel & mask )
		/// </summary>
		Gequal = TK.StencilFunction.Gequal,

		/// <summary>
		/// Test will always succeed
		/// </summary>
		Always = TK.StencilFunction.Always,
	}


	/// <summary>
	/// Alpha test function
	/// </summary>
	public enum AlphaFunction
	{
		/// <summary>
		/// Never passes
		/// </summary>
		Never = TK.AlphaFunction.Never,

		/// <summary>
		/// Passes if the incoming alpha value is less than the reference value.
		/// </summary>
		Less = TK.AlphaFunction.Less,

		/// <summary>
		/// Passes if the incoming alpha value is equal to the reference value.
		/// </summary>
		Equal = TK.AlphaFunction.Equal,

		/// <summary>
		/// Passes if the incoming alpha value is less than or equal to the reference value.
		/// </summary>
		Lequal = TK.AlphaFunction.Lequal,

		/// <summary>
		/// Passes if the incoming alpha value is greater than the reference value.
		/// </summary>
		Greater = TK.AlphaFunction.Greater,

		/// <summary>
		/// Passes if the incoming alpha value is not equal to the reference value
		/// </summary>
		Notequal = TK.AlphaFunction.Notequal,

		/// <summary>
		/// Passes if the incoming alpha value is greater than or equal to the reference value.
		/// </summary>
		Gequal = TK.AlphaFunction.Gequal,

		/// <summary>
		/// Always passes.
		/// </summary>
		Always = 519,
	}
}
