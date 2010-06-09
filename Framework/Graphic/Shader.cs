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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ArcEngine.Asset;


// http://bakura.developpez.com/tutoriels/jeux/utilisation-shaders-avec-opengl-3-x/
// http://www.siteduzero.com/tutoriel-3-8879-communiquer-avec-l-application-attributs-et-uniforms.html#ss_part_2
namespace ArcEngine.Graphic
{
	/// <summary>
	/// GLSL Shader
	/// </summary>
	public class Shader : IDisposable, IAsset
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
		/// Destructor
		/// </summary>
		~Shader()
		{
			throw new Exception("Shader : Call Dispose() !!");
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

			int success = 0;

			GL.CompileShader(VertexID);
			GL.GetShader(VertexID, ShaderParameter.CompileStatus, out success);
			if (success != 1)
			{
				VertexLog = GL.GetShaderInfoLog(VertexID);
				if (!string.IsNullOrEmpty(VertexLog))
					Trace.WriteLine("[VertexShader] : {0}", VertexLog);

				return false;
			}


			GL.CompileShader(FragmentID);
			GL.GetShader(FragmentID, ShaderParameter.CompileStatus, out success);
			if (success != 1)
			{
				FragmentLog = GL.GetShaderInfoLog(FragmentID);
				if (!string.IsNullOrEmpty(FragmentLog))
					Trace.WriteLine("[FragmentShader] : {0}", FragmentLog);

				return false;
			}

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
		static public Shader CreateColorShader()
		{
			Shader shader = new Shader();
			shader.SetSource(ShaderType.VertexShader,
				@"#version 130

				precision highp float;

				uniform mat4 mvp_matrix;
				//uniform mat4 tex_matrix;

				in vec2 in_position;
				in vec4 in_color;
				//in vec4 in_texture;

				invariant gl_Position;

				smooth out vec4 out_color;
				//smooth out vec4 out_texture;

				void main(void)
				{
					gl_Position = mvp_matrix * vec4(in_position, 0.0, 1.0);

					out_color = in_color;
					//out_texture = tex_matrix * in_texture;
				}");

			shader.SetSource(ShaderType.FragmentShader,
				@"#version 130

				precision highp float;

				//uniform sampler2D texture;

				smooth in vec4 out_color;
				//smooth in vec4 out_texture;

				out vec4 frag_color;

				void main(void)
				{
					frag_color = out_color;
				}");

			shader.Compile();

			return shader;
		}


		/// <summary>
		/// Creates a simple textured color shader
		/// </summary>
		/// <remarks>Sets uniform "texture" as the sampler2D</remarks>
		/// <returns></returns>
		static public Shader CreateTextureShader()
		{
			Shader shader = new Shader();
			shader.SetSource(ShaderType.VertexShader,
				@"#version 130

				precision highp float;

				uniform mat4 mvp_matrix;
				uniform mat4 tex_matrix;

				in vec2 in_position;
				in vec4 in_color;
				in vec4 in_texture;

				invariant gl_Position;

				smooth out vec4 out_color;
				smooth out vec4 out_texture;

				void main(void)
				{
					gl_Position = mvp_matrix * vec4(in_position, 0.0, 1.0);

					out_color = in_color;
					out_texture = tex_matrix * in_texture;
				}");

			shader.SetSource(ShaderType.FragmentShader,
				@"#version 130

				precision highp float;

				uniform sampler2D texture;

				smooth in vec4 out_color;
				smooth in vec4 out_texture;

				out vec4 frag_color;

				void main(void)
				{
					frag_color = texture2D(texture, out_texture.st) * out_color;
				}");

			shader.Compile();

			return shader;
		}



		/// <summary>
		/// Gets the source code of predefined shader
		/// </summary>
		/// <param name="name">Name of the source</param>
		/// <returns>Shader source code or empty</returns>
		static public string GetShaderSource(string name)
		{
			Stream stream = ResourceManager.GetResource("ArcEngine.Graphic.shaders." + name);
			if (stream == null)
				return string.Empty;

			StreamReader reader = new StreamReader( stream );
			string text = reader.ReadToEnd();
			reader.Close();
			stream.Close();

			return text;
		}

		#endregion


		#region Attributes


		/// <summary>
		/// Associates a generic vertex attribute index with a named attribute variable
		/// </summary>
		/// <param name="index">Specifies the index of the generic vertex attribute to be bound.</param>
		/// <param name="name">Name of the vertex shader attribute variable to which index is to be bound.</param>
		public void BindAttrib(int index, string name)
		{
			if (index < 0 || string.IsNullOrEmpty(name))
				return;

			GL.BindAttribLocation(ProgramID, index, name);
		}


		/// <summary>
		/// Returns the ID of an attribute
		/// </summary>
		/// <param name="name">Name of the attribute</param>
		/// <returns>Attribute's id</returns>
		public int GetAttribute(string name)
		{
			if (string.IsNullOrEmpty(name))
				return -1;

			return GL.GetAttribLocation(ProgramID, name);
		}


		/// <summary>
		/// Sets an attribute value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">New value</param>
		public void SetAttribute(int id, float value)
		{
			if (id < 0)
				return;

			GL.VertexAttrib1(id, value);
		}


		/// <summary>
		/// Sets an attribute value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">New value</param>
		public void SetAttribute(int id, int value)
		{
			if (id < 0)
				return;

			GL.VertexAttrib1(id, value);
		}



		/// <summary>
		/// Sets an attribute value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">New value</param>
		public void SetAttribute(int id, float[] value)
		{
			if (id < 0)
				return;

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
	

		#region Uniforms

		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">New value</param>
		public void SetUniform(int id, float value)
		{
			if (id < 0)
				return;

			GL.Uniform1(id, value);
		}

		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">New value</param>
		public void SetUniform(string name, float value)
		{
			SetUniform(GetUniform(name), value);
		}


		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">New value</param>
		public void SetUniform(int id, int value)
		{
			if (id < 0)
				return;

			GL.Uniform1(id, value);
		}


		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">New value</param>
		public void SetUniform(string name, int value)
		{
			SetUniform(GetUniform(name), value);
		}

	
		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="id">ID of the uniform</param>
		/// <param name="value">New value</param>
		public void SetUniform(int id, float[] value)
		{
			if (id < 0)
				return;

			if (value.Length == 1)
				GL.Uniform1(id, value[0]);
			else if (value.Length == 2)
				GL.Uniform2(id, value[0], value[1]);
			else if (value.Length == 3)
				GL.Uniform3(id, value[0], value[1], value[2]);
			else if (value.Length == 4)
				GL.Uniform4(id, value[0], value[1], value[2], value[3]);
			else if (value.Length == 9)
				GL.UniformMatrix3(id, 9, false, value);
		}


		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <param name="value">New value</param>
		public void SetUniform(string name, float[] value)
		{
			SetUniform(GetUniform(name), value);
		}


		/// <summary>
		/// Returns the ID of a uniform
		/// </summary>
		/// <param name="name">Name of the uniform</param>
		/// <returns>Uniform's id</returns>
		public int GetUniform(string name)
		{
			if (string.IsNullOrEmpty(name))
				return -1;
			
			int id = GL.GetUniformLocation(ProgramID, name);

	//		if (id == -1)
	//			Trace.WriteDebugLine("Uniform {0} not found.", name);

			return id;
		}


		/// <summary>
		/// Sets an uniform value
		/// </summary>
		/// <param name="name">ID of the uniform</param>
		/// <param name="matrix">New value</param>
		public void SetUniform(string name, Matrix4 matrix)
		{
			int id = GetUniform(name);
			if (id < 0)
				return;

			GL.UniformMatrix4(id, false, ref matrix);
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
		public int ProgramID
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

			FragmentLog = "";
			FragmentSource = "";
			VertexLog = "";
			VertexSource = "";
			ProgramLog = "";
			GeometryLog = "";
			GeometrySource = "";


			GC.SuppressFinalize(this);
		}



		#endregion

	}
}
