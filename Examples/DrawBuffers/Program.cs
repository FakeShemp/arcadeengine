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

// http://bakura.developpez.com/tutoriels/jeux/utilisation-shaders-avec-opengl-3-x/
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vertex-array-objects-avec-opengl-3-x/

namespace ArcEngine.Examples.DrawBufer
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
			Window.Text = "Draw buffers example";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;



			// Shader
			Shader = Shader.CreateTextureShader();
			Display.Shader = Shader;


			#region Texture

			// Loads a texture and binds it to TUI 2
			Texture = new Texture2D("data/texture.png");
			Texture.HorizontalWrap = HorizontalWrapFilter.Repeat;
			Texture.VerticalWrap = VerticalWrapFilter.Repeat;
			Display.TextureUnit = 2;
			Display.Texture = Texture;
			
			#endregion

			// Matrices
			ModelViewMatrix = Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0)); ;
			ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Display.ViewPort.Width, Display.ViewPort.Height, 0, -1.0f, 1.0f);
			TextureMatrix = Matrix4.Scale(1.0f / Texture.Size.Width, 1.0f / Texture.Size.Height, 1.0f);
			
			#region Buffer


			// Indices
			int[] indices = new int[]
			{
				0, 1, 2, 
				1, 2, 3
			};

			Index = new IndexBuffer();
			Index.Update(indices);


			// Creates a position, color, texture buffer
			Buffer = BatchBuffer.CreatePositionColorTextureBuffer();


			// Vertex elements
			float[] vertices = new float[]
			{
				// Coord							Color									Texture
				100.0f,  100.0f,				1.0f, 0.0f, 0.0f, 1.0f,			0.0f,   0.0f,
				500.0f,  100.0f,				0.0f, 1.0f, 0.0f, 1.0f,			256.0f, 0.0f,
				100.0f,  500.0f,				0.0f, 0.0f, 1.0f, 1.0f,			0.0f,   256.0f,
				500.0f,  500.0f,				1.0f, 1.0f, 1.0f, 1.0f,			256.0f, 256.0f
			};
			Buffer.SetVertices(vertices);


			// Or set data one by one
			Buffer.AddPoint(new Point(100, 100), Color.FromArgb(255, 0, 0), new Point(0, 0));
			Buffer.AddPoint(new Point(500, 100), Color.FromArgb(0, 255, 0), new Point(256, 0));
			Buffer.AddPoint(new Point(100, 500), Color.FromArgb(0, 0, 255), new Point(0, 256));
			Buffer.AddPoint(new Point(500, 500), Color.FromArgb(255, 255, 255), new Point(256, 256));
			Buffer.Update();


			#region VAO

			//Batch = new BatchBuffer();


			//VertexBuffer.Bind(0, 2);
			//Shader.BindAttrib(0, "in_position");

			//ColorBuffer.Bind(1, 4);
			//Shader.BindAttrib(1, "in_color");


			//GL.BindVertexArray(0);

			#endregion

			#endregion


			#region Font

			SpriteBatch = new Graphic.SpriteBatch();
			Font = BitmapFont.CreateFromTTF("c:\\windows\\fonts\\verdana.ttf", 16, FontStyle.Regular);

			#endregion

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

			if (Texture != null)
				Texture.Dispose();
			Texture = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;
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

			// Some dummy text
			SpriteBatch.Begin();
			SpriteBatch.DrawString(Font, new Vector2(100, 25), Color.White, "Here's an example of draw buffers :");
			SpriteBatch.End();


			Display.Texture = Texture;

			// Draws with the index buffer
			Shader.SetUniform("mvp_matrix", ModelViewMatrix * ProjectionMatrix);
			Shader.SetUniform("tex_matrix", TextureMatrix);
			Shader.SetUniform("texture", 2);
			Display.DrawIndexBuffer(Buffer, PrimitiveType.Triangles, Index);
		}



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
		/// Texture
		/// </summary>
		Texture2D Texture;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		///  Shader
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
		/// Texture matrix
		/// </summary>
		Matrix4 TextureMatrix;


		#endregion

	}
}
