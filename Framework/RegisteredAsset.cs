#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Audio;
using ArcEngine.Interface;

namespace ArcEngine
{

	/// <summary>
	/// Registred asset definition
	/// </summary>
	public class RegisteredAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type">Type of the asset</param>
		/// <param name="tag">Tag's name</param>
		/// <param name="editor">Editor form type</param>
		public RegisteredAsset(Type type, string tag, Type editor)
		{
			if (type == null || string.IsNullOrEmpty(tag))
				throw new ArgumentNullException("type");

			Dictionary = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Shared = new Dictionary<string, IAsset>(StringComparer.OrdinalIgnoreCase);
			SharedCounter = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

			Type = type;
			Editor = editor;
			Tag = tag;
		}



		/// <summary>
		/// Runs the editor form of an asset
		/// </summary>
		/// <param name="name">Name of the asset</param>
		public AssetEditorBase Edit(string name)
		{
			if (Editor == null)
			{
				Trace.WriteDebugLine("[RegisteredAsset::Edit()] No editor defined for \"" + Tag + "\".");
				return null;
			}


			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			return Activator.CreateInstance(Editor, new object[]{ Get(name)}) as AssetEditorBase;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} - Count : {1} - Tag : {2}", Type.Name, Count, Tag);
		}


		#region IO routines


		/// <summary>
		/// Saves assets
		/// </summary>
		/// <param name="xml">XmlWriter handle</param>
		/// <returns>True on success</returns>
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			foreach (XmlNode node in Dictionary.Values)
				node.WriteTo(xml);

			return true;
		}


		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Attributes["name"] == null)
			{
				Trace.Write("[RegisteredAsset::Load()] Xml attribute \"Name\" is missing.");
				return false;
			}

			string tagname = xml.Name.ToLower();
			if (tagname != Tag)
			{
				Trace.Write("[RegisteredAsset::Load()] Xml tag name differs from expected tag. ");
				Trace.WriteLine("Got \"" + tagname + "\" - Expecting \"" + Tag + "\"");
				return false;
			}



			Dictionary[xml.Attributes["name"].Value] = xml;


			return true;
		}


		#endregion


		#region Assets

		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public void Add(string name, XmlNode node)
		{
			if (string.IsNullOrEmpty(name)) // || node == null)
				return;

			Dictionary[name] = node;
		}


		/// <summary>
		/// Erases all assets
		/// </summary>
		public void Clear()
		{
			Dictionary.Clear();
		}


		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <returns>asset's name array</returns>
		public List<string> GetList(List<string> list)
		{
			if (list == null)
				throw new ArgumentNullException("list");

			foreach (string key in Dictionary.Keys)
				list.Add(key);


			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a new asset
		/// </summary>
		/// <param name="name">Name of the asset</param>
		/// <returns>Asset handle or null</returns>
		public IAsset Create(string name)
		{

			if (!Dictionary.ContainsKey(name))
				return null;

			IAsset asset = Activator.CreateInstance(Type) as IAsset;
			asset.Load(Dictionary[name]);
			return asset;
		}


		/// <summary>
		/// Returns an asset definition
		/// </summary>
		/// <param name="name">Asset's name</param>
		/// <returns>Xml definition</returns>
		public XmlNode Get(string name)
		{

			if (Dictionary.ContainsKey(name))
				return Dictionary[name];

			return null;
		}


		/// <summary>
		/// Removes an asset by its name
		/// </summary>
		/// <param name="name">Name</param>
		public void Remove(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (Dictionary.ContainsKey(name))
				Dictionary.Remove(name);
		}



		#endregion


		#region Shared assets

		/// <summary>
		/// Adds an asset as Shared
		/// </summary>
		/// <param name="name">Name of the asset to register</param>
		/// <param name="asset">Asset's handle</param>
		public void AddShared(string name, IAsset asset)
		{
			if (string.IsNullOrEmpty(name) || asset == null)
				return;

			Shared[name] = asset;
			SharedCounter[name] = 0;
		}


		/// <summary>
		/// Creates a new shared asset
		/// </summary>
		/// <param name="sharename">New shared asset name</param>
		/// <param name="assetname">Asset name to load</param>
		/// <returns>Handle to the asset</returns>
		public IAsset CreateShared(string sharename, string assetname)
		{
			if (string.IsNullOrEmpty(sharename) ||string.IsNullOrEmpty(assetname))
				return null;


			// Asset already exist
			if (Shared.ContainsKey(sharename))
				return LockShared(sharename);

			// Create a new asset
			IAsset asset = Create(assetname);
			if (asset == null)
				return null;

			AddShared(sharename, asset);

			return LockShared(sharename);
		}

		/// <summary>
		/// Locks a shared asset.
		/// </summary>
		/// <param name="name">Shared name</param>
		/// <returns>Asset handle or null</returns>
		public IAsset LockShared(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			// If asset exists, returns it
			if (Shared.ContainsKey(name))
			{
				SharedCounter[name]++;
				return Shared[name];
			}

			// Create a new asset
			IAsset asset = Create(name);
			if (asset == null)
				return null;

			AddShared(name, asset);
			SharedCounter[name]++;

			return asset;
		}


		/// <summary>
		/// Releases a shared asset
		/// </summary>
		/// <param name="handle">Asset handle</param>
		public void UnlockShared(IAsset handle)
		{
			if (handle == null)
				return;

			// Asset not found
			if (!Shared.ContainsValue(handle))
				return;

			SharedCounter[handle.Name]--;

			// No more used, remove it
			if (SharedCounter[handle.Name] == 0)
			{
				Trace.WriteDebugLine("[RegisteredAsset] Flushing asset \"" + handle.Name + "\" of type \"" + Type.Name + "\"");
				Shared[handle.Name].Dispose();
				Shared.Remove(handle.Name);
			}
		}


		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public void ClearShared()
		{
			foreach (IAsset asset in Shared.Values)
			{
				if (asset != null)
					asset.Dispose();
			}

			Shared.Clear();
			SharedCounter.Clear();
		}



		#endregion


		#region Properties



		/// <summary>
		/// Number of known assets
		/// </summary>
		public int Count
		{
			get
			{
				return Dictionary.Count;
			}
		}


		/// <summary>
		/// Number of shared count
		/// </summary>
		public int SharedCount
		{
			get
			{
				return Shared.Count;
			}
		}


		/// <summary>
		/// Type of the asset
		/// </summary>
		public Type Type
		{
			get;
			private set;
		}


		/// <summary>
		/// Tag of the asset
		/// </summary>
		public string Tag
		{
			get;
			private set;
		}


		/// <summary>
		/// Asset dictionary
		/// </summary>
		Dictionary<string, XmlNode> Dictionary;


		/// <summary>
		/// Shared dictionary
		/// </summary>
		Dictionary<string, IAsset> Shared;


		/// <summary>
		/// Counter for shared assets
		/// </summary>
		Dictionary<string, int> SharedCounter;


		/// <summary>
		/// Editor form for the asset
		/// </summary>
		public Type Editor
		{
			get;
			private set;
		}


		#endregion
	}
}
