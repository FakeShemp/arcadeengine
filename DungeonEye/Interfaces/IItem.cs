using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEye.Interfaces
{

	/// <summary>
	/// Interface for item
	/// </summary>
	public interface IItem
	{

		/// <summary>
		/// When the team collect an item
		/// </summary>
		/// <param name="item">Item</param>
		/// <param name="team">Team</param>
		void OnCollect(Item item, Team team);


		/// <summary>
		/// When an item is dropped
		/// </summary>
		/// <param name="item">Item</param>
		/// <param name="team">Team</param>
		/// <param name="block">Block where the item is</param>
		void OnDrop(Item item, Team team, MazeBlock block);

		/// <summary>
		/// When an item is used
		/// </summary>
		/// <param name="item"></param>
		/// <param name="team"></param>
		void OnUse(Item item, Team team);
	}
}
