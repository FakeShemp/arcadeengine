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
	public class GamePadProject : Game
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
			Font = new Font2d();
			Font.LoadTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);

			Gamepad.Init(Window);
			CheckDevices();
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


		//	if (Keyboard.IsNewKeyPress(Keys.K))
		//		Gamepad.SetVibration(0, 100, 100);

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
			Display.Color = Color.White;


			Font.DrawText(new Point(100, 50), "Press F1 to detect new gamepads...");
			Font.DrawText(new Point(100, 90), "Available device(s) : {0}", Gamepad.Count);


			int y = 100;
			for(int id = 0; id < Gamepad.Count; id++)
			{
				GamepadCapabilities caps = Gamepad.GetCapabilities(id);
				Font.DrawText(new Point(100, y + 20 + id * 20), "id {0} : \"{1}\"", id, caps.InstanceName);

				GamePadState state = Gamepad.GetState(id);
				Font.DrawText(new Point(100, y + 40 + id * 20), "X : {0}", state.X);
				Font.DrawText(new Point(100, y + 60 + id * 20), "Y : {0}", state.Y);
				Font.DrawText(new Point(100, y + 80 + id * 20), "Z : {0}", state.Z);

				Font.DrawText(new Point(300, y + 40 + id * 20), "Pov 0 : {0}", state.PovControllers[0]);
				Font.DrawText(new Point(300, y + 60 + id * 20), "Pov 1 : {0}", state.PovControllers[1]);
				Font.DrawText(new Point(300, y + 80 + id * 20), "Pov 2 : {0}", state.PovControllers[2]);
			

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
				Font.DrawText(new Point(100, y + 100 + id * 20), "Pressed buttons : {0}", sb);


				y += 120;
			}


			y = 400;
			foreach (string str in Messages)
			{
				Font.DrawText(new Point(100, y), str);

				y += 20;
			}
		}




		#region Properties


		/// <summary>
		/// TTF font
		/// </summary>
		Font2d Font;


		/// <summary>
		/// Events messages
		/// </summary>
		List<string> Messages;

		#endregion

	}
}
