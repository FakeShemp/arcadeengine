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
using System.Text;
using System.Collections.ObjectModel;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using System.Drawing;


// http://www.games-creators.org/wiki/Gestion_des_Game_States_en_C_plus_plus
// http://gamedevgeek.com/tutorials/managing-game-states-in-c/
// http://www.icad.puc-rio.br/~lvalente/docs/2006_sbgames.pdf
// http://www.codeproject.com/KB/architecture/statepattern3.aspx
// http://www.ogre3d.org/wiki/index.php/Managing_Game_States_with_OGRE
// http://www.gamedev.net/community/forums/topic.asp?topic_id=525171&whichpage=1?
// - Game State : http://gamedevgeek.com/tutorials/managing-game-states-in-c/

namespace ArcEngine.Utility.ScreenManager
{

	/// <summary>
	/// GameScreenManager class
	/// </summary>
	public class ScreenManager
	{

		#region Initialization

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="game">Gamebase handle</param>
		public ScreenManager(GameBase game)
		{
			Trace.WriteLine("Creating new ScreenManager.");
			Screens = new List<GameScreenBase>();
			Game = game;
		}


		/// <summary>
		/// Load your resources
		/// </summary>
		public void LoadContent()
		{
			foreach (GameScreenBase screen in Screens)
			{
				screen.LoadContent();
			}
		}


		/// <summary>
		/// Unload resource
		/// </summary>
		public void UnloadContent()
		{
			foreach (GameScreenBase screen in Screens)
			{
				screen.UnloadContent();
			}
		}

		#endregion



		#region Public Methods


		/// <summary>
		/// Tell all the current screens to fade out
		/// </summary>
		public void ClearScreens()
		{
			foreach (GameScreenBase screen in GetScreens())
			{
				screen.OnLeave();
				screen.ExitScreen();
			}

			Screens.Clear();
		}


		/// <summary>
		/// Pushes a GameScreen over the current game state
		/// </summary>
		/// <param name="screen">Screen handle</param>
		public void AddScreen(GameScreenBase screen)
		{
			if (screen == null)
				return;

			if (Screens.Count > 0)
			{
				GameScreenBase current = Screens[Screens.Count - 1];
				current.OnLeave();

			}

			Screens.Add(screen);
			screen.ScreenManager = this;
			screen.IsExiting = false;

			screen.LoadContent();
			screen.OnEnter();
		}


		/// <summary>
		/// Removes the a screen
		/// </summary>
		/// <param name="screen">Screen to remove</param>
		public void RemoveScreen(GameScreenBase screen)
		{
			if (screen == null)
				return;

			screen.OnLeave();
			screen.UnloadContent();

			Screens.Remove(screen);

			if (Screens.Count > 0)
				Screens[Screens.Count - 1].OnEnter();
		}



		/// <summary>
		/// Expose an array holding all the screens.
		/// </summary>
		public GameScreenBase[] GetScreens()
		{
			return Screens.ToArray();
		}



		#endregion



		#region Update and Draw


		/// <summary>
		/// Update game states
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update_OLD(GameTime time)
		{
			// Make a copy of the master screen list, to avoid confusion if
			// the process of updating one screen adds or removes others.
			List<GameScreenBase> screensToUpdate = new List<GameScreenBase>();
			foreach (GameScreenBase screen in Screens)
				screensToUpdate.Add(screen);

			bool hasFocus = true;
			bool isCovered = false;

			// Update each screens
			while (screensToUpdate.Count > 0)
			{
				// Pop the topmost screen off the waiting list.
				GameScreenBase screen = screensToUpdate[screensToUpdate.Count - 1];
				screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

		
				// Update screen logic
				screen.Update(time, hasFocus, isCovered);

				if (screen.ScreenState == ScreenState.Entering || screen.ScreenState == ScreenState.Active)
				{
					// If this is the first active screen we came across,
					// give it a chance to handle input.
					if (hasFocus)
					{
						screen.HandleInput();

						hasFocus = false;
					}

					// If this is an active non-popup, inform any subsequent
					// screens that they are covered by it.
					if (!screen.IsPopupScreen)
						isCovered = true;
				}

			}
		}


		/// <summary>
		/// Update game states
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
			// No screen to update
			if (Screens.Count == 0)
				return;

			// Make a copy of the master screen list, to avoid confusion if
			// the process of updating one screen adds or removes others.
			Stack<GameScreenBase> screensToUpdate = new Stack<GameScreenBase>();
			foreach (GameScreenBase scr in Screens)
				screensToUpdate.Push(scr);

			GameScreenBase screen = screensToUpdate.Pop();
			screen.Update(time, true, false);
		}



		/// <summary>
		/// Draws game states
		/// </summary>
		public void Draw()
		{
			// No screen to draw
			if (Screens.Count == 0)
				return;

			Stack<GameScreenBase> screensToUpdate = new Stack<GameScreenBase>();
			foreach (GameScreenBase scr in Screens)
				screensToUpdate.Push(scr);

			GameScreenBase screen = screensToUpdate.Pop();
			screen.Draw();

			return;

/*
			foreach (GameScreen screen in Screens)
			{
				if (screen.State == ScreenState.Hidden)
					continue;

				screen.Draw();
			}
*/
		}

		#endregion



		#region Properties

		/// <summary>
		/// GameScreen list
		/// </summary>
		List<GameScreenBase> Screens;


		/// <summary>
		/// Gamebase handle
		/// </summary>
		public GameBase Game
		{
			get;
			private set;
		}


		#endregion
	}
}
