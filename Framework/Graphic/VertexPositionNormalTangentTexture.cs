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
using System.Runtime.InteropServices;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Describes a custom vertex format structure that contains position, normal, tangent and one set of texture coordinates. 
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public class VertexPositionNormalTangentTexture
	{
		#region Properties

		/// <summary>
		/// The vertex position.
		/// </summary>
		public Vector3 Position;


		/// <summary>
		/// The normal.
		/// </summary>
		public Vector3 Normal;

		/// <summary>
		/// The normal.
		/// </summary>
		public Vector3 Tangent;


		/// <summary>
		/// 	The texture coordinates.
		/// </summary>
		public Vector2 TextureCoordinate;

		#endregion


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="normal">Normal</param>
		/// <param name="textureCoordinate">Texture coordinate</param>
		public VertexPositionNormalTangentTexture(Vector3 position, Vector3 normal, Vector3 tangent, Vector2 textureCoordinate)
		{
			Position = position;
			Normal = normal;
			Tangent = tangent;
			TextureCoordinate = textureCoordinate;
		}


		/// <summary>
		/// Size of the structure in bytes
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 44;
			}
		}



	}
}
