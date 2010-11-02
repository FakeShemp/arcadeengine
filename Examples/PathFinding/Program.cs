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
			CreateGameWindow(new Size(1280, 768));
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

			MapSize = new Size(100, 100);
			BlockSize = new Size(5, 5);
			PathFinder = new AStar(MapSize);

			ClearMap();
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
					PathNode node = PathFinder.GetNode(x, y);
					Color color = node.IsWalkable ? Color.White : Color.Black;

					Batch.FillRectangle(new Rectangle(x * BlockSize.Width, y * BlockSize.Height, BlockSize.Width, BlockSize.Height), color);

					if (!node.IsOpen)
						Batch.FillRectangle(new Rectangle(x * BlockSize.Width, y * BlockSize.Height, BlockSize.Width, BlockSize.Height), Color.FromArgb(128, Color.LightBlue));
					

				}


			// Draw path
			if (Path != null)
			{
				foreach (PathNode node in Path)
				{
					Point location = new Point(node.Location.X * BlockSize.Width, node.Location.Y * BlockSize.Height);

					// Draw rectangle
					Batch.FillRectangle(new Rectangle(location.X, location.Y, BlockSize.Width, BlockSize.Height), Color.FromArgb(128, Color.Red));
				}
			}



		//	DebugPath();



			if (!Start.IsEmpty)
				Batch.FillRectangle(new Rectangle(Start.X * BlockSize.Width, Start.Y * BlockSize.Height, BlockSize.Width, BlockSize.Height),  Color.Red);
			if (!Destination.IsEmpty)
				Batch.FillRectangle(new Rectangle(Destination.X * BlockSize.Width, Destination.Y * BlockSize.Height, BlockSize.Width, BlockSize.Height),  Color.Green);
			
			// Draw mouse
			int mousex = Mouse.Location.X - (Mouse.Location.X % BlockSize.Width);
			int mousey = Mouse.Location.Y - (Mouse.Location.Y % BlockSize.Height);
			if (mousex < BlockSize.Width * MapSize.Width && mousey < BlockSize.Height * MapSize.Height)
				Batch.DrawRectangle(new Rectangle(mousex, mousey, BlockSize.Width, BlockSize.Height), Color.FromArgb(128, Color.Red));

			// some debug text
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 10), Color.Black, "Mouse location : " + MouseLocation.ToString());
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 25), Color.Red, "Start location : " + Start.ToString());
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 40), Color.Green, "Destination location : " + Destination.ToString());

			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 100), Color.Black, "Press spacebar to reset Start and Destination.");
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 115), Color.Black, "Press enter to find the path.");
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 130), Color.Black, "Press R to make some random unwakable block.");
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 145), Color.Black, "Press C to clear map.");

			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 200), Color.Black, "F = G + H.");
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 215), Color.Black, "G = Movement cost to move from the starting point.");
			Batch.DrawString(Font, new Point(MapSize.Width * BlockSize.Width + 50, 230), Color.Black, "H = Estimated movement cost to move to the final destination.");
			
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

			// Put some unwakable blocks
			if (Keyboard.IsNewKeyPress(Keys.R))
				RandomPlot(200);

			// Put some unwakable blocks
			if (Keyboard.IsNewKeyPress(Keys.C))
				ClearMap();


			// Mouse location in the map
			MouseLocation = new Point(Mouse.Location.X / BlockSize.Width, Mouse.Location.Y / BlockSize.Height);

			if (MouseLocation.X >= MapSize.Width || MouseLocation.Y >= MapSize.Height ||
				MouseLocation.X < 0 || MouseLocation.Y < 0)
				return;


			// Change the map
			if (Mouse.IsButtonDown(MouseButtons.Left))
			{
				PathFinder.GetNode(MouseLocation).IsWalkable = false;
			}

			if (Mouse.IsButtonDown(MouseButtons.Right))
			{
				PathFinder.GetNode(MouseLocation).IsWalkable = true;
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


			// Find the path
			if (!Destination.IsEmpty && !Start.IsEmpty && Keyboard.IsKeyPress(Keys.Enter))
				Path = PathFinder.FindPath(Start, Destination);
		}


		/// <summary>
		/// Debug found path
		/// </summary>
		void DebugPath()
		{
			for (int y = 0 ; y < MapSize.Height ; y++)
				for (int x = 0 ; x < MapSize.Width ; x++)
				{
					PathNode node = PathFinder.GetNode(x, y);
					if (node.IsOpen)
						continue;


					Point location = new Point(node.Location.X * BlockSize.Width, node.Location.Y * BlockSize.Height);
					

					// Print some debug
					Batch.DrawString(Font, new Point(location.X, location.Y), Color.Black, "F = " + node.F);
					Batch.DrawString(Font, new Point(location.X, location.Y + 15), Color.Black, "G = " + node.G);
					Batch.DrawString(Font, new Point(location.X, location.Y + 30), Color.Black, "H = " + node.H);
				}

		}


		/// <summary>
		/// Plot some random unwalkable block
		/// </summary>
		/// <param name="count">Number of block to set</param>
		void RandomPlot(int count)
		{
			
			for (int i = 0 ; i< count ; i++)
			{
				PathFinder.GetNode(Random.Next(0, MapSize.Width - 1), Random.Next(0, MapSize.Height - 1)).IsWalkable = false;
			}
		}


		/// <summary>
		/// Clear the map
		/// </summary>
		void ClearMap()
		{
			for (int y = 0 ; y < MapSize.Height ; y++)
				for (int x = 0 ; x < MapSize.Width ; x++)
					PathFinder.GetNode(x, y).IsWalkable = true;


			for (int x = 0 ; x < MapSize.Width ; x++)
			{
				PathFinder.GetNode(x, 0).IsWalkable = false;
				PathFinder.GetNode(x, MapSize.Height - 1).IsWalkable = false;
			}
			for (int y = 0 ; y < MapSize.Height ; y++)
			{
				PathFinder.GetNode(0, y).IsWalkable = false;
				PathFinder.GetNode(MapSize.Width - 1, y).IsWalkable = false;
			}

		}
		
		
		
		#endregion


		#region Properties


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


		/// <summary>
		/// RootDirectory finder
		/// </summary>
		AStar PathFinder;


		/// <summary>
		/// RootDirectory
		/// </summary>
		List<PathNode> Path;


		#endregion
	}
}
