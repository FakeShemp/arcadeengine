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
using System.Windows.Forms;
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
			ResourceManager.LoadBank("data/intro.bnk");

			Scene = ResourceManager.CreateAsset<Scene>("intro");
			Scene.Font.GlyphTileset.Scale = new SizeF(2, 2);
			Scene.StringTable.LanguageName = Game.LanguageName;

			Font = ResourceManager.CreateAsset<BitmapFont>("intro");
			Font.GlyphTileset.Scale = new SizeF(2, 2);
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
				ScreenManager.RemoveScreen(this); 
				//ScreenManager.Game.Exit();

			// Pause animation
			if (Keyboard.IsNewKeyPress(Keys.Space))
				Scene.Pause = !Scene.Pause;

			// Rewind animation
			if (Keyboard.IsNewKeyPress(Keys.Left))
				Scene.Time = TimeSpan.Zero;



			// Update animation 
			if (Scene != null)
				Scene.Update(time);
		}


		/// <summary>
		/// Draws the scene
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			if (Scene != null)
			Scene.Draw();


			// Debug info
			if (Font != null)
			{
				Font.DrawText(new Point(20, 160), Color.White, Scene.Time.TotalSeconds.ToString());
			}
		}

		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		Scene Scene;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;

		#endregion

	}
}
