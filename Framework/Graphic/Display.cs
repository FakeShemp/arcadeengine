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
using System.Text.RegularExpressions;


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
			Capabilities = new RenderDeviceCapabilities();
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
			GL.ClearStencil(0);

			GL.Normal3(0.0f, 0.0f, 1.0f);


			/*
						// Get OpenGL version for 2.x
						Regex regex = new Regex(@"(\d+)\.(\d+)\.*(\d*)");
						Match match = regex.Match(GL.GetString(StringName.Version));
						if (match.Success)
						{
							MajorVersion = Convert.ToInt32(match.Groups[1].Value);
							MinorVersion = Convert.ToInt32(match.Groups[2].Value);
						}
			*/
		}


		/// <summary>
		/// Display Graphic device informations
		/// </summary>
		public static void TraceInfos()
		{
			Trace.WriteLine("Video informations :");
			Trace.Indent();
			Trace.WriteLine("Graphics card vendor : {0}", Capabilities.VideoVendor);
			Trace.WriteLine("Renderer : {0}", Capabilities.VideoRenderer);
			Trace.WriteLine("Version : {0}", Capabilities.VideoVersion);
			Trace.WriteLine("Shading Language Version : {0}", Capabilities.ShadingLanguageVersion);

			Trace.WriteLine("Display modes");
			Trace.Indent();
			foreach (DisplayDevice device in DisplayDevice.AvailableDisplays)
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
				GL.GetInteger(GetPName.NumExtensions, out count);
				Trace.Write("Supported extension ({0}) : ", count);
				for (int i = 0; i < count; i++)
					Trace.Write("{0}, ", GL.GetString(StringName.Extensions, i));
			}
			Trace.WriteLine("");

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
		/// Clears all buffers
		/// </summary>
		public static void ClearBuffers()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
		}


		/// <summary>
		/// Clears the stencil buffer
		/// </summary>
		public static void ClearStencilBuffer()
		{
			GL.Clear(ClearBufferMask.StencilBufferBit);
		}


		/// <summary>
		/// Clears the color buffer
		/// </summary>
		public static void ClearColorBuffer()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}


		/// <summary>
		/// Clears the depth buffer
		/// </summary>
		public static void ClearDepthBuffer()
		{
			GL.Clear(ClearBufferMask.DepthBufferBit);
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
		/// <param name="source">Source factor</param>
		/// <param name="dest">Destination factor</param>
		public static void BlendingFunction(BlendingFactorSrc source, BlendingFactorDest dest)
		{
			GL.BlendFunc(source, dest);
		}



		/// <summary>
		/// Stencil test function
		/// </summary>
		/// <param name="function">Test function</param>
		/// <param name="reference">Reference value</param>
		/// <param name="mask">Mask</param>
		public static void StencilFunction(StencilFunction function, int reference, int mask)
		{
			GL.StencilFunc(function, reference, mask);
		}



		/// <summary>
		/// Stencil test action
		/// </summary>
		/// <param name="fail">Specifies the action to take when the stencil test fails</param>
		/// <param name="zfail">Specifies the action when the stencil test passes, but the depth test fails</param>
		/// <param name="zpass">Specifies the action when both the stencil test and the depth test pass, or when the 
		/// stencil test passes and either there is no depth buffer or depth testing is not enabled</param>
		public static void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass)
		{
			GL.StencilOp(fail, zfail, zpass);
		}


		/// <summary>
		/// Alpha test function
		/// </summary>
		/// <param name="function">Comparison function</param>
		/// <param name="reference">Reference value</param>
		public static void AlphaFunction(AlphaFunction function, float reference)
		{
			GL.AlphaFunc(function, reference);
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
			GL.ColorMask(red, green, blue, alpha);
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
			//GL.LoadIdentity();
			//GL.Ortho(ViewPort.Left, ViewPort.Width, ViewPort.Height, ViewPort.Top, -1, 1);

			Matrix4 projection = Matrix4.CreateOrthographicOffCenter(ViewPort.Left, ViewPort.Width, ViewPort.Height, ViewPort.Top, -1, 1);
			GL.LoadMatrix(ref projection);

			// Exact pixelization is required, put a small translation in the ModelView matrix
			GL.Translate(0.001f, 0.001f, 0.0f);
		}



		/// <summary>
		/// Push a copy of the current drawing state onto the drawing state stack 
		/// </summary>
		public static void SaveState()
		{
			//GL.PushAttrib(AttribMask.AllAttribBits);

			GL.MatrixMode(MatrixMode.Projection);
			GL.PushMatrix();
		}


		/// <summary>
		/// Pop the top entry in the drawing state stack, and reset the drawing state it describes.
		/// </summary>
		public static void RestoreState()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.PopMatrix();

			//GL.PopAttrib();
		}


		#endregion


		#region Drawing


		/// <summary>
		/// Specify the line stipple pattern
		/// </summary>
		/// <param name="factor">Specifies a multiplier for each bit in the line stipple pattern</param>
		/// <param name="pattern">16-bit integer whose bit pattern determines which fragments of a line will be drawn when the line is rasterized.</param>
		public static void SetLineStipple(int factor, ushort pattern)
		{
			GL.LineStipple(factor, pattern);
		}


		/// <summary>
		/// Draws a colored rectangle
		/// </summary>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color</param>
		public static void DrawRectangle(Rectangle rect, Color color)
		{
			DrawQuad(rect.Left, rect.Top, rect.Size.Width, rect.Size.Height, color, false, 0, Point.Empty);
		}


		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="x">X location</param>
		/// <param name="y">Y location</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="color">Color</param>
		static public void DrawRectangle(int x, int y, int width, int height, Color color)
		{
			DrawQuad(x, y, width, height, color, false, 0, Point.Empty);
		}


		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="x">X location</param>
		/// <param name="y">Y location</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="color">Color</param>
		/// <param name="angle">Rotation angle</param>
		/// <param name="pivot">Origin of rotation</param>
		static public void DrawRectangle(int x, int y, int width, int height, Color color, float angle, Point pivot)
		{
			DrawQuad(x, y, width, height, color, false, angle, pivot);
		}


		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color</param>
		/// <param name="angle">Angle of rotation</param>
		/// <param name="origin">The origin of the rectangle. Specify (0,0) for the upper-left corner.</param>
		public static void DrawRectangle(Rectangle rect, Color color, float angle, Point origin)
		{
			DrawQuad(rect.Left, rect.Top, rect.Size.Width, rect.Size.Height, color, false, angle, origin);
		}


		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="x">X location</param>
		/// <param name="y">Y location</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="color">Color</param>
		/// <param name="fill">Fill the rectangle or not</param>
		/// <param name="angle">Rotation angle</param>
		/// <param name="pivot">Origin of rotation</param>
		static void DrawQuad(int x, int y, int width, int height, Color color, bool fill, float angle, Point pivot)
		{
			Texturing = false;

			// Backup color
			Color col = Color;
			Color = color;

			SaveState();

			//GL.MatrixMode(MatrixMode.Projection);
			//GL.PushMatrix();

			// Translation & rotation
			Translate(x + pivot.X, y + pivot.Y);
			x = -pivot.X;
			y = -pivot.Y;
			Rotate(angle);



			if (fill)
				GL.Begin(BeginMode.Quads);
			else
				GL.Begin(BeginMode.LineLoop);
			GL.Vertex2(x, y);
			GL.Vertex2(x, y + height);
			GL.Vertex2(x + width, y + height);
			GL.Vertex2(x + width, y);
			GL.End();

			Texturing = true;
			//GL.PopMatrix();
			RestoreState();
			Color = col;

			RenderStats.DirectCall += 4;
		}


		/// <summary>
		/// Draw a filled rectangle
		/// </summary>
		/// <param name="rect">Rectangle</param>
		/// <param name="color">Color</param>
		public static void FillRectangle(Rectangle rect, Color color)
		{
			DrawQuad(rect.X, rect.Y, rect.Width, rect.Height, color, true, 0, Point.Empty);
		}


		/// <summary>
		/// Draw a filled rectangle
		/// </summary>
		/// <param name="x">X location</param>
		/// <param name="y">Y location</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="color">Color</param>
		public static void FillRectangle(int x, int y, int width, int height, Color color)
		{
			DrawQuad(x, y, height, width, color, true, 0, Point.Empty);
		}


		/// <summary>
		/// Draw a filled rectangle
		/// </summary>
		/// <param name="rect">Rectangle</param>
		/// <param name="color">Color</param>
		/// <param name="angle">Rotation angle</param>
		/// <param name="pivot">Origin of rotation</param>
		public static void FillRectangle(Rectangle rect, Color color, float angle, Point pivot)
		{
			DrawQuad(rect.X, rect.Y, rect.Width, rect.Height, color, true, angle, pivot);
		}


		/// <summary>
		/// Draw a filled rectangle
		/// </summary>
		/// <param name="x">X location</param>
		/// <param name="y">Y location</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="color">Color</param>
		/// <param name="angle">Rotation angle</param>
		/// <param name="pivot">Origin of rotation</param>
		public static void FillRectangle(int x, int y, int width, int height, Color color, float angle, Point pivot)
		{
			DrawQuad(x, y, height, width, color, true, angle, pivot);
		}


		/// <summary>
		/// Draws a line from point "from" to point "to"
		/// </summary>
		/// <param name="from">Starting point</param>
		/// <param name="to">Ending point</param>
		/// <param name="color">Color</param>
		public static void DrawLine(Point from, Point to, Color color)
		{
			DrawLine(from.X, from.Y, to.X, to.Y, color);
		}


		/// <summary>
		/// Draws a line between the two points specified. 
		/// </summary>
		/// <param name="x1">Start x</param>
		/// <param name="y1">Start y</param>
		/// <param name="x2">End x</param>
		/// <param name="y2">End y</param>
		/// <param name="color">Color</param>
		public static void DrawLine(int x1, int y1, int x2, int y2, Color color)
		{
			Color = color;

			Texturing = false;
			GL.Begin(BeginMode.Lines);
			GL.Vertex2(x1, y1);
			GL.Vertex2(x2, y2);
			GL.End();
			Texturing = true;

			RenderStats.DirectCall += 2;
		}


		/// <summary>
		/// Draws a bunch of connected lines. The last point and the first point are not connected. 
		/// </summary>
		/// <param name="points"></param>
		/// <param name="color"></param>
		public static void DrawLines(Point[] points, Color color)
		{
			int pos = 0;
			for (pos = 0; pos < points.Length - 1; pos++)
			{
				DrawLine(points[pos], points[pos + 1], color);
			}

			RenderStats.DirectCall += pos;
		}



		/// <summary>
		/// Draws a bunch of line segments. Each pair of points represents a line segment which is drawn.
		/// No connections between the line segments are made, so there must be an even number of points. 
		/// </summary>
		/// <param name="points"></param>
		/// <param name="color"></param>
		public static void DrawLineSegments(Point[] points, Color color)
		{
			int pos = 0;
			for (pos = 0; pos < points.Length - 1; pos += 2)
			{
				DrawLine(points[pos], points[pos + 1], color);
			}

			RenderStats.DirectCall += pos;
		}


		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="point">Location of the point</param>
		/// <param name="color">Color</param>
		public static void DrawPoint(Point point, Color color)
		{
			DrawPoint(point.X, point.Y, color);
		}



		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="color">Color</param>
		public static void DrawPoint(int x, int y, Color color)
		{
			Texturing = false;
			Color = color;
			GL.Begin(BeginMode.Points);
			GL.Vertex2(x, y);
			GL.End();
			Texturing = true;

			RenderStats.DirectCall++;
		}


		/// <summary>
		/// Draw a polygon
		/// </summary>
		/// <param name="points">List of points</param>
		/// <param name="color">Line color</param>
		public static void DrawPolygon(Point[] points, Color color)
		{
			Texturing = false;
			Culling = false;
			Color = color;

			GL.Begin(BeginMode.LineLoop);
			for (int i = 0; i < points.Length; i++)
			{
				GL.Vertex2(points[i].X, points[i].Y);
			}
			GL.End();

			Texturing = true;
			RenderStats.DirectCall += points.Length;
		}


		/// <summary>
		/// Draw a filled polygon
		/// </summary>
		/// <param name="points">List of points</param>
		/// <param name="color">Fill color</param>
		public static void FillPolygon(Point[] points, Color color)
		{
			Texturing = false;
			Culling = false;
			Color = color;

			GL.Begin(BeginMode.TriangleFan);
			for (int i = 0; i < points.Length; i++)
			{
				GL.Vertex2(points[i].X, points[i].Y);
			}
			GL.End();

			Texturing = true;
			RenderStats.DirectCall += points.Length;
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


		/// <summary>
		/// Draw an ellipse
		/// </summary>
		/// <param name="rect">Bounding rectangle</param>
		/// <param name="color">Color</param>
		public static void DrawEllipse(Rectangle rect, Color color)
		{
			float DEG2RAD = 3.141590f / 180.0f;

			Point center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
			Color = color;
			Texturing = false;

			GL.Begin(BeginMode.LineLoop);
			for (int i = 0; i < 360; i++)
			{
				float degInRad = i * DEG2RAD;
				GL.Vertex2(center.X + Math.Cos(degInRad) * rect.Width / 2, center.Y + Math.Sin(degInRad) * rect.Height / 2);
			}
			GL.End();

			Texturing = true;
			RenderStats.DirectCall += 360;
		}


		/// <summary>
		/// Draw an ellipse
		/// </summary>
		/// <param name="rect">Bounding rectangle</param>
		/// <param name="color">Color</param>
		public static void FillEllipse(Rectangle rect, Color color)
		{
			float DEG2RAD = 3.141590f / 180.0f;

			Point center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
			Color = color;
			Texturing = false;

			GL.Begin(BeginMode.Polygon);
			for (int i = 0; i < 360; i++)
			{
				float degInRad = i * DEG2RAD;
				GL.Vertex2(center.X + Math.Cos(degInRad) * rect.Width / 2, center.Y + Math.Sin(degInRad) * rect.Height / 2);
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
			// No batch, or empty batch
			if (batch == null || batch.Size == 0)
				return;

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


			//GL.DrawElements(mode, batch.Size, DrawElementsType.UnsignedInt, IntPtr.Zero);
			GL.DrawArrays(mode, 0, batch.Size);


			GL.DisableClientState(EnableCap.VertexArray);
			GL.DisableClientState(EnableCap.TextureCoordArray);
			GL.DisableClientState(EnableCap.ColorArray);

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
				if (value == null)
				{
					GL.BindTexture(TextureTarget.Texture2D, 0);
					texture = null;
					return;
				}

				texture = value;
				GL.BindTexture(TextureTarget.Texture2D, value.Handle);

				RenderStats.TextureBinding++;


				GL.MatrixMode(MatrixMode.Texture);
				GL.LoadIdentity();
				GL.Scale(1.0f / value.Size.Width, 1.0f / value.Size.Height, 1.0f);
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
		/// Enables/disables stencil test
		/// </summary>
		public static bool StencilTest
		{
			get
			{
				return GL.IsEnabled(EnableCap.StencilTest);
			}

			set
			{
				if (value)
					GL.Enable(EnableCap.StencilTest);
				else
					GL.Disable(EnableCap.StencilTest);
			}
		}


		/// <summary>
		/// Gets/sets clear value for the stencil buffer 
		/// </summary>
		public static int StencilClearValue
		{
			get
			{
				int s;
				GL.GetInteger(GetPName.StencilClearValue, out s);
				return s;
			}
			set
			{
				GL.ClearStencil(value);
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
		/// Gets/sets clear value for the depth buffer 
		/// </summary>
		public static float DepthClearValue
		{
			get
			{
				int s;
				GL.GetInteger(GetPName.DepthClearValue, out s);
				return s;
			}
			set
			{
				GL.ClearDepth(value);
			}
		}


		/// <summary>
		/// Enable or disable writing into the depth buffer
		/// </summary>
		public static bool DepthMask
		{
			get
			{
				bool ret;
				GL.GetBoolean(GetPName.DepthWritemask, out ret);

				return ret;
			}

			set
			{
				GL.DepthMask(value);
			}
		}

		/// <summary>
		/// Gets/sets blending state
		/// </summary>
		public static bool AlphaTest
		{
			get
			{
				return GL.IsEnabled(EnableCap.AlphaTest);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.AlphaTest);
				else
					GL.Disable(EnableCap.AlphaTest);
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

				return color;
				//float[] tab = new float[4];
				//GL.GetFloat(GetPName.CurrentColor, tab);

				//return Color.FromArgb((int)(tab[3] * 255), (int)(tab[0] * 255), (int)(tab[1] * 255), (int)(tab[2] * 255));
			}
			set
			{
				GL.Color4(value);
				color = value;
			}
		}
		static Color color;



		/// <summary>
		/// Gets / sets the point size
		/// </summary>
		public static int PointSize
		{
			get
			{
				int value;
				GL.GetInteger(GetPName.PointSize, out value);
				return value;
			}
			set
			{
				GL.PointSize(value);
			}
		}


		/// <summary>
		/// Gets / sets the line size
		/// </summary>
		public static int LineWidth
		{
			get
			{
				int value;
				GL.GetInteger(GetPName.LineWidth, out value);
				return value;
			}
			set
			{
				GL.LineWidth(value);
			}
		}



		/// <summary>
		/// Line anti aliasing
		/// </summary>
		public static bool LineSmooth
		{
			get
			{
				return GL.IsEnabled(EnableCap.LineSmooth);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.LineSmooth);
				else
					GL.Disable(EnableCap.LineSmooth);
			}
		}


		/// <summary>
		/// Point smooth
		/// </summary>
		public static bool PointSmooth
		{
			get
			{
				return GL.IsEnabled(EnableCap.PointSmooth);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.PointSmooth);
				else
					GL.Disable(EnableCap.PointSmooth);
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
		/// Gets/Sets the stipple pattern
		/// </summary>
		public static bool LineStipple
		{
			get
			{
				return GL.IsEnabled(EnableCap.LineStipple);

			}
			set
			{
				if (value)
					GL.Enable(EnableCap.LineStipple);
				else
					GL.Disable(EnableCap.LineStipple);
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
		/// 
		/// </summary>
		public RenderDeviceCapabilities()
		{
			Extensions = new List<string>(GL.GetString(StringName.Extensions).Split(new char[] { ' ' }));


			if (Extensions.Contains("GL_ARB_texture_non_power_of_two"))
				HasNPOTTexture = true;

			if (Extensions.Contains("GL_ARB_framebuffer_object"))
				HasFBO = true;

			if (Extensions.Contains("GL_ARB_pixel_buffer_object"))
				HasPBO = true;
		}



		/// <summary>
		/// Has non power of two texture support
		/// </summary>
		public bool HasNPOTTexture
		{
			get;
			internal set;
		}


		/// <summary>
		/// Has Frame Buffer Objects support
		/// </summary>
		public bool HasFBO
		{
			get;
			internal set;
		}

		/// <summary>
		/// Has Pixel Buffer Objects support
		/// </summary>
		public bool HasPBO
		{
			get;
			internal set;
		}


		/// <summary>
		/// Returns the name of the graphic card vendor. 
		/// </summary>
		public string VideoVendor
		{
			get
			{
				return GL.GetString(StringName.Vendor);
			}
		}


		/// <summary>
		/// Returns the name of the graphic card. 
		/// </summary>
		public string VideoRenderer
		{
			get
			{
				return GL.GetString(StringName.Renderer);
			}
		}


		/// <summary>
		/// Returns OpenGL version
		/// </summary>
		public string VideoVersion
		{
			get
			{
				return GL.GetString(StringName.Version);
			}
		}


		/// <summary>
		/// This function returns the OpenGL Shading Language version that is supported by the engine
		/// </summary>
		public string ShadingLanguageVersion
		{
			get
			{
				return GL.GetString(StringName.ShadingLanguageVersion);
			}
		}


		/// <summary>
		/// Major version of the Context
		/// </summary>
		public int MajorVersion
		{
			get
			{
				int version;
				GL.GetInteger(GetPName.MajorVersion, out version);

				return version;
			}
		}


		/// <summary>
		/// Minor version of the Context
		/// </summary>
		public int MinorVersion
		{
			get
			{
				int version;
				GL.GetInteger(GetPName.MinorVersion, out version);

				return version;
			}
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
