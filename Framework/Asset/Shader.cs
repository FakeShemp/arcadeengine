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
using System.IO;
using System.Xml;
using OpenTK.Graphics.OpenGL;


// http://bakura.developpez.com/tutoriels/jeux/utilisation-shaders-avec-opengl-3-x/
// http://www.siteduzero.com/tutoriel-3-8879-communiquer-avec-l-application-attributs-et-uniforms.html#ss_part_2
namespace ArcEngine.Asset
{
	/// <summary>
	/// 
	/// </summary>
	public class Shader : IAsset, IDisposable
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Shader()
		{
			VertexID = GL.CreateShader(ShaderType.VertexShader);
			FragmentID = GL.CreateShader(ShaderType.FragmentShader);
			GeometryID = GL.CreateShader(ShaderType.GeometryShader);
			ProgramID = GL.CreateProgram();
			GeometryInput = BeginMode.Lines;
			GeometryOutput = BeginMode.Lines;
		}



		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="vertex">Vertex shader source code</param>
		/// <param name="fragment">Fragment shader source code</param>
		public Shader(string vertex, string fragment) : this()
		{
			VertexSource = vertex;
			FragmentSource = fragment;

			UseGeometryShader = false;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="vertex">Vertex shader source code</param>
		/// <param name="fragment">Fragment shader source code</param>
		/// <param name="geometry">Geometry shader source code</param>
		public Shader(string vertex, string fragment, string geometry) : this()
		{
			VertexSource = vertex;
			FragmentSource = fragment;
			GeometrySource = geometry;

			UseGeometryShader = true;
		}




		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}

	

		/// <summary>
		/// Compiles the shader
		/// </summary>
		/// <returns>Returns true is everything went right</returns>
		public bool Compile()
		{
			IsCompiled = false;

			GL.CompileShader(VertexID);
			VertexLog = GL.GetShaderInfoLog(VertexID);

			GL.CompileShader(FragmentID);
			FragmentLog = GL.GetShaderInfoLog(FragmentID);
			

			GL.AttachShader(ProgramID, VertexID);
			GL.AttachShader(ProgramID, FragmentID);

			if (UseGeometryShader && GeometryID != 0)
			{
				GL.CompileShader(GeometryID);
				GeometryLog = GL.GetShaderInfoLog(GeometryID);
				GL.AttachShader(ProgramID, GeometryID);
			}


			GL.LinkProgram(ProgramID);
			ProgramLog = GL.GetProgramInfoLog(ProgramID);


			// Is program compiled ?
			int value;
			GL.GetProgram(ProgramID, ProgramParameter.ValidateStatus, out value);
			if (value == 1)
				IsCompiled = true;
			else
			{
				Trace.WriteLine("Vertex : {0}", VertexLog);
				Trace.WriteLine("Fragment : {0}", FragmentLog);
				Trace.WriteLine("Geometry : {0}", GeometryLog);
				Trace.WriteLine("Program : {0}", ProgramLog);
			}

			return IsCompiled;
		}


		/// <summary>
		/// Sets the source code for a shader
		/// </summary>
		/// <param name="type">Shader target</param>
		/// <param name="source">Source code</param>
		public void SetSource(ShaderType type, string source)
		{
			int id = 0;
			switch (type)
			{
				case ShaderType.FragmentShader:
				{
					FragmentSource = source;
					id = FragmentID;
				}
				break;
				case ShaderType.GeometryShader:
				{
					GeometrySource = source;
					id = GeometryID;
					UseGeometryShader = true;
				}
				break;
				case ShaderType.VertexShader:
				{
					VertexSource = source;
					id = VertexID;
				}
				break;
			}

			GL.ShaderSource(id, source);
		}


		/// <summary>
		/// Sets the source code for a shader from a file
		/// </summary>
		/// <param name="type">Shader target</param>
		/// <param name="filename">Name of the file to load</param>
		public bool LoadSource(ShaderType type, string filename)
		{
			try
			{
				StreamReader streamReader = new StreamReader(filename);
				string source = streamReader.ReadToEnd();
				streamReader.Close();

				SetSource(type, source);

			}
			catch (Exception)
			{
				return false;
			}


			return true;
		}


		/// <summary>
		/// Tells what kind of geometry the shader will process
		/// </summary>
		/// <param name="input">Set the input type of the primitives we are going to feed the geometry shader</param>
		/// <param name="output">Set the output type of the geometry shader</param>
		/// <param name="count">We must tell the shader program how much vertices the geometry shader will output</param>
		public void SetGeometryPrimitives(BeginMode input, BeginMode output, int count)
		{
			// Set the input type of the primitives we are going to feed the geometry shader, this should be the same as
			// the primitive type given to GL.Begin. If the types do not match a GL error will occur (todo: verify GL_INVALID_ENUM, on glBegin)
			GL.Ext.ProgramParameter(ProgramID, ExtGeometryShader4.GeometryInputTypeExt, (int)input);
			GeometryInput = input;

			// Set the output type of the geometry shader. Because we input Lines we will output LineStrip(s).
			GL.Ext.ProgramParameter(ProgramID, ExtGeometryShader4.GeometryOutputTypeExt, (int)output);
			GeometryOutput = output;

			// We must tell the shader program how much vertices the geometry shader will output (at most).
			// One simple way is to query the maximum and use that.
			GL.Ext.ProgramParameter(ProgramID, ExtGeometryShader4.GeometryVerticesOutExt, count);
		}



		#region Statics 
	

		/// <summary>
		/// Creates a simple color shader
		/// </summary>
		/// <returns></returns>
		static public Shader ColorShader()
		{
			Shader shader = new Shader();
			shader.SetSource(ShaderType.VertexShader,
				@"
				void main()
				{
					gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
					gl_FrontColor = gl_Color;
				}");

			shader.SetSource(ShaderType.FragmentShader,
				@"
				void main()
				{					
					gl_FragColor = gl_Color;
				}");


			return shader;
		}


		/// <summary>
		/// Creates a simple textured color shader
		/// </summary>
		/// <remarks>Sets uniform "texture" as the sampler2D</remarks>
		/// <returns></returns>
		static public Shader TextureShader()
		{
			Shader shader = new Shader();
			shader.SetSource(ShaderType.VertexShader,
				@"
				void main()
				{
					gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
					gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
					gl_FrontColor = gl_Color;
				}");

			shader.SetSource(ShaderType.FragmentShader,
				@"
				uniform sampler2D texture;

				void main()
				{
					vec4 color = texture2D(texture, gl_TexCoord[0].st);
					gl_FragColor = gl_Color * color;
				}");


			return shader;
		}

		#endregion

		#region Uniforms

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		public void SetUniform(int id, float value)
		{
			GL.Uniform1(id, value);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		public void SetUniform(int id, int value)
		{
			GL.Uniform1(id, value);
		}


		/// <summary>
		/// Sets a uniform value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">Value</param>
		public void SetUniform(int id, float[] value)
		{
			if (value.Length == 1)
				GL.Uniform1(id, value[0]);
			else if (value.Length == 2)
				GL.Uniform2(id, value[0], value[1]);
			else if (value.Length == 3)
				GL.Uniform3(id, value[0], value[1], value[2]);
			else if (value.Length == 4)
				GL.Uniform4(id, value[0], value[1], value[2], value[3]);
			
		}


		/// <summary>
		/// Returns the ID of a uniform
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <returns>Uniform's id</returns>
		public int GetUniform(string name)
		{
			return GL.GetUniformLocation(ProgramID, name);
		}


		#endregion


		#region Attributes


		/// <summary>
		/// Associates a generic vertex attribute index with a named attribute variable
		/// </summary>
		/// <param name="id">Specifies the index of the generic vertex attribute to be bound.</param>
		/// <param name="name">Name of the vertex shader attribute variable to which index is to be bound.</param>
		public void BindAttrib(int index, string name)
		{
			GL.BindAttribLocation(ProgramID, index, name);
		}


		/// <summary>
		/// Returns the ID of an attribute
		/// </summary>
		/// <param name="name">Name of the attribute</param>
		/// <returns>Attribute's id</returns>
		public int GetAttribute(string name)
		{
			return GL.GetAttribLocation(ProgramID, name);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		public void SetAttribute(int id, float value)
		{
			GL.VertexAttrib1(id, value);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		public void SetAttribute(int id, int value)
		{
			GL.VertexAttrib1(id, value);
		}



		/// <summary>
		/// Sets a uniform value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">Value</param>
		public void SetAttribute(int id, float[] value)
		{
			if (value.Length == 1)
				GL.VertexAttrib1(id, value[0]);
			else if (value.Length == 2)
				GL.VertexAttrib2(id, value[0], value[1]);
			else if (value.Length == 3)
				GL.VertexAttrib3(id, value[0], value[1], value[2]);
			else if (value.Length == 4)
				GL.VertexAttrib4(id, value[0], value[1], value[2], value[3]);

		}

		#endregion


		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement(XmlTag);
			writer.WriteAttributeString("name", Name);

			writer.WriteStartElement("vertex");
			writer.WriteString(VertexSource);
			writer.WriteEndElement();

			writer.WriteStartElement("fragment");
			writer.WriteString(FragmentSource);
			writer.WriteEndElement();

			writer.WriteEndElement();

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					case "vertex":
					{
						VertexSource = xml.InnerText;
					}
					break;

					case "fragment":
					{
						FragmentSource = xml.InnerText;
					}
					break;


					default:
					{
						Trace.WriteLine("{0} : Unknown node element found ({1}) !", XmlTag, node.Name);
					}
					break;
				}
			}


			return true;
		}

		#endregion

		
		#region Properties

		/// <summary>
		/// Name of the script
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "shader";
			}
		}


		/// <summary>
		/// Gets if the Shader is compiled
		/// </summary>
		public bool IsCompiled
		{
			get;
			private set;
		}

		/// <summary>
		/// Vertex handle
		/// </summary>
		int VertexID;


		/// <summary>
		/// Vertex source code
		/// </summary>
		public string VertexSource
		{
			get;
			private set;
		}


		/// <summary>
		/// Fragment handle
		/// </summary>
		int FragmentID;


		/// <summary>
		/// Fragment source
		/// </summary>
		public string FragmentSource
		{
			get;
			private set;
		}


		/// <summary>
		/// Geometry handle
		/// </summary>
		int GeometryID;


		/// <summary>
		/// Geomerty source
		/// </summary>
		public string GeometrySource
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets the maximum number of vertices that a geometry program can output
		/// </summary>
		public int MaxGeometryOutputVertex
		{
			get
			{
				if (ProgramID == 0)
					return 0;

				int count = 0;
				GL.GetProgram(ProgramID, ProgramParameter.GeometryVerticesOut, out count);

				return count;
			}
		}


		/// <summary>
		/// Enable or not the geometry shader
		/// </summary>
		public bool UseGeometryShader
		{
			get;
			set;
		}

		/// <summary>
		/// Program handle
		/// </summary>
		internal int ProgramID
		{
			get;
			private set;
		}



		/// <summary>
		/// Vertex log
		/// </summary>
		public string VertexLog
		{
			get;
			private set;
		}

		/// <summary>
		/// Fragment log
		/// </summary>
		public string FragmentLog
		{
			get;
			private set;
		}


		/// <summary>
		/// Geometry log
		/// </summary>
		public string GeometryLog
		{
			get;
			private set;
		}


		/// <summary>
		/// Program log
		/// </summary>
		public string ProgramLog
		{
			get;
			private set;
		}



		/// <summary>
		/// 
		/// </summary>
		public BeginMode GeometryInput
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public BeginMode GeometryOutput
		{
			get;
			private set;
		}

		#endregion


		#region Dispose

		/// <summary>
		/// Implement IDisposable.
		/// </summary>
		public void Dispose()
		{
			GL.DeleteShader(FragmentID);
			GL.DeleteShader(VertexID);
			GL.DeleteShader(GeometryID);

			GL.DeleteProgram(ProgramID);
			FragmentID = -1;
			VertexID = -1;
			GeometryID = -1;
			ProgramID = -1;

		}



		#endregion
	}
}
