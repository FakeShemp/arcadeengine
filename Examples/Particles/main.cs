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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace ArcEngine.Examples.Particles
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Fountain : Game
	{

		// Creates a new TextPrinter to draw text on the screen.
        //TextPrinter printer = new TextPrinter(TextQuality.Medium);
        //Font sans_serif = new Font(FontFamily.GenericSansSerif, 18.0f);

        Font2d Font;

		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (Fountain game = new Fountain())
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
		public Fountain()
		{
			Window.ClientSize = new Size(1024, 768);
			Window.Text = "Fountain";


			Particles = new Particle[1024 * 2];
			for (int i = 0; i < Particles.Length; i++)
				Particles[i] = new Particle();


			Colors = new Color[]
			{
				Color.Red,
				Color.Blue,
				Color.Green,
				Color.Yellow
			};
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Display settings
			Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);


			// Init all particles
			for(int i = 0; i < Particles.Length; i++)
			{
				Particles[i] = new Particle();
				ResetParticle(Particles[i]);
			}


			Batch = new Batch();
			Batch.Size = Particles.Length;


			Texture = new Texture("data/particle.png");

            Font = new Font2d();
            Font.LoadTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			Batch.Dispose();
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

			if (Keyboard.IsKeyPress(Keys.Space))
			{
				foreach (Particle particle in Particles)
					ResetParticle(particle);
			}



			// Update all particles
			foreach (Particle particle in Particles)
			{
				particle.Alpha -= particle.AlphaFade;
				if (particle.Alpha <= 0.0)
				{
					ResetParticle(particle);
					continue;
				}


				particle.Location.X += particle.Velocity.X;
				particle.Location.Y += particle.Velocity.Y;

				particle.Velocity.Y += 0.1f;
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

			string msg;

			if (Keyboard.IsKeyPress(Keys.D))
			{
				foreach (Particle particle in Particles)
				{
					Display.Color = Color.FromArgb(particle.Alpha, particle.Color);
					Texture.Blit(new Point((int)particle.Location.X, (int)particle.Location.Y));
				}


				msg = "Direct mode";
			}
			else
			{


				Batch.Begin();
				foreach (Particle particle in Particles)
				{
					Batch.Blit(new Rectangle((int)particle.Location.X, (int)particle.Location.Y, Texture.Size.Width, Texture.Size.Height), Texture.Rectangle, Color.FromArgb(particle.Alpha, particle.Color));
				}
				Batch.End();

				Display.Texture = Texture;
				Display.DrawBatch(Batch, BeginMode.Quads);


				msg = "Batch mode";
			}

            Font.Color = Color.SpringGreen;
            Font.DrawText(msg, new Point(10, 100));


            Font.DrawText("Press 'D' key for direct mode", new Point(10, 180));
            Font.DrawText("DirectCall : " + Display.RenderStats.DirectCall.ToString(), new Point(10, 200));
            Font.DrawText("BatchCall : " + Display.RenderStats.BatchCall.ToString(), new Point(10, 220));
            Font.DrawText("TextureBinding : " + Display.RenderStats.TextureBinding.ToString(), new Point(10, 240));




		}


		/// <summary>
		/// Resets a particle
		/// </summary>
		/// <param name="part"></param>
		void ResetParticle(Particle part)
		{
			part.Alpha = 255 ;
			part.AlphaFade = Random.Next(1, 255);

			if (Mouse.Buttons == MouseButtons.Left)
				part.Location = new PointF(Mouse.Location.X + Random.Next(-15, 15), Mouse.Location.Y + Random.Next(-15, 15));

			else
				part.Location = new PointF(Window.Size.Width / 2 + Random.Next(-15, 15), Window.Size.Height / 2 + Random.Next(-15, 15));

			part.Velocity = new PointF(Random.Next(-3, 3), Random.Next(-8, 1));
			part.Color = Colors[Random.Next(0, Colors.Length)];
		}




		#region Properties


		/// <summary>
		/// Particule texture
		/// </summary>
		Texture Texture;


		/// <summary>
		/// Array of particles
		/// </summary>
		Particle[] Particles;


		/// <summary>
		/// Random generator
		/// </summary>
	//	Random Random;


		/// <summary>
		/// Available colors
		/// </summary>
		Color[] Colors;


		/// <summary>
		/// Rendering Batch
		/// </summary>
		Batch Batch;



		/// <summary>
		/// Font
		/// </summary>
		//TTFFont Font;

		#endregion

	}



	/// <summary>
	/// Texture informations
	/// </summary>
	class Particle
	{
		/// <summary>
		/// Location
		/// </summary>
		public PointF Location;

		/// <summary>
		/// Velocity
		/// </summary>
		public PointF Velocity;

		/// <summary>
		/// alpha value 
		/// </summary>
		public int Alpha;

		/// <summary>
		/// Fading speed
		/// </summary>
		public int AlphaFade;

		/// <summary>
		/// Texture color
		/// </summary>
		public Color Color;
	};

}
