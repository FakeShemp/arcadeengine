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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.ScreenManager;
using DungeonEye.Gui;


namespace DungeonEye
{
	/// <summary>
	/// 
	/// </summary>
	public class IntroScreen : GameScreen
	{

		/// <summary>
		/// 
		/// </summary>
		public IntroScreen()
		{
			Display.Init();
		}


		/// <summary>
		/// 
		/// </summary>
		public override void LoadContent()
		{
			Animation = ResourceManager.CreateAsset<Animation>("intro");

			Font = ResourceManager.CreateAsset<TextureFont>("intro");
		}





		#region Update & draw


		/// <summary>
		/// Update logic
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isCovered"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			// No focus byebye
			if (!hasFocus)
				return;

			// Bye bye
			if (Keyboard.IsNewKeyPress(Keys.Escape))
				ScreenManager.Game.Exit();

			// Pause animation
			if (Keyboard.IsNewKeyPress(Keys.Space))
				Animation.Pause = !Animation.Pause;

			// Rewind animation
			if (Keyboard.IsNewKeyPress(Keys.Left))
				Animation.Time = TimeSpan.Zero;



			// Update animation 
			Animation.Update(time);
		}


		/// <summary>
		/// Draws the scene
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;


			Animation.Draw();


			// Debug info
			Font.Color = Color.White;
			Font.DrawText(new Point(20, 360), Animation.Time.ToString());
		//	Font.DrawText(new Point(20, 380), Animation.CurrentFrame.ToString());

		}

		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		Animation Animation;


		/// <summary>
		/// 
		/// </summary>
		TextureFont Font;

		#endregion

	}
}
