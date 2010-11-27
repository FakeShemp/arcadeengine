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
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;

namespace DungeonEye
{
	/// <summary>
	/// Event square
	/// </summary>
	public class EventSquare : SquareActor
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square handle</param>
		public EventSquare(Square square) : base(square)
		{

		}


		#region Properties

		/// <summary>
		/// Direction Team must face to trigger
		/// </summary>
		CardinalPoint Direction
		{
			get;
			set;
		}


		/// <summary>
		/// Audio name
		/// </summary>
		public string AudioName
		{
			get;
			set;
		}


		/// <summary>
		/// Message to display
		/// </summary>
		public string Message
		{
			get;
			set;
		}

		#endregion

	}
}
