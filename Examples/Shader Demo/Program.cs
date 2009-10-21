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
			CreateGameWindow(new Size(800, 600));
			Window.Text = "Shader demo";
			Window.Resizable = true;


		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Shader = new Shader();


			Shader.LoadSource(ShaderType.VertexShader, "data/vertex.txt");
			Shader.LoadSource(ShaderType.FragmentShader, "data/fragment.txt");


			Shader.Compile();


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


			if (Keyboard.IsNewKeyPress(Keys.F1))
			{
				Shader.Use(Shader);

				float[] brick_colour = new float[] { 1, 0.3f, 0.2f };
				float[] mortar_colour = new float[] { 0.85f, 0.86f, 0.84f };
				float[] brick_size = new float[] { 0.3f, 0.15f };
				float[] brick_pct = new float[] { 0.9f, 0.85f };
				float[] light_pos = new float[] { 0.0f, 0.0f, 4.3f };

				Shader.SetUniform("BrickColor", brick_colour);
				Shader.SetUniform("MortarColor", mortar_colour);
				Shader.SetUniform("BrickSize", brick_size);
				Shader.SetUniform("BrickPct", brick_pct);
				Shader.SetUniform("LightPosition", light_pos);
				Shader.SetUniform("Toto", 1.0f);


			}
			if (Keyboard.IsNewKeyPress(Keys.F2))
				Shader.Use(null);





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


			Display.Rectangle(new Rectangle(10, 10, 200, 200), true);

			Display.Line(new Point(400, 100), new Point(500, 133));
		}




		#region Properties

		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;

		#endregion

	}
}
