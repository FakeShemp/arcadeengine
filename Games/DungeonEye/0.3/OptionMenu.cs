#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
	/// 
	/// </summary>
	public class OptionMenu : GameScreen
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public OptionMenu()
		{
			Buttons = new List<ScreenButton>(3);

		}


		/// <summary>
		/// Load content
		/// </summary>
		public override void LoadContent()
		{
			Tileset = ResourceManager.CreateAsset<TileSet>("Main Menu");
			Tileset.Scale = new SizeF(2.0f, 2.0f);

			Font = ResourceManager.CreateSharedAsset<Font2d>("intro");

			StringTable = ResourceManager.CreateAsset<StringTable>("option");


			// Available languages
			Languages = StringTable.LanguagesList;

			// Available keymaps
			Keymaps = ResourceManager.GetAssets<InputScheme>();



			// Buttons
			Buttons.Add(new ScreenButton("Keyboard : " + Game.InputSchemeName, new Rectangle(156, 324, 340, 14)));
			Buttons[0].Selected += new EventHandler(KeyboardEvent);

			Buttons.Add(new ScreenButton("Language : " + Game.LanguageName, new Rectangle(156, 342, 340, 14)));
			Buttons[1].Selected += new EventHandler(LanguageEvent);

			Buttons.Add(new ScreenButton("Back", new Rectangle(156, 360, 340, 14)));
			Buttons[2].Selected += new EventHandler(BackEvent);

		}


		#region Events

		/// <summary>
		///  Back to the game
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackEvent(object sender, EventArgs e)
		{
			Settings.Save("settings.xml");

			ScreenManager.RemoveScreen(this);
		}



		/// <summary>
		/// Change keyboard settings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void KeyboardEvent(object sender, EventArgs e)
		{
			int id = Keymaps.IndexOf(Game.InputSchemeName) + 1;

			if (id >= Keymaps.Count)
				id = 0;

			Game.InputSchemeName = Keymaps[id];
			Settings.SetToken("inputscheme", Keymaps[id]);
		}



		/// <summary>
		/// Change language
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void LanguageEvent(object sender, EventArgs e)
		{
			int id = Languages.IndexOf(Game.LanguageName) + 1;

			if (id >= Languages.Count)
				id = 0;

			Game.LanguageName = Languages[id];
			Settings.SetToken("language", Languages[id]);
		}

		#endregion


		#region Update & draw


		/// <summary>
		/// Update logic
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isCovered"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			if (!hasFocus)
				return;

			// Does the default language changed ?
			if (StringTable.LanguageName != Game.LanguageName)
				StringTable.LanguageName = Game.LanguageName;

			Buttons[0].Text = StringTable.GetString(1) + Game.InputSchemeName;
			Buttons[1].Text = StringTable.GetString(2) + Game.LanguageName;
			Buttons[2].Text = StringTable.GetString(3);


			Point mousePos = Mouse.Location;
			for (int id = 0; id < Buttons.Count; id++)
			{
				ScreenButton button = Buttons[id];
				if (button.Rectangle.Contains(mousePos))
				{
					//button.TextColor = Color.FromArgb(255, 85, 85);
					MenuID = id;
					if (Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
						button.OnSelectEntry();
				}
				//else
				//{
				//    button.TextColor = Color.White;
				//}
			}

			// Bye bye
			if (Keyboard.IsNewKeyPress(System.Windows.Forms.Keys.Escape))
			{
				Settings.Save("settings.xml");
				ScreenManager.RemoveScreen(this);
			}

			if (Keyboard.IsNewKeyPress(System.Windows.Forms.Keys.Up))
			{
				MenuID--;
				if (MenuID < 0)
					MenuID = Buttons.Count - 1;
			}
			else if (Keyboard.IsNewKeyPress(System.Windows.Forms.Keys.Down))
			{
				MenuID++;
				if (MenuID >= Buttons.Count)
					MenuID = 0;

			}
			else if (Keyboard.IsNewKeyPress(System.Windows.Forms.Keys.Enter))
			{
				Buttons[MenuID].OnSelectEntry();
			}

		}


		/// <summary>
		/// 
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;


			// Background
			Tileset.Draw(1, Point.Empty);


			// Draw buttons
			for (int id = 0; id < Buttons.Count; id++)
			{
				ScreenButton button = Buttons[id];

				Point point = button.Rectangle.Location;

				// Text
				Font.DrawText(point, id == MenuID ? Color.FromArgb(255, 85, 85) : Color.White, button.Text);
			}


			// Version info
			Font.DrawText(new Point(554, 380), Color.White, "V 0.3");

			// Draw the cursor or the item in the hand
			Display.Color = Color.White;
			Tileset.Draw(0, Mouse.Location);

		}

		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		TileSet Tileset;


		/// <summary>
		/// 
		/// </summary>
		Font2d Font;

		/// <summary>
		/// List of buttons
		/// </summary>
		List<ScreenButton> Buttons;


		/// <summary>
		/// Current MenuID
		/// </summary>
		int MenuID;


		/// <summary>
		/// All available languagess
		/// </summary>
		List<string> Languages;


		/// <summary>
		/// All available keyboard layout
		/// </summary>
		List<string> Keymaps;


		/// <summary>
		/// 
		/// </summary>
		StringTable StringTable;

		#endregion

	}
}
