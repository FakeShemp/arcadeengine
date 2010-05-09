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
using System.Text;
using OpenTK.Graphics.OpenGL;
using ArcEngine.Graphic;


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
			//IndexBuffer = new ArrayBuffer<uint>();
			//VertexBuffer = new ArrayBuffer<float>();

			Index = new IndexBuffer();
			Buffer = new ArrayBuffer<float>();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public void SetVertices(float[] data)
		{
			Buffer.Update(data);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public void SetIndices(uint[] data)
		{
			Index.UpdateIndices(data);

		}



		/// <summary>
		/// 
		/// </summary>
		public void Draw()
		{
			//VertexBuffer.Enable(1);
			//IndexBuffer.Enable(2);


			//GL.DrawElements(BeginMode.Triangles, count, DrawElementsType.UnsignedInt, 0);

			//VertexBuffer.Disable();
			//IndexBuffer.Disable();
		}


		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			if (Index != null)
				Index.Dispose();

			//VertexBuffer.Dispose();
		}





		#region Properties

		/// <summary>
		/// Index buffer
		/// </summary>
		//ArrayBuffer<uint> IndexBuffer;
		IndexBuffer Index;

		/// <summary>
		/// Vertex buffer
		/// </summary>
		//ArrayBuffer<float> VertexBuffer;
		ArrayBuffer<float> Buffer;


		//int VertexIndex;

		//int IndicesIndex;

		#endregion

	}
}
