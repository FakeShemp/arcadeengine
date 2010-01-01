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
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Utility.ScreenManager;

namespace ArcEngine.Examples.GameState
{
	/// <summary>
	/// 
	/// </summary>
	class BackgroundScreen : GameScreen
	{

		/// <summary>
		/// 
		/// </summary>
		public BackgroundScreen()
		{
			
		}



		/// <summary>
		/// 
		/// </summary>
		public override void LoadContent()
		{

			BackgroundTexture = GameBase.Device.CreateTexture();
			BackgroundTexture.LoadImage("background.png");
		}


		#region Update and Draw

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isCovered"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			base.Update(time, hasFocus, false);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			BackgroundTexture.Blit(GameBase.Window.Rectangle, BackgroundTexture.Rectangle);
		}


		#endregion


		#region Properties


		/// <summary>
		/// Background texture
		/// </summary>
		Texture BackgroundTexture;

		#endregion
	}
}
