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

				precision highp float;

				uniform mat4 mvp_matrix;
				uniform mat4 tex_matrix;

				in vec3 in_position;
				in vec3 in_normal;
				in vec4 in_color;

				invariant gl_Position;

				smooth out vec4 out_color;

				void main(void)
				{
					gl_Position = mvp_matrix * vec4(in_position, 1.0);

					out_color = in_color;
				}";


			string fshader = @"
				#version 130

				precision highp float;

				smooth in vec4 out_color;

				out vec4 frag_color;

				void main(void)
				{
					frag_color = out_color;
				}";

			Display.Shader.SetSource(ShaderType.VertexShader, vshader);
			Display.Shader.SetSource(ShaderType.FragmentShader, fshader);
			Display.Shader.Compile();

			#endregion


			#region Buffer

			// Create index buffer
			Index = new IndexBuffer();


			// Creates vertex buffer
			Buffer = new BatchBuffer();
			Buffer.AddDeclaration("in_position", 3);
			Buffer.AddDeclaration("in_normal", 3);
			Buffer.AddDeclaration("in_color", 4);

			InitializeCube();

			#endregion


			#region Font

			Font = BitmapFont.CreateFromTTF("c:\\windows\\fonts\\verdana.ttf", 16, FontStyle.Regular);

			#endregion


			#region Matrices

			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			Display.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, aspectRatio, 0.1f, 20.0f);


			Position = new Vector3(0.0f, 0.0f, 10.0f);
			Display.ModelViewMatrix = Matrix4.LookAt(
				Position,
				Vector3.Zero,
				Vector3.UnitY);

			#endregion

		}

		/// <summary>
		/// Creates an array of indexed position/normal/colored data.
		/// </summary>
		private void InitializeCube()
		{
			// vertex coords array
			float[] vertices = new float[]
			{
				// Vertex				// Normal			// Color
				1,1,1,					0,0,1,				1,0,0,1,	
				-1,1,1,					0,0,1,				1,0,0,1,
				-1,-1,1,					0,0,1,				1,0,0,1,

				1,-1,1,       			0,0,1,  				0,1,0,1,
				1,1,1,					1,0,0,				0,1,0,1,	
				1,-1,1,					1,0,0,				0,1,0,1,

				1,-1,-1,					1,0,0,				0,0,1,1,
				1,1,-1,       			1,0,0,  				0,0,1,1,
				1,1,1,					0,1,0,				0,0,1,1,	

				1,1,-1,					0,1,0,				1,0,0,1,
				-1,1,-1,					0,1,0,				1,0,0,1,
				-1,1,1,       			0,1,0,  				1,0,0,1,

				-1,1,1,					-1,0,0, 				0,1,0,1,
				-1,1,-1,					-1,0,0,				0,1,0,1,
				-1,-1,-1,				-1,0,0, 				0,1,0,1,

				-1,-1,1,   				-1,0,0, 				0,0,1,1,
				-1,-1,-1,				0,-1,0, 				0,0,1,1,
				1,-1,-1,					0,-1,0, 				0,0,1,1,

				1,-1,1,					0,-1,0, 				1,0,0,1,
				-1,-1,1,   				0,-1,0, 				1,0,0,1,
				1,-1,-1,					0,0,-1, 				1,0,0,1,

				-1,-1,-1,				0,0,-1, 				0,1,0,1,
				-1,1,-1,					0,0,-1, 				0,1,0,1,
				1,1,-1,					0,0,-1,				0,1,0,1,

			};


			// index array of vertex array
			int[] indices = new int[]
			{
				0,1,2,3,
				4,5,6,7,
				8,9,10,11,
				12,13,14,15,
				16,17,18,19,
				20,21,22,23
			};

			// Update index buffer
			Index.Update(indices);


			// Update Vertex buffer
			Buffer.SetVertices(vertices);

		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Index != null)
				Index.Dispose();
			Index = null;

			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

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
				angle -= Speed;

			if (Keyboard.IsKeyPress(Keys.Right))
				angle += Speed;

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
			Display.ModelViewMatrix = Matrix4.CreateRotationY(angle) * Display.ModelViewMatrix;
			Display.DrawIndexBuffer(Buffer, BeginMode.Triangles, Index);
			Display.PopMatrix(MatrixMode.Modelview);

		}


		float angle;


		#region Properties

		/// <summary>
		/// Index buffer
		/// </summary>
		IndexBuffer Index;

		/// <summary>
		/// Index buffer
		/// </summary>
		BatchBuffer Buffer;


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
