using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Asset;


namespace ArcEngine.Examples.PathFinding
{
	class Program : GameBase
	{
		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (Program game = new Program())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Resizable = true;

		}


		#region Initialization


		/// <summary>
		/// Called when graphics resources need to be loaded.
		/// </summary>
		public override void LoadContent()
		{
			// Display settings
			Display.RenderState.ClearColor = Color.LightGray;

			// Create a font
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 9, FontStyle.Regular);

			Batch = new SpriteBatch();

			BlockSize = new Size(20, 20);
			MapSize = new Size(25, 25);
			Map = new byte[MapSize.Width * MapSize.Height];
			for (int x = 0 ; x < MapSize.Width ; x++ )
			{
				Map[x] = 1;
				Map[(MapSize.Height - 1) * MapSize.Width + x] = 1;
			}
			for (int y = 0 ; y < MapSize.Height ; y++)
			{
				Map[MapSize.Width * y] = 1;
				Map[MapSize.Width * y + MapSize.Width - 1] = 1;
			}

		}


		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public override void UnloadContent()
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;
		}


		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			Display.ClearBuffers();

			Batch.Begin();

			// Draw map
			for (int y = 0 ; y < MapSize.Height ; y++)
				for (int x = 0; x < MapSize.Width; x++)
				{
					Color color = Map[y * MapSize.Height + x] == 1 ? Color.Black : Color.White;
					Batch.FillRectangle(new Rectangle(x * BlockSize.Width, y * BlockSize.Height, BlockSize.Width, BlockSize.Height), color);
				}

			if (!Start.IsEmpty)
				Batch.FillRectangle(new Rectangle(Start.X * BlockSize.Width, Start.Y * BlockSize.Height, BlockSize.Width, BlockSize.Height), Color.Red);
			if (!Destination.IsEmpty)
				Batch.FillRectangle(new Rectangle(Destination.X * BlockSize.Width, Destination.Y * BlockSize.Height, BlockSize.Width, BlockSize.Height), Color.Green);
			
			// Draw mouse
			int mousex = Mouse.Location.X - (Mouse.Location.X % BlockSize.Width);
			int mousey = Mouse.Location.Y - (Mouse.Location.Y % BlockSize.Height);
			Batch.DrawRectangle(new Rectangle(mousex, mousey, BlockSize.Width, BlockSize.Height), Color.FromArgb(128, Color.Red));

			// some debug text
			Batch.DrawString(Font, new Point(550, 10), Color.White, "Mouse location : " + MouseLocation.ToString());
			Batch.DrawString(Font, new Point(550, 25), Color.Red, "Start location : " + Start.ToString());
			Batch.DrawString(Font, new Point(550, 40), Color.Green, "Destination location : " + Destination.ToString());

			Batch.DrawString(Font, new Point(550, 100), Color.White, "Press spacebar to reset Start and Destination");
			Batch.End();
		}



		/// <summary>
		/// Called when the game has determined that game logic needs to be processed.
		/// </summary>
		/// <param name="gameTime">The time passed since the last update.</param>
		public override void Update(GameTime gameTime)
		{
			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

			// Reset start and destination
			if (Keyboard.IsKeyPress(Keys.Space))
			{
				Start = Point.Empty;
				Destination = Point.Empty;
			}



			// Mouse location in the map
			MouseLocation = new Point(Mouse.Location.X / BlockSize.Width, Mouse.Location.Y / BlockSize.Height);

			if (MouseLocation.X >= MapSize.Width || MouseLocation.Y >= MapSize.Height ||
				MouseLocation.X < 0 || MouseLocation.Y < 0)
				return;


			// Change the map
			if (Mouse.IsButtonDown(MouseButtons.Left))
			{
				Map[MouseLocation.Y * MapSize.Height + MouseLocation.X] = 1;
			}

			if (Mouse.IsButtonDown(MouseButtons.Right))
			{
				Map[MouseLocation.Y * MapSize.Height + MouseLocation.X] = 0;
			}



			// Start and destination locations
			if (Mouse.IsNewButtonDown(MouseButtons.Middle))
			{

				if (Start.IsEmpty)
				{
					Start = MouseLocation;
				}
				else
				{
					Destination = MouseLocation;
				}
			}


		}



		#endregion


		#region Properties

		/// <summary>
		/// Map data
		/// </summary>
		byte[] Map;


		/// <summary>
		/// Map size
		/// </summary>
		Size MapSize;

		
		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Map location of the mouse
		/// </summary>
		Point MouseLocation;


		/// <summary>
		/// Size of each block in the map
		/// </summary>
		Size BlockSize;


		/// <summary>
		/// Start point
		/// </summary>
		Point Start;


		/// <summary>
		/// Destination point
		/// </summary>
		Point Destination;
		#endregion
	}
}
