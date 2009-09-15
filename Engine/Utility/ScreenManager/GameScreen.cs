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
using ArcEngine.Graphic;

namespace ArcEngine.Utility.ScreenManager
{

	/// <summary>
	/// GameScreen
	/// </summary>
	public class GameScreen
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public GameScreen()
		{
			IsPopupScreen = false;
		}



		#region Initialization

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public virtual void LoadContent()
		{
		}


		/// <summary>
		/// Unload content for the screen.
		/// </summary>
		public virtual void UnloadContent()
		{
		}


		#endregion


		#region Update and Draw

		/// <summary>
		/// Allows the screen to run logic, such as updating the transition position.
		/// Unlike HandleInput, this method is called regardless of whether the screen
		/// is active, hidden, or in the middle of a transition.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hasFocus">Allows the screen to handle user input, otherwise it shouldn't</param>
		/// <param name="isCovered">True if screen is covered by an other screen</param>
		public virtual void Update(GameTime time, bool hasFocus,  bool isCovered)
		{
			IsScreenCovered = isCovered;
			IsScreenActive = hasFocus;

			if (IsExiting)
			{
				// If the screen is going away to die, it should transition off.
				ScreenState = ScreenState.Leaving;

				if (!UpdateTransition(time, FadeOutTime))
				{
					// When the transition finishes, remove the screen.
					ScreenManager.RemoveScreen(this);
				}
			}
			else if (isCovered)
			{
				// If the screen is covered by another, it should transition off.
				if (UpdateTransition(time, FadeInTime))
				{
					// Still busy transitioning.
					ScreenState = ScreenState.Leaving;
				}
				else
				{
					// Transition finished!
					ScreenState = ScreenState.Hidden;
				}
			}
			else
			{
				// Otherwise the screen should transition on and become active.
				if (UpdateTransition(time, FadeInTime))
				{
					// Still busy transitioning.
					ScreenState = ScreenState.Entering;
				}
				else
				{
					// Transition finished!
					ScreenState = ScreenState.Active;
				}
			}
		}


		/// <summary>
		/// Allows the screen to handle user input. Unlike Update, this method
		/// is only called when the screen is active, and not when some other
		/// screen has taken the focus.
		/// </summary>
		public virtual void HandleInput()
		{
		}



		/// <summary>
		/// Helper for updating the screen transition position.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="time"></param>
		bool UpdateTransition(GameTime gameTime, TimeSpan time)
		{
			// How much should we move by?
			float transitionDelta;

			if (time == TimeSpan.Zero)
				transitionDelta = 1;
			else
				transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
												  time.TotalMilliseconds);

			// Update the transition position.
			int dir = ScreenState == ScreenState.Entering ? 1 : -1;
			FadingTime += transitionDelta * dir;

			// Did we reach the end of the transition?
			if (((dir < 0) && (FadingTime <= 0)) ||
				 ((dir > 0) && (FadingTime >= 1)))
			{
				if (FadingTime > 1)
					FadingTime = 1;
				if (FadingTime < 0)
					FadingTime = 0;

				return false;
			}

			// Otherwise we are still busy transitioning.
			return true;
		}



		/// <summary>
		/// Draws the GameScreen
		/// </summary>
		public virtual void Draw()
		{

		}


		#endregion


		#region Public Methods


		/// <summary>
		/// Tells the screen to go away. 
		/// </summary>
		public void ExitScreen()
		{
			// If the screen has a zero transition time, remove it immediately.
			if (FadeOutTime == TimeSpan.Zero)
				ScreenManager.RemoveScreen(this);
			else
				IsExiting = true;

		}


		#endregion


		#region Properties


		/// <summary>
		/// Does the screen has focus
		/// </summary>
		public bool IsScreenActive
		{
			get;
			protected set;
		}

		/// <summary>
		/// Popup GameScreen. This allow for example transparent gamestate (Menu over the main game).
		/// </summary>
		public bool IsPopupScreen
		{
			get;
			protected set;
		}


		/// <summary>
		/// Gets the manager that this screen belongs to.
		/// </summary>
		public ScreenManager ScreenManager
		{
			get;
			internal set;
		}


		/// <summary>
		/// State of the screen
		/// </summary>
		public ScreenState ScreenState
		{
			get;
			protected set;
		}


		/// <summary>
		/// Indicates how long the screen takes to
		/// transition on when it is activated.
		/// </summary>
		public TimeSpan FadeInTime
		{
			get;
			protected set;
		}

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition off when it is deactivated.
		/// </summary>
		public TimeSpan FadeOutTime
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets the current elapsed time of the screen transition.
		/// </summary>
		public float FadingTime
		{
			get;
			protected set;
		}


		/// <summary>
		/// Fadeout the screen
		/// </summary>
		public bool IsExiting
		{
			get;
			protected internal set;
		}


		/// <summary>
		/// Is the screen covered by another screen
		/// </summary>
		public bool IsScreenCovered
		{
			get;
			protected set;
		}

		#endregion
	}


	/// <summary>
	/// Enum describes the screen transition state.
	/// </summary>
	public enum ScreenState
	{
		/// <summary>
		/// Entering the screen
		/// </summary>
		Entering,


		/// <summary>
		/// Screen is the active one
		/// </summary>
		Active,


		/// <summary>
		/// Leaving the screen
		/// </summary>
		Leaving,


		/// <summary>
		/// Screen is hidden
		/// </summary>
		Hidden,
	}



}
