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
using System.Diagnostics;
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

        Font2d Font;

		/// <summary>
		/// Application entry point
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
			ParticleCount = 2000;

			// Open the window
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Fountain";


			// Create particles
			Particles = new Particle[ParticleCount * 2];
			for (int i = 0; i < Particles.Length; i++)
				Particles[i] = new Particle();

			// Colors for the particles
			Colors = new Color[]
			{
				Color.Red,
				Color.Blue,
				Color.Green,
				Color.Yellow
			};

			Watch = new Stopwatch();
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


			// Creates the batch
			Batch = new Batch();
			//Batch.Size = Particles.Length;


			// Load the texture
			Texture = new Texture("data/particle.png");

			// Create a font
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

			// Reset particles
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

			Watch.Reset();
			if (Keyboard.IsKeyPress(Keys.D))
			{
				Watch.Start();
				foreach (Particle particle in Particles)
				{
					Display.Color = Color.FromArgb(particle.Alpha, particle.Color);
					Texture.Blit(new Point((int)particle.Location.X, (int)particle.Location.Y));
				}
				Watch.Stop();


				msg = "Direct mode";
			}
			else
			{


				Watch.Start();
				Batch.Clear();
				foreach (Particle particle in Particles)
				{
					Batch.AddRectangle(new Rectangle((int)particle.Location.X, (int)particle.Location.Y, Texture.Size.Width, Texture.Size.Height), Color.FromArgb(particle.Alpha, particle.Color), Texture.Rectangle);
				}
				Batch.Apply();

				Display.Texture = Texture;
				Display.DrawBatch(Batch, BeginMode.Quads);
				Watch.Stop();


				msg = "Batch mode";
			}


            Font.Color = Color.SpringGreen;
			Font.DrawText(new Point(10, 220), "BatchCall : {0}", Display.RenderStats.BatchCall.ToString());
			Font.DrawText(new Point(10, 100), msg);


			Font.DrawText(new Point(10, 180), "Press 'D' key for direct mode");
			Font.DrawText(new Point(10, 200), "DirectCall : {0}", Display.RenderStats.DirectCall.ToString());
			Font.DrawText(new Point(10, 240), "TextureBinding {0}", Display.RenderStats.TextureBinding.ToString());
			Font.DrawText(new Point(10, 260), "Elapsed time : {0} ms", Watch.ElapsedMilliseconds.ToString());
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
		/// Number of particle
		/// </summary>
		int ParticleCount;


		/// <summary>
		/// Particule texture
		/// </summary>
		Texture Texture;


		/// <summary>
		/// Array of particles
		/// </summary>
		Particle[] Particles;


		/// <summary>
		/// Stopwatch
		/// </summary>
		Stopwatch Watch;


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
	/// Particle informations
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
