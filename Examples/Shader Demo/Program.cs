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


			Texture = new Texture("data/3dlabs.png");
			Display.Texture = Texture;
			Display.Culling = true;


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

				float[] amplitude = new float[] { 0.05f, 0.05f };
				float[] frequence = new float[] { 4.0f, 4.0f };
				Shader.SetUniform("StartRad", 11.0f);
				Shader.SetUniform("Amplitude", amplitude);
				Shader.SetUniform("WobbleTex", 0);
				Shader.SetUniform("Freq", 0.1f);

				//float[] firecolor1 = new float[] { 0.8f, 0.7f, 0.0f, 1.0f };
				//float[] firecolor2 = new float[] { 0.6f, 0.1f, 0.0f, 1.0f };
				//float[] offset = new float[] { 0.0f, -1.0f };
				//Shader.SetUniform("FireColor1", firecolor1);
				//Shader.SetUniform("FireColor2", firecolor2);
				//Shader.SetUniform("Offset", offset);
				//Shader.SetUniform("sampler3d", 0);
				//Shader.SetUniform("Scale", 0.6f);
				//Shader.SetUniform("Extent", 0.6f);

				//float[] lightpos= new float[] { 0.0f, 0.0f, 4.0f};
				//float[] zoom = new float[] { 0.0f, 0.1f};
				//float[] innercolor = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
				//float[] outercolor1 = new float[] { 0.5f, 0.0f, 1.5f, 1.0f };
				//float[] outercolor2 = new float[] { 0.0f, 1.5f, 0.0f, 1.0f };
				//Shader.SetUniform("LightPosition", lightpos);
				//Shader.SetUniform("DiffuseContribution", 0.8f);
				//Shader.SetUniform("SpecularContribution", 0.2f);
				//Shader.SetUniform("Shininess", 16.0f);
				//Shader.SetUniform("MaxIterations", 50.0f);
				//Shader.SetUniform("Xcenter", -0.75f);
				//Shader.SetUniform("Ycenter", -0.0f);
				//Shader.SetUniform("Zoom", zoom);
				//Shader.SetUniform("InnerColor", innercolor);
				//Shader.SetUniform("OuterColor1",outercolor1);
				//Shader.SetUniform("OuterColor2", outercolor2);

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
			Display.Texturing = true;

			Display.Rectangle(new Rectangle(10, 10, 600, 600), true);
		//	Texture.Blit(Point.Empty);

		}




		#region Properties

		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;


		/// <summary>
		/// 
		/// </summary>
		Texture Texture;

		#endregion

	}
}
