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
		//North = 0,

		//East = 1,
		
		//South = 2,
		
		//West = 3,

		North = 0,
		South = 1,
		West = 2,
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
