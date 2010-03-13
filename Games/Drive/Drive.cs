using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK.Graphics.OpenGL;



// From CodeSample : http://www.codesampler.com/oglsrc/oglsrc_4.htm

namespace Drive
{
	class Game : GameBase
	{
		/// <summary>
		/// Application entry point
		/// </summary>
		[STAThread]
		static void Main()
		{
			Game game = null;
			try
			{
				using (game = new Game())
					game.Run();
			}
			catch (Exception e)
			{
				Trace.WriteLine("");
				Trace.WriteLine("!!!FATAL ERROR !!!");
				Trace.WriteLine("Message : " + e.Message);
				Trace.WriteLine("StackTrace : " + e.StackTrace);
				Trace.WriteLine("");

				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Game()
		{
		}




		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.
		/// </summary>
		public override void LoadContent()
		{
			GameWindowParams param = new GameWindowParams();
			param.Samples = 0;
			param.Size = new Size(1200, 800);
			CreateGameWindow(param);

			Window.Text = "Drive";
			Window.Resizable = true;


			Font = new BitmapFont();
			Font.LoadTTF(@"data/verdana.ttf", 10, FontStyle.Regular);


			Map = new Map();
			CircleRadius = 32;
		}




		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public override void UnloadContent()
		{
			Map.Dispose();
			ResourceManager.ClearAssets();
		}




		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Keyboard.IsKeyPress(Keys.PageUp))
				Map.Scale += 0.0125f;
			if (Keyboard.IsKeyPress(Keys.PageDown))
				Map.Scale -= 0.0125f;


			int speed = 6;

			if (Keyboard.IsKeyPress(Keys.Left))
			{
				Map.Location.Offset(-speed, 0);
				//if (Map.Location.X < 0)
				//    Map.Location = new Point(0, Map.Location.Y);
			}
			if (Keyboard.IsKeyPress(Keys.Right))
			{
				Map.Location.Offset(speed, 0);
				//if (Map.Location.X > Map.Size.Width)
				//    Map.Location = new Point(Map.Size.Width, Map.Location.Y);
			}
			if (Keyboard.IsKeyPress(Keys.Up))
			{
				Map.Location.Offset(0, -speed);
				//if (Map.Location.Y < 0)
				//    Map.Location = new Point(Map.Location.X, 0);
			}
			if (Keyboard.IsKeyPress(Keys.Down))
			{
				Map.Location.Offset(0, speed);
				//if (Map.Location.Y > Map.Size.Height)
				//    Map.Location = new Point(Map.Location.X, Map.Size.Height);
			}
		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			Display.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();

			Map.Draw();

			Display.DrawCircle(Mouse.Location, CircleRadius, Color.Red);
			Font.DrawText(new Point(10, 100), Color.White, Map.Scale.ToString());
			Font.DrawText(new Point(10, 120), Color.White, Map.Location.ToString());
		}


		#region Properties


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// 
		/// </summary>
		Map Map;


		/// <summary>
		/// 
		/// </summary>
		int CircleRadius;



		#endregion
	}
}
