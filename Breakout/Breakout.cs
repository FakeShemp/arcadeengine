using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;
using ArcEngine.Input;
using ArcEngine.Asset;
using System.Text;


namespace Breakout
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Breakout : Game
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (Breakout game = new Breakout())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Breakout()
		{
			//Device = new OpenGLRender();
			Window.Size = new Size(800, 600);
			Window.Text = "Breakout";
			Mouse.Visible = false;
			
		}



		/// <summary>
		/// 
		/// </summary>
		public override void LoadContent()
		{
			ResourceManager.AddProvider(new LevelProvider());


			// Load data
			ResourceManager.LoadBank("data/data.bnk");

			// Default font
			//Font = new TextureFont();
			//Font.LoadFromTTF("browa.ttf", 12, FontStyle.Regular);
			//Font.Color = Color.Black;


			Level = ResourceManager.CreateAsset<Level>("Level_1");
			Level.Init();


			// Display settings
			Display.Blending = true;
			Display.ClearColor = Color.Black;


			Lives = 5;

	
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{


			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();


			if (Level != null)
				Level.Update(gameTime);


			if (Level.Balls.Count == 0)
			{
				Lives--;

				Level.Balls.Add(new Ball(Level));
			}

		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();
			Display.Color = Color.White;


			Level.Draw();


			// Left panel
			//Display.Rectangle(new Rectangle(0, 0, 160, 600), true);
			//Font.DrawText(new Point(10, 20), "Level: " + 1);
			//Font.DrawText(new Point(10, 40), "Lives: " + Lives);
			//Font.DrawText(new Point(10, 60), "Score: " + Score);

		}





		#region Properties



		/// <summary>
		/// 
		/// </summary>
		//TextureFont Font;

		/// <summary>
		/// 
		/// </summary>
		public Level Level;



		/// <summary>
		/// 
		/// </summary>
		public int Lives
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public int Score
		{
			get;
			private set;
		}


		#endregion

	}
}
