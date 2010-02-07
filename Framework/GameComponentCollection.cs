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
using System.Collections.ObjectModel;
using System.Text;

namespace ArcEngine
{
	/// <summary>
	/// A collection of game components. 
	/// </summary>
	public class GameComponentCollection : Collection<GameComponent>
	{

		#region overrides

		/// <summary>
		/// Removes all children from the collection. 
		/// </summary>
		protected override void ClearItems()
		{
			for (int i = 0; i < base.Count; i++)
			{
				OnComponentRemoved(base[i]);
			}
			base.ClearItems();
		}


		/// <summary>
		/// Inserts a <see cref="GameComponent"/> into the collection at the specified location. 
		/// </summary>
		/// <param name="index">Position</param>
		/// <param name="item">Component</param>
		protected override void InsertItem(int index, GameComponent item)
		{
			if (base.IndexOf(item) != -1)
			{
				return;
			}

			base.InsertItem(index, item);
			if (OnComponentAdded != null)
			{
				OnComponentAdded(item);
			}
		}


		/// <summary>
		/// Removes a <see cref="GameComponent"/> object in the collection. 
		/// </summary>
		/// <param name="index">Position</param>
		protected override void RemoveItem(int index)
		{
			GameComponent gameComponent = base[index];
			base.RemoveItem(index);
			if (gameComponent != null)
			{
				OnComponentRemoved(gameComponent);
			}
		}


		/// <summary>
		/// Modifies the specified <see cref="GameComponent"/> object in the collection. 
		/// </summary>
		/// <param name="index">Position</param>
		/// <param name="item">Component</param>
		protected override void SetItem(int index, GameComponent item)
		{
			//base.SetItem(index, item);
		}


		#endregion


		#region Events

		/// <summary>
		/// Arguments used with events from the GameComponentCollection. 
		/// </summary>
		/// <param name="component">Added component</param>
		public delegate void ComponentAddedEventHandler(GameComponent component);

	
		/// <summary>
		/// Raised when a component is added to the GameComponentCollection.
		/// </summary>
		public event ComponentAddedEventHandler OnComponentAdded;



		/// <summary>
		/// Arguments used with events from the GameComponentCollection. 
		/// </summary>
		/// <param name="component">Removed component</param>
		public delegate void ComponentRemovedEventHandler(GameComponent component);


		/// <summary>
		/// 	 Raised when a component is removed from the GameComponentCollection. 
		/// </summary>
		public event ComponentRemovedEventHandler OnComponentRemoved;

		#endregion
	}
}
