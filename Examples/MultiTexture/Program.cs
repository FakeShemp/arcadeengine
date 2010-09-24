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
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;


//
// Multitexture example.
// Thanks to OpenGL superbible 5th edition (http://www.starstonesoftware.com/OpenGL/) 
// for the shader source codes and the texture
//

namespace ArcEngine.Examples.MultiTexture
{
	/// <summary>
	/// Main game class
	/// </summary>
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
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.Pink;
			Display.RenderState.FrontFace = FrontFace.CounterClockWise;
			Display.RenderState.DepthTest = true;

			#region Textures


			#region Cube texture
			Display.TextureUnit = 0;
			CubeTexture = new TextureCubeMap();
			CubeTexture.WrapR = TextureWrapFilter.ClampToEdge;
			CubeTexture.WrapS = TextureWrapFilter.ClampToEdge;
			CubeTexture.WrapT = TextureWrapFilter.ClampToEdge;
			CubeTexture.MagFilter = TextureMagFilter.Linear;
			CubeTexture.MinFilter = TextureMinFilter.LinearMipmapLinear;

			// load the 6 faces
			CubeTexture.LoadImage(TextureCubeFace.NegativeX, "data/textures/neg_x.png");
			CubeTexture.LoadImage(TextureCubeFace.NegativeY, "data/textures/neg_y.png");
			CubeTexture.LoadImage(TextureCubeFace.NegativeZ, "data/textures/neg_z.png");
			CubeTexture.LoadImage(TextureCubeFace.PositiveX, "data/textures/pos_x.png");
			CubeTexture.LoadImage(TextureCubeFace.PositiveY, "data/textures/pos_y.png");
			CubeTexture.LoadImage(TextureCubeFace.PositiveZ, "data/textures/pos_z.png");
			CubeTexture.GenerateMipmap();
			#endregion

			#region Tarnish texture
			Display.TextureUnit = 1;
			TarnishTexture = new Texture2D("data/textures/tarnish.png");
			TarnishTexture.MagFilter = TextureMagFilter.Linear;
			TarnishTexture.MinFilter = TextureMinFilter.LinearMipmapLinear;
			TarnishTexture.HorizontalWrap = TextureWrapFilter.ClampToEdge;
			TarnishTexture.VerticalWrap = TextureWrapFilter.ClampToEdge;
			TarnishTexture.GenerateMipmap();
			#endregion

			#endregion


			#region Meshes

			// Generate the cube mesh
			Cube = Mesh.CreateCube(3.0f);

			// Generate the spehre
			Sphere = Mesh.CreateSphere(1.0f, 32);

			#endregion


			#region Shaders

			// Reflection shader
			Reflection = new Shader();
			Reflection.LoadSource(ShaderType.VertexShader, "data/shaders/reflection.vert");
			Reflection.LoadSource(ShaderType.FragmentShader, "data/shaders/reflection.frag");
			Reflection.Compile();


			// Skybox shader
			Skybox = new Shader();
			Skybox.LoadSource(ShaderType.VertexShader, "data/shaders/skybox.vert");
			Skybox.LoadSource(ShaderType.FragmentShader, "data/shaders/skybox.frag");
			Skybox.Compile();

			#endregion
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (TarnishTexture != null)
				TarnishTexture.Dispose();

			if (CubeTexture != null)
				CubeTexture.Dispose();

			if (Cube != null)
				Cube.Dispose();

			if (Sphere != null)
				Sphere.Dispose();

			if (Skybox != null)
				Skybox.Dispose();

			if (Reflection != null)
				Reflection.Dispose();
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



			#region Matrices
			Vector3 CameraPostion = new Vector3(0.0f, 0.0f, 0.0f);
			Vector3 target = Vector3.Add(CameraPostion, new Vector3(0.0f, 0.0f, -1.0f));

			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			Matrix4 ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);
			Matrix4 ModelViewMatrix = Matrix4.LookAt(CameraPostion, target, Vector3.UnitY);
			Matrix4 mvp = ModelViewMatrix * ProjectionMatrix;

			Matrix4 NormalMatrix = Matrix4.Transpose(Matrix4.Invert(ModelViewMatrix));
			Matrix4 m = Matrix4.CreateRotationX(0.0f);

			#endregion


			rot += 0.01f;

			// Draw sphere
			Display.Shader = Reflection;
			Display.Shader.SetUniform("mvpMatrix", Matrix4.CreateRotationY(rot) * Matrix4.CreateRotationX(-rot) * Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -4.0f)) * mvp);
			Display.Shader.SetUniform("mvMatrix", ModelViewMatrix);
			Display.Shader.SetUniform("NormalMatrix", NormalMatrix);
			Display.Shader.SetUniform("mInverseCamera", Matrix4.Invert(m));//ProjectionMatrix));
			Display.Shader.SetUniform("tarnishMap", 1);
			Display.Shader.SetUniform("cubeMap", 0);
			Display.RenderState.Culling = true;
			Sphere.Draw();
			Display.RenderState.Culling = false;


			// Draw skybox
			Display.Shader = Skybox;
			Display.Shader.SetUniform("mvpMatrix", Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -8.0f)) * mvp);
			Display.Shader.SetUniform("cubeMap", 0);
			Cube.Draw();

		}

		float rot = 0.0f;

		#region Properties


		/// <summary>
		/// Reflection
		/// </summary>
		Shader Reflection;


		/// <summary>
		/// Skybox
		/// </summary>
		Shader Skybox;


		/// <summary>
		/// Cube texture
		/// </summary>
		TextureCubeMap CubeTexture;


		/// <summary>
		/// Tarnish texture
		/// </summary>
		Texture2D TarnishTexture;


		/// <summary>
		/// Cube
		/// </summary>
		Mesh Cube;


		/// <summary>
		/// Sphere
		/// </summary>
		Mesh Sphere;

		#endregion

	}

}
