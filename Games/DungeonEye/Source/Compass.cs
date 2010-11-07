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
using System.Drawing;

namespace DungeonEye
{
	/// <summary>
	/// TurnLeft
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
		/// <param name="direction">Initial direction</param>
		/// <param name="rot">Rotation needed</param>
		/// <returns>Final direction</returns>
		static public CardinalPoint Rotate(CardinalPoint direction, CompassRotation rot)
		{
			CardinalPoint[][] points = new CardinalPoint[][]
			{
				// North
				new CardinalPoint[] 
				{
					CardinalPoint.East,
					CardinalPoint.South,
					CardinalPoint.West,
					CardinalPoint.North,
				},

				// South
				new CardinalPoint[] 
				{
					CardinalPoint.West,
					CardinalPoint.North,
					CardinalPoint.East,
					CardinalPoint.South,
				},

				// West
				new CardinalPoint[] 
				{
					CardinalPoint.North,
					CardinalPoint.East,
					CardinalPoint.South,
					CardinalPoint.West,
				},

				// East
				new CardinalPoint[] 
				{
					CardinalPoint.South,
					CardinalPoint.West,
					CardinalPoint.North,
					CardinalPoint.East,
				},
			};

			return points[(int)direction][(int)rot];
		}


		/// <summary>
		/// Is facing a specific direction
		/// </summary>
		/// <param name="dir">Direction</param>
		/// <returns>True if facing the same direction</returns>
		public bool IsFacing(CardinalPoint dir)
		{
			return Direction == dir;
		}


		/// <summary>
		/// Returns the direction in which an entity should face to look at.
		/// </summary>
		/// <param name="from">From location</param>
		/// <param name="target">Target direction</param>
		/// <returns>Direction to face to</returns>
		public CardinalPoint SeekDirection(DungeonLocation from, DungeonLocation target)
		{
			Point delta = new Point(target.Coordinate.X - from.Coordinate.X, target.Coordinate.Y - from.Coordinate.Y);
			CardinalPoint dir = CardinalPoint.North;


			// Move west
			if (delta.X < 0)
			{
				if (delta.Y > 0)
					return CardinalPoint.South | CardinalPoint.West;
				else if (delta.Y < 0)
					return CardinalPoint.North | CardinalPoint.West;
				else
					return CardinalPoint.West;
			}

			// Move east
			else if (delta.X > 0)
			{
				if (delta.Y > 0)
					return CardinalPoint.South | CardinalPoint.East;
				else if (delta.Y < 0)
					return CardinalPoint.North | CardinalPoint.East;
				else
					return CardinalPoint.East;
			}

			if (delta.Y > 0)
				return CardinalPoint.South;
			else if (delta.Y < 0)
				return CardinalPoint.North;

			return CardinalPoint.North;
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
	/// TurnLeft direction
	/// </summary>
	[Flags]
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
