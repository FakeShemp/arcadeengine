#region Copyright
//
// Source code coming from http://signum.codeplex.com/SourceControl/changeset/view/25903#510461 & http://www.codeproject.com/KB/recipes/priorityqueue.aspx
//
//
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcEngine
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class PriorityQueue<T>
	{

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public PriorityQueue(): this(Comparer<T>.Default.Compare)
		{
		}


		/// <summary>
		/// Constructor with explicit IComparer (Think about using LambdaComparer)
		/// </summary>
		/// <param name="comparer"></param>
		public PriorityQueue(IComparer<T> comparer)
		{
			this.comparer = comparer.Compare;
		}


		/// <summary>
		/// Constructor with explicit Comparisor
		/// </summary>
		/// <param name="comparer"></param>
		public PriorityQueue(Comparison<T> comparer)
		{
			this.comparer = comparer;
		}

		#endregion



		/// <summary>
		/// Enqueue an element
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public int Push(T element)
		{
			int p = list.Count;
			list.Add(element);
			do
			{
				if (p == 0)
					break;
				int p2 = (p - 1) / 2;
				if (Compare(p, p2) < 0)
				{
					SwitchElements(p, p2);
					p = p2;
				}
				else
					break;
			} while (true);
			return p;
		}


		/// <summary>
		/// Enqueue all the elements
		/// </summary>
		/// <param name="elements"></param>
		public void PushAll(IEnumerable<T> elements)
		{
			foreach (var item in elements)
				Push(item);
		}


		/// <summary>
		/// Dequeue and returns the smallest element 
		/// </summary>
		/// <returns></returns>
		public T Pop()
		{
			if (Empty)
				return default(T); // throw new InvalidOperationException(Resources.EmptyPriorityQueue);

			int p = 0;
			T result = list[0];
			list[0] = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			do
			{
				int pn = p;
				int p1 = 2 * p + 1;
				int p2 = 2 * p + 2;
				if (list.Count > p1 && Compare(p, p1) > 0)
					p = p1;
				if (list.Count > p2 && Compare(p, p2) > 0)
					p = p2;

				if (p == pn)
					break;

				SwitchElements(p, pn);
			} while (true);

			return result;
		}


		/// <summary>
		/// Returns the smallest element without dequeuing 
		/// </summary>
		/// <returns></returns>
		public T Peek()
		{
			if (Empty)
				return default(T); //throw new InvalidOperationException(Resources.EmptyPriorityQueue);

			return list[0];
		}


		/// <summary>
		/// Removes all the elements in the queue
		/// </summary>
		public void Clear()
		{
			list.Clear();
		}


		/// <summary>
		/// Forces a position update of an element already in the queue
		/// Useful if its' comparison value has changed some how
		/// </summary>
		/// <param name="element"></param>
		public void Update(T element)
		{
			Update(list.IndexOf(element));
		}


		/// <summary>
		/// returns true if element is in the queue
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public bool Contains(T element)
		{
			return list.Contains(element);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		int Compare(int i, int j)
		{
			return comparer(list[i], list[j]);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		void SwitchElements(int i, int j)
		{
			T h = list[i];
			list[i] = list[j];
			list[j] = h;
		}


		/// <summary>
		/// It might be necessary to change elements that are already in the queue. 
		/// Because this is not very common (you need to find the element first), it can only be done by the 
		/// explicit IList implementation (the indexer should not be used in any other case). 
		/// Once you set the indexer, the PQ will automatically reorder. Complexity O(ld n) (surprise;))
		/// </summary>
		/// <param name="i"></param>
		void Update(int i)
		{
			int p = i, pn;
			int p1, p2;
			do
			{
				if (p == 0)
					break;
				p2 = (p - 1) / 2;
				if (Compare(p, p2) < 0)
				{
					SwitchElements(p, p2);
					p = p2;
				}
				else
					break;
			} while (true);
			if (p < i)
				return;
			do
			{
				pn = p;
				p1 = 2 * p + 1;
				p2 = 2 * p + 2;
				if (list.Count > p1 && Compare(p, p1) > 0)
					p = p1;
				if (list.Count > p2 && Compare(p, p2) > 0)
					p = p2;

				if (p == pn)
					break;
				SwitchElements(p, pn);
			} while (true);
		}


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		List<T> list = new List<T>();


		/// <summary>
		/// 
		/// </summary>
		Comparison<T> comparer;


		/// <summary>
		/// Number of elements in the queue
		/// </summary>
		public int Count
		{
			get
			{
				return list.Count;
			}
		}


		/// <summary>
		/// Returns true if no element is in the queue
		/// </summary>
		public bool Empty
		{
			get
			{
				return list.Count == 0;
			}
		}



		#endregion


	}
}
