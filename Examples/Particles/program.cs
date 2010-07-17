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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;

namespace ArcEngine.Examples.Particles
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Program : GameBase
	{

		/// <summary>
		/// Application entry point
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
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			ParticleCount = 2000;

			// Open the window
			GameWindowParams param = new GameWindowParams();
			param.Size = new Size(1024, 768);
			param.Major = 3;
			param.Minor = 0;
			CreateGameWindow(param);
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
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.One);



			// Load the texture
			Textures = new Texture2D[2];
			Textures[0] = new Texture2D("data/particle.png");
			Textures[1] = new Texture2D("data/particle2.png");

			// Init all particles
			for (int i = 0; i < Particles.Length; i++)
			{
				Particles[i] = new Particle();
				ResetParticle(Particles[i]);
			}

			// Create a font
			Font = new BitmapFont();
			Font.LoadTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);

			// SpriteBatch
			Sprite = new SpriteBatch();
	}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Sprite != null)
				Sprite.Dispose();
			Sprite = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			for (int i = 0; i < Textures.Length; i++)
			{
				if (Textures[i] != null)
					Textures[i].Dispose();
				Textures[i] = null;
			}
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
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			string msg = "empty";

			if (Keyboard.IsKeyPress(Keys.D))
			{
				msg = "Immediate mode";
				Sprite.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, false);
			}
			else
			{
				msg = "Texture sort mode";
				Sprite.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Texture, false);
			}

			Watch.Reset();
			Watch.Start();
			foreach (Particle particle in Particles)
				Sprite.Draw(particle.Texture, particle.Location, Color.FromArgb(particle.Alpha, particle.Color));
			Watch.Stop();
			Sprite.End();


			// Some blah blah...
			Sprite.Begin();
			Sprite.DrawString(Font, new Vector2(10, 220), Color.White, "BatchCall : {0}", Display.Statistics.BatchCall);
			Sprite.DrawString(Font, new Vector2(10, 100), Color.White, msg);
			Sprite.DrawString(Font, new Vector2(10, 180), Color.White, "Press 'D' key for direct mode");
			Sprite.DrawString(Font, new Vector2(10, 200), Color.White, "Shader binding : {0}", Display.Statistics.ShaderBinding);
			Sprite.DrawString(Font, new Vector2(10, 240), Color.White, "TextureBinding {0}", Display.Statistics.TextureBinding);
			Sprite.DrawString(Font, new Vector2(10, 260), Color.White, "Elapsed time : {0} ms", Watch.ElapsedMilliseconds);
			Sprite.End();
		}


		/// <summary>
		/// Resets a particle
		/// </summary>
		/// <param name="part">Particle to reset</param>
		void ResetParticle(Particle part)
		{
			part.Alpha = 255;
			part.AlphaFade = Random.Next(1, 255);
			part.Texture = Textures[Random.Next(100) > 90 ? 1 : 0];
			part.Velocity = new Vector2(Random.Next(-3, 3), Random.Next(-8, 1));
			part.Color = Colors[Random.Next(0, Colors.Length)];
			part.Rotation = Random.Next(100) / 100.0f;

			if (Mouse.Buttons == MouseButtons.Left)
				part.Location = new Vector2(Mouse.Location.X + Random.Next(-15, 15), Mouse.Location.Y + Random.Next(-15, 15));
			else
				part.Location = new Vector2(Window.Size.Width / 2.0f + Random.Next(-15, 15), Window.Size.Height / 2.0f + Random.Next(-15, 15));

			part.Location = Vector2.Add(part.Location, new Vector2(-part.Texture.Size.Width / 2.0f, -part.Texture.Size.Height / 2.0f));

		}




		#region Properties

		/// <summary>
		/// Number of particle
		/// </summary>
		int ParticleCount;


		/// <summary>
		/// Particule texture
		/// </summary>
		Texture2D[] Textures;


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
		/// Sprite batch
		/// </summary>
		SpriteBatch Sprite;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


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
		public Vector2 Location;

		/// <summary>
		/// Velocity
		/// </summary>
		public Vector2 Velocity;

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

		/// <summary>
		/// Texture to use
		/// </summary>
		public Texture2D Texture;

		/// <summary>
		/// Rotation angle
		/// </summary>
		public float Rotation;
	};

}
