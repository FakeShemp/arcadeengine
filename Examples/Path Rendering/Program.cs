#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

#region GLDataTypes
// These define the mapping between OpenGL data types
// and the .net equivalents.
using GLsizei = System.Int32;
using GLsizeiptr = System.IntPtr;
using GLintptr = System.IntPtr;
using GLenum = System.Int32;
using GLboolean = System.Int32;
using GLbitfield = System.Int32;
using GLvoid = System.Object;
using GLchar = System.Char;
using GLbyte = System.Byte;
using GLubyte = System.Byte;
using GLshort = System.Int16;
using GLushort = System.Int16;
using GLint = System.Int32;
using GLuint = System.Int32;
using GLfloat = System.Single;
using GLclampf = System.Single;
using GLdouble = System.Double;
using GLclampd = System.Double;
using GLstring = System.String;
using GLsizeiptrARB = System.IntPtr;
using GLintptrARB = System.IntPtr;
using GLhandleARB = System.Int32;
using GLhalfARB = System.Int16;
using GLhalfNV = System.Int16;
using GLcharARB = System.Char;
using GLint64EXT = System.Int64;
using GLuint64EXT = System.Int64;
using GLint64 = System.Int64;
using GLuint64 = System.Int64;
#endregion


namespace ArcEngine.Examples.PathRenderingDemo
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Program : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (Program game = new Program())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			GameWindowParams p = new GameWindowParams();
			p.Size = new Size(1024, 768);
			p.Major = 3;
			p.Minor = 0;
			CreateGameWindow(p);
		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Render states
			Display.RenderState.ClearColor = Color.Black;
			Display.RenderState.DepthTest = true;

			Batch = new SpriteBatch();

			Path = new PathRendering();

			InitExtensions();

			Init();
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Path != null)
				Path.Dispose();
			Path = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;
		}


		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the Escape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();
		}

		bool filling = true;
		bool even_odd = true;
		bool stroking = true;

		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			GL.ClearStencil(0);
			GL.ClearColor(0, 255, 0, 0);
			GL.StencilMask(~0);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);




		}


		int pathObj = 42;

		void initPathFromSVG()
		{
			/* Here is an example of specifying and then rendering a five-point
			star and a heart as a path using Scalable Vector Graphics (SVG)
			path description syntax: */

			string svgPathString = "M100,180 L40,10 L190,120 L10,120 L160,10 z" +      // star
								   "M300 300 C 100 400,100 200,300 100,500 200,500 400,300 300Z"; // heart

		//	PathString(pathObj, GL_PATH_FORMAT_SVG_NV, svgPathString.Length, Marshal.StringToHGlobalUni(svgPathString));
			PathString(pathObj, GL_PATH_FORMAT_SVG_NV, svgPathString.Length, svgPathString);
			Display.GetLastError("");
		}

		/// <summary>
		/// 
		/// </summary>
		void Init()
		{
			//	pathObj = GenPaths(1);

			initPathFromSVG();

			PathParameteri(pathObj, GL_PATH_JOIN_STYLE_NV, GL_ROUND_NV);
			Display.GetLastError("");
			PathParameterf(pathObj, GL_PATH_STROKE_WIDTH_NV, 6.5f);
			Display.GetLastError("");





		}


		/// <summary>
		/// Get all extension delegates
		/// </summary>
		void InitExtensions()
		{
			var context = OpenTK.Graphics.GraphicsContext.CurrentContext as OpenTK.Graphics.IGraphicsContextInternal;

			// GL_EXT_direct_state_access
			MatrixLoadIdentity = (glMatrixLoadIdentity)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glMatrixLoadIdentityEXT"), typeof(glMatrixLoadIdentity));
			MatrixOrtho = (glMatrixOrtho)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glMatrixOrthoEXT"), typeof(glMatrixOrtho));


			// NV_rendering_path
			PathCommands = (glPathCommands)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathCommandsNV"), typeof(glPathCommands));
			PathString = (glPathString)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathStringNV"), typeof(glPathString));
			PathParameteri = (glPathParameteri)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameteriNV"), typeof(glPathParameteri));
			PathParameterf = (glPathParameterf)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameterfNV"), typeof(glPathParameterf));
			CoverFillPath = (glCoverFillPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCoverFillPathNV"), typeof(glCoverFillPath));
			CoverStrokePath = (glCoverStrokePath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCoverStrokePathNV"), typeof(glCoverStrokePath));
			StencilFillPath = (glStencilFillPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilFillPathNV"), typeof(glStencilFillPath));
			StencilStrokePath = (glStencilStrokePath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilStrokePathNV"), typeof(glStencilStrokePath));
			  
/*
			  
			PathParameterfv = (glPathParameterfv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameterfvNV"), typeof(glPathParameterfv));
			PathParameteriv = (glPathParameteriv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameterivNV"), typeof(glPathParameteriv));
			GenPaths = (glGenPaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGenPathsNV"), typeof(glGenPaths));
			DeletePaths = (glDeletePaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glDeletePathsNV"), typeof(glDeletePaths));
			IsPath = (glIsPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glIsPathNV"), typeof(glIsPath));
			PathCoords = (glPathCoords)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathCoordsNV"), typeof(glPathCoords));
			PathSubCommands = (glPathSubCommands)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathSubCommandsNV"), typeof(glPathSubCommands));
			PathSubCoords = (glPathSubCoords)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathSubCoordsNV"), typeof(glPathSubCoords));
			PathGlyphs = (glPathGlyphs)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathGlyphsNV"), typeof(glPathGlyphs));
			PathGlyphRange = (glPathGlyphRange)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathGlyphRangeNV"), typeof(glPathGlyphRange));
			WeightPaths = (glWeightPaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glWeightPathsNV"), typeof(glWeightPaths));
			CopyPath = (glCopyPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCopyPathNV"), typeof(glCopyPath));
			InterpolatePaths = (glInterpolatePaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glInterpolatePathsNV"), typeof(glInterpolatePaths));
			TransformPath = (glTransformPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glTransformPathNV"), typeof(glTransformPath));
			PathDashArray = (glPathDashArray)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathDashArrayNV"), typeof(glPathDashArray));
			PathStencilDepthOffset = (glPathStencilDepthOffset)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathStencilDepthOffsetNV"), typeof(glPathStencilDepthOffset));
			StencilFillPathInstanced = (glStencilFillPathInstanced)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilFillPathInstancedNV"), typeof(glStencilFillPathInstanced));
			StencilStrokePathInstanced = (glStencilStrokePathInstanced)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilStrokePathInstancedNV"), typeof(glStencilStrokePathInstanced));
			PathCoverDepthFunc = (glPathCoverDepthFunc)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathCoverDepthFuncNV"), typeof(glPathCoverDepthFunc));
			PathColorGen = (glPathColorGen)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathColorGenNV"), typeof(glPathColorGen));
			PathTexGen = (glPathTexGen)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathTexGenNV"), typeof(glPathTexGen));
			PathFogGen = (glPathFogGen)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathFogGenNV"), typeof(glPathFogGen));
			CoverFillPathInstanced = (glCoverFillPathInstanced)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCoverFillPathInstancedNV"), typeof(glCoverFillPathInstanced));
			CoverStrokePathInstanced = (glCoverStrokePathInstanced)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCoverStrokePathInstancedNV"), typeof(glCoverStrokePathInstanced));
			GetPathParameteriv = (glGetPathParameteriv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathParameterivNV"), typeof(glGetPathParameteriv));
			GetPathParameterfv = (glGetPathParameterfv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathParameterfvNV"), typeof(glGetPathParameterfv));
			GetPathCommands = (glGetPathCommands)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathCommandsNV"), typeof(glGetPathCommands));
			GetPathCoords = (glGetPathCoords)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathCoordsNV"), typeof(glGetPathCoords));
			GetPathDashArray = (glGetPathDashArray)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathDashArrayNV"), typeof(glGetPathDashArray));
			GetPathMetrics = (glGetPathMetrics)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathMetricsNV"), typeof(glGetPathMetrics));
			GetPathMetricRange = (glGetPathMetricRange)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathMetricRangeNV"), typeof(glGetPathMetricRange));
			GetPathSpacing = (glGetPathSpacing)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathSpacingNV"), typeof(glGetPathSpacing));
			GetPathColorGeniv = (glGetPathColorGeniv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathColorGenivNV"), typeof(glGetPathColorGeniv));
			GetPathColorGenfv = (glGetPathColorGenfv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathColorGenfvNV"), typeof(glGetPathColorGenfv));
			GetPathTexGeniv = (glGetPathTexGeniv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathTexGenivNV"), typeof(glGetPathTexGeniv));
			GetPathTexGenfv = (glGetPathTexGenfv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathTexGenfvNV"), typeof(glGetPathTexGenfv));
			IsPointInFillPath = (glIsPointInFillPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glIsPointInFillPathNV"), typeof(glIsPointInFillPath));
			IsPointInStrokePath = (glIsPointInStrokePath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glIsPointInStrokePathNV"), typeof(glIsPointInStrokePath));
			GetPathLength = (glGetPathLength)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGetPathLengthNV"), typeof(glGetPathLength));
			PointAlongPath = (glPointAlongPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPointAlongPathNV"), typeof(glPointAlongPath));
			PathStencilFunc = (glPathStencilFunc)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathStencilFuncNV"), typeof(glPathStencilFunc));
*/
		}



		#region Extensions

		glMatrixLoadIdentity MatrixLoadIdentity;
		glMatrixOrtho MatrixOrtho;

/*
		glGenPaths GenPaths;
		glDeletePaths DeletePaths;
		glIsPath IsPath;
		glPathCommands PathCommands;
		glPathCoords PathCoords;
		glPathSubCommands PathSubCommands;
		glPathSubCoords PathSubCoords;
		glPathString PathString;
		glPathGlyphs PathGlyphs;
		glPathGlyphRange PathGlyphRange;
		glWeightPaths WeightPaths;
		glCopyPath CopyPath;
		glInterpolatePaths InterpolatePaths;
		glTransformPath TransformPath;
		glPathParameteriv PathParameteriv;
		glPathParameteri PathParameteri;
		glPathParameterfv PathParameterfv;
		glPathParameterf PathParameterf;
		glPathDashArray PathDashArray;
		glStencilFillPath StencilFillPath;
		glPathStencilDepthOffset PathStencilDepthOffset;
		glStencilStrokePath StencilStrokePath;
		glStencilFillPathInstanced StencilFillPathInstanced;
		glStencilStrokePathInstanced StencilStrokePathInstanced;
		glPathCoverDepthFunc PathCoverDepthFunc;
		glPathColorGen PathColorGen;
		glPathTexGen PathTexGen;
		glPathFogGen PathFogGen;
		glCoverFillPath CoverFillPath;
		glCoverStrokePath CoverStrokePath;
		glCoverFillPathInstanced CoverFillPathInstanced;
		glCoverStrokePathInstanced CoverStrokePathInstanced;
		glGetPathParameteriv GetPathParameteriv;
		glGetPathParameterfv GetPathParameterfv;
		glGetPathCommands GetPathCommands;
		glGetPathCoords GetPathCoords;
		glGetPathDashArray GetPathDashArray;
		glGetPathMetrics GetPathMetrics;
		glGetPathMetricRange GetPathMetricRange;
		glGetPathSpacing GetPathSpacing;
		glGetPathColorGeniv GetPathColorGeniv;
		glGetPathColorGenfv GetPathColorGenfv;
		glGetPathTexGeniv GetPathTexGeniv;
		glGetPathTexGenfv GetPathTexGenfv;
		glIsPointInFillPath IsPointInFillPath;
		glIsPointInStrokePath IsPointInStrokePath;
		glGetPathLength GetPathLength;
		glPointAlongPath PointAlongPath;
		glPathStencilFunc PathStencilFunc;
*/
		unsafe delegate void glMatrixLoadIdentity(int mode);
		unsafe delegate void glMatrixOrtho(int mode, float left, float right, float bottom, float top, float zNear, float zFar);

/*
		unsafe delegate int glGenPaths(int range);
		unsafe delegate void glDeletePaths(int path, int range);
		unsafe delegate bool glIsPath(int path);
		unsafe delegate void glPathCommands(int path, int numCommands, byte* commands, int numCoords, int coordType, IntPtr coords);
		unsafe delegate void glPathCoords(int path, int numCoords, int coordType, IntPtr coords);
		unsafe delegate void glPathSubCommands(int path, int commandStart, int commandsToDelete, int numCommands, byte* commands, int numCoords, int coordType, IntPtr coords);
		unsafe delegate void glPathSubCoords(int path, int coordStart, int numCoords, int coordType, IntPtr coords);
		unsafe delegate void glPathString(int path, int format, int length, IntPtr pathString);
		unsafe delegate void glPathGlyphs(int firstPathName, int fontTarget, IntPtr fontName, int fontStyle, int numGlyphs, int type, IntPtr charcodes, int handleMissingGlyphs, int pathParameterTemplate, float emScale);
		unsafe delegate void glPathGlyphRange(int firstPathName, int fontTarget, IntPtr fontName, int fontStyle, int firstGlyph, int numGlyphs, int handleMissingGlyphs, int pathParameterTemplate, float emScale);
		unsafe delegate void glWeightPaths(int resultPath, int numPaths, int* paths, float* weights);
		unsafe delegate void glCopyPath(int resultPath, int srcPath);
		unsafe delegate void glInterpolatePaths(int resultPath, int pathA, int pathB, float weight);
		unsafe delegate void glTransformPath(int resultPath, int srcPath, int transformType, float* transformValues);
		unsafe delegate void glPathParameteriv(int path, int pname, IntPtr value);
		unsafe delegate void glPathParameteri(int path, int pname, int value);
		unsafe delegate void glPathParameterfv(int path, int pname, IntPtr value);
		unsafe delegate void glPathParameterf(int path, int pname, double value);
		unsafe delegate void glPathDashArray(int path, int dashCount, float* dashArray);
		unsafe delegate void glPathStencilFunc(int func, int reference, int mask);
		unsafe delegate void glPathStencilDepthOffset(float factor, float units);
		unsafe delegate void glStencilFillPath(int path, int fillMode, int mask);
		unsafe delegate void glStencilStrokePath(int path, int reference, int mask);
		unsafe delegate void glStencilFillPathInstanced(int numPaths, int pathNameType, IntPtr paths, int pathBase, int fillMode, int mask, int transformType, float* transformValues);
		unsafe delegate void glStencilStrokePathInstanced(int numPaths, int pathNameType, IntPtr paths, int pathBase, int reference, int mask, int transformType, float* transformValues);
		unsafe delegate void glPathCoverDepthFunc(int func);
		unsafe delegate void glPathColorGen(int color, int genMode, int colorFormat, float* coeffs);
		unsafe delegate void glPathTexGen(int texCoordSet, int genMode, int components, float* coeffs);
		unsafe delegate void glPathFogGen(int genMode);
		unsafe delegate void glCoverFillPath(int path, int coverMode);
		unsafe delegate void glCoverStrokePath(int path, int coverMode);
		unsafe delegate void glCoverFillPathInstanced(int numPaths, int pathNameType, IntPtr paths, int pathBase, int coverMode, int transformType, float* transformValues);
		unsafe delegate void glCoverStrokePathInstanced(int numPaths, int pathNameType, IntPtr paths, int pathBase, int coverMode, int transformType, float* transformValues);
		unsafe delegate void glGetPathParameteriv(int path, int pname, int* value);
		unsafe delegate void glGetPathParameterfv(int path, int pname, float* value);
		unsafe delegate void glGetPathCommands(int path, byte* commands);
		unsafe delegate void glGetPathCoords(int path, float* coords);
		unsafe delegate void glGetPathDashArray(int path, float* dashArray);
		unsafe delegate void glGetPathMetrics(int metricQueryMask, int numPaths, int pathNameType, IntPtr paths, int pathBase, int stride, IntPtr metrics);
		unsafe delegate void glGetPathMetricRange(int metricQueryMask, int firstPathName, int numPaths, int stride, IntPtr metrics);
		unsafe delegate void glGetPathSpacing(int pathListMode, int numPaths, int pathNameType, IntPtr paths, int pathBase, float advanceScale, float kerningScale, int transformType, IntPtr returnedSpacing);
		unsafe delegate void glGetPathColorGeniv(int color, int pname, int* value);
		unsafe delegate void glGetPathColorGenfv(int color, int pname, float* value);
		unsafe delegate void glGetPathTexGeniv(int texCoordSet, int pname, int* value);
		unsafe delegate void glGetPathTexGenfv(int texCoordSet, int pname, float* value);
		unsafe delegate bool glIsPointInFillPath(int path, int mask, float x, float y);
		unsafe delegate bool glIsPointInStrokePath(int path, float x, float y);
		unsafe delegate float glGetPathLength(int path, int startSegment, int numSegments);
		unsafe delegate bool glPointAlongPath(int path, int startSegment, int numSegments, float distance, float* x, float* y, float* tangentX, float* tangentY);
*/



		unsafe delegate void glPathCommands(GLuint path, GLsizei numCommands, GLubyte *commands, GLsizei numCoords, GLenum coordType, GLintptr coords);
		unsafe delegate void glPathString(GLuint path, GLenum format, GLsizei length, string pathString);
		unsafe delegate void glPathParameteri(GLuint path, GLenum pname, GLint value);
		unsafe delegate void glPathParameterf(GLuint path, GLenum pname, GLfloat value);
		unsafe delegate void glCoverFillPath(GLuint path, GLenum fillMode, GLuint mask);
		unsafe delegate void glCoverStrokePath(GLuint path, GLint reference, GLuint mask);
		unsafe delegate void glStencilFillPath(GLuint path, GLenum coverMode);
		unsafe delegate void glStencilStrokePath(GLuint path, GLenum coverMode);

		glPathCommands				PathCommands;		
		glPathString				PathString;		
		glPathParameteri			PathParameteri;	
		glPathParameterf			PathParameterf;	
		glCoverFillPath				CoverFillPath;		
		glCoverStrokePath			CoverStrokePath;	
		glStencilFillPath			StencilFillPath;	
		glStencilStrokePath			StencilStrokePath;


		#endregion


		#region Defines
		/* MatrixMode */
		const int GL_MODELVIEW = 0x1700;
		const int GL_PROJECTION = 0x1701;
		const int GL_TEXTURE = 0x1702;




		const int GL_BYTE = 0x1400;
		const int GL_UNSIGNED_BYTE = 0x1401;
		const int GL_SHORT = 0x1402;
		const int GL_UNSIGNED_SHORT = 0x1403;
		const int GL_INT = 0x1404;
		const int GL_UNSIGNED_INT = 0x1405;
		const int GL_FLOAT = 0x1406;
		const int GL_2_BYTES = 0x1407;
		const int GL_3_BYTES = 0x1408;
		const int GL_4_BYTES = 0x1409;
		const int GL_DOUBLE = 0x140A;


		const int GL_CLOSE_PATH_NV = 0x00;
		const int GL_MOVE_TO_NV = 0x02;
		const int GL_RELATIVE_MOVE_TO_NV = 0x03;
		const int GL_LINE_TO_NV = 0x04;
		const int GL_RELATIVE_LINE_TO_NV = 0x05;
		const int GL_HORIZONTAL_LINE_TO_NV = 0x06;
		const int GL_RELATIVE_HORIZONTAL_LINE_TO_NV = 0x07;
		const int GL_VERTICAL_LINE_TO_NV = 0x08;
		const int GL_RELATIVE_VERTICAL_LINE_TO_NV = 0x09;
		const int GL_QUADRATIC_CURVE_TO_NV = 0x0A;
		const int GL_RELATIVE_QUADRATIC_CURVE_TO_NV = 0x0B;
		const int GL_CUBIC_CURVE_TO_NV = 0x0C;
		const int GL_RELATIVE_CUBIC_CURVE_TO_NV = 0x0D;
		const int GL_SMOOTH_QUADRATIC_CURVE_TO_NV = 0x0E;
		const int GL_RELATIVE_SMOOTH_QUADRATIC_CURVE_TO_NV = 0x0F;
		const int GL_SMOOTH_CUBIC_CURVE_TO_NV = 0x10;
		const int GL_RELATIVE_SMOOTH_CUBIC_CURVE_TO_NV = 0x11;
		const int GL_SMALL_CCW_ARC_TO_NV = 0x12;
		const int GL_RELATIVE_SMALL_CCW_ARC_TO_NV = 0x13;
		const int GL_SMALL_CW_ARC_TO_NV = 0x14;
		const int GL_RELATIVE_SMALL_CW_ARC_TO_NV = 0x15;
		const int GL_LARGE_CCW_ARC_TO_NV = 0x16;
		const int GL_RELATIVE_LARGE_CCW_ARC_TO_NV = 0x17;
		const int GL_LARGE_CW_ARC_TO_NV = 0x18;
		const int GL_RELATIVE_LARGE_CW_ARC_TO_NV = 0x19;
		const int GL_CIRCULAR_CCW_ARC_TO_NV = 0xF8;
		const int GL_CIRCULAR_CW_ARC_TO_NV = 0xFA;
		const int GL_CIRCULAR_TANGENT_ARC_TO_NV = 0xFC;
		const int GL_ARC_TO_NV = 0xFE;
		const int GL_RELATIVE_ARC_TO_NV = 0xFF;
		const int GL_PATH_FORMAT_SVG_NV = 0x9070;
		const int GL_PATH_FORMAT_PS_NV = 0x9071;
		const int GL_STANDARD_FONT_NAME_NV = 0x9072;
		const int GL_SYSTEM_FONT_NAME_NV = 0x9073;
		const int GL_FILE_NAME_NV = 0x9074;
		const int GL_PATH_STROKE_WIDTH_NV = 0x9075;
		const int GL_PATH_END_CAPS_NV = 0x9076;
		const int GL_PATH_INITIAL_END_CAP_NV = 0x9077;
		const int GL_PATH_TERMINAL_END_CAP_NV = 0x9078;
		const int GL_PATH_JOIN_STYLE_NV = 0x9079;
		const int GL_PATH_MITER_LIMIT_NV = 0x907A;
		const int GL_PATH_DASH_CAPS_NV = 0x907B;
		const int GL_PATH_INITIAL_DASH_CAP_NV = 0x907C;
		const int GL_PATH_TERMINAL_DASH_CAP_NV = 0x907D;
		const int GL_PATH_DASH_OFFSET_NV = 0x907E;
		const int GL_PATH_CLIENT_LENGTH_NV = 0x907F;
		const int GL_PATH_FILL_MODE_NV = 0x9080;
		const int GL_PATH_FILL_MASK_NV = 0x9081;
		const int GL_PATH_FILL_COVER_MODE_NV = 0x9082;
		const int GL_PATH_STROKE_COVER_MODE_NV = 0x9083;
		const int GL_PATH_STROKE_MASK_NV = 0x9084;
		const int GL_PATH_SAMPLE_QUALITY_NV = 0x9085;
		const int GL_COUNT_UP_NV = 0x9088;
		const int GL_COUNT_DOWN_NV = 0x9089;
		const int GL_PATH_OBJECT_BOUNDING_BOX_NV = 0x908A;
		const int GL_CONVEX_HULL_NV = 0x908B;
		const int GL_BOUNDING_BOX_NV = 0x908D;
		const int GL_TRANSLATE_X_NV = 0x908E;
		const int GL_TRANSLATE_Y_NV = 0x908F;
		const int GL_TRANSLATE_2D_NV = 0x9090;
		const int GL_TRANSLATE_3D_NV = 0x9091;
		const int GL_AFFINE_2D_NV = 0x9092;
		const int GL_AFFINE_3D_NV = 0x9094;
		const int GL_TRANSPOSE_AFFINE_2D_NV = 0x9096;
		const int GL_TRANSPOSE_AFFINE_3D_NV = 0x9098;
		const int GL_UTF8_NV = 0x909A;
		const int GL_UTF16_NV = 0x909B;
		const int GL_BOUNDING_BOX_OF_BOUNDING_BOXES_NV = 0x909C;
		const int GL_PATH_COMMAND_COUNT_NV = 0x909D;
		const int GL_PATH_COORD_COUNT_NV = 0x909E;
		const int GL_PATH_DASH_ARRAY_COUNT_NV = 0x909F;
		const int GL_PATH_COMPUTED_LENGTH_NV = 0x90A0;
		const int GL_PATH_FILL_BOUNDING_BOX_NV = 0x90A1;
		const int GL_PATH_STROKE_BOUNDING_BOX_NV = 0x90A2;
		const int GL_SQUARE_NV = 0x90A3;
		const int GL_ROUND_NV = 0x90A4;
		const int GL_TRIANGULAR_NV = 0x90A5;
		const int GL_BEVEL_NV = 0x90A6;
		const int GL_MITER_REVERT_NV = 0x90A7;
		const int GL_MITER_TRUNCATE_NV = 0x90A8;
		const int GL_SKIP_MISSING_GLYPH_NV = 0x90A9;
		const int GL_USE_MISSING_GLYPH_NV = 0x90AA;
		const int GL_PATH_DASH_OFFSET_RESET_NV = 0x90B4;
		const int GL_MOVE_TO_RESETS_NV = 0x90B5;
		const int GL_MOVE_TO_CONTINUES_NV = 0x90B6;
		const int GL_BOLD_BIT_NV = 0x01;
		const int GL_ITALIC_BIT_NV = 0x02;
		const int GL_PATH_ERROR_POSITION_NV = 0x90AB;
		const int GL_PATH_FOG_GEN_MODE_NV = 0x90AC;
		const int GL_GLYPH_WIDTH_BIT_NV = 0x01;
		const int GL_GLYPH_HEIGHT_BIT_NV = 0x02;
		const int GL_GLYPH_HORIZONTAL_BEARING_X_BIT_NV = 0x04;
		const int GL_GLYPH_HORIZONTAL_BEARING_Y_BIT_NV = 0x08;
		const int GL_GLYPH_HORIZONTAL_BEARING_ADVANCE_BIT_NV = 0x10;
		const int GL_GLYPH_VERTICAL_BEARING_X_BIT_NV = 0x20;
		const int GL_GLYPH_VERTICAL_BEARING_Y_BIT_NV = 0x40;
		const int GL_GLYPH_VERTICAL_BEARING_ADVANCE_BIT_NV = 0x80;
		const int GL_GLYPH_HAS_KERNING_NV = 0x100;
		const int GL_FONT_X_MIN_BOUNDS_NV = 0x00010000;
		const int GL_FONT_Y_MIN_BOUNDS_NV = 0x00020000;
		const int GL_FONT_X_MAX_BOUNDS_NV = 0x00040000;
		const int GL_FONT_Y_MAX_BOUNDS_NV = 0x00080000;
		const int GL_FONT_UNITS_PER_EM_NV = 0x00100000;
		const int GL_FONT_ASCENDER_NV = 0x00200000;
		const int GL_FONT_DESCENDER_NV = 0x00400000;
		const int GL_FONT_HEIGHT_NV = 0x00800000;
		const int GL_FONT_MAX_ADVANCE_WIDTH_NV = 0x01000000;
		const int GL_FONT_MAX_ADVANCE_HEIGHT_NV = 0x02000000;
		const int GL_FONT_UNDERLINE_POSITION_NV = 0x04000000;
		const int GL_FONT_UNDERLINE_THICKNESS_NV = 0x08000000;
		const int GL_FONT_HAS_KERNING_NV = 0x10000000;
		const int GL_ACCUM_ADJACENT_PAIRS_NV = 0x90AD;
		const int GL_ADJACENT_PAIRS_NV = 0x90AE;
		const int GL_FIRST_TO_REST_NV = 0x90AF;
		const int GL_PATH_GEN_MODE_NV = 0x90B0;
		const int GL_PATH_GEN_COEFF_NV = 0x90B1;
		const int GL_PATH_GEN_COLOR_FORMAT_NV = 0x90B2;
		const int GL_PATH_GEN_COMPONENTS_NV = 0x90B3;
		const int GL_PATH_STENCIL_FUNC_NV = 0x90B7;
		const int GL_PATH_STENCIL_REF_NV = 0x90B8;
		const int GL_PATH_STENCIL_VALUE_MASK_NV = 0x90B9;
		const int GL_PATH_STENCIL_DEPTH_OFFSET_FACTOR_NV = 0x90BD;
		const int GL_PATH_STENCIL_DEPTH_OFFSET_UNITS_NV = 0x90BE;
		const int GL_PATH_COVER_DEPTH_FUNC_NV = 0x90BF;
		#endregion


		#region Enums

		enum PathCommand
		{
			CLOSE_PATH_NV = 0x00,
			MOVE_TO_NV = 0x02,
			RELATIVE_MOVE_TO_NV = 0x03,
			LINE_TO_NV = 0x04,
			RELATIVE_LINE_TO_NV = 0x05,
			HORIZONTAL_LINE_TO_NV = 0x06,
			RELATIVE_HORIZONTAL_LINE_TO_NV = 0x07,
			VERTICAL_LINE_TO_NV = 0x08,
			RELATIVE_VERTICAL_LINE_TO_NV = 0x09,
			QUADRATIC_CURVE_TO_NV = 0x0A,
			RELATIVE_QUADRATIC_CURVE_TO_NV = 0x0B,
			CUBIC_CURVE_TO_NV = 0x0C,
			RELATIVE_CUBIC_CURVE_TO_NV = 0x0D,
			SMOOTH_QUADRATIC_CURVE_TO_NV = 0x0E,
			RELATIVE_SMOOTH_QUADRATIC_CURVE_TO_NV = 0x0F,
			SMOOTH_CUBIC_CURVE_TO_NV = 0x10,
			RELATIVE_SMOOTH_CUBIC_CURVE_TO_NV = 0x11,
			SMALL_CCW_ARC_TO_NV = 0x12,
			RELATIVE_SMALL_CCW_ARC_TO_NV = 0x13,
			SMALL_CW_ARC_TO_NV = 0x14,
			RELATIVE_SMALL_CW_ARC_TO_NV = 0x15,
			LARGE_CCW_ARC_TO_NV = 0x16,
			RELATIVE_LARGE_CCW_ARC_TO_NV = 0x17,
			LARGE_CW_ARC_TO_NV = 0x18,
			RELATIVE_LARGE_CW_ARC_TO_NV = 0x19,
			CIRCULAR_CCW_ARC_TO_NV = 0xF8,
			CIRCULAR_CW_ARC_TO_NV = 0xFA,
			CIRCULAR_TANGENT_ARC_TO_NV = 0xFC,
			ARC_TO_NV = 0xFE,
			RELATIVE_ARC_TO_NV = 0xFF,
		}

		enum PathFormat
		{
			PATH_FORMAT_SVG_NV = 0x9070,
			PATH_FORMAT_PS_NV = 0x9071,
		}

		enum PathTarget
		{
			STANDARD_FONT_NAME_NV = 0x9072,
			SYSTEM_FONT_NAME_NV = 0x9073,
			FILE_NAME_NV = 0x9074,
		}

		enum PathMissingGlyph
		{
			SKIP_MISSING_GLYPH_NV = 0x90A9,
			USE_MISSING_GLYPH_NV = 0x90AA,
		}

		enum PathName
		{
			PATH_STROKE_WIDTH_NV = 0x9075,
			PATH_INITIAL_END_CAP_NV = 0x9077,
			PATH_TERMINAL_END_CAP_NV = 0x9078,
			PATH_JOIN_STYLE_NV = 0x9079,
			PATH_MITER_LIMIT_NV = 0x907A,
			PATH_INITIAL_DASH_CAP_NV = 0x907C,
			PATH_TERMINAL_DASH_CAP_NV = 0x907D,
			PATH_DASH_OFFSET_NV = 0x907E,
			PATH_CLIENT_LENGTH_NV = 0x907F,
			PATH_DASH_OFFSET_RESET_NV = 0x90B4,

			PATH_FILL_MODE_NV = 0x9080,
			PATH_FILL_MASK_NV = 0x9081,
			PATH_FILL_COVER_MODE_NV = 0x9082,
			PATH_STROKE_COVER_MODE_NV = 0x9083,
			PATH_STROKE_MASK_NV = 0x9084,

		}

		enum PathCaps
		{
			PATH_END_CAPS_NV = 0x9076,
			PATH_DASH_CAPS_NV = 0x907B,
		}

		enum PathFillMode
		{
			COUNT_UP_NV = 0x9088,
			COUNT_DOWN_NV = 0x9089,
		}

		enum PathColor
		{
			PRIMARY_COLOR = 0x8577,  // from OpenGL 1.3
			PRIMARY_COLOR_NV = 0x852C,  // from NV_register_combiners
			SECONDARY_COLOR_NV = 0x852D,  // from NV_register_combiners

		}

		enum PathGenMode
		{
			//NONE
			//EYE_LINEAR
			//OBJECT_LINEAR
			PATH_OBJECT_BOUNDING_BOX_NV = 0x908A,

		}

		enum PathStrokeCoverMode
		{
			CONVEX_HULL_NV = 0x908B,
			BOUNDING_BOX_NV = 0x908D,
			// PATH_FILL_COVER_MODE_NV                         see above

		}

		enum PathTransformType
		{
			//NONE
			TRANSLATE_X_NV = 0x908E,
			TRANSLATE_Y_NV = 0x908F,
			TRANSLATE_2D_NV = 0x9090,
			TRANSLATE_3D_NV = 0x9091,
			AFFINE_2D_NV = 0x9092,
			AFFINE_3D_NV = 0x9094,
			TRANSPOSE_AFFINE_2D_NV = 0x9096,
			TRANSPOSE_AFFINE_3D_NV = 0x9098,

		}

		enum PathNameType
		{
			UTF8_NV = 0x909A,
			UTF16_NV = 0x909B,

		}

		enum PathCoverMode
		{

			//CONVEX_HULL_NV                                  see above
			//BOUNDING_BOX_NV                                 see above
			BOUNDING_BOX_OF_BOUNDING_BOXES_NV = 0x909C,
			//PATH_FILL_COVER_MODE_NV                         see above

		}

		enum PathParameter
		{
			PATH_COMMAND_COUNT_NV = 0x909D,
			PATH_COORD_COUNT_NV = 0x909E,
			PATH_DASH_ARRAY_COUNT_NV = 0x909F,

			PATH_COMPUTED_LENGTH_NV = 0x90A0,

			//    PATH_OBJECT_BOUNDING_BOX_NV           =         see above
			PATH_FILL_BOUNDING_BOX_NV = 0x90A1,
			PATH_STROKE_BOUNDING_BOX_NV = 0x90A2,

		}

		enum PathParameterValue
		{
			//    FLAT
			SQUARE_NV = 0x90A3,
			ROUND_NV = 0x90A4,
			TRIANGULAR_NV = 0x90A5,

		}

		enum PathJoinStyle
		{
			//NONE
			//ROUND_NV                                        see above
			BEVEL_NV = 0x90A6,
			MITER_REVERT_NV = 0x90A7,
			MITER_TRUNCATE_NV = 0x90A8,
		}

		enum PathDashOffset
		{
			MOVE_TO_RESETS_NV = 0x90B5,
			MOVE_TO_CONTINUES_NV = 0x90B6,

		}

		enum PathFontStyle
		{
			//        NONE
			BOLD_BIT_NV = 0x01,
			ITALIC_BIT_NV = 0x02,

		}

		enum PathGet
		{
			PATH_ERROR_POSITION_NV = 0x90AB,

			PATH_FOG_GEN_MODE_NV = 0x90AC,

			PATH_STENCIL_FUNC_NV = 0x90B7,
			PATH_STENCIL_REF_NV = 0x90B8,
			PATH_STENCIL_VALUE_MASK_NV = 0x90B9,

			PATH_STENCIL_DEPTH_OFFSET_FACTOR_NV = 0x90BD,
			PATH_STENCIL_DEPTH_OFFSET_UNITS_NV = 0x90BE,

			PATH_COVER_DEPTH_FUNC_NV = 0x90BF,

		}

		[Flags]
		enum PathMetrics
		{
			// per-glyph metrics
			GLYPH_WIDTH_BIT_NV = 0x01,
			GLYPH_HEIGHT_BIT_NV = 0x02,
			GLYPH_HORIZONTAL_BEARING_X_BIT_NV = 0x04,
			GLYPH_HORIZONTAL_BEARING_Y_BIT_NV = 0x08,
			GLYPH_HORIZONTAL_BEARING_ADVANCE_BIT_NV = 0x10,
			GLYPH_VERTICAL_BEARING_X_BIT_NV = 0x20,
			GLYPH_VERTICAL_BEARING_Y_BIT_NV = 0x40,
			GLYPH_VERTICAL_BEARING_ADVANCE_BIT_NV = 0x80,
			GLYPH_HAS_KERNING_NV = 0x100,

			// per-font face metrics				   
			FONT_X_MIN_BOUNDS_NV = 0x00010000,
			FONT_Y_MIN_BOUNDS_NV = 0x00020000,
			FONT_X_MAX_BOUNDS_NV = 0x00040000,
			FONT_Y_MAX_BOUNDS_NV = 0x00080000,
			FONT_UNITS_PER_EM_NV = 0x00100000,
			FONT_ASCENDER_NV = 0x00200000,
			FONT_DESCENDER_NV = 0x00400000,
			FONT_HEIGHT_NV = 0x00800000,
			FONT_MAX_ADVANCE_WIDTH_NV = 0x01000000,
			FONT_MAX_ADVANCE_HEIGHT_NV = 0x02000000,
			FONT_UNDERLINE_POSITION_NV = 0x04000000,
			FONT_UNDERLINE_THICKNESS_NV = 0x08000000,
			FONT_HAS_KERNING_NV = 0x10000000,


		}

		enum PathListMode
		{
			ACCUM_ADJACENT_PAIRS_NV = 0x90AD,
			ADJACENT_PAIRS_NV = 0x90AE,
			FIRST_TO_REST_NV = 0x90AF,

		}

		enum PathTex
		{
			PATH_GEN_MODE_NV = 0x90B0,
			PATH_GEN_COEFF_NV = 0x90B1,
			PATH_GEN_COLOR_FORMAT_NV = 0x90B2,
			PATH_GEN_COMPONENTS_NV = 0x90B3,

		}


		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Message to display
		/// </summary>
		string Message;


		int glyphBase;
		int pathTemplate;
		//const char* message = "Hello world!"; /* the message to show */
		//size_t messageLen;
		float[] xtranslate;
		float window_width, window_height, aspect_ratio;
		float view_width, view_height;
		//Transform3x2 win2obj;
		int iheight;
		int background = 2;
		float yMin, yMax, underline_position, underline_thickness;
		float totalAdvance, xBorder;
		int emScale = 2048;


		/// <summary>
		/// 
		/// </summary>
		PathRendering Path;

		#endregion

	}

}


