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
			Shader = new Shader();

			Font = Font2d.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);


			// Setup the shader
			Shader.LoadSource(ShaderType.VertexShader, "data/vertex.txt");
			Shader.LoadSource(ShaderType.FragmentShader, "data/fragment.txt");
			Shader.Compile();

			// Load the texture
			Texture = new Texture("data/checkerboard.png");
			Display.Texture = Texture;

		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			Shader.Dispose();
		}


		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the Excape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;
			Display.Texturing = true;


			if (Mouse.IsButtonDown(MouseButtons.Left))
			{
				Shader.Use(Shader);

				Shader.SetUniform("mouse", new float[2] { Mouse.Location.X, Mouse.Location.Y});

			}

			// Draw the texture
			Texture.Blit(Display.ViewPort, TextureLayout.Tile);


			Shader.Use(null);
			Font.DrawText(new Point(100, 100), "Press left mouse button to activate the shader");
		}



		#region Properties

		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;


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
