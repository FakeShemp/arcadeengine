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
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;

namespace DungeonEye.Gui
{
	/// <summary>
	/// Camp window class
	/// </summary>
	public class CampWindow
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public CampWindow()
		{
			Buttons = new List<ScreenButton>();
			Message = new MessageBox();
			Message.Yes = "Yes";
			Message.No = "No";
		}


		/// <summary>
		/// Initializes the Window
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{

			Font = ResourceManager.CreateSharedAsset<Font2d>("intro");


			BgColor = Color.FromArgb(101, 105, 182);
			Rectangle = new Rectangle(0, 0, 352, 288);


			// Adds buttons
			RestParty = new ScreenButton("Rest Party", new Rectangle(16, 40, 320, 28));
			RestParty.Selected += new EventHandler(RestParty_Selected);
			Buttons.Add(RestParty);
			MemorizeSpells = new ScreenButton("Memorize Spells", new Rectangle(16, 74, 320, 28));
			MemorizeSpells.Selected += new EventHandler(MemorizeSpells_Selected);
			Buttons.Add(MemorizeSpells);
			PrayForSpells = new ScreenButton("Pray for Spells", new Rectangle(16, 108, 320, 28));
			PrayForSpells.Selected += new EventHandler(PrayForSpells_Selected);
			Buttons.Add(PrayForSpells);
			ScribeScrolls = new ScreenButton("Scribe Scrolls", new Rectangle(16, 142, 320, 28));
			ScribeScrolls.Selected += new EventHandler(ScribeScrolls_Selected);
			Buttons.Add(ScribeScrolls);
			Preferences = new ScreenButton("Preferences", new Rectangle(16, 176, 320, 28));
			Preferences.Selected += new EventHandler(Preferences_Selected);
			Buttons.Add(Preferences);
			GameOptions = new ScreenButton("Game Options", new Rectangle(16, 210, 320, 28));
			GameOptions.Selected += new EventHandler(GameOptions_Selected);
			Buttons.Add(GameOptions);

			Exit = new ScreenButton("Exit", new Rectangle(256, 244, 80, 28));
			Exit.Selected += new EventHandler(Exit_Selected);
			Buttons.Add(Exit);

			return true;
		}


		#region Events


		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Exit_Selected(object sender, EventArgs e)
		{
			IsVisible = false;
		}


		/// <summary>
		/// Game options
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void GameOptions_Selected(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// Preferences
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Preferences_Selected(object sender, EventArgs e)
		{

		}



		/// <summary>
		/// ScribeScrolls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ScribeScrolls_Selected(object sender, EventArgs e)
		{

		}



		/// <summary>
		/// PrayForSpells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PrayForSpells_Selected(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// Memorize spells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MemorizeSpells_Selected(object sender, EventArgs e)
		{

		}



		/// <summary>
		/// Rest Party
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void RestParty_Selected(object sender, EventArgs e)
		{
			
		}

		#endregion


		#region Update


		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
			if (!IsVisible)
				return;

			Point mousePos = Mouse.Location;
			foreach (ScreenButton button in Buttons)
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

		#endregion


		#region Draw


		/// <summary>
		/// Draws the Window
		/// </summary>
		public void Draw()
		{
			if (!IsVisible)
				return;


			DrawBevel(Rectangle, BgColor, Color.FromArgb(138, 146, 207), Color.FromArgb(44, 48, 134));
			Font.DrawText(new Point(8, 10), Color.FromArgb(85, 255, 255), "Camp :");



			// Draw buttons
			foreach (ScreenButton button in Buttons)
			{
				DrawBevel(button.Rectangle, BgColor, Color.FromArgb(138, 146, 207), Color.FromArgb(44, 48, 134));

				// Text
				Point point = button.Rectangle.Location;
				point.Offset(6, 6);
				Font.DrawText(point, button.TextColor, button.Text);
			}

		}


		/// <summary>
		/// Draws a beveled rectangle
		/// </summary>
		/// <param name="rect">Rectangle</param>
		/// <param name="bg">Background color</param>
		/// <param name="light">Light color</param>
		/// <param name="dark">Dark color</param>
		public void DrawBevel(Rectangle rect, Color bg, Color light, Color dark)
		{
			Display.FillRectangle(rect, bg);

			Point point = rect.Location;
			Size size = rect.Size;

			Display.FillRectangle(new Rectangle(point.X + 2, point.Y, size.Width - 2, 2), light);
			Display.FillRectangle(new Rectangle(point.X + 4, point.Y + 2, size.Width - 4, 2), light);
			Display.FillRectangle(new Rectangle(rect.Right - 4, point.Y + 4, 2, size.Height - 8), light);
			Display.FillRectangle(new Rectangle(rect.Right - 2, point.Y + 4, 2, size.Height - 6), light);

			Display.FillRectangle(new Rectangle(point.X, point.Y + 2, 2, size.Height - 4), dark);
			Display.FillRectangle(new Rectangle(point.X + 2, point.Y + 4, 2, size.Height - 6), dark);
			Display.FillRectangle(new Rectangle(point.X, point.Y + size.Height - 2, size.Width - 2, 2), dark);
			Display.FillRectangle(new Rectangle(point.X, point.Y + size.Height - 4, size.Width - 4, 2), dark);
		}


		#endregion


		#region Buttons


		ScreenButton RestParty;
		ScreenButton MemorizeSpells;
		ScreenButton PrayForSpells; 
		ScreenButton ScribeScrolls; 
		ScreenButton Preferences;
		ScreenButton GameOptions;
		ScreenButton Exit;


		#endregion

		
		#region Properties

		/// <summary>
		/// Message box
		/// </summary>
		MessageBox Message;


		/// <summary>
		/// List of buttons
		/// </summary>
		public List<ScreenButton> Buttons;


		/// <summary>
		/// Font to use
		/// </summary>
		Font2d Font;


		/// <summary>
		/// Rectangle of the Window
		/// </summary>
		public Rectangle Rectangle;


		/// <summary>
		/// Shows / hides the Window
		/// </summary>
		public bool IsVisible;


		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor;


		#endregion
	}
}
