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
using System.Drawing;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;

namespace DungeonEye.Gui.CampWindows
{
	/// <summary>
	/// Preference window
	/// </summary>
	public class PreferencesWindow : Window
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PreferencesWindow(Camp camp)
			: base(camp, "Preferences :")
		{
			ScreenButton button;
			button = new ScreenButton("Tunes ar ON", new Rectangle(16, 40, 320, 28));
			button.Selected += new EventHandler(Tunes_Selected);
			Buttons.Add(button);

			button = new ScreenButton("Sounds are ON", new Rectangle(16, 74, 320, 28));
			button.Selected += new EventHandler(Sounds_Selected);
			Buttons.Add(button);

			button = new ScreenButton("Bar Graphs are ON", new Rectangle(16, 108, 320, 28));
			button.Selected += new EventHandler(Bar_Selected);
			Buttons.Add(button);

			button = new ScreenButton("Exit", new Rectangle(256, 244, 80, 28));
			button.Selected += new EventHandler(Exit_Selected);
			Buttons.Add(button);

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
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Tunes_Selected(object sender, EventArgs e)
		{
		}



		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Sounds_Selected(object sender, EventArgs e)
		{
		}



		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Bar_Selected(object sender, EventArgs e)
		{
		}


		#endregion


		#region Properties




		#endregion

	}
}
