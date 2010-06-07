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
using System.Xml;


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
			Window.Text = "CellShading example";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;
			Display.DepthTest = true;
	

			#region Shader

			string vshader = @"
				#version 130

				uniform mat4 mvp_matrix;
				uniform mat3  NormalMatrix;

				in vec3 in_position;
				in vec3 in_normal;
				in vec4 in_color;

				out vec4 out_color;
				out vec3 EyespaceNormal;

				void main(void)
				{
					gl_Position = mvp_matrix * vec4(in_position, 1.0);

					out_color = in_color;
					EyespaceNormal = NormalMatrix * in_normal;
				}";


			string fshader = @"
				#version 130

				uniform vec3 LightDir;

				in vec4 out_color;
				in vec3 EyespaceNormal;

				out vec4 frag_color;

				vec4 CelShading(vec4 color) 
				{
//					float Intensity = dot( LightDir , normalize(EyespaceNormal) );
//					float factor = 1.0;
//					if ( Intensity < 0.5 ) factor = 0.5;
//					color = vec4 ( factor, factor, factor, 1.0 );

					return color;
				}


				void main(void)
				{
					frag_color = CelShading(out_color);
				}";

			Display.Shader.SetSource(ShaderType.VertexShader, vshader);
			Display.Shader.SetSource(ShaderType.FragmentShader, fshader);
			Display.Shader.Compile();

			Display.Shader.SetUniform("LightDir", new float[] { 0.5f, 0.5f, 0.0f });
			Display.Shader.SetUniform("nNormalMatrix", Matrix4.Identity);

			#endregion


			#region Buffer

			// Creates vertex buffer
			Buffer = new BatchBuffer();
			Buffer.AddDeclaration("in_position", 3);
			Buffer.AddDeclaration("in_normal", 3);
			Buffer.AddDeclaration("in_color", 4);

			int Slices = 128;
			int Stacks = 32;
			float ds = 1.0f / Slices;
			float dt = 1.0f / Stacks;
			int VertexCount = Slices * Stacks;

			float[] buffer = new float[VertexCount * 10];
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


					buffer[pos++] = 1.0f;
					buffer[pos++] = 0.0f;
					buffer[pos++] = 0.0f;
					buffer[pos++] = 1.0f;
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


			#region Matrices

			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			Display.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, aspectRatio, 0.1f, 20.0f);


			Position = new Vector3(0.0f, 0.0f, -5.0f);
			Display.ModelViewMatrix = Matrix4.LookAt(
				Position,
				Vector3.Zero,
				Vector3.UnitY);

			

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


			if (Keyboard.IsKeyPress(Keys.Left))
				Yaw -= Speed;

			if (Keyboard.IsKeyPress(Keys.Right))
				Yaw += Speed;


			if (Keyboard.IsKeyPress(Keys.Up))
				Pitch += Speed;

			if (Keyboard.IsKeyPress(Keys.Down))
				Pitch -= Speed;


			if (Keyboard.IsKeyPress(Keys.Space))
			{
				Pitch = 0.0f;
				Yaw = 0.0f;
			}
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			// Some dummy text
		//	Font.DrawText(new Point(100, 25), Color.White, "Here's an example of draw buffers.");


			// Draws with the index buffer
			Display.PushMatrix(MatrixMode.Modelview);
			Display.ModelViewMatrix = Matrix4.CreateRotationY(Yaw) * Display.ModelViewMatrix;
			Display.ModelViewMatrix = Matrix4.CreateRotationX(Pitch) * Display.ModelViewMatrix;
			//Display.DrawBatch(Buffer, 0, 24567);
			Display.DrawIndexBuffer(Buffer, BeginMode.Triangles, Index);
			Display.PopMatrix(MatrixMode.Modelview);

		}


		float Yaw;
		float Pitch;

		#region Properties

		/// <summary>
		/// Index buffer
		/// </summary>
		BatchBuffer Buffer;

		
		/// <summary>
		/// 
		/// </summary>
		IndexBuffer Index;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// 
		/// </summary>
		Vector3 Position;


		/// <summary>
		/// 
		/// </summary>
		float Speed = 1.0f / 60.0f;

		#endregion

	}
}
