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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Storage;
using ArcEngine.Utility.ScreenManager;
using RuffnTumble.Editor;

//
//http://gamedevgeek.com/tutorials/managing-game-states-in-c/
//
// Physic Engine :
// - http://www.box2d.org/
// - chipmunk http://www.codeplex.com/chipmunkxna
// - http://www.codeplex.com/FarseerPhysics/
// - http://www.codeplex.com/FarseerPhysics
//
// Gestionnaire d'evenements
// - http://www.ziggyware.com/readarticle.php?article_id=101
//
//
// GUI:
// - http://www.codeplex.com/neoforce
//
// Text :
// - http://www.createdbyx.com/BufferID-Code+Snips__DrawString+method+with+word+wrap+and+text+alignment-XNA.aspx
//
// Editor :
// - http://www.xs4all.nl/~gimbal/projects/levelbuilder.htm
//
//
// http://msdn.microsoft.com/en-us/library/dd254918.aspx


namespace RuffnTumble
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	class Game : GameBase
	{

		/// <summary>
		/// Application entry point
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Start tracing
			Trace.Start("log.html", "Ruff'n'Tumble");

			using (Game game = new Game())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Game()
		{

			// Check for new version
		//	AutoUpdater.CheckForNewVersion("http://www.mimicprod.net/updater.xml");


			// Add the assets
			ResourceManager.RegisterAsset<World>(typeof(WorldForm));

			// Game state manager
			GSM = new ScreenManager(this);

		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public override void LoadContent()
		{
			// Loads content
			Storage = new BankStorage("data/world1.bnk");
			ResourceManager.AddStorage(Storage);
			ResourceManager.RootDirectory = "data";


			GameWindowParams param = new GameWindowParams();
			param.Major = 2;
			param.Minor = 1;
			param.Compatible = true;
			param.Size = new Size(800, 600);
			CreateGameWindow(param);
			Window.Text = "Ruff'n'Tumble";


			// Remove Multi sampling
			Display.RenderState.MultiSample = false;


			// Default texture parameters
			Texture2D.DefaultBorderColor = Color.Black;
			Texture2D.DefaultMagFilter = TextureMagFilter.Nearest;
			Texture2D.DefaultMinFilter = TextureMinFilter.Nearest;


			// Gamepad initialization
			Gamepad.Init(Window);

			// Register specific game assets
			ResourceManager.RegisterAsset<World>(typeof(RuffnTumble.Editor.WorldForm), "world");


			GSM.AddScreen(new GameScreen());

		}


		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
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
				RunEditor(Storage);

			// Update game screens
			GSM.Update(gameTime);
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			// Render game screens
			GSM.Draw();
		}


		/// <summary>
		/// Process command entered in the terminal
		/// </summary>
		/// <param name="text"></param>
		void ProcessConsoleCommands(string cmd)
		{
			Trace.WriteLine(cmd);
		}



		#region Events


		/// <summary>
		/// On key down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="evt"></param>
		void OnKeyDown(object sender, PreviewKeyDownEventArgs evt)
		{
			switch (evt.KeyCode)
			{
				// Byebye
				case Keys.Escape:
				{
					Console.WriteLine("Quit !!!!");
					Exit();
				}
				break;

				// The player is like a god
				//case Keys.W:
				//{
				//    Layer layer = CurrentWorld.GetLayer("tiles");
				//    if (layer == null)
				//        break;

				//    Entity player = layer.GetEntity("Player");
				//    if (player == null)
				//        break;

				//    player.God = !player.God;

				//}
				//break;

				// Display grid
				//case Keys.G:
				//{
				//    foreach (Layer layer in CurrentWorld.Layers)
				//    {
				//        layer.ShowGrid = !layer.ShowGrid;
				//    }
				//}
				//break;


				//case Keys.F11:
				//{
				//    Display.WaitVbl = !Display.WaitVbl;
				//}
				//break;

				// 
				case Keys.F12:
				{
					//Layer layer = CurrentWorld.GetLayer("tiles");
					//if (layer == null)
					//    break;

					//Entity player = layer.GetEntity("Player");
					//if (player == null)
					//    break;

					//player.Location = layer.GetSpawnPoint("player1").Location;
				}
				break;


				// Shows some debug
				case Keys.Insert:
				{
					//DebugStat = !DebugStat;
				}
				break;

				// Shows collision layer
				case Keys.C:
				{
					//Layer layer = CurrentWorld.GetLayer("collision");
					//if (layer != null)
					//    layer.Visible = !CurrentWorld.GetLayer("collision").Visible;
				}
				break;



			}
		}



		#endregion



		#region Properties

		/// <summary>
		/// Game state manager
		/// </summary>
		ScreenManager GSM;


		/// <summary>
		/// Storage
		/// </summary>
		StorageBase Storage;

		#endregion
	}
}
