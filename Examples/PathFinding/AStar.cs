using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


// http://www.policyalmanac.org/games/aStarTutorial.htm
// http://www.siteduzero.com/tutoriel-3-34333-le-pathfinding-avec-a.html
// http://www.3dbuzz.com/vbforum/showthread.php?176912-Wolf-s-A*-pathfinding-tutorial.
//

namespace ArcEngine.Examples.PathFinding
{
	/// <summary>
	/// 
	/// </summary>
	public class AStar
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="size">Size of the grid</param>
		public AStar(Size size)
		{
			OpenQueue = new PriorityQueue<PathFinderNode>();

			GridSize = size;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="grid"></param>
		/// <returns></returns>
		public List<PathFinderNode> FindPath(Point start, Point end, byte[,] grid)
		{
			
			// Clear the queue
			OpenQueue.Clear();

			// Add root node
			OpenQueue.Push(GetNode(start));


			return null;

		}


		/// <summary>
		/// Gets a node
		/// </summary>
		/// <param name="location">Location</param>
		/// <returns></returns>
		PathFinderNode GetNode(Point location)
		{
			if (location.X >= GridSize.Width || location.Y >= GridSize.Height ||
				location.X < 0 || location.Y < 0)
				return null;

			return Nodes[location.Y * GridSize.Width + location.X];
		}

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		PriorityQueue<PathFinderNode> OpenQueue;


		/// <summary>
		/// Size of the grid
		/// </summary>
		public Size GridSize
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		Rectangle Rectangle
		{
			get
			{
				return new Rectangle(Point.Empty, GridSize);
			}
		}

		/// <summary>
		/// Nodes
		/// </summary>
		PathFinderNode[] Nodes;

		#endregion
	}
}
