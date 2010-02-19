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
using System.Text;

namespace ArcEngine.Utility.GameState
{

	/// <summary>
	/// Interface for a state
	/// </summary>
	public abstract class State
	{

		/// <summary>
		/// Fired the first time the state is called
		/// </summary>
		public virtual void OnEnter()
		{
		}


		/// <summary>
		/// Fired just before exiting
		/// </summary>
		public virtual void OnLeave()
		{
		}


		/// <summary>
		/// Fired when the state is activated
		/// </summary>
		public virtual void OnActivated()
		{
		}


		/// <summary>
		/// Fired when the state is deactivated
		/// </summary>
		public virtual void OnDeactivated()
		{
		}


		/// <summary>
		/// Time to update
		/// </summary>
		/// <param name="time">Elapsed time</param>
		public virtual void Update(GameTime time)
		{
		}


		/// <summary>
		/// Time to render
		/// </summary>
		public virtual void Draw()
		{
		}


		#region


		/// <summary>
		/// Set to true to exit
		/// </summary>
		public bool Exit
		{
			get;
			protected set;
		}

		#endregion
	}
}
