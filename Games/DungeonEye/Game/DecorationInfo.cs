﻿#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Interface;


namespace DungeonEye
{
	/// <summary>
	/// Information about a specific decoration
	/// </summary>
	public class DecorationInfo
	{
		/// <summary>
		/// 
		/// </summary>
		public DecorationInfo()
		{

		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tile">Tile id</param>
		/// <param name="location">Screen location</param>
		public DecorationInfo(int tileId, Point location)
		{
			TileId = tileId;
			Location = location;
		}


		#region properties


		/// <summary>
		/// Tile Id
		/// </summary>
		public int TileId;


		/// <summary>
		/// Display location on the screen
		/// </summary>
		public Point Location;

		/// <summary>
		/// Tile handle
		/// </summary>
		public Tile Tile;

		#endregion

	}
}
