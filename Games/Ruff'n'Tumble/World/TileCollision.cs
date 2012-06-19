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
	}
}
