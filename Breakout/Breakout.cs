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
using System.Collections.Generic;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using ArcEngine.Input;
using ArcEngine.Asset;
using System.Text;


namespace ArcEngine.Games.Breakout
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Breakout : Game
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (Breakout game = new Breakout())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Breakout()
		{
			//Device = new OpenGLRender();
			Window.Size = new Size(800, 600);
			Window.Text = "Breakout";
			Mouse.Visible = false;
			
		}



		/// <summary>
		/// 
		/// </summary>
		public override void LoadContent()
		{
			ResourceManager.AddProvider(new LevelProvider());


			// Load data
			ResourceManager.LoadBank("data/data.bnk");

			// Default font
			//Font = new TextureFont();
			//Font.LoadFromTTF("browa.ttf", 12, FontStyle.Regular);
			//Font.Color = Color.Black;


			Level = ResourceManager.CreateAsset<Level>("Level_1");
			Level.Init();


			// Display settings
			Display.Blending = true;
			Display.ClearColor = Color.Black;


			Lives = 5;

	
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{


			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();


			if (Level != null)
				Level.Update(gameTime);


			if (Level.Balls.Count == 0)
			{
				Lives--;

				Level.Balls.Add(new Ball(Level));
			}

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


			Level.Draw();


			// Left panel
			//Display.Rectangle(new Rectangle(0, 0, 160, 600), true);
			//Font.DrawText(new Point(10, 20), "Level: " + 1);
			//Font.DrawText(new Point(10, 40), "Lives: " + Lives);
			//Font.DrawText(new Point(10, 60), "Score: " + Score);

		}





		#region Properties



		/// <summary>
		/// 
		/// </summary>
		//TextureFont Font;

		/// <summary>
		/// 
		/// </summary>
		public Level Level;



		/// <summary>
		/// 
		/// </summary>
		public int Lives
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public int Score
		{
			get;
			private set;
		}


		#endregion

	}
}
