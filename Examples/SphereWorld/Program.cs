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
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Asset;

namespace ArcEngine.Examples.SphereWorld
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Example : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (Example game = new Example())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Example()
		{
			CreateGameWindow(new Size(1024, 768));
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.RenderState.DepthTest = true;


			Batch = new Graphic.SpriteBatch();
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 11, FontStyle.Regular);
					
			
			#region Matrices

			CameraPostion = new Vector3(0.0f, 1.0f, 7.0f);

			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);

			#endregion


			#region Shader

			#region Vertex Shader
			string vshader = @"
				#version 130

				uniform mat4 mvpMatrix;

				in vec4 in_position;
				in vec2 in_texcoord;

				out vec2 vTex;

				void main(void) 
				{ 
					vTex = in_texcoord;
					gl_Position = mvpMatrix * in_position; 
				}";

			#endregion

			#region Fragment Shader
			string fshader = @"
				#version 130

				in vec2 vTex;

				uniform sampler2D textureUnit0;

				out vec4 frag_color;

				void main(void) 
				{
					frag_color = texture2D(textureUnit0, vTex); 
				}";

			#endregion

			Shader = new Shader();
			Shader.SetSource(ShaderType.VertexShader, vshader);
			Shader.SetSource(ShaderType.FragmentShader, fshader);
			Shader.Compile();
			Display.Shader = Shader;
			#endregion


			Marble = new Texture2D("data/Marble.png");
			Marble.MagFilter = TextureMagFilter.Linear;
			Marble.MinFilter = TextureMinFilter.Linear;

			Moon = new Texture2D("data/Moon.png");
			Mars = new Texture2D("data/Mars.png");

			Torus = Mesh.CreateTorus(0.15f, 0.40f, 40, 20);
			Sphere = Mesh.ggCreateSphere(0.1f, 26);
			float[] data = new float[]
			{
				-10.0f, -0.41f,  20.0f,		0.0f,     0.0f,
				 10.0f, -0.41f,  20.0f,		0.0f,   128.0f,
				 10.0f, -0.41f, -20.0f,		256.0f, 128.0f,
				-10.0f, -0.41f, -20.0f,		256.0f,   0.0f,
			};
			Floor = new Mesh();
			Floor.SetVertices(data);
			Floor.SetIndices(new int[] { 0, 1, 2, 0, 2, 3});
			Floor.Buffer.AddDeclaration("in_position", 3);
			Floor.Buffer.AddDeclaration("in_texture", 2);
			Floor.PrimitiveType = PrimitiveType.Triangles;
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Torus != null)
				Torus.Dispose();
			Torus = null;

			if (Sphere != null)
				Sphere.Dispose();
			Sphere = null;

			if (Floor != null)
				Floor.Dispose();
			Floor = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (Moon != null)
				Moon.Dispose();
			Moon = null;

			if (Mars != null)
				Mars.Dispose();
			Mars = null;

			if (Marble != null)
				Marble.Dispose();
			Marble = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

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

			if (Keyboard.IsKeyPress(Keys.Down))
				CameraPostion = Vector3.Add(CameraPostion, new Vector3(0.0f, 0.0f, CameraSpeed));

			if (Keyboard.IsKeyPress(Keys.Up))
				CameraPostion = Vector3.Add(CameraPostion, new Vector3(0.0f, 0.0f, -CameraSpeed));


			if (Keyboard.IsKeyPress(Keys.Left))
				CameraPostion = Vector3.Add(CameraPostion, new Vector3(-CameraSpeed, 0.0f, 0.0f));

			if (Keyboard.IsKeyPress(Keys.Right))
				CameraPostion = Vector3.Add(CameraPostion, new Vector3(CameraSpeed, 0.0f, 0.0f));

			// Update camera view
			Matrix4 cameraRotation = Matrix4.CreateRotationX(Mouse.MoveDelta.X) * Matrix4.CreateRotationY(Mouse.MoveDelta.Y);

	
			// Center mouse
	//		Mouse.Location = new Point(Window.Size.Width / 2, Window.Size.Height / 2);

			// http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series4/Mouse_camera.php
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			// ModelViewProjection matrix
			Vector3 target = Vector3.Add(CameraPostion, new Vector3(0.0f, 0.0f, -1.0f));
			ModelViewMatrix = Matrix4.LookAt(CameraPostion, target, Vector3.UnitY);
			Matrix4 mvp = ModelViewMatrix * ProjectionMatrix;

			Display.Shader = Shader;
			Shader.SetUniform("mvpMatrix", mvp);
			Shader.SetUniform("textureUnit0", 0);

			Display.Texture = Moon;
			Torus.Draw();

			Display.Texture = Marble;
			Sphere.Draw();

			Display.Texture = Mars;
			Floor.Draw();


			Batch.Begin();
			Batch.DrawString(Font, new Vector2(10, 50), Color.White, "Camera position : {0}", CameraPostion.ToString());
			Batch.DrawString(Font, new Vector2(10, 70), Color.White, "Camera rotation : {0}", CameraRotation.ToString());
			Batch.DrawString(Font, new Vector2(10, 90), Color.White, "Camera target : {0}", target.ToString());
			Batch.End();
		}



		private void UpdateViewMatrix()
		{
			float updownRot = 0.0f;
			float leftrightRot = 0.0f;
			Matrix4 cameraRotation = Matrix4.CreateRotationX(updownRot) * Matrix4.CreateRotationY(leftrightRot);

			Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
			Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
			Vector3 cameraFinalTarget = CameraPostion + cameraRotatedTarget;

			Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);
			Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

			Matrix4 viewMatrix = Matrix4.LookAt(CameraPostion, cameraFinalTarget, cameraRotatedUpVector);
		}


		#region Properties


		/// <summary>
		/// Camera rotation speed
		/// </summary>
		const float RotationSpeed = 0.3f;


		/// <summary>
		/// Camera move speed
		/// </summary>
		const float CameraSpeed = 0.1f;


		/// <summary>
		/// Position of the camera
		/// </summary>
		Vector3 CameraPostion;

		/// <summary>
		/// 
		/// </summary>
		Vector2 CameraRotation;


		/// <summary>
		/// 
		/// </summary>
		Mesh Torus;


		/// <summary>
		/// 
		/// </summary>
		Mesh Sphere;


		/// <summary>
		/// 
		/// </summary>
		Mesh Floor;


		/// <summary>
		/// 
		/// </summary>
		Shader Shader;


		/// <summary>
		/// Model view matrix
		/// </summary>
		Matrix4 ModelViewMatrix;


		/// <summary>
		/// Projection matrix
		/// </summary>
		Matrix4 ProjectionMatrix;


		/// <summary>
		/// 
		/// </summary>
		Texture2D Marble;


		/// <summary>
		/// 
		/// </summary>
		Texture2D Mars;


		/// <summary>
		/// 
		/// </summary>
		Texture2D Moon;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;
		

		/// <summary>
		/// 
		/// </summary>
		SpriteBatch Batch;

		#endregion

	}

}
