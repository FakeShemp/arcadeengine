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

namespace DungeonEye.Gui
{
	/// <summary>
	/// This class represents a page in the camp menu
	/// </summary>
	public class Window
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Window()
		{
			Buttons = new List<ScreenButton>();
		}


		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
		}


		/// <summary>
		/// Draws the window
		/// </summary>
		public void Draw()
		{
		}



		#region Properties

		/// <summary>
		/// List of buttons
		/// </summary>
		public List<ScreenButton> Buttons
		{
			get;
			private set;
		}


		#endregion

	}
}
