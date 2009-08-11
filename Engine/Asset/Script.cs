using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.VisualBasic;


// TODO Pouvoir ajouter des references a des dll depuis soit le ResourceManager
//      soit directement en parametre dans le fichier xml du script
//
// http://www.csscript.net/
// 
namespace ArcEngine.Asset
{

	/// <summary>
	/// Script resource
	/// </summary>
	public class Script : IAsset
	{

		/// <summary>
		/// constructor
		/// </summary>
		public Script()
		{
			// Configure parameters
			Params = new CompilerParameters();
			Params.GenerateExecutable = false;
			Params.GenerateInMemory = true;
			Params.IncludeDebugInformation = true;
			Params.WarningLevel = 3;
			Params.CompilerOptions = "/optimize /define:DEBUG;TRACE";
			



			// Adds the executable itself
			AddReference(Assembly.GetExecutingAssembly().Location);
			AddReference(Assembly.GetCallingAssembly().Location);
			AddReference(Assembly.GetEntryAssembly().Location);
			AddReference("System.dll");
			AddReference("System.Drawing.dll");
			AddReference("System.Xml.dll");
			AddReference("System.Windows.Forms.dll");

			AssemblyName[] ass = Assembly.GetCallingAssembly().GetReferencedAssemblies();
			ass = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
			
			

		}



		/// <summary>
		/// Invoke a method in the script
		/// </summary>
		/// <param name="method">Method's name</param>
		/// <param name="args">Arguments</param>
		/// <returns>true if args or false</returns>
		public bool Invoke(string method, params object[] args)
		{
			if (!IsCompiled)
				return false;


			try
			{
				Type[] type = CompiledAssembly.GetTypes();
				MethodInfo[] mi = type[0].GetMethods();

				object instance = Activator.CreateInstance(type[0]);
				object result = type[0].InvokeMember(method,
					BindingFlags.Default | BindingFlags.InvokeMethod,
					null,
					instance,
					args);
			}
			catch (Exception e)
			{
			}

			return true;
		}


		/// <summary>
		/// Returns all methods
		/// </summary>
		/// <returns></returns>
		public List<string> GetMethods()
		{
			List<string> list = new List<string>();

			if (!IsCompiled)
				Compile();


			if (CompiledAssembly == null)
				return list;

			// Loop through types looking for one that implements the given interface
			foreach (Type t in CompiledAssembly.GetTypes())
			{

				foreach (MethodInfo m in t.GetMethods())
				{

					list.Add(m.Name);
				}

			}

			list.Sort();

			return list;
		}



		/// <summary>
		/// Compiles a given string script
		/// </summary>
		/// <returns>True if compilation ok</returns>
		public bool Compile()
		{
			// Already compiled ?
			if (IsCompiled)
				return true;

			IsCompiled = false;
			IsModified = false;
			HasErrors = false;
			Errors = null;
			CompiledAssembly = null;

			// Generate the compiler provider
			switch (Language)
			{
				case ScriptLanguage.CSharp:
				{
					ScriptProvider = new Microsoft.CSharp.CSharpCodeProvider();
					if (ScriptProvider == null)
					{
						Trace.WriteLine("Failed to initialize a new instance of the CSharpCodeProvider class");

						return false;
					}
				}
				break;

				case ScriptLanguage.VBNet:
				{
					ScriptProvider = new Microsoft.VisualBasic.VBCodeProvider();
					if (ScriptProvider == null)
					{
						Trace.WriteLine("Failed to initialize a new instance of the VBCodeProvider class");
						return false;
					}
				}
				break;

				default:
				{
					Trace.WriteLine("Unknown scripting language !!!");
					return false;
				}

			}




			// Compile
			CompilerResults results = ScriptProvider.CompileAssemblyFromSource(Params, sourceCode);
			if (results.Errors.Count == 0)
			{
				CompiledAssembly = results.CompiledAssembly;
				IsCompiled = true;
				return true;
			}



			Errors = results.Errors;
			HasErrors = true;

			Trace.WriteLine("Compile complete -- " + Errors.Count + " error(s).");

			foreach (CompilerError error in Errors)
				Trace.WriteLine("line " + error.Line + " : " + error.ErrorText);



			return IsCompiled;
		}



		/// <summary>
		/// Adds reference to the assembly
		/// </summary>
		/// <param name="name">Name of the reference</param>
		public void AddReference(string name)
		{
			Params.ReferencedAssemblies.Add(name);
		}




		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("script");
			writer.WriteAttributeString("name", Name);


			writer.WriteStartElement("language");
			writer.WriteAttributeString("name", Language.ToString());
			writer.WriteEndElement();


			writer.WriteStartElement("source");
			writer.WriteRaw(HttpUtility.HtmlEncode(SourceCode));
			writer.WriteEndElement();

			//writer.WriteStartElement("debug");
			//writer.WriteAttributeString("value", DebugMode.ToString());
			//writer.WriteEndElement();

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
					case "language":
					{
						Language = (ScriptLanguage)Enum.Parse(typeof(ScriptLanguage), node.Attributes["name"].Value, true);
					}
					break;


					case "source":
					{
						sourceCode = HttpUtility.HtmlDecode(xml.InnerText);
					}
					break;

					case "reference":
					{
						AddReference(node.Attributes["name"].Value.ToString());
					}
					break;


					//case "debug":
					//{
					//   DebugMode = Boolean.Parse(node.Attributes["value"].Value);
					//}
					//break;

					default:
					{
						Trace.WriteLine("Script : Unknown node element found (" + node.Name + ")");
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
		/// Compiler parameters
		/// </summary>
		CompilerParameters Params;


		/// <summary>
		/// Source code of the script
		/// </summary>
		[Browsable(false)]
		public string SourceCode
		{
			get
			{
				return sourceCode;
			}

			set
			{
				sourceCode = value;
				IsModified = true;
				IsCompiled = false;
			}
		}
		string sourceCode;


		/// <summary>
		/// Enables / disables the debug mode
		/// </summary>
		[CategoryAttribute("Debug")]
		[Description("Enables / disables the debug mode")]
		public bool DebugMode
		{
			get
			{
				return Params.IncludeDebugInformation;
			}
			set
			{
				Params.IncludeDebugInformation = value;
			}
		}

		/// <summary>
		/// Is source code modified ?
		/// </summary>
		[Browsable(false)]
		public bool IsModified
		{
			get;
			private set;
		}



		/// <summary>
		/// Returns the state of the script compilation
		/// </summary>
		[Browsable(false)]
		public bool IsCompiled
		{
			get;
			private set;
		}



		/// <summary>
		/// Gets the compilation log
		/// </summary>
		[Browsable(false)]
		public CompilerErrorCollection Errors
		{
			get;
			private set;
		}


		/// <summary>
		/// Does the script contains error
		/// </summary>
		[Browsable(false)]
		public bool HasErrors
		{
			get;
			private set;
		}



		/// <summary>
		/// Compiled assembly
		/// </summary>
		Assembly CompiledAssembly;


		/// <summary>
		/// Script langage
		/// </summary>
		[CategoryAttribute("Language")]
		[Description("Script's language")]
		public ScriptLanguage Language
		{
			get;
			set;
		}



		/// <summary>
		/// http://www.divil.co.uk/net/articles/plugins/scripting.asp
		/// </summary>
		CodeDomProvider ScriptProvider;


		#endregion

	}

	/// <summary>
	/// Supported languages
	/// </summary>
	public enum ScriptLanguage
	{
		/// <summary>
		/// CSharp language
		/// </summary>
		CSharp,

		/// <summary>
		/// VB.Net language
		/// </summary>
		VBNet
	}




}
