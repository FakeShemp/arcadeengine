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
using RuffnTumble;


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
	class Ruff : GameBase
	{
		/// <summary>
		/// Game entry point
		/// </summary>
		/// <param name="args"></param>
		[STAThread]
		static void Main(string[] args)
		{
			Ruff game = new Ruff();
			game.Run();
		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public override void LoadContent()
		{
			CreateGameWindow(new Size(800, 600));
			Window.Text = "Ruff'n'Tumble";

			Gamepad.Init(Window);

			// Loads content
			ResourceManager.AddProvider(new WorldProvider());
			ResourceManager.LoadBank("data/world1.bnk");


			Batch = new SpriteBatch();


			// Sets the level
			World = ResourceManager.CreateAsset<World>("test");
			if (World != null)
			{
				World.Init();
				World.SetLevel("Level_1");
			}



			// Default rendering font
			Font = new BitmapFont();
			Font.LoadTTF("verdana.ttf", 12, FontStyle.Regular);


			Icons = ResourceManager.CreateAsset<TileSet>("Layout");
		}


		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		public override void UnloadContent()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Icons != null)
				Icons.Dispose();
			Icons = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (World != null)
				World.Dispose();
			World = null;
		}



		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		public override void Update(GameTime time)
		{
			// Run the editor
			if (Keyboard.IsKeyPress(Keys.Insert))
				RunEditor();

			// Byebye
			if (Keyboard.IsNewKeyPress(Keys.Escape))
				Exit();


			if (World != null)
				World.Update(time);

			/*

			// Center the collision layer to match the location on the screen
			Layer layer = CurrentWorld.GetLayer("tiles");
			if (layer != null)
			{
				// Center the player in the middle of the screen
				Entity player = layer.GetEntity("Player");
				if (player != null)
				{
					// Level layer
					CurrentWorld.Location = new Point(
							(int)(player.Location.X - Level.ViewPort.Width / 2.0f),
							(int)(player.Location.Y - Level.ViewPort.Height / 2.0f));

				}

			}
			*/


			// Change to level 1_1 => 1_5
			if (Keyboard.IsNewKeyPress(Keys.F1))
			{
				World.SetLevel("Level_1");
			}
			if (Keyboard.IsNewKeyPress(Keys.F2))
			{
				World.SetLevel("Level_2");
			}
			if (Keyboard.IsNewKeyPress(Keys.F3))
			{
				World.SetLevel("Level_3");
			}
			if (Keyboard.IsNewKeyPress(Keys.F4))
			{
				World.SetLevel("Level_4");
			}
			if (Keyboard.IsNewKeyPress(Keys.F5))
			{
				World.SetLevel("Level_5");
			}

			int speed = 4;

			if (Keyboard.IsKeyPress(Keys.Right))
			{
				World.CurrentLevel.Camera.Location.Offset(speed, 0);
			}
			if (Keyboard.IsKeyPress(Keys.Left))
			{
				World.CurrentLevel.Camera.Location.Offset(-speed, 0);
			}
			if (Keyboard.IsKeyPress(Keys.Up))
			{
				World.CurrentLevel.Camera.Location.Offset(0, -speed);
			}
			if (Keyboard.IsKeyPress(Keys.Down))
			{
				World.CurrentLevel.Camera.Location.Offset(0, speed);
			}

		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Batch.Begin();

			// Draw the level
			if (World != null)
				World.Draw(Batch);


			// Draw the status bar
			Batch.DrawTile(Icons, 0, Point.Empty);


			Batch.DrawString(Font, new Point(100, 100), Color.White, "Camera location : " + World.CurrentLevel.Camera.Location.ToString());


			#region Stats
			//Point pos = Point.Empty;
			//Layer layer = CurrentWorld.GetLayer("tiles");
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

			Batch.End();

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
					DebugStat = !DebugStat;
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
		/// Drawing font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Shows/Hides debug stats
		/// </summary>
		bool DebugStat;


		/// <summary>
		/// Gets the world
		/// </summary>
		public World World
		{
			get;
			private set;
		}



		/// <summary>
		/// Layout icons
		/// </summary>
		TileSet Icons;


		#endregion
	}
}
