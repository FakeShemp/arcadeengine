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
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Asset;

//
// http://gametuto.com/tetris-tutorial-in-c-render-independent/
//

namespace ProjectT
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class ProjectT : GameBase
	{


		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (ProjectT game = new ProjectT())
					game.Run();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public ProjectT()
		{
			// Setup the game window
			CreateGameWindow(new Size(640, 768));
			Window.Text = "Project T";

			// Size of the board
			BoardSize = new Size(10, 20);

			Shape = new Shape();
			Board = new Board(new Size(10, 20));
			Board.Location = new Point(100, 20);


		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			ResourceManager.LoadBank("data/data.bnk");

			Shape.Init();
			Board.Init();
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
			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

			if (Keyboard.IsNewKeyPress(Keys.Insert))
				RunEditor();


			Board.Update(gameTime);
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			Board.Draw();


			// Next block
			Display.Color = Color.Red;
			Shape.Draw(new Point(450, 50), 2, 2);

		}




		#region Properties

		/// <summary>
		/// Size of the board
		/// </summary>
		Size BoardSize;


		/// <summary>
		/// Next coming block
		/// </summary>
		byte NextBlock;


		/// <summary>
		/// 
		/// </summary>
		Shape Shape;


		/// <summary>
		/// 
		/// </summary>
		Board Board;

		#endregion

	}


}
