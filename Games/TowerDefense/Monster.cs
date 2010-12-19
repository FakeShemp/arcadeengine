using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEngine.Graphic;
using ArcEngine;
using System.Drawing;

namespace TowerDefense
{
	public class Monster
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Monster()
		{
			WayPointCoords = new List<Vector2>();
			foreach (Point point in WayPoints)
				WayPointCoords.Add(new Vector2(point.X * Game.Scale.X + Game.Scale.X / 2.0f, point.Y * Game.Scale.Y + Game.Scale.Y / 2.0f));


			Position = WayPointCoords[0];
			TargetID = 1;
			Size = new Vector2(20.0f, 20.0f);
			Speed = 50.0f;
			Health = new Vector2(10.0f, 10.0f);
		}


		/// <summary>
		/// Draws the monster
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch)
		{
			if (batch == null)
				return;

			batch.FillRectangle(Zone, Color.Blue);
			
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			// No health
			if (Health.X <=0)
			{
				IsDead = true;
				return;
			}


			// Update location
			Vector2 target = WayPointCoords[TargetID];
			Vector2 offset = target - Position;

			// New way point
			if (Math.Abs(offset.X) + Math.Abs(offset.Y) < 1.0f)
			{
				TargetID++;

				// Reached the last waypoint
				if (TargetID >= WayPoints.Length)
				{
					IsDead = true;
					return;
				}

				target = WayPointCoords[TargetID];
				offset = target - Position;
			}

			offset.Normalize();
			offset = Vector2.Multiply(offset, Speed * time.ElapsedGameTime.Milliseconds / 1000.0f);

			Position += offset;

		}


		/// <summary>
		/// Draws monster path from Waypoints
		/// </summary>
		/// <param name="batch">Spritebatch to use</param>
		static public void DrawWayPoints(SpriteBatch batch)
		{
			if (batch == null)
				return;

			
			batch.DrawLines(WayPointCoords.ToArray(), Color.Red);

		}


		#region Properties

		/// <summary>
		/// Monster position
		/// </summary>
		public Vector2 Position
		{
			get;
			private set;
		}


		/// <summary>
		/// Zone covered by the monster
		/// </summary>
		public Vector4 Zone
		{
			get
			{
				return new Vector4(Position.X - Size.X / 2.0f, Position.Y - Size.Y / 2.0f, Size.X, Size.Y);
			}
		}


		/// <summary>
		/// Monster size
		/// </summary>
		Vector2 Size;


		/// <summary>
		/// Monster speed
		/// </summary>
		public float Speed
		{
			get;
			private set;
		}


		/// <summary>
		/// Health
		/// </summary>
		/// <remarks>X = current health, Y = maximum health</remarks>
		public Vector2 Health
		{
			get;
			private set;
		}


		/// <summary>
		/// Waypoints screen location
		/// </summary>
		static List<Vector2> WayPointCoords;


		/// <summary>
		/// Way points
		/// </summary>
		static Point[] WayPoints = new Point[]
		{
			new Point(1, -1),
			new Point(1,4),
			new Point(5, 4),
			new Point(5, 2),
			new Point(8, 2),
			new Point(8, 7),
			new Point(4, 7),
			new Point(4, 9),
			new Point(1, 9),
			new Point(1, 11),
			new Point(9, 11),
			new Point(9, 9),
			new Point(11, 9),
			new Point(11, 11),
			new Point(14, 11),
			new Point(14, 7),
			new Point(11, 7),
			new Point(11, 2),
			new Point(13, 2),
			new Point(13, -1),
		};

		/// <summary>
		/// Waypoint id to reach
		/// </summary>
		int TargetID;


		/// <summary>
		/// Does the monster dead
		/// </summary>
		public bool IsDead
		{
			get;
			private set;
		}

		#endregion
	}
}
