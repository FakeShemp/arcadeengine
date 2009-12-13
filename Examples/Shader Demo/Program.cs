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
	public class ShaderProject : Game
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

			Font = Font2d.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);


			// Setup the simple shader
			SimpleShader = new Shader();
			SimpleShader.LoadSource(ShaderType.VertexShader, "data/vertex.txt");
			SimpleShader.LoadSource(ShaderType.FragmentShader, "data/fragment.txt");
			SimpleShader.Compile();


			// Setup the geometry shader
			GeomShader = new Shader(@"
				void main( void )
				{
					gl_FrontColor = gl_Color;
					gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
				}	
			",
			@"
				void main( void )
				{
					gl_FragColor = gl_Color; // vec4(1, 0, 1, 1);
				}	
			");

			GeomShader.LoadSource(ShaderType.GeometryShader, "data/geometry.txt");
			GeomShader.SetGeometryPrimitives(BeginMode.Lines, BeginMode.LineStrip, 50);
			GeomShader.Compile();


			// Load the texture
			Texture = new Texture("data/checkerboard.png");
			Display.Texture = Texture;

		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			SimpleShader.Dispose();
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
			Display.Color = Color.White;

			// Simple shader
			if (Mouse.IsButtonDown(MouseButtons.Left))
			{
				Shader.Use(SimpleShader);
				SimpleShader.SetUniform("mouse", new float[2] { Mouse.Location.X, Display.ViewPort.Bottom - Mouse.Location.Y});
			}

			// Draw the texture
			Texture.Blit(Display.ViewPort, TextureLayout.Tile);

			Shader.Use(null);

			// Geometry shader
			if (Mouse.IsButtonDown(MouseButtons.Right))
				Shader.Use(GeomShader);
			Display.DrawLine(500, 200, 500, 300, Color.White);
			Display.DrawRectangle(new Rectangle(500, 350, 100, 50), Color.Red);


			Shader.Use(null);
			Font.DrawText(new Point(25, 50), Color.White, "Press left / right mouse button to activate the shaders");
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
		Font2d Font;


		#endregion

	}
}
