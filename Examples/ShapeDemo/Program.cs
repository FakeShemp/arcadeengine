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
using ArcEngine.Utility;
using System.Collections.Generic;

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
	public class EmptyProject : GameBase
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
					game.Run();
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
			CreateGameWindow(new Size(1024, 768));
			Window.Resizable = true;
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;
			Shape = new Shape();

	//		GL.Enable(EnableCap.Map2Vertex3);
	//		GL.MapGrid2(gridsize, 0.0, 1.0, gridsize, 0.0, 1.0);

		}


		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

	
		//	Display.Color = Color.White;
		//	GL.Map2(MapTarget.Map2Vertex3, 0, 1, 3, uSize, 0, 1, uSize * 3, vSize, grid4x4);
		//	GL.EvalMesh2(MeshMode2.Line, 0, gridsize, 0, gridsize);


			// Filled triangle 
	//		Display.Color = Color.AntiqueWhite;
			Shape.Begin(ShapeMode.Fill);
	//		Display.Color = Color.Yellow;
			Shape.MoveTo(225, 25);
	//		Display.Color = Color.Blue;
			Shape.LineTo(305, 25);
	//		Display.Color = Color.Pink;
			Shape.LineTo(225, 105);
			Shape.ClosePath();
			Shape.End();

			// Empty triangle 
	//		Display.Color = Color.White;
			Shape.Begin(ShapeMode.Stroke);
			Shape.MoveTo(325, 125);
			Shape.LineTo(325, 45);
			Shape.LineTo(245, 125);
			Shape.ClosePath();
			Shape.End();


			// Smiley
	//		Display.Color = Color.Yellow;
			Shape.Begin(ShapeMode.Stroke);
			Shape.ArcTo(75, 75, 50, 0, (float)(Math.PI * 2.0f));
			Shape.MoveTo(110, 75);
			Shape.ArcTo(75, 75, 35, 0, (int)Math.PI);
			Shape.MoveTo(65, 65);
			Shape.ArcTo(60, 65, 5, 0, (float)(Math.PI * 2.0f));
			Shape.MoveTo(95, 65);
			Shape.ArcTo(90, 65, 5, 0, (float)(Math.PI * 2.0f));
			Shape.End();


			//Display.DrawCircle(700, 400, 40, Color.White);
			//Display.DrawArc(600, 400, 40, (float)0.0f, (float)(Math.PI * 1.7f), Color.Red);
			//Display.DrawArc(800, 400, 40, (float)Math.PI * 0.0f, (float)(Math.PI * -1.3f), Color.Green);


			// Arcs
			float angle = (float)(Math.PI * 0.2f); 
			for (int x = 50; x < 550; x += 50)
			{
				Display.DrawArc(x, 170, 20, 0, angle, Color.Red);

				angle += (float)(Math.PI * 0.2f); 
			}


			// Speech bubble
			Display.DrawQuadraticCurve(new Point(75, 225), new Point(25, 262), new Point(25, 225), Color.White);
			Display.DrawQuadraticCurve(new Point(25, 262), new Point(50, 300), new Point(25, 300), Color.White);
			Display.DrawQuadraticCurve(new Point(50, 300), new Point(30, 325), new Point(50, 320), Color.White);
			Display.DrawQuadraticCurve(new Point(30, 325), new Point(65, 300), new Point(60, 320), Color.White);
			Display.DrawQuadraticCurve(new Point(65, 300), new Point(125, 262), new Point(125, 300), Color.White);
			Display.DrawQuadraticCurve(new Point(125, 262), new Point(75, 225), new Point(125, 225), Color.White);

			// Heart
			Display.DrawBezier(new Point(275, 240), new Point(250, 225), new Point(275, 237), new Point(270, 225), Color.Pink);
			Display.DrawBezier(new Point(250, 225), new Point(220, 262), new Point(220, 225), new Point(220, 262), Color.Pink);
			Display.DrawBezier(new Point(220, 262), new Point(275, 320), new Point(220, 280), new Point(240, 302), Color.Pink);
			Display.DrawBezier(new Point(275, 320), new Point(330, 262), new Point(310, 302), new Point(330, 280), Color.Pink);
			Display.DrawBezier(new Point(330, 262), new Point(300, 225), new Point(330, 262), new Point(330, 225), Color.Pink);
			Display.DrawBezier(new Point(300, 225), new Point(275, 240), new Point(285, 225), new Point(275, 237), Color.Pink);

	//		Display.Color = Color.Pink;
			Shape.Begin(ShapeMode.Fill);
			Shape.MoveTo(275, 340);
			Shape.BezierCurveTo(new Point(250, 325), new Point(275, 337), new Point(270, 325));
			Shape.BezierCurveTo(new Point(220, 362), new Point(220, 325), new Point(220, 362));
			Shape.BezierCurveTo(new Point(275, 420), new Point(220, 380), new Point(240, 402));
			Shape.BezierCurveTo(new Point(330, 362), new Point(310, 402), new Point(330, 380));
			Shape.BezierCurveTo(new Point(300, 325), new Point(330, 362), new Point(330, 325));
			Shape.BezierCurveTo(new Point(275, 340), new Point(285, 325), new Point(275, 337));
			Shape.End();

			// Bezier curve
			Display.DrawBezier(new Point(610, 100), new Point(900, 100), new Point(600, 400), new Point(900, 500), Color.White);


	//		Display.Color = Color.Red;
			Shape.Begin(ShapeMode.Stroke);
			Shape.MoveTo(600, 100);
			Shape.BezierCurveTo(new Point(890, 100), new Point(590, 400), new Point(890, 500));
			Shape.End();

			// Rounded rectangle
			Rectangle rectangle = new Rectangle(400, 250, 150, 50);
			int radius = 15;
	//		Display.Color = Color.SpringGreen;

			Shape.Begin(ShapeMode.Stroke);
			Shape.RoundedRectangle(rectangle, radius);
			Shape.End();

			rectangle.Offset(0, 75);
			Shape.Begin(ShapeMode.Fill);
			Shape.RoundedRectangle(rectangle, radius);
			Shape.End();
		}




		#region Properties


		/// <summary>
		/// Drawing shape
		/// </summary>
		Shape Shape;

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
