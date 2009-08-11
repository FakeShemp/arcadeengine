using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Class holding string translation for one language
	/// </summary>
	public class Language
	{

		/// <summary>
		/// 
		/// </summary>
		public Language()
		{
			Strings = new Dictionary<int, string>();
		}


		/// <summary>
		/// Adds a new string
		/// </summary>
		/// <param name="message">Message</param>
		/// <returns>ID of the string</returns>
		public int AddString(string message)
		{
			LastStringID++;
			SetString(LastStringID, message);

			return LastStringID;
		}

		/// <summary>
		/// Returns a string
		/// </summary>
		/// <param name="id">ID of the string</param>
		/// <returns>The string or string.Empty if not found</returns>
		public string GetString(int id)
		{
			if (Strings.ContainsKey(id))
				return Strings[id];

			return string.Empty;
		}


		/// <summary>
		/// Sets the value of a string
		/// </summary>
		/// <param name="id">ID of the string</param>
		/// <param name="message">Value of the message</param>
		public void SetString(int id, string message)
		{
			// If empty, remove the string
			if (string.IsNullOrEmpty(message))
			{
				Strings.Remove(id);
				LastStringID = GetLastID();
			}
			else
			{
				Strings[id] = message;
				LastStringID = Math.Max(LastStringID, id);
			}

		}


		/// <summary>
		/// Get the last string id
		/// </summary>
		/// <returns>Last ID</returns>
		int GetLastID()
		{
			int id = 0;

			foreach (int i in Strings.Keys)
				id = Math.Max(id, i);


			return id;
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

			xml.WriteStartElement("language");
			xml.WriteAttributeString("name", Name);

			foreach(KeyValuePair<int, string> kvp in Strings)
			{
				xml.WriteStartElement("string");
				xml.WriteAttributeString("id", kvp.Key.ToString());
				xml.WriteRaw(kvp.Value);
				xml.WriteEndElement();
			}

			xml.WriteEndElement();
			return true;
		}


		/// <summary>
		/// Loads the collection from an xmlnode
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			LastStringID = 0;

			Name = xml.Attributes["name"].Value;


			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "string":
					{
						Strings[int.Parse(node.Attributes["id"].Value)] = node.InnerText;

						if (int.Parse(node.Attributes["id"].Value) > LastStringID)
							LastStringID = int.Parse(node.Attributes["id"].Value);
					}
					break;
				}
			}

			return true;
		}


		#endregion



		#region Properties

		/// <summary>
		/// Language name
		/// </summary>
		public string Name;


		/// <summary>
		/// Current list of strings
		/// </summary>
		public Dictionary<int, string> Strings
		{
			get;
			private set;
		}


		/// <summary>
		/// Last inserted string id
		/// </summary>
		public int LastStringID
		{
			get;
			set;
		}


		#endregion
	}
}
