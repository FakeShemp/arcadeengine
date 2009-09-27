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
using ArcEngine.Asset;
using System;
using System.Collections.Generic;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine.Input;

using ArcEngine;
using System.Windows.Forms;
//
// Exemple de console : http://mindshifter.wordpress.com/2008/06/21/xna-console-library/
//
//
//
//
//
//
//
//
//
namespace ArcEngine.Games.RuffnTumble
{
	/// <summary>
	/// 
	/// </summary>
	public class ConsoleMenu
	{


		#region Initialization


		/// <summary>
		/// ctor
		/// </summary>
		public ConsoleMenu()
		{
			LogMsg.Add(message);
			IsActive = false;

			BackgroundColor = Color.FromArgb(130, Color.Black);
			
			
			// Intercepte les messages du clavier
			//Events.OnKeyDown += new EventHandler<KeyboardDownEventArgs>(OnKeyDown);
		}


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			font = ResourceManager.CreateAsset<Font2d>("Console");
			if (font == null)
			{
				return false;
			}
			


			return true;
		}
		#endregion


		#region Update and Draw

		/// <summary>
		/// 
		/// </summary>
		/// <param name="elapsed"></param>
		public void Update(int elapsed)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public void Draw()
		{
			if (!IsActive)
				return;

			Display.Texturing = false;
			Display.Color = BackgroundColor;

			Display.Rectangle(Rectangle, true);
			Display.Color = Color.White;
			Display.Texturing = true;


			//if (font == null)
			//   return;
			//Display.Font = font;

			/*				
						int fontheight = (int)font.MeasureString(prefix).Y;
						int y = 0;

						// Saves a copy of the current screen into the texture.  This allows the menus to be transparent
						GraphicsDisplay.ResolveBackBuffer(texture);

						batch.Begin();

						// Clear the texture ?
						batch.Draw(texture, new Rectangle(0, 0, width, GraphicsDisplay.Viewport.Height), Color.White);

						// Draw transparent menu
						batch.Draw(texture, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), Color.Gray);


						// Draw command string
						batch.DrawString(font, prefix + command, new Vector2(10.0f, height - (fontheight * 1.0f)), Color.White);
			*/
			// Draw log
			Point pos = new Point(0, Rectangle.Height);
			for (int i = 0; i < LogMsg.Count; i++)
			{

				// Change the text color
				Color color = Color.White;


				// Y text location
				pos.Y -= font.Size.Height;

				// Draw text
				//Font.DrawText(pos, LogMsg[i]);
			}




		}


		#endregion


		#region Helper Functions

		/// <summary>
		/// Checks for inputs
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == ToggleKey)
			{
				if (!Rectangle.IsEmpty)
					IsActive = !IsActive;
				return;
			}

			/*
						switch (e.Key)
						{
							case ToggleKey:
							{
								IsActive = false;
							}
							break;
						}
			*/
			/*
						// Toggle the console menu on or off
						if (e.Character == (char)ToggleKey && e.RepeatCount == 1)
						{
							isActive = !isActive;

							// Clears the log when the menu closes
							if (isActive == false)
							{
								log.Clear();
								log.Add(message);
							}
						}

			
						// Check input for backspace
						else if (e.Character == (char)Keys.Back && command.Length > 0)
							command = command.Remove(command.Length - 1, 1);


						// Check input for enter
						else if (e.Character == (char)Keys.Enter)
						{
							log.Add(command);
							ExecuteCommand();
							command = "";
						}

						// Add the key to the command
						else if (e.Character >= ' ')
							command += e.Character;
			*/
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <todo>
		/// - Gerer la couleur du texte a afficher
		/// - Avec Shift + up / Shift + down on peut scroller dans le log
		/// </todo>
		public void AddLog(string msg)
		{
			LogMsg.Add(msg);
		}


		/// <summary>
		/// Parses commands
		/// </summary>
		void ExecuteCommand()
		{
			// Checking your command string can be done many different ways.  I decided to use StartsWith and EndsWith.
			// This allows you to use ignore casing so the user can enter "Help", "hELp", "helP", "HELP", etc. and the
			// command will still work.
			//
			// The reason I used EndsWith is so the string has to be exactly "Help" for example.  "Help bladhsdas" will
			// not work.
			//
			// The following commands are just examples.  You can change them however you want.


			if (command.StartsWith("Help", true, null) && command.EndsWith("Help", true, null))
			{
				LogMsg.Add(logPrefix + "COMMANDS - Displays a list of commands");
				LogMsg.Add(logPrefix + "COMPONENTS - Displays a list of components to use with the commands");
			}
			else if (command.StartsWith("Commands", true, null) && command.EndsWith("Commands", true, null))
			{
				LogMsg.Add(logPrefix + "TITLE string - Change the title to a user defined string");
				LogMsg.Add(logPrefix + "TOGGLE component - Turns the component on or off");
				LogMsg.Add(logPrefix + "QUIT - Exits the program");
			}
			else if (command.StartsWith("Components", true, null) && command.EndsWith("Components", true, null))
			{
				LogMsg.Add(logPrefix + "Background - Displays the sunset background");
				LogMsg.Add(logPrefix + "FPS - Displays the current frames per second");
			}
			else if (command.StartsWith("Title ", true, null))
			{
				//Game.Window.Title = command.Remove(0, 6);
				LogMsg.Add(logPrefix + "Title updated");
			}
			else if (command.StartsWith("Quit", true, null) && command.EndsWith("Quit", true, null))
			{
				//Game.Exit();
			}
			else
				LogMsg.Add(logPrefix + "Command does not exist");
		}


		#endregion


		#region Properties

		/// <summary>
		/// Gets/sets the console dimension
		/// </summary>
		public Rectangle Rectangle
		{
			get;
			set;
		}

		private string command = "";
		//string prefix = ">";
		string logPrefix = "--> ";
		string message = "";


		/// <summary>
		/// Historic
		/// </summary>
		private List<string> LogMsg = new List<string>();


		/// <summary>
		/// The that toggles the console
		/// </summary>
		public Keys ToggleKey = Keys.Tab;


		/// <summary>
		/// Is the console active ?
		/// </summary>
		public bool IsActive;


		Font2d font;

/*
		/// <summary>
		/// Gets / sets the font
		/// </summary>
		public Font2D Font
		{
			get;
			set;
		}

*/


		/// <summary>
		/// Gets/sets the background color
		/// </summary>
		public Color BackgroundColor
		{
			get;
			set;
		}


		#endregion
	}
}