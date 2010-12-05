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
using System.Drawing;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;

namespace DungeonEye
{
	/// <summary>
	/// Scripted dialog
	/// </summary>
	public class ScriptedDialog : DialogBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square</param>
		public ScriptedDialog(Square square)
		{
			if (square == null)
				throw new ArgumentNullException("Square is null");

			Square = square;
			Picture = new Texture2D(Event.PictureName);

			if (Event.DisplayBorder)
				Border = new Texture2D("border.png");

		}


		/// <summary>
		/// 
		/// </summary>
		public override void Dispose()
		{
			if (Picture != null)
				Picture.Dispose();
			Picture = null;

			if (Border != null)
				Border.Dispose();
			Border = null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			if (Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
				Exit();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		public override void Draw(SpriteBatch batch)
		{
			// Border
			if (Event.DisplayBorder)
				batch.Draw(Border, Point.Empty, Color.White);

			// Picture
			batch.Draw(Picture, new Point(16, 16), Color.White);


			
			DrawSimpleBevel(batch, DisplayCoordinates.ScriptedDialog);

		}



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Square Square;

		/// <summary>
		/// 
		/// </summary>
		EventSquare Event
		{
			get
			{
				if (Square == null)
					return null;

				if (!(Square.Actor is EventSquare))
					throw new ArgumentException("Bad Actor type");

				return Square.Actor as EventSquare;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		Texture2D Picture;


		/// <summary>
		/// 
		/// </summary>
		Texture2D Border;


		#endregion
	}
}
