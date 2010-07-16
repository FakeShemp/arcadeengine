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
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;


namespace DungeonEye.Gui
{
	/// <summary>
	/// Dialog box window asking for a simple yes no question
	/// </summary>
	public class MessageBox
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		public void Draw(SpriteBatch batch)
		{
			if (batch == null)
				return;

			batch.FillRectangle(new Rectangle(10, 10, 200, 200), Color.FromArgb(101, 105, 182));

		}


		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
		}


		#region Properties

		/// <summary>
		/// Message
		/// </summary>
		public string Message
		{
			get;
			set;
		}


		/// <summary>
		/// Yes string
		/// </summary>
		public string Yes
		{
			get;
			set;
		}

		/// <summary>
		/// No string
		/// </summary>
		public string No
		{
			get;
			set;
		}

		#endregion
	}
}
