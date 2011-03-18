#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using System.Collections.Generic;
using ArcEngine.Storage;
using System.IO;

namespace ArcEngine.Examples.MonsterBlob
{
	/// <summary>
	/// Monster definition
	/// </summary>
	public class Monster
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="position">Starting location</param>
		/// <param name="speed">Initial speed</param>
		/// <param name="segment">Number of segment</param>
		public Monster(Vector2 position, float speed, int segment)
		{
			Speed = speed;
			Tail = new List<Vector2>();
			Tail.Add(position);

			for (int i = 0; i < segment; i++)
				Tail.Add(new Vector2(position));

			Elasticity = 0.3f;
		}



		/// <summary>
		/// Update the monster
		/// </summary>
		/// <param name="time">Game time</param>
		public void Update(GameTime time)
		{
			Time += Speed / 100.0f;

			Position = new Vector2(
				(15.0f * (float)Math.Sin(Time * 6.0f)) + (512 + (400.0f * (float)Math.Cos(Time / 1.5f))),
				(15.0f * (float)Math.Cos(Time * -6.0f)) + (384 + (300.0f * (float)Math.Sin(Time * 1.3f)))
				);

			Position = new Vector2(Mouse.Location.X, Mouse.Location.Y);

			// Draw the main body
			for (int i = 1; i < Tail.Count; i++)
			{
				// calculate distance between the current point and the previous
				Vector2 distance = new Vector2(Tail[i - 1].X - Tail[i].X, Tail[i - 1].Y - Tail[i].Y);

				if (distance.Length > 7.0f)
				{
					Tail[i] = new Vector2(
						Tail[i].X + (distance.X * Elasticity),
						Tail[i].Y + (distance.Y * Elasticity)
						);
				}

			}

		}


		/// <summary>
		/// Draws the monster
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		/// <param name="texture">Texure handle</param>
		public void Draw(SpriteBatch batch, Texture2D texture)
		{
			if (batch == null || texture == null)
				return;

			Vector4 rect = Vector4.Zero;
			Vector2 origin = new Vector2(texture.Size.Width / 2.0f, texture.Size.Height / 2.0f);
			Vector2 scale = new Vector2(1.0f, 1.0f);
			float rotation = 0.0f;
			Color color = Color.White;

			// Draw the main body
			for (int i = 0; i < Segments - 1; i++)
			{
				color = Color.FromArgb((int)(255 * 0.15f), 0, 200, 150);
				scale = new Vector2(1 + (0.5f * (float)Math.Sin(i * 35.0f)), 1 + (0.5f * (float)Math.Sin(i * 35.0f)));
				batch.Draw(texture, Tail[i], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);

				color = Color.FromArgb((int)(255 * 0.8f), 0, 200, 150);
				scale = new Vector2(0.1f, 0.1f);
				batch.Draw(texture, Tail[i], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);
			}

			// Spikes on tail
			color = Color.FromArgb((int)(255 * 0.8f), 255, 255, 255);
			scale = new Vector2(0.6f, 0.1f);
			origin = new Vector2(0.0f, texture.Size.Height / 2.0f);
			rotation = 10.0f * (float)Math.Sin(Time * 10.0f) + CalculateAngle(Tail[Segments - 1], Tail[Segments - 5]) + 90.0f;
			batch.Draw(texture, Tail[Segments - 1], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);
			rotation = 10.0f * (float)Math.Sin(-Time * 10.0f) + CalculateAngle(Tail[Segments - 1], Tail[Segments - 5]) + 90.0f;
			batch.Draw(texture, Tail[Segments - 1], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);


			// begin looping through the body sections again. Note that we don't want fins
			// on the first and last section because we want other things at those coords.
			for (int i = 1; i < Segments - 2; i++)
			{
				color = Color.FromArgb((int)(255 * 1.0f), 255, 255, 255);
				scale = new Vector2(0.1f + (0.6f * (float)Math.Sin(i * 30.0f)), 0.05f);
				rotation = 33.0f * (float)Math.Sin(Time * 5.0f + i * 30.0f) + CalculateAngle(Tail[i], Tail[i - 1]);
				batch.Draw(texture, Tail[i], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);

				rotation = 33.0f * (float)Math.Sin(-Time * 5.0f - i * 30.0f) + CalculateAngle(Tail[i], Tail[i - 1]) + 180.0f;
				batch.Draw(texture, Tail[i], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);
			}


			// Draws the eyes
			origin = new Vector2(texture.Size.Width / 2.0f, texture.Size.Height / 2.0f);
			color = Color.FromArgb((int)(255 * 0.3f), 255, 0, 0);
			scale = new Vector2(0.6f, 0.6f);
			float angle = CalculateAngle(Tail[0], Tail[1]);
			Vector2 pos = Vector2.Add(Position, new Vector2(7.0f * (float)Math.Cos(angle + 50.0f), 7.0f * (float)Math.Sin(angle + 50.0f)));
			batch.Draw(texture, pos, rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);
			pos = Vector2.Add(Position, new Vector2(7.0f * (float)Math.Cos(angle + 140.0f), 7.0f * (float)Math.Sin(angle + 140.0f)));
			batch.Draw(texture, pos, rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);

			color = Color.FromArgb((int)(255 * 0.5f), 255, 255, 255);
			scale = new Vector2(0.1f, 0.1f);
			pos = Vector2.Add(Position, new Vector2(7.0f * (float)Math.Cos(angle + 50.0f), 7.0f * (float)Math.Sin(angle + 50.0f)));
			batch.Draw(texture, pos, rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);
			pos = Vector2.Add(Position, new Vector2(7.0f * (float)Math.Cos(angle + 140.0f), 7.0f * (float)Math.Sin(angle + 140.0f)));
			batch.Draw(texture, pos, rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);



			// Beaky thing
			scale = new Vector2(0.3f, 0.1f);
			origin = new Vector2(0, texture.Size.Width / 2.0f);
			color = Color.FromArgb((int)(255 * 0.8f), 0, 200, 155);
			angle += 95.0f;
			batch.Draw(texture, Tail[0], rect, color, angle, origin, scale, SpriteEffects.None, 0.0f);

			// Yellow light
			scale = new Vector2(4.0f, 4.0f);
			color = Color.FromArgb((int)(255 * 0.2f), 255, 255, 0);
			origin = new Vector2(texture.Size.Width / 2.0f, texture.Size.Height / 2.0f);
			batch.Draw(texture, Tail[0], rect, color, rotation, origin, scale, SpriteEffects.None, 0.0f);

		}



		/// <summary>
		/// This function calculates and returns the angle between two 2d coordinates
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		float CalculateAngle(Vector2 from, Vector2 to)
		{
			return (float)-Math.Atan2(from.X - to.X, from.Y - to.Y);
		}


		#region Properties

		/// <summary>
		/// Location on the screen
		/// </summary>
		public Vector2 Position
		{
			get
			{
				return Tail[0];
			}
			set
			{
				Tail[0] = value;
			}
		}


		/// <summary>
		/// Movement speed
		/// </summary>
		public float Speed;


		/// <summary>
		/// Body of the monster
		/// </summary>
		List<Vector2> Tail;


		/// <summary>
		/// Number of segment
		/// </summary>
		int Segments
		{
			get
			{
				return Tail.Count;
			}
		}


		/// <summary>
		/// Monster time
		/// </summary>
		float Time;


		/// <summary>
		/// Spring effect
		/// </summary>
		float Elasticity;

		#endregion
	}
}
