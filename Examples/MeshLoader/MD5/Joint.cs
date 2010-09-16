#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
