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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ArcEngine.PInvoke;
using System.Collections.Generic;


// http://blog.paranoidferret.com/index.php/2008/04/03/winforms-accessing-mouse-and-keyboard-state/
// http://cboard.cprogramming.com/showthread.php?p=783623

namespace ArcEngine.Input
{
	/// <summary>
	/// Handles the keyboard
	/// </summary>
	public static class Keyboard
	{
		/// <summary>
		/// Constructor
		/// </summary>
		static Keyboard()
		{
			Trace.WriteDebugLine("[Keyboard] Constructor()");
			PreviousState = new bool[256];
			CurrentState = new bool[256];
		}



		/// <summary>
		/// Update the keyboard status.
		/// </summary>
		static public void Update()
		{

			// Scan each key
			for (int i = 0; i < 256; i++)
			{
				if (PreviousState[i] != CurrentState[i])
				{
					if (CurrentState[i])
						KeyDown(new PreviewKeyDownEventArgs((Keys)i));
					else
						KeyUp(new KeyEventArgs((Keys)i));
				}					
				PreviousState[i] = CurrentState[i];
				CurrentState[i] = (User32.GetKeyState(i) & 0x8000) != 0;
			}
		}



		/// <summary>
		/// KeyDown event
		/// </summary>
		/// <param name="e"></param>
		static internal void KeyDown(PreviewKeyDownEventArgs e)
		{
			if (OnKeyDown != null)
				OnKeyDown(null, e);
		}


		/// <summary>
		/// KeyUp event
		/// </summary>
		/// <param name="e"></param>
		static internal void KeyUp(KeyEventArgs e)
		{
			if (OnKeyUp != null)
			    OnKeyUp(null, e);
		}


		/// <summary>
		/// Gets if a key is pressed
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>True if pressed</returns>
		public static bool IsKeyPress(Keys key)
		{
			return CurrentState[(int)key];
		}

	
		/// <summary>
		/// Gets if a key is released
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>True if released</returns>
		public static bool IsKeyReleased(Keys key)
		{
			return !CurrentState[(int)key];
		}


		/// <summary>
		/// Gets if a new key is down
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>True if down</returns>
		public static bool IsNewKeyPress(Keys key)
		{
			return CurrentState[(int)key] && !PreviousState[(int)key];
		}


		/// <summary>
		/// Gets if a new key is up
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>True if up</returns>
		public static bool IsNewKeyUp(Keys key)
		{
			return !CurrentState[(int)key] && PreviousState[(int)key];
		}



		/// <summary>
		/// Gets a list of all pressed keys
		/// </summary>
		/// <returns>List of pressed keys</returns>
		public static List<Keys> GetPressedKeys()
		{
			List<Keys> list = new List<Keys>();

			for (int i = 0; i < 254; i++)
			{
				if (IsKeyPress((Keys)i))
					list.Add((Keys)i);
			}
			return list;
		}


		/// <summary>
		/// Gets a list of all released keys
		/// </summary>
		/// <returns>List of released keys</returns>
		public static List<Keys> GetReleasedKeys()
		{
			List<Keys> list = new List<Keys>();

			for (int i = 0; i < 254; i++)
			{
				if (IsKeyReleased((Keys)i))
					list.Add((Keys)i);
			}
			return list;
		}



		#region Events

		/// <summary>
		///  Event fired when a KeyDown occur
		/// </summary>
		public static event EventHandler<PreviewKeyDownEventArgs> OnKeyDown;


		/// <summary>
		/// Event fired when a KeyUp occurs
		/// </summary>
		public static event EventHandler<KeyEventArgs> OnKeyUp;


		#endregion


		#region Properties

		/// <summary>
		/// Current state of the keyboard
		/// </summary>
		static bool[] CurrentState;

		/// <summary>
		/// Previous state pf the keyboard
		/// </summary>
		static bool[] PreviousState;

		#endregion

	}


}
