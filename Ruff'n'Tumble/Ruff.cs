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
using RuffnTumble.Asset;


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


namespace RuffnTumble
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	class Ruff : Game
	{
		/// <summary>
		/// Game entry point
		/// </summary>
		/// <param name="args"></param>
		[STAThread]
		static void Main(string [] args)
		{
			Ruff game = new Ruff();

			//try
			//{
				game.LoadContent();
				game.RunEditor();
			//}
			//catch (Exception e)
			//{
			//    Trace.WriteLine("");
			//    Trace.WriteLine("!!!FATAL ERROR !!!");
			//    Trace.WriteLine("Message : " + e.Message);
			//    Trace.WriteLine("StackTrace : " + e.StackTrace);
			//    Trace.WriteLine("");


			//    MessageBox.Show(e.StackTrace, e.Message);
			//}

			game.Exit();
		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public override void LoadContent()
		{

			Window.ClientSize = new Size(800, 600);
			Window.Text = "Ruff'n'Tumble";

			// Device settings
			Display.Blending = true;

	

			// Enble the console
			Terminal.Enable = true;
	
			// Loads content
			ResourceManager.AddProvider(new LevelProvider());
			ResourceManager.LoadBank("data/world1.bnk");


/*
			// Sets the level
			CurrentLevel = ResourceManager.CreateAsset<Level>("Level1_1");
			CurrentLevel.Init();
			Level.DisplayZone = new Rectangle(0, 56, 800, 544);


			// Default rendering font
			Font = new TextureFont();
			//Font.LoadFromTTF("verdana.ttf", 12, FontStyle.Regular);


			// Events 
			Keyboard.OnKeyDown += new EventHandler<PreviewKeyDownEventArgs>(OnKeyDown);
			Terminal.OnProcessCommand += new ArcEngine.Forms.ProcessCommand(ProcessConsoleCommands);


			Icon = ResourceManager.CreateAsset<TileSet>("Layout");
*/
		}


		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		public override void UnloadContent()
		{
			ResourceManager.ClearAssets();
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		public override void Update(GameTime time)
		{

			if (Keyboard.IsKeyPress(Keys.Insert))
				RunEditor();

			if (Keyboard.IsNewKeyPress(Keys.Escape))
				Exit();
	
			
			if (CurrentLevel == null)
				return;


			CurrentLevel.Update(time);



			// Center the collision layer to match the location on the screen
			Layer layer = CurrentLevel.GetLayer("tiles");
			if (layer != null)
			{
				// Center the player in the middle of the screen
				Entity player = layer.GetEntity("Player");
				if (player != null)
				{
					// Level layer
					CurrentLevel.Location = new Point(
							(int)(player.Location.X - Level.ViewPort.Width / 2.0f),
							(int)(player.Location.Y - Level.ViewPort.Height / 2.0f));

				}

			}
		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			if (CurrentLevel == null)
				return;


			// Draw the level
			CurrentLevel.Draw();


			// Draw the status bar
			Icon.Draw(0, Point.Empty);


			#region Stats
			Point pos = Point.Empty;
			Layer layer = CurrentLevel.GetLayer("tiles");
			//Entity player = CurrentLevel.GetLayer("tiles").GetEntity("Player");
			//if (player.God)
			//   Font.DrawText(new Point(700, 60), "GOD MODE !!!!");


/*
			if (DebugStat)
			{

				Display.Color = Color.White;
				Rectangle rect = player.CollisionBoxLocation;
				rect.Location = CurrentLevel.LevelToScreen(rect.Location);
				Display.Rectangle(rect, false);

				Point tl = new Point(player.CollisionBoxLocation.Left, player.CollisionBoxLocation.Top);
				Point tr = new Point(player.CollisionBoxLocation.Left + player.CollisionBoxLocation.Width, player.CollisionBoxLocation.Top);
				Point bl = new Point(tl.X, tl.Y + player.CollisionBoxLocation.Height);
				Point br = new Point(tr.X, bl.Y);

				Display.Color = Color.White;
				Layer col = CurrentLevel.CollisionLayer;
				Font.DrawText(new Point(400, 100), "tl : " + tl.ToString() + " = " + col.GetTileAtCoord(tl));
				Font.DrawText(new Point(400, 120), "tr : " + tr.ToString() + " = " + col.GetTileAtCoord(tr));
				Font.DrawText(new Point(400, 140), "bl : " + bl.ToString() + " = " + col.GetTileAtCoord(bl));
				Font.DrawText(new Point(400, 160), "br : " + br.ToString() + " = " + col.GetTileAtCoord(br));


				string txt;
				pos = new Point(100, 70);
			//	txt = "Level name : \"" + CurrentLevel.Name + "\"";
			//	Font.DrawText(pos, txt);
				Point lvlpos = CurrentLevel.Location;

				txt = "Level pos : " + lvlpos.X + "x" + lvlpos.Y;
				pos.Y += Font.LineHeight;
				Font.DrawText(pos, txt);


				if (player != null)
				{
					txt = "Player pos : " + player.Location.X + "x" + player.Location.Y;
					pos.Y += Font.LineHeight;
					Font.DrawText(pos, txt);
				}



				pos = new Point(10, 200);
				pos.Y += Font.LineHeight;
				//Display.DrawText(pos, Video.Time.ElapsedGameTime.ToString());

				pos.Y += Font.LineHeight;
				Font.DrawText(pos, "Jumping : " + player.IsJumping + " - " + player.Jump);
				pos.Y += Font.LineHeight;
				Font.DrawText(pos, "Falling : " + player.IsFalling + "   ");

				pos = new Point(10, 550);
				txt = "Press 'C' to shows/hides collision layer, 'G' to shows/hides grid, 'W' for god mode";
				Font.DrawText(pos, txt);
				txt = "Press F1 to F5 to change level, 'Escape' to quit";
				pos.Y += Font.LineHeight;
				Font.DrawText(pos, txt);
			}


			// Hot spot
			Display.Color = Color.Red;
			Point off = CurrentLevel.LevelToScreen(player.Location);
			Display.Plot(off);
			Display.Color = Color.White;
*/
			#endregion

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
				case Keys.W:
				{
					Layer layer = CurrentLevel.GetLayer("tiles");
					if (layer == null)
						break;

					Entity player = layer.GetEntity("Player");
					if (player == null)
						break;

					player.God = !player.God;

				}
				break;

				// Display grid
				case Keys.G:
				{
					foreach (Layer layer in CurrentLevel.Layers)
					{
						layer.ShowGrid = !layer.ShowGrid;
					}
				}
				break;


				//case Keys.F11:
				//{
				//    Display.WaitVbl = !Display.WaitVbl;
				//}
				//break;

				// 
				case Keys.F12:
				{
					Layer layer = CurrentLevel.GetLayer("tiles");
					if (layer == null)
						break;

					Entity player = layer.GetEntity("Player");
					if (player == null)
						break;

					player.Location = layer.GetSpawnPoint("player1").Location;
				}
				break;


				// Shows some debug
				case Keys.Insert:
				{
					DebugStat = !DebugStat;
				}
				break;

				// Shows collision layer
				case Keys.C:
				{
					Layer layer = CurrentLevel.GetLayer("collision");
					if (layer != null)
						layer.Visible = !CurrentLevel.GetLayer("collision").Visible;
				}
				break;

				// Change to level 1_1 => 1_5
				case Keys.F1:
				{
					CurrentLevel = ResourceManager.CreateAsset<Level>("Level1_1");
					CurrentLevel.Init();
				}
				break;
				case Keys.F2:
				{
					CurrentLevel = ResourceManager.CreateAsset<Level>("Level1_2");
					CurrentLevel.Init();
				}
				break;
				case Keys.F3:
				{
					CurrentLevel = ResourceManager.CreateAsset<Level>("Level1_3");
					CurrentLevel.Init();
				}
				break;
				case Keys.F4:
				{
					CurrentLevel = ResourceManager.CreateAsset<Level>("Level1_4");
					CurrentLevel.Init();
				}
				break;
				case Keys.F5:
				{
					CurrentLevel = ResourceManager.CreateAsset<Level>("Level1_5");
					CurrentLevel.Init();
				}
				break;


			}
		}



		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		TextureFont Font;


		/// <summary>
		/// Shows/Hides debug stats
		/// </summary>
		bool DebugStat;



		/// <summary>
		/// Gets/sets the current level to use
		/// </summary>
		public Level CurrentLevel
		{
			get;
			private set;
		}



		/// <summary>
		/// Layout icons
		/// </summary>
		TileSet Icon;


		#endregion
	}
}
