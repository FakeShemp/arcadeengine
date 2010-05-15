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
		/// Generates a shape
		/// </summary>
		/// <param name="name">Name of the geometry</param>
		/// <returns>Returns an handle to the shape or null</returns>
		public Shape GenerateShape(string name)
		{
			if (string.IsNullOrEmpty(name) || !Geometries.ContainsKey(name))
				return null;

			Shape shape = new Shape();

			// Get the geometry
			Geometry geometry = Geometries[name];
			Mesh mesh = geometry.Mesh;




			// Get the vertex buffer
			Source vsource = mesh.GetVerticesSource();


			// Get the size of the buffer
			int stride = 0;
			foreach (Source src in mesh.Sources.Values)
				stride += src.Technique.Accessor.Stride;


	
			
			// Create the index buffer first

			// Number of index
			int indexCount = mesh.Triangles.Count * 3;

			int[] indexbuffer = new int[indexCount];
			for (int i = 0; i < indexbuffer.Length; i++)
				indexbuffer[i] = -1;

			// For each index in the <p> tag
			Input input = mesh.Triangles.GetInput("VERTEX");
			for (int i = 0; i < indexCount; i++)
			{
				// Position in the <p> tag
				int pos = i * mesh.Triangles.Inputs.Count + input.Offset;

				// Fill the index buffer
				indexbuffer[i] = mesh.Triangles.Data[pos];
			}	
			
			
			
			
			// Destination buffer for OpenGL
			float[] buffer = new float[stride * mesh.VertexCount];
			for (int i = 0; i < buffer.Length; i++)
				buffer[i] = 99.0f;


			// Offset in the OpenGL buffer according to <inputs> strides
			int offset = 0;

			// For each <input> tag in the triangle
			foreach (Input inpt in mesh.Triangles.Inputs)
			{
				// Get the <source> tag
				Source source = mesh.GetSource(inpt.Source);
				if (source == null)
					continue;


				// For each index in the <p> tag
				for (int i = 0; i < indexCount; i++)
				{
					// Position in the <p> tag
					int pos = i * mesh.Triangles.Inputs.Count + input.Offset;

					for (int sub = 0; sub < source.Technique.Accessor.Stride; sub++)
					{
						float value = source.Array.Data[(mesh.Triangles.Data[pos] * source.Technique.Accessor.Stride) + sub];
						buffer[mesh.Triangles.Data[pos] * stride + offset + sub] = value;
					}
				}

				//
				offset += source.Technique.Accessor.Stride;
			}





			// Data from collada
			int[] indexsrc = geometry.Mesh.Triangles.Data;
			float[] vertexsrc = geometry.Mesh.Sources[geometry.Mesh.Vertices.Input.Source].Array.Data;
			float[] normalsrc = geometry.Mesh.Sources[geometry.Mesh.Triangles.Inputs[1].Source].Array.Data;


			// Data for OpenGL
			//int baseLen = geometry.Mesh.Triangles.Count * 3;
			float[] vertex = new float[indexCount * 3];
			float[] normal = new float[indexCount * 3];

			for (int i = 0; i < indexCount; i++)
			{
				int vindex = indexsrc[i * geometry.Mesh.Triangles.InputCount];
				vertex[i * 3] = vertexsrc[vindex * 3];
				vertex[i * 3 + 1] = vertexsrc[vindex * 3 + 1];
				vertex[i * 3 + 2] = vertexsrc[vindex * 3 + 2];

				int nindex = indexsrc[i * geometry.Mesh.Triangles.InputCount + 1];
				normal[i * 3] = normalsrc[nindex * 3];
				normal[i * 3 + 1] = normalsrc[nindex * 3 + 1];
				normal[i * 3 + 2] = normalsrc[nindex * 3 + 2];
			}

			shape.SetVertices(null);
			

			return shape;
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
			/// Loads a geometry tag
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
							Mesh = new Mesh(node);
						}
						break;
					}
				}
			}



			#region Properties

			/// <summary>
			/// 
			/// </summary>
			public Mesh Mesh
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
		class Mesh
		{
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="xml"></param>
			public Mesh(XmlNode xml)
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


			/// <summary>
			/// Returns a source by its name
			/// </summary>
			/// <param name="name">Name of the source</param>
			/// <returns>Handle to the source or null</returns>
			public Source GetSource(string name)
			{
				if (string.IsNullOrEmpty(name))
					return null;

				if (name.StartsWith("#"))
					name = name.Substring(1, name.Length - 1);

				if (Sources.ContainsKey(name))
					return Sources[name];

				
				return null;
			}


			/// <summary>
			/// Returns the source of the vertices for the mesh
			/// </summary>
			/// <returns></returns>
			public Source GetVerticesSource()
			{
				return GetSource(Vertices.Input.Source);
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
			public Dictionary<string, Source> Sources;


			/// <summary>
			/// Triangles
			/// </summary>
			public Triangles Triangles
			{
				get;
				private set;
			}


			/// <summary>
			/// Number of vertex in the mesh
			/// </summary>
			public int VertexCount
			{
				get
				{
					Source source = GetVerticesSource();
					if (source == null)
						return 0;

					return source.Array.Count / 3;
				}
			}

			/// <summary>
			/// Number of face in the mesh
			/// </summary>
			public int FaceCount
			{
				get
				{
					if (Triangles == null)
						return 0;

					return Triangles.Count;
				}
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


			/// <summary>
			/// Gets an <see cref="Input"/> from its name
			/// </summary>
			/// <param name="name">Semantic name</param>
			/// <returns>Handle or null</returns>
			public Input GetInput(string name)
			{
				if (string.IsNullOrEmpty(name))
					return null;

				foreach (Input input in Inputs)
				{
					if (input.Semantic == name)
						return input;
				}

				return null;
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
			/// Name of this element
			/// </summary>
			public string Name
			{
				get;
				private set;
			}



			/// <summary>
			/// Number of triangle primitives
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
			/// Number of input for this element
			/// </summary>
			public int InputCount
			{
				get
				{
					return Inputs.Count;
				}
			}

			/// <summary>
			/// Indices that describes the vertex attributes for a number of triangles
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