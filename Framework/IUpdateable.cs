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
	/// Defines an interface for a component that should be updated in Game.Update. 
	/// </summary>
	public interface IUpdateable
	{

		#region Events

		/// <summary>
		/// Raised when the Enabled property changes. 
		/// </summary>
	//	event EventHandler EnabledChanged;


		/// <summary>
		/// Raised when the UpdateOrder property changes. 
		/// </summary>
	//	event EventHandler UpdateOrderChanged;

		#endregion


		/// <summary>
		/// Called when the game component should be updated.
		/// </summary>
		/// <param name="gameTime"></param>
		void Update(GameTime gameTime);

		
		#region Properties

		/// <summary>
		/// Indicates whether the game component's Update method should be called in Game.Update.
		/// </summary>
		bool Enabled { get; }


		/// <summary>
		/// Indicates when the game component should be updated relative to other game components. Lower values are updated first.
		/// </summary>
		int UpdateOrder { get; }

		#endregion
	}
}
