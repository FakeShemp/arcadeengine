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
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEye
{
	/// <summary>
	/// 
	/// </summary>
	public class Compass
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Compass()
		{
			Direction = CardinalPoint.North;
		}


		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="compass"></param>
		public Compass(Compass compass)
		{
			Direction = compass.Direction;
		}


		/// <summary>
		/// Rotate the team
		/// </summary>
		/// <param name="rot"></param>
		public void Rotate(CompassRotation rot)
		{


			switch (Direction)
			{
				case CardinalPoint.North:
				{
					switch (rot)
					{
						case CompassRotation.Rotate90:
						{
							Direction = CardinalPoint.East;
						}
						break;

						case CompassRotation.Rotate270:
						{
							Direction = CardinalPoint.West;
						}
						break;

						case CompassRotation.Rotate180:
						{
							Direction = CardinalPoint.South;
						}
						break;
					}
				}
				break;

				case CardinalPoint.South:
				{
					switch (rot)
					{
						case CompassRotation.Rotate90:
						{
							Direction = CardinalPoint.West;
						}
						break;

						case CompassRotation.Rotate270:
						{
							Direction = CardinalPoint.East;
						}
						break;

						case CompassRotation.Rotate180:
						{
							Direction = CardinalPoint.North;
						}
						break;
					}
				}
				break;

				case CardinalPoint.East:
				{
					switch (rot)
					{
						case CompassRotation.Rotate90:
						{
							Direction = CardinalPoint.South;
						}
						break;

						case CompassRotation.Rotate270:
						{
							Direction = CardinalPoint.North;
						}
						break;

						case CompassRotation.Rotate180:
						{
							Direction = CardinalPoint.West;
						}
						break;
					}
				}
				break;

				case CardinalPoint.West:
				{
					switch (rot)
					{
						case CompassRotation.Rotate90:
						{
							Direction = CardinalPoint.North;
						}
						break;

						case CompassRotation.Rotate270:
						{
							Direction = CardinalPoint.South;
						}
						break;

						case CompassRotation.Rotate180:
						{
							Direction = CardinalPoint.East;
						}
						break;
					}
				}
				break;
			}
		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public CardinalPoint Direction
		{
			get;
			set;
		}


		#endregion
	}


	/// <summary>
	/// Compass direction
	/// </summary>
	public enum CardinalPoint
	{
		/// <summary>
		/// 
		/// </summary>
		North = 0,

		/// <summary>
		/// 
		/// </summary>
		South = 1,

		/// <summary>
		/// 
		/// </summary>
		West = 2,

		/// <summary>
		/// 
		/// </summary>
		East = 3,
	}


		/// <summary>
	/// Type of rotation
	/// </summary>
	public enum CompassRotation
	{
		Rotate90,

		Rotate180,

		Rotate270,
	}


}
