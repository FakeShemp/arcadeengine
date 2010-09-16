using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEngine.Examples.MeshLoader.MD5
{
	/// <summary>
	/// MD5 Joint
	/// </summary>
	public class Joint
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parentid"></param>
		/// <param name="position"></param>
		/// <param name="quaternion"></param>
		public Joint(string name, int parentid, Vector3 position, Vector3 quaternion)
		{
			Name = name;
			ParentID = parentid;
			Position = position;


			float t = 1.0f - (quaternion.X * quaternion.X) - (quaternion.Y * quaternion.Y) - (quaternion.Z * quaternion.Z);
			float w = 0.0f;
			if (t < 0.0f)
			{
				w = 0.0f;
			}
			else
			{
				w = (float) -Math.Sqrt(t);
			}
			Orientation = new Quaternion(quaternion, w);

		}


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public string Name;


		/// <summary>
		/// 
		/// </summary>
		public int ParentID;


		/// <summary>
		/// 
		/// </summary>
		public Vector3 Position;


		/// <summary>
		/// 
		/// </summary>
		public Quaternion Orientation;

		#endregion
	}
}
