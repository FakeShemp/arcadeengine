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
using ArcEngine;
using ArcEngine.Input;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using System.Xml;

namespace DungeonEye
{
	/// <summary>
	/// Result of the attack of a hero
	/// </summary>
	public class AttackResult
	{
		/// <summary>
		/// Time of the attack
		/// </summary>
		public DateTime Date;


		/// <summary>
		/// Result of the attack.
		/// </summary>
		/// <remarks>If Result == 0 the attack missed</remarks>
		public short Result;


		/// <summary>
		/// Monster involved in the fight.
		/// </summary>
		public Monster Monster;


		/// <summary>
		/// Hom many time the hero have to wait before attacking again with this hand
		/// </summary>
		public TimeSpan OnHold;

	}

}
