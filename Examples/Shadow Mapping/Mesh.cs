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
using ArcEngine.Asset;
using OpenTK;
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
namespace ArcEngine.Examples.ShadowMapping
{
	/// <summary>
	/// Class which holds the geometry of a 3d object.  
	/// </summary>
	public class Mesh : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Mesh()
		{
			Index = new IndexBuffer();
			Buffer = new BatchBuffer();

			Position = new Vector3();
			Rotation = new Vector3();
		}



		/// <summary>
		/// Update vertices
		/// </summary>
		/// <param name="data"></param>
		public void SetVertices(float[] data)
		{
			Buffer.SetVertices(data);
		}


		/// <summary>
		/// Update indices
		/// </summary>
		/// <param name="data"></param>
		public void SetIndices(int[] data)
		{
			Index.Update(data);
		}



		/// <summary>
		/// Draws the mesh
		/// </summary>
		public void Draw(Shader shader)
		{
			//Display.PushMatrix(MatrixMode.Modelview);
			//Rotation.Normalize();
		//	Display.ModelViewMatrix = Matrix4.CreateFromAxisAngle(Rotation, 1.0f) * Matrix4.CreateTranslation(Position) * Display.ModelViewMatrix;
		
			Display.DrawIndexBuffer(shader, Buffer, PrimitiveType.Triangles, Index);
			
		//	Display.PopMatrix(MatrixMode.Modelview);
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Index != null)
				Index.Dispose();
			Index = null;

			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;
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
		/// Creates a cube mesh
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Mesh MakeSolidCube(float size)
		{
			int[] indices = new int[]
			  {
					0, 1, 2,		 // face 1
					2, 1, 3,
					2, 3, 4,		 // face 2
					4, 3, 5,
					4, 5, 6,		 // face 3
					6, 5, 7,
					6, 7, 0,		 // face 4
					0, 7, 1,
					1, 7, 3,		 // face 5
					3, 7, 5,
					6, 0, 4,		 // face 6
					4, 0, 2,
			  };

			float[] vertices = new float[]
				{
				 -size, -size,  size,								  // vertex 0
				  size, -size,  size,								  // vertex 1
				 -size,  size,  size,								  // vertex 2
				  size,  size,  size,								  // vertex 3
				 -size,  size, -size,								  // vertex 4
				  size,  size, -size,								  // vertex 5
				 -size, -size, -size,								  // vertex 6
				  size, -size, -size,								  // vertex 7
				};

			Mesh mesh = new Mesh();
			mesh.Buffer.AddDeclaration("in_position", 3);
			
			mesh.SetVertices(vertices);
			mesh.SetIndices(indices);


	
			
			return mesh;
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
		public IndexBuffer Index
		{
			get;
			private set;
		}

		/// <summary>
		/// Vertex buffer
		/// </summary>
		public BatchBuffer Buffer
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public Vector3 Position;


		/// <summary>
		/// 
		/// </summary>
		public Vector3 Rotation;


		#endregion

	}
}
