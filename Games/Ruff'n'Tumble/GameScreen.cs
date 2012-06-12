#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2012 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;


namespace RuffnTumble
{

	/// <summary>
	/// Dungeon crawler game screen class
	/// </summary>
	public class GameScreen : GameScreenBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public GameScreen()
		{
		}


		/// <summary>
		/// Initialize the team
		/// </summary>
		public override void LoadContent()
		{
			// Sprite batch
			Batch = new SpriteBatch();

			// Sets the first level
			World = ResourceManager.CreateAsset<World>("test");
			if (World != null)
				World.SetLevel("Level_1");



			// Default rendering font
			Font = new BitmapFont();
			Font.LoadTTF(@"C:\Windows\Fonts\verdana.ttf", 12, FontStyle.Regular);


			Icons = ResourceManager.CreateAsset<TileSet>("Layout");

		}


		/// <summary>
		/// Dispose
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


		#region Draws



		/// <summary>
		/// Display team informations
		/// </summary>
		public override void Draw()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();


			Batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, false);


			// Draw the level
			if (World != null)
				World.Draw(Batch);


			// Draw the status bar
			Batch.DrawTile(Icons, 0, Point.Empty);


			if (World != null)
			{
				Batch.DrawString(Font, new Point(100, 100), Color.White, "Camera location : " + World.CurrentLevel.Camera.Location.ToString());
				Batch.DrawString(Font, new Point(100, 120), Color.White, "Camera speed : " + World.CurrentLevel.Camera.speed.ToString());
			}

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


			//Batch.FillRectangle(new Rectangle(10, 10, 200, 200), Color.Red);

			Batch.End();
		}


		#endregion


		#region Updates

		/// <summary>
		/// Update the Team status
		/// </summary>
		/// <param name="time">Time passed since the last call to the last update.</param>
		/// <param name="hasFocus">Put it true if Game Screen is focused</param>
		/// <param name="isCovered">Put it true if Game Screen has something over it</param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{

			// Update the world logic
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

			#region Keyboard

			// Exit
			if (Keyboard.IsNewKeyPress(Keys.Escape))
				Game.Exit();

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
			#endregion
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
