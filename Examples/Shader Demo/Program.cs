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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK.Graphics.OpenGL;

namespace Shader_Demo
{
	public class ShaderProject : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (ShaderProject game = new ShaderProject())
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
		public ShaderProject()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Shader demo";
			Window.Resizable = true;
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{

			// Something to display text
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);


			// Load the texture
			Texture = new Texture("data/checkerboard.png");
			Display.Texture = Texture;


			// Setup the simple shader
			SimpleShader = new Shader();
			SimpleShader.LoadSource(ShaderType.VertexShader, "data/vertex.txt");
			SimpleShader.LoadSource(ShaderType.FragmentShader, "data/fragment.txt");
			SimpleShader.Compile();
			mouseID = SimpleShader.GetUniform("mouse");
			SimpleShader.SetUniform(SimpleShader.GetUniform("texture"), 0);

			// Setup the geometry shader
			GeomShader = Shader.CreateColorShader();
			GeomShader.LoadSource(ShaderType.GeometryShader, "data/geometry.txt");
			GeomShader.SetGeometryPrimitives(BeginMode.Lines, BeginMode.LineStrip, 50);
			GeomShader.Compile();


		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (SimpleShader != null)
				SimpleShader.Dispose();
			SimpleShader = null;

			if (GeomShader != null)
				GeomShader.Dispose();
			GeomShader = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;
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

			// Simple shader
			if (Mouse.IsButtonDown(MouseButtons.Left))
			{
				Display.Shader = SimpleShader;
				SimpleShader.SetUniform(mouseID, new float[2] { Mouse.Location.X, Display.ViewPort.Bottom - Mouse.Location.Y });
			}

			// Draw the texture
			Texture.Blit(Display.ViewPort, TextureLayout.Tile);

			// No shader
		//	Display.Shader = null;

			// Geometry shader
			if (Mouse.IsButtonDown(MouseButtons.Right))
				Display.Shader = GeomShader;
			Display.DrawLine(500, 200, 500, 300, Color.White);
			Display.DrawRectangle(new Rectangle(500, 350, 100, 50), Color.Red);
			Display.DrawEllipse(new Rectangle(500, 450, 100, 50), Color.Teal);

			// No shader
		//	Display.Shader = null;


			Font.DrawText(new Point(25, 50), Color.White, "Press left mouse button to activate the lens shaders");
			Font.DrawText(new Point(25, 70), Color.White, "Press right mouse button to activate the geometry shaders");
		}



		#region Properties

		/// <summary>
		/// Shader
		/// </summary>
		Shader SimpleShader;


		/// <summary>
		/// Geometry shader
		/// </summary>
		Shader GeomShader;


		/// <summary>
		/// Texture to use
		/// </summary>
		Texture Texture;


		/// <summary>
		/// Font to display text
		/// </summary>
		BitmapFont Font;

		/// <summary>
		/// ID of the uniform value
		/// </summary>
		int mouseID;



		#endregion

	}
}
