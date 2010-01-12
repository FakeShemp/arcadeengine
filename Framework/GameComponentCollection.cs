using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ArcEngine
{
	/// <summary>
	/// 
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
