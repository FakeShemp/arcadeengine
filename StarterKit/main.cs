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
        Font2d Font;
	
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
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public StarterKit()
		{
			Window.ClientSize = new Size(1024, 768);
			Window.Text = "Starter Kit";

            elapsed = 0;
            Effect = EffectMode.Shear;
        }



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
            Display.ClearColor = Color.White;
            Font = new Font2d();
            Font.LoadTTF(@"c:\windows\fonts\verdana.ttf", 12, FontStyle.Regular);
            Font.Color = Color.Black;


            Smiley = new Texture("smiley.png");
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
			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

            elapsed += gameTime.ElapsedGameTime.TotalSeconds;
            SineWave = (float)(Math.Sin(elapsed) + 1) / 2;


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


            switch (Effect)
            {
                case EffectMode.Shear:
                {
                    Display.Transform(1.0f, 0.0f, SineWave - 0.5f, 1.0f, 0.0f, 0.0f);
                    Smiley.Blit(Location);
                }
                break;

                case EffectMode.Scale:
                {
                    Display.Scale(SineWave, SineWave);
                    Display.Translate((Display.ViewPort.Width / 2.0f) * (1 - SineWave), (Display.ViewPort.Height / 2.0f) *(1 - SineWave));
                    Smiley.Blit(Location);
                }
                break;

                case EffectMode.Rotate:
                {
                    Smiley.Blit(Location, (float)(SineWave * Math.PI * 120.0f), new Point(Smiley.Size.Width / 2, Smiley.Size.Height / 2));
                }
                break;
            }

            Display.DefaultMatrix();

            Font.DrawText("Press S to Shear", new Point(10, 450));
            Font.DrawText("Press R to Rotate", new Point(10, 470));
            Font.DrawText("Press D to Scale", new Point(10, 490));




            Font.DrawText("Rotate : " + (SineWave * Math.PI * 2).ToString(), new Point(10, 230));
            Font.DrawText("SineWave : " + SineWave.ToString(), new Point(10, 250));
            Font.DrawText("elapsed : " + elapsed.ToString(), new Point(10, 270));

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
