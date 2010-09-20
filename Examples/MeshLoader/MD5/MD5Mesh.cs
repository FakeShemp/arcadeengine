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
using System.IO;
using ArcEngine.Graphic;


//
// http://tfc.duke.free.fr/coding/md5-specs-fr.html
//
namespace ArcEngine.Examples.MeshLoader.MD5
{
	/// <summary>
	/// MD5 mesh
	/// </summary>
	public class MD5Mesh : IDisposable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MD5Mesh()
		{
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			foreach (SubMesh mesh in Meshes)
				mesh.Dispose();
			Meshes = null;
		}


		/// <summary>
		/// Loads a md5mesh file
		/// </summary>
		/// <param name="filename">File name to load</param>
		/// <returns>True if successful</returns>
		public bool Load(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return false;

			if (!File.Exists(filename))
				return false;

			int numjoints = 0;
			int nummeshes = 0;

			using (StreamReader stream = new StreamReader(filename))
			{
				string line = null;
				while ((line = stream.ReadLine()) != null)
				{
					if (line.StartsWith("MD5Version"))
					{
						if (Version != int.Parse(line.Substring("MD5Version".Length)))
						{
							Trace.WriteLine("Unsuported MD5Mesh found !");
							return false;
						}
					}
					else if (line.StartsWith("numJoints"))
					{
						numjoints = int.Parse(line.Substring("numJoints".Length));
						Joints = new Joint[numjoints];
					}
					else if (line.StartsWith("numMeshes"))
					{
						nummeshes = int.Parse(line.Substring("numMeshes".Length));
						Meshes = new List<SubMesh>(nummeshes);
					}
					else if (line.StartsWith("joints"))
					{
						ReadJoints(stream);
					}
					else if (line.StartsWith("mesh"))
					{
						SubMesh mesh = new SubMesh();
						mesh.ReadMesh(stream);
						Meshes.Add(mesh);
					}
				}
			}

			Prepare();

			return true;
		}


		/// <summary>
		/// Draw the mesh
		/// </summary>
		public void Draw()
		{
			foreach (SubMesh sub in Meshes)
				sub.Draw();
		}


		/// <summary>
		/// Prepare the MD5Mesh for rendering
		/// </summary>
		void Prepare()
		{
			// Prepare each mesh
			foreach (SubMesh mesh in Meshes)
				mesh.Prepare(this);


			#region Shader

			Shader = new Shader();
			Shader.LoadSource(ShaderType.VertexShader, "data/md5/shaders/shader.vert");
			Shader.LoadSource(ShaderType.FragmentShader, "data/md5/shaders/shader.frag");
			Shader.Compile();

			#endregion
		}


		/// <summary>
		/// Reads joints definition
		/// </summary>
		/// <param name="stream">Stream to the file</param>
		void ReadJoints(StreamReader stream)
		{
			if (stream == null)
				return;

			// Allow to read float with '.' instead od ','
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");

			string line = null;
			for (int i = 0 ; i < JointCount ; i++)
			{
				line = stream.ReadLine();
				line = line.Trim();
				string[] lines = line.Split(new char[]{' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

				// End of joints list
				if (lines.Length == 1)
					break;

				string name = lines[0];
				int parentid = int.Parse(lines[1]);
				Vector3 position = new Vector3(float.Parse(lines[3], ci), float.Parse(lines[4], ci), float.Parse(lines[5], ci));
				Vector3 quaternion = new Vector3(float.Parse(lines[8], ci), float.Parse(lines[9], ci), float.Parse(lines[10], ci));

				// Add the joint to the list
				Joints[i] = new Joint(name, parentid, position, quaternion);
			}
		}



		/// <summary>
		/// Get a joint
		/// </summary>
		/// <param name="id">Id of the joint in the list</param>
		/// <returns></returns>
		public Joint GetJoint(int id)
		{
			if (id > JointCount)
				throw new ArgumentOutOfRangeException("id");

			return Joints[id];
		}


		#region Properties

		/// <summary>
		/// Joints
		/// </summary>
		Joint[] Joints;


		/// <summary>
		/// Joints
		/// </summary>
		List<SubMesh> Meshes;


		/// <summary>
		/// MD5 mesh version
		/// </summary>
		static public int Version
		{
			get
			{
				return 10;
			}
		}


		/// <summary>
		/// Number of joints
		/// </summary>
		public int JointCount
		{
			get
			{
				if (Joints == null)
					return -1;

				return Joints.Length;
			}
		}


		/// <summary>
		/// Number of mesh
		/// </summary>
		public int MeshCount
		{
			get
			{
				if (Meshes == null)
					return -1;

				return Meshes.Count;
			}
		}


		/// <summary>
		/// Shader
		/// </summary>
		public Shader Shader
		{
			get;
			private set;
		}

		#endregion
	}
}
