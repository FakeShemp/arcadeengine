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
using ArcEngine.Asset;

namespace ArcEngine
{

	/// <summary>
	/// Keyboard scheme
	/// </summary>
	public class KeyboardScheme : IAsset
	{

		/// <summary>
		/// 
		/// </summary>
		public KeyboardScheme()
		{
			Inputs = new Dictionary<string, Keys>();
		}


		/// <summary>
		/// Load input scheme
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					// Level
					case "input":
					{
						Inputs[node.Attributes["name"].Value] = (Keys)Enum.Parse(typeof(Keys), node.Attributes["key"].Value);
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
			return false;
		}


		/// <summary>
		/// Defaults all inputs
		/// </summary>
		public void Reset()
		{
			Inputs.Clear();
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
				return Inputs[name];
			}
			set
			{
				Inputs[name] = value;
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
				return "keyboardscheme";
			}
		}


		/// <summary>
		/// 
		/// </summary>
		Dictionary<string, Keys> Inputs;


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
}
