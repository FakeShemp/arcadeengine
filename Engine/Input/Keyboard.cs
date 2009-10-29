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
//using Microsoft.DirectX.DirectInput;



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
			//if (Terminal.Enable && e.KeyCode == Terminal.ToggleKey)
			//   Terminal.Visible = true;

			if (OnKeyUp != null)
			    OnKeyUp(null, e);
		}


/*

		/// <summary>
		/// Returns the state of a Key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static KeyState GetKey(Keys key)
		{

			KeyState state = new KeyState();
			state.CurrentState = CurrentState[(int)key];
			state.PreviousState = PreviousState[(int)key];

			
	//		Trace.WriteLine(key.ToString() + " : " + state.CurrentState.ToString() + " " + state.PreviousState.ToString());

			//if ((int)key == (int)Keys.E)
			//    Trace.WriteLine(key.ToString() + " : " + 'e');

			return state;
		}
*/


		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool IsKeyPress(Keys key)
		{
			return CurrentState[(int)key];
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool IsNewKeyPress(Keys key)
		{
			return CurrentState[(int)key] && !PreviousState[(int)key];
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool IsNewKeyUp(Keys key)
		{
			return !CurrentState[(int)key] && PreviousState[(int)key];
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
