#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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

namespace ArcEngine.Utility.GameState
{

	/// <summary>
	/// A simple state manager
	/// </summary>
	public class StateManager
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public StateManager()
		{
			States = new Stack<State>();
		}



		/// <summary>
		/// Removes previous states and sets a new one
		/// </summary>
		/// <param name="state">State's handle</param>
		public void SetState(State state)
		{
			while (States.Count > 0)
				PopState();

			PushState(state);
		}



		/// <summary>
		/// Adds a state
		/// </summary>
		/// <param name="state">State's handle</param>
		public void PushState(State state)
		{
			if (state == null)
				return;

			if (CurrentState != null)
				CurrentState.OnDeactivated();

			States.Push(state);
			CurrentState.OnEnter();
			CurrentState.OnActivated();
		}



		/// <summary>
		/// Removes the current state
		/// </summary>
		public void PopState()
		{
			if (CurrentState == null)
				return;

			CurrentState.OnDeactivated();
			CurrentState.OnLeave();

			States.Pop();
		}


		/// <summary>
		/// Update game states
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
			// Removes expired states
			while(CurrentState != null && CurrentState.Exit)
				PopState();

			if (CurrentState == null)
				return;

			CurrentState.Update(time);
		}



		/// <summary>
		/// Draws states
		/// </summary>
		public void Draw()
		{
			if (CurrentState == null)
				return;

			CurrentState.Draw();
		}



		#region Properties


		/// <summary>
		/// Statck of states
		/// </summary>
		Stack<State> States;



		/// <summary>
		/// Gets the current state
		/// </summary>
		public State CurrentState
		{
			get
			{
				if (States.Count == 0)
					return null;

				return States.Peek();
			}
		}

		#endregion
	}
}
