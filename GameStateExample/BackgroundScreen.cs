using System;
using System.Collections.Generic;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.ScreenManager;

namespace GameStateExample
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

			BackgroundTexture = Game.Device.CreateTexture();
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
		public override void Draw(VideoRender device)
		{
			BackgroundTexture.Blit(Game.Window.Rectangle, BackgroundTexture.Rectangle);
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
