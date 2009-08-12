using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.ScreenManager;
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
		public Team()
		{
			Messages = new List<ScreenMessage>();
			CampWindow = new CampWindow();
			TeamSpeed = TimeSpan.FromSeconds(0.15f);
			SpellBook = new SpellBook();
		}




		/// <summary>
		/// Initialize the team
		/// </summary>
		public override void LoadContent()
		{
			// Language
			Language = ResourceManager.CreateAsset<StringTable>("game");
			if (Language == null)
			{
				Trace.WriteLine("ERROR !!! No StringTable defined for the game !!!");
				ExitScreen();
			}
			Language.LanguageName = DungeonEye.LanguageName;

			// Keyboard input shceme
			KeyboardScheme = ResourceManager.CreateAsset<KeyboardScheme>(DungeonEye.KeyboardSchemeName);
			if (KeyboardScheme == null)
			{
				Trace.WriteLine("ERROR !!! No KeyboardSchema detected !!!");
				KeyboardScheme = new KeyboardScheme();
				KeyboardScheme["MoveForward"] = Keys.Z;
				KeyboardScheme["MoveBackward"] = Keys.S;
				KeyboardScheme["StrafeLeft"] = Keys.Q;
				KeyboardScheme["StrafeRight"] = Keys.D;
				KeyboardScheme["TurnLeft"] = Keys.A;
				KeyboardScheme["TurnRight"] = Keys.E;
				KeyboardScheme["Inventory"] = Keys.I;
				KeyboardScheme["SelectHero1"] = Keys.D1;
				KeyboardScheme["SelectHero2"] = Keys.D2;
				KeyboardScheme["SelectHero3"] = Keys.D3;
				KeyboardScheme["SelectHero4"] = Keys.D4;
				KeyboardScheme["SelectHero5"] = Keys.D5;
				KeyboardScheme["SelectHero6"] = Keys.D6;
			}

			TileSet = ResourceManager.CreateAsset<TileSet>("Interface");
			TileSet.Scale = new SizeF(2.0f, 2.0f);


			Heads = ResourceManager.CreateAsset<TileSet>("Heroes");
			Heads.Scale = new SizeF(2.0f, 2.0f);


			Items = ResourceManager.CreateAsset<TileSet>("Items");
			Items.Scale = new SizeF(2.0f, 2.0f);


			//HACK: Load heroes
			string[] name = new string[] { "Allabar", "Ariel", "Valanau", "Tenmiyana", "Bob", "Chuck" };
			Heroes = new Hero[6];
			for (int i = 0; i < 4; i++)
			{
				Hero hero = new Hero(this);
				hero.Name = name[i];
				hero.Generate();

				Heroes[i] = hero;
			}
			SelectedHero = Heroes[0];



			Font = ResourceManager.CreateSharedAsset<TextureFont>("inventory");
			OutlinedFont = ResourceManager.CreateAsset<TextureFont>("outline");

			CampWindow.Init();
			SpellBook.LoadContent();

	
			
			// The dungeon
			Dungeon = ResourceManager.CreateAsset<Dungeon>("Eye");
			Dungeon.Init();


			Maze = Dungeon.GetMaze(Dungeon.StartLocation.Maze);
			Location = Dungeon.StartLocation;
			Move(Point.Empty);
			

		}



		#region IO


		/// <summary>
		/// Loads a party
		/// </summary>
		/// <param name="filename">Xml data</param>
		/// <returns>True if team successfuly loaded, otherwise false</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name.ToLower() != "team")
				return false;


			// Clear the team
			for (int i = 0; i < Heroes.Length; i++)
				Heroes[i] = null;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "location":
					{
						Location.Position = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
						Location.Direction = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["direction"].Value, true);
						Location.Maze= node.Attributes["name"].Value;
					}
					break;


					case "hero":
					{
						HeroPosition position = (HeroPosition)Enum.Parse(typeof(HeroPosition), node.Attributes["position"].Value, true);
						Heroes[(int)position] = new Hero(this);
						Heroes[(int)position].Load(node);
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

			writer.WriteStartElement("location");
			writer.WriteAttributeString("x", Location.Position.X.ToString());
			writer.WriteAttributeString("y", Location.Position.Y.ToString());
			writer.WriteAttributeString("direction", Location.Direction.ToString());
			writer.WriteAttributeString("name", Location.Maze);
			writer.WriteEndElement();


			// Save each hero
			foreach (Hero hero in Heroes)
			{
				if (hero != null)
				{
					writer.WriteStartElement("hero");
					writer.WriteAttributeString("position", GetHeroPosition(hero).ToString());
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
			writer.Flush();
			writer.Close();
	
			return false;
		}

		#endregion


		#region Draws



		/// <summary>
		/// Display team informations
		/// </summary>
		public override void Draw()
		{

			// Draw the current maze
			if (Maze != null)
				Maze.Draw(Location);


			// The backdrop
			Display.Color = Color.White;
			TileSet.Draw(0, Point.Empty);


			// Display the compass
			TileSet.Draw(5 + (int)Location.Direction * 3, new Point(224, 254));
			TileSet.Draw(6 + (int)Location.Direction * 3, new Point(154, 308));
			TileSet.Draw(7 + (int)Location.Direction * 3, new Point(298, 308));


			// Interfaces
			if (Interface == TeamInterface.Inventory)
				DrawInventory();

			else if (Interface == TeamInterface.Statistic)
				DrawStatistics();

			else
			{
				DrawMain();

				CampWindow.Draw();
			}



			// Display the last 3 messages
			int i = 0;
			foreach (ScreenMessage msg in Messages)
			{
				Font.Color = msg.Color;
				Font.DrawText(new Point(10, 358 + i * 12), msg.Message);
				i++;
			}


			// Team location
			Font.Color = Color.White;
			Font.DrawText(new Point(0, 340), Location.Position.ToString());


			// Draw the spell window
			SpellBook.Draw();


			// Draw the cursor or the item in the hand
			Display.Color = Color.White;
			if (ItemInHand != null)
				Items.Draw(ItemInHand.TileID, Mouse.Location);
			else
				Items.Draw(0, Mouse.Location);

			//Display.Color = Color.FromArgb(128, Color.Red);
			//Display.Rectangle(new Rectangle(0, 0, 176, 108), true);
			//Display.Color = Color.FromArgb(128, Color.Blue);
			//Display.Rectangle(new Rectangle(176, 0, 176, 144), true);

			//Display.Color = Color.FromArgb(128, Color.Red);
			//Display.Rectangle(new Rectangle(0, 144, 176, 58), true);
			//Display.Color = Color.FromArgb(128, Color.Blue);
			//Display.Rectangle(new Rectangle(176, 144, 176, 58), true);

			//Display.Color = Color.FromArgb(200, Color.Red);
			//Display.Rectangle(new Rectangle(0, 202, 176, 38), true);
			//Display.Color = Color.FromArgb(200, Color.Blue);
			//Display.Rectangle(new Rectangle(176, 202, 176, 38), true);


		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		private void DrawMain()
		{
			Display.Color = Color.White;

			// Draw heroes
			Point pos;
			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 2; x++)
				{
					Hero hero = Heroes[y * 2 + x];
					if (hero == null)
						continue;

					pos = new Point(366 + 144 * x, y * 104 + 2);

					// Backdrop
					TileSet.Draw(17, pos);

					// Head
					if (hero.IsDead)
						TileSet.Draw(4, new Point(pos.X + 2, pos.Y + 20));
					else
						Heads.Draw(hero.Head, new Point(pos.X + 2, pos.Y + 20));

					// Hero uncouncious
					if (hero.IsUnconscious)
						TileSet.Draw(2, new Point(pos.X + 4, pos.Y + 20));

					// Name
					Font.Color = Color.Black;
					if (HeroToSwap == hero)
					{
						Font.Color = Color.Red;
						Font.DrawText(new Point(pos.X + 4, pos.Y + 6), " Swapping");
						Font.Color = Color.Black;
					}
					else if (SelectedHero == hero)
					{
						Font.Color = Color.White;
						Font.DrawText(new Point(pos.X + 4, pos.Y + 6), hero.Name);
						Font.Color = Color.Black;
					}
					else
					{
						Font.Color = Color.Black;
						Font.DrawText(new Point(pos.X + 4, pos.Y + 6), hero.Name);
					}

					// HP
					Font.DrawText(new Point(pos.X + 4, pos.Y + 88), hero.HitPoint + " of " + hero.MaxHitPoint);




					// Primary
					Item item = hero.GetInventoryItem(InventoryPosition.Primary);
					if (item != null)
						Items.Draw(item.TileID, new Point(pos.X + 96, pos.Y + 36));
					else
						Items.Draw(86, new Point(pos.X + 96, pos.Y + 36));

					// Hero hit a monster a few moment ago
					AttackResult attack = hero.GetLastAttack(HeroHand.Primary);
					if (attack.Date + attack.OnHold > DateTime.Now)
					{
						if (attack.Monster != null)
						{
							TileSet.Draw(21, new Point(pos.X + 64, pos.Y + 20));

							Font.Color = Color.White;
							if (attack.Result > 0)
								Font.DrawText(new Point(pos.X + 90, pos.Y + 32), attack.Result.ToString());
							else
								Font.DrawText(new Point(pos.X + 76, pos.Y + 32), "MISS");
						}
						else
						{
							TileSet.Draw(3, new Point(pos.X + 66, pos.Y + 20));
						}
					}



					// Secondary
					item = hero.GetInventoryItem(InventoryPosition.Secondary);
					if (item != null)
						Items.Draw(item.TileID, new Point(pos.X + 96, pos.Y + 68));
					else
						Items.Draw(85, new Point(pos.X + 96, pos.Y + 68));



					if (hero.IsUnconscious || hero.IsDead)
					{
						TileSet.Draw(3, new Point(pos.X + 66, pos.Y + 52));
						TileSet.Draw(3, new Point(pos.X + 66, pos.Y + 20));
					}



					// Hero hit a monster a few moment ago
					attack = hero.GetLastAttack(HeroHand.Secondary);
					if (attack.Date + attack.OnHold > DateTime.Now)
					{

						if (attack.Monster != null)
						{
							TileSet.Draw(21, new Point(pos.X + 64, pos.Y + 52));

							Font.Color = Color.White;
							if (attack.Result > 0)
								Font.DrawText(new Point(pos.X + 90, pos.Y + 64), attack.Result.ToString());
							else
								Font.DrawText(new Point(pos.X + 76, pos.Y + 64), "MISS");
						}
						else
						{
							TileSet.Draw(3, new Point(pos.X + 66, pos.Y + 52));
						}
					}



					// Hero was hit
					if (hero.LastHitTime + TimeSpan.FromSeconds(1) > DateTime.Now)
					{
						TileSet.Draw(20, new Point(pos.X + 24, pos.Y + 66));
						Font.Color = Color.White;
						Font.DrawText(new Point(pos.X + 52, pos.Y + 86), hero.LastHit.ToString());
					}
	
				}
			}


			// Mini map
			if (Maze != null)
				Maze.DrawMiniMap(this, new Point(500, 220));

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		void DrawInventory()
		{
			// Background
			TileSet.Draw(18, new Point(356, 0));

			// Head
			Heads.Draw(SelectedHero.Head, new Point(360, 4));


			// Name
			OutlinedFont.Color = Color.White;
			OutlinedFont.DrawText(new Point(430, 12), SelectedHero.Name);

			// HP and Food
			Font.Color = Color.Black;
			Font.DrawText(new Point(500, 30), SelectedHero.HitPoint + " of " + SelectedHero.MaxHitPoint);

			// Food
			if (SelectedHero.Food > 50)
				Display.Color = Color.Green;
			else if (SelectedHero.Food > 25)
				Display.Color = Color.Yellow;
			else
				Display.Color = Color.Red;

			Display.Rectangle(new Rectangle(498, 48, SelectedHero.Food, 10), true);


			// Draw inventory
			Display.Color = Color.White;
			int pos = 0;
			for (int y = 94; y < 344; y += 36)
				for (int x = 378; x < 442; x += 36)
				{
					if (SelectedHero.GetInventoryItem(InventoryPosition.Inventory_01 + pos) != null)
						Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Inventory_01 + pos).TileID, new Point(x, y));

					pos++;
				}


			// Quiver count
			Font.Color = Color.White;
			if (SelectedHero.Quiver > 99)
				Font.DrawText(new Point(454, 128), "++");
			else
				Font.DrawText(new Point(454, 128), SelectedHero.Quiver.ToString());

			// Armor
			if (SelectedHero.GetInventoryItem(InventoryPosition.Armor) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Armor).TileID, new Point(464, 166));

			// Wrist
			if (SelectedHero.GetInventoryItem(InventoryPosition.Wrist) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Wrist).TileID, new Point(466, 206));

			// Primary
			if (SelectedHero.GetInventoryItem(InventoryPosition.Primary) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Primary).TileID, new Point(476, 246));

			// Ring 1
			if (SelectedHero.GetInventoryItem(InventoryPosition.Ring_1) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Ring_1).TileID, new Point(464, 278));

			// Ring 2
			if (SelectedHero.GetInventoryItem(InventoryPosition.Ring_2) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Ring_2).TileID, new Point(488, 278));

			// Feet
			if (SelectedHero.GetInventoryItem(InventoryPosition.Shoes) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Shoes).TileID, new Point(570, 288));

			// Secondary
			if (SelectedHero.GetInventoryItem(InventoryPosition.Secondary) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Secondary).TileID, new Point(572, 246));

			// Back 1 598,184,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Belt_1) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Belt_1).TileID, new Point(616, 202));

			// Back 2 598,220,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Belt_2) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Belt_2).TileID, new Point(616, 238));

			// Back 3 598,256,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Belt_3) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Belt_3).TileID, new Point(616, 274));

			// Neck 572,146,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Neck) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Neck).TileID, new Point(590, 164));

			// Head 594,106,36,36
			if (SelectedHero.GetInventoryItem(InventoryPosition.Helmet) != null)
				Items.Draw(SelectedHero.GetInventoryItem(InventoryPosition.Helmet).TileID, new Point(612, 124));


			// Ring 1 454,268,20,20
			// Ring 2 478,268,20,20

			//Display.Color = Color.FromArgb(127, Color.Red);
			//Display.Rectangle(new Rectangle(454, 268, 20, 20), true);
			//Display.Rectangle(new Rectangle(478, 268, 20, 20), true);


		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		void DrawStatistics()
		{
			// Background
			TileSet.Draw(18, new Point(356, 0));
			Display.Color = Color.FromArgb(164, 164, 184);
			Display.Rectangle(new Rectangle(360, 70, 186, 30), true);
			Display.Rectangle(new Rectangle(360, 100, 276, 194), true);
			Display.Rectangle(new Rectangle(360, 294, 242, 36), true);
			Display.Color = Color.White;


			// Hero head
			Heads.Draw(SelectedHero.Head, new Point(360, 4));


			OutlinedFont.Color = Color.White;
			OutlinedFont.DrawText(new Point(430, 12), SelectedHero.Name);
			OutlinedFont.DrawText(new Point(370, 80), "Character info");

			// HP and Food
			Font.Color = Color.Black;
			Font.DrawText(new Point(500, 30), SelectedHero.HitPoint + " of " + SelectedHero.MaxHitPoint);

			// Food
			if (SelectedHero.Food > 50)
				Display.Color = Color.Green;
			else if (SelectedHero.Food > 25)
				Display.Color = Color.Yellow;
			else
				Display.Color = Color.Red;

			Display.Rectangle(new Rectangle(498, 48, SelectedHero.Food, 10), true);


			Font.Color = Color.Black;
			Font.DrawText(new Point(366, 110), SelectedHero.Professions[0].Classe.ToString());
			Font.DrawText(new Point(366, 124), SelectedHero.Alignment.ToString());
			Font.DrawText(new Point(366, 138), SelectedHero.Race.ToString());

			Font.DrawText(new Point(366, 166), "Strength");
			Font.DrawText(new Point(366, 180), "Intelligence");
			Font.DrawText(new Point(366, 194), "Wisdom");
			Font.DrawText(new Point(366, 208), "Dexterity");
			Font.DrawText(new Point(366, 222), "Constitution");
			Font.DrawText(new Point(366, 236), "Charisma");
			Font.DrawText(new Point(366, 250), "Armor class");

			Font.DrawText(new Point(470, 280), "EXP");
			Font.DrawText(new Point(550, 280), "LVL");

			Font.Color = Color.White;
			Font.DrawText(new Point(552, 166), SelectedHero.Strength.ToString() + "/" + SelectedHero.MaxStrength.ToString());
			Font.DrawText(new Point(552, 180), SelectedHero.Intelligence.ToString());
			Font.DrawText(new Point(552, 194), SelectedHero.Wisdom.ToString());
			Font.DrawText(new Point(552, 208), SelectedHero.Dexterity.ToString());
			Font.DrawText(new Point(552, 222), SelectedHero.Constitution.ToString());
			Font.DrawText(new Point(552, 236), SelectedHero.Charisma.ToString());
			Font.DrawText(new Point(552, 250), SelectedHero.ArmorClass.ToString());


			int y = 0;
			for (int i = 0; i < SelectedHero.Professions.Length; i++)
			{
				if (SelectedHero.Professions[i].Experience <= 0)
					continue;


				Font.Color = Color.Black;
				Font.DrawText(new Point(366, 300 + y), SelectedHero.Professions[i].Classe.ToString());

				Font.Color = Color.White;
				Font.DrawText(new Point(460, 300 + y), SelectedHero.Professions[i].Experience.ToString());
				Font.DrawText(new Point(560, 300 + y), SelectedHero.Professions[i].Level.ToString());

				y += 12;
			}

		}




		#endregion


		#region Updates

		/// <summary>
		/// Update the Team status
		/// </summary>
		/// <param name="time">Time passed since the last call to the last update.</param>
		//public override void Update(GameTime time)
		public override void Update(GameTime time, bool hasFocus, bool isCovered)
		{
			HasMoved = false;


			#region Keyboard


			// Hit the hero under the mouse
			if (Keyboard.IsNewKeyPress(Keys.O))
			{
				Hero hero = GetHeroFromLocation(Mouse.Location);
				if (hero != null)
				{
					hero.Hit(null, Game.Random.Next(1, 5));
				}
			}

			// Reload data banks
			if (Keyboard.IsNewKeyPress(Keys.R))
			{
				Dungeon = ResourceManager.CreateAsset<Dungeon>("Eye");
				Dungeon.Init();
				AddMessage("Dungeon reloaded...");
			}

			// AutoMap
			if (Keyboard.IsNewKeyPress(Keys.Tab))
			{
				ScreenManager.AddScreen(new AutoMap());
			}

			// Bye bye
			if (Keyboard.IsNewKeyPress(Keys.Escape))
				ExitScreen();



			// Save team
			if (Keyboard.IsNewKeyPress(Keys.J))
			{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.OmitXmlDeclaration = false;
				settings.IndentChars = "\t";
				settings.Encoding = System.Text.ASCIIEncoding.ASCII;
				XmlWriter xml = XmlWriter.Create(@"team.xml", settings);
				Save(xml);
				xml.Close();

				AddMessage("Team saved...", Color.YellowGreen);
			}

			// Load team
			if (Keyboard.IsNewKeyPress(Keys.L))
			{
				if (System.IO.File.Exists("team.xml"))
				{
					XmlDocument xml = new XmlDocument();
					xml.Load("team.xml");

					foreach (XmlNode node in xml)
					{
						if (node.Name.ToLower() == "team")
							Load(node);

					}

					AddMessage("Team Loaded...", Color.YellowGreen);
				}
			}

			#region Change maze
			// Change maze
			for (int i = 0; i < 12; i++)
			{
				if (Keyboard.IsNewKeyPress(Keys.F1 + i))
				{
					int id = i + 1;
					string lvl = "0" + id.ToString();
					lvl = "maze_" + lvl.Substring(lvl.Length - 2, 2);

					Maze maze = Dungeon.GetMaze(lvl);
					if (maze != null)
					{
						Maze = maze;
						AddMessage("Loading " + lvl + ":" + Maze.Description);
					}
					break;
				}
			}

			// Test maze
			if (Keyboard.IsNewKeyPress(Keys.T))
			{
				Maze = Dungeon.GetMaze("test");
				AddMessage("Loading maze test", Color.Blue);
			}

			#endregion


			#region Team move & managment

			// Display inventory
			if (Keyboard.IsNewKeyPress(KeyboardScheme["Inventory"]))
			{
				if (Interface == TeamInterface.Inventory)
					Interface = TeamInterface.Main;
				else
					Interface = TeamInterface.Inventory;
			}


			// Turn left
			if (Keyboard.IsNewKeyPress(KeyboardScheme["TurnLeft"]))
				Location.Compass.Rotate(CompassRotation.Rotate270);


			// Turn right
			if (Keyboard.IsNewKeyPress(KeyboardScheme["TurnRight"]))
				Location.Compass.Rotate(CompassRotation.Rotate90);


			// Move forward
			if (Keyboard.IsNewKeyPress(KeyboardScheme["MoveForward"]))
				Walk(0, -1);


			// Move backward
			if (Keyboard.IsNewKeyPress(KeyboardScheme["MoveBackward"]))
				Walk(0, 1);


			// Strafe left
			if (Keyboard.IsNewKeyPress(KeyboardScheme["StrafeLeft"]))
				Walk(-1, 0);

			// Strafe right
			if (Keyboard.IsNewKeyPress(KeyboardScheme["StrafeRight"]))
				Walk(1, 0);

			// Select Hero 1
			if (Keyboard.IsNewKeyPress(KeyboardScheme["SelectHero1"]))
				SelectedHero = Heroes[0];

			// Select Hero 2
			if (Keyboard.IsNewKeyPress(KeyboardScheme["SelectHero2"]))
				SelectedHero = Heroes[1];

			// Select Hero 3
			if (Keyboard.IsNewKeyPress(KeyboardScheme["SelectHero3"]))
				SelectedHero = Heroes[2];

			// Select Hero 4
			if (Keyboard.IsNewKeyPress(KeyboardScheme["SelectHero4"]))
				SelectedHero = Heroes[3];

			// Select Hero 5
			if (Keyboard.IsNewKeyPress(KeyboardScheme["SelectHero5"]) && HeroCount >= 5)
				SelectedHero = Heroes[4];

			// Select Hero 6
			if (Keyboard.IsNewKeyPress(KeyboardScheme["SelectHero6"]) && HeroCount >= 6)
				SelectedHero = Heroes[5];
			#endregion


			#endregion


			GroundPosition groundpos = GroundPosition.NorthEast;
			Point mousePos = Mouse.Location;
			Point pos = Point.Empty;

			// Get the block at team position
			MazeBlock CurrentBlock = Maze.GetBlock(Location.Position);

			#region Mouse

			#region Left mouse button
			if (Mouse.IsNewButtonDown(MouseButtons.Left))
			{

				#region Compass
				// Turn left
				if (new Rectangle(10, 256, 38, 34).Contains(mousePos))
					Location.Compass.Rotate(CompassRotation.Rotate270);

				// Forward
				else if (new Rectangle(48, 256, 40, 34).Contains(mousePos))
					Walk(0, -1);

				// Turn right
				else if (new Rectangle(88, 256, 40, 34).Contains(mousePos))
					Location.Compass.Rotate(CompassRotation.Rotate90);

				// Move left
				else if (new Rectangle(10, 290, 38, 34).Contains(mousePos))
					Walk(-1, 0);

				// Backward
				else if (new Rectangle(48, 290, 40, 34).Contains(mousePos))
				{
					if (!Walk(0, 1))
						AddMessage("You can't go that way.");
				}
				// Move right
				else if (new Rectangle(88, 290, 40, 34).Contains(mousePos))
				{
					if (!Walk(1, 0))
						AddMessage("You can't go that way.");
				}
				#endregion

				#region Camp button
				else if (new Rectangle(578, 354, 62, 42).Contains(mousePos))
				{
					SpellBook.Close();
					CampWindow.IsVisible = true;
					Interface = TeamInterface.Main;
				}
				#endregion


				#region Gather item on the ground Left

				// Team's feet
				else if (new Rectangle(0, 202, 176, 38).Contains(mousePos))
				{
					switch (Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.NorthWest;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.SouthWest;
						break;
					}
					if (ItemInHand != null)
					{
						if (CurrentBlock.DropItem(groundpos, ItemInHand))
							ItemInHand = null;
					}
					else
					{
						ItemInHand = CurrentBlock.CollectItem(groundpos);
						if (ItemInHand != null)
						{
							AddMessage(Language.BuildMessage(2, ItemInHand.Name));
						}
					}
				}

				// In front of the team
				else if (new Rectangle(0, 144, 176, 58).Contains(mousePos))
				{
					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.NorthWest;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.SouthEast;
						break;
					}

		
					if (ItemInHand != null)
					{
						if (FrontBlock.DropItem(groundpos, ItemInHand))
							ItemInHand = null;
					}
					else
					{
						ItemInHand = FrontBlock.CollectItem(groundpos);
						if (ItemInHand != null)
							AddMessage(Language.BuildMessage(2, ItemInHand.Name));
					}
				}


				#endregion

				#region Gather item on the ground right
				else if (new Rectangle(176, 202, 176, 38).Contains(mousePos))
				{
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.NorthWest;
						break;
					}
					
					if (ItemInHand != null)
					{
						if (CurrentBlock.DropItem(groundpos, ItemInHand))
							ItemInHand = null;
					}
					else
					{
						ItemInHand = CurrentBlock.CollectItem(groundpos);
						if (ItemInHand != null)
							AddMessage(Language.BuildMessage(2, ItemInHand.Name));

					}
				}

				// In front of the team
				else if (new Rectangle(176, 144, 176, 58).Contains(mousePos))
				{

					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.NorthWest;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.NorthEast;
						break;
					}


					if (ItemInHand != null)
					{
						if (FrontBlock.DropItem(groundpos, ItemInHand))
							ItemInHand = null;
					}
					else
					{
						ItemInHand = FrontBlock.CollectItem(groundpos);
						if (ItemInHand != null)
							AddMessage(Language.BuildMessage(2, ItemInHand.Name));
					}
				}

				#endregion


				#region Throw an object left side
				else if (new Rectangle(0, 0, 176, 144).Contains(mousePos) && ItemInHand != null)
				{
					DungeonLocation loc = new DungeonLocation(Location);
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						loc.GroundPosition = GroundPosition.SouthWest;
						break;
						case CardinalPoint.East:
						loc.GroundPosition = GroundPosition.NorthWest;
						break;
						case CardinalPoint.South:
						loc.GroundPosition = GroundPosition.NorthEast;
						break;
						case CardinalPoint.West:
						loc.GroundPosition = GroundPosition.SouthEast;
						break;
					}
					Maze.FlyingItems.Add(new FlyingItem(ItemInHand, loc, TimeSpan.FromSeconds(0.25), int.MaxValue));
					ItemInHand = null;
				}
				#endregion

				#region Throw an object in the right side
				else if (new Rectangle(176, 0, 176, 144).Contains(mousePos) && ItemInHand != null)
				{

					DungeonLocation loc = new DungeonLocation(Location);

					switch (Location.Direction)
					{
						case CardinalPoint.North:
						loc.GroundPosition = GroundPosition.SouthEast;
						break;
						case CardinalPoint.East:
						loc.GroundPosition = GroundPosition.SouthWest;
						break;
						case CardinalPoint.South:
						loc.GroundPosition = GroundPosition.NorthWest;
						break;
						case CardinalPoint.West:
						loc.GroundPosition = GroundPosition.NorthEast;
						break;
					}

					Maze.FlyingItems.Add(new FlyingItem(ItemInHand, loc, TimeSpan.FromSeconds(0.25), int.MaxValue));
					ItemInHand = null;
				}
				#endregion


				#region Action to process on the front block

				// Click on the block in front of the team
				else if(new Rectangle(56, 44, 256, 192).Contains(mousePos) && ItemInHand == null)
				{
					switch (Direction)
					{
						case CardinalPoint.North:
						FrontBlock.OnClick(this, mousePos, CardinalPoint.South);
						break;
						case CardinalPoint.South:
						FrontBlock.OnClick(this, mousePos, CardinalPoint.North);
						break;
						case CardinalPoint.West:
						FrontBlock.OnClick(this, mousePos, CardinalPoint.East);
						break;
						case CardinalPoint.East:
						FrontBlock.OnClick(this, mousePos, CardinalPoint.West);
						break;
					}

				}
				#endregion

			}

			#endregion

			#region Right mouse button

			else if (Mouse.IsNewButtonDown(MouseButtons.Right))
			{
				#region Gather item on the ground Left

				// Team's feet
				if (new Rectangle(0, 202, 176, 38).Contains(mousePos))
				{
					switch (Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.NorthWest;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.SouthWest;
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
				else if (new Rectangle(0, 144, 176, 58).Contains(mousePos))
				{
					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.NorthWest;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.SouthEast;
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
				else if (new Rectangle(176, 202, 176, 38).Contains(mousePos))
				{
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.NorthEast;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.NorthWest;
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
				else if (new Rectangle(176, 144, 176, 58).Contains(mousePos))
				{

					// Ground position
					switch (Location.Direction)
					{
						case CardinalPoint.North:
						groundpos = GroundPosition.SouthEast;
						break;
						case CardinalPoint.East:
						groundpos = GroundPosition.SouthWest;
						break;
						case CardinalPoint.South:
						groundpos = GroundPosition.NorthWest;
						break;
						case CardinalPoint.West:
						groundpos = GroundPosition.NorthEast;
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

					//Point mousePos = Mouse.Location;
					Item item = null;

					// Update each hero interface
					for (int y = 0; y < 3; y++)
					{
						for (int x = 0; x < 2; x++)
						{
							// Get the hero
							Hero hero = Heroes[y * 2 + x];
							if (hero == null)
								continue;

							// Select hero
							if (new Rectangle(368 + 144 * x, y * 104 + 2, 126, 18).Contains(mousePos))
								SelectedHero = hero;


							// Swap hero
							if (new Rectangle(370 + 144 * x, y * 104 + 22, 62, 64).Contains(mousePos))
							{
								SelectedHero = hero;
								HeroToSwap = null;
								Interface = TeamInterface.Inventory;
							}


							// Take object in primary hand
							if (new Rectangle(434 + 144 * x, y * 104 + 22, 60, 32).Contains(mousePos) && hero.CanAttack(HeroHand.Primary))
							{
								item = hero.GetInventoryItem(InventoryPosition.Primary);

								if (ItemInHand != null && ((ItemInHand.Slot & ItemSlot.Primary) == ItemSlot.Primary))
								{
									Item swap = ItemInHand;
									ItemInHand = hero.GetInventoryItem(InventoryPosition.Primary);
									hero.SetInventoryItem(InventoryPosition.Primary, swap);
								}
								else if (ItemInHand == null && item != null)
								{
									ItemInHand = item;
									hero.SetInventoryItem(InventoryPosition.Primary, null); 
								}

								if (ItemInHand != null)
									AddMessage(Language.BuildMessage(2, ItemInHand.Name));

							}

							// Take object in secondary hand
							if (new Rectangle(434 + 144 * x, y * 104 + 54, 60, 32).Contains(mousePos) && hero.CanAttack(HeroHand.Secondary))
							{
								item = hero.GetInventoryItem(InventoryPosition.Secondary);

								if (ItemInHand != null && ((ItemInHand.Slot & ItemSlot.Secondary) == ItemSlot.Secondary))
								{
									Item swap = ItemInHand;
									ItemInHand = hero.GetInventoryItem(InventoryPosition.Secondary);
									hero.SetInventoryItem(InventoryPosition.Secondary, swap);

								}
								else if (ItemInHand == null && item != null)
								{
									ItemInHand = item;
									hero.SetInventoryItem(InventoryPosition.Secondary, null); 
								}

								if (ItemInHand != null)
									AddMessage(Language.BuildMessage(2, ItemInHand.Name));

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
					for (int y = 0; y < 3; y++)
					{
						for (int x = 0; x < 2; x++)
						{

							// Get the hero
							Hero hero = Heroes[y * 2 + x];
							if (hero == null)
								continue;


							// Swap hero
							if (new Rectangle(368 + 144 * x, y * 104 + 2, 126, 18).Contains(mousePos))
							{
								if (HeroToSwap == null)
								{
									HeroToSwap = hero;
								}
								else
								{
									Heroes[(int)GetHeroPosition(HeroToSwap)] = hero;
									Heroes[y * 2 + x] = HeroToSwap;


									HeroToSwap = null;
								}
							}

							// Show Hero inventory
							if (new Rectangle(370 + 144 * x, y * 104 + 22, 62, 64).Contains(mousePos))
							{
								SelectedHero = hero;
								Interface = TeamInterface.Inventory;
							}


							// Use object in primary hand
							if (new Rectangle(434 + 144 * x, y * 104 + 22, 60, 32).Contains(mousePos))
							{
								hero.Attack(HeroHand.Primary);

								if (hero.GetLastAttack(HeroHand.Primary).Monster != null)
								{
									Monster monster = hero.GetLastAttack(HeroHand.Primary).Monster;
									AddMessage(monster.Name + " : -" + hero.GetLastAttack(HeroHand.Primary).Result + " (" + monster.Life.Actual + "/" + monster.Life.Max + ")");
								}

							}

							// Use object in secondary hand
							if (new Rectangle(434 + 144 * x, y * 104 + 54, 60, 32).Contains(mousePos))
							{
								hero.Attack(HeroHand.Secondary);

								if (hero.GetLastAttack(HeroHand.Secondary).Monster != null)
								{
									Monster monster = hero.GetLastAttack(HeroHand.Secondary).Monster;
									AddMessage(monster.Name + " : -" + hero.GetLastAttack(HeroHand.Secondary).Result + " (" + monster.Life.Actual + "/" + monster.Life.Max + ")");
								}
							}
						}
					}
				}
				#endregion
			}

			CampWindow.Update(time);


			#region Heros update

			// Update all heroes
			foreach (Hero hero in Heroes)
			{
				if (hero == null)
					continue;

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


			// Now check where the team is and what it can do
			// if IT has moved !!
			//if (!HasMoved)
			//   return;

			if (CanMove)
				MazeBlock.OnTeamStand(this);		
	
		}




		/// <summary>
		/// Updates inventory
		/// </summary>
		/// <param name="time"></param>
		void UpdateInventory(GameTime time)
		{
			Point mousePos = Mouse.Location;
			Item item = null;
	
			
			if (Mouse.IsNewButtonDown(MouseButtons.Left))
			{
				// Close inventory
				if (new Rectangle(362, 4, 64, 64).Contains(mousePos))
					Interface = TeamInterface.Main;


				// Show statistics
				if (new Rectangle(606, 296, 36, 36).Contains(mousePos))
					Interface = TeamInterface.Statistic;


				// Previous Hero
				if (new Rectangle(548, 68, 40, 30).Contains(mousePos))
					SelectedHero = GetPreviousHero();

				// Next Hero
				if (new Rectangle(594, 68, 40, 30).Contains(mousePos))
					SelectedHero = GetNextHero();



				// Manage bag pack items
				int pos = 0;
				for (int y = 70; y < 328; y += 36)
					for (int x = 360; x < 426; x += 36)
					{

						if (new Rectangle(x, y, 36, 36).Contains(mousePos))
						{
							Item swap = ItemInHand;
							ItemInHand = SelectedHero.GetInventoryItem(InventoryPosition.Inventory_01 + pos);
							SelectedHero.SetInventoryItem(InventoryPosition.Inventory_01 + pos, swap);

							if (ItemInHand != null)
								AddMessage(ItemInHand.Name + " taken.");
						}

						// Next position
						pos++;
					}


				// Quiver
				if (new Rectangle(448, 108, 36, 36).Contains(mousePos))
				{
					if (ItemInHand == null && SelectedHero.Quiver > 0)
					{
						SelectedHero.Quiver--;
						ItemInHand = ResourceManager.CreateAsset<ItemSet>("Items").GetItem("Arrow");
					}
					else if (ItemInHand != null && (ItemInHand.Slot & ItemSlot.Ammo) == ItemSlot.Ammo)
					{
						SelectedHero.Quiver++;
						ItemInHand = null;
					}
				}

				// Armor
				else if (new Rectangle(446, 148, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Armor);

					if (ItemInHand != null && ItemInHand.Slot == ItemSlot.Body)
					{
						Item swap = ItemInHand;
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Armor, swap);
					}
					else if (ItemInHand == null && item != null)
					{
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Armor, null);
					}
				}

				// Food 472,72,62,32
				else if (new Rectangle(472, 72, 62, 30).Contains(mousePos))
				{
					if (ItemInHand != null && ItemInHand.Type == ItemType.Food)
					{
						SelectedHero.Food += 25;
						ItemInHand = null;
					}
				}



				// Wrist 448,188,36,36
				else if (new Rectangle(448, 188, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Wrist);

					if (ItemInHand != null && ItemInHand.Slot == ItemSlot.Wrist)
					{
						Item swap = ItemInHand;
						ItemInHand = item; 
						SelectedHero.SetInventoryItem(InventoryPosition.Wrist, swap);
					}
					else if (ItemInHand == null && item != null)
					{
						ItemInHand = item; 
						SelectedHero.SetInventoryItem(InventoryPosition.Wrist, null);
					}
				}


				// Primary 458,228,36,36
				else if (new Rectangle(458, 228, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Primary);

					if (ItemInHand != null && ((ItemInHand.Slot & ItemSlot.Primary) == ItemSlot.Primary || (ItemInHand.Slot & ItemSlot.Secondary) == ItemSlot.Secondary))
					{
						Item swap = ItemInHand;
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Primary, swap); 
					}
					else if (ItemInHand == null &&item != null)
					{
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Primary, null);
					}
				}




				// Feet 552,270,36,36
				else if (new Rectangle(552, 270, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Shoes);

					if (ItemInHand != null && ItemInHand.Slot == ItemSlot.Feet)
					{
						Item swap = ItemInHand;
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Shoes, swap); 
					}
					else if (ItemInHand == null &&item != null)
					{
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Shoes, null);
					}
				}


				// Secondary 554,228,36,36
				else if (new Rectangle(554, 228, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Secondary);

					if (ItemInHand != null && ((ItemInHand.Slot & ItemSlot.Primary) == ItemSlot.Primary || (ItemInHand.Slot & ItemSlot.Secondary) == ItemSlot.Secondary))
					{
						Item swap = ItemInHand;
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Secondary, swap);
					}
					else if (ItemInHand == null && item != null)
					{
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Secondary, null);
					}
				}



				// Neck 572,146,36,36
				else if (new Rectangle(572, 146, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Neck);

					if (ItemInHand != null && ItemInHand.Slot == ItemSlot.Neck)
					{
						Item swap = ItemInHand;
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Neck, swap);
					}
					else if (ItemInHand == null && item != null)
					{
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Neck, null);
					}
				}


				// Head 594,106,36,36
				else if (new Rectangle(594, 106, 36, 36).Contains(mousePos))
				{
					item = SelectedHero.GetInventoryItem(InventoryPosition.Helmet);

					if (ItemInHand != null && ItemInHand.Slot == ItemSlot.Head)
					{
						Item swap = ItemInHand;
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Helmet, swap);
					}
					else if (ItemInHand == null && item != null)
					{
						ItemInHand = item;
						SelectedHero.SetInventoryItem(InventoryPosition.Helmet, null);
					}
				}


				else
				{
					// Back 1 598,184,36,36
					// Back 2 598,220,36,36
					// Back 3 598,256,36,36
					for (int i = 0; i < 3; i++)
					{
						if (new Rectangle(598, 184 + i * 36, 36, 36).Contains(mousePos))
						{
							item = SelectedHero.GetInventoryItem(InventoryPosition.Belt_1 + i);
			
							if (ItemInHand != null && (ItemInHand.Slot & ItemSlot.Waist) == ItemSlot.Waist)
							{
								Item swap = ItemInHand;
								ItemInHand = item;
								SelectedHero.SetInventoryItem(InventoryPosition.Belt_1 + i, swap);
							}
							else if (ItemInHand == null && item != null)
							{
								ItemInHand = item;
								SelectedHero.SetInventoryItem(InventoryPosition.Belt_1 + i, null);
							}
						}
					}



					// Ring 1 454,268,20,20
					// Ring 2 478,268,20,20
					for (int i = 0; i < 2; i++)
					{
						item = SelectedHero.GetInventoryItem(InventoryPosition.Ring_1 + i);

						if (new Rectangle(454 + i * 24, 268, 20, 20).Contains(mousePos))
						{
							if (ItemInHand != null && ItemInHand.Slot == ItemSlot.Ring)
							{
								Item swap = ItemInHand;
								ItemInHand = item;
								SelectedHero.SetInventoryItem(InventoryPosition.Ring_1 + i, item);
							}
							else if (ItemInHand == null && item != null)
							{
								ItemInHand = item;
								SelectedHero.SetInventoryItem(InventoryPosition.Ring_1 + i, null);
							}
						}
					}

				}
			}



		}


		/// <summary>
		/// Updates statistics
		/// </summary>
		/// <param name="time"></param>
		void UpdateStatistics(GameTime time)
		{
			Point mousePos = Mouse.Location;

			if (Mouse.IsNewButtonDown(System.Windows.Forms.MouseButtons.Left))
			{

				// Close inventory
				if (new Rectangle(362, 4, 64, 64).Contains(mousePos))
					Interface = TeamInterface.Main;


				// Show statistics
				if (new Rectangle(606, 296, 36, 36).Contains(mousePos))
					Interface = TeamInterface.Inventory;



				// Previous Hero
				if (new Rectangle(548, 68, 40, 30).Contains(mousePos))
					SelectedHero = GetPreviousHero();

				// Next Hero
				if (new Rectangle(594, 68, 40, 30).Contains(mousePos))
					SelectedHero = GetNextHero();

			}
		}


		#endregion


		#region Misc


		/// <summary>
		/// Does the Team can see this place
		/// </summary>
		/// <param name="mz">Maze</param>
		/// <param name="loc">Place to see</param>
		/// <returns>True if can see the location</returns>
		/// <see cref="http://tom.cs.byu.edu/~455/3DDDA.pdf"/>
		/// <see cref="http://www.tar.hu/gamealgorithms/ch22lev1sec1.html"/>
		/// <see cref="http://www.cse.yorku.ca/~amana/research/grid.pdf"/>
		/// <see cref="http://www.siggraph.org/education/materials/HyperGraph/scanline/outprims/drawline.htm#dda"/>
		public bool CanSee(Maze mz, Point loc)
		{
			Point dist = Point.Empty;

			// Not the same maze
			if (Maze != mz)
				return false;


			Rectangle rect = Rectangle.Empty;
			switch (Location.Direction)
			{
				case CardinalPoint.North:
				{
					if (!new Rectangle(Location.Position.X - 1, Location.Position.Y - 3, 3, 4).Contains(loc) &&
						loc != new Point(Location.Position.X - 3, Location.Position.Y - 3) &&
						loc != new Point(Location.Position.X - 2, Location.Position.Y - 3) &&
						loc != new Point(Location.Position.X - 2, Location.Position.Y - 2) &&
						loc != new Point(Location.Position.X + 3, Location.Position.Y - 3) &&
						loc != new Point(Location.Position.X + 2, Location.Position.Y - 3) &&
						loc != new Point(Location.Position.X + 2, Location.Position.Y - 2))
						return false;

					// Is there a wall between the Team and the location
					int dx = loc.X - Location.Position.X;
					int dy = loc.Y - Location.Position.Y;
					float delta = (float)dy / (float)dx;
					float y = 0;
					for (int pos = Location.Position.Y; pos >= loc.Y; pos--)
					{
						if (Maze.GetBlock(new Point(pos, Location.Position.Y + (int)y)).Type == BlockType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

				case CardinalPoint.South:
				{
					if (!new Rectangle(Location.Position.X - 1, Location.Position.Y, 3, 4).Contains(loc) &&
						loc != new Point(Location.Position.X - 3, Location.Position.Y + 3) &&
						loc != new Point(Location.Position.X - 2, Location.Position.Y + 3) &&
						loc != new Point(Location.Position.X - 2, Location.Position.Y + 2) &&
						loc != new Point(Location.Position.X + 3, Location.Position.Y + 3) &&
						loc != new Point(Location.Position.X + 2, Location.Position.Y + 3) &&
						loc != new Point(Location.Position.X + 2, Location.Position.Y + 2))
						return false;


					// Is there a wall between the Team and the location
					int dx = loc.X - Location.Position.X;
					int dy = loc.Y - Location.Position.Y;
					float delta = (float)dy / (float)dx;
					float y = 0;
					for (int pos = Location.Position.Y; pos <= loc.Y; pos++)
					{
						if (Maze.GetBlock(new Point(pos, Location.Position.Y + (int)y)).Type == BlockType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

				case CardinalPoint.West:
				{
					if (!new Rectangle(Location.Position.X - 3, Location.Position.Y - 1, 4, 3).Contains(loc) &&
						loc != new Point(Location.Position.X - 3, Location.Position.Y - 3) &&
						loc != new Point(Location.Position.X - 3, Location.Position.Y - 2) &&
						loc != new Point(Location.Position.X - 2, Location.Position.Y - 2) &&
						loc != new Point(Location.Position.X - 3, Location.Position.Y + 3) &&
						loc != new Point(Location.Position.X - 3, Location.Position.Y + 2) &&
						loc != new Point(Location.Position.X - 2, Location.Position.Y + 2))
						return false;



					// Is there a wall between the Team and the location
					int dx = loc.X - Location.Position.X;
					int dy = loc.Y - Location.Position.Y;
					float delta = (float)dy / (float)dx;
					float y = 0;
					for (int pos = Location.Position.X; pos >= loc.X; pos--)
					{
						if (Maze.GetBlock(new Point(pos, Location.Position.Y + (int)y)).Type == BlockType.Wall)
							return false;

						y += delta;
					}

					return true;
				}

				case CardinalPoint.East:
				{
					if (!new Rectangle(Location.Position.X, Location.Position.Y - 1, 4, 3).Contains(loc) &&
						loc != new Point(Location.Position.X + 3, Location.Position.Y - 3) &&
						loc != new Point(Location.Position.X + 3, Location.Position.Y - 2) &&
						loc != new Point(Location.Position.X + 2, Location.Position.Y - 2) &&
						loc != new Point(Location.Position.X + 3, Location.Position.Y + 3) &&
						loc != new Point(Location.Position.X + 3, Location.Position.Y + 2) &&
						loc != new Point(Location.Position.X + 2, Location.Position.Y + 2))
						return false;

					// Is there a wall between the Team and the location
					int dx = loc.X - Location.Position.X;
					int dy = loc.Y - Location.Position.Y;
					float delta = (float)dy / (float)dx;
					float y = 0;
					for (int pos = Location.Position.X; pos <= loc.X; pos++)
					{
						if (Maze.GetBlock(new Point(pos, Location.Position.Y + (int)y)).Type == BlockType.Wall)
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
		/// <param name="loc">Location to check</param>
		/// <returns>Distance with the Team</returns>
		public Point Distance(Point loc)
		{
			return new Point(loc.X - Location.Position.X, loc.Y - Location.Position.Y);
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
		/// Hit all heroes in the team
		/// </summary>
		/// <param name="damage">Amount of damage</param>
		public void Hit(int damage)
		{
			foreach (Hero hero in Heroes)
			{
				if (hero != null)
					hero.Hit(null, damage);
			}
		}


		/// <summary>
		/// Returns next hero in the team
		/// </summary>
		/// <returns></returns>
		Hero GetNextHero()
		{
			int i = 0;
			for (i = 0; i < HeroCount; i++)
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
			for (i = 0; i < HeroCount; i++)
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
		/// <returns></returns>
		public HeroPosition GetHeroPosition(Hero hero)
		{
			int pos = -1;

			for (int id = 0; id < Heroes.Length; id++)
			{
				if (Heroes[id] == hero)
				{
					pos = id;
					break;
				}
			}

			if (pos == -1)
				throw new ArgumentOutOfRangeException("hero");

			return (HeroPosition)pos;
		}


		/// <summary>
		/// Returns the ground position of a hero
		/// </summary>
		/// <param name="Hero">Hero handle</param>
		/// <returns></returns>
		public GroundPosition GetHeroGroundPosition(Hero Hero)
		{
			GroundPosition groundpos = GroundPosition.Middle;


			// Get the hero position in the team
			HeroPosition pos = GetHeroPosition(Hero);


			switch (Location.Direction)
			{
				case CardinalPoint.North:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = GroundPosition.NorthWest;
					else if (pos == HeroPosition.FrontRight)
						groundpos = GroundPosition.NorthEast;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = GroundPosition.SouthWest;
					else
						groundpos = GroundPosition.SouthEast;
				}
				break;
				case CardinalPoint.East:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = GroundPosition.NorthEast;
					else if (pos == HeroPosition.FrontRight)
						groundpos = GroundPosition.SouthEast;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = GroundPosition.NorthWest;
					else
						groundpos = GroundPosition.SouthWest;
				}
				break;
				case CardinalPoint.South:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = GroundPosition.SouthEast;
					else if (pos == HeroPosition.FrontRight)
						groundpos = GroundPosition.SouthWest;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = GroundPosition.NorthEast;
					else
						groundpos = GroundPosition.NorthWest;
				}
				break;
				case CardinalPoint.West:
				{
					if (pos == HeroPosition.FrontLeft)
						groundpos = GroundPosition.SouthWest;
					else if (pos == HeroPosition.FrontRight)
						groundpos = GroundPosition.NorthWest;
					else if (pos == HeroPosition.MiddleLeft || pos == HeroPosition.RearLeft)
						groundpos = GroundPosition.SouthEast;
					else
						groundpos = GroundPosition.NorthEast;
				}
				break;
			}


			return groundpos;
		}



		/// <summary>
		/// Returns the Hero at a given position in the team
		/// </summary>
		/// <param name="pos">Position rank</param>
		/// <returns></returns>
		public Hero GetHeroFromPosition(HeroPosition pos)
		{
			return Heroes[(int)pos];
		}


		/// <summary>
		/// Returns the Hero under the mouse location
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public Hero GetHeroFromLocation(Point location)
		{

			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 2; x++)
				{
					// find the hero under the location 
					if (new Rectangle(366 + x * 144, 2 + y * 104, 130, 104).Contains(location))
						return Heroes[y * 2 + x];

				}
			}

			return null;
		}

		#endregion


		#region Movement


		/// <summary>
		/// Move the team according its facing direction
		/// </summary>
		/// <param name="front">Forward / backward offset</param>
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
		/// <param name="offset">Offset</param>
		private bool Move(Point offset)
		{
			// Can't move and force is false
			if (!CanMove)
				return false;

			// Get informations about the destination block
			Point dst = Location.Position;
			dst.Offset(offset);


			// Check all blocking states
			bool state = true;

			// A wall
			MazeBlock dstblock = Maze.GetBlock(dst);
			if (dstblock.IsBlocking)
				state = false;

			// Stairs
			if (dstblock.Stair != null)
				state = true;

			// Monsters
			if (Maze.GetMonsterCount(dst) > 0)
				state = false;

			// blocking door
			if (dstblock.Door != null && dstblock.Door.IsBlocking)
				state = false;

			// If can't pass through
			if (!state)
			{
				AddMessage(Language.GetString(1));
				return false;
			}


			// Leave the current block
			if (MazeBlock != null)
				MazeBlock.OnTeamLeave(this);


			Location.Position.Offset(offset);
			LastMove = DateTime.Now;
			HasMoved = true;

			// Enter the new block
			MazeBlock = Maze.GetBlock(Location.Position);
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
			Maze maze = Dungeon.GetMaze(location.Maze);
			if (maze == null)
				return false;

			MazeBlock.OnTeamLeave(this);
		   Location.Position = location.Position;
		   Maze = maze;


			MazeBlock = Maze.GetBlock(Location.Position);

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
		Dungeon Dungeon;


		/// <summary>
		/// Current maze
		/// </summary>
		public Maze Maze;


		/// <summary>
		/// MazeBlock where the team is
		/// </summary>
		public MazeBlock MazeBlock
		{
			get;
			private set;
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
		public Hero[] Heroes;


		/// <summary>
		/// Number of heroes in the team
		/// </summary>
		public int HeroCount
		{
			get
			{
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
		/// Location of the team
		/// </summary>
		public DungeonLocation Location
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
		/// Returns the MazeBlock in front of the team
		/// </summary>
		public MazeBlock FrontBlock
		{
			get
			{
				return Maze.GetBlock(FrontCoord);
			}
		}


		/// <summary>
		/// Returns the location in front of the team
		/// </summary>
		public Point FrontCoord
		{
			get
			{
				Point pos = Point.Empty;

				switch (Location.Direction)
				{
					case CardinalPoint.North:
					pos = new Point(Location.Position.X, Location.Position.Y - 1);
					break;
					case CardinalPoint.South:
					pos = new Point(Location.Position.X, Location.Position.Y + 1);
					break;
					case CardinalPoint.West:
					pos = new Point(Location.Position.X - 1, Location.Position.Y);
					break;
					case CardinalPoint.East:
					pos = new Point(Location.Position.X + 1, Location.Position.Y);
					break;
				}

				return pos;
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
		/// Display font
		/// </summary>
		TextureFont Font;

		/// <summary>
		/// Outlined font
		/// </summary>
		TextureFont OutlinedFont;


		/// <summary>
		/// Messages to display
		/// </summary>
		List<ScreenMessage> Messages;


		/// <summary>
		/// Item hold in the hand
		/// </summary>
		public Item ItemInHand;


		/// <summary>
		/// Window GUI
		/// </summary>
		CampWindow CampWindow;


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
		KeyboardScheme KeyboardScheme;



		/// <summary>
		/// Spell book window
		/// </summary>
		public SpellBook SpellBook
		{
			get;
			private set;
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
		/// 
		/// </summary>
		FrontLeft,

		/// <summary>
		/// 
		/// </summary>
		FrontRight,

		/// <summary>
		/// 
		/// </summary>
		MiddleLeft,

		/// <summary>
		/// 
		/// </summary>
		MiddleRight,

		/// <summary>
		/// 
		/// </summary>
		RearLeft,

		/// <summary>
		/// 
		/// </summary>
		RearRight
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

}
