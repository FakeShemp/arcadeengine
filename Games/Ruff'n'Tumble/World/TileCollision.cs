#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2012 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine;
using System;
using ArcEngine.Input;

namespace RuffnTumble
{
	/// <summary>
	/// Controls the collision detection and response behavior of a tile.
	/// </summary>
	public enum TileCollision
	{
		/// <summary>
		/// A passable tile is one which does not hinder player motion at all.
		/// </summary>
		Passable = 0,

		/// <summary>
		/// An impassable tile is one which does not allow the player to move through
		/// it at all. It is completely solid.
		/// </summary>
		Impassable = 1,

		/// <summary>
		/// A platform tile is one which behaves like a passable tile except when the
		/// player is above it. A player can jump up through a platform as well as move
		/// past it to the left and right, but can not fall down through the top of it.
		/// </summary>
		Platform = 2,

		/// <summary>
		/// A slope tile
		/// </summary>
		Slope = 3,

		/// <summary>
		/// Lader
		/// </summary>
		Ladder = 4,

		/// <summary>
		/// Cause instant death
		/// </summary>
		Death = 5
	}


	/// <summary>
	/// Height data for slopes
	/// </summary>
	class SlopeTileData
	{
		static public byte[,] Data = 
		{
			// Passable
			{32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32},

			// Impassable
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

			// High platform
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

			// Low platform
			{16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16},
			
			// Slope 1
			{6, 6, 8, 8, 8, 8, 10, 10, 10, 10, 12, 12, 12, 12, 12, 12, 14, 14, 14, 14, 14, 14, 14, 14, 16, 16, 16, 16, 16, 16, 16, 16},

			// Slope 2
			{16, 16, 16, 16, 16, 16, 16, 16, 14, 14, 14, 14, 14, 14, 14, 14, 12, 12, 12, 12, 12, 12, 10, 10, 10, 10, 8, 8, 8, 8, 6, 6},

			// Slope 3
			{0, 0, 2, 2, 4, 4, 6, 6, 8, 8, 10, 10, 12, 12, 14, 14, 16, 16, 18, 18, 20, 20, 22, 22, 24, 24, 26, 26, 28, 28, 30, 30},

			// Slope 4
			{30, 30, 28, 28, 26, 26, 24, 24, 22, 22, 20, 20, 18, 18, 16, 16, 14, 14, 12, 12, 10, 10, 8, 8, 6, 6, 4, 4, 2, 2, 0, 0},

			// Slope 5
			{0, 0, 2, 2, 2, 2, 4, 4, 4, 4, 6, 6, 6, 6, 8, 8, 8, 8, 10, 10, 10, 10, 12, 12, 12, 12, 14, 14, 14, 14, 16, 16},

			// Slope 6
			{16, 16, 18, 18, 18, 18, 20, 20, 20, 20, 22, 22, 22, 22, 24, 24, 24, 24, 26, 26, 26, 26, 28, 28, 28, 28, 30, 30, 30, 30, 32, 32},

			// Slope 7
			{32, 32, 30, 30, 30, 30, 28, 28, 28, 28, 26, 26, 26, 26, 24, 24, 24, 24, 22, 22, 22, 22, 20, 20, 20, 20, 18, 18, 18, 18, 16, 16},

			// Slope 8
			{16, 16, 14, 14, 14, 14, 12, 12, 12, 12, 10, 10, 10, 10, 8, 8, 8, 8, 6, 6, 6, 6, 4, 4, 4, 4, 2, 2, 2, 2, 0, 0},

			// Death
			{32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32},

			// Ladder
			{32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32},
		};
	}

}
