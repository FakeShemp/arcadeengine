using System;
using System.Collections.Generic;
using System.Text;

namespace ArcEngine.Examples
{
	/// <summary>
	/// Mesh object
	/// </summary>
	public class Mesh : IDisposable
	{

		/// <summary>
		/// 
		/// </summary>
		public Mesh()
		{

			IndexBuffer = new BufferObject<uint>();
			VertexBuffer = new BufferObject<float>();
		}




		/// <summary>
		/// Loads a mesh
		/// </summary>
		/// <param name="name">COLLADA file name</param>
		/// <returns>True on success</returns>
		public bool Load(string name)
		{
			ColladaLoader loader = new ColladaLoader();
			loader.Load(name);


			return false;
		}


		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
		}


		#region Properties

		/// <summary>
		/// Index buffer
		/// </summary>
		BufferObject<uint> IndexBuffer;

		/// <summary>
		/// Vertex buffer
		/// </summary>
		BufferObject<float> VertexBuffer;

		#endregion

	}
}
