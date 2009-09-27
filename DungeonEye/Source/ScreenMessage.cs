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
using System.Drawing;


namespace ArcEngine.Games.DungeonEye
{

	/// <summary>
	/// Messages to display on the screen
	/// </summary>
	public class ScreenMessage
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="col"></param>
		public ScreenMessage(string msg, Color col)
		{
			Message = msg;
			Color = col;
			Life = 5000;
		}


		/// <summary>
		/// Message to display
		/// </summary>
		public string Message
		{
			get;
			private set;
		}


		/// <summary>
		/// Color to use
		/// </summary>
		public Color Color
		{
			get;
			private set;
		}


		/// <summary>
		/// Life time of the message
		/// </summary>
		public int Life;

	}
}
