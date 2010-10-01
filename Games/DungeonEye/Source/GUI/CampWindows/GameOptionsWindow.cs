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
	/// 
	/// </summary>
	public class GameOptionsWindow : Window
	{
		/// <summary>
		/// 
		/// </summary>
		public GameOptionsWindow(Camp camp)
			: base(camp, "Game Options:")
		{
			Load = new ScreenButton("Load Game", new Rectangle(16, 40, 320, 28));
			Load.Selected += new EventHandler(Load_Selected);
			Buttons.Add(Load);

			Save = new ScreenButton("Save Game", new Rectangle(16, 74, 320, 28));
			Save.Selected += new EventHandler(Save_Selected);
			Buttons.Add(Save);

			Drop = new ScreenButton("Drop Character", new Rectangle(16, 108, 320, 28));
			Drop.Selected += new EventHandler(Bar_Selected);
			Buttons.Add(Drop);

			Quit = new ScreenButton("Quit Game", new Rectangle(16, 142, 320, 28));
			Quit.Selected += new EventHandler(Quit_Selected);
			Buttons.Add(Quit);

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
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Load_Selected(object sender, EventArgs e)
		{
		}



		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Save_Selected(object sender, EventArgs e)
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



		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Quit_Selected(object sender, EventArgs e)
		{
			Game.Exit();
		}


		#endregion


		#region Buttons

		ScreenButton Quit;
		ScreenButton Load;
		ScreenButton Save;
		ScreenButton Drop;
		ScreenButton Exit;


		#endregion



		#region Properties




		#endregion

	}
}
