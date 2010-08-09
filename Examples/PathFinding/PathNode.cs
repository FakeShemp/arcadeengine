using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace ArcEngine.Examples.PathFinding
{
	/// <summary>
	/// Pathfinding node
	/// </summary>
	public class PathNode : IComparable<PathNode>
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="location">Node location in the map</param>
		public PathNode(Point location)
		{
			Location = location;
			IsWalkable = true;
			IsOpen = true;
		}


		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			IsOpen = true;
			G = 0;
			H = 0;
		}

		#region Comparer

		/// <summary>
		/// 
		/// </summary>
		/// <param name="to"></param>
		/// <returns></returns>
		public int CompareTo(PathNode to)
		{
			if (to.F > F)
				return -1;

			if (to.F == F)
				return 0;

			return 1;
		}

		

		#endregion


		#region Properties

		/// <summary>
		/// Parent node
		/// </summary>
		public PathNode Parent;


		/// <summary>
		/// Is the node walkable
		/// </summary>
		public bool IsWalkable;


		/// <summary>
		/// 
		/// </summary>
		public bool IsOpen;


		/// <summary>
		/// The exact cost to reach this node from the starting node.  
		/// </summary>
		public int G;


		/// <summary>
		/// The estimated(heuristic) cost to reach the destination from here. 
		/// </summary>
		public int H;


		/// <summary>
		/// how expensive we think it will be to reach our goal by way of that node
		/// </summary>
		public int F
		{
			get
			{
				return G + H;
			}
		}


		/// <summary>
		/// Location of the block
		/// </summary>
		public Point Location
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} F = {1}, G = {2}, H = {3}", Location.ToString(), F ,G ,H);
		}


		#endregion
	}
}
