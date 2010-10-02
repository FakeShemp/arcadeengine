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
using DungeonEye.Gui;

namespace DungeonEye.Gui.CampWindows
{
	/// <summary>
	/// Select a hero by its class
	/// </summary>
	public class SelectHeroByClassWindow : Window
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public SelectHeroByClassWindow(Camp camp)
			: base(camp, "")
		{
			// Adds buttons
			ScreenButton button;

			button = new ScreenButton("Exit", new Rectangle(256, 244, 80, 28));
			button.Selected += new EventHandler(Exit_Selected);
			Buttons.Add(button);

			Interface = ResourceManager.CreateSharedAsset<TileSet>("Interface");

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		public override void Draw(SpriteBatch batch)
		{
			base.Draw(batch);

			// Display message
			batch.DrawString(Camp.Font, new Point(26, 58), Color.White, Message);


			// Draw heroes
			Point pos;
			for (int y = 0 ; y < 3 ; y++)
			{
				for (int x = 0 ; x < 2 ; x++)
				{
					Hero hero = Camp.Team.Heroes[y * 2 + x];
					if (hero == null)
						continue;

					// Hero apply
					if (hero.CheckClass(Filter))
						continue;

					pos = new Point(366 + 144 * x, y * 104 + 2);

					// Ghost name
					batch.DrawTile(Interface, 31, new Point(368 + 144 * x, y * 104 + 4));

					// Draw rectangle around the hero
					// 366, 2 x 130, 104
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			base.Update(time);

			if (Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
			{
				for (int y = 0 ; y < 3 ; y++)
				{
					for (int x = 0 ; x < 2 ; x++)
					{
						Hero hero = Camp.Team.Heroes[y * 2 + x];
						if (hero == null)
							continue;

						// Hero don't apply
						if (!hero.CheckClass(Filter))
							continue;

						if (new Rectangle(368 + x * 144, 4 + y * 104, 126, 100).Contains(Mouse.Location))
						{
							Target = hero;
							Closing = true;
							OnHeroSelected();
							break;
						}
					}
				}
			}
		}



		#region Events


		/// <summary>
		/// Event raised when the menu is selected.
		/// </summary>
		public event EventHandler HeroSelected;


		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public virtual void OnHeroSelected()
		{
			if (HeroSelected != null)
				HeroSelected(this, null);
		}


		#endregion


		#region Events


		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Exit_Selected(object sender, EventArgs e)
		{
			Closing = true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Number of hero who apply to the filter
		/// </summary>
		public int Count
		{
			get
			{
				int count = 0;
				foreach (Hero hero in Camp.Team.Heroes)
				{
					// Hero applies
					if (hero != null && hero.CheckClass(Filter))
						count++;
				}

				return count;
			}
		}


		/// <summary>
		/// Message to display
		/// </summary>
		public string Message
		{
			get;
			set;
		}


		/// <summary>
		/// Selected hero
		/// </summary>
		public Hero Target
		{
			get;
			private set;
		}


		/// <summary>
		/// Class filter
		/// </summary>
		public HeroClass Filter
		{
			get;
			set;
		}


		/// <summary>
		/// Tileset
		/// </summary>
		TileSet Interface;


		#endregion

	}
}
