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


// http://www.planet-dev.com/developpement/jeux-video/cel-shading-en-glsl
// http://prideout.net/blog/?p=22

namespace ArcEngine.Examples.CellShading
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
			GameWindowParams param = new GameWindowParams();
			param.Size = new Size(1024, 768);
			param.Major = 3;
			param.Minor = 0;
			CreateGameWindow(param);
			Window.Text = "Cell Shading example from http://prideout.net/blog/?p=22";
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
			float aspectRatio = (float) Display.ViewPort.Width / (float) Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 20.0f);


			#region Shader

			#region Vertex Shader
			string vshader = @"
				#version 130

				uniform mat4 modelview;
				uniform vec3 DiffuseMaterial;

				in vec3 in_position;
				in vec3 in_normal;

				out vec3 EyespaceNormal;
				out vec3 Diffuse;

				void main(void)
				{
					gl_Position = modelview * vec4(in_position, 1.0);

					Diffuse = DiffuseMaterial;

					mat3 NormalMatrix = mat3(modelview);
					EyespaceNormal = NormalMatrix * in_normal;
				}";
			#endregion

			#region Fragment Shader
			string fshader = @"
				#version 130

				uniform vec3 LightPosition;
				uniform vec3 AmbientMaterial;
				uniform vec3 SpecularMaterial;
				uniform float Shininess;

				in vec3 EyespaceNormal;
				in vec3 Diffuse;

				out vec4 frag_color;


				float stepmix(float edge0, float edge1, float E, float x)
				{
					 float T = clamp(0.5 * (x - edge0 + E) / E, 0.0, 1.0);
					 return mix(edge0, edge1, T);
				}


				void main(void)
				{
					vec3 N = normalize(EyespaceNormal);
					vec3 L = normalize(LightPosition);
					vec3 Eye = vec3(0, 0, 1);
					vec3 H = normalize(L + Eye);
    
					float df = max(0.0, dot(N, L));				// Diffuse factor
					float sf = max(0.0, dot(N, H));				// Specular factor
					sf = pow(sf, Shininess);

					const float A = 0.1;			// gradient regions
					const float B = 0.3;		
					const float C = 0.6;		
					const float D = 1.0;		
					float E = fwidth(df);		// Epsilon to smooth edges

					if      (df > A - E && df < A + E) df = stepmix(A, B, E, df);
					else if (df > B - E && df < B + E) df = stepmix(B, C, E, df);
					else if (df > C - E && df < C + E) df = stepmix(C, D, E, df);
					else if (df < A) df = 0.0;
					else if (df < B) df = B;
					else if (df < C) df = C;
					else		df = D;

					E = fwidth(sf);
					if (sf > 0.5 - E && sf < 0.5 + E)
					{
						sf = smoothstep(0.5 - E, 0.5 + E, sf);
					}
					else
					{
						sf = step(0.5, sf);
					}

					vec3 color = AmbientMaterial + df * Diffuse + sf * SpecularMaterial;
					frag_color = vec4(color, 1.0);
				}";
			#endregion

			Shader = new Shader();
			Shader.SetSource(ShaderType.VertexShader, vshader);
			Shader.SetSource(ShaderType.FragmentShader, fshader);
			Shader.Compile();
			Display.Shader = Shader;
			#endregion


			#region Mesh

			Trefoil = Mesh.CreateTrefoil(128, 32);
			//Torus = Mesh.CreateTorus(0.25f, 0.5f, 32, 32);
			Torus = Mesh.CreateDisk(0.25f, 1.0f, 32, 32);

			#endregion


			#region Font

			Sprite = new SpriteBatch();
			Font = BitmapFont.CreateFromTTF("c:\\windows\\fonts\\verdana.ttf", 16, FontStyle.Regular);

			#endregion
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (Sprite != null)
				Sprite.Dispose();
			Sprite = null;

			if (Trefoil != null)
				Trefoil.Dispose();
			Trefoil = null;

			if (Torus != null)
				Torus.Dispose();
			Torus = null;
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

			// Swap the mesh
			if (Keyboard.IsNewKeyPress(Keys.Space))
				swap = !swap;

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

			// Aplly a rotation
			Matrix4 mvp = Matrix4.CreateRotationY(Yaw) * ModelViewMatrix * ProjectionMatrix;

			Display.Shader = Shader;

			// Uniforms
			Shader.SetUniform("modelview", mvp);
			Shader.SetUniform("DiffuseMaterial", new float[] { 0.0f, 0.75f, 0.75f });
			Shader.SetUniform("LightPosition", new float[] { 0.25f, 0.25f, -1.0f });
			Shader.SetUniform("AmbientMaterial", new float[] { 0.04f, 0.04f, 0.04f });
			Shader.SetUniform("SpecularMaterial", new float[] { 0.5f, 0.5f, 0.5f });
			Shader.SetUniform("Shininess", 50.0f);

			if (swap)
				Torus.Draw();
			else
				Trefoil.Draw();

			// Some dummy text
			Sprite.Begin();
			Sprite.DrawString(Font, new Vector2(50, 25), Color.White, "Here's an example of draw buffers.");
			Sprite.DrawString(Font, new Vector2(50, 50), Color.White, "Press space key to  swap mesh.");
			Sprite.End();

		}


		#region Properties

		/// <summary>
		/// Rotation
		/// </summary>
		float Yaw;


		/// <summary>
		/// Swap between to meshes
		/// </summary>
		bool swap;


		/// <summary>
		/// Trefoil
		/// </summary>
		Mesh Trefoil;


		/// <summary>
		/// Torus
		/// </summary>
		Mesh Torus;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Sprite batch
		/// </summary>
		SpriteBatch Sprite;


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

		#endregion

	}
}
