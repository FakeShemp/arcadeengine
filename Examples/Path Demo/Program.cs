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
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK.Graphics.OpenGL;
using ArcEngine;


// http://www.opengl.org/resources/code/samples/mjktips/grid/index.html
// http://www.opengl.org/resources/code/samples/mjktips/grid/editgrid.c
//
//
//

namespace PathDemo
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class EmptyProject : Game
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (EmptyProject game = new EmptyProject())
				{
					game.RunEditor();
					//game.Run();
				}
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
		public EmptyProject()
		{
			Window.ClientSize = new Size(1024, 768);
			Window.Resizable = true;

			ResourceManager.LoadBank("data/data.bnk");
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;

			GL.Enable(EnableCap.Map2Vertex3);
			GL.MapGrid2(gridsize, 0.0, 1.0, gridsize, 0.0, 1.0);

		}

		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
		}


		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the Excape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();




		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;


			GL.Map2(MapTarget.Map2Vertex3, 0, 1, 3, uSize, 0, 1, uSize * 3, vSize, grid4x4);
			GL.EvalMesh2(MeshMode2.Line, 0, gridsize, 0, gridsize);


		}




		#region Properties





		/// <summary>
		/// 
		/// </summary>
		float[] grid4x4 = new float[4 * 4 * 3] 
		{
			00.0f, 100.0f, 0.0f,
			250.0f, 100.0f, 0.0f,
			550.0f, 100.0f, 0.0f,
			900.0f, 100.0f, 0.0f,
		  
		    100.0f, 250.0f, 0.0f,
			250.0f, 250.0f, 0.0f,
			550.0f, 250.0f, 0.0f,
			700.0f, 250.0f, 0.0f,
		  
			100.0f, 550.0f, 0.0f,
			250.0f, 550.0f, 0.0f,
			550.0f, 550.0f, 0.0f,
			700.0f, 550.0f, 0.0f,
		  
			100.0f, 700.0f, 0.0f,
			250.0f, 700.0f, 0.0f,
			550.0f, 700.0f, 0.0f,
			700.0f, 700.0f, 0.0f
		};


		/// <summary>
		/// Size of the grid
		/// </summary>
		int gridsize = 60;


		int uSize = 4;
		int vSize = 4;


		#endregion

	}

}
