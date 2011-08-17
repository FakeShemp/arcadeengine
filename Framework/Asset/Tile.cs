#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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

using System.Drawing;
using System.ComponentModel;


namespace ArcEngine.Asset
{

	/// <summary>
	/// Definition of a tile
	/// </summary>
	public class Tile
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="source"></param>
		/// <param name="origin"></param>
		public Tile(Rectangle source, Point origin)
		{
			Rectangle = source;
			Origin = origin;
		}

		/// <summary>
		/// 
		/// </summary>
		public Tile()
		{
			Rectangle = Rectangle.Empty;
			Origin = Point.Empty;
		}



		#region Properties

		/// <summary>
		/// Tag
		/// </summary>
		public const string Tag = "tile";

	
		/// <summary>
		/// Gets / sets the rectangle of the tile in the texture
		/// </summary>
		public Rectangle Rectangle
		{
			get;
			set;
		}



		/// <summary>
		/// Size of the tile
		/// </summary>
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return Rectangle.Size;
			}
		}

									


		/// <summary>
		/// HotSpot of the tile
		/// </summary>
		public Point Origin
		{
			get;
			set;
		}


		/// <summary>
		/// Gets/sets the collision box of the tile relative to the hotspot
		/// (Usefull for sprites)
		/// </summary>
		public Rectangle CollisionBox
		{
			get;
			set;
		}


		/// <summary>
		/// Get tile colors
		/// </summary>
		[Browsable(false)]
		public Color[,] Data
		{
			get;
			internal set;
		}


/*
		/// <summary>
		/// Collision mask
		/// </summary>
		public bool[,] CollisionMask
		{
			get;
			set;
		}
*/
		#endregion

	}

}
