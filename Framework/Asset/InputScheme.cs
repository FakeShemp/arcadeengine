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
using System.Xml;
using System.Windows.Forms;
using System.Reflection;

namespace ArcEngine.Asset
{

	/// <summary>
	/// Input scheme.
	/// Allow an abstract between an input from any device and a name.
	/// </summary>
	public class InputScheme : IAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public InputScheme()
		{
			InputList = new Dictionary<string, Keys>();
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
		/// Adds a new input
		/// </summary>
		/// <param name="name">Name of the input</param>
		/// <param name="key">Key</param>
		public void AddInput(string name, Keys key)
		{
			if (string.IsNullOrEmpty(name))
				return;

			InputList[name] = key;
		}


		/// <summary>
		/// Remove an input
		/// </summary>
		/// <param name="name">Name of the input</param>
		public void RemoveInput(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (InputList.ContainsKey(name))
				InputList.Remove(name);
		}



		#region IO

		/// <summary>
		/// Load input scheme
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
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "input":
					{
						AddInput(node.Attributes["name"].Value, (Keys)Enum.Parse(typeof(Keys), node.Attributes["key"].Value));
					}
					break;

				}


			}

			return true;
		}



		/// <summary>
		/// Save input scheme
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement(XmlTag);
			writer.WriteAttributeString("name", Name);


			foreach (KeyValuePair<string, Keys> kvp in InputList)
			{
				writer.WriteStartElement("input");
				writer.WriteAttributeString("name", kvp.Key);
				writer.WriteAttributeString("key", kvp.Value.ToString());
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			return true;
		}

		#endregion


		/// <summary>
		/// Defaults all inputs
		/// </summary>
		public void Clear()
		{
			InputList.Clear();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Keys this[string name]
		{
			get
			{
				return InputList[name];
			}
			set
			{
				InputList[name] = value;
			}
		}


		#region Properties


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "inputscheme";
			}
		}


		/// <summary>
		/// Gets a sorted list of all available inputs
		/// </summary>
		public List<string> Inputs
		{
			get
			{
				List<string> list = new List<string>();

				foreach (string name in InputList.Keys)
					list.Add(name);

				list.Sort();
				return list;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		Dictionary<string, Keys> InputList;


		/// <summary>
		/// Name of the Scheme
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		#endregion
	}




	/// <summary>
	/// all type of input
	/// </summary>
	public enum InputDevice
	{
		/// <summary>
		/// Input from the Keyboard
		/// </summary>
		Keyboard,

		/// <summary>
		/// Input from the Mouse
		/// </summary>
		Mouse,

		/// <summary>
		/// Input from the Gamepad
		/// </summary>
		Gamepad
	}



}



