using System;
using System.Collections.Generic;
using ArcEngine;
using ArcEngine.Graphic;
using System.Windows.Forms;
using System.Drawing;
using ArcEngine.Utility.GUI;
using ArcEngine.Asset;



namespace ArcEngine.Examples.UserInterface
{
	public class Program : GameBase
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (Program game = new Program())
					game.Run();
			}
			catch (Exception e)
			{
				// Oops, an error happened !
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}




		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			// Create the game window
			CreateGameWindow(new Size(1024, 768));

			// Change the window title
			Window.Text = "ArcEngine User interface";


		}


		/// <summary>
		/// Load content
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;

			ResourceManager.LoadBank("data/data.bnk");


			Manager = new GuiManager();
			Manager.TileSet = ResourceManager.CreateAsset<TileSet>("Skin");


			window = new Window(Manager);
			window.BgColor = Color.LightCoral;
			window.Location = new Point(100, 100);
			window.Size = new Size(320, 240);
			Manager.Add(window);
		}



		/// <summary>
		/// Unload content
		/// </summary>
		public override void UnloadContent()
		{
			if (Manager != null)
				Manager.Dispose();
			Manager = null;
		}


		/// <summary>
		/// Updates the game
		/// </summary>
		/// <param name="gameTime">Elapsed game time</param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);


			Manager.Update(gameTime);
		}


		/// <summary>
		/// Draws the game
		/// </summary>
		public override void Draw()
		{
			Display.ClearBuffers();


			Manager.Draw();
		}



		#region Properties

		/// <summary>
		/// Main GUI manager
		/// </summary>
		GuiManager Manager;


		/// <summary>
		/// Window
		/// </summary>
		Window window;

		#endregion
	}
}
