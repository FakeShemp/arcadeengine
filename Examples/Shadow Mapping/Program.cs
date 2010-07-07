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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
//using OpenTK;
//using OpenTK.Graphics.OpenGL;
using System.Xml;


namespace ArcEngine.Examples.ShadowMapping
{
	class Program : GameBase
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
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Shadow mapping example";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.RenderState.DepthTest = true;
	
			
			// Matrices
			ModelViewMatrix = Matrix4.LookAt(new Vector3(0.0f, 0.0f, -2.5f), Vector3.Zero, Vector3.UnitY);
			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2.0f, aspectRatio, 0.1f, 20.0f);


			#region Mesh
			//Mesh = PlyLoader.LoadPly("data/bunny.ply");
			Torus = Mesh.CreateTorus(0.5f, 1.0f, 32, 32);
			Plane = Mesh.CreatePlane(10.0f);
			#endregion


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
			#endregion


			#region Font

			SpriteBatch = new SpriteBatch();
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 10, FontStyle.Regular);

			#endregion

			Yaw = 0.0f;
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Plane != null)
				Plane.Dispose();
			Plane = null;

			if (Torus != null)
				Torus.Dispose();
			Torus = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;
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

			if (Keyboard.IsKeyPress(Keys.Left))
				Plane.Position.X += 0.01f;

			if (Keyboard.IsKeyPress(Keys.Right))
				Plane.Position.X -= 0.01f;


			Yaw += 0.005f;
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			Display.Shader = Shader;


			// Draws with the index buffer
			Shader.SetUniform("modelview_matrix", ModelViewMatrix);
			Shader.SetUniform("projection_matrix", ProjectionMatrix);
			Shader.SetUniform("mvp_matrix", ModelViewMatrix * ProjectionMatrix);
			//Plane.Draw();

			Shader.SetUniform("mvp_matrix", Matrix4.CreateRotationY(Yaw) * ModelViewMatrix * ProjectionMatrix);
			Torus.Draw();


			// Some dummy text
			SpriteBatch.Begin();
			SpriteBatch.DrawString(Font, new Vector2(10, 15), Color.White, "Here's an example of shadow mapping.");
			SpriteBatch.End();
		}



		#region Properties

		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Mesh to draw
		/// </summary>
		Mesh Plane;


		/// <summary>
		/// Torus
		/// </summary>
		Mesh Torus;

		/// <summary>
		/// Model view matrix
		/// </summary>
		Matrix4 ModelViewMatrix;


		/// <summary>
		/// Projection matrix
		/// </summary>
		Matrix4 ProjectionMatrix;


		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;


		/// <summary>
		/// Rotation angle
		/// </summary>
		float Yaw;


		#endregion


	}
}
