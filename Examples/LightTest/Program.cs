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


namespace ArcEngine.Examples.LightTest
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
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			// Create the game window
			CreateGameWindow(new Size(1024, 768));

			// Change the window title
			Window.Text = "2D light test";
			Mouse.Visible = false;

		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Clear color of the screen
			Display.ClearColor = Color.Black;
			Display.Shader.Dispose();
			Display.Shader = Shader.CreateColorShader();


			Lights = new Light[2];
			Lights[0] = new Light();
			Lights[0].Location = new Point(200, 200);

			Manager = new LightManager();
			Manager.AddLight(Lights[0]);


			Manager.AddWall(new Wall(new Point(300, 200), new Point(700, 500)));
			Manager.AddWall(new Wall(new Point(700, 200), new Point(300, 500)));
			Manager.AddWall(new Wall(new Point(100, 200), new Point(100, 500)));
			Manager.AddWall(new Wall(new Point(900, 200), new Point(800, 500)));
			Manager.AddWall(new Wall(new Point(300, 100), new Point(700, 100)));

		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Manager != null)
				Manager.Dispose();
			Manager = null;
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


			Lights[0].Location = Mouse.Location;
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Manager.Render();
		}




		#region Properties

		
		/// <summary>
		/// Light manager
		/// </summary>
		LightManager Manager;


		/// <summary>
		/// Lights
		/// </summary>
		Light[] Lights;

		#endregion

	}
}










