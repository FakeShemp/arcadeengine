using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.ScreenManager;

namespace GameStateExample
{
	/// <summary>
	/// 
	/// </summary>
	class OptionScreen : GameScreen
	{

		public OptionScreen()
		{
			// Create our menu entries.
			MenuEntry PortMenuEntry = new MenuEntry("Port : ");
			MenuEntry exitMenuEntry = new MenuEntry("Exit");

			// Hook up menu event handlers.
			PortMenuEntry.Selected += new EventHandler(PortMenuEntry_Selected);
			exitMenuEntry.Selected += OnCancel;

			// Add entries to the menu.
			Menus.Add(PortMenuEntry);
			Menus.Add(exitMenuEntry);

		}



		void PortMenuEntry_Selected(object sender, EventArgs e)
		{
			

		}
	}
}
