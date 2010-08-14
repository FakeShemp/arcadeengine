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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Xml;
using System.Reflection;


namespace ArcEngine.Asset
{

	/// <summary>
	/// Class holding the name of the script and the interface to use
	/// </summary>
	public class ScriptInterface
	{


		/// <summary>
		/// Creates an instance 
		/// </summary>
		/// <typeparam name="T">Type of instance</typeparam>
		/// <returns>Instance handle</returns>
		public T CreateInstance<T>()
		{
			if (string.IsNullOrEmpty(ScriptName) && string.IsNullOrEmpty(InterfaceName))
				return default(T);

			// Create the script
			Script script = ResourceManager.CreateAsset<Script>(ScriptName);
			script.Compile();

			
			// Get the generic method
			MethodInfo mi = script.GetType().GetMethod("CreateInstance").MakeGenericMethod(typeof(T));


			// Invoke the generic method to create the instance
			object obj = mi.Invoke(script, new object[]{InterfaceName});

			// return the interface
			return (T) obj;
		}


		#region Load & Save

		/// <summary>
		/// Loads an item definition
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns>True if loaded, or false</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.NodeType != XmlNodeType.Element)
				return false;


			if (xml.Attributes["script"] != null)
				ScriptName = xml.Attributes["script"].Value;

			if (xml.Attributes["interface"] != null)
				InterfaceName = xml.Attributes["interface"].Value;

			return true;
		}



		/// <summary>
		/// Saves the item definition
		/// </summary>
		/// <param name="name">Name of the tag</param>
		/// <param name="writer">Xml writer handle</param>
		/// <returns>True if saved, or false</returns>
		public bool Save(string name, XmlWriter writer)
		{
			if (writer == null || string.IsNullOrEmpty(name))
				return false;

			writer.WriteStartElement(name);
			writer.WriteAttributeString("script", ScriptName);
			writer.WriteAttributeString("interface", InterfaceName);
			writer.WriteEndElement();

			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// Script to use
		/// </summary>
		public string ScriptName
		{
			get;
			set;
		}


		/// <summary>
		/// Interface to use
		/// </summary>
		public string InterfaceName
		{
			get;
			set;
		}

		#endregion
	}
}
