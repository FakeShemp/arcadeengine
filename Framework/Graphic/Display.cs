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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ArcEngine.Asset;
using System.Xml;
using System.Text;

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

			RenderStats = new RenderStats();
			TextureParameters = new DefaultTextParameters();

			CircleResolution = 50;

		}


		/// <summary>
		/// Resets default state
		/// </summary>
		public static void Init()
		{
			Trace.WriteDebugLine("[Display] Init()");
			Capabilities = new RenderDeviceCapabilities();


			#region Shader
			Shader = Shader.CreateTextureShader();
			#endregion


			Texturing = true;
			Blending = true;
			ClearColor = Color.Black;
			Culling = false;
			DepthTest = false;
			Color = Color.White;

			GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
			GL.Enable(EnableCap.PolygonSmooth);

			LineSmooth = true;
			PointSmooth = true;
			BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.ClearStencil(0);

			// Matrices
			modelViewMatrix = Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0));;
			projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, ViewPort.Width, ViewPort.Height, 0, 0, 5); ;
			textureMatrix = Matrix4.Identity;
			UpdateMatrix();

			Buffer = BatchBuffer.CreatePositionColorTextureBuffer();
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		internal static void Dispose()
		{
			Trace.WriteDebugLine("[Display] : Dispose()");

			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;
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
				Trace.Write("Supported extensions ({0}) : ", count);
				for (int i = 0; i < count; i++)
					Trace.Write("{0}, ", GL.GetString(StringName.Extensions, i));
			}
			Trace.WriteLine("");

			Trace.Unindent();
		}


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
		/// <param name="x">Horizontal translation</param>
		/// <param name="y">Vertical translation</param>
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
		/// <param name="m11">m11</param>
		/// <param name="m12">m12</param>
		/// <param name="m21">m21</param>
		/// <param name="m22">m22</param>
		/// <param name="dx">dx</param>
		/// <param name="dy">dy</param>
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
		/// <param name="m11">m11</param>
		/// <param name="m12">m12</param>
		/// <param name="m21">m21</param>
		/// <param name="m22">m22</param>
		/// <param name="dx">dx</param>
		/// <param name="dy">dy</param>
		public static void SetTransform(float m11, float m12, float m21, float m22, float dx, float dy)
		{
			DefaultMatrix();
			Transform(m11, m12, m21, m22, dx, dy);
		}


		/// <summary>
		/// Changes the transformation matrix to apply a scaling transformation with the given characteristics.
		/// </summary>
		/// <param name="x">X factor</param>
		/// <param name="y">Y factor</param>
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
			GL.MatrixMode(MatrixMode.Modelview);
		//	GL.Translate(0.0001f, 0.0001f, 0.0f);

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



		/// <summary>
		/// Update matrix
		/// </summary>
		static void UpdateMatrix()
		{
			if (Shader == null)
				return;

			Shader.SetUniform("mvp_matrix", modelViewMatrix * projectionMatrix);
			Shader.SetUniform("tex_matrix", TextureMatrix);
		}

		#endregion


		#region Drawing


		/// <summary>
		/// Specify the line stipple pattern
		/// </summary>
		/// <param name="factor">Specifies a multiplier for each bit in the line stipple pattern</param>
		/// <param name="pattern">16-bit integer whose bit pattern determines which fragments of a line will be drawn when the line is rasterized.</param>
		public static void SetLineStipple(int factor, short pattern)
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

			//GL.PopMatrix();
			RestoreState();
			Texturing = true;
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
			DrawQuad(x, y, width, height, color, true, 0, Point.Empty);
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
		/// <param name="points">Points</param>
		/// <param name="color">Color</param>
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
		/// <param name="points">Points</param>
		/// <param name="color">Color</param>
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
		/// Draws a circle
		/// </summary>
		/// <param name="x">X location</param>
		/// <param name="y">Y location</param>
		/// <param name="xradius">X radius</param>
		/// <param name="yradius">Y radius</param>
		/// <param name="color">Color</param>
		/// <param name="fill">Fill or not</param>
		static void DrawCircle(int x, int y, int xradius, int yradius, Color color, bool fill)
		{

			Texturing = false;
			Color = color;

			if (fill)
				GL.Begin(BeginMode.Polygon);
			else
				GL.Begin(BeginMode.LineLoop);
			float angle;
			for (int i = 0; i < CircleResolution; i++)
			{
				angle = i * 2.0f * (float)Math.PI / CircleResolution;
				GL.Vertex2(x + Math.Cos(angle) * xradius, y + Math.Sin(angle) * yradius);
			}
			GL.End();


			Texturing = true;
			RenderStats.DirectCall += CircleResolution;
		}



		/// <summary>
		/// Draws an arc
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radius">Radius</param>
		/// <param name="start">Start angle</param>
		/// <param name="angle">Angle amount</param>
		/// <param name="color">Color</param>
		public static void DrawArc(float x, float y, float radius, float start, float angle, Color color)
		{
			DrawArc(x, y, radius, start, angle, color, false);
		}


		/// <summary>
		/// Draws a filled arc
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radius">Radius</param>
		/// <param name="start">Start angle</param>
		/// <param name="angle">Angle amount</param>
		/// <param name="color">Color</param>
		public static void FillArc(float x, float y, float radius, float start, float angle, Color color)
		{
			DrawArc(x, y, radius, start, angle, color, true);
		}



		/// <summary>
		/// Draws an arc
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radius">Radius</param>
		/// <param name="start">Start angle</param>
		/// <param name="end">End angle</param>
		/// <param name="color">Color</param>
		/// <param name="fill">Filled or not</param>
		static void DrawArc(float x, float y, float radius, float start, float end, Color color, bool fill)
		{

			Color = color;
			Texturing = false;

			int real_segments = (int)(Math.Abs(end) / (2 * Math.PI) * (float)CircleResolution) + 1;

			float theta = end / (float)(real_segments);
			float tangetial_factor = (float)Math.Tan(theta);
			float radial_factor = (float)(1 - Math.Cos(theta));

			float xx = (float)(x + radius * Math.Cos(start));
			float yy = (float)(y + radius * Math.Sin(start));

			if (fill)
				GL.Begin(BeginMode.Polygon);
			else
				GL.Begin(BeginMode.LineStrip);
			for (int ii = 0; ii < real_segments + 1; ii++)
			{
				GL.Vertex2(xx, yy);

				float tx = -(yy - y);
				float ty = xx - x;

				xx += tx * tangetial_factor;
				yy += ty * tangetial_factor;

				float rx = x - xx;
				float ry = y - yy;

				xx += rx * radial_factor;
				yy += ry * radial_factor;
			}
			GL.End();

			Texturing = true;
			RenderStats.DirectCall += real_segments;
		}




		/// <summary>
		/// Draws a circle
		/// </summary>
		/// <param name="location">Location of the circle</param>
		/// <param name="radius">Radius</param>
		/// <param name="color">Color</param>
		public static void DrawCircle(Point location, int radius, Color color)
		{
			DrawCircle(location.X, location.Y, radius, radius, color, false);
		}


		/// <summary>
		/// Draws a circle
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radius">Radius</param>
		/// <param name="color">Color</param>
		public static void DrawCircle(int x, int y, int radius, Color color)
		{
			DrawCircle(x, y, radius, radius, color, false);
		}


		/// <summary>
		/// Draws a disk
		/// </summary>
		/// <param name="location">Location of the circle</param>
		/// <param name="radius">Radius</param>
		/// <param name="color">Color</param>
		public static void DrawDisk(Point location, int radius, Color color)
		{
			DrawCircle(location.X, location.Y, radius, radius, color, true);
		}


		/// <summary>
		/// Draws a disk
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radius">Radius</param>
		/// <param name="color">Color</param>
		public static void DrawDisk(int x, int y, int radius, Color color)
		{
			DrawCircle(x, y, radius, radius, color, true);
		}


		/// <summary>
		/// Draw an ellipse
		/// </summary>
		/// <param name="rect">Bounding rectangle</param>
		/// <param name="color">Color</param>
		public static void DrawEllipse(Rectangle rect, Color color)
		{
			DrawCircle(rect.X + rect.Width / 2, rect.Y + rect.Height / 2, rect.Width / 2, rect.Height / 2, color, false);
		}


		/// <summary>
		/// Draws an ellipse
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radiusx">X radius</param>
		/// <param name="radiusy">Y radius</param>
		/// <param name="color">Color</param>
		public static void DrawEllipse(int x, int y, int radiusx, int radiusy, Color color)
		{
			DrawCircle(x, y, radiusx, radiusy, color, false);
		}


		/// <summary>
		/// Draw an ellipse
		/// </summary>
		/// <param name="rect">Bounding rectangle</param>
		/// <param name="color">Color</param>
		public static void FillEllipse(Rectangle rect, Color color)
		{
			DrawCircle(rect.X + rect.Width / 2, rect.Y + rect.Height /2, rect.Width / 2, rect.Height / 2, color, true);
		}



		/// <summary>
		/// Draws a filled ellipse
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="radiusx">X radius</param>
		/// <param name="radiusy">Y radius</param>
		/// <param name="color">Color</param>
		public static void FillEllipse(int x, int y, int radiusx, int radiusy, Color color)
		{
			DrawCircle(x, y, radiusx, radiusy, color, true);
		}


		/// <summary>
		/// Draw a Bezier curve
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		/// <param name="control1">Control point 1</param>
		/// <param name="control2">Control point 2</param>
		/// <param name="color">Color</param>
		public static void DrawBezier(Point start, Point end, Point control1, Point control2, Color color)
		{
			float[] points = new float[]
			{
				start.X, start.Y, 0,
				control1.X, control1.Y, 0,
				control2.X, control2.Y, 0,
				end.X, end.Y, 0,
			};

			//float[] colors = new float[]
			//{
			//   startcolor.R, startcolor.G, startcolor.B, startcolor.A,
			//   endcolor.R, endcolor.G, endcolor.B, endcolor.A,
			//};


			Color = color;
			GL.Enable(EnableCap.Map1Vertex3);
			//GL.Enable(EnableCap.Map1Color4);

			GL.Map1(MapTarget.Map1Vertex3, 0, CircleResolution, 3, 4, points);
			//GL.Map1(MapTarget.Map1Color4, 0, CircleResolution, 4, 2, colors);
			GL.Begin(BeginMode.LineStrip);
			for (int i = 0; i <= CircleResolution; i++)
				GL.EvalCoord1(i);
			GL.End();

			GL.Disable(EnableCap.Map1Vertex3);
			GL.Disable(EnableCap.Map1Color4);

			//PointSize = 2;
			//DrawPoint(start, Color.Red);
			//DrawPoint(end, Color.Red);
			//DrawPoint(control1, Color.Green);
			//DrawPoint(control2, Color.Green);
			//PointSize = 1;
		}


		/// <summary>
		/// Draws a quadratic curve
		/// </summary>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		/// <param name="control">Control point</param>
		/// <param name="color">Color</param>
		public static void DrawQuadraticCurve(Point start, Point end, Point control, Color color)
		{
			Point control1 = new Point(
				(int)(start.X  + 2.0f / 3.0f * (control.X - start.X)),
				(int)(start.Y  + 2.0f / 3.0f * (control.Y - start.Y)));

			Point control2 = new Point(
				(int)(control1.X + (end.X - start.X) / 3.0f),
				(int)(control1.Y + (end.Y - start.Y) / 3.0f));

			DrawBezier(start, end, control1, control2, color);
		}


		#endregion


		#region Shared textures


		/// <summary>
		/// Creates a shared texture
		/// </summary>
		/// <param name="name">Name of the texture</param>
		/// <returns>Texture handle</returns>
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
		/// <param name="name">Name of the texture</param>
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

			GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, stride, offset);
		}



		/// <summary>
		/// Binds and draws an IndexBuffer
		/// </summary>
		/// <param name="buffer">Buffer handle</param>
		/// <param name="mode">Drawing mode</param>
		/// <param name="index">Index buffer</param>
		/// <returns></returns>
		public static void DrawIndexBuffer(BatchBuffer buffer, BeginMode mode, IndexBuffer index)
		{
			if (buffer == null || index == null)
				return;

			// Set the index buffer
			index.Bind();

			// Bind shader
			buffer.Bind(Shader);

			// Draw
			GL.DrawElements(mode, index.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

			RenderStats.BatchCall++;
		}



		/// <summary>
		/// Draws a batch
		/// </summary>
		/// <param name="batch">Batch to draw</param>
		/// <param name="first">Specifies the starting index in the enabled arrays.</param>
		/// <param name="count">Specifies the number of indices to be rendered.</param>
		/// <returns></returns>
		public static void DrawBatch(BatchBuffer batch, int first, int count)
		{
			DrawBatch(batch, BeginMode.Triangles, first, count);
		}



		/// <summary>
		/// Draws a user batch
		/// </summary>
		/// <param name="batch">Batch to draw</param>
		/// <param name="mode">Drawing mode</param>
		/// <param name="first">Specifies the starting index in the enabled arrays.</param>
		/// <param name="count">Specifies the number of indices to be rendered.</param>
		public static void DrawBatch(BatchBuffer batch, BeginMode mode, int first, int count)
		{
			// No batch, or empty batch
			if (batch == null)
				return;

			// Bind buffer
			batch.Bind(Shader);

			GL.DrawArrays(mode, first, count);

			RenderStats.BatchCall++;

			return;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="shape"></param>
		/// <param name="shader"></param>
		public static void DrawShape(Shape shape, Shader shader)
		{

		}


		/// <summary>
		/// Enables a buffer index 
		/// </summary>
		/// <param name="id"></param>
		public static void EnableBufferIndex(int id)
		{
			if (id < 0)
				return;

			GL.EnableVertexAttribArray(id);
		}


		/// <summary>
		/// Disables a buffer index
		/// </summary>
		/// <param name="id"></param>
		public static void DisableBufferIndex(int id)
		{
			GL.DisableVertexAttribArray(id);
		}

		#endregion


		#region Texture blits

		/// <summary>
		/// Draws a texture on the screen
		/// </summary>
		/// <param name="texture">Texture to display</param>
		/// <param name="location">Location on the screen</param>
		static public void DrawTexture(Texture texture, Point location)
		{
			DrawTexture(texture, location, Color.White);
		}


		/// <summary>
		/// Draws a texture on the screen
		/// </summary>
		/// <param name="texture">Texture to display</param>
		/// <param name="location">Location on the screen</param>
		/// <param name="color">Color to apply</param>
		static public void DrawTexture(Texture texture, Point location, Color color)
		{
			if (texture == null)
				return;

			Texture = texture;

			Buffer.AddRectangle(new Rectangle(location, texture.Size), color, texture.Rectangle);
			int count = Buffer.Update();
			DrawBatch(Buffer, 0, count);
		}


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
		/// Internal batch buffer
		/// </summary>
		static BatchBuffer Buffer;


		/// <summary>
		/// Projection matrix
		/// </summary>
		public static Matrix4 ProjectionMatrix
		{
			get
			{
				return projectionMatrix;
			}
			set
			{
				projectionMatrix = value;
				UpdateMatrix();
			}
		}
		static Matrix4 projectionMatrix;


		/// <summary>
		/// ModelView matrix
		/// </summary>
		public static Matrix4 ModelViewMatrix
		{
			get
			{
				return modelViewMatrix;
			}
			set
			{
				modelViewMatrix = value;
				UpdateMatrix();
			}
		}
		static Matrix4 modelViewMatrix;


		/// <summary>
		/// Texture matrix
		/// </summary>
		public static Matrix4 TextureMatrix
		{
			get
			{
				return textureMatrix;
			}
			set
			{
				textureMatrix = value;
				UpdateMatrix();
			}
		}
		static Matrix4 textureMatrix;



		/// <summary>
		/// Circle resolution
		/// </summary>
		static public int CircleResolution
		{
			get;
			set;
		}

		/// <summary>
		/// Shared textures
		/// </summary>
		static Dictionary<string, Texture> SharedTextures = new Dictionary<string, Texture>();


		/// <summary>
		/// Default texture parameters
		/// </summary>
		static public DefaultTextParameters TextureParameters
		{
			get;
			private set;
		}


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
				if (texture == value)
					return;

				texture = value;
				GL.BindTexture(TextureTarget.Texture2D, value.Handle);

				RenderStats.TextureBinding++;


				GL.MatrixMode(MatrixMode.Texture);
				GL.LoadIdentity();
				GL.Scale(1.0f / value.Size.Width, 1.0f / value.Size.Height, 1.0f);

				TextureMatrix = Matrix4.Scale(1.0f / texture.Size.Width, 1.0f / texture.Size.Height, 1.0f);
			}
			get
			{
				return texture;
			}
		}
		static Texture texture;


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
				if (value > Capabilities.MaxTextureUnits || value < 0)
					return;

				GL.ActiveTexture(OpenTK.Graphics.OpenGL.TextureUnit.Texture0 + value);
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
		[Obsolete("Deprecated")]
		public static Color Color
		{
			get
			{
				return color;
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
		[Obsolete("Deprecated")]
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
		[Obsolete("Deprecated")]
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
		[Obsolete("Deprecated")]
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
		/// FSAA
		/// </summary>
		public static bool MultiSample
		{
			get
			{
				return GL.IsEnabled(EnableCap.Multisample);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.Multisample);
				else
					GL.Disable(EnableCap.Multisample);
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
		[Obsolete("Deprecated")]
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



		/// <summary>
		/// Gets/sets the current shader
		/// </summary>
		public static Shader Shader
		{
			get
			{
				return shader;
			}
			set
			{
				if (value == null)
					GL.UseProgram(0);
				else
					GL.UseProgram(value.ProgramID);

				shader = value;
			}
		}
		static Shader shader;


		#endregion
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

			string version = GL.GetString(StringName.Version);
			if (version[0] - '0' >= 3)
			{
				int value = 0;
				GL.GetInteger(GetPName.MajorVersion, out value);
				MajorVersion = value;
				GL.GetInteger(GetPName.MinorVersion, out value);
				MinorVersion = value;


				Extensions = new List<string>();
				int count = 0;
				GL.GetInteger(GetPName.NumExtensions, out count);
				for (int id = 0; id < count; id++)
				{
					Extensions.Add(GL.GetString(StringName.Extensions, id));
				}
			}
			else
			{
				MajorVersion = version[0] - '0';
				MinorVersion = version[2] - '0';

				Extensions = new List<string>(GL.GetString(StringName.Extensions).Split(new char[] { ' ' }));
		
			}


			if (Extensions.Contains("GL_ARB_texture_non_power_of_two"))
				HasNonPowerOf2Textures = true;

			if (Extensions.Contains("GL_ARB_framebuffer_object"))
				HasFBO = true;

			if (Extensions.Contains("GL_ARB_pixel_buffer_object"))
				HasPBO = true;

			if (Extensions.Contains("GL_ARB_vertex_buffer_object"))
				HasVBO = true;

			if (Extensions.Contains("GL_ARB_multisample"))
			{
				HasMultiSample = true;
				GL.GetInteger(GetPName.SampleBuffers, out integer);
				MaxMultiSample = integer;
			}

			GL.GetInteger(GetPName.MaxTextureUnits, out integer);
			MaxTextureUnits = integer;

			GL.GetInteger(GetPName.MaxDrawBuffers, out integer);
			MaxDrawBuffers = integer;

			if (Extensions.Contains("GL_NVX_gpu_memory_info"))
			{
				Trace.Indent();
				int val = 0;
				GL.GetInteger((GetPName)0x9047, out val);
				Trace.WriteLine("Dedicated video memory : {0} Kb", val);
				GL.GetInteger((GetPName)0x9048, out val);
				Trace.WriteLine("Total available memory : {0} Kb", val);
				GL.GetInteger((GetPName)0x9049, out val);
				Trace.WriteLine("Current available dedicated video memory : {0} Kb", val);
				GL.GetInteger((GetPName)0x904A, out val);
				Trace.WriteLine("Total evictions : {0} Kb", val);
				GL.GetInteger((GetPName)0x904B, out val);
				Trace.WriteLine("Total video memory evicted : {0} Kb", val);
				Trace.Unindent();
			}

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
		/// Total number of texture image units from the fragment 
		/// and vertex processor can access combined.
		/// </summary>
		public int MaxTextureUnits
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
		/// Has Vertex Buffer Objects support
		/// </summary>
		public bool HasVBO
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
	/// <see cref="GameWindow"/> creation parameters
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
	}


	/// <summary>
	/// Default parameters applied to a new texture
	/// </summary>
	public class DefaultTextParameters
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public DefaultTextParameters()
		{
			MagFilter = TextureMagFilter.Linear;
			MinFilter = TextureMinFilter.Linear;
			BorderColor = Color.Black;
			HorizontalWrapFilter = HorizontalWrapFilter.Clamp;
			VerticalWrapFilter = VerticalWrapFilter.Clamp;
		}


		/// <summary>
		/// Magnify filter
		/// </summary>
		public TextureMagFilter MagFilter
		{
			get;
			set;
		}


		/// <summary>
		/// Minify filter
		/// </summary>
		public TextureMinFilter MinFilter
		{
			get;
			set;
		}


		/// <summary>
		/// Border color
		/// </summary>
		public Color BorderColor
		{
			get;
			set;
		}


		/// <summary>
		/// Horizontal wrap filter
		/// </summary>
		public HorizontalWrapFilter HorizontalWrapFilter
		{
			get;
			set;
		}


		/// <summary>
		/// Vertical wrap filter
		/// </summary>
		public VerticalWrapFilter VerticalWrapFilter
		{
			get;
			set;
		}


	}

}
