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
	public class MemorizeSpellsWindow : Window
	{
		/// <summary>
		/// Memorize spell window
		/// </summary>
		public MemorizeSpellsWindow(Camp camp)
			: base(camp, "Memorize Spells :")
		{
			ScreenButton button;

			button = new ScreenButton("Exit", new Rectangle(256, 244, 80, 28));
			button.Selected += new EventHandler(Exit_Selected);
			Buttons.Add(button);

			button = new ScreenButton("Clear", new Rectangle(16, 244, 96, 28));
			button.Selected += new EventHandler(Clear_Selected);
			Buttons.Add(button);

			for (int i = 0 ; i < 6 ; i++)
			{
				button = new ScreenButton((i + 1).ToString(), new Rectangle(22 + i * 54, 32, 40, 46));
				button.Selected += new EventHandler(Level_Selected);
				Buttons.Add(button);
			}



		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			base.Update(time);
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
		void Level_Selected(object sender, EventArgs e)
		{
		}



		/// <summary>
		/// Clear button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Clear_Selected(object sender, EventArgs e)
		{
		}


		#endregion


		#region Properties




		#endregion

	}
}
