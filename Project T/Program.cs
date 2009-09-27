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


namespace ArcEngine.Games.ProjectT
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class ProjectT : Game
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
			Window.ClientSize = new Size(640, 768);
			Window.Text = "Project T";
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			ResourceManager.LoadBank("data/data.bnk");


			TileSet = ResourceManager.CreateAsset<TileSet>("tiles");
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


			if (Keyboard.IsKeyPress(Keys.Insert))
				RunEditor();


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
			TileSet.Draw(0, Point.Empty);

			
			Size size = new Size(32, 32);


			// A red rectangle
			for(int x = 0; x < 10; x++)
				TileSet.Draw(2, new Point(x * size.Width + x * 2, 10));
		


		}




		#region Properties


		/// <summary>
		/// 
		/// </summary>
		TileSet TileSet;

		#endregion

	}


}
