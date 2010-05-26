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
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using ArcEngine.Graphic;
using OpenTK.Graphics.OpenGL;

// http://www.wazim.com/Collada_Tutorial_1.htm
// http://www.wazim.com/Collada_Tutorial_2.htm

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
		public Mesh GenerateShape(string name)
		{
			if (string.IsNullOrEmpty(name) || !Geometries.ContainsKey(name))
				return null;

			Mesh shape = new Mesh();

			// Get the geometry
			Geometry geometry = Geometries[name];
			MeshDef mesh = geometry.Mesh;



			// Get the size of one element (sum of all strides)
			int stridesum = 0;
			foreach (Source src in mesh.Sources.Values)
				stridesum += src.Technique.Accessor.Stride;


			

			// Create the index buffer
			int indexCount = mesh.Triangles.Count * 3;
			int[] indexbuffer = new int[indexCount];
			//for (int i = 0; i < indexbuffer.Length; i++)
			//   indexbuffer[i] = -1;

			// Create the array buffer
			float[] buffer = new float[stridesum * mesh.VertexCount];
			for (int i = 0; i < buffer.Length; i++)
			   buffer[i] = 99.0f;

	
			
			
			// Offset in the <p> buffer according to <inputs> strides
			int offset = mesh.Triangles.GetInput(InputSemantic.Vertex).Offset;

			// For each index in the <p> tag
			for (int i = 0; i < indexCount; i++)
			{
				// Position in the <p> tag
				int pos = i * mesh.Triangles.Inputs.Count + offset;

				// Fill the index buffer
				indexbuffer[i] = mesh.Triangles.Data[pos];
			}	
			
			

			// Copy all vertices to the array buffer
			Source source = mesh.GetVerticesSource();
			for (int i = 0; i < source.Technique.Accessor.Count; i++)
			{
				buffer[i * stridesum] = source.Array.Data[i * 3];
				buffer[i * stridesum + 1] = source.Array.Data[i * 3 + 1];
				buffer[i * stridesum + 2] = source.Array.Data[i * 3 + 2];
			}

			


			// For each <input> tag in the triangle
			offset = 0;
			foreach (Input input in mesh.Triangles.Inputs)
			{
				// Get the <source> tag
				source = mesh.GetSource(input.Source);
				if (source == null)
				{
					offset += 3;
					continue;
				}


				// For each index in the <p> tag
				for (int i = 0; i < indexCount; i++)
				{
					// Position in the <p> tag
					int pos = i * mesh.Triangles.Inputs.Count + input.Offset;


					// For each param in the source
					for (int sub = 0; sub < source.Technique.Accessor.Stride; sub++)
					{
						int index = mesh.Triangles.Data[pos];

						float value = source.Array.Data[(index * source.Technique.Accessor.Stride) + sub];

						try
						{

							//buffer[mesh.Triangles.Data[pos] * stridesum + offset + sub] = value;
							buffer[ index * stridesum + offset + sub] = value;
						}
						catch
						{
						}
					}
				}

				//
				offset += source.Technique.Accessor.Stride;
			}


			shape.SetIndices(indexbuffer);
			shape.SetVertices(buffer);
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
							Mesh = new MeshDef(node);
						}
						break;
					}
				}
			}



			#region Properties

			/// <summary>
			/// 
			/// </summary>
			public MeshDef Mesh
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
		class MeshDef
		{
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="xml"></param>
			public MeshDef(XmlNode xml)
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

				Semantic = GetSemantic(xml.Attributes["semantic"].Value);
				Source = xml.Attributes["source"].Value;

				if (xml.Attributes["set"] != null)
					Set = int.Parse(xml.Attributes["set"].Value);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="name"></param>
			/// <returns></returns>
			InputSemantic GetSemantic(string name)
			{
				Dictionary<string, InputSemantic> dic = new Dictionary<string,InputSemantic>();
				dic["BINORMAL"] = InputSemantic.Binormal;
				dic["COLOR"] = InputSemantic.Color;
				dic["CONTINUITY"] = InputSemantic.Continuity;
				dic["IMAGE"] = InputSemantic.Image;
				dic["INPUT"] = InputSemantic.Input;
				dic["IN_TANGENT"] = InputSemantic.InTangent;
				dic["INTERPOLATION"] = InputSemantic.Interpolation;
				dic["INV_BIND_MATRIX"] = InputSemantic.InvBindMatrix;
				dic["JOINT"] = InputSemantic.Joint;
				dic["LINEAR_STEPS"] = InputSemantic.LinearSteps;
				dic["MORPH_TARGET"] = InputSemantic.MorphTarget;
				dic["MORPH_WEIGHT"] = InputSemantic.MorphWeight;
				dic["NORMAL"] = InputSemantic.Normal;
				dic["OUTPUT"] = InputSemantic.Output;
				dic["OUT_TANGENT"] = InputSemantic.OutTangent;
				dic["POSITION"] = InputSemantic.Position;
				dic["TANGENT"] = InputSemantic.Tangent;
				dic["TEXBINORMAL"] = InputSemantic.TexBinormal;
				dic["TEXCOORD"] = InputSemantic.TexCoord;
				dic["TEXTANGENT"] = InputSemantic.TexTangent;
				dic["UV"] = InputSemantic.UV;
				dic["VERTEX"] = InputSemantic.Vertex;
				dic["WEIGHT"] = InputSemantic.Weight;


				if (!dic.ContainsKey(name))
					throw new ArgumentOutOfRangeException();

				return dic[name];
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
			/// 
			/// </summary>
			public int Set
			{
				get;
				private set;
			}

			/// <summary>
			/// Semantic
			/// </summary>
			public InputSemantic Semantic
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
		/// Input semantic connection
		/// </summary>
		enum InputSemantic
		{
			Binormal,

			Color,

			Continuity,

			Image,

			Input,

			InTangent,

			Interpolation,

			InvBindMatrix,

			Joint,

			LinearSteps,

			MorphTarget,

			MorphWeight,

			Normal,

			Output,

			OutTangent,

			Position,

			Tangent,

			TexBinormal,

			TexCoord,

			TexTangent,

			UV,

			Vertex,

			Weight
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
			public Input GetInput(InputSemantic semantic)
			{

				foreach (Input input in Inputs)
				{
					if (input.Semantic == semantic)
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