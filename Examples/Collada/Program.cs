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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// http://www.wazim.com/Collada_Tutorial_1.htm
// http://www.wazim.com/Collada_Tutorial_2.htm
namespace ArcEngine.Examples
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Collada : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (Collada game = new Collada())
					game.Run();
			}
			catch (Exception e)
			{
				// Oops, an error happened !
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Collada()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Collada loader";
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;

/*
			ColladaLoader loader = new ColladaLoader();
			loader.Load("data/plane.dae");
			Mesh = loader.GenerateMesh("Plane_001");


			Batch = new Batch();
			Batch.AddRectangle(new Rectangle(10, 10, 100, 100), Color.Red, Rectangle.Empty);
			Batch.Apply();
*/


			#region Shader
			Shader = new Shader();
			Shader.SetSource(ShaderType.VertexShader,
			@"
			#version 130

			precision highp float;

			uniform mat4 mvp_matrix;

			in vec2 in_position;
			in vec4 in_color;

			out vec4 out_color;

			void main(void)
			{
				gl_Position = mvp_matrix * vec4(in_position, 0.0, 1.0);
				
				out_color = in_color;
			}");


			Shader.SetSource(ShaderType.FragmentShader,
			@"
			#version 130

			precision highp float;

			in vec4 out_color;

			out vec4 out_frag_color;
		
			void main(void)
			{
				out_frag_color = out_color;
			}
			");
			Shader.Compile();
			Display.Shader = Shader;

			// Matrix
			Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Display.ViewPort.Width, Display.ViewPort.Height, 0, 0, 5);
			Matrix4 modelviewMatrix = Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
			Matrix4 mvpMatrix = modelviewMatrix * projectionMatrix;
			Shader.SetUniform("mvp_matrix", mvpMatrix);

			#endregion


			#region Vertex Buffer
			indicesVboData = new uint[]
			{
				0, 1, 2,
			};

			Vector2[] positionVboData = new Vector2[]
			{
				new Vector2( 100.0f,  100.0f),
				new Vector2( 150.0f,  200.0f),
				new Vector2( 50.0f,   200.0f),
			};


			Vector4[] colorVboData = new Vector4[]
			{
				new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
				new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
				new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
			};


			#region VBO
			VertexBuffer = new ArrayBuffer<Vector2>();
			VertexBuffer.Update(positionVboData);


			ColorBuffer = new ArrayBuffer<Vector4>();
			ColorBuffer.Update(colorVboData);


			IndicesBuffer = new IndexBuffer();
			IndicesBuffer.Update(indicesVboData);


			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			#endregion



			#region VAO
			Batch = new DrawBatch();

			VertexBuffer.Bind(0, 2);
			Shader.BindAttrib(0, "in_position");

			ColorBuffer.Bind(1, 4);
			Shader.BindAttrib(1, "in_color");


			IndicesBuffer.Bind();

			GL.BindVertexArray(0);
			#endregion 

			#endregion


			Mesh = new Mesh();
			Mesh.SetIndices(indicesVboData);
		}


		uint[] indicesVboData;
		ArrayBuffer<Vector2> VertexBuffer;
		ArrayBuffer<Vector4> ColorBuffer;
		IndexBuffer IndicesBuffer;






		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Mesh != null)
				Mesh.Dispose();

			if (Shader != null)
				Shader.Dispose();

			if (Batch != null)
				Batch.Dispose();

			if (IndicesBuffer != null)
				IndicesBuffer.Dispose();
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

			Batch.Bind();
			//GL.BindVertexArray(vaoHandle);
			GL.DrawElements(BeginMode.Triangles, indicesVboData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

			Batch.Draw(BeginMode.Triangles);

		}




		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Mesh Mesh;



		/// <summary>
		/// 
		/// </summary>
		//Batch Batch;
		DrawBatch Batch;


		/// <summary>
		/// 
		/// </summary>
		Shader Shader;


		int bufferid;

		#endregion

	}

}
