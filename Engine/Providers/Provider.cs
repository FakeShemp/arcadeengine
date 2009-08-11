using ArcEngine.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Editor;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Providers
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
			Version = new Version();
		}

		/// <summary>
		/// Checks if <paramref name="T"/> is a registred asset type, and if <paramref name="name"/> is not null or empty
		/// </summary>
		/// <typeparam name="T">Type to check</typeparam>
		/// <param name="name"><c>string</c> to check</param>
		protected void CheckValue<T>(string name)
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


		#region Editor

		/// <summary>
		/// Edits an asset
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public virtual AssetEditor EditAsset<T>(string name)
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
		public abstract void Remove<T>(string name);




		/// <summary>
		/// Returns an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>Definition of an asset</returns>
		public abstract XmlNode Get<T>(string name);



		/// <summary>
		/// Creates a resource from an asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>The resource or null if asset not found</returns>
		public abstract T Create<T>(string name);

	
		/// <summary>
		/// Returns a list of available assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>List of available assets</returns>
		public abstract List<string> GetAssets<T>();


		/// <summary>
		/// Removes a specific type of assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public abstract void Remove<T>();


		/// <summary>
		/// Erases all assets from the provider
		/// </summary>
		public abstract void Clear();


		#endregion



		#region Shared assets


		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public abstract T CreateShared<T>(string name);



		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public abstract void RemoveShared<T>(string name);



		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public abstract void RemoveShared<T>();


		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public abstract void ClearShared();


		#endregion



		#region IO routines

		/// <summary>
		/// Saves assets to a xml file
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public virtual bool Save(Type type, XmlWriter xml)
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
