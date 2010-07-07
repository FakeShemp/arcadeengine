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
using System.Collections.Generic;
using ArcEngine.Graphic;
using System.IO;

namespace ArcEngine.Examples.ShadowMapping
{
	/// <summary>
	/// Ply file loader
	/// </summary>
	static public class PlyLoader
	{

		/// <summary>
		/// Loads a ply file
		/// </summary>
		/// <param name="filename">file name</param>
		/// <returns>Mesh handle or null</returns>
		public static Mesh LoadPly(string filename)
		{
			if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
				return null;

			// Generate the mesh
			Mesh mesh = new Mesh();
			mesh.Buffer.AddDeclaration("in_position", 3);

			
			int vcount = 0;
			int fcount = 0;
			using (StreamReader reader = new StreamReader(filename))
			{
				while(reader.Peek() >= 0)
				{
					string line = reader.ReadLine();

					if (line.StartsWith("element vertex"))
					{
						vcount = int.Parse(line.Substring("element vertex".Length));
					}
					else if (line.StartsWith("element face"))
					{
						fcount = int.Parse(line.Substring("element face".Length));
					}
					else if (line.StartsWith("end_header"))
					{
						LoadVertices(mesh, reader, vcount);
						LoadFaces(mesh, reader, fcount);
					}
				}



			}

			return mesh;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="mesh"></param>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		static void LoadVertices(Mesh mesh, StreamReader reader, int count)
		{
			if (mesh == null || reader == null || count <= 0)
				return;

			float[] vert = new float[count * 3];
			for (int i = 0; i < count; i++)
			{
				string[] line = reader.ReadLine().Replace('.', ',').Trim().Split(' ');
				vert[i * 3] = float.Parse(line[0]);
				vert[i * 3 + 1] = float.Parse(line[1]);
				vert[i * 3 + 2] = float.Parse(line[2]);
			}

			mesh.SetVertices(vert);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="mesh"></param>
		/// <param name="reader"></param>
		/// <param name="count"></param>
		static void LoadFaces(Mesh mesh, StreamReader reader, int count)
		{
			if (mesh == null || reader == null || count <= 0)
				return;

			int[] faces = new int[count * 3];
			for (int i = 0; i < count; i++)
			{
				string[] line = reader.ReadLine().Trim().Split(' ');
				faces[i * 3] = int.Parse(line[1]);
				faces[i * 3 + 1] = int.Parse(line[2]);
				faces[i * 3 + 2] = int.Parse(line[3]);
			}

			mesh.SetIndices(faces);

		}



	}
}
