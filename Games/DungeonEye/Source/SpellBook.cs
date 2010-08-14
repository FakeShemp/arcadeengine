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
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;
using DungeonEye.Gui;

namespace DungeonEye
{
	/// <summary>
	/// Speel window
	/// </summary>
	public class SpellBook : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public SpellBook()
		{
			SpellLevel = 1;
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

			ResourceManager.RemoveSharedAsset<TileSet>("interface");
			Tileset = null;
		}


		/// <summary>
		/// Loads content
		/// </summary>
		public void LoadContent()
		{
			Font = ResourceManager.CreateAsset<BitmapFont>("inventory");
			Font.GlyphTileset.Scale = new Vector2(2.0f, 2.0f);

			Tileset = ResourceManager.CreateSharedAsset<TileSet>("Interface");
		}



		/// <summary>
		/// Open the Spell Window
		/// </summary>
		/// <param name="hero"></param>
		public void Open(Hero hero)
		{
			Hero = hero;
			IsVisible = true;
		}


		/// <summary>
		/// Close the Spell Window
		/// </summary>
		public void Close()
		{
			IsVisible = false;
		}


		#region Draw


		/// <summary>
		/// Display the window
		/// </summary>
		/// <param name="batch">Spritebatch to use</param>
		public void Draw(SpriteBatch batch)
		{
			if (!IsVisible)
			return;

			// Main window
			batch.DrawTile(Tileset, 23, MainRectangle.Location);

			// Levels
			string[] title = new string[]{"1ST", "2ND", "3RD", "4TH", "5TH"};
			for (int level = 0; level < 5; level++)
			{
				int id = SpellLevel - 1 == level ? 24 : 25;
				batch.DrawTile(Tileset, id, new Point(MainRectangle.X + level * 42, MainRectangle.Top - 18));
				batch.DrawString(Font, new Point(MainRectangle.X + level * 42 + 4, MainRectangle.Top - 18 + 4), Color.White, title[level]);
			}


			// Abort spell
			batch.DrawString(Font, new Point(146, 336), Color.White, "abort spell");
		}




		#endregion



		#region Update

		/// <summary>
		/// Update the window
		/// </summary>
		/// <param name="time">Game time</param>
		public void Update(GameTime time)
		{
			if (!IsVisible)
				return;

			// Left mouse button
			if (Mouse.IsNewButtonDown(MouseButtons.Left))
			{
				for (int level = 0; level < 5; level++)
				{
					if (new Rectangle(MainRectangle.X + level * 42, MainRectangle.Top - 18, 42, 18).Contains(Mouse.Location))
					{
						SpellLevel = level + 1;
					}
				}


				// Abort spell
				if (new Rectangle(MainRectangle.X + 2, MainRectangle.Bottom - 16, MainRectangle.Width - 4, 14).Contains(Mouse.Location))
					IsVisible = false;
			}


		}

		#endregion

		
		
		#region Properties

		/// <summary>
		/// Concerned hero
		/// </summary>
		public Hero Hero
		{
			get;
			private set;
		}

		/// <summary>
		/// Display font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Rectangle of the window
		/// </summary>
		Rectangle MainRectangle = new Rectangle(142, 262, 210, 88);



		/// <summary>
		/// Spell level selected
		/// </summary>
		public int SpellLevel;



		/// <summary>
		/// Shows / hides the spell window
		/// </summary>
		public bool IsVisible
		{
			get;
			private set;
		}


		/// <summary>
		/// Tileset
		/// </summary>
		TileSet Tileset;

		#endregion
	}
}
