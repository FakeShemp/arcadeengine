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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Storage;


namespace ArcEngine.Examples.KinectExample
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
			Window.Text = "Kinect";
		}


		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			// Render states
			Display.RenderState.ClearColor = Color.Black;

			Batch = new SpriteBatch();

			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\lucon.ttf", 12, FontStyle.Regular);

			Kinect = new Kinect(0);
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;

			if (Kinect != null)
				Kinect.Dispose();
			Kinect = null;

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

			if (Keyboard.IsNewKeyPress(Keys.Up))
				Kinect.SetMotorPosition(8000);

			if (Keyboard.IsNewKeyPress(Keys.Down))
				Kinect.SetMotorPosition(-8000);

			for (byte i = 0; i < 8; i++)
			{
				if (Keyboard.IsNewKeyPress(Keys.F1+ i))
					Kinect.SetLED((KinectLedBlinkMode)i);
			}
		}


		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Batch.Begin();


			Batch.DrawString(Font, new Vector2(10, 100), Color.White, "Accelerometer : {0,8:#.00} x {1,8:0.00} x {2,8:0.00}",
				Kinect.Accelerometer.X,
				Kinect.Accelerometer.Y,
				Kinect.Accelerometer.Z);

			Batch.End();
		}



		#region Properties


		/// Texture
		/// </summary>
		Texture2D Texture;


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// Kinect handle
		/// </summary>
		Kinect Kinect;


		#endregion

	}

}
