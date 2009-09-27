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
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
//
//
//
// - GameScreen.PrimaryScreen.WorkingArea property, which holds information about your screen width/height etc.
// ChangeDisplaySettings : Permet de changer les réglages d’un périphérique d’affichage. Prend en entrée le nom du périphérique (retourné par EnumDisplayDevices) ainsi que les réglages désirés.
// EnumDisplayDevices : Retourne les périphériques d’affichage de la session courante.
// EnumDisplaySettings : Retourne les réglages actuel du périphérique donnés en entrée (EnumDisplayDevices)
// http://www.pinvoke.net/default.aspx/user32/ChangeDisplaySettingsEx.html
//
// http://www.gamedev.net/community/forums/topic.asp?topic_id=418397
// http://www.pinvoke.net/default.aspx/coredll/ChangeDisplaySettingsEx.html
// http://www.codeproject.com/KB/cs/csdynamicscrres.aspx
//
//
//
// - http://hge.relishgames.com/
//
// - Rounded rectangle : http://www.experts-exchange.com/Programming/Game/Game_Graphics/OpenGL/Q_21660812.html
//
//
//
// http://gamasutra.com/features/20060804/boutros_01.shtml
//
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
			RenderStats = new RenderStats();
		}



		/// <summary>
		/// Resets default state
		/// </summary>
		public static void Init()
		{
			Texturing = true;
			Blending = true;
			ClearColor = Color.Black;
			Culling = false;
			DepthTest = false;
			BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

		}


		/// <summary>
		/// Display Graphic device informations
		/// </summary>
		public static void TraceInfos()
		{
			Trace.WriteLine("Video informations :");
			Trace.Indent();
			Trace.WriteLine("Graphics card vendor : {0}", GL.GetString(StringName.Vendor));
			Trace.WriteLine("Renderer : {0}", GL.GetString(StringName.Renderer));

			//int major, minor;
			//GL.GetInteger(GetPName.MajorVersion, out major);
			//GL.GetInteger(GetPName.MinorVersion, out minor);
			//Trace.WriteLine("Version : {0} ({1}, {2})", GL.GetString(StringName.Version), major, minor);


			Trace.WriteLine("Display modes");
			Trace.Indent();

			foreach (DisplayDevice device in DisplayDevice.AvailableDisplays)
				Trace.WriteLine(device.ToString());

			Trace.Unindent();
			Trace.Unindent();


		}


		#region Fading


		/// <summary>
		/// Fades the screen to black
		/// </summary>
		/// <param name="speed">MaxVelocity in milliseconds</param>
		public static void FadeToBlack(int speed)
		{
			System.Threading.Thread.Sleep(speed);
		}


		/// <summary>
		/// Fades the screen to a specified color
		/// </summary>
		/// <param name="col">Fade to color</param>
		/// <param name="speed">MaxVelocity in milliseconds</param>
		public static void FadeTo(Color col, int speed)
		{
			System.Threading.Thread.Sleep(speed);
		}


		#endregion


		#region OpenGL

		/// <summary>
		/// Clears buffers
		/// </summary>
		public static void ClearBuffers()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
		}


		/// <summary>
		/// Gets Opengl errors
		/// </summary>
		public static void GetLastError(string command)
		{
			ErrorCode error = GL.GetError();
			if (error == ErrorCode.NoError)
				return;


			System.Diagnostics.StackFrame stack = new System.Diagnostics.StackFrame(1, true);

			string msg = command + " => " + error.ToString();


			//Log.Send(new LogEventArgs(LogLevel.Error, "\"" + stack.GetFileName() + ":" + stack.GetFileLineNumber() + "\" => GL error : " + msg + " (" + error + ")", null));
			Trace.WriteLine("\"" + stack.GetFileName() + ":" + stack.GetFileLineNumber() + "\" => GL error : " + msg + " (" + error + ")");

		}


		/// <summary>
		/// Specifies blending arithmetic
		/// </summary>
		/// <param name="source"></param>
		/// <param name="dest"></param>
		public static void BlendingFunction(BlendingFactorSrc source, BlendingFactorDest dest)
		{
			GL.BlendFunc(source, dest);
		}

		#endregion


        #region Transformation matrix


        /// <summary>
        /// Changes the transformation matrix to apply a translation transformation with the given characteristics.
		/// </summary>
		/// <param name="x"></param>
        /// <param name="y"></param>
		public static void Translate(float x, float y)
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.Translate(x, y, 0);
		}


		/// <summary>
        /// Changes the transformation matrix to apply a rotation transformation with the given characteristics.
		/// </summary>
		/// <param name="angle">The angle of rotation, in degrees.</param>
		public static void Rotate(float angle)
		{

			GL.MatrixMode(MatrixMode.Projection);
			GL.Rotate(angle, 0, 0, 1.0f);

		}



        /// <summary>
        /// Changes the transformation matrix to apply the matrix given by the arguments as described below.
        /// </summary>
        /// <remarks>http://en.wikipedia.org/wiki/Transformation_matrix#Examples_in_2D_graphics</remarks>
        /// <param name="m11"></param>
        /// <param name="m12"></param>
        /// <param name="m21"></param>
        /// <param name="m22"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static void Transform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            float[] val = new float[16];
            GL.GetFloat(GetPName.ProjectionMatrix, val);
            
            float[] values = new float[]
            {
                m11, m12, dx, 0,
                m21, m22, dy, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            };
            GL.MatrixMode(MatrixMode.Projection);
            GL.MultMatrix(values);
        }


        /// <summary>
        /// Changes the transformation matrix to the matrix given by the arguments as described below.
        /// </summary>
        /// <param name="m11"></param>
        /// <param name="m12"></param>
        /// <param name="m21"></param>
        /// <param name="m22"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static void SetTransform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            DefaultMatrix();
            Transform(m11, m12, m21, m22, dx, dy);
        }


        /// <summary>
        /// Changes the transformation matrix to apply a scaling transformation with the given characteristics.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Scale(float x, float y)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.Scale(x, y, 1.0f);
        }



		/// <summary>
		/// Replaces the current matrix with the identity matrix.
		/// </summary>
		public static void DefaultMatrix()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(ViewPort.Left, ViewPort.Width, ViewPort.Height, ViewPort.Top, -1, 1);
		}



        /// <summary>
        /// Push a copy of the current drawing state onto the drawing state stack 
        /// </summary>
        public static void Save()
        {
            GL.PushAttrib(AttribMask.AllAttribBits);

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
        }


        /// <summary>
        /// Pop the top entry in the drawing state stack, and reset the drawing state it describes.
        /// </summary>
        public static void Restore()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.PopAttrib();
        }


        #endregion


		/// <summary>
		/// Draws a colored rectangle
		/// </summary>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="fill">Fill in the rectangle or not</param>
		/// <param name="angle">Angle of rotation</param>
		/// <param name="origin">The origin of the rectangle. Specify (0,0) for the upper-left corner.</param>
		public static void Rectangle(Rectangle rect, bool fill, float angle, Point origin)
		{
			Translate(rect.Location.X + origin.X, rect.Location.Y + origin.Y);
			Rotate(angle);

			Rectangle(new Rectangle(-origin.X, -origin.Y, rect.Size.Width, rect.Size.Height), fill);
			DefaultMatrix();
		}



		#region Drawing

		/// <summary>
		/// Draws a colored rectangle
		/// </summary>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="fill">Fill in the rectangle or not</param>
		public static void Rectangle(Rectangle rect, bool fill)
		{
			Texturing = false;

			if (fill)
				GL.Begin(BeginMode.Quads);
			else
				GL.Begin(BeginMode.LineLoop);
			GL.Vertex2(rect.X, rect.Y);
			GL.Vertex2(rect.X, rect.Bottom);
			GL.Vertex2(rect.Right, rect.Bottom);
			GL.Vertex2(rect.Right, rect.Y);
			GL.End();
			Texturing = true;

			RenderStats.DirectCall += 4;
		}


		/// <summary>
		/// Draws a line from point "from" to point "to"
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static void Line(Point from, Point to)
		{
			Texturing = false;
			GL.Begin(BeginMode.Lines);
			GL.Vertex2(from.X, from.Y);
			GL.Vertex2(to.X, to.Y);
			GL.End();
			Texturing = true;

			RenderStats.DirectCall += 2;
		}


		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="point"></param>
		public static void Plot(Point point)
		{
			Texturing = false;
			GL.Begin(BeginMode.Points);
			GL.Vertex2(point.X, point.Y);
			GL.End();
			Texturing = true;

			RenderStats.DirectCall++;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="radius"></param>
		public static void DrawCircle(Point location, float radius)
		{

			Texturing = false;

			float DEG2RAD = 3.14159f / 180.0f;


			GL.Begin(BeginMode.LineLoop);

			for (int i = 0; i < 360; i++)
			{
				float degInRad = i * DEG2RAD;
				GL.Vertex2(Math.Cos(degInRad) * radius + location.X, Math.Sin(degInRad) * radius + location.Y);

			}

			GL.End();

			Texturing = true;

			RenderStats.DirectCall += 360;
		}


		#endregion


		#region Shared textures


		/// <summary>
		/// Creates a shared texture
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Texture CreateSharedTexture(string name)
		{

			// Texture already exist, so return it
			if (SharedTextures.ContainsKey(name))
				return SharedTextures[name];

			// Else create the texture
			SharedTextures[name] = new Texture();
			return SharedTextures[name];
		}



		/// <summary>
		/// Deletes a shared texture
		/// </summary>
		/// <param name="name"></param>
		public static void DeleteSharedTexture(string name)
		{
			SharedTextures[name] = null;
		}


		/// <summary>
		/// Removes all shared textures
		/// </summary>
		public static void DeleteSharedTextures()
		{
			SharedTextures.Clear();
		}

		#endregion


		#region Batchs

		/// <summary>
		/// Draws a batch
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="mode"></param>
		public static void DrawBatch(Batch batch, BeginMode mode)
		{
			//if (true)
			//{
				// Vertex
				GL.EnableClientState(EnableCap.VertexArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, batch.BufferID[0]);
				GL.VertexPointer(2, VertexPointerType.Int, 0, IntPtr.Zero);


				// Texture
				GL.EnableClientState(EnableCap.TextureCoordArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, batch.BufferID[1]);
				GL.TexCoordPointer(2, TexCoordPointerType.Int, 0, IntPtr.Zero);

				// Color
				GL.EnableClientState(EnableCap.ColorArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, batch.BufferID[2]);
				GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, IntPtr.Zero);




				GL.DrawArrays(mode, 0, batch.Size * 8);


				GL.DisableClientState(EnableCap.VertexArray);
				GL.DisableClientState(EnableCap.TextureCoordArray);
				GL.DisableClientState(EnableCap.ColorArray);
			//}
			//else
			//{
			//    GL.VertexPointer(2, VertexPointerType.Int, 0, batch.Vertex);
			//    GL.TexCoordPointer(2, TexCoordPointerType.Int, 0, batch.Texture);
			//    GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, batch.Color);

			//    GL.DrawArrays(mode, 0, batch.Size);
			//}

				RenderStats.BatchCall++;
		}


		#endregion


		#region Texture blits

	

		/// <summary>
		/// Raw draw a textured quad on the screen
		/// </summary>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="tex">Rectangle in the texture</param>
		static internal void RawBlit(Rectangle rect, Rectangle tex)
		{
			GL.Begin(BeginMode.Quads);
			
			GL.TexCoord2(tex.X, tex.Y);
			GL.Vertex2(rect.X, rect.Y);

			GL.TexCoord2(tex.X, tex.Y + tex.Height);
			GL.Vertex2(rect.X, rect.Y + rect.Height);

			GL.TexCoord2(tex.X + tex.Width, tex.Y + tex.Height);
			GL.Vertex2(rect.X + rect.Width, rect.Y + rect.Height);

			GL.TexCoord2(tex.X + tex.Width, tex.Y);
			GL.Vertex2(rect.X + rect.Width, rect.Y);

			GL.End();

			RenderStats.DirectCall += 4;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Shared textures
		/// </summary>
		static Dictionary<string, Texture> SharedTextures = new Dictionary<string, Texture>();

		/// <summary>
		/// Current texture
		/// </summary>
		static public Texture Texture
		{
			set
			{
				//if (texture == value)
				//	return;

				texture = value;
				if (value == null)
				{
					GL.BindTexture(TextureTarget.Texture2D, 0);
					return;
				}

				GL.BindTexture(TextureTarget.Texture2D, value.Handle);

				GL.MatrixMode(MatrixMode.Texture);
				GL.LoadIdentity();
				GL.Scale(1.0f / value.Size.Width, 1.0f / value.Size.Height, 1.0f);


				RenderStats.TextureBinding++;
			}
			get
			{
				return texture;
			}
		}
		static Texture texture;


		/// <summary>
		/// Sets a texture environment 
		/// </summary>
		static public TextureEnvMode TexEnv
		{
			get
			{
				float[] mode = new float[1];

				GL.GetTexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, mode);

				return (TextureEnvMode)mode[0];
			}

			set
			{
				GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)value);
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

				GL.GetInteger(GetPName.Viewport, tab);

				return new Rectangle(tab[0], tab[1], tab[2], tab[3]);
			}
			set
			{
				if (value.Size.IsEmpty)
					return;

				Rectangle rect = value;
				if (rect.Width == 0)
					rect.Width = 1;



				GL.Viewport(0, 0, rect.Width, rect.Height);
				//GL.MatrixMode(MatrixMode.Projection);
				//GL.LoadIdentity();
				//GL.Ortho(rect.Left, rect.Width, rect.Height, rect.Top, -1, 1);
				DefaultMatrix();
		//		GL.MatrixMode(MatrixMode.Modelview);
		//		GL.LoadIdentity();
			}
		}


		/// <summary>
		/// Gets/sets the cleacolor
		/// </summary>
		public static Color ClearColor
		{
			get
			{
				float[] tab = new float[4];
				GL.GetFloat(GetPName.ColorClearValue, tab);

				return Color.FromArgb((int)(tab[3] * 255), (int)(tab[0] * 255), (int)(tab[1] * 255), (int)(tab[2] * 255));
			}
			set
			{
				GL.ClearColor(value.R / 255.0f, value.G / 255.0f, value.B / 255.0f, value.A / 255.0f);
			}
		}



		/// <summary>
		/// Enables/disables face culling
		/// </summary>
		public static bool Culling
		{
			get
			{
				return GL.IsEnabled(EnableCap.CullFace);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.CullFace);
				else
					GL.Disable(EnableCap.CullFace);
			}
		}


		/// <summary>
		/// Enables/disables depth test
		/// </summary>
		public static bool DepthTest
		{
			get
			{
				return GL.IsEnabled(EnableCap.DepthTest);
			}

			set
			{
				if (value)
					GL.Enable(EnableCap.DepthTest);
				else
					GL.Disable(EnableCap.DepthTest);
			}
		}


		/// <summary>
		/// Gets/sets blending state
		/// </summary>
		public static bool Blending
		{
			get
			{
				return GL.IsEnabled(EnableCap.Blend);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.Blend);
				else
					GL.Disable(EnableCap.Blend);
			}
		}


		/// <summary>
		/// Enables/disables 2d texture
		/// </summary>
		public static bool Texturing
		{
			get
			{
				return GL.IsEnabled(EnableCap.Texture2D);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.Texture2D);
				else
					GL.Disable(EnableCap.Texture2D);

			}
		}


		/// <summary>
		/// Gets / sets the current color
		/// </summary>
		public static Color Color
		{
			get
			{
				float[] tab = new float[4];
				GL.GetFloat(GetPName.CurrentColor, tab);

				return Color.FromArgb((int)(tab[3] * 255), (int)(tab[0] * 255), (int)(tab[1] * 255), (int)(tab[2] * 255));
			}
			set
			{
				GL.Color4(value);
			}
		}


		/// <summary>
		/// Gets/Sets the scissor test
		/// </summary>
		public static bool Scissor
		{
			get
			{
				return GL.IsEnabled(EnableCap.ScissorTest);

			}
			set
			{
				if (value)
					GL.Enable(EnableCap.ScissorTest);
				else
					GL.Disable(EnableCap.ScissorTest);
			}
		}


		/// <summary>
		/// Gets/sets the scissor zone
		/// </summary>
		public static Rectangle ScissorZone
		{
			get
			{
				int[] rect = new int[4];
				GL.GetInteger(GetPName.ScissorBox, rect);

				return new Rectangle(rect[0], rect[1], rect[2], rect[3]);
			}
			set
			{
				//GL.Scissor(scissorZone.X,scissorZone.Bottom, scissorZone.Width,  scissorZone.Top - scissorZone.Bottom);
				GL.Scissor(value.X, ViewPort.Height - value.Top - value.Height, value.Width, value.Height);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public static RenderDeviceCapabilities Capabilities
		{
			get;
			private set;
		}


		/// <summary>
		/// Rendering stats
		/// </summary>
		public static RenderStats RenderStats
		{
			get;
			private set;
		}


		#endregion

	}


	/// <summary>
	/// Supported device capabilities
	/// </summary>
	public class RenderDeviceCapabilities
	{

		/// <summary>
		/// Is texture size limited to power of two size ?
		/// </summary>
		public bool NonPowerOfTwoTexture
		{
			get;
			internal set;
		}


		/// <summary>
		/// This function returns the name of the graphic card vendor. 
		/// </summary>
		public string VideoVendor
		{
			get;
			internal set;
		}


		/// <summary>
		/// This function returns the name of the graphic card. 
		/// </summary>
		public string VideoRenderer
		{
			get;
			internal set;
		}



		/// <summary>
		/// This function returns OpenGL version
		/// </summary>
		public string VideoVersion
		{
			get;
			internal set;
		}



		/// <summary>
		/// This function returns the OpenGL Shading Language version that is supported by the engine
		/// </summary>
		public string ShadingLanguageVersion
		{
			get;
			internal set;
		}



		/// <summary>
		/// This function returns a space-separated list of OpenGL supported extensions
		/// </summary>
		public string VideoExtensions
		{
			get;
			internal set;
		}



	}


	/// <summary>
	/// 
	/// </summary>
	public class RenderStats
	{


		/// <summary>
		/// Reset counters
		/// </summary>
		internal void Reset()
		{
			DirectCall = 0;
			BatchCall = 0;
			BatchUpload = 0;
			TextureBinding = 0;
		}


		#region Properties


		/// <summary>
		/// Number of direct call (bad !)
		/// </summary>
		public int DirectCall
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
		/// Number of modified batch
		/// </summary>
		public int BatchUpload
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

}
