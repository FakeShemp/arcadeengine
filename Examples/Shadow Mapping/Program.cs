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
			CreateGameWindow(new Size(640, 480));
			Window.Text = "Shadow mapping example";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.RenderState.DepthTest = true;
	


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

                out vec3 normal;
                out vec3 fragmentEye;

				void main(void)
				{
              		gl_Position = mvp_matrix * vec4(in_position, 1.0);
                    
                    normal = (modelview_matrix * vec4(in_normal, 0.0)).xyz;
	                fragmentEye = (modelview_matrix * vec4(in_position, 0.0)).xyz;
	                fragmentEye = -normalize(fragmentEye);

				}";
			#endregion

			#region Fragment Shader
			string fshader = @"
				#version 130

            //    uniform vec3 Light;

                in vec3 normal;
                in vec3 fragmentEye;

				out vec4 frag_color;

				void main(void)
				{
            //        float intensity = max(dot(normal, Light), 0.0);
            //        frag_color =  vec4(intensity, 0.0, 0.0, 1.0) ;


	                float diffuseIntensity;
	                float specularItensity;

	                vec3 light;

	                vec3 normal;
	                vec3 eye;
	
	                vec3 reflection;

	                light = normalize(vec3(5.0, 5.0, 10.0));

	                normal = normalize(normal);
	                eye = normalize(fragmentEye);

	                diffuseIntensity = clamp(max(dot(normal, light), 0.0), 0.0, 1.0);
	
	                reflection = normalize(reflect(-light, normal));
	                specularItensity = pow(clamp(max(dot(reflection, eye), 0.0), 0.0, 1.0), 20.0 );

	                frag_color = vec4(0.0, 0.0, 0.0, 1.0) + 
				                vec4(0.1, 0.1, 0.1, 1.0) + 
				                vec4(1.0, 0.0, 0.0, 1.0)*diffuseIntensity + 
				                vec4(1.0, 0.9, 0.9, 1.0)*specularItensity;

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


			// Frame buffer
			FrameBuffer = new FrameBuffer(new Size(512, 512));


			Camera = new Vector3(-5.0f, 5.0f, 10.0f);
			Light = new Vector3(0.0f, 0.0f, 1.0f);

			
			// Matrices
			ModelViewMatrix = Matrix4.LookAt(Camera, Vector3.Zero, Vector3.UnitY);
			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(40.0f), aspectRatio, 1.0f, 100.0f);

			// Rotation
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

			if (FrameBuffer != null)
				FrameBuffer.Dispose();
			FrameBuffer = null;
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


			Yaw += 0.01f;
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
            Matrix4 mvp = ModelViewMatrix * ProjectionMatrix;
			Shader.SetUniform("modelview_matrix", ModelViewMatrix);
            Shader.SetUniform("projection_matrix", ProjectionMatrix);
            Shader.SetUniform("normal_matrix", Matrix4.Transpose(Matrix4.Invert((mvp))));
            Shader.SetUniform("Light", Light);


			Vector3 Position = new Vector3(0.0f, 0.0f, -5.0f);
			Shader.SetUniform("mvp_matrix", Matrix4.CreateTranslation(Position) * mvp);
			Plane.Draw();

			Shader.SetUniform("mvp_matrix", Matrix4.CreateRotationY(Yaw) * mvp);
			Torus.Draw();


			// Some dummy text
			SpriteBatch.Begin();
			SpriteBatch.DrawString(Font, new Vector2(10, 15), Color.White, "Here's an example of shadow mapping.");
			SpriteBatch.End();
		}



		#region Properties


		/// <summary>
		/// Camera position
		/// </summary>
		Vector3 Camera;


		/// <summary>
		/// Light position
		/// </summary>
		Vector3 Light;


		/// <summary>
		/// Frame buffer
		/// </summary>
		FrameBuffer FrameBuffer;


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
