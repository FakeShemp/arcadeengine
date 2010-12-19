using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using ArcEngine.Input;
using ArcEngine.Storage;

namespace TowerDefense
{
	class Game : GameBase
	{
		///<summary>
		/// Application entry point
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Start tracing
			Trace.Start("log.html", "Dungeon Eye");

			using (Game game = new Game())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Game()
		{
			Scale = new Vector2(32.0f, 32.0f);
			Monsters = new List<Monster>();

			Towers = new List<Tower>();
			Towers.Add(new Tower(new Point(5, 5)));
			Towers.Add(new Tower(new Point(10, 7)));

			SpawnRate = TimeSpan.FromSeconds(1.0f);
			LastSpawn = DateTime.Now;
		}


		/// <summary>
		/// Load content
		/// </summary>
		public override void LoadContent()
		{
			ResourceManager.AddStorage(new BankStorage("data/data.bnk"));

			GameWindowParams param = new GameWindowParams();
			param.Major = 2;
			param.Minor = 1;
			param.Compatible = true;
			param.Size = new Size(512, 512);
			CreateGameWindow(param);

			Window.Text = "Tower Defense";

			Batch = new SpriteBatch();

			Display.RenderState.ClearColor = Color.CornflowerBlue;

			Background = new Texture2D("background.png");
		}


		/// <summary>
		/// Unload content
		/// </summary>
		public override void UnloadContent()
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (Background != null)
				Background.Dispose();
			Background = null;
		}


		/// <summary>
		/// Draws the game
		/// </summary>
		public override void Draw()
		{
			Display.ClearBuffers();

			Batch.Begin();


			// Background
			Batch.Draw(Background, Point.Empty, Color.White);

			// Way points
			if (Keyboard.IsKeyPress(Keys.Space))
				Monster.DrawWayPoints(Batch);


			// Monsters
			foreach (Monster monster in Monsters)
				monster.Draw(Batch);

			// Towers
			foreach (Tower tower in Towers)
				tower.Draw(Batch);


		//	Batch.DrawCircle(Mouse.Location, new Point(120, 120), Color.FromArgb(128, Color.Black));


			Batch.End();
		}


		/// <summary>
		/// Update the game
		/// </summary>
		/// <param name="gameTime">Elapsed game time</param>
		public override void Update(GameTime gameTime)
		{

			base.Update(gameTime);

			#region Keyboard


			#endregion


			#region Mouse

			// Left mouse button down
			if (Mouse.IsNewButtonDown(MouseButtons.Left))
			{
				// Tower under 
				Point coord = new Point((int)(Mouse.Location.X / Scale.X), (int)(Mouse.Location.Y / Scale.Y));
				foreach (Tower tower in Towers)
				{
					if (tower.Location == coord)
					{
						Selected = tower;
						break;
					}
				}


				// Monster under
				foreach (Monster monster in Monsters)
				{
					RectangleF zone = new RectangleF(monster.Zone.X, monster.Zone.Y, monster.Zone.Width, monster.Zone.Height);
					if (zone.Contains(Mouse.Location.X, Mouse.Location.Y))
					{
						Selected = monster;
						break;
					}
				}
			}

			#endregion


			#region Monsters

			// Remove dead monsters
			Monsters.RemoveAll(
				delegate(Monster monster)
				{
					return monster.IsDead;
				}
			);


			// Time to add new monsters ?
			if (LastSpawn + SpawnRate < DateTime.Now)
			{
				Monsters.Add(new Monster());
				LastSpawn = DateTime.Now;
			}


			// Monsters
			foreach (Monster monster in Monsters)
			{
				monster.Update(gameTime);
			}
			#endregion


			#region Towers
			foreach (Tower tower in Towers)
				tower.Update(gameTime);

			#endregion

		}



		#region Properties

		/// <summary>
		/// Sprite batch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Background texture
		/// </summary>
		Texture2D Background;


		/// <summary>
		/// Monsters
		/// </summary>
		List<Monster> Monsters;


		/// <summary>
		/// Towers
		/// </summary>
		List<Tower> Towers;


		/// <summary>
		/// Monster spawn rate
		/// </summary>
		TimeSpan SpawnRate;


		/// <summary>
		/// Last time a monster spwaned
		/// </summary>
		DateTime LastSpawn;


		/// <summary>
		/// Number of monster alive
		/// </summary>
		int MonsterCount
		{
			get
			{
				return Monsters.Count;
			}
		}


		/// <summary>
		/// Number of towers
		/// </summary>
		int TowerCount
		{
			get
			{
				return Towers.Count;
			}
		}


		/// <summary>
		/// Game scale
		/// </summary>
		static public Vector2 Scale
		{
			get;
			private set;
		}


		/// <summary>
		/// Selected object
		/// </summary>
		object Selected;

		#endregion
	}
}
