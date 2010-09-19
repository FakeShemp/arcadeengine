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
	/// MD5 mesh
	/// </summary>
	public class SubMesh : IDisposable
	{

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			if (Mesh != null)
				Mesh.Dispose();
			Mesh = null;
		}


		/// <summary>
		/// 
		/// </summary>
		public void Draw()
		{
			Mesh.Draw();
		}


		/// <summary>
		/// Prepare the mesh for drawing
		/// </summary>
		internal void Prepare(MD5Mesh mesh)
		{

			ComputeWeightNormals(mesh);
			ComputeWeightTangents(mesh);

			// Allocate buffers :
			// 3 vertices
			// 3 normals
			// 3 tangents
			// 2 texture coords
			float[] buffer = new float[Vertices.Length * 11];

			// Process vertices
			for (int i = 0 ; i< Vertices.Length ; i++)
			{
				Vector3 vertex = Vector3.Zero;
				Vector3 normal = Vector3.Zero;
				Vector3 tangent = Vector3.Zero;

				// Calculate final vertex to draw with weights
				for (int j = 0 ; j < Vertices[i].WeightCount ; j++)
				{
					Weight weight = Weights[Vertices[i].StartWeight + j];
					Joint joint = mesh.GetJoint(weight.Joint);

					// Calculate transformed vertex for this weight
					Vector3 wv = Quaternion.RotatePoint(ref joint.Orientation, ref weight.Position);
					vertex += (joint.Position + wv) * weight.Bias;

					// Calculate transformed normal for this weight
					Vector3 wn = Quaternion.RotatePoint(ref joint.Orientation, ref weight.Normal);
					normal += wn * weight.Bias;

					// Calculate transformed tangent for this weight
					Vector3 wt = Quaternion.RotatePoint(ref joint.Orientation, ref weight.Tangent);
					tangent += wt *weight.Bias;
				}


				buffer[i * 11 +  0] = vertex.X;
				buffer[i * 11 +  1] = vertex.Y;
				buffer[i * 11 +  2] = vertex.Z;
						   
				buffer[i * 11 +  3] = normal.X;
				buffer[i * 11 +  4] = normal.Y;
				buffer[i * 11 +  5] = normal.Z;
						   
				buffer[i * 11 +  6] = tangent.X;
				buffer[i * 11 +  7] = tangent.Y;
				buffer[i * 11 +  8] = tangent.Z;

				buffer[i * 11 +  9] = Vertices[i].Texture.X;
				buffer[i * 11 + 10] = Vertices[i].Texture.Y;
			}


			int[] indices = new int[Triangles.Length * 3];
			for (int i = 0 ; i < Triangles.Length ; i++)
			{
				indices[i * 3 + 0] = Triangles[i].A;
				indices[i * 3 + 1] = Triangles[i].B;
				indices[i * 3 + 2] = Triangles[i].C;
			}

			Mesh = new Graphic.Mesh();
			Mesh.Buffer.AddDeclaration("in_position", 3);
			Mesh.Buffer.AddDeclaration("in_normal", 3);
			Mesh.Buffer.AddDeclaration("in_tangent", 3);
			Mesh.Buffer.AddDeclaration("in_texcoord", 2);
			Mesh.SetVertices(buffer);
			Mesh.SetIndices(indices);
		}


		/// <summary>
		/// Compute weight normals
		/// </summary>
		void ComputeWeightNormals(MD5Mesh mesh)
		{
			Vector3[] bindposeVertex = new Vector3[Vertices.Length];
			Vector3[] bindposeNorms = new Vector3[Vertices.Length];

			for (int i = 0; i < Vertices.Length; i++)
			{
				for (int j = 0 ; j < Vertices[i].WeightCount ; j++)
				{
					Weight weight = Weights[Vertices[i].StartWeight + j];
					Joint joint = mesh.GetJoint(weight.Joint);

					// Calculate transformed vertex for this weight
					Vector3 wv = Quaternion.RotatePoint(ref joint.Orientation, ref weight.Position);

					bindposeVertex[i] += (joint.Position + wv) * weight.Bias;
				}
			}

			// Compute triangle normals
			for (int i = 0 ; i < Triangles.Length ; i++)
			{
				Triangle triangle = Triangles[i];
				Vector3 normal = new Vector3(-ComputeNormal(
					bindposeVertex[triangle.A], 
					bindposeVertex[triangle.B], 
					bindposeVertex[triangle.C]));

				bindposeNorms[triangle.A] += normal;
				bindposeNorms[triangle.B] += normal;
				bindposeNorms[triangle.C] += normal;
			}

			// Average the surface normals
			for (int i = 0 ; i < Vertices.Length ; i++)
				bindposeNorms[i].Normalize();


			// Compute weight normals by invert-transforming the normal by the bone-space matrix
			for (int i = 0 ; i < Vertices.Length ; i++)
			{
				for (int j = 0 ; j < Vertices[i].WeightCount ; j++)
				{
					Weight weight = Weights[Vertices[i].StartWeight + j];
					Joint joint = mesh.GetJoint(weight.Joint);

					Quaternion invRot = Quaternion.Invert(joint.Orientation);
					Vector3 wn = Quaternion.RotatePoint(ref invRot, ref bindposeNorms[i]);

					weight.Normal += wn;
				}
			}

			// Average all weight normals
			for (int i = 0 ; i < Weights.Length ; i++)
				Weights[i].Normal.Normalize();

		}


		/// <summary>
		/// Compute vertex tangent and weight tangent
		/// </summary>
		void ComputeWeightTangents(MD5Mesh mesh)
		{
			// Final vertex, normal and tangent
			Vector3[] bindposeVerts = new Vector3[Vertices.Length];
			Vector3[] bindposeNorms = new Vector3[Vertices.Length];
			Vector3[] bindposeTans = new Vector3[Vertices.Length];

			// s-tangents and t-tangents
			Vector3[] sTan = new Vector3[Vertices.Length];
			Vector3[] tTan = new Vector3[Vertices.Length];


			// Compute bind-pose vertices and normals
			for (int i = 0 ; i < Vertices.Length ; i++)
			{
				for (int j = 0 ; j < Vertices[i].WeightCount ; j++)
				{
					Weight weight = Weights[Vertices[i].StartWeight + j];
					Joint joint = mesh.GetJoint(weight.Joint);

					// Calculate transformed vertex for this weight
					Vector3 wv = Quaternion.RotatePoint(ref joint.Orientation, ref weight.Position);
					bindposeVerts[i] += (joint.Position + wv) *weight.Bias;

					// Calculate transformed normal for this weight
					Vector3 wn = Quaternion.RotatePoint(ref joint.Orientation, ref weight.Normal);
					bindposeNorms[i] += wn * weight.Bias;
				}
			}


			// Calculate s-tangent and t-tangent at triangle level
			for (int i = 0 ; i < Triangles.Length ; i++)
			{
				Triangle triangle = Triangles[i];

				Vector3 v0 = bindposeVerts[triangle.A];
				Vector3 v1 = bindposeVerts[triangle.B];
				Vector3 v2 = bindposeVerts[triangle.C];

				Vector2 w0 = Vertices[triangle.A].Texture;
				Vector2 w1 = Vertices[triangle.B].Texture;
				Vector2 w2 = Vertices[triangle.C].Texture;

				float x1 = v1.X - v0.X;
				float x2 = v2.X - v0.X;
				float y1 = v1.Y - v0.Y;
				float y2 = v2.Y - v0.Y;
				float z1 = v1.Z - v0.Z;
				float z2 = v2.Z - v0.Z;

				float s1 = w1.X - w0.X;
				float s2 = w2.X - w0.X;
				float t1 = w1.Y - w0.Y;
				float t2 = w2.Y - w0.Y;

				float r = (s1 * t2) - (s2 * t1);
				if (r == 0.0f)
					r = 1.0f;

				float oneOverR = 1.0f / r;

				Vector3 sDir = new Vector3(
					(t2 * x1 - t1 * x2) * oneOverR,
					(t2 * y1 - t1 * y2) * oneOverR,
					(t2 * z1 - t1 * z2) * oneOverR);
				Vector3 tDir = new Vector3(
					(s1 * x2 - s2 * x1) * oneOverR,
					(s1 * y2 - s2 * y1) * oneOverR,
					(s1 * z2 - s2 * z1) * oneOverR);

				sTan[triangle.A] += sDir;
				sTan[triangle.A] += tDir;
				sTan[triangle.B] += sDir;
				sTan[triangle.B] += tDir;
				sTan[triangle.C] += sDir;
				sTan[triangle.C] += tDir;
			}


			// Calculate vertex tangent
			for (int i = 0 ; i < Vertices.Length ; i++)
			{
				Vector3 n = bindposeNorms[i];
				Vector3 t = sTan[i];

				// Gram-Schmidt orthogonalize
				bindposeTans[i] = (t - n * Vector3.Dot(n, t));
				bindposeTans[i].Normalize();

				// Calculate handedness
				if (Vector3.Dot(Vector3.Cross(n, t), tTan[i]) < 0.0f)
					bindposeTans[i] = -bindposeTans[i];

				// Compute weight tangent
				for (int j = 0 ; j < Vertices[i].WeightCount ; j++)
				{
					Weight weight = Weights[Vertices[i].StartWeight + j];
					Joint joint = mesh.GetJoint(weight.Joint);

					// Compute inverse quaternion rotation
					Quaternion invrot = Quaternion.Invert(joint.Orientation);
					Vector3 wt = Quaternion.RotatePoint(ref invrot, ref bindposeTans[i]);

					weight.Tangent += wt;
				}
			}

			for (int i = 0 ; i < Weights.Length ; i++)
				Weights[i].Tangent.Normalize();
		}


		/// <summary>
		/// Gets the normal of a triangle
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <param name="p3"></param>
		/// <returns></returns>
		Vector3 ComputeNormal(Vector3 p1, Vector3 p2, Vector3 p3)
		{
			Vector3 vec1 = new Vector3(p1 - p2);
			Vector3 vec2 = new Vector3(p1 - p3);
			Vector3 result = Vector3.Cross(vec1, vec2);
			result.Normalize();

			return result;
		}


		#region Properties

		/// <summary>
		/// Shader
		/// </summary>
		public string Shader;


		/// <summary>
		/// Vertices
		/// </summary>
		public Vertex[] Vertices;


		/// <summary>
		/// Triangles
		/// </summary>
		public Triangle[] Triangles;


		/// <summary>
		/// Weights
		/// </summary>
		public Weight[] Weights;


		/// <summary>
		/// Mesh
		/// </summary>
		ArcEngine.Graphic.Mesh Mesh;

		#endregion
	}
}
