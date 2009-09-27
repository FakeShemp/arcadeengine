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
using ArcEngine.Utility.ScreenManager;

namespace ArcEngine.Examples.GameState
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
