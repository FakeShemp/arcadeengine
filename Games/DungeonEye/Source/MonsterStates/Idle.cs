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
using System.Drawing;

namespace DungeonEye.MonsterStates
{

	/// <summary>
	/// Monster is idle
	/// </summary>
	public class IdleState : MonsterState
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster">Monster handle</param>
		public IdleState(Monster monster) : base(monster)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			// Target range
			int range = Game.Random.Next(5);

			// Direction to face to
			CardinalPoint direction = CardinalPoint.North;

			while (true)
			{
				int dir = Dice.GetD20(1);
				Point vector = Monster.Location.Position;

				switch (Monster.Location.Direction)
				{
					case CardinalPoint.North:
					{
						if (dir < 10)
						{
							direction = CardinalPoint.West;
							vector.X--;
						}
						else
						{
							direction = CardinalPoint.East;
							vector.X++;
						}
					}
					break;
					case CardinalPoint.South:
					{
						if (dir < 10)
						{
							direction = CardinalPoint.East;
							vector.X++;
						}
						else
						{
							direction = CardinalPoint.West;
							vector.X--;
						}

					}
					break;
					case CardinalPoint.West:
					{
						if (dir < 10)
						{
							direction = CardinalPoint.North;
							vector.Y--;
						}
						else
						{
							direction = CardinalPoint.South;
							vector.Y++;
						}

					}
					break;
					case CardinalPoint.East:
					{
						if (dir < 10)
						{
							direction = CardinalPoint.South;
							vector.Y++;
						}
						else
						{
							direction = CardinalPoint.North;
							vector.Y--;
						}

					}
					break;
				}

				// Check the block
				MazeBlock block = Monster.Location.Maze.GetBlock(vector);
				if (block != null) //&& !block.IsWall)
				{
					Monster.StateManager.PushState(new MoveState(Monster, range, direction));
					return;
				}
			}
		}



	}
}
