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
using ArcEngine;
using RuffnTumble.Interface;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Input;
using System.Windows.Forms;

namespace RuffnTumble
{
	/// <summary>
	/// The hero of the Game
	/// </summary>
	public class Player : Entity
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Player()
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{


			return true;
		}


		/// <summary>
		/// Update the player logic
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			float speed = 250.0f * time.ElapsedGameTime.Milliseconds / 1000.0f;

			if (Keyboard.IsKeyPress(Keys.Right))
			{
				location.X += speed;
			}
			if (Keyboard.IsKeyPress(Keys.Left))
			{
				location.X -= speed;
			}
			if (Keyboard.IsKeyPress(Keys.Up))
			{
				location.Y -= speed;
			}
			if (Keyboard.IsKeyPress(Keys.Down))
			{
				location.Y += speed;
			}
		}


		/// <summary>
		/// Render the player
		/// </summary>
		public override void Draw(SpriteBatch batch, Camera camera)
		{
			Vector4 pos = new Vector4(Location.X, Location.Y, 32, 64);
			pos.Offset(-camera.Location);
			batch.FillRectangle(pos, Color.Blue);
		}



		#region Properties


		#endregion
	}
}
