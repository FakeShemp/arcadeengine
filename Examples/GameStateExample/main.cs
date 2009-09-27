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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;
using ArcEngine.Asset;


namespace ArcEngine.Examples.GameState
{
	class main : Game
	{
		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (main game = new main())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public main()
		{
			Display.ClearColor = Color.CornflowerBlue;
			Window.Size = new Size(1024, 768);

			GSM = new ScreenManager(this);
			GSM.AddScreen(new BackgroundScreen());
			GSM.AddScreen(new MainScreen());

		}



		#region Initialization


		/// <summary>
		/// Called when graphics resources need to be loaded.
		/// </summary>
		public override void LoadContent()
		{
			// Load a bank
			ResourceManager.LoadBank("data/data.bnk");

			// Initialize the Game Screen Manager
			GSM.Initialize();
			GSM.LoadContent();

			GSM.Font = new TTFFont(Device);
			((TTFFont)GSM.Font).LoadFromTTF("browa.ttf", 25, FontStyle.Regular);
		}


		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public override void UnloadContent()
		{
			GSM.UnloadContent();
		}


		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		/// <param name="device">Rendering device</param>
		public override void Draw()
		{
			device.ClearBuffers();

			GSM.Draw(device);


			if (Keyboard.IsNewKeyPress(Keys.Space))
				device.Rectangle(new Rectangle(10, 10, 100, 100), true);

		}



		/// <summary>
		/// Called when the game has determined that game logic needs to be processed.
		/// </summary>
		/// <param name="gameTime">The time passed since the last update.</param>
		public override void Update(GameTime gameTime)
		{
			GSM.Update(gameTime);
		}


		
		#endregion


		#region Properties


		/// <summary>
		/// Game state manager
		/// </summary>
		ScreenManager GSM;


		#endregion
	}
}
