using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ArcEngine.Examples.MeshLoader.MD5
{
	/// <summary>
	/// 
	/// </summary>
	public class MD5Mesh
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MD5Mesh()
		{
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
						Joints = new List<Joint>(numjoints);
					}
					else if (line.StartsWith("numMeshes"))
					{
						nummeshes = int.Parse(line.Substring("numMeshes".Length));
						Meshes = new List<MD5Mesh>(nummeshes);
					}
					else if (line.StartsWith("joints"))
					{
						ReadJoints(stream);
					}
					else if (line.StartsWith("mesh"))
					{
						ReadMesh(stream);
					}
				}
			}


			return true;
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
			while ((line = stream.ReadLine()) != null)
			{
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
				Joints.Add(new Joint(name, parentid, position, quaternion));
			}
		}


		/// <summary>
		/// Reads a mesh definition
		/// </summary>
		/// <param name="stream">Stream to the file</param>
		void ReadMesh(StreamReader stream)
		{
			if (stream == null)
				return;


			// Allow to read float with '.' instead od ','
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");

			string line = null;
			while ((line = stream.ReadLine()) != null)
			{
				line = line.Trim();
				string[] lines = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

				// End of joints list
				if (lines.Length == 1)
					break;

				string name = lines[0];
				int parentid = int.Parse(lines[1]);
				Vector3 position = new Vector3(float.Parse(lines[3], ci), float.Parse(lines[4], ci), float.Parse(lines[5], ci));
				Vector3 quaternion = new Vector3(float.Parse(lines[8], ci), float.Parse(lines[9], ci), float.Parse(lines[10], ci));

				// Add the joint to the list
				Joints.Add(new Joint(name, parentid, position, quaternion));
			}

		}

		#region Properties

		/// <summary>
		/// Joints
		/// </summary>
		List<Joint> Joints;


		/// <summary>
		/// Joints
		/// </summary>
		List<MD5Mesh> Meshes;


		/// <summary>
		/// MD5 mesh version
		/// </summary>
		public int Version
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

				return Joints.Count;
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



		#endregion
	}
}
