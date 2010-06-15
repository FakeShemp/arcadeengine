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
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Asset;
using OpenTK.Graphics.OpenGL;


// http://kometbomb.net/2008/07/23/collision-detection-with-occlusion-queries-redux/
// http://blogs.msdn.com/b/shawnhar/archive/2008/12/31/pixel-perfect-collision-detection-using-gpu-occlusion-queries.aspx

namespace ArcEngine.Examples.PerPixelCollision
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
			try
			{
				using (Program game = new Program())
					game.Run();
			}
			catch (Exception e)
			{
				// Oops, an error happened !
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			// Create the game window
			CreateGameWindow(new Size(1024, 768));

			// Change the window title
			Window.Text = "Per pixel perfect collision test";

		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Clear color of the screen
			Display.ClearColor = Color.LightGray;
			Mouse.Visible = false;


			// Check for availability
			if (!Display.Capabilities.Extensions.Contains("GL_ARB_occlusion_query"))
			{
				MessageBox.Show("GL_ARB_occlusion_query not found !", "Unsupported extension");
				Exit();
			}

			// Query
			GL.GenQueries(1, out QueryID);


			// Textures
			Logo = new Texture("data/logo.png");
			Star = new Texture("data/star.png");

			// Font
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			GL.DeleteQueries(1, ref QueryID);
			QueryID = -1;
			
			
			if (Logo != null)
				Logo.Dispose();

			if (Star != null)
				Star.Dispose();

			if (Font != null)
				Font.Dispose();
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
			Display.DefaultMatrix();


			#region First draw

			// Draw logo
			Display.DrawTexture(Logo, new Point(200, 200));

			// Draw the star
			Display.DrawTexture(Star, new Point(Mouse.Location.X, Mouse.Location.Y));

			#endregion


			#region Occlusion query

			// Disable writing to the color buffer
			Display.ColorMask(false, false, false, true);

			// Activate stencil buffer
			Display.StencilTest = true;
			Display.AlphaTest = true;

			// Draw the blue print
			GL.StencilFunc(StencilFunction.Always, 1, 1);
			GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);

			Display.DrawTexture(Logo, new Point(200, 200));


			// Begin the query
			GL.StencilFunc(StencilFunction.Equal, 1, 1);
			GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);


			GL.BeginQuery(QueryTarget.SamplesPassed, QueryID);
			Display.DrawTexture(Star, new Point(Mouse.Location.X, Mouse.Location.Y));
			GL.EndQuery(QueryTarget.SamplesPassed);

			GL.GetQueryObject(QueryID, GetQueryObjectParam.QueryResult, out Count);

			// Deactivate stencil buffer
			Display.StencilTest = false;

			// Enable writing to the color buffer
			Display.ColorMask(true, true, true, true);
			#endregion



			// Some text
			Font.DrawText(new Point(10, 30), Color.Red, "Count {0}", Count);
		}




		#region Properties

		/// <summary>
		/// 
		/// </summary>
		Texture Logo;


		/// <summary>
		/// 
		/// </summary>
		Texture Star;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// 
		/// </summary>
		int QueryID;


		/// <summary>
		/// 
		/// </summary>
		int Count;

		#endregion

	}
}
