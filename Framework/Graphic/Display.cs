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

			RenderStats = new RenderStats();
			TextureParameters = new DefaultTextParameters();

			RenderState = new RenderState();
		}


		/// <summary>
		/// Resets default state
		/// </summary>
		public static void Init()
		{
			Trace.WriteDebugLine("[Display] Init()");
			Capabilities = new RenderDeviceCapabilities();

			Texturing = true;
			RenderState.Blending = true;
			RenderState.ClearColor = Color.Black;
			RenderState.Culling = false;
			RenderState.DepthTest = false;

			TK.GL.Hint(TK.HintTarget.PolygonSmoothHint, TK.HintMode.Nicest);
			TK.GL.Hint(TK.HintTarget.LineSmoothHint, TK.HintMode.Nicest);
			TK.GL.Hint(TK.HintTarget.PointSmoothHint, TK.HintMode.Nicest);
			TK.GL.Enable(TK.EnableCap.PolygonSmooth);

			BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			TK.GL.ClearStencil(0);


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
		public static void GetLastError(string command)
		{
			TK.ErrorCode error = TK.GL.GetError();
			if (error == TK.ErrorCode.NoError)
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



		#region Drawing


		/// <summary>
		/// Specify the line stipple pattern
		/// </summary>
		/// <param name="factor">Specifies a multiplier for each bit in the line stipple pattern</param>
		/// <param name="pattern">16-bit integer whose bit pattern determines which fragments of a line will be drawn when the line is rasterized.</param>
		public static void SetLineStipple(int factor, short pattern)
		{
			TK.GL.LineStipple(factor, pattern);
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
		/// <param name="angle">Rotation angle in degree</param>
		/// <param name="pivot">Origin of rotation</param>
		static void DrawQuad(int x, int y, int width, int height, Color color, bool fill, float angle, Point pivot)
		{
			Texturing = false;

	//		SaveState();

			//GL.MatrixMode(MatrixMode.Projection);
			//GL.PushMatrix();

			// Translation & rotation
	//		Translate(x + pivot.X, y + pivot.Y);
			x = -pivot.X;
			y = -pivot.Y;
	//		Rotate(angle);



			if (fill)
				TK.GL.Begin(TK.BeginMode.Quads);
			else
				TK.GL.Begin(TK.BeginMode.LineLoop);
			TK.GL.Vertex2(x, y);
			TK.GL.Vertex2(x, y + height);
			TK.GL.Vertex2(x + width, y + height);
			TK.GL.Vertex2(x + width, y);
			TK.GL.End();

			//GL.PopMatrix();
		//	RestoreState();
			Texturing = true;

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
			Texturing = false;
			TK.GL.Begin(TK.BeginMode.Lines);
			TK.GL.Vertex2(x1, y1);
			TK.GL.Vertex2(x2, y2);
			TK.GL.End();
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
			TK.GL.Begin(TK.BeginMode.Points);
			TK.GL.Vertex2(x, y);
			TK.GL.End();
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
			RenderState.Culling = false;

			TK.GL.Begin(TK.BeginMode.LineLoop);
			for (int i = 0; i < points.Length; i++)
			{
				TK.GL.Vertex2(points[i].X, points[i].Y);
			}
			TK.GL.End();

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
			RenderState.Culling = false;

			TK.GL.Begin(TK.BeginMode.TriangleFan);
			for (int i = 0; i < points.Length; i++)
			{
				TK.GL.Vertex2(points[i].X, points[i].Y);
			}
			TK.GL.End();

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

			if (fill)
				TK.GL.Begin(TK.BeginMode.Polygon);
			else
				TK.GL.Begin(TK.BeginMode.LineLoop);
			float angle;
			for (int i = 0; i < 50.0f; i++)
			{
				angle = i * 2.0f * (float)Math.PI / 50.0f;
				TK.GL.Vertex2(x + Math.Cos(angle) * xradius, y + Math.Sin(angle) * yradius);
			}
			TK.GL.End();


			Texturing = true;
			RenderStats.DirectCall += 50;
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

			Texturing = false;

			int real_segments = (int)(Math.Abs(end) / (2.0f * Math.PI) * 50.0f) + 1;

			float theta = end / (float)(real_segments);
			float tangetial_factor = (float)Math.Tan(theta);
			float radial_factor = (float)(1 - Math.Cos(theta));

			float xx = (float)(x + radius * Math.Cos(start));
			float yy = (float)(y + radius * Math.Sin(start));

			if (fill)
				TK.GL.Begin(TK.BeginMode.Polygon);
			else
				TK.GL.Begin(TK.BeginMode.LineStrip);
			for (int ii = 0; ii < real_segments + 1; ii++)
			{
				TK.GL.Vertex2(xx, yy);

				float tx = -(yy - y);
				float ty = xx - x;

				xx += tx * tangetial_factor;
				yy += ty * tangetial_factor;

				float rx = x - xx;
				float ry = y - yy;

				xx += rx * radial_factor;
				yy += ry * radial_factor;
			}
			TK.GL.End();

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


			TK.GL.Enable(TK.EnableCap.Map1Vertex3);
			//GL.Enable(EnableCap.Map1Color4);

			TK.GL.Map1(TK.MapTarget.Map1Vertex3, 0, 50, 3, 4, points);
			//GL.Map1(MapTarget.Map1Color4, 0, CircleResolution, 4, 2, colors);
			TK.GL.Begin(TK.BeginMode.LineStrip);
			for (int i = 0; i <= 50; i++)
				TK.GL.EvalCoord1(i);
			TK.GL.End();

			TK.GL.Disable(TK.EnableCap.Map1Vertex3);
			TK.GL.Disable(TK.EnableCap.Map1Color4);

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

			TK.GL.VertexAttribPointer(index, size, TK.VertexAttribPointerType.Float, false, stride, offset);
		}



		/// <summary>
		/// Binds and draws an IndexBuffer
		/// </summary>
		/// <param name="buffer">Buffer handle</param>
		/// <param name="mode">Drawing mode</param>
		/// <param name="index">Index buffer</param>
		/// <param name="shader"></param>
		/// <returns></returns>
		public static void DrawIndexBuffer(Shader shader, BatchBuffer buffer, PrimitiveType mode, IndexBuffer index)
		{
			if (buffer == null || index == null)
				return;

			// Set the index buffer
			index.Bind();

			// Bind shader
			buffer.Bind();

			// Draw
			TK.GL.DrawElements((TK.BeginMode)mode, index.Count, TK.DrawElementsType.UnsignedInt, IntPtr.Zero);

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

			TK.GL.EnableVertexAttribArray(id);
		}


		/// <summary>
		/// Disables a buffer index
		/// </summary>
		/// <param name="id"></param>
		public static void DisableBufferIndex(int id)
		{
			TK.GL.DisableVertexAttribArray(id);
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
		/// Draws a texture
		/// </summary>
		/// <param name="texture">Texture to display</param>
		/// <param name="rect"></param>
		/// <param name="tex">Texture coords</param>
		static public void DrawTexture(Texture texture, Rectangle rect, Rectangle tex)
		{
			DrawTexture(texture, rect, tex, Color.White);
		}


		/// <summary>
		/// Draws a texture
		/// </summary>
		/// <param name="texture">Texture to display</param>
		/// <param name="rect"></param>
		/// <param name="tex"></param>
		/// <param name="color">Color to apply</param>
		static public void DrawTexture(Texture texture, Rectangle rect, Rectangle tex, Color color)
		{
			if (texture == null)
				return;

			Texture = texture;

			Buffer.AddRectangle(rect, color, tex);
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
			TK.GL.Begin(TK.BeginMode.Quads);

			TK.GL.TexCoord2(tex.X, tex.Y);
			TK.GL.Vertex2(rect.X, rect.Y);

			TK.GL.TexCoord2(tex.X, tex.Y + tex.Height);
			TK.GL.Vertex2(rect.X, rect.Y + rect.Height);

			TK.GL.TexCoord2(tex.X + tex.Width, tex.Y + tex.Height);
			TK.GL.Vertex2(rect.X + rect.Width, rect.Y + rect.Height);

			TK.GL.TexCoord2(tex.X + tex.Width, tex.Y);
			TK.GL.Vertex2(rect.X + rect.Width, rect.Y);

			TK.GL.End();

			RenderStats.DirectCall += 4;
		}


		#endregion



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
		/// Batch buffer
		/// </summary>
		public static BatchBuffer Buffer
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
				if (shader == value)
					return;

				shader = value;
				TK.GL.UseProgram(shader == null ? 0 : shader.ProgramID);

				RenderStats.ShaderBinding++;
			}
		}
		static Shader shader;

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
					TK.GL.BindTexture(TK.TextureTarget.Texture2D, 0);
					texture = null;
					return;
				}
				if (texture != value)
				{

					texture = value;
					TK.GL.BindTexture(TK.TextureTarget.Texture2D, value.Handle);

					RenderStats.TextureBinding++;
				}
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
				if (value.Size.IsEmpty)
					return;

				Rectangle rect = value;
				if (rect.Width == 0)
					rect.Width = 1;



				TK.GL.Viewport(0, 0, rect.Width, rect.Height);
				//GL.MatrixMode(MatrixMode.Projection);
				//GL.LoadIdentity();
				//GL.Ortho(rect.Left, rect.Width, rect.Height, rect.Top, -1, 1);
			//	DefaultMatrix();
				//		GL.MatrixMode(MatrixMode.Modelview);
				//		GL.LoadIdentity();
			}
		}

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
			set
			{
				//GL.Scissor(scissorZone.X,scissorZone.Bottom, scissorZone.Width,  scissorZone.Top - scissorZone.Bottom);
				TK.GL.Scissor(value.X, ViewPort.Height - value.Top - value.Height, value.Width, value.Height);
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

			TK.GL.GetInteger(TK.GetPName.MaxTextureUnits, out integer);
			MaxTextureUnits = integer;

			TK.GL.GetInteger(TK.GetPName.MaxDrawBuffers, out integer);
			MaxDrawBuffers = integer;

			if (Extensions.Contains("GL_NVX_gpu_memory_info"))
			{
				Trace.Indent();
				int val = 0;
				TK.GL.GetInteger((TK.GetPName)0x9047, out val);
				Trace.WriteLine("Dedicated video memory : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)0x9048, out val);
				Trace.WriteLine("Total available memory : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)0x9049, out val);
				Trace.WriteLine("Current available dedicated video memory : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)0x904A, out val);
				Trace.WriteLine("Total evictions : {0} Kb", val);
				TK.GL.GetInteger((TK.GetPName)0x904B, out val);
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
		LineStrip = TK.BeginMode.LineStrip,
	}


	/// <summary>
	/// 
	/// </summary>
	public enum StencilOp
	{
		/// <summary>
		/// 
		/// </summary>
		Zero = TK.StencilOp.Zero,

		/// <summary>
		/// 
		/// </summary>
		Invert = TK.StencilOp.Invert,

		/// <summary>
		/// 
		/// </summary>
		Keep = TK.StencilOp.Keep,

		/// <summary>
		/// 
		/// </summary>
		Replace = TK.StencilOp.Replace,

		/// <summary>
		/// 
		/// </summary>
		Incr = TK.StencilOp.Incr,

		/// <summary>
		/// 
		/// </summary>
		Decr = TK.StencilOp.Decr,

		/// <summary>
		/// 
		/// </summary>
		IncrWrap = TK.StencilOp.IncrWrap,

		/// <summary>
		/// 
		/// </summary>
		DecrWrap = TK.StencilOp.DecrWrap,
	}


	/// <summary>
	/// 
	/// </summary>
	public enum StencilFunction
	{
		/// <summary>
		/// 
		/// </summary>
		Never = TK.StencilFunction.Never,

		/// <summary>
		/// 
		/// </summary>
		Less = TK.StencilFunction.Less,

		/// <summary>
		/// 
		/// </summary>
		Equal = TK.StencilFunction.Equal,

		/// <summary>
		/// 
		/// </summary>
		Lequal = TK.StencilFunction.Lequal,

		/// <summary>
		/// 
		/// </summary>
		Greater = TK.StencilFunction.Greater,

		/// <summary>
		/// 
		/// </summary>
		Notequal = TK.StencilFunction.Notequal,

		/// <summary>
		/// 
		/// </summary>
		Gequal = TK.StencilFunction.Gequal,

		/// <summary>
		/// 
		/// </summary>
		Always = TK.StencilFunction.Always,
	}


	/// <summary>
	/// 
	/// </summary>
	public enum AlphaFunction
	{
		/// <summary>
		/// 
		/// </summary>
		Never = TK.AlphaFunction.Never,

		/// <summary>
		/// 
		/// </summary>
		Less = TK.AlphaFunction.Less,

		/// <summary>
		/// 
		/// </summary>
		Equal = TK.AlphaFunction.Equal,

		/// <summary>
		/// 
		/// </summary>
		Lequal = TK.AlphaFunction.Lequal,

		/// <summary>
		/// 
		/// </summary>
		Greater = TK.AlphaFunction.Greater,

		/// <summary>
		/// 
		/// </summary>
		Notequal = TK.AlphaFunction.Notequal,

		/// <summary>
		/// 
		/// </summary>
		Gequal = TK.AlphaFunction.Gequal,
		Always = 519,
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
	/// 
	/// </summary>
	public enum TextureMagFilter
	{
		/// <summary>
		/// 
		/// </summary>
		Nearest = TK.TextureMagFilter.Nearest,

		/// <summary>
		/// 
		/// </summary>
		Linear = TK.TextureMagFilter.Linear,
	}


	/// <summary>
	/// 
	/// </summary>
	public enum TextureMinFilter
	{

		/// <summary>
		/// 
		/// </summary>
		Nearest = TK.TextureMinFilter.Nearest,

		/// <summary>
		/// 
		/// </summary>
		Linear = TK.TextureMinFilter.Linear,

		/// <summary>
		/// 
		/// </summary>
		NearestMipmapNearest = TK.TextureMinFilter.NearestMipmapNearest,

		/// <summary>
		/// 
		/// </summary>
		LinearMipmapNearest = TK.TextureMinFilter.LinearMipmapNearest,

		/// <summary>
		/// 
		/// </summary>
		NearestMipmapLinear = TK.TextureMinFilter.NearestMipmapLinear,

		/// <summary>
		/// 
		/// </summary>
		LinearMipmapLinear = TK.TextureMinFilter.LinearMipmapLinear,
	}

}
