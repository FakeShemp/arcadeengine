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
using ArcEngine.Graphic;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Forms;

namespace ArcEngine.Providers
{
	/// <summary>
	/// 
	/// </summary>
	public class KeyboardSchemeProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public KeyboardSchemeProvider()
		{
			Schemes = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedSchemes = new Dictionary<string, KeyboardScheme>(StringComparer.OrdinalIgnoreCase);

			Name = "KeyboardScheme";
			Tags = new string[] {"keyboardscheme" };
			Assets = new Type[] { typeof(KeyboardScheme) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetResource("ArcEngine.Data.Icons.Font.png"));

		}



		#region Init & Close


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			return false;
		}



		/// <summary>
		/// Close all opened resources
		/// </summary>
		public override void Close()
		{

		}

		#endregion


		#region IO routines


		/// <summary>
		/// Saves all textures
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{
			if (typeof(T) == typeof(KeyboardScheme))
			{
				foreach (XmlNode node in Schemes.Values)
					node.WriteTo(xml);
			}


			return true;
		}



		/// <summary>
		/// Loads a texture
		/// </summary>
		public override bool Load(XmlNode xml)
		{

			if (xml == null)
				return false;

			switch (xml.Name.ToLower())
			{
				case "keyboardscheme":
				{

					string name = xml.Attributes["name"].Value;
					Schemes[name] = xml;

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
			AssetEditor form = null;

			if (typeof(T) == typeof(KeyboardScheme))
			{
				XmlNode node = null;
				if (Schemes.ContainsKey(name))
					node = Schemes[name];
				form = new ArcEngine.Editor.KeyboardSchemeForm(node);
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

			if (typeof(T) == typeof(KeyboardScheme))
				Schemes[name] = node;
		}


		/// <summary>
		/// Returns an array of all available schemes
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>List of assets</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(KeyboardScheme))
				foreach (string key in Schemes.Keys)
					list.Add(key);


			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a Keyboardscheme asset
		/// </summary>
		/// <typeparam name="T">Type of asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);


			if (typeof(T) == typeof(KeyboardScheme) && Schemes.ContainsKey(name))
			{
				KeyboardScheme scheme = new KeyboardScheme();
				scheme.Load(Schemes[name]);
				return (T)(object)scheme;
			}

			return default(T);
		}



		/// <summary>
		/// Returns a <c>Font</c> definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(KeyboardScheme) && Schemes.ContainsKey(name))
				return Schemes[name];

			return null;
		}




		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(KeyboardScheme) && Schemes.ContainsKey(name))
				Schemes.Remove(name);
		}




		/// <summary>
		/// Removes a font
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
			if (typeof(T) == typeof(KeyboardScheme))
				Schemes.Clear();
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Schemes.Clear();
		}


		/// <summary>
		/// Returns the number of known assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Number of available asset</returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(KeyboardScheme))
				return Schemes.Count;

			return 0;
		}


		#endregion


		#region Shared assets

		/// <summary>
		/// Adds an asset as Shared
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset to register</param>
		/// <param name="asset">Asset's handle</param>
		public override void AddShared<T>(string name, IAsset asset)
		{
			if (string.IsNullOrEmpty(name))
				return;
			
			if (typeof(T) == typeof(KeyboardScheme))
				SharedSchemes[name] = asset as KeyboardScheme;
		}

		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(KeyboardScheme))
			{
				if (SharedSchemes.ContainsKey(name))
					return (T)(object)SharedSchemes[name];

				KeyboardScheme scheme = new KeyboardScheme();
				SharedSchemes[name] = scheme;

				return (T)(object)scheme;
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
			if (string.IsNullOrEmpty(name))
				return;

			if (typeof(T) == typeof(KeyboardScheme))
				SharedSchemes.Remove(name);
		}




		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(KeyboardScheme))
			{
				SharedSchemes.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedSchemes.Clear();
		}



		#endregion


		#region Properties

		/// <summary>
		/// KeyboarScheme definition
		/// </summary>
		Dictionary<string, XmlNode> Schemes;


		/// <summary>
		/// Shared fonts
		/// </summary>
		Dictionary<string, KeyboardScheme> SharedSchemes;

		#endregion
	}

}

