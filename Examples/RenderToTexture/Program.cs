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
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Input;
using System.Windows.Forms;


namespace ArcEngine.Examples.RenderToTexture
{
	class Program : GameBase
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


		#region Initialization


		/// <summary>
		/// Loads contents
		/// </summary>
		public override void  LoadContent()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Frame Buffer example";

			// Matrices
			ModelViewMatrix = Matrix4.LookAt(new Vector3(0.0f, 0.0f, -2.5f), Vector3.Zero, Vector3.UnitY);
			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, aspectRatio, 0.1f, 20.0f);


			#region Shader


			#region Vertex Shader
			string vshader = @"
				#version 130

				uniform mat4 modelview_matrix;
				uniform mat4 projection_matrix;
				uniform mat4 mvp_matrix;

				in vec3 in_position;
				in vec3 in_normal;
				in vec3 in_tangent;
				in vec2 in_texcoord;

				out vec4 out_color;

				void main(void)
				{
					gl_Position = mvp_matrix * vec4(in_position, 1.0);
					
					out_color = modelview_matrix * vec4(in_normal, 1.0);
				}";
			#endregion

			#region Fragment Shader
			string fshader = @"
				#version 130

				in vec4 out_color;

				out vec4 frag_color;

				void main(void)
				{
					frag_color = out_color;
				}";
			#endregion

			Shader = new Shader();
			Shader.SetSource(ShaderType.VertexShader, vshader);
			Shader.SetSource(ShaderType.FragmentShader, fshader);
			Shader.Compile();
			Display.Shader = Shader;
			#endregion


			// Enable depth test writting
			Display.RenderState.DepthTest = true;

			// Frame buffer
			Buffer = new FrameBuffer(new Size(512, 512));

			// Texture to display
			Texture = new Texture2D("data/test.png");

			// Mesh
			Mesh = Mesh.CreateTrefoil(128, 32);


			#region Font

			SpriteBatch = new SpriteBatch();
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 12, FontStyle.Regular);

			#endregion
		}



		/// <summary>
		/// Unload
		/// </summary>
		public override void UnloadContent()
		{
			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Mesh != null)
				Mesh.Dispose();
			Mesh = null;
		}


		#endregion


		public override void Update(GameTime gameTime)
		{
			// Check if the Escape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

			// Rotation
			Yaw += 0.01f;
		}


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			Display.RenderState.ClearColor = Color.Black;
			Display.ClearBuffers();

			Rectangle rect = new Rectangle(1, 1, 100, 100);

			#region Render target
			// Bind the render buffer
			Buffer.Bind();
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();



			// Aplly a rotation
			Display.Shader = Shader;
			Shader.SetUniform("modelview_matrix", ModelViewMatrix);
			Shader.SetUniform("projection_matrix", ProjectionMatrix);
			Shader.SetUniform("mvp_matrix", Matrix4.CreateRotationY(Yaw) * ModelViewMatrix * ProjectionMatrix);
			Mesh.Draw();
/*
			SpriteBatch.Begin();
			SpriteBatch.Draw(Texture, new Vector2(100, 10), Color.White);

		//	Display.DrawRectangle(rect, Color.Red);
		//	Display.DrawCircle(new Point(100, 10), 25, Color.Red);


			// Draw only in the depth buffer
			Display.ColorMask(false, false, false, false);

	//		Display.FillEllipse(new Rectangle(25, 125, 200, 100), Color.Yellow);
	//		Display.DrawRectangle(new Rectangle(25, 125, 200, 100), Color.Red);
			
			Display.ColorMask(true, true, true, true);

			SpriteBatch.End();
*/ 
			Buffer.End();
			#endregion



			SpriteBatch.Begin();

			// Blit both buffer on the screen
			SpriteBatch.Draw(Buffer.ColorTexture, new Vector2(10, 50), Color.White);
			SpriteBatch.Draw(Buffer.DepthTexture, new Vector2(550, 50), Color.White);



			SpriteBatch.DrawString(Font, new Vector2(10, 10), Color.White, "Render target example");
			SpriteBatch.End();

		}



		#region Properties

		/// <summary>
		/// Rotation
		/// </summary>
		float Yaw;

		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;

		/// <summary>
		/// Frame buffer
		/// </summary>
		FrameBuffer Buffer;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Texture
		/// </summary>
		Texture2D Texture;

		
		/// <summary>
		/// Mesh to draw
		/// </summary>
		Mesh Mesh;


		/// <summary>
		///  font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Model view matrix
		/// </summary>
		Matrix4 ModelViewMatrix;


		/// <summary>
		/// Projection matrix
		/// </summary>
		Matrix4 ProjectionMatrix;

		#endregion
	}
}
