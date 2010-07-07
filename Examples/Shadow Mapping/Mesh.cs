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
using ArcEngine.Asset;
using ArcEngine.Graphic;
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
			PrimitiveType = PrimitiveType.Triangles;
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
		public void Draw()
		{
			//Display.PushMatrix(MatrixMode.Modelview);
			//Rotation.Normalize();
		//	Display.ModelViewMatrix = Matrix4.CreateFromAxisAngle(Rotation, 1.0f) * Matrix4.CreateTranslation(Position) * Display.ModelViewMatrix;

			Display.DrawIndexBuffer(Buffer, PrimitiveType, Index);
			
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
		public static Mesh CreateSphere(float radius, int slices, int stacks)
		{
			return null;
		}


		/// <summary>
		/// Creates a cube mesh
		/// </summary>
		/// <param name="size">Size of a side</param>
		/// <returns></returns>
		public static Mesh CreateCube(float size)
		{
			// Indices
			int[] indices = new int[]
			{
				 0, 2, 1,
				 0, 3, 2, 
				 4, 5, 6,
				 4, 6, 7,
				 8, 9, 10,
				 8, 10, 11, 
				 12, 15, 14,
				 12, 14, 13, 
				 16, 17, 18,
				 16, 18, 19, 
				 20, 23, 22,
				 20, 22, 21,
			};


			float[] vertices = new float[]
			{
				//Vertex							Normal							Tangent							Texture
				-size, -size, -size,			0.0f, -1.0f,  0.0f,		  -1.0f,  0.0f,  0.0f,			0.0f, 0.0f,
				-size, -size, +size,			0.0f, -1.0f,  0.0f,		  -1.0f,  0.0f,  0.0f,			0.0f, 1.0f,
				+size, -size, +size,			0.0f, -1.0f,  0.0f,		  -1.0f,  0.0f,  0.0f,			1.0f, 1.0f,
				+size, -size, -size,			0.0f, -1.0f,  0.0f,		  -1.0f,  0.0f,  0.0f,			1.0f, 0.0f,
																										 		
				-size, +size, -size,			0.0f, +1.0f,  0.0f,		  +1.0f,  0.0f,  0.0f,			1.0f, 0.0f,
				-size, +size, +size,			0.0f, +1.0f,  0.0f,		  +1.0f,  0.0f,  0.0f,			1.0f, 1.0f,
				+size, +size, +size,			0.0f, +1.0f,  0.0f,		  +1.0f,  0.0f,  0.0f,			0.0f, 1.0f,
				+size, +size, -size,			0.0f, +1.0f,  0.0f,		  +1.0f,  0.0f,  0.0f,			0.0f, 0.0f,
																										 			
				-size, -size, -size,			0.0f,  0.0f, -1.0f,		  -1.0f,  0.0f,  0.0f,			0.0f, 0.0f,
				-size, +size, -size,			0.0f,  0.0f, -1.0f,		  -1.0f,  0.0f,  0.0f,			0.0f, 1.0f,
				+size, +size, -size,			0.0f,  0.0f, -1.0f,		  -1.0f,  0.0f,  0.0f,			1.0f, 1.0f,
				+size, -size, -size,			0.0f,  0.0f, -1.0f,		  -1.0f,  0.0f,  0.0f,			1.0f, 0.0f,
																										 		
				-size, -size, +size,			0.0f,  0.0f, +1.0f,		  +1.0f,  0.0f,  0.0f,			0.0f, 0.0f,
				-size, +size, +size,			0.0f,  0.0f, +1.0f,		  +1.0f,  0.0f,  0.0f,			0.0f, 1.0f,
				+size, +size, +size,			0.0f,  0.0f, +1.0f,		  +1.0f,  0.0f,  0.0f,			1.0f, 1.0f,
				+size, -size, +size,			0.0f,  0.0f, +1.0f,		  +1.0f,  0.0f,  0.0f,			1.0f, 0.0f,
			 																							 		
				-size, -size, -size,			-1.0f,  0.0f,  0.0f,			0.0f,  0.0f, +1.0f,			0.0f, 0.0f,
				-size, -size, +size,			-1.0f,  0.0f,  0.0f,			0.0f,  0.0f, +1.0f,			0.0f, 1.0f,
				-size, +size, +size,			-1.0f,  0.0f,  0.0f,			0.0f,  0.0f, +1.0f,			1.0f, 1.0f,
				-size, +size, -size,			-1.0f,  0.0f,  0.0f,			0.0f,  0.0f, +1.0f,			1.0f, 0.0f,
																										 			
				+size, -size, -size,			+1.0f,  0.0f,  0.0f,			0.0f,  0.0f, -1.0f,			0.0f, 0.0f,
				+size, -size, +size,			+1.0f,  0.0f,  0.0f,			0.0f,  0.0f, -1.0f,			0.0f, 1.0f,
				+size, +size, +size,			+1.0f,  0.0f,  0.0f,			0.0f,  0.0f, -1.0f,			1.0f, 1.0f,
				+size, +size, -size,			+1.0f,  0.0f,  0.0f, 		0.0f,  0.0f, -1.0f,			1.0f, 0.0f,
			};


			Mesh mesh = new Mesh();
			mesh.Buffer.AddDeclaration("in_position", 3);
			mesh.Buffer.AddDeclaration("in_normal", 3);
			mesh.Buffer.AddDeclaration("in_tangent", 3);
			mesh.Buffer.AddDeclaration("in_texcoord", 2);
			
			mesh.SetVertices(vertices);
			mesh.SetIndices(indices);
			mesh.PrimitiveType = PrimitiveType.Triangles;
	
			
			return mesh;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Mesh CreateCone(float radius, float height, int slice, int stacks)
		{
			return null;
		}


		/// <summary>
		/// Creates a torus mesh
		/// </summary>
		/// <param name="inner">Inner radius</param>
		/// <param name="outter">Outter radius</param>
		/// <param name="sides">Number of sides</param>
		/// <param name="faces">Number of faces</param>
		/// <returns></returns>
		/// <remarks>@author Pablo Alonso-Villaverde Roza (www.nopper.tv)</remarks>
		public static Mesh CreateTorus(float innerRadius, float outerRadius, int numSides, int numFaces)
		{
			if (numSides < 3 || numFaces < 3)
				return null;


			int numberVertices = (numFaces + 1) * (numSides + 1);
			int numberIndices = numFaces * numSides * 2 * 3;

			#region Vertices
			
			float[] vertices = new float[numberVertices * 11];
			float tIncr = 1.0f / (float)numFaces;
			float sIncr = 1.0f / (float)numSides;
			float s = 0.0f;
			float t = 0.0f;
			float cos2PIt = 0.0f;
			float sin2PIt = 0.0f;
			float cos2PIs = 0.0f;
			float sin2PIs = 0.0f;
		
			int indexVertices = 0;


			// generate vertices and its attributes
			for (int sideCount = 0; sideCount <= numSides; ++sideCount, s += sIncr)
			{
				// precompute some values
				cos2PIs = (float)Math.Cos(2.0f * Math.PI * s);
				sin2PIs = (float)Math.Sin(2.0f * Math.PI * s);

				t = 0.0f;
				for (int faceCount = 0; faceCount <= numFaces; ++faceCount, t += tIncr)
				{
					// precompute some values
					cos2PIt = (float)Math.Cos(2.0f * Math.PI * t);
					sin2PIt = (float)Math.Sin(2.0f * Math.PI * t);

					// generate vertex and stores it in the right position
					vertices[indexVertices++] = (outerRadius + innerRadius * cos2PIt) * cos2PIs;
					vertices[indexVertices++] = (outerRadius + innerRadius * cos2PIt) * sin2PIs;
					vertices[indexVertices++] = innerRadius * sin2PIt;

					// generate normal and stores it in the right position
					// NOTE: cos (2PIx) = cos (x) and sin (2PIx) = sin (x) so, we can use this formula
					//       normal = {cos(2PIs)cos(2PIt) , sin(2PIs)cos(2PIt) ,sin(2PIt)}    
					Vector3 normal = new Vector3(cos2PIs * cos2PIt, sin2PIs * cos2PIt, sin2PIt);
					vertices[indexVertices++] = normal.X;
					vertices[indexVertices++] = normal.Y;
					vertices[indexVertices++] = normal.Z;

					// tangent vector can be calculated with a cross product between the helper vector, and the normal vector
					// We must take care if both the normal and helper are parallel (cross product = 0, that's not a valid tangent!)			
					Vector3 tangent = Vector3.Cross(normal, Vector3.UnitY);
					vertices[indexVertices++] = tangent.X;
					vertices[indexVertices++] = tangent.Y;
					vertices[indexVertices++] = tangent.Z;

					if (tangent.Length == 0.0f)
					{
						vertices[indexVertices++] = 1.0f;
						vertices[indexVertices++] = 0.0f;
						vertices[indexVertices++] = 0.0f;
					}

					// generate texture coordinates and stores it in the right position
					vertices[indexVertices++] = t;
					vertices[indexVertices++] = s;
				}
			}

			#endregion


			#region Indices
			int[] indices = new int[numberIndices];
			int indexIndices = 0;
			for (int sideCount = 0; sideCount < numSides; sideCount++)
			{
				for (int faceCount = 0; faceCount < numFaces; faceCount++)
				{
					// get the number of the vertices for a face of the torus. They must be < numVertices
					int v0 = ((sideCount * (numFaces + 1)) + faceCount);
					int v1 = (((sideCount + 1) * (numFaces + 1)) + faceCount);
					int v2 = (((sideCount + 1) * (numFaces + 1)) + (faceCount + 1));
					int v3 = ((sideCount * (numFaces + 1)) + (faceCount + 1));
				
					// first triangle of the face, counter clock wise winding		
					indices[indexIndices++] = v0;
					indices[indexIndices++] = v1;
					indices[indexIndices++] = v2;

					// second triangle of the face, counter clock wise winding
					indices[indexIndices++] = v0;
					indices[indexIndices++] = v2;
					indices[indexIndices++] = v3;
				}
			}
			#endregion


			Mesh mesh = new Mesh();
			mesh.Buffer.AddDeclaration("in_position", 3);
			mesh.Buffer.AddDeclaration("in_normal", 3);
			mesh.Buffer.AddDeclaration("in_tangent", 3);
			mesh.Buffer.AddDeclaration("in_texcoord", 2);

			mesh.SetVertices(vertices);
			mesh.SetIndices(indices);
			mesh.PrimitiveType = PrimitiveType.Triangles;

			return mesh;
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


		/// <summary>
		/// 
		/// </summary>
		public PrimitiveType PrimitiveType;

		#endregion

	}
}
