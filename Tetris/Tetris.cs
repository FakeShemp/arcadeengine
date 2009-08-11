using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;


namespace Tetris
{
	/// <summary>
	/// 
	/// </summary>
	class Tetris : Game
	{


		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (Tetris game = new Tetris())
				game.Run();
		}


		/// <summary>
		/// 
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

			Device = new OpenGLRender();
			Window.Size = new Size(640, 400);
			Window.Text = "Tetris";


			Grid = new byte[12, 20];


		}





		/// <summary>
		/// 
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Update(GameTime gameTime)
		{
			
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		protected override void Draw(VideoRender device)
		{
			Device.ClearBuffers();
		}





		#region Properties


		/// <summary>
		/// 
		/// </summary>
		byte[,] Grid;

		/// <summary>
		/// Block types we can have for each new block that falls down.
		/// </summary>
		public enum BlockTypes
		{
			Empty,
			Block,
			Triangle,
			Line,
			RightT,
			LeftT,
			RightShape,
			LeftShape,
		}

		/// <summary>
		/// Unrotated shapes
		/// </summary>
		readonly byte[][,] BlockShapes = new byte[][,]
			{
				// Empty
				new byte[,] { { 0 } },
				// Line
				new byte[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } },
				// Block
				new byte[,] { { 1, 1 }, { 1, 1 } },
				// RightT
				new byte[,] { { 1, 1 }, { 1, 0 }, { 1, 0 } },
				// LeftT
				new byte[,] { { 1, 1 }, { 0, 1 }, { 0, 1 } },
				// RightShape
				new byte[,] { { 0, 1, 1 }, { 1, 1, 0 } },
				// LeftShape
				new byte[,] { { 1, 1, 0 }, { 0, 1, 1 } },
				// LeftShape
				new byte[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } },
			};

		#endregion
	}
}
