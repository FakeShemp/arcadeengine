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
using OpenTK;
using OpenTK.Graphics.OpenGL;


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
			Window.Text = "Cell Shading example from http://prideout.net/blog/?p=22";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;
			Display.DepthTest = true;
			Display.ViewPerspective((float)Math.PI / 4.0f, 0.1f, 20.0f);
			ModelViewMatrix = Matrix4.LookAt(new Vector3(0.0f, 0.0f, -2.5f), Vector3.Zero, Vector3.UnitY);
			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, aspectRatio, 0.1f, 20.0f);

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


			Display.Shader.SetSource(ShaderType.VertexShader, vshader);
			Display.Shader.SetSource(ShaderType.FragmentShader, fshader);
			Display.Shader.Compile();
			#endregion


			#region Buffer

			// Creates vertex buffer
			Buffer = new BatchBuffer();
			Buffer.AddDeclaration("in_position", 3);
			Buffer.AddDeclaration("in_normal", 3);

			int Slices = 128;
			int Stacks = 32;
			float ds = 1.0f / Slices;
			float dt = 1.0f / Stacks;
			int VertexCount = Slices * Stacks;

			float[] buffer = new float[VertexCount * 6];
			int pos = 0;

			for (float s = 0; s < 1 - ds / 2; s += ds)
			{
				for (float t = 0; t < 1 - dt / 2; t += dt)
				{
					const float E = 0.01f;
					Vector3 p = EvaluateTrefoil(s, t);
					Vector3 u = EvaluateTrefoil(s + E, t) - p;
					Vector3 v = EvaluateTrefoil(s, t + E) - p;
					Vector3 n = Vector3.Normalize(Vector3.Cross(u, v));

					// Position
					buffer[pos++] = p.X;
					buffer[pos++] = p.Y;
					buffer[pos++] = p.Z;

					// Normal
					buffer[pos++] = n.X;
					buffer[pos++] = n.Y;
					buffer[pos++] = n.Z;
				}
			}

			Buffer.SetVertices(buffer);

			#endregion


			#region Index

			Index = new IndexBuffer();

			int[] indices = new int[Slices * Stacks * 6];
			pos = 0;
			int m = 0;
			for (int i = 0; i < Slices; i++)
			{
				for (int j = 0; j < Stacks; j++)
				{
					indices[pos++] = m + j;
					indices[pos++] = m + (j + 1) % Stacks;
					indices[pos++] = (m + j + Stacks) % VertexCount;

					indices[pos++] = (m + j + Stacks) % VertexCount;
					indices[pos++] = (m + (j + 1) % Stacks) % VertexCount;
					indices[pos++] = (m + (j + 1) % Stacks + Stacks) % VertexCount;
				}
				m += Stacks;
			}
			Index.Update(indices);



			#endregion


			#region Font

			Font = BitmapFont.CreateFromTTF("c:\\windows\\fonts\\verdana.ttf", 16, FontStyle.Regular);

			#endregion
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <param name="t"></param>
		/// <returns></returns>
		Vector3 EvaluateTrefoil(float s, float t)
		{
			float TwoPi = (float)Math.PI * 2.0f;
			float a = 0.5f;
			float b = 0.3f;
			float c = 0.5f;
			float d = 0.1f;
			float u = (1.0f - s) * 2.0f * TwoPi;
			float v = t * TwoPi;
			float r = a + b * (float)Math.Cos(1.5f * u);
			float x = r * (float)Math.Cos(u);
			float y = r * (float)Math.Sin(u);
			float z = c * (float)Math.Sin(1.5f * u);

			Vector3 dv;
			dv.X = -1.5f * b * (float)Math.Sin(1.5f * u) * (float)Math.Cos(u) - (a + b * (float)Math.Cos(1.5f * u)) * (float)Math.Sin(u);
			dv.Y = -1.5f * b * (float)Math.Sin(1.5f * u) * (float)Math.Sin(u) + (a + b * (float)Math.Cos(1.5f * u)) * (float)Math.Cos(u);
			dv.Z = 1.5f * c * (float)Math.Cos(1.5f * u);

			Vector3 q = dv;
			q.Normalize();
			Vector3 qvn = Vector3.Normalize(new Vector3(q.Y, -q.X, 0));
			Vector3 ww = Vector3.Cross(q, qvn);
			

			Vector3 range;
			range.X = x + d * (qvn.X * (float)Math.Cos(v) + ww.X * (float)Math.Sin(v));
			range.Y = y + d * (qvn.Y * (float)Math.Cos(v) + ww.Y * (float)Math.Sin(v));
			range.Z = z + d * ww.Z * (float)Math.Sin(v);
			return range;
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

			if (Index != null)
				Index.Dispose();
			Index = null;

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

			// Some dummy text
			Font.DrawText(new Point(100, 25), Color.White, "Here's an example of draw buffers.");


			// Aplly a rotation
			Matrix4 mvp = Matrix4.CreateRotationY(Yaw) * ModelViewMatrix * ProjectionMatrix;

			// Uniforms
			Display.Shader.SetUniform("modelview", mvp);
			Display.Shader.SetUniform("DiffuseMaterial", new float[] { 0.0f, 0.75f, 0.75f });	
			Display.Shader.SetUniform("LightPosition", new float[] { 0.25f, 0.25f, -1.0f});
			Display.Shader.SetUniform("AmbientMaterial", new float[] { 0.04f, 0.04f, 0.04f });
			Display.Shader.SetUniform("SpecularMaterial", new float[] { 0.5f, 0.5f, 0.5f });
			Display.Shader.SetUniform("Shininess", 50.0f);

			// Draws with the index buffer
			Display.DrawIndexBuffer(Buffer, BeginMode.Triangles, Index);

		}




		#region Properties

		/// <summary>
		/// Rotation
		/// </summary>
		float Yaw;


		/// <summary>
		/// Index buffer
		/// </summary>
		BatchBuffer Buffer;

		
		/// <summary>
		/// Index buffer
		/// </summary>
		IndexBuffer Index;


		/// <summary>
		/// Font
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
