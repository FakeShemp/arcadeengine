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
