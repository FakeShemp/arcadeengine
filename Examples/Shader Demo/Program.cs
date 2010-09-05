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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;

namespace ArcEngine.Examples.ShaderDemo
{
	public class Program : GameBase
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


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Shader demo";
		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{

			Display.RenderState.DepthTest = true;

			// Something to display text
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 11, FontStyle.Regular);


			// Sprite batch
			Batch = new SpriteBatch();


			// Load the texture
			Texture = new Texture2D("data/checkerboard.png");


			// Setup the simple shader
			SimpleShader = new Shader();
			SimpleShader.LoadSource(ShaderType.VertexShader, "data/vertex.txt");
			SimpleShader.LoadSource(ShaderType.FragmentShader, "data/fragment.txt");
	//		SimpleShader.LoadSource(ShaderType.GeometryShader, "data/geometry.txt");
			SimpleShader.Compile();


			// Create a mesh
			Mesh = Mesh.CreateTorus(0.25f, 0.5f, 32, 32);
			

			// Matrices
			ModelViewMatrix = Matrix4.LookAt(new Vector3(0.0f, 0.0f, -2.5f), Vector3.Zero, Vector3.UnitY);
			float aspectRatio = (float) Display.ViewPort.Width / (float) Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 20.0f);


		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Mesh != null)
				Mesh.Dispose();
			Mesh = null;

			if (SimpleShader != null)
				SimpleShader.Dispose();
			SimpleShader = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;

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


			// Rotation
			Yaw += 0.01f;
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			Display.Shader = SimpleShader;
			Display.Texture = Texture;

			// Uniforms
			Matrix4 mvp = Matrix4.CreateRotationY(Yaw) * ModelViewMatrix * ProjectionMatrix;
			Display.Shader.SetUniform("modelview", mvp);
			Display.Shader.SetUniform("texture", 0);

			// Draw the mesh
			Mesh.Draw();

			// Some text
			Batch.Begin();
			Batch.DrawString(Font, new Point(5, 10), Color.White, "Press left mouse button to activate the lens shaders");
			Batch.DrawString(Font, new Point(5, 30), Color.White, "Press right mouse button to activate the geometry shaders");
			Batch.End();
		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		float Yaw;

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
		Shader SimpleShader;


		/// <summary>
		/// Mesh
		/// </summary>
		Mesh Mesh;


		/// <summary>
		/// Texture to use
		/// </summary>
		Texture2D Texture;


		/// <summary>
		/// Font to display text
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch Batch;


		#endregion

	}
}
