using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEngine.Examples.MeshLoader.MD5
{
	/// <summary>
	/// 
	/// </summary>
	public class Vertex
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
		public Vertex(Vector2 texture, int start, int count)
		{
			Texture = texture;
			StartWeight = start;
			WeightCount = count;
		}

		/// <summary>
		/// Texture coordinates
		/// </summary>
		Vector2 Texture;

		/// <summary>
		/// 
		/// </summary>
		int StartWeight;

		/// <summary>
		/// 
		/// </summary>
		int WeightCount;
	}
}
