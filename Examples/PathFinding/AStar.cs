using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


// http://www.policyalmanac.org/games/aStarTutorial.htm
// http://www.siteduzero.com/tutoriel-3-34333-le-pathfinding-avec-a.html
// http://www.3dbuzz.com/vbforum/showthread.php?176912-Wolf-s-A*-pathfinding-tutorial.
// http://wiki.gamegardens.com/Path_Finding_Tutorial
// 
//
//

namespace ArcEngine.Examples.PathFinding
{
	/// <summary>
	/// 
	/// </summary>
	public class AStar
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="size">Size of the grid</param>
		public AStar(Size size)
		{
			NodeQueue = new PriorityQueue<PathNode>();

			GridSize = size;

			Nodes = new PathNode[GridSize.Width * GridSize.Height];
			for(int y = 0; y < size.Height; y++)
				for (int x = 0 ; x < size.Width ; x++)
				{
					Nodes[y * GridSize.Width + x] = new PathNode(new Point(x, y));
				}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public List<PathNode> FindPath(Point start, Point end)
		{

			Clear();
			
			// Add root node
			PathNode startnode = GetNode(start);
			startnode.Parent = null;
			startnode.IsOpen = false;
			NodeQueue.Push(startnode);
			

			


			PathNode dest = GetNode(end);

			int MovementCost = 10;



			while (NodeQueue.Count > 0)
			{
				// No path...
				if (NodeQueue.Count == 0)
				{
					NodeQueue.Clear();
					return null;
				}


				// Get first node
				PathNode node = NodeQueue.Pop();

				// if (this node is the goal)
				if (node == dest)
				{
					return GetPath(node);
				}

				// Move the current node to the closed 
				node.IsOpen = false;

				// All neighbors
				Point[] neighbors = new Point[]
				{
					new Point(0, -1),	// Top
					new Point(1, 0),	// Right
					new Point(0, 1),	// Bottom
					new Point(-1, 0),	// Left

					new Point(1, -1),	// Top right
					new Point(-1, -1),	// Top left
					new Point(1, 1),	// Bottom right
					new Point(-1, 1),	// Bottom left
				};

				// Consider all of its neighbors
				for (int i = 0 ; i < neighbors.Length ; i++)
				{
					// Get next neighbor
					PathNode neighbor = GetNode(node.Location.X + neighbors[i].X, node.Location.Y + neighbors[i].Y);
					if (neighbor == null)
						continue;

					// if (this neighbor is in the closed list and our current g value is lower)
					if (!neighbor.IsOpen && node.G < neighbor.G)
					{
						// update the neighbor with the new, lower, g value 
						neighbor.G = node.G;

						// change the neighbor's parent to our current node
						neighbor.Parent = node;
					}

					// else if (this neighbor is in the open list and our current g value is lower)
					else if (neighbor.IsOpen && node.G < neighbor.G)
					{
						// update the neighbor with the new, lower, g value 
						neighbor.G = node.G;

						// change the neighbor's parent to our current node
						neighbor.Parent = node;
					}

					// else this neighbor is not in either the open or closed list 
					else if (neighbor.IsOpen &&  neighbor.IsWalkable)
					{
						// add the neighbor to the open list and set its g value
						neighbor.Parent = node;
						neighbor.G = node.G + MovementCost;
						neighbor.H = GetHeuristic(neighbor.Location, end);
						neighbor.IsOpen = false;
						NodeQueue.Push(neighbor);
					}

				}

			}

			return null;
		}


		/// <summary>
		/// Clear
		/// </summary>
		public void Clear()
		{

			// Clear the queue
			NodeQueue.Clear();


			// Clear nodes
			foreach (PathNode node in Nodes)
			{
				node.Clear();
			}



		}


		/// <summary>
		/// Get the past
		/// </summary>
		/// <returns></returns>
		List<PathNode> GetPath(PathNode node)
		{
			List<PathNode> path = new List<PathNode>();

			while (node.Parent != null)
			{
				path.Add(node);

				node = node.Parent;
			}


			return path;
		}


		/// <summary>
		/// Gets heuristic value
		/// </summary>
		/// <param name="start">Current position</param>
		/// <param name="destination">Destination</param>
		/// <returns></returns>
		int GetHeuristic(Point start, Point destination)
		{
			// Manhattan distance
			return Math.Abs(start.X - destination.X) + Math.Abs(start.Y - destination.Y);
		}


		/// <summary>
		/// Gets a node
		/// </summary>
		/// <param name="location">Location</param>
		/// <returns></returns>
		public PathNode GetNode(Point location)
		{
			return GetNode(location.X, location.Y);
		}


		/// <summary>
		/// Gets a node
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <returns></returns>
		public PathNode GetNode(int x, int y)
		{
			if (x >= GridSize.Width || y >= GridSize.Height ||
				x < 0 || y < 0)
				return null;

			return Nodes[y * GridSize.Width + x];
		}

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		PriorityQueue<PathNode> NodeQueue;


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
		PathNode[] Nodes;

		#endregion
	}
}
