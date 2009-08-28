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
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;



namespace ArcEngine
{


	/// <summary>
	/// Settings class holding all settings of the game
	/// </summary>
	static public class Settings
	{

		/// <summary>
		/// Constructor
		/// </summary>
		static Settings()
		{
			Tokens = new Dictionary<string, string>();
		}


		/// <summary>
		/// Remove all settings
		/// </summary>
		public static void Clear()
		{
			Tokens.Clear();
		}




		#region Tokens

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		static public void SetToken(string name, string value)
		{
			Tokens[name] = value;
		}


		/// <summary>
		/// Gets a string token
		/// </summary>
		/// <param name="name">Token's name</param>
		/// <returns>String value or string.Empty if not found</returns>
		static public string GetString(string name)
		{
			if (Tokens.ContainsKey(name))
				return Tokens[name];

			return string.Empty;
		}


		/// <summary>
		/// Gets an int token
		/// </summary>
		/// <param name="name">Token's name</param>
		/// <returns>Int value, or 0 if not found</returns>
		static public int GetInt(string name)
		{
			if (Tokens.ContainsKey(name))
				return int.Parse(Tokens[name]);

			return 0;
		}


		/// <summary>
		/// Gets a float token
		/// </summary>
		/// <param name="name">Token's name</param>
		/// <returns>Float value, or 0.0f if not found</returns>
		static float GetFloat(string name)
		{
			if (Tokens.ContainsKey(name))
				return float.Parse(Tokens[name]);

			return 0.0f;
		}


		#endregion



		#region IO

		/// <summary>
		/// Saves settings
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <returns></returns>
		static public bool Save(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return false;


			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = false;
			settings.IndentChars = "\t";
			settings.Encoding = ASCIIEncoding.ASCII;

			XmlWriter writer = XmlWriter.Create(filename, settings);
			writer.WriteStartDocument(true);
			writer.WriteStartElement("settings");
			foreach (KeyValuePair<string, string> kvp in Tokens)
			{
				writer.WriteElementString(kvp.Key, kvp.Value);
			}
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();


			return true;
		}



		/// <summary>
		/// Loads settings from a file
		/// </summary>
		/// <param name="filename">Filename to loade</param>
		/// <returns></returns>
		static public bool Load(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return false;


			Trace.Write("Loading settings from file \"" + filename + "\"...");

			// File exists ??
			if (File.Exists(filename) == false)
			{
				Trace.WriteLine("File not found !!!");
				return false;
			}

			
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			// Check the root node
			XmlElement xml = doc.DocumentElement;
			if (xml.Name.ToLower() != "settings")
			{
				Trace.WriteLine("\"" + filename + "\" is not a valid settings file !");
				return false;
			}



			// For each nodes, process it
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;

				Tokens[node.Name] = node.InnerText;
			}


			Trace.WriteLine("OK");
			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// List of tokens
		/// </summary>
		static Dictionary<string, string> Tokens;



		#endregion

	}
}


