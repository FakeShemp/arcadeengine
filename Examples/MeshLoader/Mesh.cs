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
using ArcEngine.Graphic;
using ArcEngine.Asset;

/*
 * 
 * http://old.nabble.com/OpenGL%27s-VBO-with-Haskell-td19148385.html
 * 
 * http://learningwebgl.com/cookbook/index.php/How_to_draw_a_sphere 
 * 
 * http://www.scenejs.org/docs/symbols/src/_home_lindsay_xeolabs_projects_scenejs_src_scenejs_loadCollada_loadCollada.js.html
 * 
 * http://www.collada.org/public_forum/viewtopic.php?f=12&t=1568&p=5394&hilit=opengl#p5394
 * 
 * 
 * 
 * 
 * 
 * 
 * meshes are subdivided in  subsets , each corresponding to a portion of the mesh characterized by the same material. 
*/
namespace ArcEngine.Examples.MeshLoader
{
	/// <summary>
	/// Class which holds the geometry of a 3d object.  
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

			//Index = new int[]();
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
		public void SetIndices(int[] data)
		{
			//Index.Update(data);
			Index = data;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="shader">Shader to use</param>
		public void Draw(Shader shader)
		{

			if (shader == null)
				return;

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

			//VertexBuffer.Dispose();
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <param name="normalized"></param>
		public void AddVertexAttribute(string name, int id, float[] normalized)
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="primitiveType"></param>
		/// <param name="indices"></param>
		public void AddIndexedTopology(string name, int primitiveType, uint[] indices) 
		{
		}

		#region Statics

		/// <summary>
		/// 
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="slices"></param>
		/// <param name="stacks"></param>
		/// <returns></returns>
		public static Mesh MakeWireSphere(float radius, int slices, int stacks)
		{
			return null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="slices"></param>
		/// <param name="stacks"></param>
		/// <returns></returns>
		public static Mesh MakeSolidSphere(float radius, int slices, int stacks)
		{
			return null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Mesh MakeWireCube(float size)
		{
			return null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Mesh MakeSolidCube(float size)
		{
			return null;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Mesh MakeWireCone(float radius, float height, int slice, int stacks)
		{
			return null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Mesh MakeSolidCone(float radius, float height, int slice, int stacks)
		{
			return null;
		}



		#endregion



		#region Properties

		/// <summary>
		/// Index buffer
		/// </summary>
		//ArrayBuffer<uint> IndexBuffer;
		int[] Index;

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
