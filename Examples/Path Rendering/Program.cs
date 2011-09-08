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
			p.Major = 4;
			p.Minor = 1;
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


		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

		}


		/// <summary>
		/// 
		/// </summary>
		void Init()
		{
			float[] font_data = new float[4];
			int numChars = 256;
			float[] horizontalAdvance = new float[256];
			Message = "Hello World";


			DeletePaths(glyphBase, numChars);

			/* Create a range of path objects corresponding to Latin-1 character codes. */
			glyphBase = GenPaths(1 + numChars);
			pathTemplate = glyphBase;
			PathParameteri(pathTemplate, GL_PATH_STROKE_WIDTH_NV, 50);
			PathParameteri(pathTemplate, GL_PATH_JOIN_STYLE_NV, GL_ROUND_NV);
			glyphBase++;
			/* Choose a bold sans-serif font face, preferring Veranda over Arial; if
			neither font is available as a system font, settle for the "Sans" standard
			(built-in) font. */
			PathGlyphRange(glyphBase,
				GL_FILE_NAME_NV, "Pacifico.ttf", 0,
				0, numChars,
				GL_SKIP_MISSING_GLYPH_NV, pathTemplate, emScale);
			PathGlyphRange(glyphBase,
				GL_STANDARD_FONT_NAME_NV, "Sans", GL_BOLD_BIT_NV,
				0, numChars,
				GL_SKIP_MISSING_GLYPH_NV, pathTemplate, emScale);

			/* Query font and glyph metrics. */
			GetPathMetricRange(GL_FONT_Y_MIN_BOUNDS_NV | GL_FONT_Y_MAX_BOUNDS_NV |
				GL_FONT_UNDERLINE_POSITION_NV | GL_FONT_UNDERLINE_THICKNESS_NV,
				glyphBase + ' ', /*count*/1,
				4 * sizeof(float),
				font_data);
			yMin = font_data[0];
			yMax = font_data[1];
			underline_position = font_data[2];
			underline_thickness = font_data[3];
			//printf("Y min,max = %f,%f\n", yMin, yMax);
			//printf("underline: pos=%f, thickness=%f\n", underline_position, underline_thickness);
			GetPathMetricRange(GL_GLYPH_HORIZONTAL_BEARING_ADVANCE_BIT_NV,
				glyphBase, numChars,
				0, /* stride of zero means sizeof(float) since 1 bit in mask */
				horizontalAdvance);

			/* Query spacing information for example's message. */
			messageLen = strlen(message);
			xtranslate = (float*)malloc(sizeof(float) * messageLen);
			if (!xtranslate)
			{
				fprintf(stderr, "%s: malloc of xtranslate failed\n", programName);
				exit(1);
			}
			xtranslate[0] = 0.0;  /* Initial xtranslate is zero. */
			{
				/* Use 100% spacing; use 0.9 for both for 90% spacing. */
				float advanceScale = 1.0,
					kerningScale = 1.0; /* Set this to zero to ignore kerning. */
				GetPathSpacing(GL_ACCUM_ADJACENT_PAIRS_NV,
					(int)messageLen, GL_UNSIGNED_BYTE, message,
					glyphBase,
					advanceScale, kerningScale,
					GL_TRANSLATE_X_NV,
					&xtranslate[1]);  /* messageLen-1 accumulated translates are written here. */
			}

			/* Total advance is accumulated spacing plus horizontal advance of
			the last glyph */
			totalAdvance = xtranslate[messageLen - 1] +
				horizontalAdvance[message_ub[messageLen - 1]];
			xBorder = totalAdvance / messageLen;

			glEnable(GL_STENCIL_TEST);
			glStencilFunc(GL_NOTEQUAL, 0, ~0);
			glStencilOp(GL_KEEP, GL_KEEP, GL_ZERO);

		}



		/// <summary>
		/// Get all extension delegates
		/// </summary>
		void InitExtensions()
		{
			var context = OpenTK.Graphics.GraphicsContext.CurrentContext as OpenTK.Graphics.IGraphicsContextInternal;
			GenPaths = (glGenPaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glGenPathsNV"), typeof(glGenPaths));
			DeletePaths = (glDeletePaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glDeletePathsNV"), typeof(glDeletePaths));
			IsPath = (glIsPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glIsPathNV"), typeof(glIsPath));
			PathCommands = (glPathCommands)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathCommandsNV"), typeof(glPathCommands));
			PathCoords = (glPathCoords)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathCoordsNV"), typeof(glPathCoords));
			PathSubCommands = (glPathSubCommands)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathSubCommandsNV"), typeof(glPathSubCommands));
			PathSubCoords = (glPathSubCoords)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathSubCoordsNV"), typeof(glPathSubCoords));
			PathString = (glPathString)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathStringNV"), typeof(glPathString));
			PathGlyphs = (glPathGlyphs)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathGlyphsNV"), typeof(glPathGlyphs));
			PathGlyphRange = (glPathGlyphRange)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathGlyphRangeNV"), typeof(glPathGlyphRange));
			WeightPaths = (glWeightPaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glWeightPathsNV"), typeof(glWeightPaths));
			CopyPath = (glCopyPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCopyPathNV"), typeof(glCopyPath));
			InterpolatePaths = (interpolatePaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("interpolatePathsNV"), typeof(interpolatePaths));
			TransformPath = (glTransformPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glTransformPathNV"), typeof(glTransformPath));
			PathParameteriv = (glPathParameteriv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameterivNV"), typeof(glPathParameteriv));
			PathParameteri = (glPathParameteri)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameteriNV"), typeof(glPathParameteri));
			PathParameterfv = (glPathParameterfv)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameterfvNV"), typeof(glPathParameterfv));
			PathParameterf = (glPathParameterf)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathParameterfNV"), typeof(glPathParameterf));
			PathDashArray = (glPathDashArray)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathDashArrayNV"), typeof(glPathDashArray));
			StencilFillPath = (glStencilFillPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilFillPathNV"), typeof(glStencilFillPath));
			PathStencilDepthOffset = (glPathStencilDepthOffset)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathStencilDepthOffsetNV"), typeof(glPathStencilDepthOffset));
			StencilStrokePath = (glStencilStrokePath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilStrokePathNV"), typeof(glStencilStrokePath));
			StencilFillPathInstanced = (glStencilFillPathInstanced)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilFillPathInstancedNV"), typeof(glStencilFillPathInstanced));
			StencilStrokePathInstanced = (glStencilStrokePathInstanced)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glStencilStrokePathInstancedNV"), typeof(glStencilStrokePathInstanced));
			PathCoverDepthFunc = (glPathCoverDepthFunc)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathCoverDepthFuncNV"), typeof(glPathCoverDepthFunc));
			PathColorGen = (glPathColorGen)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathColorGenNV"), typeof(glPathColorGen));
			PathTexGen = (glPathTexGen)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathTexGenNV"), typeof(glPathTexGen));
			PathFogGen = (glPathFogGen)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glPathFogGenNV"), typeof(glPathFogGen));
			CoverFillPath = (glCoverFillPath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCoverFillPathNV"), typeof(glCoverFillPath));
			CoverStrokePath = (glCoverStrokePath)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glCoverStrokePathNV"), typeof(glCoverStrokePath));
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

		}



		#region Extensions


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
		interpolatePaths InterpolatePaths;
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

		unsafe delegate int glGenPaths(int range);
		unsafe delegate void glDeletePaths(int path, int range);
		unsafe delegate void glIsPath(int path);
		unsafe delegate void glPathCommands(int path, int numCommands, byte* commands, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathCoords(int path, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathSubCommands(int path, int commandStart, int commandsToDelete, int numCommands, byte* commands, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathSubCoords(int path, int coordStart, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathString(int path, int format, int length, void* pathString);
		unsafe delegate void glPathGlyphs(int firstPathName, int fontTarget, void* fontName, int fontStyle, int numGlyphs, int type, void* charcodes, int handleMissingGlyphs, int pathParameterTemplate, float emScale);
		unsafe delegate void glPathGlyphRange(int firstPathName, int fontTarget, string fontName, int fontStyle, int firstGlyph, int numGlyphs, int handleMissingGlyphs, int pathParameterTemplate, float emScale);
		unsafe delegate void glWeightPaths(int resultPath, int numPaths, int* paths, float* weights);
		unsafe delegate void glCopyPath(int resultPath, int srcPath);
		unsafe delegate void interpolatePaths(int resultPath, int pathA, int pathB, float weight);
		unsafe delegate void glTransformPath(int resultPath, int srcPath, int transformType, float* transformValues);
		unsafe delegate void glPathParameteriv(int path, int pname, int* value);
		unsafe delegate void glPathParameteri(int path, int pname, int value);
		unsafe delegate void glPathParameterfv(int path, int pname, float* value);
		unsafe delegate void glPathParameterf(int path, int pname, float value);
		unsafe delegate void glPathDashArray(int path, int dashCount, float* dashArray);
		unsafe delegate void glStencilFillPath(int mode, int reference, int mask);
		unsafe delegate void glPathStencilDepthOffset(float factor, float units);
		unsafe delegate void glStencilStrokePath(int path, int fillMode, int mask);
		unsafe delegate void glStencilFillPathInstanced(int path, int reference, int mask);
		unsafe delegate void glStencilStrokePathInstanced(int numPaths, int pathNameType, void* paths, int pathBase, int fillMode, int mask, int transformType, float* transformValues);
		unsafe delegate void glPathCoverDepthFunc(int numPaths, int pathNameType, void* paths, int pathBase, int reference, int mask, int transformType, float* transformValues);
		unsafe delegate void glPathColorGen(int func);
		unsafe delegate void glPathTexGen(int color, int genMode, int colorFormat, float* coeffs);
		unsafe delegate void glPathFogGen(int texCoordSet, int genMode, int components, float* coeffs);
		unsafe delegate void glCoverFillPath(int genMode);
		unsafe delegate void glCoverStrokePath(int path, int coverMode);
		unsafe delegate void glCoverFillPathInstanced(int path, int coverMode);
		unsafe delegate void glCoverStrokePathInstanced(int numPaths, int pathNameType, void* paths, int pathBase, int coverMode, int transformType, float* transformValues);
		unsafe delegate void glGetPathParameteriv(int numPaths, int pathNameType, void* paths, int pathBase, int coverMode, int transformType, float* transformValues);
		unsafe delegate void glGetPathParameterfv(int path, int pname, int* value);
		unsafe delegate void glGetPathCommands(int path, int pname, float* value);
		unsafe delegate void glGetPathCoords(int path, byte* commands);
		unsafe delegate void glGetPathDashArray(int path, float* coords);
		unsafe delegate void glGetPathMetrics(int path, float* dashArray);
		unsafe delegate void glGetPathMetricRange(int metricQueryMask, int numPaths, int pathNameType, void* paths, int pathBase, int stride, float* metrics);
		unsafe delegate void glGetPathSpacing(int metricQueryMask, int firstPathName, int numPaths, int stride, float* metrics);
		unsafe delegate void glGetPathColorGeniv(int pathListMode, int numPaths, int pathNameType, void* paths, int pathBase, float advanceScale, float kerningScale, int transformType, float* returnedSpacing);
		unsafe delegate void glGetPathColorGenfv(int color, int pname, int* value);
		unsafe delegate void glGetPathTexGeniv(int color, int pname, float* value);
		unsafe delegate void glGetPathTexGenfv(int texCoordSet, int pname, int* value);
		unsafe delegate bool glIsPointInFillPath(int texCoordSet, int pname, float* value);
		unsafe delegate bool glIsPointInStrokePath(int path, int mask, float x, float y);
		unsafe delegate float glGetPathLength(int path, float x, float y);
		unsafe delegate bool glPointAlongPath(int path, int startSegment, int numSegments);

		unsafe delegate void glPathStencilFunc(int path, int startSegment, int numSegments, float distance, float* x, float* y, float* tangentX, float* tangentY);









unsafe delegate int   GenPathsNV (int range);
unsafe delegate void  DeletePathsNV (uint path, int range);
unsafe delegate bool  IsPathNV (uint path);
unsafe delegate void  PathCommandsNV (uint path, int numCommands, const GLubyte *commands, int numCoords, int coordType, const GLvoid *coords);
unsafe delegate void  PathCoordsNV (uint path, int numCoords, int coordType, const GLvoid *coords);
unsafe delegate void  PathSubCommandsNV (uint path, int commandStart, int commandsToDelete, int numCommands, const GLubyte *commands, int numCoords, int coordType, const GLvoid *coords);
unsafe delegate void  PathSubCoordsNV (uint path, int coordStart, int numCoords, int coordType, const GLvoid *coords);
unsafe delegate void  PathStringNV (uint path, int format, int length, const GLvoid *pathString);
unsafe delegate void  PathGlyphsNV (uint firstPathName, int fontTarget, const GLvoid *fontName, GLbitfield fontStyle, int numGlyphs, int type, const GLvoid *charcodes, int handleMissingGlyphs, uint pathParameterTemplate, float emScale);
unsafe delegate void  PathGlyphRangeNV (uint firstPathName, int fontTarget, const GLvoid *fontName, GLbitfield fontStyle, uint firstGlyph, int numGlyphs, int handleMissingGlyphs, uint pathParameterTemplate, float emScale);
unsafe delegate void  WeightPathsNV (uint resultPath, int numPaths, const uint *paths, const float *weights);
unsafe delegate void  CopyPathNV (uint resultPath, uint srcPath);
unsafe delegate void  InterpolatePathsNV (uint resultPath, uint pathA, uint pathB, float weight);
unsafe delegate void  TransformPathNV (uint resultPath, uint srcPath, int transformType, const float *transformValues);
unsafe delegate void  TransformPathNV (uint resultPath, uint srcPath, int transformType, const float *transformValues);
unsafe delegate void  PathParameterivNV (uint path, int pname, const int *value);
unsafe delegate void  PathParameteriNV (uint path, int pname, int value);
unsafe delegate void  PathParameterfvNV (uint path, int pname, const float *value);
unsafe delegate void  PathParameterfNV (uint path, int pname, float value);
unsafe delegate void  PathDashArrayNV (uint path, int dashCount, const float *dashArray);
unsafe delegate void  PathStencilFuncNV (int func, int ref, uint mask);
unsafe delegate void  PathStencilDepthOffsetNV (float factor, float units);
unsafe delegate void  StencilFillPathNV (uint path, int fillMode, uint mask);
unsafe delegate void  StencilStrokePathNV (uint path, int reference, uint mask);
unsafe delegate void  StencilFillPathInstancedNV (int numPaths, int pathNameType, const GLvoid *paths, uint pathBase, int fillMode, uint mask, int transformType, const float *transformValues);
unsafe delegate void  StencilStrokePathInstancedNV (int numPaths, int pathNameType, const GLvoid *paths, uint pathBase, int reference, uint mask, int transformType, const float *transformValues);
unsafe delegate void  PathCoverDepthFuncNV (int func);
unsafe delegate void  PathColorGenNV (int color, int genMode, int colorFormat, const float *coeffs);
unsafe delegate void  PathTexGenNV (int texCoordSet, int genMode, int components, const float *coeffs);
unsafe delegate void  PathFogGenNV (int genMode);
unsafe delegate void  CoverFillPathNV (uint path, int coverMode);
unsafe delegate void  CoverStrokePathNV (uint path, int coverMode);
unsafe delegate void  CoverFillPathInstancedNV (int numPaths, int pathNameType, const GLvoid *paths, uint pathBase, int coverMode, int transformType, const float *transformValues);
unsafe delegate void  CoverStrokePathInstancedNV (int numPaths, int pathNameType, const GLvoid *paths, uint pathBase, int coverMode, int transformType, const float *transformValues);
unsafe delegate void  GetPathParameterivNV (uint path, int pname, int *value);
unsafe delegate void  GetPathParameterfvNV (uint path, int pname, float *value);
unsafe delegate void  GetPathCommandsNV (uint path, GLubyte *commands);
unsafe delegate void  GetPathCoordsNV (uint path, float *coords);
unsafe delegate void  GetPathDashArrayNV (uint path, float *dashArray);
unsafe delegate void  GetPathMetricsNV (GLbitfield metricQueryMask, int numPaths, int pathNameType, const GLvoid *paths, uint pathBase, int stride, float *metrics);
unsafe delegate void  GetPathMetricRangeNV (GLbitfield metricQueryMask, uint firstPathName, int numPaths, int stride, float *metrics);
unsafe delegate void  GetPathSpacingNV (int pathListMode, int numPaths, int pathNameType, const GLvoid *paths, uint pathBase, float advanceScale, float kerningScale, int transformType, float *returnedSpacing);
unsafe delegate void  GetPathColorGenivNV (int color, int pname, int *value);
unsafe delegate void  GetPathColorGenfvNV (int color, int pname, float *value);
unsafe delegate void  GetPathTexGenivNV (int texCoordSet, int pname, int *value);
unsafe delegate void  GetPathTexGenfvNV (int texCoordSet, int pname, float *value);
unsafe delegate bool  IsPointInFillPathNV (uint path, uint mask, float x, float y);
unsafe delegate bool  IsPointInStrokePathNV (uint path, float x, float y);
unsafe delegate float GetPathLengthNV (uint path, int startSegment, int numSegments);
unsafe delegate bool  PointAlongPathNV (uint path, int startSegment, int numSegments, float distance, float *x, float *y, float *tangentX, float *tangentY);










		#endregion


		#region Defines

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
		/// Message to display
		/// </summary>
		string Message;


		int glyphBase;
		int pathTemplate;
		//const char* message = "Hello world!"; /* the message to show */
		//size_t messageLen;
		float xtranslate;
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


