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
using System;

namespace DungeonEye
{
	/// <summary>
	/// Dialog base class
	/// </summary>
	public abstract class DialogBase : IDisposable
	{




		/// <summary>
		/// Closes the dialog
		/// </summary>
		public virtual void Exit()
		{
			Quit = true;
		}



		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public virtual void Update(GameTime time)
		{
		}


		/// <summary>
		/// Draws the Window
		/// </summary>
		/// <param name="batch"></param>
		public virtual void Draw(SpriteBatch batch)
		{
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="rectangle"></param>
		public void DrawDoubleBevel(SpriteBatch batch, Rectangle rectangle)
		{
			DrawDoubleBevel(batch, rectangle, GameColors.Main, GameColors.Light, GameColors.Dark);
		}


		/// <summary>
		/// Draws a beveled rectangle
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		/// <param name="rect">Rectangle</param>
		/// <param name="bg">Background color</param>
		/// <param name="light">Light color</param>
		/// <param name="dark">Dark color</param>
		public void DrawDoubleBevel(SpriteBatch batch, Rectangle rect, Color bg, Color light, Color dark)
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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="rectangle"></param>
		public void DrawSimpleBevel(SpriteBatch batch, Rectangle rectangle)
		{
			DrawSimpleBevel(batch, rectangle, GameColors.Main, GameColors.Light, GameColors.Dark);
		}


		/// <summary>
		/// Draws a beveled rectangle
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		/// <param name="rectangle">Rectangle</param>
		/// <param name="bg">Background color</param>
		/// <param name="light">Light color</param>
		/// <param name="dark">Dark color</param>
		public void DrawSimpleBevel(SpriteBatch batch, Rectangle rectangle, Color bg, Color light, Color dark)
		{
			batch.FillRectangle(rectangle, bg);

			Point point = rectangle.Location;
			Size size = rectangle.Size;

			batch.FillRectangle(new Rectangle(point.X + 2, point.Y, size.Width - 2, 2), light);
			batch.FillRectangle(new Rectangle(rectangle.Right - 2, point.Y + 2, 2, size.Height - 4), light);

			batch.FillRectangle(new Rectangle(point.X, point.Y + 2, 2, size.Height - 2), dark);
			batch.FillRectangle(new Rectangle(point.X + 2, point.Y + size.Height - 2, size.Width - 4, 2), dark);
		}


		/// <summary>
		/// 
		/// </summary>
		public virtual void Dispose()
		{
		}



		#region Properties


		/// <summary>
		/// Dialog is over
		/// </summary>
		public bool Quit
		{
			get;
			private set;
		}

		#endregion
	}
}
