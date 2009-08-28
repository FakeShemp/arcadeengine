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

using ArcEngine.Forms;
using ArcEngine.Graphic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Providers
{

	/// <summary>
	/// Script provider
	/// </summary>
	public class ScriptProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public ScriptProvider()
		{
			Scripts = new Dictionary<string, XmlNode>();
			SharedScripts = new Dictionary<string, Script>();
			Models = new Dictionary<string, XmlNode>();

			Name = "Script";
			Tags = new string[] { "script", "scriptmodel" };
			Assets = new Type[] { typeof(Script), typeof(ScriptModel) };
			Version = new Version(0, 1);

			EditorImage = new Bitmap(ResourceManager.GetInternalResource("ArcEngine.Data.Icons.Script.png"));
	
		}


		#region IO routines


		/// <summary>
		/// Saves all scripts
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{
			if (type == typeof(Script))
			{
				foreach (XmlNode node in Scripts.Values)
					node.WriteTo(xml);
			}
			else if (type == typeof(ScriptModel))
			{
				foreach (XmlNode node in Models.Values)
					node.WriteTo(xml);
			}


			return true;

		}




		/// <summary>
		/// Loads a script
		/// </summary>
		/// <param name="xml"></param>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;
			

			switch (xml.Name.ToLower())
			{
				case "script":
				{
					string name = xml.Attributes["name"].Value;
					Scripts[name] = xml;
				}
				break;

				case "scriptmodel":
				{
					string name = xml.Attributes["name"].Value;
					Models[name] = xml;
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
			AssetEditor form = null;

			if (typeof(T) == typeof(Script))
			{
				XmlNode node = null;
				if (Scripts.ContainsKey(name))
					node = Scripts[name];
				form = new ArcEngine.Editor.ScriptForm(node);
				form.TabText = name;
			}
			else if (typeof(T) == typeof(ScriptModel))
			{
				XmlNode node = null;
				if (Models.ContainsKey(name))
					node = Models[name];
				form = new ArcEngine.Editor.ScriptModelForm(node);
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

			if (typeof(T) == typeof(Script))
				Scripts[name] = node;
			else if (typeof(T) == typeof(ScriptModel))
				Models[name] = node;
		}


		/// <summary>
		/// Returns an array of all available scripts
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Script's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			if (typeof(T) == typeof(Script))
			{
				foreach (string key in Scripts.Keys)
					list.Add(key);
			}
			else if (typeof(T) == typeof(ScriptModel))
			{
				foreach (string key in Models.Keys)
					list.Add(key);
			}
			list.Sort();

			return list;
		}




		/// <summary>
		/// Creates a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Script))
			{
				if (!Scripts.ContainsKey(name))
					return default(T);

				Script script = new Script();
				script.Load(Scripts[name]);
				return (T)(object)script;
			}
			else if (typeof(T) == typeof(ScriptModel))
			{
				if (!Models.ContainsKey(name))
					return default(T);

				ScriptModel model = new ScriptModel();
				model.Load(Models[name]);
				return (T)(object)model;
			}

			return default(T);
		}



		/// <summary>
		/// Returns a <c>Script</c>
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Script))
			{
				if (!Scripts.ContainsKey(name))
					return null;

				return Scripts[name];
			}
			else if (typeof(T) == typeof(ScriptModel))
			{
				if (!Models.ContainsKey(name))
					return null;

				return Models[name];
			}

			return null;
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
			Scripts.Clear();
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
			if (typeof(T) == typeof(Script))
			{
				if (SharedScripts.ContainsKey(name))
					return (T)(object)SharedScripts[name];

				Script script = new Script();
				SharedScripts[name] = script;

				return (T)(object)script;
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
			if (typeof(T) == typeof(Script))
			{
				SharedScripts[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(Script))
			{
				SharedScripts.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedScripts.Clear();
		}



		#endregion


		#region Progerties


		/// <summary>
		/// Scripts
		/// </summary>
		Dictionary<string, XmlNode> Scripts;

		/// <summary>
		/// Shared scripts
		/// </summary>
		Dictionary<string, Script> SharedScripts;


		/// <summary>
		/// Script models
		/// </summary>
		public Dictionary<string, XmlNode> Models;


		#endregion
	}
}
