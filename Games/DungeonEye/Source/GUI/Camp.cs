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
using System.Collections.Generic;
using System.Drawing;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using DungeonEye.Gui.CampWindows;

namespace DungeonEye.Gui
{
	/// <summary>
	/// Camp window class
	/// </summary>
	public class Camp
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Camp(Team team)
		{
			Team = team;
			Windows = new Stack<Window>();

			Buttons = new List<ScreenButton>();
		}


		/// <summary>
		/// Initializes the Window
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{

			Font = ResourceManager.CreateSharedAsset<BitmapFont>("intro");


			//BgColor = Color.FromArgb(101, 105, 182);
			Rectangle = new Rectangle(0, 0, 352, 288);


			return true;
		}

		/// <summary>
		/// Shows the camp window
		/// </summary>
		public void Show()
		{
			if (IsVisible)
				return;

			AddWindow(new MainWindow(this));
		}


		/// <summary>
		/// Close the camp window
		/// </summary>
		public void Close()
		{
			if (!IsVisible)
				return;

			Windows.Clear();
		}

		/// <summary>
		/// Adds a window to the stack
		/// </summary>
		/// <param name="window">Window handle</param>
		public void AddWindow(Window window)
		{
			if (window == null)
				return;

			Windows.Push(window);
		}


		#region Update & Draw


		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
			if (!IsVisible)
				return;



			if (Windows.Count > 0)
			{
				// Remove closing windows
				while (Windows.Count > 0 && Windows.Peek().Closing)
					Windows.Pop();
			
	
				if (Windows.Count > 0)
					Windows.Peek().Update(time);
			}
		}


		/// <summary>
		/// Draws the Window
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch)
		{
			if (!IsVisible)
				return;

			if (Windows.Count > 0)
				Windows.Peek().Draw(batch);
		}


		/// <summary>
		/// Draws a beveled rectangle
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		/// <param name="rect">Rectangle</param>
		/// <param name="bg">Background color</param>
		/// <param name="light">Light color</param>
		/// <param name="dark">Dark color</param>
		public void DrawBevel(SpriteBatch batch, Rectangle rect, Color bg, Color light, Color dark)
		{
			batch.FillRectangle(rect, bg);

			Point point = rect.Location;
			Size size = rect.Size;

			batch.FillRectangle(new Rectangle(point.X + 2, point.Y, size.Width - 2, 2), light);
			batch.FillRectangle(new Rectangle(point.X + 4, point.Y + 2, size.Width - 4, 2), light);
			batch.FillRectangle(new Rectangle(rect.Right - 4, point.Y + 4, 2, size.Height - 8), light);
			batch.FillRectangle(new Rectangle(rect.Right - 2, point.Y + 4, 2, size.Height - 6), light);

			batch.FillRectangle(new Rectangle(point.X, point.Y + 2, 2, size.Height - 4), dark);
			batch.FillRectangle(new Rectangle(point.X + 2, point.Y + 4, 2, size.Height - 6), dark);
			batch.FillRectangle(new Rectangle(point.X, point.Y + size.Height - 2, size.Width - 2, 2), dark);
			batch.FillRectangle(new Rectangle(point.X, point.Y + size.Height - 4, size.Width - 4, 2), dark);
		}


		#endregion
		

		#region Properties

		/// <summary>
		/// Team handle
		/// </summary>
		public Team Team
		{
			get;
			private set;
		}

		/// <summary>
		/// List of buttons
		/// </summary>
		protected List<ScreenButton> Buttons;


		/// <summary>
		/// Font to use
		/// </summary>
		public BitmapFont Font
		{
			get;
			private set;
		}


		/// <summary>
		/// Rectangle of the Window
		/// </summary>
		Rectangle Rectangle;


		/// <summary>
		/// Is the camp window visible
		/// </summary>
		public bool IsVisible
		{
			get
			{
				return Windows.Count > 0;
			}
		}


		/// <summary>
		/// Panels
		/// </summary>
		Stack<Window> Windows;

		#endregion
	}


	/// <summary>
	/// GUI colors
	/// </summary>
	struct Colors
	{
		/// <summary>
		/// 
		/// </summary>
		static public Color Dark = Color.FromArgb(52, 52, 81);


		/// <summary>
		/// 
		/// </summary>
		static public Color Light = Color.FromArgb(150, 150, 174);


		/// <summary>
		/// 
		/// </summary>
		static public Color Main = Color.FromArgb(109, 109, 138);

		/// <summary>
		/// 
		/// </summary>
		static public Color Red = Color.FromArgb(255, 85, 85);

	}
}
