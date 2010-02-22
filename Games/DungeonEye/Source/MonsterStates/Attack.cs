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
using ArcEngine.Utility.GameState;
using System.Text;
using ArcEngine;


namespace DungeonEye.MonsterStates
{

	/// <summary>
	/// Monster is attack
	/// </summary>
	public class AttackState : MonsterState
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="manager"></param>
		public AttackState(Monster monster) : base(monster)
		{

		}



		/// <summary>
		/// Update
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public override void Update(GameTime time)
		{
			if (!Monster.CanSee(Monster.Location.Dungeon.Team.Location))
				Exit = true;
		}


	}
}
