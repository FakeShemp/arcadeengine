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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.VisualBasic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


// http://bakura.developpez.com/tutoriels/jeux/utilisation-shaders-avec-opengl-3-x/

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
			ProgramID = GL.CreateProgram();


			SetSource(ShaderType.VertexShader,
				@"
				void main()
				{
					//gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
					gl_FrontColor = gl_Color;
					gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
				}");

			SetSource(ShaderType.FragmentShader,
				@"
				uniform sampler2D texture;
				void main(){
					//gl_FragColor = texture2D(texture, gl_TexCoord[0]);
					gl_FragColor = gl_Color;
				}");

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="vertex"></param>
		/// <param name="fragment"></param>
		public Shader(string vertex, string fragment) : this()
		{
			VertexSource = vertex;
			FragmentSource = fragment;
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
				Trace.WriteLine("Program : {0}", ProgramLog);
			}

			return IsCompiled;
		}




		/// <summary>
		/// Use the shader
		/// </summary>
		/// <returns></returns>
		static public bool Use(Shader shader)
		{
			if (shader == null)
				GL.UseProgram(0);
			else 
				GL.UseProgram(shader.ProgramID);
			return true;
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
					return;
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
			catch (Exception e)
			{
				return false;
			}


			return true;
		}

		#region Uniforms

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetUniform(string name, float value)
		{
			int id = GetUniform(name);
			Trace.WriteLine("{0} = {1}", name, id);

			GL.Uniform1(id, value);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetUniform(string name, int value)
		{
			int id = GetUniform(name);
		//	Trace.WriteLine("{0} = {1}", name, id);

			GL.Uniform1(id, value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void SetUniform(string name, float[] value)
		{
			int id = GetUniform(name);


		//	Trace.WriteLine("{0} = {1}", name, id);

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
		/// <param name="name"></param>
		/// <returns></returns>
		int GetUniform(string name)
		{
			return GL.GetUniformLocation(ProgramID, name);
		}


		/// <summary>
		/// Returns the ID of an attribute
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		int GetAttribute(string name)
		{
			return GL.GetAttribLocation(ProgramID, name);
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
		/// Program handle
		/// </summary>
		internal int ProgramID
		{
			get;
			private set;
		}



		/// <summary>
		/// 
		/// </summary>
		public string VertexLog
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public string FragmentLog
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public string ProgramLog
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
			Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose(bool disposing) executes in two distinct scenarios.
		/// If disposing equals true, the method has been called directly
		/// or indirectly by a user's code. Managed and unmanaged resources
		/// can be disposed.
		/// If disposing equals false, the method has been called by the
		/// runtime from inside the finalizer and you should not reference
		/// other objects. Only unmanaged resources can be disposed.
		/// </summary>
		/// <param name="disposing"></param>
		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				// If disposing equals true, dispose all managed
				// and unmanaged resources.
				if (disposing)
				{
					// Dispose managed resources.
				}


				GL.DeleteShader(FragmentID);
				GL.DeleteShader(VertexID);

				GL.DeleteProgram(ProgramID);

				// Note disposing has been done.
				disposed = true;

			}
		}


		private bool disposed = false;

		#endregion
	}
}
