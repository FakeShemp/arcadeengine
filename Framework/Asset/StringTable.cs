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
using System.Text;
using System.Collections.Generic;
using System.Xml;
using ArcEngine.Interface;

//
// http://wiki.openttd.org/Format_of_langfiles
//
//
//
//
//
//
//
//
//


namespace ArcEngine.Asset
{
	/// <summary>
	/// Helper class that automates text strings management. You may define all your strings in a single text
	/// file and use it to access them by an id (makes localization for different countries much easier). 
	/// </summary>
	public class StringTable : IAsset
	{

		/// <summary>
		/// Constructeur
		/// </summary>
		public StringTable()
		{
			Languages = new Dictionary<string, Language>();
			IsDisposed = false;

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
		/// 
		/// </summary>
		public void Dispose()
		{
		}


		/// <summary>
		/// Build a message from a string and arguments
		/// </summary>
		/// <param name="id">ID of the string</param>
		/// <param name="args">Arguments</param>
		/// <returns>Built message</returns>
		public string BuildMessage(int id, params object[] args)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat(GetString(id), args);

			return sb.ToString();
		}


		#region IO routines

		///
		///<summary>
		/// Saves the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			xml.WriteStartElement("stringtable");
			xml.WriteAttributeString("name", Name);

			xml.WriteStartElement("default");
			xml.WriteAttributeString("name", Default);
			xml.WriteEndElement();


			foreach (Language lang in Languages.Values)
				lang.Save(xml);

			xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads the collection from an xmlnode
		/// </summary>
		/// <param name="xml">Xml definition</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;


			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					// Found a cell to add
					case "language":
					{
						Language lang = new Language();
						lang.Name = node.Attributes["name"].Value;
						lang.Load(node);

						Languages[lang.Name] = lang;
					}
					break;

					case "default":
					{
						Default = node.Attributes["name"].Value;
					}
					break;
		
					default:
					{
						Trace.WriteLine("StringTable : Unknown node element found \"" + node.Name + "\"");
					}
					break;
				}
			}


			return true;
		}


		#endregion


		#region String helpers
	
		/// <summary>
		/// Returns all strings
		/// </summary>
		/// <param name="languagename">Name of the language</param>
		/// <returns></returns>
		public List<string> GetStrings(string languagename)
		{
			List<string> list = new List<string>();
			if (!Languages.ContainsKey(languagename))
				return list;

			Language lang = Languages[languagename];


			return list;
		}


		/// <summary>
		/// Adds a string to the current language
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public int AddString(string message)
		{
			if (CurrentLanguage == null)
				return -1;

			return CurrentLanguage.AddString(message);
		}



		/// <summary>
		/// Returns a string
		/// </summary>
		/// <param name="id">ID of the string</param>
		/// <returns>The string or string.Empty if not found</returns>
		public string GetString(int id)
		{
			if (CurrentLanguage == null)
			{
				// Try default language
				CurrentLanguage = GetLanguage(Default);
				if (CurrentLanguage == null)
					return string.Empty;
			}

			string str = string.Empty;

			try
			{
				// First try to find in the current language
				str = CurrentLanguage.GetString(id);
			}
			catch (KeyNotFoundException)
			{
				// If not found, try to find in the default language
				try
				{
					str = Languages[Default].GetString(id);
				}
				catch (KeyNotFoundException)
				{
					// Oops, not found at all !!
					return string.Empty;
				}
			}

			return str;
		}


		/// <summary>
		/// Returns a formated string with arguments
		/// </summary>
		/// <param name="id">string id</param>
		/// <param name="args">Arguments</param>
		/// <returns>Formated string</returns>
		public string GetString(int id, params object[] args)
		{
			string str = GetString(id);
			return string.Format(str, args);
		}


		/// <summary>
		/// Returns a string in a specified language
		/// </summary>
		/// <param name="id">String id</param>
		/// <param name="language">Language name</param>
		/// <returns>Formated string</returns>
		public string GetString(int id, string language)
		{
			if (!Languages.ContainsKey(language))
				return string.Empty;


			string str = string.Empty;
			try
			{
				str = Languages[language].GetString(id);
			}
			catch (KeyNotFoundException)
			{
				// Oops, not found at all !!
				return string.Empty;
			}

			return str;
		}



		/// <summary>
		/// Returns a formated string in a specified language
		/// </summary>
		/// <param name="id">String id</param>
		/// <param name="language">Language name</param>
		/// <param name="args">Arguments</param>
		/// <returns>Formated string</returns>
		public string GetString(int id, string language, params object[] args)
		{
			string str = GetString(id, language);
			return string.Format(str, args);
		}


		/// <summary>
		/// Sets the value of a string
		/// </summary>
		/// <param name="id">ID of the string</param>
		/// <param name="message">Value of the message</param>
		public void SetString(int id, string message)
		{
			if (CurrentLanguage == null)
				return;

			CurrentLanguage.SetString(id, message);
		}


		/// <summary>
		/// Sets the value of a string in a specified language
		/// </summary>
		/// <param name="id">String id</param>
		/// <param name="message">Message</param>
		/// <param name="language">Language name</param>
		public void SetString(int id, string message, string language)
		{
			if (!Languages.ContainsKey(language))
				return;

			Languages[language].SetString(id, message);
		}

		#endregion


		#region Languages

		/// <summary>
		/// Adds a new language
		/// </summary>
		/// <param name="name">Name of the new language</param>
		public void AddLanguage(string name)
		{
			Language lang = new Language();
			lang.Name = name;

			Languages[name] = lang;
		}


		/// <summary>
		/// Remove a language
		/// </summary>
		/// <param name="name"></param>
		public void RemoveLanguage(string name)
		{
			if (Languages.ContainsKey(name))
				Languages.Remove(name);

			if (CurrentLanguage.Name == name)
			{
				CurrentLanguage = null;
				Default = string.Empty;
			}
		}

		/// <summary>
		/// Gets a Language
		/// </summary>
		/// <param name="name">Name</param>
		/// <returns></returns>
		public Language GetLanguage(string name)
		{
			if (!Languages.ContainsKey(name))
				return null;


			return Languages[name];
		}

		#endregion



		#region Properties

		/// <summary>
		/// Name of the string table
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Is asset disposed
		/// </summary>
		public bool IsDisposed { get; private set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "stringtable";
			}
		}


		/// <summary>
		/// Default language to use as a fall back
		/// </summary>
		public string Default
		{
			get;
			set;
		}


		/// <summary>
		/// Gets / sets the language name to use
		/// </summary>
		public string LanguageName
		{
			get
			{
				if (CurrentLanguage != null)
					return CurrentLanguage.Name;
				else
					return string.Empty;
			}
			set
			{
				if (Languages.ContainsKey(value))
					CurrentLanguage = Languages[value];
			}
		}


		/// <summary>
		/// Returns a list of available languages
		/// </summary>
		public List<string> LanguagesList
		{
			get
			{
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, Language> kvp in Languages)
					list.Add(kvp.Key);

				return list;
			}
		}


		/// <summary>
		/// Gets the current language to use
		/// </summary>
		public Language CurrentLanguage
		{
			get;
			private set;
		}


		/// <summary>
		/// All available strings
		/// </summary>
		Dictionary<string, Language> Languages;

		#endregion


	}

}
