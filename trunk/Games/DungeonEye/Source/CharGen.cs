﻿#region Licence
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
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;
using DungeonEye.Gui;

namespace DungeonEye
{
	/// <summary>
	/// Charactere generation
	/// </summary>
	public class CharGen : GameScreen
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public CharGen()
		{

		}




		/// <summary>
		/// Loads content
		/// </summary>
		public override void LoadContent()
		{
			Tileset = ResourceManager.CreateSharedAsset<TileSet>("CharGen");
			Tileset.Scale = new SizeF(2.0f, 2.0f);


			Font = ResourceManager.CreateSharedAsset<Font2d>("intro");
		//	Font.TileSet.Scale = new SizeF(2.0f, 2.0f);

			PlayButton = new ScreenButton(string.Empty, new Rectangle(48, 362, 166, 32));
			PlayButton.Selected += new EventHandler(PlayButton_Selected);
		}



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PlayButton_Selected(object sender, EventArgs e)
		{
			ScreenManager.AddScreen(new Team());
			ExitScreen();
		}


		#endregion


		#region Updates & Draws


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isCovered"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			if (Keyboard.IsNewKeyPress(Keys.Escape))
				ExitScreen();


			if (PlayButton.Rectangle.Contains(Mouse.Location) && Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
				PlayButton.OnSelectEntry();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;


			// Background
			Tileset.Draw(0, Point.Empty);


			Font.Color = Color.White;


			if (CurrentHero == null)
			{
				Font.DrawText("Select the box of", new Point(304, 160));
				Font.DrawText("the character you", new Point(304, 178));
				Font.DrawText("wish to create or", new Point(304, 196));
				Font.DrawText("view.", new Point(304, 212));
			}


			// Team is ready, game can begin...
			if (IsTeamReadyToPlay())
			{
				Tileset.Draw(1, new Point(48, 362));
			}



			// Draw the cursor or the item in the hand
			Display.Color = Color.White;
			Tileset.Draw(999, Mouse.Location);


		}


		#endregion



		/// <summary>
		/// Returns true if the team is ready to play
		/// </summary>
		/// <returns></returns>
		bool IsTeamReadyToPlay()
		{
			return false;
		}



		#region Properties

		/// <summary>
		/// Tileset
		/// </summary>
		TileSet Tileset;


		/// <summary>
		/// 
		/// </summary>
		Font2d Font;


		/// <summary>
		/// Play button
		/// </summary>
		ScreenButton PlayButton;



		/// <summary>
		/// Heroes in the team
		/// </summary>
		Hero[] Heroes;


		/// <summary>
		/// Currently selected hero
		/// </summary>
		Hero CurrentHero;

		#endregion
	}
}