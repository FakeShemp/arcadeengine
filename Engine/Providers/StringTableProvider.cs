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
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Graphic;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Providers
{

	/// <summary>
	/// Script provider
	/// </summary>
	public class StringTableProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public StringTableProvider()
		{
			Strings = new Dictionary<string, XmlNode>();
			SharedStrings = new Dictionary<string, StringTable>();


			Name = "StringTable";
			Tags = new string[] { "stringtable" };
			Assets = new Type[] { typeof(StringTable) };
			Version = new Version(0, 1);

			EditorImage = new Bitmap(ResourceManager.GetInternalResource("ArcEngine.Data.Icons.StringTable.png"));
		}



		#region IO routines


		/// <summary>
		/// Saves all assets
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{
			if (type == typeof(StringTable))
			{
				foreach (XmlNode node in Strings.Values)
					node.WriteTo(xml);
			}

			return true;
		}




		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml"></param>
		public override bool Load(XmlNode xml)
		{

			switch (xml.Name.ToLower())
			{
				case "stringtable":
				{

					string name = xml.Attributes["name"].Value;
					Strings[name] = xml;
				}
				break;

				default:
				{

				}
				break;
			}

			return true;
		}



		#endregion


		#region Editor


		/// <summary>
		/// Edits an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override AssetEditor EditAsset<T>(string name)
		{
			//DockContent form = null;
			AssetEditor form = null;

			if (typeof(T) == typeof(StringTable))
			{
				XmlNode node = null;
				if (Strings.ContainsKey(name))
					node = Strings[name];
				form = new ArcEngine.Editor.StringTableForm(node);
				form.TabText = name;
			}

			return form;
		}


		#endregion


		#region Assets

		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public override void Add<T>(string name, XmlNode node)
		{
			CheckValue<T>(name);
			Strings[name] = node;
		}


		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>asset's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			if (typeof(T) == typeof(StringTable))
			{
				foreach (string key in Strings.Keys)
				{
					list.Add(key);
				}
			}

			list.Sort();
			return list;
		}



		/// <summary>
		/// Creates an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (!Strings.ContainsKey(name))
				return default(T);

			// Creates a Texture
			StringTable str = new StringTable();
			str.Load(Strings[name]);

			return (T)(object)str;
		}



		/// <summary>
		/// Returns a <c>StringTable</c>
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (!Strings.ContainsKey(name))
				return null;

			return Strings[name];
		}




		/// <summary>
		/// Removes a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
		}



		/// <summary>
		/// Removes a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
		}



		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Strings.Clear();
		}

		#endregion


		#region Shared assets


		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(StringTable))
			{
				if (SharedStrings.ContainsKey(name))
					return (T)(object)SharedStrings[name];

				StringTable st = new StringTable();
				SharedStrings[name] = st;

				st.Load(Strings[name]);		

				return (T)(object)st;
			}

			return default(T);
		}



		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public override void RemoveShared<T>(string name)
		{
			if (typeof(T) == typeof(StringTable))
			{
				SharedStrings[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(StringTable))
			{
				SharedStrings.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedStrings.Clear();
		}



		#endregion


		#region Progerties

		/// <summary>
		/// Scripts
		/// </summary>
		Dictionary<string, XmlNode> Strings;


		/// <summary>
		/// Schared strings
		/// </summary>
		Dictionary<string, StringTable> SharedStrings;

		#endregion
	}
}
