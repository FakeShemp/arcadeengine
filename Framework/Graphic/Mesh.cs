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
namespace ArcEngine.Graphic
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
			Display.DrawIndexBuffer(Buffer, PrimitiveType, Index);
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
		public void AddIndexedTopology(string name, int primitiveType, int[] indices) 
		{
		}


		#region Statics

		/// <summary>
		/// Creates a sphere mesh
		/// </summary>
		/// <param name="radius">Radius</param>
		/// <param name="slices">Number of slices</param>
		/// <returns></returns>
		/// <remarks>From nopper.tv</remarks>
		public static Mesh ggCreateSphere(float radius, int slices)
		{

			#region Vertex
			int numberParallels = slices;
			float[] vertices = new float[(numberParallels + 1) * (slices + 1) * 11];

			float angleStep = (2.0f * (float)Math.PI) / ((float)slices);
			int offset = 0;
			for (int i = 0; i < numberParallels + 1; i++)
			{
				for (int j = 0; j < slices + 1; j++)
				{
					// Vertices
					Vector3 vertex = new Vector3(radius * (float)Math.Sin(angleStep * (float)i) * (float)Math.Sin(angleStep * (float)j),
														  radius * (float)Math.Cos(angleStep * (float)i),
														  radius * (float)Math.Sin(angleStep * (float)i) * (float)Math.Cos(angleStep * (float)j));
					vertices[offset++] = vertex.X;
					vertices[offset++] = vertex.Y;
					vertices[offset++] = vertex.Z;
					
					// Normals
					Vector3 normal = vertex / radius;
					vertices[offset++] = normal.X;
					vertices[offset++] = normal.Y;
					vertices[offset++] = normal.Z;

					// Tangent
					Vector3 tangent = Vector3.Cross(normal, Vector3.UnitY);
					if (tangent.Length == 0.0f)
					{
						vertices[offset++] = 1.0f;
						vertices[offset++] = 0.0f;
						vertices[offset++] = 0.0f;
					}
					else
					{
						vertices[offset++] = tangent.X;
						vertices[offset++] = tangent.Y;
						vertices[offset++] = tangent.Z;
					}

					// Texture
					vertices[offset++] = (float)j / (float)slices;
					vertices[offset++] = (1.0f - (float)i) / (float)(numberParallels - 1);
				}
			}

			#endregion


			#region Index
			int[] indices = new int[numberParallels * slices * 6];
			offset = 0;
			for (int i = 0; i < numberParallels; i++)
			{
				for (int j = 0; j < slices; j++)
				{
					indices[offset++] = i * (slices + 1) + j;
					indices[offset++] = (i + 1) * (slices + 1) + j;
					indices[offset++] = (i + 1) * (slices + 1) + (j + 1);

					indices[offset++] = i * (slices + 1) + j;
					indices[offset++] = (i + 1) * (slices + 1) + (j + 1);
					indices[offset++] = i * (slices + 1) + (j + 1);
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
        /// <param name="innerradius">Inner radius</param>
        /// <param name="outerradius">Outer radius</param>
        /// <param name="slices">Number of slice</param>
        /// <param name="stacks">Number of stack</param>
        /// <returns></returns>
        public static Mesh CreateDisk(float innerradius, float outerradius, int slices, int stacks)
        {

            // How much step for each stack
            float radial = outerradius - innerradius;
            if (radial < 0.0f)
                radial *= -1.0f;
            radial /= (float)stacks;

            float slicesize = ((float)Math.PI * 2.0f) / (float)slices;
            float radialscale = 1.0f / outerradius;


			#region Vertices

			float[] vertices = new float[slices * stacks * 44];


            int offset = 0;
            for (int i = 0; i < stacks; i++)
            {
                float theyta;
                float theytaNext;

                
                for (int j = 0; j < slices; j++)
                {
			        float inner = innerradius + (float)(i) * radial;
			        float outer = innerradius + (float)(i + 1.0f) * radial;
                    float x = 0.0f;
                    float y = 0.0f;

                    theyta = slicesize * (float)j;
                    if (j == (slices - 1))
                        theytaNext = 0.0f;
                    else
                        theytaNext = slicesize * (float)(j + 1.0f);


                    // Inner First
                    x = (float)Math.Cos(theyta) * inner;
                    y = (float)Math.Sin(theyta) * inner;

                    // Vertex
                    vertices[offset++] = x;
                    vertices[offset++] = y;
                    vertices[offset++] = 0.0f;

                    // Normals
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 1.0f;

                    // Tangent
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;

                    // Texture
                    vertices[offset++] = ((x * radialscale) + 1.0f) * 0.5f;
                    vertices[offset++] = ((y * radialscale) + 1.0f) * 0.5f;



                    // Outer First
                    x = (float)Math.Cos(theyta) * outer;
                    y = (float)Math.Sin(theyta) * outer;
                    
                    vertices[offset++] = x;
                    vertices[offset++] = y;
                    vertices[offset++] = 0.0f;

                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 1.0f;

                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;

                    vertices[offset++] = ((x * radialscale) + 1.0f) * 0.5f;
                    vertices[offset++] = ((y * radialscale) + 1.0f) * 0.5f;


                    // Inner Second
                    x = (float)Math.Cos(theytaNext) * inner;
                    y = (float)Math.Sin(theytaNext) * inner;

                    vertices[offset++] = x;
                    vertices[offset++] = y;
                    vertices[offset++] = 0.0f;

                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 1.0f;

                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;

                    vertices[offset++] = ((x * radialscale) + 1.0f) * 0.5f;
                    vertices[offset++] = ((y * radialscale) + 1.0f) * 0.5f;


                    // Outer Second
                    x = (float)Math.Cos(theytaNext) * outer;
                    y = (float)Math.Sin(theytaNext) * outer; 


                    vertices[offset++] = x;
                    vertices[offset++] = y;
                    vertices[offset++] = 0.0f;

                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 1.0f;

                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;
                    vertices[offset++] = 0.0f;

                    vertices[offset++] = ((x * radialscale) + 1.0f) * 0.5f;
                    vertices[offset++] = ((y * radialscale) + 1.0f) * 0.5f;


                }
			}

			#endregion


			#region Indices

			int[] indices = new int[stacks * slices * 6];
			offset = 0;
			for (int i = 0; i < stacks; i++)
			{
				for (int j = 0; j < slices; j++)
				{
					indices[offset++] = i * (slices + 1) + j;
					indices[offset++] = (i + 1) * (slices + 1) + j;
					indices[offset++] = (i + 1) * (slices + 1) + (j + 1);

					indices[offset++] = i * (slices + 1) + j;
					indices[offset++] = (i + 1) * (slices + 1) + (j + 1);
					indices[offset++] = i * (slices + 1) + (j + 1);
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



		/// <summary>
		/// 
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="height"></param>
		/// <param name="slice"></param>
		/// <param name="stacks"></param>
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
		public static Mesh CreateTorus(float inner, float outter, int sides, int faces)
		{
			if (sides < 3 || faces < 3)
				return null;


			int numberVertices = (faces + 1) * (sides + 1);
			int numberIndices = faces * sides * 2 * 3;

			#region Vertices
			
			float[] vertices = new float[numberVertices * 11];
			float tIncr = 1.0f / (float)faces;
			float sIncr = 1.0f / (float)sides;
			float s = 0.0f;
			float t = 0.0f;
			float cos2PIt = 0.0f;
			float sin2PIt = 0.0f;
			float cos2PIs = 0.0f;
			float sin2PIs = 0.0f;
		
			int indexVertices = 0;


			// generate vertices and its attributes
			for (int sideCount = 0; sideCount <= sides; ++sideCount, s += sIncr)
			{
				// precompute some values
				cos2PIs = (float)Math.Cos(2.0f * Math.PI * s);
				sin2PIs = (float)Math.Sin(2.0f * Math.PI * s);

				t = 0.0f;
				for (int faceCount = 0; faceCount <= faces; ++faceCount, t += tIncr)
				{
					// precompute some values
					cos2PIt = (float)Math.Cos(2.0f * Math.PI * t);
					sin2PIt = (float)Math.Sin(2.0f * Math.PI * t);

					// generate vertex and stores it in the right position
					vertices[indexVertices++] = (outter + inner * cos2PIt) * cos2PIs;
					vertices[indexVertices++] = (outter + inner * cos2PIt) * sin2PIs;
					vertices[indexVertices++] = inner * sin2PIt;

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
					if (tangent.Length == 0.0f)
					{
						vertices[indexVertices++] = 1.0f;
						vertices[indexVertices++] = 0.0f;
						vertices[indexVertices++] = 0.0f;
					}
					else
					{
						vertices[indexVertices++] = tangent.X;
						vertices[indexVertices++] = tangent.Y;
						vertices[indexVertices++] = tangent.Z;
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
			for (int sideCount = 0; sideCount < sides; sideCount++)
			{
				for (int faceCount = 0; faceCount < faces; faceCount++)
				{
					// get the number of the vertices for a face of the torus. They must be < numVertices
					int v0 = ((sideCount * (faces + 1)) + faceCount);
					int v1 = (((sideCount + 1) * (faces + 1)) + faceCount);
					int v2 = (((sideCount + 1) * (faces + 1)) + (faceCount + 1));
					int v3 = ((sideCount * (faces + 1)) + (faceCount + 1));
				
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


		/// <summary>
		/// Creates a plane mesh
		/// </summary>
		/// <param name="radius">Radius of the plane</param>
		/// <returns></returns>
		static public Mesh CreatePlane(float radius)
		{
			// Vertices
			float[] vertices = new float[]
			{
				-radius, -radius, 0.0f,		0.0f, 0.0f, 1.0f,			1.0f, 0.0f, 0.0f,				0.0f, 0.0f,
				+radius, -radius, 0.0f,		0.0f, 0.0f, 1.0f,			1.0f, 0.0f, 0.0f,				1.0f, 0.0f,	
				-radius, +radius, 0.0f,		0.0f, 0.0f, 1.0f,			1.0f, 0.0f, 0.0f,				0.0f, 1.0f,	
				+radius, +radius, 0.0f,		0.0f, 0.0f, 1.0f,			1.0f, 0.0f, 0.0f,				1.0f, 1.0f,
			};

			// Indices
			int[] indices = new int[]
			{
				0, 1, 2,
				1, 3, 2,
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
		/// Creates a Trefoil mesh
		/// </summary>
		/// <param name="slices">Number of slices</param>
		/// <param name="stacks">Number of stacks</param>
		/// <returns></returns>
		/// <remarks>http://prideout.net/blog/?p=22</remarks>
		public static Mesh CreateTrefoil(int slices, int stacks)
		{

			#region Vertices

			float ds = 1.0f / slices;
			float dt = 1.0f / stacks;
			int VertexCount = slices * stacks;

			float[] vertices = new float[VertexCount * 11];
			int pos = 0;

			for (float s = 0; s < 1 - ds / 2; s += ds)
			{
				for (float t = 0; t < 1 - dt / 2; t += dt)
				{
					const float E = 0.01f;
					Vector3 p = EvaluateTrefoil(s, t);
					Vector3 u = EvaluateTrefoil(s + E, t) - p;
					Vector3 v = EvaluateTrefoil(s, t + E) - p;
					Vector3 n = Vector3.Normalize(Vector3.Cross(u, v));

					// Position
					vertices[pos++] = p.X;
					vertices[pos++] = p.Y;
					vertices[pos++] = p.Z;

					// Normal
					vertices[pos++] = n.X;
					vertices[pos++] = n.Y;
					vertices[pos++] = n.Z;

					// Tangent
					Vector3 tangent = Vector3.Cross(n, Vector3.UnitY);
					if (tangent.Length == 0.0f)
					{
						vertices[pos++] = 1.0f;
						vertices[pos++] = 0.0f;
						vertices[pos++] = 0.0f;
					}
					else
					{
						vertices[pos++] = tangent.X;
						vertices[pos++] = tangent.Y;
						vertices[pos++] = tangent.Z;
					}

					// Texture			
					vertices[pos++] = 0.0f;
					vertices[pos++] = 0.0f;
				}
			}
			#endregion


			#region Indices

			int[] indices = new int[slices * stacks * 6];
			pos = 0;
			int m = 0;
			for (int i = 0; i < slices; i++)
			{
				for (int j = 0; j < stacks; j++)
				{
					indices[pos++] = m + j;
					indices[pos++] = m + (j + 1) % stacks;
					indices[pos++] = (m + j + stacks) % VertexCount;

					indices[pos++] = (m + j + stacks) % VertexCount;
					indices[pos++] = (m + (j + 1) % stacks) % VertexCount;
					indices[pos++] = (m + (j + 1) % stacks + stacks) % VertexCount;
				}
				m += stacks;
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


		/// <summary>
		/// Evaluate a trefoil
		/// </summary>
		/// <param name="s">S</param>
		/// <param name="t">T</param>
		/// <returns></returns>
		static private Vector3 EvaluateTrefoil(float s, float t)
		{
			float TwoPi = (float)Math.PI * 2.0f;
			float a = 0.5f;
			float b = 0.3f;
			float c = 0.5f;
			float d = 0.1f;
			float u = (1.0f - s) * 2.0f * TwoPi;
			float v = t * TwoPi;
			float r = a + b * (float)Math.Cos(1.5f * u);
			float x = r * (float)Math.Cos(u);
			float y = r * (float)Math.Sin(u);
			float z = c * (float)Math.Sin(1.5f * u);

			Vector3 dv;
			dv.X = -1.5f * b * (float)Math.Sin(1.5f * u) * (float)Math.Cos(u) - (a + b * (float)Math.Cos(1.5f * u)) * (float)Math.Sin(u);
			dv.Y = -1.5f * b * (float)Math.Sin(1.5f * u) * (float)Math.Sin(u) + (a + b * (float)Math.Cos(1.5f * u)) * (float)Math.Cos(u);
			dv.Z = 1.5f * c * (float)Math.Cos(1.5f * u);

			Vector3 q = dv;
			q.Normalize();
			Vector3 qvn = Vector3.Normalize(new Vector3(q.Y, -q.X, 0));
			Vector3 ww = Vector3.Cross(q, qvn);


			Vector3 range;
			range.X = x + d * (qvn.X * (float)Math.Cos(v) + ww.X * (float)Math.Sin(v));
			range.Y = y + d * (qvn.Y * (float)Math.Cos(v) + ww.Y * (float)Math.Sin(v));
			range.Z = z + d * ww.Z * (float)Math.Sin(v);
			return range;
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
