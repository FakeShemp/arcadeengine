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
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;


namespace ArcEngine.Examples.Joystick
{
	public class GamePadProject : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (GamePadProject game = new GamePadProject())
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
		public GamePadProject()
		{
			Messages = new List<string>();

			CreateGameWindow(new Size(1024, 768));
			Window.Text = "GamePad demo";
			Window.Resizable = true;


		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;

			SpriteBatch = new Graphic.SpriteBatch();
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 16, FontStyle.Regular);

			Gamepad.Init(Window);
			CheckDevices();
		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;
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



			if (Keyboard.IsNewKeyPress(Keys.F1))
				CheckDevices();


			if (Keyboard.IsNewKeyPress(Keys.K))
				Gamepad.SetVibration(0, 100, 100);

		}


		/// <summary>
		/// Check for new devices
		/// </summary>
		void CheckDevices()
		{
			Gamepad.CheckForDevices();
			Gamepad.OnUnplug += new Gamepad.UnpluggedDevice(GamePad_OnUnplug);

			Messages.Clear();
		}


		/// <summary>
		/// Unplugged device
		/// </summary>
		/// <param name="id"></param>
		void GamePad_OnUnplug(int id)
		{
			Messages.Add("Device " + id.ToString() + " unplugged.");
		}


		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Display.DrawRectangle(new Rectangle(100, 100, 100, 100), Color.Red);

			SpriteBatch.Begin();

			SpriteBatch.DrawString(Font, new Vector2(100, 50), Color.White, "Press F1 to detect new gamepads...");
			SpriteBatch.DrawString(Font, new Vector2(100, 90), Color.White, "Available device(s) : {0}", Gamepad.Count);


			int y = 100;
			for(int id = 0; id < Gamepad.Count; id++)
			{
				GamepadCapabilities caps = Gamepad.GetCapabilities(id);
				SpriteBatch.DrawString(Font, new Vector2(100, y + 20 + id * 20), Color.White, "id {0} : \"{1}\"", id, caps.InstanceName);

				GamePadState state = Gamepad.GetState(id);
				SpriteBatch.DrawString(Font, new Vector2(100, y + 40 + id * 20), Color.White, "X : {0}", state.X);
				SpriteBatch.DrawString(Font, new Vector2(100, y + 60 + id * 20), Color.White, "Y : {0}", state.Y);
				SpriteBatch.DrawString(Font, new Vector2(100, y + 80 + id * 20), Color.White, "Z : {0}", state.Z);

				SpriteBatch.DrawString(Font, new Vector2(300, y + 40 + id * 20), Color.White, "Pov 0 : {0}", state.PovControllers[0]);
				SpriteBatch.DrawString(Font, new Vector2(300, y + 60 + id * 20), Color.White, "Pov 1 : {0}", state.PovControllers[1]);
				SpriteBatch.DrawString(Font, new Vector2(300, y + 80 + id * 20), Color.White, "Pov 2 : {0}", state.PovControllers[2]);
			

				// Buttons state
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < state.GetButtons.Length; i++)
				{
					if (state.GetButtons[i])
					{
						sb.Append(i);
						sb.Append(" ");
					}
				}
				SpriteBatch.DrawString(Font, new Vector2(100, y + 100 + id * 20), Color.White, "Pressed buttons : {0}", sb);


				y += 120;
			}


			y = 400;
			foreach (string str in Messages)
			{
				SpriteBatch.DrawString(Font, new Vector2(100, y), Color.White, str);

				y += 20;
			}

			SpriteBatch.End();
		}




		#region Properties


		/// <summary>
		/// TTF font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Events messages
		/// </summary>
		List<string> Messages;

		#endregion

	}
}
