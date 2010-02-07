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

namespace ArcEngine
{

	/// <summary>
	/// Defines the interface for a drawable game component. 
	/// </summary>
	public interface IDrawable
	{

		#region Events

		/// <summary>
		/// Raised when the Visible property changes. 
		/// </summary>
		event EventHandler VisibleChanged;


		/// <summary>
		/// Raised when the DrawOrder property changes.
		/// </summary>
		event EventHandler DrawOrderChanged;

		#endregion

			
		/// <summary>
		/// Draws the IDrawable.
		/// </summary>
		void Draw();


		#region Properties

		/// <summary>
		/// Indicates whether IDrawable.Draw should be called in Game.Draw for this game component. 
		/// </summary>
		bool Visible { get; }


		/// <summary>
		/// The order in which to draw this object relative to other objects. Objects with a lower value are drawn first.
		/// </summary>
		int DrawOrder { get; }

		#endregion
	}
}
