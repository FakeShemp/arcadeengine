using System;
using System.Collections.Generic;
using System.Text;
using DungeonEye;


namespace DungeonEye.Interfaces
{

	/// <summary>
	/// Interface for monsters
	/// </summary>
	public interface IMonster
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		void OnUpdate(Monster monster);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		void OnDraw(Monster monster);



		#region Properties



		#endregion

	}
}
