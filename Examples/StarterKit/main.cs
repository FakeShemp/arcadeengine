#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Asset;


namespace ArcEngine.Examples.StarterKit
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class StarterKit : Game
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (StarterKit game = new StarterKit())
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
		public StarterKit()
		{
			// Create the game window
			CreateGameWindow(new Size(1024, 768));


			// Change the window title
			Window.Text = "Starter Kit";

			// Reset our variables
			elapsed = 0;
			Effect = EffectMode.Shear;
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{

			// Clear color of the screen
			Display.ClearColor = Color.White;

			// Load Verdana font
			Font = new Font2d();
			Font.LoadTTF(@"c:\windows\fonts\verdana.ttf", 12, FontStyle.Regular);
			Font.Color = Color.Black;

			// Load a texture
			Smiley = new Texture("smiley.png");

			// Texture location on the screen
			Location = new Point(
				 Display.ViewPort.Width / 2 - Smiley.Size.Width / 2,
				 Display.ViewPort.Height / 2 - Smiley.Size.Height / 2);
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
		}


		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the Excape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

			// Update the motion
			elapsed += gameTime.ElapsedGameTime.TotalSeconds;
			SineWave = (float)(Math.Sin(elapsed) + 1) / 2;


			// Check if a key is just pressed *once*
			if (Keyboard.IsNewKeyPress(Keys.S)) Effect = EffectMode.Shear;
			if (Keyboard.IsNewKeyPress(Keys.D)) Effect = EffectMode.Scale;
			if (Keyboard.IsNewKeyPress(Keys.R)) Effect = EffectMode.Rotate;

		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			// Display the desired effect
			switch (Effect)
			{
				// Shear the smiley
				case EffectMode.Shear:
				{
					Display.Transform(1.0f, 0.0f, SineWave - 0.5f, 1.0f, 0.0f, 0.0f);
					Smiley.Blit(Location);
				}
				break;

				// Scale the smiley a little bit
				case EffectMode.Scale:
				{
					Display.Scale(SineWave, SineWave);
					Display.Translate((Display.ViewPort.Width / 2.0f) * (1 - SineWave), (Display.ViewPort.Height / 2.0f) * (1 - SineWave));
					Smiley.Blit(Location);
				}
				break;

				// Rotate the smiely
				case EffectMode.Rotate:
				{
					Smiley.Blit(Location, (float)(SineWave * Math.PI * 120.0f), new Point(Smiley.Size.Width / 2, Smiley.Size.Height / 2));
				}
				break;
			}

			// Restore default matrix
			Display.DefaultMatrix();


			// Print some text
			Font.DrawText(new Point(10, 450), "Press S to Shear");
			Font.DrawText(new Point(10, 470), "Press R to Rotate");
			Font.DrawText(new Point(10, 490), "Press D to Scale");
		}




		#region Properties

		/// <summary>
		/// A value between 0 and 1, used to rotate rectangle
		/// </summary>
		float SineWave;


		/// <summary>
		/// Effect to use
		/// </summary>
		EffectMode Effect;

		/// <summary>
		/// Elapsed gametime
		/// </summary>
		double elapsed;


		/// <summary>
		/// Texture to display
		/// </summary>
		Texture Smiley;


		/// <summary>
		/// Location of the texture
		/// </summary>
		Point Location;


		/// <summary>
		/// Drawing font
		/// </summary>
		Font2d Font;

		#endregion

	}

	/// <summary>
	/// Effect to apply to the rectangle
	/// </summary>
	enum EffectMode
	{
		/// <summary>
		/// 
		/// </summary>
		Shear,

		/// <summary>
		/// 
		/// </summary>
		Scale,


		/// <summary>
		/// 
		/// </summary>
		Rotate
	}
}
