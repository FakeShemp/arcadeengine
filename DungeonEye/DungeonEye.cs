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
using ArcEngine.Input;
using ArcEngine.Providers;
using ArcEngine.ScreenManager;


namespace DungeonEye
{
	class DungeonEye : Game
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			DungeonEye game = null;

			try
			{
				using (game = new DungeonEye())
					game.Run();
			}
			catch (Exception e)
			{
				Trace.WriteLine("");
				Trace.WriteLine("!!!FATAL ERROR !!!");
				Trace.WriteLine("Message : " + e.Message);
				Trace.WriteLine("StackTrace : " + e.StackTrace);
				Trace.WriteLine("");

				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public DungeonEye()
		{
			Settings.Load("settings.xml");
			KeyboardSchemeName = Settings.GetString("keyboardscheme");
			LanguageName = Settings.GetString("language");

			GSM = new ScreenManager(this);
		}



		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.
		/// </summary>
		public override void LoadContent()
		{
			
			Window.ClientSize = new Size(640, 400);
			Window.Text = "Dungeon Eye";

			// Enble the console
			Terminal.Enable = true;

			// Display settings
			Mouse.Visible = false;

			// Add the provider
			ResourceManager.AddProvider(new DungeonProvider());

			// Load data
		//	ResourceManager.LoadBank("data/data.bnk");

			// Language to use
			//Settings.StringTable = ResourceManager.CreateSharedAsset<StringTable>(Settings.LanguageName);


			GSM.AddScreen(new IntroScreen());
			//GSM.AddScreen(new MainMenu());
			//GSM.AddScreen(new Team());
		}




		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public override void UnloadContent()
		{
			GSM.UnloadContent();

			ResourceManager.ClearAssets();
		}




		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		public override void Update(GameTime gameTime)
		{
			if (Keyboard.IsKeyPress(Keys.Insert))
				RunEditor();


			GSM.Update(gameTime);
			
		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			GSM.Draw();


			//if (Keyboard.IsKeyPress(Keys.LControlKey) ||Keyboard.IsKeyPress(Keys.RControlKey))
			//{
			//   printer.Begin();
			//   GL.MatrixMode(MatrixMode.Texture);
			//   GL.LoadIdentity();
			//   printer.Print("DirectCall : " + Display.RenderStats.DirectCall.ToString(), sans_serif, Color.White, new RectangleF(10, 20, 0, 0));
			//   printer.Print("BatchCall : " + Display.RenderStats.BatchCall.ToString(), sans_serif, Color.White, new RectangleF(10, 32, 0, 0));
			//   printer.Print("TextureBinding : " + Display.RenderStats.TextureBinding.ToString(), sans_serif, Color.White, new RectangleF(10, 44, 0, 0));
			//   printer.End();
			//}
		 }





		#region Properties


		/// <summary>
		/// Game state manager
		/// </summary>
		ScreenManager GSM;

		/// <summary>
		/// Current keyboard schema
		/// </summary>
		static public string KeyboardSchemeName = "azerty";

		/// <summary>
		/// Current language
		/// </summary>
		static public string LanguageName = "English";

		#endregion
	}

}
