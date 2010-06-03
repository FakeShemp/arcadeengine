using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK.Graphics.OpenGL;

namespace PathFinding
{
	class AStar : GameBase
	{
		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (AStar game = new AStar())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public AStar()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Resizable = true;

		}


		#region Initialization


		/// <summary>
		/// Called when graphics resources need to be loaded.
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.LightGray;
			Display.Blending = false;
		}


		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public override void UnloadContent()
		{
		}


		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		/// <param name="device">Rendering device</param>
		public override void Draw()
		{
			Display.ClearBuffers();


		//	Display.Color = Color.Red;
			float[] grid2x2 = new float[12]
			{
				0.0f, 0.0f, 0.0f, 
				0.0f, 600.0f, 0.0f,
				600.0f, 0.0f, 0.0f,
				600.0f, 600.0f, 0.0f, 
			};

			GL.Enable(EnableCap.Map2Vertex3);
			GL.Map2(MapTarget.Map2Vertex3,
			  0.0f, 100.0f,  /* U ranges 0..1 */
			  3,         /* U stride, 3 floats per coord */
			  2,         /* U is 2nd order, ie. linear */
			  0.0f, 100.0f,  /* V ranges 0..1 */
			  2 * 3,     /* V stride, row is 2 coords, 3 floats per coord */
			  2,         /* V is 2nd order, ie linear */
			  grid2x2);  /* control points */


			GL.MapGrid2(5, 0.0f, 10.0f, 6, 0.0f, 10.0f);


			GL.EvalMesh2(MeshMode2.Line,
  0, 100,   /* Starting at 0 mesh 5 steps (rows). */
  0, 100);  /* Starting at 0 mesh 6 steps (columns). */


		}



		/// <summary>
		/// Called when the game has determined that game logic needs to be processed.
		/// </summary>
		/// <param name="gameTime">The time passed since the last update.</param>
		public override void Update(GameTime gameTime)
		{
			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();
		}



		#endregion


		#region Properties




		#endregion
	}
}
