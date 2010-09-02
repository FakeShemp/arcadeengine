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

// http://www.codeproject.com/KB/openGL/Outline_Mode.aspx

namespace ArcEngine.Examples.Outlining
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
			GameWindowParams p = new GameWindowParams();
			p.Size = new Size(1024, 768);
			p.Major = 3;
			p.Minor = 1;
			CreateGameWindow(p);
			Window.Text = "Outlining demo";
		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.RenderState.DepthTest = true;
		//	Display.RenderState.Culling = true;
			Display.RenderState.StencilClearValue = 0;

			#region Matrices


			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);

			#endregion


			#region Shader

			#region Vertex Shader
			string vshader = @"
				#version 130

				uniform mat4 mvpMatrix;

				in vec4 in_position;

				void main(void) 
				{ 
					gl_Position = mvpMatrix * in_position; 
				}";

			#endregion

			#region Fragment Shader
			string fshader = @"
				#version 130

				uniform vec4 Color;

				out vec4 frag_color;

				void main(void) 
				{
					frag_color = Color; 
				}";

			#endregion

			Shader = new Shader();
			Shader.SetSource(ShaderType.VertexShader, vshader);
			Shader.SetSource(ShaderType.FragmentShader, fshader);
			Shader.Compile();
			Display.Shader = Shader;

			#endregion


			Mesh = Mesh.CreateTorus(0.15f, 0.50f, 40, 20);
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Mesh != null)
				Mesh.Dispose();
			Mesh = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;
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


			Mesh.Rotation += new Vector3(0.0f, 0.025f, 0.0f);

		}




		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();



			// ModelViewProjection matrix
			Vector3 CameraPostion = new Vector3(0.0f, 0.0f, 3.0f);
			Vector3 target = Vector3.Add(CameraPostion, new Vector3(0.0f, 0.0f, -1.0f));
			Matrix4 ModelViewMatrix = Matrix4.LookAt(CameraPostion, target, Vector3.UnitY);
			Matrix4 mvp = ModelViewMatrix * ProjectionMatrix;

			// Bind the shader
			Display.Shader = Shader;
			Shader.SetUniform("Color", new Vector4(0.0f, 0.0f, 0.0f, 1.0f)); 
			Shader.SetUniform("mvpMatrix", Mesh.Matrix * mvp);

			// Ckear the stencil buffer
			Display.ClearStencilBuffer();
			
			// Set the stencil buffer to 1 in every written pixel
			Display.RenderState.StencilTest = true;
			Display.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
			Mesh.Draw();

			// Set the stencil buffer to only allow writing when the stencil buffer is not 1
			Display.StencilFunction(StencilFunction.Notequal, 1, int.MaxValue);
			Display.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);

			// Draw the mesh with thick lines
			Display.PolygonOffset(1.0f, -1.0f);
			Display.RenderState.PolygonOffsetLine = true;

			Display.RenderState.LineWidth = 5.0f;
			Display.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
			Shader.SetUniform("Color", new Vector4(1.0f, 1.0f, 0.0f, 1.0f));
			Mesh.Draw();

			// Removes changes
			Display.RenderState.LineWidth = 1.0f;
			Display.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
			Display.RenderState.StencilTest = false;
			Display.RenderState.PolygonOffsetLine = false;
		}



		#region Properties


		/// <summary>
		/// Projection matrix
		/// </summary>
		Matrix4 ProjectionMatrix;


		/// <summary>
		/// Torus mesh
		/// </summary>
		Mesh Mesh;


		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;

		#endregion

	}

}
