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
			Display.ClearBuffers();

			Level level = World.CurrentLevel;

			Batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, false);


			// Draw the level
			if (World != null)
				World.Draw(Batch);


			// Draw the status bar
			Batch.DrawTile(Icons, 0, Point.Empty);


			if (level != null)
			{
				Batch.DrawString(Font, new Point(100, 80), Color.White, "Block coordinate: " + level.PositionToBlock(new Vector2(Mouse.Location.X + level.Camera.Location.X, Mouse.Location.Y + level.Camera.Location.Y)).ToString());
				Batch.DrawString(Font, new Point(100, 100), Color.White, "Camera location : " + level.Camera.Location.ToString());
				Batch.DrawString(Font, new Point(100, 120), Color.White, "Velocity : " + level.Player.Velocity.ToString());
				Batch.DrawString(Font, new Point(100, 140), Color.White, "Layer coordinate : " + level.Player.LayerCoordinate.ToString());
				Batch.DrawString(Font, new Point(100, 160), Color.White, "previousBottom : " + level.Player.previousBottom.ToString());

				if (World.CurrentLevel.Player.IsInSlope)
				{
					Batch.DrawString(Font, new Point(100, 200), Color.White, "In slope : " + level.Player.IsInSlope.ToString());
					Batch.DrawString(Font, new Point(100, 220), Color.White, "Offset : " + level.Player.offset.ToString());
				}
				if (World.CurrentLevel.Player.IsClimbing)
					Batch.DrawString(Font, new Point(100, 260), Color.White, "Climbing !");
				
			}

			// Tile under the player
			Point p = level.LevelToScreen(new Vector2(level.Player.LayerCoordinate.X * 32, level.Player.LayerCoordinate.Y * 32));
			Vector4 rect = new Vector4(p.X - level.Camera.ViewPort.Left, p.Y - level.Camera.ViewPort.Top, 32, 32);
			Batch.DrawRectangle(rect, Color.Red);


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
