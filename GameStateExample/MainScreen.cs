using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using ArcEngine.Input;
using System.Windows.Forms;
using ArcEngine.ScreenManager;

using ArcEngine.Asset;



namespace GameStateExample
{
	/// <summary>
	/// Selection menu screen
	/// </summary>
	class MainScreen : GameScreen
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public MainScreen()
		{
			// Create our menu entries.
			MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
			MenuEntry optionsMenuEntry = new MenuEntry("Options");
			MenuEntry exitMenuEntry = new MenuEntry("Exit");

			// Hook up menu event handlers.
			playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
			optionsMenuEntry.Selected += OptionsMenuEntrySelected;
			exitMenuEntry.Selected += OnCancel;

			// Add entries to the menu.
			Menus.Add(playGameMenuEntry);
			Menus.Add(optionsMenuEntry);
			Menus.Add(exitMenuEntry);
		}


		#region Handle Input


		/// <summary>
		/// Event handler for when the Play Game menu entry is selected.
		/// </summary>
		void PlayGameMenuEntrySelected(object sender, EventArgs e)
		{
			ScreenManager.ClearScreens();
			ScreenManager.AddScreen(new GamePlayScreen());

		}


		/// <summary>
		/// Event handler for when the Options menu entry is selected.
		/// </summary>
		void OptionsMenuEntrySelected(object sender, EventArgs e)
		{
			ScreenManager.AddScreen(new OptionScreen());
		}


		/// <summary>
		/// When the user cancels the main menu, ask if they want to exit the sample.
		/// </summary>
		protected override void OnCancel(object sender, EventArgs e)
		{
			ScreenManager.Game.Exit();
		}


		/// <summary>
		/// Event handler for when the user selects ok on the "are you sure
		/// you want to exit" message box.
		/// </summary>
		void ConfirmExitMessageBoxAccepted(object sender, EventArgs e)
		{
		}


		#endregion


		#region Update & draw


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			base.Update(time, hasFocus, isCovered);

			if (!hasFocus)
				return;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		public override void Draw(VideoRender device)
		{
			device.Color = Color.White;
			base.Draw(device);

		}


		#endregion
	}
}
