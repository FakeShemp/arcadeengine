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
		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			Display.ClearBuffers();

			Map.Draw();

			Display.DrawCircle(Mouse.Location, CircleRadius, Color.Red);
			Font.DrawText(new Point(10, 100), Color.White, GameTime.ElapsedRealTime.ToString());
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
