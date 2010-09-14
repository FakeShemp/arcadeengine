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
using System.Collections.Generic;



//
// Tunnel effect taken from http://www.rozengain.com/files/webgl/tunnel/
//
namespace ArcEngine.Examples.Tunnel
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
			Window.Text = "Tunnel effect - Thanks to http://www.rozengain.com/blog/";
		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Render states
			Display.RenderState.ClearColor = Color.Black;
			Display.RenderState.DepthTest = true;
			

			// Setup the shader
			Shader = new Shader();
			Shader.LoadSource(ShaderType.VertexShader, "data/shader.vert");
			Shader.LoadSource(ShaderType.FragmentShader, "data/shader.frag");
			Shader.Compile();


			// Load the texture
			Texture = new Texture2D("data/texture.jpg");
			Texture.VerticalWrap = VerticalWrapFilter.Repeat;
			Texture.HorizontalWrap = HorizontalWrapFilter.Repeat;
			Texture.MagFilter = TextureMagFilter.Linear;
			Texture.MinFilter = TextureMinFilter.Linear;


			// Build the mesh
			#region Mesh

			float radius = 7.0f;
			float currentRadius = radius;
			int segments = 24;
			int spacing = 2;
			int numRings = 18;
			int index = 0;

			List<float> data = new List<float>();
			List<int> indices = new List<int>();
			for (int ring = 0 ; ring < numRings ; ring++)
			{
				for (int segment = 0; segment < segments; segment++)
				{
					float degrees = (360/segments) * segment;
					float radians = (float) (Math.PI/180) * degrees;

					// Vertices
					data.Add((float) Math.Cos(radians) * currentRadius);
					data.Add((float) Math.Sin(radians) * currentRadius);
					data.Add(ring * -spacing);

					// Texture coord
					if (segment < (segments-1)/ 2)
					{
						data.Add((1.0f/(segments)) * segment * 2.0f);
						data.Add((1.0f/4.0f) * ring);
					}
					else
					{
						data.Add(2.0f-((1.0f/(segments)) * segment * 2.0f));
						data.Add((1.0f / 4.0f) * ring);
					}

					// Color
					float color = 1.0f - ((1.0f / (numRings - 1)) * ring);
					data.Add(color);
					data.Add(color);
					data.Add(color);
					data.Add(1.0f);


					// Indices
					if (ring<numRings-1)
					{
						if (segment < segments-1)
						{
							indices.Add(index);
							indices.Add(index + segments + 1);
							indices.Add(index + segments);

							indices.Add(index);
							indices.Add(index+1);
							indices.Add(index + segments + 1);
						}
						else
						{
							indices.Add(index);
							indices.Add(index + 1);
							indices.Add(index + segments);

							indices.Add(index);
							indices.Add(index - segments + 1);
							indices.Add(index + 1);
						}
					}

					index++;
				}
				currentRadius -= radius/numRings;
			}

			Mesh = new Mesh();
			Mesh.Buffer.AddDeclaration("aVertexPosition", 3);
			Mesh.Buffer.AddDeclaration("aTextureCoord", 2);
			Mesh.Buffer.AddDeclaration("aVertexColor", 4);

			Mesh.SetVertices(data.ToArray());
			Mesh.SetIndices(indices.ToArray());
			Mesh.PrimitiveType = PrimitiveType.Triangles;

			#endregion
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (Mesh != null)
				Mesh.Dispose();
			Mesh = null;

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


			// Wireframe mode
			if (Keyboard.IsKeyPress(Keys.Space))
				Mesh.PrimitiveType = PrimitiveType.LineStrip;
			else
				Mesh.PrimitiveType = PrimitiveType.Triangles;
		}


		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			// Binds the texture
			Display.Texture = Texture;

			// Build matrix
			Matrix4 ModelViewMatrix = Matrix4.LookAt(new Vector3(0.0f, 0.0f, 8.0f), Vector3.Zero, Vector3.UnitY);
			float aspectRatio = (float) Display.ViewPort.Width / (float) Display.ViewPort.Height;
			Matrix4 ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);

			// Bind shader and uniforms
			Display.Shader = Shader;
			Shader.SetUniform("uPMatrix", ProjectionMatrix);
			Shader.SetUniform("uMVMatrix", ModelViewMatrix);
			Shader.SetUniform("uSampler", 0);

			Shader.SetUniform("fTime", currentTime);
			currentTime = (currentTime + 0.02f);

			// Draw the mesh
			Mesh.Draw();

		}


		#region Properties

		/// <summary>
		/// Tunnel time
		/// </summary>
		float currentTime = 0.0f;


		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;


		/// <summary>
		/// Texture
		/// </summary>
		Texture2D Texture;


		/// <summary>
		/// Mesh
		/// </summary>
		Mesh Mesh;

		#endregion

	}

}
