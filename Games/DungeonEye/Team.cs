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
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Storage;
using ArcEngine.Utility.ScreenManager;
using DungeonEye.Gui;


namespace DungeonEye
{

	/// <summary>
	/// Represents the player's heroes in the dungeon
	/// </summary>
	public class Team : GameScreen
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="heroes">Heroes team</param>
		public Team(Hero[] heroes)
		{
			Messages = new List<ScreenMessage>();
			CampWindow = new Camp(this);
			TeamSpeed = TimeSpan.FromSeconds(0.15f);
			SpellBook = new SpellBook();

			DrawHPAsBar = true;

			Heroes = new List<Hero>();
			for (int i = 0 ; i < 6 ; i++)
				Heroes.Add(null);

			if (heroes != null)
			{
				for (int i = 0 ; i < heroes.Length ; i++)
					Heroes[i] = heroes[i];
			}
			else
				SaveGame = "data/savegame.xml";
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public override void UnloadContent()
		{
			Trace.WriteDebugLine("[Team] : UnloadContent");

			if (Dungeon != null)
				Dungeon.Dispose();
			Dungeon = null;

			if (OutlinedFont != null)
				OutlinedFont.Dispose();
			OutlinedFont = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Items != null)
				Items.Dispose();
			Items = null;

			if (Heads != null)
				Heads.Dispose();
			Heads = null;

			if (TileSet != null)
			{
				ResourceManager.RemoveSharedAsset<TileSet>("interface");
				TileSet.Dispose();
			}
			TileSet = null;

			if (SpellBook != null)
				SpellBook.Dispose();
			SpellBook = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;



			SaveGame = "";
			Heroes = null;
			SelectedHero = null;
			Messages = null;
			MazeBlock = null;
			Location = null;
			LastMove = DateTime.MinValue;
			Language = null;
			InputScheme = null;
			CampWindow = null;
		}


		/// <summary>
		/// Initialize the team
		/// </summary>
		public override void LoadContent()
		{
			DrawHPAsBar = Settings.GetBool("HPAsBar");

			Batch = new SpriteBatch();

			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Start();

			Trace.WriteLine("Content loaded ({0} ms)", watch.ElapsedMilliseconds);

			// Language
			Language = ResourceManager.CreateAsset<StringTable>("game");
			if (Language == null)
			{
				Trace.WriteLine("ERROR !!! No StringTable defined for the game !!!");
				ExitScreen();
				return;
			}
			Language.LanguageName = Game.LanguageName;

			// Keyboard input scheme
			InputScheme = ResourceManager.CreateAsset<InputScheme>(Game.InputSchemeName);
			if (InputScheme == null)
			{
				Trace.WriteLine("ERROR !!! No InputSchema detected !!!");
				InputScheme = new InputScheme();
				InputScheme["MoveForward"] = Keys.Z;
				InputScheme["MoveBackward"] = Keys.S;
				InputScheme["StrafeLeft"] = Keys.Q;
				InputScheme["StrafeRight"] = Keys.D;
				InputScheme["TurnLeft"] = Keys.A;
				InputScheme["TurnRight"] = Keys.E;
				InputScheme["Inventory"] = Keys.I;
				InputScheme["SelectHero1"] = Keys.D1;
				InputScheme["SelectHero2"] = Keys.D2;
				InputScheme["SelectHero3"] = Keys.D3;
				InputScheme["SelectHero4"] = Keys.D4;
				InputScheme["SelectHero5"] = Keys.D5;
				InputScheme["SelectHero6"] = Keys.D6;
			}
			Trace.WriteLine("InputScheme ({0} ms)", watch.ElapsedMilliseconds);


			// Interface tileset
			TileSet = ResourceManager.CreateSharedAsset<TileSet>("Interface", "Interface");

			// Heroe's heads
			Heads = ResourceManager.CreateAsset<TileSet>("Heads");

			// Items tileset
			Items = ResourceManager.CreateAsset<TileSet>("Items");

			// Fonts
			Font = ResourceManager.CreateSharedAsset<BitmapFont>("inventory", "inventory");
			OutlinedFont = ResourceManager.CreateSharedAsset<BitmapFont>("outline", "outline");

			// Misc init
			CampWindow.Init();
			SpellBook.LoadContent();



			// Loads a saved game
			if (!LoadParty(SaveGame))
			{
				Dungeon = ResourceManager.CreateAsset<Dungeon>("Eye");
				if (Dungeon == null)
				{
					Trace.WriteLine("[Team]Team(): Failed to create default dungeon !");
					throw new NullReferenceException("Dungeon");
				}

				Dungeon.Team = this;
				Dungeon.Init();

				// Set initial location
				Location = new DungeonLocation(Dungeon);
				Teleport(Dungeon.StartLocation);
				Location.Direction = Dungeon.StartLocation.Direction;

				// Select the first hero
				SelectedHero = Heroes[0];
			}


			watch.Stop();
			Trace.WriteLine("Team::LoadContent() finished ! ({0} ms)", watch.ElapsedMilliseconds);
		}


		/// <summary>
		/// Loads a team party
		/// </summary>
		/// <param name="filename">File name to load</param>
		/// <returns>True if loaded</returns>
		public bool LoadParty(string filename)
		{
			if (!System.IO.File.Exists(filename))
				return false;

			XmlDocument xml = new XmlDocument();
			xml.Load(filename);


			Location = null;

			foreach (XmlNode node in xml)
			{
				if (node.Name.ToLower() == "team")
					Load(node);
			}

			if (Dungeon == null)
			{
				Trace.WriteLine("[Team]LoadParty() : Dungeon == NULL !!");
				throw new NullReferenceException("Dungeon");
			}

			Dungeon.Init();
			Teleport(Location);

			SaveGame = filename;

			AddMessage("Party Loaded...", Color.YellowGreen);
			return true;
		}


		/// <summary>
		/// Saves a party progress
		/// </summary>
		/// <param name="filename">File name</param>
		/// <returns></returns>
		public bool SaveParty(string filename)
		{
			try
			{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.OmitXmlDeclaration = false;
				settings.IndentChars = "\t";
				settings.Encoding = System.Text.ASCIIEncoding.ASCII;
				XmlWriter xml = XmlWriter.Create(filename, settings);
				Save(xml);
				xml.Close();

				SaveGame = filename;
				AddMessage("Party saved...", Color.YellowGreen);
			}
			catch (Exception e)
			{
				Trace.WriteLine("[Team] SaveParty() : Failed to save the party (filename = '{0}') => {1} !", filename, e.Message);
				AddMessage("Party NOT saved...", Color.Red);
				return false;
			}
			return true;
		}


		#region IO


		/// <summary>
		/// Loads a party
		/// </summary>
		/// <param name="filename">Xml data</param>
		/// <returns>True if team successfuly loaded, otherwise false</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name.ToLower() != "team")
				return false;


			// Clear the team
			for (int i = 0 ; i < Heroes.Count ; i++)
				Heroes[i] = null;

			// Dispose dungeon
			if (Dungeon != null)
				Dungeon.Dispose();
			Dungeon = null;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "dungeon":
					{
						Dungeon = ResourceManager.CreateAsset<Dungeon>(node.Attributes["name"].Value);
						Dungeon.Team = this;
					}
					break;

					case "location":
					{
						if (Location == null)
							Location = new DungeonLocation(Dungeon);

						Location.Load(node);
					}
					break;


					case "position":
					{
						HeroPosition position = (HeroPosition) Enum.Parse(typeof(HeroPosition), node.Attributes["slot"].Value, true);
						Hero hero = new Hero(this);
						hero.Load(node.FirstChild);
						AddHero(hero, position);
					}
					break;

					case "message":
					{
						AddMessage(node.Attributes["text"].Value, Color.FromArgb(int.Parse(node.Attributes["A"].Value), int.Parse(node.Attributes["R"].Value), int.Parse(node.Attributes["G"].Value), int.Parse(node.Attributes["B"].Value)));
					}
					break;
				}
			}


			SelectedHero = Heroes[0];
			return true;
		}



		/// <summary>
		/// Saves the party
		/// </summary>
		/// <param name="filename">XmlWriter</param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("team");

			writer.WriteStartElement("dungeon");
			writer.WriteAttributeString("name", Location.Dungeon.Name);
			writer.WriteEndElement();
			Location.Save("location", writer);

			// Save each hero
			foreach (Hero hero in Heroes)
			{
				if (hero != null)
				{
					writer.WriteStartElement("position");
					writer.WriteAttributeString("slot", GetHeroPosition(hero).ToString());
					hero.Save(writer);
					writer.WriteEndElement();
				}
			}


			System.ComponentModel.TypeConverter colorConverter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(Color));
			foreach (ScreenMessage message in Messages)
			{
				writer.WriteStartElement("message");
				writer.WriteAttributeString("text", message.Message);
				writer.WriteAttributeString("R", message.Color.R.ToString());
				writer.WriteAttributeString("G", message.Color.G.ToString());
				writer.WriteAttributeString("B", message.Color.B.ToString());
				writer.WriteAttributeString("A", message.Color.A.ToString());
				writer.WriteEndElement();
			}



			writer.WriteEndElement();

			return false;
		}


		#endregion


		#region Draws



		/// <summary>
		/// Display team informations
		/// </summary>
		public override void Draw()
		{

			Batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, false);


			// Draw the current maze
			if (Location.Maze != null)
				Location.Maze.Draw(Batch, Location);


			// The backdrop
			Batch.DrawTile(TileSet, 0, Point.Empty);


			// Display the compass
			Batch.DrawTile(TileSet, 5 + (int) Location.Direction * 3, new Point(228, 262));
			Batch.DrawTile(TileSet, 6 + (int) Location.Direction * 3, new Point(158, 316));
			Batch.DrawTile(TileSet, 7 + (int) Location.Direction * 3, new Point(302, 316));


			// Interfaces
			if (Interface == TeamInterface.Inventory)
				DrawInventory(Batch);

			else if (Interface == TeamInterface.Statistic)
				DrawStatistics(Batch);

			else
			{
				DrawMain(Batch);

				/*
				// Action zones
				for (int id = 0 ; id < 6 ; id++)
				{
					Batch.FillRectangle(InterfaceCoord.PrimaryHand[id], Color.FromArgb(128, Color.Red));
					Batch.FillRectangle(InterfaceCoord.SecondaryHand[id], Color.FromArgb(128, Color.Red));
					Batch.FillRectangle(InterfaceCoord.SelectHero[id], Color.FromArgb(128, Color.Red));
					Batch.FillRectangle(InterfaceCoord.HeroFace[id], Color.FromArgb(128, Color.Red));
				}
				Batch.FillRectangle(InterfaceCoord.TurnLeft, Color.FromArgb(128, Color.Red));
				Batch.FillRectangle(InterfaceCoord.TurnRight, Color.FromArgb(128, Color.Red));
				Batch.FillRectangle(InterfaceCoord.MoveForward, Color.FromArgb(128, Color.Red));
				Batch.FillRectangle(InterfaceCoord.MoveBackward, Color.FromArgb(128, Color.Red));
				Batch.FillRectangle(InterfaceCoord.MoveLeft, Color.FromArgb(128, Color.Red));
				Batch.FillRectangle(InterfaceCoord.MoveRight, Color.FromArgb(128, Color.Red));
				*/



				CampWindow.Draw(Batch);
			}



			// Display the last 3 messages
			int i = 0;
			foreach (ScreenMessage msg in Messages)
			{
				Batch.DrawString(Font, new Point(10, 358 + i * 12), msg.Color, msg.Message);
				i++;
			}


			// Draw the spell window
			SpellBook.Draw(Batch);


			// Draw the cursor or the item in the hand
			if (ItemInHand != null)
				Batch.DrawTile(Items, ItemInHand.TileID, Mouse.Location, 0.5f);


			Batch.End();
		}




		/// <summary>
		/// Draws the right side of the panel
		/// </summary>
		/// <param name="batch"></param>
		private void DrawMain(SpriteBatch batch)
		{
			// Draw heroes
			Point pos;
			for (int y = 0 ; y < 3 ; y++)
			{
				for (int x = 0 ; x < 2 ; x++)
				{
					Hero hero = Heroes[y * 2 + x];
					if (hero == null)
						continue;

					pos = new Point(366 + 144 * x, y * 104 + 2);

					// Backdrop
					batch.DrawTile(TileSet, 17, pos);

					// Head
					if (hero.IsDead)
						batch.DrawTile(TileSet, 4, new Point(pos.X + 2, pos.Y + 20));
					else
						batch.DrawTile(Heads, hero.Head, new Point(pos.X + 2, pos.Y + 20));

					// Hero uncouncious
					if (hero.IsUnconscious)
						batch.DrawTile(TileSet, 2, new Point(pos.X + 4, pos.Y + 20));

					// Name
					if (HeroToSwap == hero)
					{
						batch.DrawString(Font, new Point(pos.X + 6, pos.Y + 6), Color.Red, " Swapping");
					}
					else if (SelectedHero == hero)
					{
						batch.DrawString(Font, new Point(pos.X + 6, pos.Y + 6), Color.White, hero.Name);
					}
					else
					{
						batch.DrawString(Font, new Point(pos.X + 6, pos.Y + 6), Color.Black, hero.Name);
					}

					// HP
					if (DrawHPAsBar)
					{
						float percent = (float) hero.HitPoint.Current / (float) hero.HitPoint.Max;
						Color color = Color.Green;
						if (percent < 0.15)
							color = Color.Red;
						else if (percent < 0.4)
							color = Color.Yellow;

						batch.DrawString(Font, new Point(pos.X + 6, pos.Y + 88), Color.Black, "HP");
						DrawProgressBar(batch, hero.HitPoint.Current, hero.HitPoint.Max, new Rectangle(pos.X + 30, pos.Y + 88, 92, 10), color);
					}
					else
						batch.DrawString(Font, new Point(pos.X + 6, pos.Y + 88), Color.Black, hero.HitPoint.Current + " of " + hero.HitPoint.Max);


					// Hands
					for (int i = 0 ; i < 2 ; i++)
					{
						HeroHand hand = (HeroHand) i;
						int yoffset = i * 32;

						// Primary
						Item item = hero.GetInventoryItem(hand == HeroHand.Primary ? InventoryPosition.Primary : InventoryPosition.Secondary);
						batch.DrawTile(Items, item != null ? item.TileID : 86, new Point(pos.X + 96, pos.Y + 36 + yoffset));

						if (!hero.CanUseHand(hand))
							batch.DrawTile(TileSet, 3, new Point(pos.X + 66, pos.Y + 20 + yoffset));


						// Hero hit a monster a few moment ago
						Attack attack = hero.GetLastAttack(hand);
						if (attack != null)
						{

							if (!hero.CanUseHand(hand))
							{
								// Ghost item
								batch.DrawTile(TileSet, 3, new Point(pos.X + 66, pos.Y + 20 + yoffset));

								// Monster hit ?
								if (attack.Target != null && !attack.IsOutdated(DateTime.Now, 1000))
								{
									batch.DrawTile(TileSet, 21, new Point(pos.X + 64, pos.Y + 20 + yoffset));

									if (attack.IsAHit)
										batch.DrawString(Font, new Point(pos.X + 90, pos.Y + 32 + yoffset), Color.White, attack.Hit.ToString());
									else if (attack.IsAMiss)
										batch.DrawString(Font, new Point(pos.X + 76, pos.Y + 32 + yoffset), Color.White, "MISS");
								}
							}


						}



						HandAction action = hero.GetLastActionResult(hand);
						if (action.Result != ActionResult.Ok && !action.IsOutdated(DateTime.Now, 1000))
						{
							batch.DrawTile(TileSet, 22, new Point(pos.X + 66, pos.Y + 20 + yoffset));

							switch (action.Result)
							{
								case ActionResult.NoAmmo:
								{
									batch.DrawString(Font, new Point(pos.X + 86, pos.Y + 24 + yoffset), Color.White, "NO");
									batch.DrawString(Font, new Point(pos.X + 74, pos.Y + 38 + yoffset), Color.White, "AMMO");
								}
								break;
								case ActionResult.CantReach:
								{
									batch.DrawString(Font, new Point(pos.X + 68, pos.Y + 24 + yoffset), Color.White, "CAN'T");
									batch.DrawString(Font, new Point(pos.X + 68, pos.Y + 38 + yoffset), Color.White, "REACH");
								}
								break;
							}
						}
					}


					// Dead or uncounscious
					if (hero.IsUnconscious || hero.IsDead)
					{
						batch.DrawTile(TileSet, 3, new Point(pos.X + 66, pos.Y + 52));
						batch.DrawTile(TileSet, 3, new Point(pos.X + 66, pos.Y + 20));
					}


					// Hero was hit
					if (hero.LastAttack != null && !hero.LastAttack.IsOutdated(DateTime.Now, 1000))
					{
						batch.DrawTile(TileSet, 20, new Point(pos.X + 24, pos.Y + 66));
						batch.DrawString(Font, new Point(pos.X + 52, pos.Y + 86), Color.White, hero.LastAttack.Hit.ToString());
					}

				}
			}


			// Mini map
			if (Debug)
			{
				Location.Maze.DrawMiniMap(batch, this, new Point(500, 220));

				// Team location
				batch.DrawString(Font, new Point(10, 340), Color.White, Location.ToString());
			}
		}




		/// <summary>
		/// Draws a progress bar
		/// </summary>
		/// <param name="batch">SpriteBatch to use</param>
		/// <param name="value">Current value</param>
		/// <param name="max">Maximum value</param>
		/// <param name="rectangle">Rectangle</param>
		/// <param name="color">Bar color</param>
		public void DrawProgressBar(SpriteBatch batch, int value, int max, Rectangle rectangle, Color color)
		{
			if (value > 0)
			{
				Vector4 zone = new Vector4(
					rectangle.Left + 1,
					rectangle.Top + 1,
					((float) value / (float) max * (rectangle.Width - 1)),
					rectangle.Height - 2
					);
				batch.FillRectangle(zone, color);
			}

			batch.DrawLine(rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom, Color.FromArgb(138, 146, 207));
			batch.DrawLine(rectangle.Left, rectangle.Bottom, rectangle.Right + 2, rectangle.Bottom, Color.FromArgb(138, 146, 207));


			batch.DrawLine(rectangle.Left + 1, rectangle.Top, rectangle.Right + 1, rectangle.Top, Color.FromArgb(44, 48, 138));
			batch.DrawLine(rectangle.Right + 1, rectangle.Top, rectangle.Right + 1, rectangle.Bottom, Color.FromArgb(44, 48, 138));
		}



		/// <summary>
		/// Draws the inventory
		/// </summary>
		/// <param name="batch"></param>
		void DrawInventory(SpriteBatch batch)
		{
			// Background
			batch.DrawTile(TileSet, 18, new Point(352, 0));

			// Name
			batch.DrawString(OutlinedFont, new Point(430, 12), Color.White, SelectedHero.Name);

			// HP and Food
			batch.DrawString(Font, new Point(500, 30), Color.Black, SelectedHero.HitPoint.Current + " of " + SelectedHero.HitPoint.Max);

			// Dead or uncounscious
			if (SelectedHero.IsUnconscious)
			{
				batch.DrawString(OutlinedFont, new Point(450, 316), Color.FromArgb(255, 85, 85), "UNCONSCIOUS");
				batch.DrawTile(TileSet, 2, new Point(360, 4));
			}
			else if (SelectedHero.IsDead)
			{
				batch.DrawString(OutlinedFont, new Point(500, 316), Color.FromArgb(255, 85, 85), "DEAD");
				batch.DrawTile(TileSet, 4, new Point(360, 4));
			}
			else
				batch.DrawTile(Heads, SelectedHero.Head, new Point(360, 4));


			// Food
			if (SelectedHero.Food > 0)
			{
				Color color;
				if (SelectedHero.Food > 50)
					color = Color.Green;
				else if (SelectedHero.Food > 25)
					color = Color.Yellow;
				else
					color = Color.Red;

				batch.FillRectangle(new Rectangle(500, 48, SelectedHero.Food, 10), color);
			}

			// Draw inventory
			int pos = 0;
			for (int y = 94 ; y < 346 ; y += 36)
				for (int x = 376 ; x < 448 ; x += 36)
				{
					if (SelectedHero.GetBackPackItem(pos) != null)
						batch.DrawTile(Items, SelectedHero.GetBackPackItem(pos).TileID, new Point(x, y));

					pos++;
				}


			// Quiver count
			if (SelectedHero.Quiver > 99)
				batch.DrawString(Font, new Point(452, 128), Color.White, "++");
			else
				batch.DrawString(Font, new Point(452, 128), Color.White, SelectedHero.Quiver.ToString());

			// Armor
			if (SelectedHero.GetInventoryItem(InventoryPosition.Armor) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Armor).TileID, new Point(462, 166));

			// Wrists
			if (SelectedHero.GetInventoryItem(InventoryPosition.Wrist) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Wrist).TileID, new Point(464, 206));

			// Primary
			if (SelectedHero.GetInventoryItem(InventoryPosition.Primary) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Primary).TileID, new Point(474, 244));

			// Fingers 1
			if (SelectedHero.GetInventoryItem(InventoryPosition.Ring_Left) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Ring_Left).TileID, new Point(462, 278));

			// Fingers 2
			if (SelectedHero.GetInventoryItem(InventoryPosition.Ring_Right) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Ring_Right).TileID, new Point(486, 278));

			// Feet
			if (SelectedHero.GetInventoryItem(InventoryPosition.Feet) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Feet).TileID, new Point(568, 288));

			// Secondary
			if (SelectedHero.GetInventoryItem(InventoryPosition.Secondary) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Secondary).TileID, new Point(568, 246));

			// Back 1 598,184,36,36
			if (SelectedHero.GetWaistPackItem(0) != null)
				batch.DrawTile(Items, SelectedHero.GetWaistPackItem(0).TileID, new Point(614, 202));

			// Back 2 598,220,36,36
			if (SelectedHero.GetWaistPackItem(1) != null)
				batch.DrawTile(Items, SelectedHero.GetWaistPackItem(1).TileID, new Point(614, 238));

			// Back 3 598,256,36,36
			if (SelectedHero.GetWaistPackItem(2) != null)
				batch.DrawTile(Items, SelectedHero.GetWaistPackItem(2).TileID, new Point(614, 272));

			// Neck 572,146,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Neck) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Neck).TileID, new Point(588, 164));

			// Head 594,106,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Helmet) != null)
				batch.DrawTile(Items, SelectedHero.GetInventoryItem(InventoryPosition.Helmet).TileID, new Point(610, 124));

			/* 
						// Debug draw
						Batch.FillRectangle(InterfaceCoord.PreviousHero, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.CloseInventory, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.NextHero, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.ShowStatistics, Color.FromArgb(128, Color.Red));
						foreach (Rectangle rectangle in InterfaceCoord.BackPack)
							Batch.FillRectangle(rectangle, Color.FromArgb(128, Color.Red));
						foreach (Rectangle rectangle in InterfaceCoord.Rings)
							Batch.FillRectangle(rectangle, Color.FromArgb(128, Color.Red));
						foreach (Rectangle rectangle in InterfaceCoord.Belt)
							Batch.FillRectangle(rectangle, Color.FromArgb(128, Color.Red));

						Batch.FillRectangle(InterfaceCoord.Head, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.Neck, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.SecondaryHandInventory, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.PrimaryHandInventory, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.Feet, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.Wrists, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.Food, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.Armor, Color.FromArgb(128, Color.Red));
						Batch.FillRectangle(InterfaceCoord.Quiver, Color.FromArgb(128, Color.Red));
			*/
		}



		/// <summary>
		/// Draws hero statistic
		/// </summary>
		/// <param name="batch"></param>
		void DrawStatistics(SpriteBatch batch)
		{
			// Background
			batch.DrawTile(TileSet, 18, new Point(352, 0));
			batch.FillRectangle(new Rectangle(360, 70, 182, 30), Color.FromArgb(164, 164, 184));
			batch.FillRectangle(new Rectangle(360, 100, 276, 194), Color.FromArgb(164, 164, 184));
			batch.FillRectangle(new Rectangle(360, 294, 242, 36), Color.FromArgb(164, 164, 184));


			// Hero head
			batch.DrawTile(Heads, SelectedHero.Head, new Point(360, 4));


			batch.DrawString(OutlinedFont, new Point(430, 12), Color.White, SelectedHero.Name);
			batch.DrawString(OutlinedFont, new Point(370, 80), Color.White, "Character info");

			// HP and Food
			batch.DrawString(Font, new Point(500, 30), Color.Black, SelectedHero.HitPoint.Current + " of " + SelectedHero.HitPoint.Max);

			// Food
			Color color;
			if (SelectedHero.Food > 50)
				color = Color.Green;
			else if (SelectedHero.Food > 25)
				color = Color.Yellow;
			else
				color = Color.Red;

			batch.FillRectangle(new Rectangle(498, 48, SelectedHero.Food, 10), color);

			string txt = string.Empty;
			foreach (Profession prof in SelectedHero.Professions)
				txt += prof.Class.ToString() + "/";
			txt = txt.Substring(0, txt.Length - 1);

			batch.DrawString(Font, new Point(366, 110), Color.Black, txt);
			batch.DrawString(Font, new Point(366, 124), Color.Black, SelectedHero.Alignment.ToString());
			batch.DrawString(Font, new Point(366, 138), Color.Black, SelectedHero.Race.ToString());

			batch.DrawString(Font, new Point(366, 166), Color.Black, "Strength");
			batch.DrawString(Font, new Point(366, 180), Color.Black, "Intelligence");
			batch.DrawString(Font, new Point(366, 194), Color.Black, "Wisdom");
			batch.DrawString(Font, new Point(366, 208), Color.Black, "Dexterity");
			batch.DrawString(Font, new Point(366, 222), Color.Black, "Constitution");
			batch.DrawString(Font, new Point(366, 236), Color.Black, "Charisma");
			batch.DrawString(Font, new Point(366, 250), Color.Black, "Armor class");


			batch.DrawString(Font, new Point(552, 166), Color.Black, SelectedHero.Strength.Value.ToString());// + "/" + SelectedHero.MaxStrength.ToString());
			batch.DrawString(Font, new Point(552, 180), Color.Black, SelectedHero.Intelligence.Value.ToString());
			batch.DrawString(Font, new Point(552, 194), Color.Black, SelectedHero.Wisdom.Value.ToString());
			batch.DrawString(Font, new Point(552, 208), Color.Black, SelectedHero.Dexterity.Value.ToString());
			batch.DrawString(Font, new Point(552, 222), Color.Black, SelectedHero.Constitution.Value.ToString());
			batch.DrawString(Font, new Point(552, 236), Color.Black, SelectedHero.Charisma.Value.ToString());
			batch.DrawString(Font, new Point(552, 250), Color.Black, SelectedHero.ArmorClass.ToString());


			batch.DrawString(Font, new Point(470, 270), Color.Black, "EXP");
			batch.DrawString(Font, new Point(550, 270), Color.Black, "LVL");
			int y = 0;
			foreach (Profession prof in SelectedHero.Professions)
			{
				batch.DrawString(Font, new Point(366, 290 + y), Color.Black, prof.Class.ToString());
				batch.DrawString(Font, new Point(460, 290 + y), Color.White, prof.Experience.ToString());
				batch.DrawString(Font, new Point(560, 290 + y), Color.White, prof.Level.ToString());

				y += 12;
			}

		}




		#endregion


		#region Updates

		/// <summary>
		/// Update the Team status
		/// </summary>
		/// <param name="time">Time passed since the last call to the last update.</param>
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			HasMoved = false;


			#region Keyboard

			// Bye bye
			if (Keyboard.IsNewKeyPress(Keys.Escape))
			{
				ExitScreen();
				return;
			}

			// Reload data banks
			if (Keyboard.IsNewKeyPress(Keys.W))
			{
				MazeDisplayCoordinates.Load();
				AddMessage("MazeDisplayCoordinates reloaded...");
			}



			if (Keyboard.IsNewKeyPress(Keys.V))
			{
				ReorderHeroes();
			}


			// Reload data banks
			if (Keyboard.IsNewKeyPress(Keys.R))
			{
				Dungeon = ResourceManager.CreateAsset<Dungeon>("Eye");
				Dungeon.Team = this;
				Dungeon.Init();
				AddMessage("Dungeon reloaded...");
			}


			// AutoMap
			if (Keyboard.IsNewKeyPress(Keys.Tab))
			{
				ScreenManager.AddScreen(new AutoMap(Batch));
			}


			// Debug
			if (Keyboard.IsNewKeyPress(Keys.Space))
				Debug = !Debug;

			// Save team
			if (Keyboard.IsNewKeyPress(Keys.J))
			{
				SaveParty(@"z:\data\savegame.xml");
			}

			// Load team
			if (Keyboard.IsNewKeyPress(Keys.L))
			{
				LoadParty("data/savegame.xml");
			}


			#region Change maze
			// Change maze
			for (int i = 0 ; i < 12 ; i++)
			{
				if (Keyboard.IsNewKeyPress(Keys.F1 + i))
				{
					int id = i + 1;
					string lvl = "0" + id.ToString();
					lvl = "maze_" + lvl.Substring(lvl.Length - 2, 2);

					if (Location.SetMaze(lvl))
						AddMessage("Loading " + lvl + ":" + Location.Maze.Description);

					break;
				}
			}

			// Test maze
			if (Keyboard.IsNewKeyPress(Keys.T))
			{
				Location.SetMaze("test");
				AddMessage("Loading maze test", Color.Blue);
			}

			#endregion


			#region Team move & managment

			// Display inventory
			if (Keyboard.IsNewKeyPress(InputScheme["Inventory"]))
			{
				if (Interface == TeamInterface.Inventory)
					Interface = TeamInterface.Main;
				else
					Interface = TeamInterface.Inventory;
			}


			// Turn left
			if (Keyboard.IsNewKeyPress(InputScheme["TurnLeft"]))
				Location.Direction = Compass.Rotate(Location.Direction, CompassRotation.Rotate270);


			// Turn right
			if (Keyboard.IsNewKeyPress(InputScheme["TurnRight"]))
				Location.Direction = Compass.Rotate(Location.Direction, CompassRotation.Rotate90);


			// Move forward
			if (Keyboard.IsNewKeyPress(InputScheme["MoveForward"]))
				Walk(0, -1);


			// Move backward
			if (Keyboard.IsNewKeyPress(InputScheme["MoveBackward"]))
				Walk(0, 1);


			// Strafe left
			if (Keyboard.IsNewKeyPress(InputScheme["StrafeLeft"]))
				Walk(-1, 0);

			// Strafe right
			if (Keyboard.IsNewKeyPress(InputScheme["StrafeRight"]))
				Walk(1, 0);

			// Select Hero 1
			if (Keyboard.IsNewKeyPress(InputScheme["SelectHero1"]))
				SelectedHero = Heroes[0];

			// Select Hero 2
			if (Keyboard.IsNewKeyPress(InputScheme["SelectHero2"]))
				SelectedHero = Heroes[1];

			// Select Hero 3
			if (Keyboard.IsNewKeyPress(InputScheme["SelectHero3"]))
				SelectedHero = Heroes[2];

			// Select Hero 4
			if (Keyboard.IsNewKeyPress(InputScheme["SelectHero4"]))
				SelectedHero = Heroes[3];

			// Select Hero 5
			if (Keyboard.IsNewKeyPress(InputScheme["SelectHero5"]) && HeroCount >= 5)
				SelectedHero = Heroes[4];

			// Select Hero 6
			if (Keyboard.IsNewKeyPress(InputScheme["SelectHero6"]) && HeroCount >= 6)
				SelectedHero = Heroes[5];
			#endregion


			#endregion


			SquarePosition groundpos = SquarePosition.NorthEast;
			Point mousePos = Mouse.Location;
			Point pos = Point.Empty;

			// Get the block at team position
			Square CurrentBlock = Location.Maze.GetBlock(Location.Coordinate);


			#region Mouse

			#region Left mouse button
			if (Mouse.IsNewButtonDown(MouseButtons.Left) && !CampWindow.IsVisible)
			{

				#region Direction buttons
				// Turn left
				if (InterfaceCoord.TurnLeft.Contains(mousePos))
					Location.Direction = Compass.Rotate(Location.Direction, CompassRotation.Rotate270);

				// MoveForward
				else if (InterfaceCoord.MoveForward.Contains(mousePos))
					Walk(0, -1);

				// Turn right
				else if (InterfaceCoord.TurnRight.Contains(mousePos))
					Location.Direction = Compass.Rotate(Location.Direction, CompassRotation.Rotate90);

				// Move left
				else if (InterfaceCoord.MoveLeft.Contains(mousePos))
					Walk(-1, 0);

				// Backward
				else if (InterfaceCoord.MoveBackward.Contains(mousePos))
				{
					if (!Walk(0, 1))
						AddMessage("You can't go that way.");
				}
				// Move right
				else if (InterfaceCoord.MoveRight.Contains(mousePos))
				{
					if (!Walk(1, 0))
						AddMessage("You can't go that way.");
				}
				#endregion

				#region Camp button
				else if (MazeDisplayCoordinates.CampButton.Contains(mousePos))
				{
					SpellBook.Close();
					CampWindow.Show();
					Interface = TeamInterface.Main;
				}
				#endregion


				#region Gather item on the ground Left

				// Team's feet
				else if (MazeDisplayCoordinates.LeftFeetTeam.Contains(mousePos))
				{
					switch (Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.NorthWest;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.NorthEast;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.SouthEast;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.SouthWest;
						break;
					}
					if (ItemInHand != null)
					{
						if (CurrentBlock.DropItem(groundpos, ItemInHand))
							SetItemInHand(null);
					}
					else
					{
						SetItemInHand(CurrentBlock.CollectItem(groundpos));
					}
				}

				// In front of the team
				else if (!FrontBlock.IsWall && MazeDisplayCoordinates.LeftFrontTeamGround.Contains(mousePos))
				{
					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.SouthWest;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.NorthWest;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.NorthEast;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.SouthEast;
						break;
					}


					if (ItemInHand != null)
					{
						if (FrontBlock.DropItem(groundpos, ItemInHand))
							SetItemInHand(null);
					}
					else
						SetItemInHand(FrontBlock.CollectItem(groundpos));
				}


				#endregion

				#region Gather item on the ground right
				else if (MazeDisplayCoordinates.RightFeetTeam.Contains(mousePos))
				{
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.NorthEast;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.SouthEast;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.SouthWest;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.NorthWest;
						break;
					}

					if (ItemInHand != null)
					{
						if (CurrentBlock.DropItem(groundpos, ItemInHand))
							SetItemInHand(null);
					}
					else
					{
						SetItemInHand(CurrentBlock.CollectItem(groundpos));
						//if (ItemInHand != null)
						//    AddMessage(Language.BuildMessage(2, ItemInHand.Name));

					}
				}

				// In front of the team
				else if (!FrontBlock.IsWall && MazeDisplayCoordinates.RightFrontTeamGround.Contains(mousePos))
				{

					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.SouthEast;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.SouthWest;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.NorthWest;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.NorthEast;
						break;
					}


					if (ItemInHand != null)
					{
						if (FrontBlock.DropItem(groundpos, ItemInHand))
							SetItemInHand(null);
					}
					else
					{
						SetItemInHand(FrontBlock.CollectItem(groundpos));
						//if (ItemInHand != null)
						//    AddMessage(Language.BuildMessage(2, ItemInHand.Name));
					}
				}

				#endregion


				#region Action to process on the front block

				// Click on the block in front of the team
				else if (MazeDisplayCoordinates.FrontBlock.Contains(mousePos))// && FrontBlock.IsWall)
				{
					if (!FrontBlock.OnClick(this, mousePos, FrontWallSide))
					{
						#region Throw an object left side
						if (MazeDisplayCoordinates.ThrowLeft.Contains(mousePos) && ItemInHand != null)
						{
							DungeonLocation loc = new DungeonLocation(Location);
							switch (Location.Direction)
							{
								case CardinalPoint.North:
									loc.Position = SquarePosition.SouthWest;
									break;
								case CardinalPoint.East:
									loc.Position = SquarePosition.NorthWest;
									break;
								case CardinalPoint.South:
									loc.Position = SquarePosition.NorthEast;
									break;
								case CardinalPoint.West:
									loc.Position = SquarePosition.SouthEast;
									break;
							}
							Location.Maze.ThrownItems.Add(new ThrownItem(SelectedHero, ItemInHand, loc, TimeSpan.FromSeconds(0.25), 4));
							SetItemInHand(null);
						}
						#endregion

						#region Throw an object in the right side
						else if (MazeDisplayCoordinates.ThrowRight.Contains(mousePos) && ItemInHand != null)
						{

							DungeonLocation loc = new DungeonLocation(Location);

							switch (Location.Direction)
							{
								case CardinalPoint.North:
								loc.Position = SquarePosition.SouthEast;
								break;
								case CardinalPoint.East:
								loc.Position = SquarePosition.SouthWest;
								break;
								case CardinalPoint.South:
								loc.Position = SquarePosition.NorthWest;
								break;
								case CardinalPoint.West:
								loc.Position = SquarePosition.NorthEast;
								break;
							}

							Location.Maze.ThrownItems.Add(new ThrownItem(SelectedHero, ItemInHand, loc, TimeSpan.FromSeconds(0.25), 4));
							SetItemInHand(null);
						}
						#endregion
					}
				}
				#endregion
			}

			#endregion

			#region Right mouse button

			else if (Mouse.IsNewButtonDown(MouseButtons.Right))
			{
				#region Action to process on the front block

				// Click on the block in front of the team
				if (MazeDisplayCoordinates.Alcove.Contains(mousePos) && FrontBlock.IsWall)
				{
					//FrontBlock.OnClick(this, mousePos, FrontWallSide); 
					SelectedHero.AddToInventory(FrontBlock.CollectAlcoveItem(FrontWallSide));
				}
				#endregion

				#region Gather item on the ground Left

				// Team's feet
				else if (MazeDisplayCoordinates.LeftFeetTeam.Contains(mousePos))
				{
					switch (Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.NorthWest;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.NorthEast;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.SouthEast;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.SouthWest;
						break;
					}
					if (ItemInHand == null)
					{
						Item item = CurrentBlock.CollectItem(groundpos);
						if (item != null)
						{
							if (SelectedHero.AddToInventory(item))
								AddMessage(Language.BuildMessage(2, item.Name));
							else
								CurrentBlock.DropItem(groundpos, item);
						}
					}
				}

				// In front of the team
				else if (MazeDisplayCoordinates.LeftFrontTeamGround.Contains(mousePos) && !FrontBlock.IsWall)
				{
					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.SouthWest;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.NorthWest;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.NorthEast;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.SouthEast;
						break;
					}

					if (ItemInHand == null)
					{
						Item item = FrontBlock.CollectItem(groundpos);

						if (item != null)
						{
							if (SelectedHero.AddToInventory(item))
								AddMessage(Language.BuildMessage(2, item.Name));
							else
								FrontBlock.DropItem(groundpos, item);
						}
					}
				}


				#endregion

				#region Gather item on the ground right
				else if (MazeDisplayCoordinates.RightFeetTeam.Contains(mousePos))
				{
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.NorthEast;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.SouthEast;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.SouthWest;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.NorthWest;
						break;
					}

					if (ItemInHand == null)
					{
						Item item = CurrentBlock.CollectItem(groundpos);
						if (item != null)
						{
							if (SelectedHero.AddToInventory(item))
								AddMessage(Language.BuildMessage(2, item.Name));
							else
								CurrentBlock.DropItem(groundpos, item);
						}
					}
				}

				// In front of the team
				else if (MazeDisplayCoordinates.RightFrontTeamGround.Contains(mousePos) && !FrontBlock.IsWall)
				{

					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = SquarePosition.SouthEast;
						break;
						case CardinalPoint.East:
						groundpos = SquarePosition.SouthWest;
						break;
						case CardinalPoint.South:
						groundpos = SquarePosition.NorthWest;
						break;
						case CardinalPoint.West:
						groundpos = SquarePosition.NorthEast;
						break;
					}

					if (ItemInHand == null)
					{
						Item item = FrontBlock.CollectItem(groundpos);
						if (item != null)
						{
							if (SelectedHero.AddToInventory(item))
								AddMessage(Language.BuildMessage(2, item.Name));
							else
								FrontBlock.DropItem(groundpos, item);
						}
					}
				}

				#endregion
			}

			#endregion

			#endregion


			#region Interface actions

			if (Interface == TeamInterface.Inventory)
			{
				UpdateInventory(time);
			}
			else if (Interface == TeamInterface.Statistic)
			{
				UpdateStatistics(time);
			}
			else
			{
				#region Left mouse button action


				// Left mouse button down
				if (Mouse.IsNewButtonDown(MouseButtons.Left) && !CampWindow.IsVisible)
				{

					Item item = null;

					// Update each hero interface
					for (int id = 0 ; id < 6 ; id++)
					{
						// Get the hero
						Hero hero = Heroes[id];
						if (hero == null)
							continue;

						// Select hero
						if (InterfaceCoord.SelectHero[id].Contains(mousePos))
							SelectedHero = hero;


						// Swap hero
						if (InterfaceCoord.HeroFace[id].Contains(mousePos))
						{
							SelectedHero = hero;
							HeroToSwap = null;
							Interface = TeamInterface.Inventory;
						}


						// Take object in primary hand
						if (InterfaceCoord.PrimaryHand[id].Contains(mousePos)) // && hero.CanUseHand(HeroHand.Primary))
						{
							item = hero.GetInventoryItem(InventoryPosition.Primary);

							if (ItemInHand != null && ((ItemInHand.Slot & BodySlot.Primary) == BodySlot.Primary))
							{
								Item swap = ItemInHand;
								SetItemInHand(hero.GetInventoryItem(InventoryPosition.Primary));
								hero.SetInventoryItem(InventoryPosition.Primary, swap);
							}
							else if (ItemInHand == null && item != null)
							{
								SetItemInHand(item);
								hero.SetInventoryItem(InventoryPosition.Primary, null);
							}
						}

						// Take object in secondary hand
						if (InterfaceCoord.SecondaryHand[id].Contains(mousePos)) // && hero.CanUseHand(HeroHand.Secondary))
						{
							item = hero.GetInventoryItem(InventoryPosition.Secondary);

							if (ItemInHand != null && ((ItemInHand.Slot & BodySlot.Secondary) == BodySlot.Secondary))
							{
								Item swap = ItemInHand;
								SetItemInHand(hero.GetInventoryItem(InventoryPosition.Secondary));
								hero.SetInventoryItem(InventoryPosition.Secondary, swap);

							}
							else if (ItemInHand == null && item != null)
							{
								SetItemInHand(item);
								hero.SetInventoryItem(InventoryPosition.Secondary, null);
							}
						}
					}


				}

				#endregion

				#region Right mouse button action

				// Right mouse button down
				if (Mouse.IsNewButtonDown(MouseButtons.Right) && !CampWindow.IsVisible)
				{

					// Update each hero interface
					for (int id = 0 ; id < 6 ; id++)
					{
						// Get the hero
						Hero hero = Heroes[id];
						if (hero == null)
							continue;


						#region Swap hero
						if (InterfaceCoord.SelectHero[id].Contains(mousePos))
						{
							if (HeroToSwap == null)
							{
								HeroToSwap = hero;
							}
							else
							{
								Heroes[(int) GetHeroPosition(HeroToSwap)] = hero;
								Heroes[id] = HeroToSwap;


								HeroToSwap = null;
							}
						}
						#endregion

						#region Show Hero inventory
						if (InterfaceCoord.HeroFace[id].Contains(mousePos))
						{
							SelectedHero = hero;
							Interface = TeamInterface.Inventory;
						}
						#endregion

						#region Use object in primary hand
						if (InterfaceCoord.PrimaryHand[id].Contains(mousePos) && hero.CanUseHand(HeroHand.Primary))
							hero.UseHand(HeroHand.Primary);

						#endregion

						#region Use object in secondary hand
						if (InterfaceCoord.SecondaryHand[id].Contains(mousePos) && hero.CanUseHand(HeroHand.Secondary))
							hero.UseHand(HeroHand.Secondary);
						#endregion
					}
				}
				#endregion
			}

			CampWindow.Update(time);

			#endregion


			#region Heros update

			// Update all heroes
			foreach (Hero hero in Heroes)
			{
				if (hero != null)
					hero.Update(time);
			}
			#endregion


			#region Spell window

			SpellBook.Update(time);

			#endregion


			#region Screen messages update

			// Remove older screen messages and display them
			while (Messages.Count > 3)
				Messages.RemoveAt(0);

			foreach (ScreenMessage msg in Messages)
				msg.Life -= time.ElapsedGameTime.Milliseconds;

			Messages.RemoveAll(
				delegate(ScreenMessage msg)
				{
					return msg.Life < 0;
				}
			);

			#endregion


			// Update the dungeon
			Dungeon.Update(time);


			if (CanMove && MazeBlock != null)
				MazeBlock.OnTeamStand(this);
		}


		/// <summary>
		/// Updates inventory
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		void UpdateInventory(GameTime time)
		{
			Point mousePos = Mouse.Location;
			Item item = null;


			if (Mouse.IsNewButtonDown(MouseButtons.Left))
			{
				// Close inventory
				if (InterfaceCoord.CloseInventory.Contains(mousePos))
					Interface = TeamInterface.Main;


				// Show statistics
				if (InterfaceCoord.ShowStatistics.Contains(mousePos))
					Interface = TeamInterface.Statistic;


				// Previous Hero
				if (InterfaceCoord.PreviousHero.Contains(mousePos))
					SelectedHero = GetPreviousHero();

				// Next Hero
				if (InterfaceCoord.NextHero.Contains(mousePos))
					SelectedHero = GetNextHero();



				#region Manage bag pack items
				for (int id = 0 ; id < 14 ; id++)
				{
					if (InterfaceCoord.BackPack[id].Contains(mousePos))
					{
						Item swap = ItemInHand;
						SetItemInHand(SelectedHero.GetBackPackItem(id));
						SelectedHero.SetBackPackItem(id, swap);
					}
				}
				#endregion

				#region Quiver
				if (InterfaceCoord.Quiver.Contains(mousePos))
				{
					if (ItemInHand == null && SelectedHero.Quiver > 0)
					{
						SelectedHero.Quiver--;
						SetItemInHand(ResourceManager.CreateAsset<Item>("Arrow"));
					}
					else if (ItemInHand != null && (ItemInHand.Slot & BodySlot.Quiver) == BodySlot.Quiver)
					{
						SelectedHero.Quiver++;
						SetItemInHand(null);
					}
				}
				#endregion

				#region Armor
				else if (InterfaceCoord.Armor.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Armor);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Armor, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null && item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Armor, null);
					}
				}
				#endregion

				#region Food
				else if (InterfaceCoord.Food.Contains(mousePos))
				{
					if (ItemInHand != null && ItemInHand.Type == ItemType.Consumable)
					{
						if (ItemInHand.Script.Instance != null)
							ItemInHand.Script.Instance.OnUse(ItemInHand, SelectedHero);
						SetItemInHand(null);
					}
				}
				#endregion

				#region Wrists
				else if (InterfaceCoord.Wrist.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Wrist);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Wrist, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null && item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Wrist, null);
					}
				}
				#endregion

				#region Primary
				else if (InterfaceCoord.PrimaryHandInventory.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Primary);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Primary, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null &&item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Primary, null);
					}
				}
				#endregion

				#region Feet
				else if (InterfaceCoord.Feet.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Feet);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Feet, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null && item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Feet, null);
					}
				}
				#endregion

				#region Secondary
				else if (InterfaceCoord.SecondaryHandInventory.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Secondary);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Secondary, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null && item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Secondary, null);
					}
				}
				#endregion

				#region Neck
				else if (InterfaceCoord.Neck.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Neck);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Neck, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null && item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Neck, null);
					}
				}
				#endregion

				#region Head
				else if (InterfaceCoord.Head.Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Helmet);

					if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Helmet, ItemInHand))
						SetItemInHand(item);
					else if (ItemInHand == null && item != null)
					{
						SetItemInHand(item);
						SelectedHero.SetInventoryItem(InventoryPosition.Helmet, null);
					}
				}
				#endregion

				else
				{
					#region Belt
					for (int id = 0 ; id < 3 ; id++)
					{
						if (InterfaceCoord.Waist[id].Contains(mousePos))
						{
							item = SelectedHero.GetWaistPackItem(id);

							if (ItemInHand != null && SelectedHero.SetWaistPackItem(id, ItemInHand))
								SetItemInHand(item);
							else if (ItemInHand == null && item != null)
							{
								SetItemInHand(item);
								SelectedHero.SetWaistPackItem(id, null);
							}
						}
					}
					#endregion

					#region Rings
					for (int id = 0 ; id < 2 ; id++)
					{
						item = SelectedHero.GetInventoryItem(InventoryPosition.Ring_Left + id);

						if (InterfaceCoord.Rings[id].Contains(mousePos))
						{
							if (ItemInHand != null && SelectedHero.SetInventoryItem(InventoryPosition.Ring_Left + id, ItemInHand))
								SetItemInHand(item);
							else if (ItemInHand == null && item != null)
							{
								SetItemInHand(item);
								SelectedHero.SetInventoryItem(InventoryPosition.Ring_Left + id, null);
							}
						}
					}
					#endregion
				}
			}



		}


		/// <summary>
		/// Updates statistics
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		void UpdateStatistics(GameTime time)
		{
			Point mousePos = Mouse.Location;

			if (Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
			{

				// Close inventory
				if (InterfaceCoord.CloseInventory.Contains(mousePos))
					Interface = TeamInterface.Main;


				// Show statistics
				if (InterfaceCoord.ShowStatistics.Contains(mousePos))
					Interface = TeamInterface.Inventory;



				// Previous Hero
				if (InterfaceCoord.PreviousHero.Contains(mousePos))
					SelectedHero = GetPreviousHero();

				// Next Hero
				if (InterfaceCoord.NextHero.Contains(mousePos))
					SelectedHero = GetNextHero();

			}
		}


		#endregion


		#region Misc


		/// <summary>
		/// Does the Team can see this place
		/// </summary>
		/// <param name="maze">Maze</param>
		/// <param name="location">Place to see</param>
		/// <returns>True if can see the location</returns>
		/// http://tom.cs.byu.edu/~455/3DDDA.pdf
		/// http://www.tar.hu/gamealgorithms/ch22lev1sec1.html
		/// http://www.cse.yorku.ca/~amana/research/grid.pdf
		/// http://www.siggraph.org/education/materials/HyperGraph/scanline/outprims/drawline.htm#dda
		public bool CanSee(Maze maze, Point location)
		{
			Point dist = Point.Empty;

			// Not the same maze
			if (Location.Maze != maze)
				return false;


			Rectangle rect = Rectangle.Empty;
			switch (Location.Direction)
			{
				case CardinalPoint.North:
				{
					if (!new Rectangle(Location.Coordinate.X - 1, Location.Coordinate.Y - 3, 3, 4).Contains(location) &&
						location != new Point(Location.Coordinate.X - 3, Location.Coordinate.Y - 3) &&
						location != new Point(Location.Coordinate.X - 2, Location.Coordinate.Y - 3) &&
						location != new Point(Location.Coordinate.X - 2, Location.Coordinate.Y - 2) &&
						location != new Point(Location.Coordinate.X + 3, Location.Coordinate.Y - 3) &&
						location != new Point(Location.Coordinate.X + 2, Location.Coordinate.Y - 3) &&
						location != new Point(Location.Coordinate.X + 2, Location.Coordinate.Y - 2))
						return false;

					// Is there a wall between the Team and the location
					int dx = location.X - Location.Coordinate.X;
					int dy = location.Y - Location.Coordinate.Y;
					float delta = (float) dy / (float) dx;
					float y = 0;
					for (int pos = Location.Coordinate.Y ; pos >= location.Y ; pos--)
					{
						if (Location.Maze.GetBlock(new Point(pos, Location.Coordinate.Y + (int) y)).Type == SquareType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

				case CardinalPoint.South:
				{
					if (!new Rectangle(Location.Coordinate.X - 1, Location.Coordinate.Y, 3, 4).Contains(location) &&
						location != new Point(Location.Coordinate.X - 3, Location.Coordinate.Y + 3) &&
						location != new Point(Location.Coordinate.X - 2, Location.Coordinate.Y + 3) &&
						location != new Point(Location.Coordinate.X - 2, Location.Coordinate.Y + 2) &&
						location != new Point(Location.Coordinate.X + 3, Location.Coordinate.Y + 3) &&
						location != new Point(Location.Coordinate.X + 2, Location.Coordinate.Y + 3) &&
						location != new Point(Location.Coordinate.X + 2, Location.Coordinate.Y + 2))
						return false;


					// Is there a wall between the Team and the location
					int dx = location.X - Location.Coordinate.X;
					int dy = location.Y - Location.Coordinate.Y;
					float delta = (float) dy / (float) dx;
					float y = 0;
					for (int pos = Location.Coordinate.Y ; pos <= location.Y ; pos++)
					{
						if (Location.Maze.GetBlock(new Point(pos, Location.Coordinate.Y + (int) y)).Type == SquareType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

				case CardinalPoint.West:
				{
					if (!new Rectangle(Location.Coordinate.X - 3, Location.Coordinate.Y - 1, 4, 3).Contains(location) &&
						location != new Point(Location.Coordinate.X - 3, Location.Coordinate.Y - 3) &&
						location != new Point(Location.Coordinate.X - 3, Location.Coordinate.Y - 2) &&
						location != new Point(Location.Coordinate.X - 2, Location.Coordinate.Y - 2) &&
						location != new Point(Location.Coordinate.X - 3, Location.Coordinate.Y + 3) &&
						location != new Point(Location.Coordinate.X - 3, Location.Coordinate.Y + 2) &&
						location != new Point(Location.Coordinate.X - 2, Location.Coordinate.Y + 2))
						return false;



					// Is there a wall between the Team and the location
					int dx = location.X - Location.Coordinate.X;
					int dy = location.Y - Location.Coordinate.Y;
					float delta = (float) dy / (float) dx;
					float y = 0;
					for (int pos = Location.Coordinate.X ; pos >= location.X ; pos--)
					{
						if (Location.Maze.GetBlock(new Point(pos, Location.Coordinate.Y + (int) y)).Type == SquareType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

				case CardinalPoint.East:
				{
					if (!new Rectangle(Location.Coordinate.X, Location.Coordinate.Y - 1, 4, 3).Contains(location) &&
						location != new Point(Location.Coordinate.X + 3, Location.Coordinate.Y - 3) &&
						location != new Point(Location.Coordinate.X + 3, Location.Coordinate.Y - 2) &&
						location != new Point(Location.Coordinate.X + 2, Location.Coordinate.Y - 2) &&
						location != new Point(Location.Coordinate.X + 3, Location.Coordinate.Y + 3) &&
						location != new Point(Location.Coordinate.X + 3, Location.Coordinate.Y + 2) &&
						location != new Point(Location.Coordinate.X + 2, Location.Coordinate.Y + 2))
						return false;

					// Is there a wall between the Team and the location
					int dx = location.X - Location.Coordinate.X;
					int dy = location.Y - Location.Coordinate.Y;
					float delta = (float) dy / (float) dx;
					float y = 0;
					for (int pos = Location.Coordinate.X ; pos <= location.X ; pos++)
					{
						if (Location.Maze.GetBlock(new Point(pos, Location.Coordinate.Y + (int) y)).Type == SquareType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

			}

			return false;
		}



		/// <summary>
		/// Returns the distance between the Team and a location
		/// </summary>
		/// <param name="location">Location to check</param>
		/// <returns>Distance with the Team</returns>
		public Point Distance(Point location)
		{
			return new Point(location.X - Location.Coordinate.X, location.Y - Location.Coordinate.Y);
		}

		#endregion


		#region Messages

		/// <summary>
		/// Adds a message to the interface
		/// </summary>
		/// <param name="msg">Message</param>
		public void AddMessage(string msg)
		{
			Messages.Add(new ScreenMessage(msg, Color.White));
		}


		/// <summary>
		/// Adds a messgae to the interface
		/// </summary>
		/// <param name="msg">Message</param>
		/// <param name="color">Color</param>
		public void AddMessage(string msg, Color color)
		{
			Messages.Add(new ScreenMessage(msg, color));
		}


		#endregion


		#region Heroes



		/// <summary>
		/// Make damage to the whole team
		/// </summary>
		/// <param name="damage">Attack roll</param>
		/// <param name="type">Type of saving throw</param>
		/// <param name="difficulty">Difficulty</param>
		public void Damage(Dice damage, SavingThrowType type, int difficulty)
		{
			foreach (Hero hero in Heroes)
				if (hero != null)
					hero.Damage(damage, type, difficulty);
		}


		/// <summary>
		/// Add experience to the whole team
		/// </summary>
		/// <param name="amount">Amount to be distributed among the entire team</param>
		public void AddExperience(int amount)
		{
			if (amount == 0)
				return;

			int value = amount / HeroCount;
			foreach (Hero hero in Heroes)
				if (hero != null)
					hero.AddExperience(value);
		}


		/// <summary>
		/// Hit all heroes in the team
		/// </summary>
		/// <param name="damage">Amount of damage</param>
		public void Hit(int damage)
		{
			foreach (Hero hero in Heroes)
			{
				if (hero != null)
					hero.HitPoint.Current -= damage;
			}
		}


		/// <summary>
		/// Returns next hero in the team
		/// </summary>
		/// <returns></returns>
		Hero GetNextHero()
		{
			int i = 0;
			for (i = 0 ; i < HeroCount ; i++)
			{
				if (Heroes[i] == SelectedHero)
				{
					i++;
					if (i == HeroCount)
						i = 0;

					break;
				}
			}

			return Heroes[i];
		}


		/// <summary>
		/// Returns previous hero
		/// </summary>
		/// <returns></returns>
		Hero GetPreviousHero()
		{
			int i = 0;
			for (i = 0 ; i < HeroCount ; i++)
			{
				if (Heroes[i] == SelectedHero)
				{
					i--;
					if (i < 0)
						i = HeroCount - 1;

					break;
				}
			}

			return Heroes[i];
		}


		/// <summary>
		/// Returns the position of a hero in the team
		/// </summary>
		/// <param name="hero">Hero handle</param>
		/// <returns>Position of the hero in the team</returns>
		public HeroPosition GetHeroPosition(Hero hero)
		{
			int pos = -1;

			for (int id = 0 ; id < Heroes.Count ; id++)
			{
				if (Heroes[id] == hero)
				{
					pos = id;
					break;
				}
			}

			if (pos == -1)
				throw new ArgumentOutOfRangeException("hero");

			return (HeroPosition) pos;
		}


		/// <summary>
		/// Gets if the hero is in front row
		/// </summary>
		/// <param name="hero"></param>
		/// <returns></returns>
		public bool IsHeroInFront(Hero hero)
		{
			return GetHeroFromPosition(HeroPosition.FrontLeft) == hero || GetHeroFromPosition(HeroPosition.FrontRight) == hero;
		}


		/// <summary>
		/// Returns the ground position of a hero
		/// </summary>
		/// <param name="Hero">Hero handle</param>
		/// <returns>Ground position of the hero</returns>
		public SquarePosition GetHeroGroundPosition(Hero Hero)
		{
			SquarePosition groundpos = SquarePosition.Center;


			// Get the hero position in the team
			HeroPosition pos = GetHeroPosition(Hero);


			switch (Location.Direction)
			{
				case CardinalPoint.North:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = SquarePosition.NorthWest;
					else if (pos == HeroPosition.FrontRight)
						groundpos = SquarePosition.NorthEast;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = SquarePosition.SouthWest;
					else
						groundpos = SquarePosition.SouthEast;
				}
				break;
				case CardinalPoint.East:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = SquarePosition.NorthEast;
					else if (pos == HeroPosition.FrontRight)
						groundpos = SquarePosition.SouthEast;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = SquarePosition.NorthWest;
					else
						groundpos = SquarePosition.SouthWest;
				}
				break;
				case CardinalPoint.South:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = SquarePosition.SouthEast;
					else if (pos == HeroPosition.FrontRight)
						groundpos = SquarePosition.SouthWest;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = SquarePosition.NorthEast;
					else
						groundpos = SquarePosition.NorthWest;
				}
				break;
				case CardinalPoint.West:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = SquarePosition.SouthWest;
					else if (pos == HeroPosition.FrontRight)
						groundpos = SquarePosition.NorthWest;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = SquarePosition.SouthEast;
					else
						groundpos = SquarePosition.NorthEast;
				}
				break;
			}


			return groundpos;
		}



		/// <summary>
		/// Returns the Hero at a given position in the team
		/// </summary>
		/// <param name="pos">Position rank</param>
		/// <returns>Hero handle or null</returns>
		public Hero GetHeroFromPosition(HeroPosition pos)
		{
			return Heroes[(int) pos];
		}


		/// <summary>
		/// Returns the Hero under the mouse location
		/// </summary>
		/// <param name="location">Screen location</param>
		/// <returns>Hero handle or null</returns>
		public Hero GetHeroFromLocation(Point location)
		{

			for (int y = 0 ; y < 3 ; y++)
				for (int x = 0 ; x < 2 ; x++)
				{
					// find the hero under the location 
					if (new Rectangle(366 + x * 144, 2 + y * 104, 130, 104).Contains(location))
						return Heroes[y * 2 + x];
				}

			return null;
		}



		/// <summary>
		/// Removes a hero from the team
		/// </summary>
		/// <param name="position">Hero's position</param>
		public void DropHero(HeroPosition position)
		{
			Heroes[(int) position] = null;
			ReorderHeroes();
		}


		/// <summary>
		/// Removes a hero from the team
		/// </summary>
		/// <param name="position">Hero's position</param>
		public void DropHero(Hero hero)
		{
			if (hero == null)
				return;

			for (int i = 0 ; i < Heroes.Count ; i++)
			{
				if (Heroes[i] == hero)
				{
					Heroes[i] = null;
					ReorderHeroes();
					return;
				}
			}
		}



		/// <summary>
		/// Adds a hero to the team
		/// </summary>
		/// <param name="hero"></param>
		/// <param name="position"></param>
		public void AddHero(Hero hero, HeroPosition position)
		{
			Heroes[(int) position] = hero;
		}


		/// <summary>
		/// Reorder heroes
		/// </summary>
		public void ReorderHeroes()
		{
			Heroes.RemoveAll(item => item == null);

			while (Heroes.Count < 6)
				Heroes.Add(null);

		}


		#endregion


		#region Hand management


		/// <summary>
		/// Sets the item in the hand
		/// </summary>
		/// <param name="item">Item handle</param>
		public void SetItemInHand(Item item)
		{
			ItemInHand = item;

			if (ItemInHand != null)
				AddMessage(Language.BuildMessage(2, ItemInHand.Name));

		}

		#endregion


		#region Movement


		/// <summary>
		/// Move the team according its facing direction
		/// </summary>
		/// <param name="front">MoveForward / backward offset</param>
		/// <param name="strafe">Left / right offset</param>
		/// <returns>True if move allowed, otherwise false</rereturns>
		public bool Walk(int front, int strafe)
		{

			switch (Location.Direction)
			{
				case CardinalPoint.North:
				return Move(new Point(front, strafe));

				case CardinalPoint.South:
				return Move(new Point(-front, -strafe));

				case CardinalPoint.East:
				return Move(new Point(-strafe, front));

				case CardinalPoint.West:
				return Move(new Point(strafe, -front));
			}


			return false;
		}



		/// <summary>
		/// Move team despite the direction the team is facing
		/// (useful for forcefield)
		/// </summary>
		/// <param name="offset">Direction of the move</param>
		/// <param name="count">Number of block</param>
		public void Offset(CardinalPoint direction, int count)
		{
			Point offset = Point.Empty;

			switch (direction)
			{
				case CardinalPoint.North:
				offset = new Point(0, -1);
				break;
				case CardinalPoint.South:
				offset = new Point(0, 1);
				break;
				case CardinalPoint.West:
				offset = new Point(-1, 0);
				break;
				case CardinalPoint.East:
				offset = new Point(1, 0);
				break;
			}

			Move(offset);
		}


		/// <summary>
		/// Move the team
		/// </summary>
		/// <param name="offset">Step offset</param>
		/// <returns>True if the team moved, or false</returns>
		private bool Move(Point offset)
		{
			// Can't move and force is false
			if (!CanMove)
				return false;

			// Get informations about the destination block
			Point dst = Location.Coordinate;
			dst.Offset(offset);


			// Check all blocking states
			bool state = true;

			// Is blocking
			Square dstblock = Location.Maze.GetBlock(dst);
			if (dstblock.IsBlocking)
				state = false;

			// Stairs
			//if (dstblock.Stair != null)
			//    state = true;

			// Monsters
			if (dstblock.MonsterCount > 0)
				state = false;

			// blocking door
			//if (dstblock.Door != null && dstblock.Door.IsBlocking)
			//	state = false;

			// If can't pass through
			if (!state)
			{
				AddMessage(Language.GetString(1));
				return false;
			}


			// Leave the current block
			if (MazeBlock != null)
				MazeBlock.OnTeamLeave(this);


			Location.Coordinate.Offset(offset);
			LastMove = DateTime.Now;
			HasMoved = true;

			// Enter the new block
			MazeBlock = Location.Maze.GetBlock(Location.Coordinate);
			if (MazeBlock != null)
				MazeBlock.OnTeamEnter(this);


			return true;
		}


		/// <summary>
		/// Teleport the team to a new location, but don't change direction
		/// </summary>
		/// <param name="location">Location in the dungeon</param>
		/// <returns>True if teleportion is ok, or false if M. Spoke failed !</returns>
		public bool Teleport(DungeonLocation location)
		{
			if (Dungeon == null || location == null)
				return false;

			// Destination maze
			Maze maze = Dungeon.GetMaze(location.MazeName);
			if (maze == null)
				return false;

			// Leave current block
			if (MazeBlock != null)
				MazeBlock.OnTeamLeave(this);

			// Change location
			Location.Coordinate = location.Coordinate;
			Location.SetMaze(maze.Name);

			// New block
			MazeBlock = Location.Maze.GetBlock(location.Coordinate);

			// Enter new block
			MazeBlock.OnTeamEnter(this);

			return true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Current language
		/// </summary>
		StringTable Language;


		/// <summary>
		/// Dungeon to use
		/// </summary>
		public Dungeon Dungeon
		{
			get;
			private set;
		}


		/// <summary>
		/// Location of the team
		/// </summary>
		public DungeonLocation Location
		{
			get;
			private set;
		}


		/// <summary>
		/// Square where the team is
		/// </summary>
		public Square MazeBlock
		{
			get;
			private set;
		}


		/// <summary>
		/// Direction the team is facing
		/// </summary>
		public CardinalPoint Direction
		{
			get
			{
				return Location.Direction;
			}
			set
			{
				Location.Direction = value;
			}
		}


		/// <summary>
		/// Debug mode
		/// </summary>
		public bool Debug
		{
			get;
			set;
		}


		/// <summary>
		/// Drawing Tileset
		/// </summary>
		TileSet TileSet;


		/// <summary>
		/// Heads of the Heroes
		/// </summary>
		TileSet Heads;


		/// <summary>
		/// Items tilesets
		/// </summary>
		TileSet Items;


		/// <summary>
		/// All heros in the team
		/// </summary>
		public List<Hero> Heroes;


		/// <summary>
		/// Number of heroes in the team
		/// </summary>
		public int HeroCount
		{
			get
			{
				if (Heroes == null)
					return 0;

				int count = 0;
				foreach (Hero hero in Heroes)
				{
					if (hero != null)
						count++;
				}

				return count;
			}
		}


		/// <summary>
		/// Returns the Square in front of the team
		/// </summary>
		public Square FrontBlock
		{
			get
			{
				return Location.Maze.GetBlock(FrontLocation.Coordinate);
			}
		}


		/// <summary>
		/// Gets the front wall side
		/// </summary>
		public CardinalPoint FrontWallSide
		{
			get
			{
				CardinalPoint[] points = new CardinalPoint[]
					{
						CardinalPoint.South,
						CardinalPoint.North,
						CardinalPoint.East,
						CardinalPoint.West,
					};

				return points[(int) Direction];
			}
		}


		/// <summary>
		/// Returns the location in front of the team
		/// </summary>
		public DungeonLocation FrontLocation
		{
			get
			{
				DungeonLocation location = new DungeonLocation(Location);

				switch (Location.Direction)
				{
					case CardinalPoint.North:
					location.Coordinate = new Point(Location.Coordinate.X, Location.Coordinate.Y - 1);
					break;
					case CardinalPoint.South:
					location.Coordinate = new Point(Location.Coordinate.X, Location.Coordinate.Y + 1);
					break;
					case CardinalPoint.West:
					location.Coordinate = new Point(Location.Coordinate.X - 1, Location.Coordinate.Y);
					break;
					case CardinalPoint.East:
					location.Coordinate = new Point(Location.Coordinate.X + 1, Location.Coordinate.Y);
					break;
				}

				return location;
			}
		}



		/// <summary>
		/// Return the currently selected hero
		/// </summary>
		public Hero SelectedHero;


		/// <summary>
		/// Interface to display
		/// </summary>
		public TeamInterface Interface;

		/// <summary>
		/// 
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Display font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Outlined font
		/// </summary>
		BitmapFont OutlinedFont;


		/// <summary>
		/// Messages to display
		/// </summary>
		public List<ScreenMessage> Messages
		{
			get;
			private set;
		}


		/// <summary>
		/// Item hold in the hand
		/// </summary>
		public Item ItemInHand
		{
			get;
			private set;
		}


		/// <summary>
		/// Window GUI
		/// </summary>
		Camp CampWindow;


		/// <summary>
		/// If the team moved during the last update
		/// </summary>
		public bool HasMoved
		{
			get;
			private set;
		}


		/// <summary>
		/// Does the team can move
		/// </summary>
		public bool CanMove
		{
			get
			{
				if (LastMove + TeamSpeed > DateTime.Now)
					return false;

				return true;
			}
		}


		/// <summary>
		/// Last time the team moved
		/// </summary>
		DateTime LastMove
		{
			get;
			set;
		}


		/// <summary>
		/// Speed of the team.
		/// TODO: Check all heroes and return the slowest one
		/// </summary>
		TimeSpan TeamSpeed
		{
			get;
			set;
		}


		/// <summary>
		/// Hero swapping
		/// </summary>
		Hero HeroToSwap;


		/// <summary>
		/// Allow the player to personalize keyboard input shceme
		/// </summary>
		InputScheme InputScheme;



		/// <summary>
		/// Spell book window
		/// </summary>
		public SpellBook SpellBook
		{
			get;
			private set;
		}


		/// <summary>
		/// Draw HP as bar
		/// </summary>
		public bool DrawHPAsBar
		{
			get;
			set;
		}



		/// <summary>
		/// Name of the savegame file
		/// </summary>
		public string SaveGame
		{
			get;
			set;
		}


		/// <summary>
		/// Gets if the whole team is dead
		/// </summary>
		public bool IsDead
		{
			get
			{
				foreach (Hero hero in Heroes)
				{
					if (hero != null && !hero.IsDead)
						return false;
				}

				return true;
			}
		}


		/// <summary>
		/// Gets if the whole team is uncounscious
		/// </summary>
		public bool IsUncounscious
		{
			get
			{
				foreach (Hero hero in Heroes)
				{
					if (hero != null && !hero.IsUnconscious)
						return false;
				}

				return true;
			}
		}

		#endregion
	}


	#region Enums



	/// <summary>
	/// Position of a Hero in the team
	/// </summary>
	public enum HeroPosition
	{
		/// <summary>
		/// Front left
		/// </summary>
		FrontLeft = 0,

		/// <summary>
		/// Front right
		/// </summary>
		FrontRight = 1,

		/// <summary>
		/// Center left
		/// </summary>
		MiddleLeft = 2,

		/// <summary>
		/// Center right
		/// </summary>
		MiddleRight = 3,

		/// <summary>
		/// Rear left
		/// </summary>
		RearLeft = 4,

		/// <summary>
		/// Rear right
		/// </summary>
		RearRight = 5
	}



	/// <summary>
	/// Interface to display
	/// </summary>
	public enum TeamInterface
	{
		/// <summary>
		/// Display the main interface
		/// </summary>
		Main,


		/// <summary>
		/// Display the inventory
		/// </summary>
		Inventory,


		/// <summary>
		/// Display statistic page
		/// </summary>
		Statistic
	}


	#endregion


	/// <summary>
	/// Interface coordinates
	/// </summary>
	struct InterfaceCoord
	{

		#region Main window

		/// <summary>
		/// TurnLeft button
		/// </summary>
		static public Rectangle TurnLeft = new Rectangle(10, 256, 38, 34);

		/// <summary>
		/// Move Forward button
		/// </summary>
		static public Rectangle MoveForward = new Rectangle(48, 256, 40, 34);


		/// <summary>
		/// Turn right button
		/// </summary>
		static public Rectangle TurnRight = new Rectangle(88, 256, 40, 34);


		/// <summary>
		/// Move left button
		/// </summary>
		static public Rectangle MoveLeft = new Rectangle(10, 290, 38, 34);


		/// <summary>
		/// Move backward button
		/// </summary>
		static public Rectangle MoveBackward = new Rectangle(48, 290, 40, 34);


		/// <summary>
		/// Move right button
		/// </summary>
		static public Rectangle MoveRight = new Rectangle(88, 290, 40, 34);


		/// <summary>
		/// Select hero's rectangle
		/// </summary>
		static public Rectangle[] SelectHero = new Rectangle[]
		{
			new Rectangle(368      ,       2, 126, 20),		// Hero 1
			new Rectangle(368 + 144,       2, 126, 20),		// Hero 2

			new Rectangle(368      , 104 + 2, 126, 20),		// Hero 3
			new Rectangle(368 + 144, 104 + 2, 126, 20),		// Hero 4

			new Rectangle(368      , 208 + 2, 126, 20),		// Hero 5
			new Rectangle(368 + 144, 208 + 2, 126, 20),		// Hero 6
		};


		/// <summary>
		/// Hero's Face rectangle
		/// </summary>
		static public Rectangle[] HeroFace = new Rectangle[]
		{
			new Rectangle(368      ,       22, 64, 64),		// Hero 1
			new Rectangle(368 + 144,       22, 64, 64),		// Hero 2

			new Rectangle(368      , 104 + 22, 64, 64),		// Hero 3
			new Rectangle(368 + 144, 104 + 22, 64, 64),		// Hero 4
			
			new Rectangle(368      , 208 + 22, 64, 64),		// Hero 5
			new Rectangle(368 + 144, 208 + 22, 64, 64),		// Hero 6
		};


		/// <summary>
		/// Hero's primary hand rectangle
		/// </summary>
		static public Rectangle[] PrimaryHand = new Rectangle[]
		{
			new Rectangle(432      ,       22, 62, 32),		// Hero 1
			new Rectangle(432 + 144,       22, 62, 32),		// Hero 2

			new Rectangle(432      , 104 + 22, 62, 32),		// Hero 3
			new Rectangle(432 + 144, 104 + 22, 62, 32),		// Hero 4

			new Rectangle(432      , 208 + 22, 62, 32),		// Hero 5
			new Rectangle(432 + 144, 208 + 22, 60, 32),		// Hero 6
		};


		/// <summary>
		/// Hero's primary hand rectangle
		/// </summary>
		static public Rectangle[] SecondaryHand = new Rectangle[]
		{
			new Rectangle(432      ,       54, 62, 32),		// Hero 1
			new Rectangle(432 + 144,       54, 62, 32),		// Hero 2

			new Rectangle(432      , 104 + 54, 62, 32),		// Hero 3
			new Rectangle(432 + 144, 104 + 54, 62, 32),		// Hero 4

			new Rectangle(432      , 208 + 54, 62, 32),		// Hero 5
			new Rectangle(432 + 144, 208 + 54, 62, 32),		// Hero 6
		};


		#endregion



		#region Inventory screen

		/// <summary>
		/// Close inventory button
		/// </summary>
		static public Rectangle CloseInventory = new Rectangle(360, 4, 64, 64);


		/// <summary>
		/// Show statistics button
		/// </summary>
		static public Rectangle ShowStatistics = new Rectangle(602, 296, 36, 36);


		/// <summary>
		/// Close inventory button
		/// </summary>
		static public Rectangle PreviousHero = new Rectangle(546, 68, 40, 30);


		/// <summary>
		/// Close inventory button
		/// </summary>
		static public Rectangle NextHero = new Rectangle(592, 68, 40, 30);


		/// <summary>
		/// Backpack buttons
		/// </summary>
		static public Rectangle[] BackPack = new Rectangle[]
		{
			new Rectangle(358     , 76     ,  36, 36),
			new Rectangle(358 + 36, 76     ,  36, 36),
			new Rectangle(358     , 76 + 36,  36, 36),
			new Rectangle(358 + 36, 76 + 36,  36, 36),
			new Rectangle(358     , 76 + 72,  36, 36),
			new Rectangle(358 + 36, 76 + 72,  36, 36),
			new Rectangle(358     , 76 + 108, 36, 36),
			new Rectangle(358 + 36, 76 + 108, 36, 36),
			new Rectangle(358     , 76 + 144, 36, 36),
			new Rectangle(358 + 36, 76 + 144, 36, 36),
			new Rectangle(358     , 76 + 180, 36, 36),
			new Rectangle(358 + 36, 76 + 180, 36, 36),
			new Rectangle(358     , 76 + 216, 36, 36),
			new Rectangle(358 + 36, 76 + 216, 36, 36),
		};

		/// <summary>
		/// Rings buttons
		/// </summary>
		static public Rectangle[] Rings = new Rectangle[]
		{
			new Rectangle(452     , 268, 20, 20),
			new Rectangle(452 + 24, 268, 20, 20),
		};


		/// <summary>
		/// Belt buttons
		/// </summary>
		static public Rectangle[] Waist = new Rectangle[]
		{
			new Rectangle(596, 184     , 36, 36),
			new Rectangle(596, 184 + 36, 36, 36),
			new Rectangle(596, 184 + 72, 36, 36),
		};


		/// <summary>
		/// Quiver button
		/// </summary>
		static public Rectangle Quiver = new Rectangle(446, 108, 36, 36);


		/// <summary>
		/// Quiver button
		/// </summary>
		static public Rectangle Armor = new Rectangle(444, 148, 36, 36);


		/// <summary>
		/// Food button
		/// </summary>
		static public Rectangle Food = new Rectangle(470, 72, 62, 30);


		/// <summary>
		/// Wrists button
		/// </summary>
		static public Rectangle Wrist = new Rectangle(446, 188, 36, 36);


		/// <summary>
		/// Primary Hand inventory button
		/// </summary>
		static public Rectangle PrimaryHandInventory = new Rectangle(456, 228, 36, 36);


		/// <summary>
		/// Feet button
		/// </summary>
		static public Rectangle Feet = new Rectangle(550, 270, 36, 36);


		/// <summary>
		/// Food button
		/// </summary>
		static public Rectangle SecondaryHandInventory = new Rectangle(552, 228, 36, 36);


		/// <summary>
		/// Neck button
		/// </summary>
		static public Rectangle Neck = new Rectangle(570, 146, 36, 36);


		/// <summary>
		/// Head button
		/// </summary>
		static public Rectangle Head = new Rectangle(592, 106, 36, 36);











		#endregion
	}
}
