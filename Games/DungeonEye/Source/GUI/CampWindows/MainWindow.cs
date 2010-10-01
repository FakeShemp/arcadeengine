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
using DungeonEye.Gui;

namespace DungeonEye.Gui.CampWindows
{
	/// <summary>
	/// 
	/// </summary>
	public class MainWindow : Window
	{
		/// <summary>
		/// 
		/// </summary>
		public MainWindow(Camp camp)
			: base(camp, "Camp :")
		{
			// Adds buttons
			RestParty = new ScreenButton("Rest Party", new Rectangle(16, 40, 320, 28));
			RestParty.Selected += new EventHandler(RestParty_Selected);
			Buttons.Add(RestParty);

			MemorizeSpells = new ScreenButton("Memorize Spells", new Rectangle(16, 74, 320, 28));
			MemorizeSpells.Selected += new EventHandler(MemorizeSpells_Selected);
			Buttons.Add(MemorizeSpells);

			PrayForSpells = new ScreenButton("Pray for Spells", new Rectangle(16, 108, 320, 28));
			PrayForSpells.Selected += new EventHandler(PrayForSpells_Selected);
			Buttons.Add(PrayForSpells);

			ScribeScrolls = new ScreenButton("Scribe Scrolls", new Rectangle(16, 142, 320, 28));
			ScribeScrolls.Selected += new EventHandler(ScribeScrolls_Selected);
			Buttons.Add(ScribeScrolls);

			Preferences = new ScreenButton("Preferences", new Rectangle(16, 176, 320, 28));
			Preferences.Selected += new EventHandler(Preferences_Selected);
			Buttons.Add(Preferences);

			GameOptions = new ScreenButton("Game Options", new Rectangle(16, 210, 320, 28));
			GameOptions.Selected += new EventHandler(GameOptions_Selected);
			Buttons.Add(GameOptions);

			Exit = new ScreenButton("Exit", new Rectangle(256, 244, 80, 28));
			Exit.Selected += new EventHandler(Exit_Selected);
			Buttons.Add(Exit);

		}



		#region Events


		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Exit_Selected(object sender, EventArgs e)
		{
			Closing = true;
		}


		/// <summary>
		/// Game options
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void GameOptions_Selected(object sender, EventArgs e)
		{
			Camp.AddWindow(new GameOptionsWindow(Camp));
		}


		/// <summary>
		/// Preferences
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Preferences_Selected(object sender, EventArgs e)
		{
			Camp.AddWindow(new PreferencesWindow(Camp));
		}



		/// <summary>
		/// ScribeScrolls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ScribeScrolls_Selected(object sender, EventArgs e)
		{
			Camp.AddWindow(new ScribeScrollsWindow(Camp));
		}



		/// <summary>
		/// PrayForSpells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PrayForSpells_Selected(object sender, EventArgs e)
		{
			Camp.AddWindow(new PrayForSpellsWindow(Camp));
		}


		/// <summary>
		/// Memorize spells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MemorizeSpells_Selected(object sender, EventArgs e)
		{
			Camp.AddWindow(new MemorizeSpellsWindow(Camp));
		}



		/// <summary>
		/// Rest Party
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void RestParty_Selected(object sender, EventArgs e)
		{
			Camp.AddWindow(new RestPartyWindow(Camp));
		}

		#endregion


		#region Buttons


		ScreenButton RestParty;
		ScreenButton MemorizeSpells;
		ScreenButton PrayForSpells;
		ScreenButton ScribeScrolls;
		ScreenButton Preferences;
		ScreenButton GameOptions;
		ScreenButton Exit;


		#endregion


		#region Properties




		#endregion

	}
}
