#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Collections.Generic;
using ArcEngine.Storage;
using System.IO;

//
// http://www.charliesgames.com/wordpress/?p=441
//
namespace ArcEngine.Examples.MonsterBlob
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Program : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (Program game = new Program())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			GameWindowParams p = new GameWindowParams();
			p.Size = new Size(1024, 768);
			p.Major = 4;
			p.Minor = 1;
			CreateGameWindow(p);
			Window.Text = "Blob Monster - Thanks to http://www.charliesgames.com/wordpress/?p=441";

			Monsters = new List<Monster>();
			Monsters.Add(new Monster(new Vector2(200.0f, 100.0f), 1.0f, 7));
		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Render states
			Display.RenderState.ClearColor = Color.Black;
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			Display.RenderState.Blending = true;

			Batch = new SpriteBatch();

			// Load the texture
			Texture = new Texture2D("data/blob.png");
			Texture.MagFilter = TextureMagFilter.Linear;
			Texture.MinFilter = TextureMinFilter.Linear;

		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;
		}


		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Check if the Escape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

			// Update each monster
			foreach (Monster monster in Monsters)
				monster.Update(gameTime);
		}


		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Batch.Begin();


			Vector4 rect = new Vector4(400, 400, 10, 10);
			Vector2 origin = new Vector2(rect.Width / 2.0f, rect.Height / 2.0f);
			rotation += 2.0f;

			Batch.DrawRectangle(rect, Color.FromArgb(128, Color.Red), rotation, origin, new Vector2(4.0f, 2.0f));



			//Batch.DrawRectangle(rect, Color.White, 0.0f, origin);


			//Batch.Draw(Texture, rect.Xy, Vector4.Zero, Color.White, -rotation, new Vector2(Texture.Size.Width / 2.0f, Texture.Size.Height / 2.0f), new Vector2(3.0f, 1.0f), SpriteEffects.None, 0.0f);
			Batch.DrawPoint(rect.Xy, Color.Red);


			//Batch.Draw(Texture, rect.Xy, Vector4.Zero, Color.FromArgb(128, Color.White), -rotation, new Vector2(Texture.Size.Width / 2.0f, Texture.Size.Height / 2.0f), new Vector2(3.0f, 2.0f), SpriteEffects.None, 0.0f);

			foreach (Monster monster in Monsters)
				monster.Draw(Batch, Texture);


			Batch.End();
		}

		float rotation = 0.0f;

		#region Properties

		/// <summary>
		/// Monster list
		/// </summary>
		List<Monster> Monsters;

		/// Texture
		/// </summary>
		Texture2D Texture;


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch Batch;

		#endregion

	}

}
