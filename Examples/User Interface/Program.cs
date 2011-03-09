using System;
using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Utility.GUI;
using ArcEngine.Input;
using ArcEngine.Storage;

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
				System.Windows.Forms.MessageBox.Show(e.StackTrace, e.Message);
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


			ResourceManager.AddStorage(new BankStorage("data/data.bnk"));


			Manager = new GuiManager();
			Manager.TileSet = ResourceManager.CreateAsset<TileSet>("Skin");
			Manager.Font = BitmapFont.CreateFromTTF(@"C:\Windows\Fonts\Verdana.ttf", 12, FontStyle.Regular);


			window = new Window("Title...");
			window.BgColor = Color.LightCoral;
			window.Location = new Point(100, 100);
			window.Size = new Size(320, 240);
			Manager.Add(window);


			CheckBox checkbox1 = new CheckBox();
			checkbox1.Location = new Point(50, 50);
			checkbox1.Text = "checkbox 1";
			checkbox1.Font = BitmapFont.CreateFromTTF(@"C:\Windows\Fonts\Verdana.ttf", 9, FontStyle.Regular);
			checkbox1.CheckedChanged +=new EventHandler(checkbox_CheckedChanged);
			window.Add(checkbox1);

			CheckBox checkbox2 = new CheckBox();
			checkbox2.Location = new Point(50, 65);
			checkbox1.Text = "checkbox 2";
			checkbox2.Font = BitmapFont.CreateFromTTF(@"C:\Windows\Fonts\Verdana.ttf", 10, FontStyle.Regular);
			checkbox2.CheckedChanged +=new EventHandler(checkbox_CheckedChanged);
			window.Add(checkbox2);




			Font = BitmapFont.CreateFromTTF(@"C:\Windows\Fonts\Verdana.ttf", 9, FontStyle.Regular);
			Batch = new SpriteBatch();
		}



		void checkbox_CheckedChanged(object sender, EventArgs e)
		{
			Trace.WriteLine("Checked !!");	
		}



		/// <summary>
		/// Unload content
		/// </summary>
		public override void UnloadContent()
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

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

			
			Batch.Begin();
			Batch.DrawString(Font, new Point(10, 10), Color.White, "Mouse Location : " + Mouse.Location.ToString());

			if (Manager.ControlUnderMouse != null)
			{
				Batch.DrawString(Font, new Point(10, 25), Color.White, Manager.ControlUnderMouse.ToString() + " : " + Manager.ControlUnderMouse.Location.ToString());
			}


			Batch.End();

			// Draw the gui
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


		/// <summary>
		/// 
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;



		#endregion
	}
}
