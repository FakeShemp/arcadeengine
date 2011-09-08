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
			InterpolatePaths = (glInterpolatePaths)Marshal.GetDelegateForFunctionPointer(context.GetAddress("glInterpolatePathsNV"), typeof(glInterpolatePaths));
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

		unsafe delegate void glGenPaths(int range);
		unsafe delegate void glDeletePaths(uint path, int range);
		unsafe delegate void glIsPath(uint path);
		unsafe delegate void glPathCommands(uint path, int numCommands, byte* commands, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathCoords(uint path, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathSubCommands(uint path, int commandStart, int commandsToDelete, int numCommands, byte* commands, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathSubCoords(uint path, int coordStart, int numCoords, int coordType, void* coords);
		unsafe delegate void glPathString(uint path, int format, int length, void* pathString);
		unsafe delegate void glPathGlyphs(uint firstPathName, int fontTarget, void* fontName, int fontStyle, int numGlyphs, int type, void* charcodes, int handleMissingGlyphs, uint pathParameterTemplate, float emScale);
		unsafe delegate void glPathGlyphRange(uint firstPathName, int fontTarget, void* fontName, int fontStyle, uint firstGlyph, int numGlyphs, int handleMissingGlyphs, uint pathParameterTemplate, float emScale);
		unsafe delegate void glWeightPaths(uint resultPath, int numPaths, uint* paths, float* weights);
		unsafe delegate void glCopyPath(uint resultPath, uint srcPath);
		unsafe delegate void glInterpolatePaths(uint resultPath, uint pathA, uint pathB, float weight);
		unsafe delegate void glTransformPath(uint resultPath, uint srcPath, int transformType, float* transformValues);
		unsafe delegate void glPathParameteriv(uint path, int pname, int* value);
		unsafe delegate void glPathParameteri(uint path, int pname, int value);
		unsafe delegate void glPathParameterfv(uint path, int pname, float* value);
		unsafe delegate void glPathParameterf(uint path, int pname, float value);
		unsafe delegate void glPathDashArray(uint path, int dashCount, float* dashArray);
		unsafe delegate void glStencilFillPath(int mode, int reference, uint mask);
		unsafe delegate void glPathStencilDepthOffset(float factor, float units);
		unsafe delegate void glStencilStrokePath(uint path, int fillMode, uint mask);
		unsafe delegate void glStencilFillPathInstanced(uint path, int reference, uint mask);
		unsafe delegate void glStencilStrokePathInstanced(int numPaths, int pathNameType, void* paths, uint pathBase, int fillMode, uint mask, int transformType, float* transformValues);
		unsafe delegate void glPathCoverDepthFunc(int numPaths, int pathNameType, void* paths, uint pathBase, int reference, uint mask, int transformType, float* transformValues);
		unsafe delegate void glPathColorGen(int func);
		unsafe delegate void glPathTexGen(int color, int genMode, int colorFormat, float* coeffs);
		unsafe delegate void glPathFogGen(int texCoordSet, int genMode, int components, float* coeffs);
		unsafe delegate void glCoverFillPath(int genMode);
		unsafe delegate void glCoverStrokePath(uint path, int coverMode);
		unsafe delegate void glCoverFillPathInstanced(uint path, int coverMode);
		unsafe delegate void glCoverStrokePathInstanced(int numPaths, int pathNameType, void* paths, uint pathBase, int coverMode, int transformType, float* transformValues);
		unsafe delegate void glGetPathParameteriv(int numPaths, int pathNameType, void* paths, uint pathBase, int coverMode, int transformType, float* transformValues);
		unsafe delegate void glGetPathParameterfv(uint path, int pname, int* value);
		unsafe delegate void glGetPathCommands(uint path, int pname, float* value);
		unsafe delegate void glGetPathCoords(uint path, byte* commands);
		unsafe delegate void glGetPathDashArray(uint path, float* coords);
		unsafe delegate void glGetPathMetrics(uint path, float* dashArray);
		unsafe delegate void glGetPathMetricRange(int metricQueryMask, int numPaths, int pathNameType, void* paths, uint pathBase, int stride, float* metrics);
		unsafe delegate void glGetPathSpacing(int metricQueryMask, uint firstPathName, int numPaths, int stride, float* metrics);
		unsafe delegate void glGetPathColorGeniv(int pathListMode, int numPaths, int pathNameType, void* paths, uint pathBase, float advanceScale, float kerningScale, int transformType, float* returnedSpacing);
		unsafe delegate void glGetPathColorGenfv(int color, int pname, int* value);
		unsafe delegate void glGetPathTexGeniv(int color, int pname, float* value);
		unsafe delegate void glGetPathTexGenfv(int texCoordSet, int pname, int* value);
		unsafe delegate void glIsPointInFillPath(int texCoordSet, int pname, float* value);
		unsafe delegate void glIsPointInStrokePath(uint path, uint mask, float x, float y);
		unsafe delegate void glGetPathLength(uint path, float x, float y);
		unsafe delegate void glPointAlongPath(uint path, int startSegment, int numSegments);
		unsafe delegate void glPathStencilFunc(uint path, int startSegment, int numSegments, float distance, float* x, float* y, float* tangentX, float* tangentY);

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
		PathRendering Path;

		#endregion

	}

}


