#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using ArcEngine.Graphic;

// http://kometbomb.net/2008/07/23/collision-detection-with-occlusion-queries-redux/
// http://blogs.msdn.com/b/shawnhar/archive/2008/12/31/pixel-perfect-collision-detection-using-gpu-occlusion-queries.aspx

namespace ArcEngine.Utility
{
	/// <summary>
	/// Per pixel perfect collision class
	/// </summary>
	static public class PixelCollision
	{
		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns>True if everything went OK</returns>
		static public bool Init()
		{
			if (QueryID != -1)
				return true;


			// Check for extension availability
			if (!Display.Capabilities.Extensions.Contains("GL_ARB_occlusion_query"))
			{
				Trace.WriteLine("[PixelCollision] Init() : GL_ARB_occlusion_query not found !");
				return false;
			}

			// Query
			OpenTK.Graphics.OpenGL.GL.GenQueries(1, out QueryID);

			return true;
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public static void Dispose()
		{
			if (QueryID != -1)
				OpenTK.Graphics.OpenGL.GL.DeleteQueries(1, ref QueryID);
			QueryID = -1;
		}


		/// <summary>
		/// Begin the blue print mode
		/// </summary>
		/// <param name="treshold">Alpha test treshold (0 super sensible, 1 no collision)</param>
		static public void Begin(float treshold)
		{
			// Disable writing to the color buffer
			Display.ColorMask(false, false, false, false);

			// Activate stencil buffer
			Display.RenderState.StencilTest = true;
			Display.StencilFunction(StencilFunction.Always, 1, 1);
			//GL.StencilFunc(StencilFunction.Always, 1, 1);

			Display.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
			//GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);

			// Activate alpha test
			Display.RenderState.AlphaTest = true;
			Display.AlphaFunction(AlphaFunction.Greater, treshold);
		}


		/// <summary>
		/// End the blue print mode
		/// </summary>
		static public void End()
		{
			// Deactivate stencil buffer and write to the color buffer
			Display.RenderState.StencilTest = false;
			Display.RenderState.AlphaTest = false;
			Display.ColorMask(true, true, true, true);
		}


		/// <summary>
		/// Begin the test mode
		/// </summary>
		static public void BeginQuery()
		{
			// End blue print
			//GL.StencilFunc(StencilFunction.Equal, 1, 1);
			Display.StencilFunction(StencilFunction.Equal, 1, 1);

			//GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
			Display.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);

			//
			OpenTK.Graphics.OpenGL.GL.BeginQuery(OpenTK.Graphics.OpenGL.QueryTarget.SamplesPassed, QueryID);
		}


		/// <summary>
		/// end the test mode
		/// </summary>
		static public void EndQuery()
		{
			OpenTK.Graphics.OpenGL.GL.EndQuery(OpenTK.Graphics.OpenGL.QueryTarget.SamplesPassed);
		}


		/// <summary>
		/// Number of colliding fragment
		/// </summary>
		static public int Count
		{
			get
			{
				int count = 0;
				OpenTK.Graphics.OpenGL.GL.GetQueryObject(QueryID, OpenTK.Graphics.OpenGL.GetQueryObjectParam.QueryResult, out count);
				return count;
			}
		}



		/// <summary>
		/// Query ID
		/// </summary>
		static int QueryID = -1;



	}
}
