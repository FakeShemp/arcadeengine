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

namespace DungeonEye
{
	/// <summary>
	/// Compass
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
		/// Gets or sets the direction
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
		/// North
		/// </summary>
		North = 0,

		/// <summary>
		/// South
		/// </summary>
		South = 1,

		/// <summary>
		/// West
		/// </summary>
		West = 2,

		/// <summary>
		/// East
		/// </summary>
		East = 3,
	}


		/// <summary>
	/// Type of rotation
	/// </summary>
	public enum CompassRotation
	{
		/// <summary>
		/// Rotate 90°
		/// </summary>
		Rotate90,

		/// <summary>
		/// Rotate 180°
		/// </summary>
		Rotate180,

		/// <summary>
		/// Rotate 270°
		/// </summary>
		Rotate270,
	}


}
