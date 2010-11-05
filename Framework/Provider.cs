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
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Interface;

namespace ArcEngine
{
	/// <summary>
	/// Provider interface 
	/// </summary>
	public abstract class Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Provider()
		{
			//Version = new Version();
		}

		/// <summary>
		/// Checks if <typeparamref name="T"/> is a registred asset type, and if <paramref name="name"/> is not null or empty
		/// </summary>
		/// <typeparam name="T">Type to check</typeparam>
		/// <param name="name"><c>string</c> to check</param>
		protected void CheckValue<T>(string name)  where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			foreach (Type type in Assets)
			{
				if (type == typeof(T))
					return;
			}

			throw new ArgumentException("Unknown asset type", "T");
		}



		#region Initialization


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public abstract bool Init();



		/// <summary>
		/// Close all opened resources
		/// </summary>
		public abstract void Dispose();

		#endregion



		#region Editor

		/// <summary>
		/// Edits an asset
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public virtual AssetEditorBase EditAsset<T>(string name) where T : IAsset
		{
			return null;
		}


		#endregion



		#region Asset management


		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public abstract void Add<T>(string name, XmlNode node) where T : IAsset;



		/// <summary>
		/// Removes a specific asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public abstract void Remove<T>(string name) where T : IAsset;


		/// <summary>
		/// Returns an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>Definition of an asset</returns>
		public abstract XmlNode Get<T>(string name) where T : IAsset;


		/// <summary>
		/// Creates a resource from an asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>The resource or null if asset not found</returns>
		public abstract T Create<T>(string name) where T : IAsset;

	
		/// <summary>
		/// Returns a list of available assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>List of available assets</returns>
		public abstract List<string> GetAssets<T>() where T : IAsset;

		
		/// <summary>
		/// Removes a specific type of assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public abstract void Remove<T>() where T : IAsset;


		/// <summary>
		/// Erases all assets from the provider
		/// </summary>
		public abstract void Clear();


		/// <summary>
		/// Returns the number of known assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Number of available asset</returns>
		public abstract int Count<T>() where T : IAsset;

		#endregion



		#region Shared assets


		/// <summary>
		/// Adds an asset as Shared
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset to register</param>
		/// <param name="asset">Asset's handle</param>
		public abstract void AddShared<T>(string name, IAsset asset) where T : IAsset;


		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public abstract T GetShared<T>(string name) where T : IAsset;



		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public abstract void RemoveShared<T>(string name) where T : IAsset;



		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public abstract void RemoveShared<T>() where T : IAsset;


		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public abstract void ClearShared();


		#endregion



		#region IO routines

		/// <summary>
		/// Saves assets to a xml file
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <returns></returns>
		public virtual bool Save<T>(XmlWriter xml)
		{

			return false;
		}


		/// <summary>
		/// Loads assets from a xml node
		/// </summary>
		public virtual bool Load(XmlNode xml)
		{

			return false;
		}


		#endregion



		#region Properties

		/// <summary>
		/// Display name of the asset
		/// </summary>
		/// <remarks>This is also used in the bank file as the name of the file asset</remarks>
		public string Name
		{
			get;
			protected set;
		}


		/// <summary>
		/// Handled XML tags
		/// </summary>
		public string[] Tags
		{
			get;
			protected set;
		}


		/// <summary>
		/// Managed assets types
		/// </summary>
		public Type[] Assets
		{
			get;
			protected set;
		}


		/// <summary>
		/// Version of the provider
		/// </summary>
		public Version Version
		{
			get;
			protected set;
		}


		/// <summary>
		/// Image displayed in the editor
		/// </summary>
		public Image EditorImage
		{
			get;
			protected set;
		}

		#endregion


	}
}
