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
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;

namespace ArcEngine.Examples.GameState
{
	/// <summary>
	/// Main screen of the game
	/// </summary>
	class GamePlayScreen : GameScreenBase
	{


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			base.Update(time, hasFocus, isCovered);


			if (Keyboard.IsNewKeyPress(Keys.Escape))
			{
				ScreenManager.AddScreen(new BackgroundScreen());
				ScreenManager.AddScreen(new MainScreen());
				ScreenManager.RemoveScreen(this);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			device.ClearColor = Color.Black;
			device.ClearBuffers();

			device.Color = Color.Green;
			device.Rectangle(new Rectangle(100, 100, 200, 200), true);


			device.Color = Color.White;
			ScreenManager.Font.DrawText(new Point(300, 300), "Put game logic here...");
		}
	
	}
}
