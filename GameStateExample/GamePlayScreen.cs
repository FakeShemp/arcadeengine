using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Input;
using ArcEngine.ScreenManager;

namespace GameStateExample
{
	/// <summary>
	/// Main screen of the game
	/// </summary>
	class GamePlayScreen : GameScreen
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
