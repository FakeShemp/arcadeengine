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
using OpenTK;

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

			if (!PerPixelCollision.Init())
			{
				MessageBox.Show("GL_ARB_occlusion_query not found !", "Unsupported extension");
				Exit();
			}

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
			PerPixelCollision.Dispose();			
			
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

			Angle += 0.5f;
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

			// Draw the logo
			DrawLogo();

			// Draw the star
			if (PerPixelCollision.Count > 0)
				StarColor = Color.Red;
			else
				StarColor = Color.White;
			Display.DrawTexture(Star, new Point(Mouse.Location.X, Mouse.Location.Y), StarColor);

			#endregion


			#region Occlusion query

			PerPixelCollision.Begin(0.1f);

			DrawLogo();


			// Begin query
			PerPixelCollision.BeginQuery();
			Display.DrawTexture(Star, new Point(Mouse.Location.X, Mouse.Location.Y));
			PerPixelCollision.EndQuery();



			PerPixelCollision.End();

			#endregion



			// Some text
			Font.DrawText(new Point(10, 30), Color.Red, "Count {0}", PerPixelCollision.Count);
		}


		/// <summary>
		/// Draws the collision logo
		/// </summary>
		private void DrawLogo()
		{
			Display.PushMatrices();
			Display.ProjectionMatrix = Matrix4.CreateRotationZ((float)Math.PI * Angle / 180.0f) * Matrix4.CreateTranslation(400, 400, 0.0f) * Display.ProjectionMatrix;
			Display.DrawTexture(Logo, new Point(-200, -200));
			Display.PopMatrices();
		}



		#region Properties

		/// <summary>
		/// Texture
		/// </summary>
		Texture Logo;


		/// <summary>
		/// Texture
		/// </summary>
		Texture Star;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Star drawing color
		/// </summary>
		Color StarColor;


		/// <summary>
		/// Rotation angle of the logo
		/// </summary>
		float Angle;


		#endregion

	}
}
