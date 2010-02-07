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
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Utility.ScreenManager;
using DungeonEye.Gui;

namespace DungeonEye
{
	/// <summary>
	/// Charactere generation
	/// </summary>
	public class CharGen : GameScreen
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public CharGen()
		{
			Heroes = new Hero[4];

			HeroeBoxes = new Rectangle[] 
			{
				new Rectangle(34,  128, 124, 128),
				new Rectangle(162, 128, 124, 128),
				new Rectangle(34,  256, 124, 128),
				new Rectangle(162, 256, 124, 128),
			};
			HeroID = -1;

			BackButton = new Rectangle(528, 344, 76, 32);
		}




		/// <summary>
		/// Loads content
		/// </summary>
		public override void LoadContent()
		{
			ResourceManager.LoadBank("data/chargen.bnk");

			Tileset = ResourceManager.CreateAsset<TileSet>("CharGen");
			Tileset.Scale = new SizeF(2.0f, 2.0f);

			Heads = ResourceManager.CreateAsset<TileSet>("Heads");
			Heads.Scale = new SizeF(2.0f, 2.0f);

			Font = ResourceManager.CreateAsset<Font2d>("intro");
			Font.GlyphTileset.Scale = new SizeF(2.0f, 2.0f);

			PlayButton = new ScreenButton(string.Empty, new Rectangle(48, 362, 166, 32));
			PlayButton.Selected += new EventHandler(PlayButton_Selected);

			StringTable = ResourceManager.CreateAsset<StringTable>("Chargen");
			StringTable.LanguageName = Game.LanguageName;

			Anims = ResourceManager.CreateAsset<Animation>("Animations");
			Anims.TileSet.Scale = new SizeF(2.0f, 2.0f);
			Anims.Play();

			CurrentState = CharGenStates.SelectHero;
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Tileset != null)
				Tileset.Dispose();
			if (Font != null)
				Font.Dispose();
			if (Anims != null)
				Anims.Dispose();

			Tileset = null;
			Font = null;
			Anims = null;
		}


		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PlayButton_Selected(object sender, EventArgs e)
		{
			ScreenManager.AddScreen(new Team());
			ExitScreen();
		}


		#endregion


		#region Updates & Draws


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus"></param>
		/// <param name="isCovered"></param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			if (Keyboard.IsNewKeyPress(Keys.Escape))
				ExitScreen();



			switch (CurrentState)
			{
				#region Select race
				case CharGenStates.SelectRace:
				{
				}
				break;
				#endregion

				#region Select hero
				case CharGenStates.SelectHero:
				if (Mouse.IsButtonDown(MouseButtons.Left))
				{
					for (int id = 0; id < 4; id++)
					{
						if (HeroeBoxes[id].Contains(Mouse.Location))
						{
							HeroID = id;
							Heroes[id] = new Hero(null);
							CurrentState = CharGenStates.SelectClass;
						}
					}
				}
				break;
				#endregion

				#region Select class
				case CharGenStates.SelectClass:
				{
					Point point = new Point(304, 0);
					for (int i = 0; i < 9; i++)
					{
						point.Y = 176 + i *18;
						if (new Rectangle(286, 176 + i * 18, 324, 16).Contains(Mouse.Location) && Mouse.IsNewButtonDown(MouseButtons.Left))
						{
							CurrentHero.Professions.Clear();

							switch (i)
							{
								case 0:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Fighter));
								break;
								case 1:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Ranger));
								break;
								case 2:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Mage));
								break;
								case 3:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Cleric));
								break;
								case 4:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Thief));
								break;
								case 5:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Fighter));
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Thief));
								break;
								case 6:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Fighter));
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Mage));
								break;
								case 7:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Fighter));
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Mage));
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Thief));
								break;
								case 8:
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Thief));
								CurrentHero.Professions.Add(new Profession(0, HeroClass.Mage));
								break;
							}

							CurrentState = CharGenStates.SelectAlignment;
						}


						// Back
						if (BackButton.Contains(Mouse.Location) && Mouse.IsNewButtonDown(MouseButtons.Left))
						{
							Heroes[HeroID] = null;
							CurrentState = CharGenStates.SelectHero;
						}
					}
				
					
				}
				break;
				#endregion

				#region Select alignment
				case CharGenStates.SelectAlignment:
				{
					Point point = new Point(304, 0);
					for (int i = 0; i < 9; i++)
					{
						point.Y = 176 + i * 18;
						if (new Rectangle(286, 176 + i * 18, 324, 16).Contains(Mouse.Location) && Mouse.IsNewButtonDown(MouseButtons.Left))
						{
							EntityAlignment[] alignments = new EntityAlignment[]
							{
								EntityAlignment.LawfulGood,
								EntityAlignment.NeutralGood,
								EntityAlignment.ChaoticGood,
								EntityAlignment.LawfulNeutral,
								EntityAlignment.TrueNeutral,
								EntityAlignment.ChoaticNeutral,
								EntityAlignment.LawfulEvil,
								EntityAlignment.NeutralEvil,
								EntityAlignment.ChaoticEvil,
							};

							CurrentHero.Alignment = alignments[i];
							HeadID = 0;
							CurrentState = CharGenStates.SelectFace;
						}
					}

					// Back
					if (BackButton.Contains(Mouse.Location) && Mouse.IsNewButtonDown(MouseButtons.Left))
						CurrentState = CharGenStates.SelectClass;
				}
				break;
				#endregion

				#region Select face
				case CharGenStates.SelectFace:
				{



					// Back
					if (BackButton.Contains(Mouse.Location) && Mouse.IsNewButtonDown(MouseButtons.Left))
						CurrentState = CharGenStates.SelectAlignment;
				}
				break;
				#endregion


				case CharGenStates.Confirm:
				break;
				case CharGenStates.SelectName:
				break;
			}

			if (Anims != null)
				Anims.Update(time);

			if (PlayButton.Rectangle.Contains(Mouse.Location) && Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left) && IsTeamReadyToPlay)
				PlayButton.OnSelectEntry();
		}


		/// <summary>
		/// 
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;


			// Background
			Tileset.Draw(0, Point.Empty);


			for (int i = 0; i < 4; i++)
			{
				Hero hero = Heroes[i];
				if (hero == null)
					continue;

				Heads.Draw(hero.Head, HeroeBoxes[i].Location);
			}


			switch (CurrentState)
			{
				#region Select race
				case CharGenStates.SelectRace:
				{
				}
				break;
				#endregion

				#region Select hero
				case CharGenStates.SelectHero:
				{
					Font.DrawText(new Rectangle(304, 160, 300, 64), Color.White, StringTable.GetString(1));

					// Team is ready, game can begin...
					if (IsTeamReadyToPlay)
						Tileset.Draw(1, new Point(48, 362));
				}
				break;
				#endregion

				#region Select class
				case CharGenStates.SelectClass:
				{
					Anims.Draw(HeroeBoxes[HeroID].Location);
					Font.DrawText(new Rectangle(304, 140, 300, 64), Color.FromArgb(85, 255, 255), StringTable.GetString(2));

					Point point = new Point(304, 0);
					Color color;
					for (int i = 0; i < 9; i++)
					{
						point.Y = 176 + i *18;
						if (new Rectangle(286, 176 + i * 18, 324, 16).Contains(Mouse.Location))
							color = Color.FromArgb(255, 85, 85);
						else
							color = Color.White;

						Font.DrawText(point, color, StringTable.GetString(i + 3));
					}

					// Back
					Tileset.Draw(3, BackButton.Location);
					Tileset.Draw(12, new Point(BackButton.Location.X + 12, BackButton.Location.Y + 12));
				}
				break;
				#endregion

				#region Select alignment
				case CharGenStates.SelectAlignment:
				{
					Anims.Draw(HeroeBoxes[HeroID].Location);
					Font.DrawText(new Rectangle(304, 140, 300, 64), Color.FromArgb(85, 255, 255), StringTable.GetString(12));

					Point point = new Point(304, 0);
					Color color;
					for (int i = 0; i < 9; i++)
					{
						point.Y = 176 + i * 18;
						if (new Rectangle(286, 176 + i * 18, 324, 16).Contains(Mouse.Location))
							color = Color.FromArgb(255, 85, 85);
						else
							color = Color.White;

						Font.DrawText(point, color, StringTable.GetString(i + 13));
					}

					// Back
					Tileset.Draw(3, BackButton.Location);
					Tileset.Draw(12, new Point(BackButton.Location.X + 12, BackButton.Location.Y + 12));
				}
				break;
				#endregion

				#region Select face
				case CharGenStates.SelectFace:
				{
					Anims.Draw(HeroeBoxes[HeroID].Location);

					Tileset.Draw(3, new Point(288, 132));
					Tileset.Draw(18, new Point(300, 140));
					Tileset.Draw(3, new Point(288, 164));
					Tileset.Draw(19, new Point(300, 172));

					// Faces
					for (int i = 0; i < 4; i++)
					{
						Heads.Draw(HeadID + i, new Point(354 + i * 64, 132));
					}

					// Back
					Tileset.Draw(3, BackButton.Location);
					Tileset.Draw(12, new Point(BackButton.Location.X + 12, BackButton.Location.Y + 12));
				}
				break;
				#endregion

				case CharGenStates.Confirm:
				break;
				case CharGenStates.SelectName:
				break;
			}





			// Draw the cursor or the item in the hand
			Display.Color = Color.White;
			Tileset.Draw(999, Mouse.Location);
		}


		#endregion


		#region Properties


		/// <summary>
		/// Current state
		/// </summary>
		CharGenStates CurrentState;


		/// <summary>
		/// Returns true if the team is ready to play
		/// </summary>
		bool IsTeamReadyToPlay
		{
			get
			{
				for (int id = 0; id < 4; id++)
				{
					if (Heroes[id] == null)
						return false;
				}

				return true;
			}
		}
		
		
		/// <summary>
		/// Tileset
		/// </summary>
		TileSet Tileset;


		/// <summary>
		/// Bitmap font
		/// </summary>
		Font2d Font;


		/// <summary>
		/// Play button
		/// </summary>
		ScreenButton PlayButton;



		/// <summary>
		/// Heroes in the team
		/// </summary>
		Hero[] Heroes;


		/// <summary>
		/// Currently selected hero
		/// </summary>
		Hero CurrentHero
		{
			get
			{
				if (HeroID == -1)
					return null;

				return Heroes[HeroID];
			}
		}


		/// <summary>
		/// ID of the current hero
		/// </summary>
		int HeroID;


		/// <summary>
		/// String table for translations
		/// </summary>
		StringTable StringTable;


		/// <summary>
		/// Animations
		/// </summary>
		Animation Anims;


		/// <summary>
		/// Heroe's box
		/// </summary>
		Rectangle[] HeroeBoxes;

		/// <summary>
		/// Back button
		/// </summary>
		Rectangle BackButton;


		/// <summary>
		/// Hero's heads
		/// </summary>
		TileSet Heads;

		/// <summary>
		/// 
		/// </summary>
		int HeadID;


		#endregion
	}


	/// <summary>
	/// Differents states of the team generation
	/// </summary>
	enum CharGenStates
	{
		SelectRace,
		SelectHero,
		SelectClass,
		SelectAlignment,
		SelectFace,
		Confirm,
		SelectName
	}
}
