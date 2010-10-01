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
using ArcEngine.Input;


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
		/// <param name="camp">Camp window handle</param>
		/// <param name="title">Window title</param>
		public Window(Camp camp, string title)
		{
			Camp = camp;
			Title = title;
			Buttons = new List<ScreenButton>();
			Closing = false;
		}


		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time"></param>
		public virtual void Update(GameTime time)
		{
			UpdateButtons(Buttons);
		}


		/// <summary>
		/// Draws the window
		/// </summary>
		/// <param name="batch"></param>
		public virtual void Draw(SpriteBatch batch)
		{
			// Draw background
			DrawBackground(batch);


			// Draw buttons
			DrawButtons(batch, Buttons);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="buttons"></param>
		protected void DrawButtons(SpriteBatch batch, List<ScreenButton> buttons)
		{

			// Draw buttons
			foreach (ScreenButton button in buttons)
			{
				Camp.DrawBevel(batch, button.Rectangle, Colors.Main, Colors.Light, Colors.Dark);

				// Text
				Point point = button.Rectangle.Location;
				point.Offset(6, 6);
				batch.DrawString(Camp.Font, point, button.TextColor, button.Text);
			}

		}


		/// <summary>
		/// Draw the background window
		/// </summary>
		/// <param name="batch">Spritebatch to use</param>
		protected void DrawBackground(SpriteBatch batch)
		{
			Rectangle rect = new Rectangle(0, 0, 352, 288);
			Camp.DrawBevel(batch, rect, Colors.Main, Colors.Light, Colors.Dark);

			batch.DrawString(Camp.Font, new Point(8, 10), Color.FromArgb(85, 255, 255), Title);
		}


		/// <summary>
		/// Update butons int he window
		/// </summary>
		/// <param name="buttons">List of buttons</param>
		protected void UpdateButtons(List<ScreenButton> buttons)
		{
			Point mousePos = Mouse.Location;
			foreach (ScreenButton button in buttons)
			{
				if (button.Rectangle.Contains(mousePos))
				{
					button.TextColor = Color.FromArgb(255, 85, 85);
					if (Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
						button.OnSelectEntry();
				}
				else
				{
					button.TextColor = Color.White;
				}
			}
		}



		#region Properties


		/// <summary>
		/// Window title
		/// </summary>
		public string Title
		{
			get;
			private set;
		}


		/// <summary>
		/// List of buttons
		/// </summary>
		protected List<ScreenButton> Buttons
		{
			get;
			private set;
		}


		/// <summary>
		/// Sets to true to close the window
		/// </summary>
		public bool Closing
		{
			get;
			protected set;
		}


		/// <summary>
		/// Camp window
		/// </summary>
		protected Camp Camp
		{
			get;
			private set;
		}

		#endregion

	}
}
