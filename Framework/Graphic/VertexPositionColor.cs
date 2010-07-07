#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using System.Runtime.InteropServices;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Describes a custom vertex format structure that contains position and color.
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public struct VertexPositionColor
	{
		#region Properties

		/// <summary>
		/// The vertex position.
		/// </summary>
		public Vector3 Position;


		/// <summary>
		/// 	The vertex color.
		/// </summary>
		public Color Color;


		#endregion


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="color">Color</param>
		public VertexPositionColor(Vector3 position, Color color)
		{
			this.Position = position;
			this.Color = color;
		}


		/// <summary>
		/// Size of the structure in bytes
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 28;
			}
		}



	}
}
