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


namespace Example
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Template : Game
	{

	
		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (Template game = new Template())
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
		public Template()
		{
			Window.ClientSize = new Size(1024, 768);
			Window.Text = "Template";

		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
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




		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			// A red rectangle
			Display.Color = Color.Red;
			Display.Rectangle(new Rectangle(10, 10, 100, 100), true);

			// A green rectangle
			Display.Color = Color.Green;
			Display.Rectangle(new Rectangle(120, 10, 100, 100), true);

			// A blue rectangle
			Display.Color = Color.Blue;
			Display.Rectangle(new Rectangle(230, 10, 100, 100), true);


		}




		#region Properties


		#endregion

	}


}
