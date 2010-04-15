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
using System.Runtime.InteropServices;
using System.Xml;



namespace ArcEngine.Examples
{
	/// <summary>
	/// Collada file loader
	/// </summary>
	class ColladaLoader
	{
		/// <summary>
		/// 
		/// </summary>
		public ColladaLoader()
		{

			Geometries = new Dictionary<string, Geometry>();
		}


		/// <summary>
		/// Loads a COLLADA file
		/// </summary>
		/// <param name="name">Name of the file</param>
		/// <returns>True on success</returns>
		public bool Load(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;

			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(name);
				if (doc.DocumentElement.Name != "COLLADA")
				{
					Trace.WriteLine("Not a COLLADA file !");
					return false;
				}

				if (doc.DocumentElement.Attributes["version"] == null || doc.DocumentElement.Attributes["version"].Value != "1.4.0")
				{
					Trace.WriteLine("Wrong format version. Only version 1.4.0 is supported !");
					return false;
				}

				// Browse each node
				foreach (XmlNode node in doc.DocumentElement)
				{
					switch (node.Name)
					{
						case "library_geometries":
						{
							LoadGeometries(node);
						}
						break;
					}
				}

			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
				return false;
			}
			finally
			{

			}

			return true;
		}



		/// <summary>
		/// Loads geometries
		/// </summary>
		/// <param name="node">Geometries node</param>
		void LoadGeometries(XmlNode xml)
		{
			if (xml == null || xml.Name != "library_geometries")
				return;

			foreach (XmlNode node in xml.ChildNodes)
			{
				if (node.Name == "geometry")
				{
					Geometry geometry = new Geometry(node);
					Geometries[geometry.Name] = geometry;
				}
			}

		}



		/// <summary>
		/// Generates a mesh
		/// </summary>
		/// <param name="name">Name of the geometry</param>
		/// <returns></returns>
		public Mesh GenerateMesh(string name)
		{
			if (string.IsNullOrEmpty(name) || !Geometries.ContainsKey(name))
				return null;

			Mesh mesh = new Mesh();



			return mesh;
		}



		#region Properties


		/// <summary>
		/// Geometries
		/// </summary>
		Dictionary<string, Geometry> Geometries;

		#endregion





		/// <summary>
		/// Describes the visual shape and appearance of an object in a scene.
		/// </summary>
		class Geometry
		{

			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Geometry(XmlNode xml)
			{
				if (xml == null || xml.Name != "geometry")
					throw new ArgumentNullException("xml");

				if (xml.Attributes["id"] != null)
					ID = xml.Attributes["id"].Value;
				if (xml.Attributes["name"] != null)
					Name = xml.Attributes["name"].Value;


				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "mesh":
						{
							Mesh = new SubMesh(node);
						}
						break;
					}
				}
			}



			#region Properties

			/// <summary>
			/// 
			/// </summary>
			public SubMesh Mesh
			{
				get;
				private set;
			}

			/// <summary>
			/// Name of the geometry
			/// </summary>
			public string Name
			{
				get;
				private set;
			}

			/// <summary>
			/// ID of the geometry
			/// </summary>
			public string ID
			{
				get;
				private set;
			}


			#endregion
		}


		/// <summary>
		/// Describes basic geometric meshes using vertex and primitive information.
		/// </summary>
		class SubMesh
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public SubMesh(XmlNode xml)
			{
				if (xml == null || xml.Name != "mesh")
					throw new ArgumentException("xml");

				Sources = new Dictionary<string, Source>();

				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "source":
						{
							Source source = new Source(node);
							Sources[source.Id] = source;
						}
						break;

						case "vertices":
						{
							Vertices = new Vertices(node);
						}
						break;

						case "triangles":
						{
							Triangles = new Triangles(node);
						}
						break;
					}
				}
			}


			#region Properties


			/// <summary>
			/// Vertices
			/// </summary>
			public Vertices Vertices
			{
				get;
				private set;
			}

			/// <summary>
			/// Sources
			/// </summary>
			Dictionary<string, Source> Sources;


			/// <summary>
			/// Triangles
			/// </summary>
			public Triangles Triangles
			{
				get;
				private set;
			}


			#endregion
		}


		/// <summary>
		///Declares a data repository that provides values according 
		///to the semantics of an <input> element that refers to it.
		/// </summary>
		class Source
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Source(XmlNode xml)
			{
				if (xml == null || xml.Name != "source")
					throw new ArgumentException("xml");

				Id = xml.Attributes["id"].Value;
				if (xml.Attributes["name"] != null)
					Name = xml.Attributes["name"].Value;

				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "float_array":
						{
							Array = new FloatArray(node);
						}
						break;

						case "technique_common":
						{
							Technique = new TechniqueCommon(node);
						}
						break;
					}
				}
			}


			#region Properties


			/// <summary>
			/// Unique identifier of the element.
			/// </summary>
			public string Id
			{
				get;
				private set;
			}


			/// <summary>
			/// Name
			/// </summary>
			public string Name
			{
				get;
				private set;
			}


			/// <summary>
			/// 
			/// </summary>
			public FloatArray Array
			{
				get;
				private set;
			}

			/// <summary>
			/// 
			/// </summary>
			public TechniqueCommon Technique
			{
				get;
				private set;
			}

			#endregion
		}


		/// <summary>
		/// Specifies information for a specific element for the common profile that all COLLADA implementations must support.
		/// </summary>
		class TechniqueCommon
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public TechniqueCommon(XmlNode xml)
			{
				if (xml == null || xml.Name != "technique_common")
					throw new ArgumentException("xml");

				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "accessor":
						{
							Accessor = new Accessor(node);
						}
						break;
					}
				}
			}


			#region Properties


			/// <summary>
			/// Accessor
			/// </summary>
			public Accessor Accessor
			{
				get;
				private set;
			}



			#endregion
		}


		/// <summary>
		/// Declares the storage for a homogenous array of floating-point values.
		/// </summary>
		class FloatArray
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public FloatArray(XmlNode xml)
			{
				if (xml == null || xml.Name != "float_array")
					throw new ArgumentException("xml");

				Count = int.Parse(xml.Attributes["count"].Value);
				if (xml.Attributes["id"] != null)
					Id = xml.Attributes["id"].Value;
				if (xml.Attributes["name"] != null)
					Name = xml.Attributes["name"].Value;


				Data = new float[Count];
				string[] arr = xml.InnerText.Replace('.', ',').Split(' ');
				for (int i = 0; i < arr.Length; i++)
				{
					Data[i] = float.Parse(arr[i]);
				}
			}



			#region Properties

			/// <summary>
			/// Datas
			/// </summary>
			public float[] Data
			{
				get;
				private set;
			}


			/// <summary>
			/// Count
			/// </summary>
			public int Count
			{
				get;
				private set;
			}


			/// <summary>
			/// Id
			/// </summary>
			public string Id
			{
				get;
				private set;
			}

			/// <summary>
			/// Name
			/// </summary>
			public string Name
			{
				get;
				private set;
			}

			#endregion
		}


		/// <summary>
		/// Describes a stream of values from an array data source.
		/// </summary>
		class Accessor
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Accessor(XmlNode xml)
			{
				if (xml == null || xml.Name != "accessor")
					throw new ArgumentException("xml");

				Params = new List<Param>();
				Source = xml.Attributes["source"].Value;
				if (xml.Attributes["stride"] != null)
				{
				}
				Stride = int.Parse(xml.Attributes["stride"].Value);
				Count = int.Parse(xml.Attributes["count"].Value);
				if (xml.Attributes["offset"] != null)
					Offset = int.Parse(xml.Attributes["offset"].Value);

				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "param":
						{
							Params.Add(new Param(node));
						}
						break;
					}
				}
			}


			#region Properties

			/// <summary>
			/// Params
			/// </summary>
			public List<Param> Params
			{
				get;
				private set;
			}


			/// <summary>
			/// Source
			/// </summary>
			public string Source
			{
				get;
				private set;
			}



			/// <summary>
			/// Count
			/// </summary>
			public int Count
			{
				get;
				private set;
			}


			/// <summary>
			/// Stride
			/// </summary>
			public int Stride
			{
				get;
				private set;
			}

			/// <summary>
			/// Offset
			/// </summary>
			public int Offset
			{
				get;
				private set;
			}


			#endregion
		}


		/// <summary>
		/// Declares parametric information for its parent element.
		/// </summary>
		class Param
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Param(XmlNode xml)
			{
				if (xml == null || xml.Name != "param")
					throw new ArgumentException("xml");

				if (xml.Attributes["name"] != null)
					Name = xml.Attributes["name"].Value;

			}


			#region Properties

			/// <summary>
			/// 
			/// </summary>
			public string Name
			{
				get;
				private set;
			}

			/// <summary>
			/// 
			/// </summary>
			public Type Type
			{
				get;
				private set;
			}

			#endregion
		}


		/// <summary>
		/// Declares the attributes and identity of mesh-vertices.
		/// </summary>
		class Vertices
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Vertices(XmlNode xml)
			{
				if (xml == null || xml.Name != "vertices")
					throw new ArgumentException("xml");

				Id = xml.Attributes["id"].Value;
				if (xml.Attributes["name"] != null)
					Name = xml.Attributes["name"].Value;

				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "input":
						{
							Input = new Input(node);
						}
						break;
					}
				}
			}


			#region Properties


			/// <summary>
			/// Id
			/// </summary>
			public string Id
			{
				get;
				private set;
			}



			/// <summary>
			/// Name
			/// </summary>
			public string Name
			{
				get;
				private set;
			}


			/// <summary>
			/// 
			/// </summary>
			public Input Input
			{
				get;
				private set;
			}

			#endregion
		}


		/// <summary>
		/// Declares the input semantics of a data source and connects a consumer to that source.
		/// </summary>
		class Input
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Input(XmlNode xml)
			{
				if (xml == null || xml.Name != "input")
					throw new ArgumentException("xml");

				if (xml.Attributes["offset"] != null)
					Offset = int.Parse(xml.Attributes["offset"].Value);

				Semantic = xml.Attributes["semantic"].Value;
				Source = xml.Attributes["source"].Value;
			}


			#region Properties


			/// <summary>
			/// Offset
			/// </summary>
			public int Offset
			{
				get;
				private set;
			}


			/// <summary>
			/// Semantic
			/// </summary>
			public string Semantic
			{
				get;
				private set;
			}


			/// <summary>
			/// Source
			/// </summary>
			public string Source
			{
				get;
				private set;
			}



			#endregion
		}


		/// <summary>
		/// Provides the information needed to for a mesh to bind vertex attributes together and 
		/// then organize those vertices into individual triangles.
		/// </summary>
		class Triangles
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="xml"></param>
			public Triangles(XmlNode xml)
			{
				if (xml == null || xml.Name != "triangles")
					throw new ArgumentException("xml");


				Inputs = new List<Input>();
				Count = int.Parse(xml.Attributes["count"].Value);

				if (xml.Attributes["material"] != null)
					Material = xml.Attributes["material"].Value;
				if (xml.Attributes["name"] != null)
					Name = xml.Attributes["name"].Value;

				foreach (XmlNode node in xml.ChildNodes)
				{
					switch (node.Name)
					{
						case "input":
						{
							Inputs.Add(new Input(node));
						}
						break;

						case "p":
						{
							Data = new int[Count * 3 * Inputs.Count];

							string[] arr = node.InnerText.Split(' ');
							for (int i = 0; i < arr.Length; i++)
							{
								Data[i] = int.Parse(arr[i]);
							}
						}
						break;
					}
				}
			}


			#region Properties

			/// <summary>
			/// Material
			/// </summary>
			public string Material
			{
				get;
				private set;
			}


			/// <summary>
			/// Name
			/// </summary>
			public string Name
			{
				get;
				private set;
			}



			/// <summary>
			/// Count
			/// </summary>
			public int Count
			{
				get;
				private set;
			}


			/// <summary>
			/// Inputs
			/// </summary>
			public List<Input> Inputs
			{
				get;
				private set;
			}

			/// <summary>
			/// 
			/// </summary>
			public int[] Data
			{
				get;
				private set;
			}

			#endregion
		}
	}

}