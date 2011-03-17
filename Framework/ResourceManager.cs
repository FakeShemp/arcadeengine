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
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Audio;
using ArcEngine.Editor;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Interface;
using ArcEngine.Storage;


namespace ArcEngine
{
	/// <summary>
	/// Resource manager
	/// </summary>
	static public class ResourceManager
	{

		#region Constructor


		/// <summary>
		/// Initializes static members of the ResourceManager class.
		/// </summary>
		static ResourceManager()
		{
			Trace.WriteDebugLine("[ResourceManager] Constructor()");

			BinaryLock = new object();

			StorageList = new List<StorageBase>();
			FailbackStorage = new FileSystemStorage(Directory.GetCurrentDirectory());


			RegisteredAssets = new List<RegisteredAsset>();
			RegisterAsset<TileSet>(typeof(TileSetForm));
			RegisterAsset<StringTable>(typeof(StringTableForm));
			RegisterAsset<Animation>(typeof(AnimationForm));
			RegisterAsset<Scene>(typeof(SceneForm));
			RegisterAsset<Layout>(typeof(LayoutForm));
			RegisterAsset<BitmapFont>(typeof(BitmapFontForm));
			RegisterAsset<Script>(typeof(ScriptForm));
			RegisterAsset<ScriptModel>(null);
			RegisterAsset<AudioSample>(typeof(AudioForm));
			RegisterAsset<InputScheme>(typeof(InputSchemeForm));
			RegisterAsset<Shader>(null);
		}



		#endregion


		/// <summary>
		/// Initialize each providers
		/// </summary>
		static public void Dispose()
		{
			Trace.WriteDebugLine("[ResourceManager] Dispose()");

			
			if (TileSet.InUse != null && TileSet.InUse.Count > 0)
			{
				Trace.WriteLine("{0} TileSet remaining...", TileSet.InUse.Count);
				foreach (TileSet ts in TileSet.InUse)
				{
					Trace.WriteLine("Tileset \"" + ts.Name + "\" (" + ts.Count + " lock(s)).");
				}
			}

			ClearSharedAssets();


			foreach (StorageBase storage in StorageList)
				storage.Dispose();
			StorageList.Clear();
		}


		/// <summary>
		/// Adds a storage to the manager
		/// </summary>
		/// <param name="storage"></param>
		static public void AddStorage(StorageBase storage)
		{
			if (storage == null)
				return;

			storage.Process();
			StorageList.Add(storage);
		}


		/// <summary>
		/// Gets an internal resource
		/// </summary>
		/// <param name="name">Name of the resource</param>
		/// <returns>Resource stream or null if not found</returns>
		/// <remarks>Don't forget to Dispose the stream !!</remarks>
		static public Stream GetInternalResource(string name)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			if (assembly == null)
				return null;

			List<string> list = new List<string>(assembly.GetManifestResourceNames());
			if (list.Contains(name))
				return assembly.GetManifestResourceStream(name);

			assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
				return null;
			list = new List<string>(assembly.GetManifestResourceNames());
			if (list.Contains(name))
				return assembly.GetManifestResourceStream(name);

			return null;
		}


		/// <summary>
		/// Loads a resource from storages
		/// </summary>
		/// <param name="filename">Name of the file to load</param>
		/// <returns>Resource stream or null if not found</returns>
		/// <remarks>Don't forget to Dispose the stream !!</remarks>
		static public Stream Load(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;

			foreach (StorageBase storage in StorageList)
			{
				Stream stream = storage.OpenFile(filename, FileAccess.Read);
				if (stream != null)
					return stream;
			}

			// Last chance...
			return FailbackStorage.OpenFile(filename, FileAccess.Read);
		}


		#region Providers
/*

		/// <summary>
		/// Adds a provider
		/// </summary>
		/// <param name="provider">The provider</param>
		static public void AddProvider(Provider provider)
		{
			if (provider == null)
				return;

			Trace.WriteDebugLine("[ResourceManager] AddProvider()");

			if (Providers.Contains(provider))
			{
				Trace.WriteLine("Provider \"{0}\" already present !!", provider.Name);
				return;
			}

			Trace.Write("Adding new provider : " + provider.ToString() + " {");

			// Adds the provider to the list
			Providers.Add(provider);

			// Register asset's tags
			foreach (string tag in provider.Tags)
			{
				RegistredTags[tag] = provider;
			}


			// Register assets
			foreach (Type type in provider.Assets)
			{
				AssetProviders[type] = provider;
				Trace.Write(type.ToString() + ", ");
			}

			Trace.WriteLine("}");


			provider.Init();
		}


		/// <summary>
		/// Returns a provider's instance
		/// </summary>
		/// <typeparam name="T">Type of the provider</typeparam>
		/// <returns>Instance or null</returns>
		static public T GetProvider<T>() where T : Provider
		{
			foreach (Provider provider in Providers)
			{
				if (provider is T)
					return (T)(object)provider;
			}

			return default(T);
		}


		/// <summary>
		/// Returns the provider of a tag
		/// </summary>
		/// <param name="tag">Xml tag</param>
		/// <returns>Provider or null</returns>
		static public Provider GetTagProvider(string tag)
		{
			if (string.IsNullOrEmpty(tag))
				return null;

			if (!RegistredTags.ContainsKey(tag))
				return null;

			return RegistredTags[tag];
		}


		/// <summary>
		/// Returns the <see cref="Provider"/> of an asset
		/// </summary>
		/// <param name="type">asset type</param>
		/// <returns>Provider of the asset or null</returns>
		static public Provider GetAssetProvider(Type type)
		{
			if (type == null)
				return null;

			if (AssetProviders.ContainsKey(type))
				return AssetProviders[type];

			return null;
		}


		/// <summary>
		/// Returns the <see cref="Provider"/> of an asset
		/// </summary>
		/// <param name="name">Name of the type</param>
		/// <returns>Provider of the asset or null</returns>
		static public Provider GetAssetProvider(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			foreach (KeyValuePair<Type, Provider> kvp in AssetProviders)
			{
				if (kvp.Key.Name == name)
					return kvp.Value;
			}


			return null;
		}


		/// <summary>
		/// Removes a specific provider
		/// </summary>
		/// <param name="provider">Provider to remove</param>
		static public void RemoveProvider(Provider provider)
		{
			if (provider == null)
				return;


			// Remove tags
			foreach (string tag in provider.Tags)
				RegistredTags.Remove(tag);


			// Remove provider
			Providers.Remove(provider);
		}
*/

		#endregion


		#region Assets


		/// <summary>
		/// Convert an asset to a XmlNode
		/// </summary>
		/// <param name="asset">Asset to convert</param>
		/// <returns>XmlNode</returns>
		static public XmlNode ConvertAsset(IAsset asset)
		{
			if (asset == null)
				return null;

			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				asset.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			return doc.DocumentElement;
		}


		/// <summary>
		/// Registers an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="editor">Type of the editor form</param>
		/// <returns>True on success</returns>
		static public bool RegisterAsset<T>(Type editor) where T : IAsset
		{
			return RegisterAsset<T>(editor, typeof(T).Name.ToLower());
		}


		/// <summary>
		/// Registers an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="editor">Type of the editor form</param>
		/// <param name="tag">Tag's name</param>
		/// <returns>True on success</returns>
		static public bool RegisterAsset<T>(Type editor, string tag) where T : IAsset
		{
			foreach (RegisteredAsset ra in RegisteredAssets)
			{
				if (ra.Type == typeof(T))
				{
					Trace.WriteDebugLine("Type of asset \"" + typeof(T).Name + "\" already registered !");
					return false;
				}

				if (ra.Tag == tag)
				{
					Trace.WriteDebugLine("Asset tag \"" + tag + "\" already registered !");
					return false;
				}
			}

			Trace.WriteDebugLine("Registering asset \"" + typeof(T).Name + "\" as tag \"" + tag + "\"");
			RegisteredAssets.Add(new RegisteredAsset(typeof(T), tag, editor));

			return true;
		}


		/// <summary>
		/// Unregister an asset definition
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		static public void UnregisterAsset<T>() where T : IAsset
		{
			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
					{
						RegisteredAssets.Remove(ra);
						return;
					}
				}

				//Provider provider = GetAssetProvider(typeof(T));
				//if (provider == null)
				//    return;

				//provider.Remove<T>();
			}
		}


		/// <summary>
		/// Finds the registered asset by its tag
		/// </summary>
		/// <param name="tag">Tag's name</param>
		/// <returns>Registered asset or null</returns>
		static public RegisteredAsset GetRegisteredByTag(string tag)
		{
			if (string.IsNullOrEmpty(tag))
				return null;

			foreach (RegisteredAsset ra in RegisteredAssets)
			{
				if (ra.Tag == tag)
					return ra;
			}

			return null;
		}


		/// <summary>
		/// Finds the registered asset by its type
		/// </summary>
		/// <param name="type">Type definition</param>
		/// <returns>Registered asset or null</returns>
		static public RegisteredAsset GetRegisteredByType(Type type)
		{
			if (type == null)
				return null;

			foreach (RegisteredAsset ra in RegisteredAssets)
			{
				if (ra.Type == type)
					return ra;
			}

			return null;
		}


		/// <summary>
		/// Adds an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml description of the asset</param>
		static public bool AddAsset<T>(string name, XmlNode node) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return false;

			Trace.WriteDebugLine("[ResourceManager] AddAsset (name = {0})", name);

			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
					{

						ra.Add(name, node);
						return true;
					}
				}
			}

			return false;
		}


		/// <summary>
		/// Adds an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="asset">Asset handle</param>
		static public void AddAsset<T>(string name, IAsset asset) where T : IAsset
		{
			XmlNode node = ConvertAsset(asset);
			AddAsset<T>(name, node);
		}


		/// <summary>
		/// Returns all assets of a type
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <returns>Assets</returns>
		static public XmlNode GetAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return null;

			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
						return ra.Get(name);
				}

				//if (!AssetProviders.ContainsKey(typeof(T)))
				//    throw new ArgumentException("Unknown asset type");

				//return AssetProviders[typeof(T)].Get<T>(name);
			}

			return null;
		}


		/// <summary>
		/// Creates an asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset name</param>
		/// <returns>The asset or null</returns>
		static public T CreateAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return default(T);

			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
						return (T)ra.Create(name);
				}

			}

			return default(T);
		}


		/// <summary>
		/// Removes an asset by name
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset</param>
		static public void RemoveAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return;

			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
						ra.Remove(name);
				}

				//if (!AssetProviders.ContainsKey(typeof(T)))
				//    throw new ArgumentException("Unknown asset type");

				//AssetProviders[typeof(T)].Remove<T>(name);
			}
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		static public void ClearAssets()
		{
			Trace.WriteDebugLine("[ResourceManager] ClearAssets()");


			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
					ra.Clear();
			}


			// Remove all storages
			foreach (StorageBase storage in StorageList)
				storage.Dispose();
			StorageList.Clear();
		}


		/// <summary>
		/// Returns a list of all availabe asset
		/// </summary>
		/// <typeparam name="T">Asset's type</typeparam>
		/// <returns>Sorted list of available asset</returns>
		static public List<string> GetAssets<T>() where T : IAsset
		{
			List<string> list = new List<string>();
			
			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
						return ra.GetList(list);
				}

				//if (!AssetProviders.ContainsKey(typeof(T)))
				//    return new List<string>();

				//return AssetProviders[typeof(T)].GetAssets<T>();

			}

			return list;
		}

		#endregion


		#region Shared assets


		/// <summary>
		/// Creates and retruns a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset name</param>
		/// <returns>Handle to the asset</returns>
		static public T LockSharedAsset<T>(string name) where T : IAsset
		{
			// Bad name
			if (string.IsNullOrEmpty(name))
				return default(T);

			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
						return (T) ra.LockShared(name);
				}
			}

			return default(T);
		}


		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="handle">Handle of the asset</param>
		static public void UnlockSharedAsset<T>(IAsset handle) where T : IAsset
		{
			if (handle == null)
				return;

			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
					{
						ra.UnlockShared(handle);
						return;
					}
				}

			}
		}


		/// <summary>
		/// Creates and loads a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">New shared asset name</param>
		/// <param name="asset">Asset name to load</param>
		/// <returns>Handle to the asset</returns>
		static public T CreateSharedAsset<T>(string name, string asset) where T : IAsset
		{
			foreach (RegisteredAsset ra in RegisteredAssets)
			{
				if (ra.Type == typeof(T))
				{
					return (T) ra.CreateShared(name, asset);
				}
			}

			return default(T);
		}


		/// <summary>
		/// Adds an asset to the shared list
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="asset">Asset to add</param>
		static public void AddSharedAsset<T>(string name, T asset) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return;

			lock (BinaryLock)
			{

				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
					{
						ra.AddShared(name, asset);
						return;
					}
				}


				//if (!AssetProviders.ContainsKey(typeof(T)))
				//    throw new ArgumentException("Unknown asset type");

				//AssetProviders[typeof(T)].AddShared<T>(name, asset);
			}

		}


		/// <summary>
		/// Removes a specific shared asset type
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		static public void RemoveSharedAsset<T>() where T : IAsset
		{
			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
				{
					if (ra.Type == typeof(T))
						ra.ClearShared();
				}
			}
		}


		/// <summary>
		/// Removes all shared assets
		/// </summary>
		static public void ClearSharedAssets()
		{
			lock (BinaryLock)
			{
				foreach (RegisteredAsset ra in RegisteredAssets)
					ra.ClearShared();
			}
		}




		#endregion


		#region Asset operations

		/*		
		/// <summary>
		/// Adds an asset from a file in a bank
		/// </summary>
		/// <param name="bankname">Bank's name</param>
		/// <param name="assetname">Asset name in the bank</param>
		/// <param name="filename">File to add in the bank</param>
		/// <returns>True on success</returns>
		static public bool SaveAsset(string bankname, string assetname, string filename)
		{
			if (string.IsNullOrEmpty(bankname) || string.IsNullOrEmpty(assetname) || string.IsNullOrEmpty(filename))
				return false;

			using (Stream stream = new FileStream(filename, FileMode.Open))
				return SaveAsset(bankname, assetname, stream);
		}


		/// <summary>
		/// Adds an asset from a stream in a bank
		/// </summary>
		/// <param name="bankname">Bank's name</param>
		/// <param name="assetname">Asset name in the bank</param>
		/// <param name="stream">Stream to add in the bank</param>
		/// <returns>True on success</returns>
		static public bool SaveAsset(string bankname, string assetname, Stream stream)
		{
			if (string.IsNullOrEmpty(bankname) || string.IsNullOrEmpty(assetname) || stream == null)
				return false;

			Trace.Write("Saving asset \"{0}\" to bank \"{1}\"... ", assetname, bankname);

			try
			{
				// Open bank
				ZipStorer zip = ZipStorer.Open(bankname, FileAccess.Write);

				// Get a listing of all present files
				List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

				// Remove duplicate name before insert
				List<ZipStorer.ZipFileEntry> remove = new List<ZipStorer.ZipFileEntry>();
				foreach (ZipStorer.ZipFileEntry ent in dir)
				{
					if (ent.FilenameInZip == assetname)
						remove.Add(ent);
				}
				ZipStorer.RemoveEntries(ref zip, remove);


				// Add stream
				zip.AddStream(ZipStorer.Compression.Deflate, assetname, stream, DateTime.Now, string.Empty);


				// Close the file
				zip.Close();

				Trace.WriteLine("Done !");
			}
			catch (Exception e)
			{
				Trace.WriteLine("Failed !");
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}


			return true;
		}

*/


		/// <summary>
		/// Saves all assets to a storage
		/// </summary>
		/// <param name="storage">Storage bank</param>
		/// <returns>True on success</returns>
		static public bool SaveAssetsToStorage(StorageBase storage)
		{
			if (storage == null)
				return false;

			Trace.WriteLine("[ResourceManager] : Saving all resources to storage \"" + storage.ToString() + "\"...");

			try
			{
				// Xml settings
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.OmitXmlDeclaration = false;
				settings.IndentChars = "\t";
				settings.Encoding = ASCIIEncoding.ASCII;

				// For each Provider
				//foreach (Provider provider in Providers)
				{
					// for each registred asset
					//foreach (Type type in provider.Assets)
					foreach(RegisteredAsset ra in RegisteredAssets)
					{
						// Get the number of asset
						//MethodInfo mi = provider.GetType().GetMethod("Count").MakeGenericMethod(type);
						//if ((int)mi.Invoke(provider, null) == 0)
						//    continue;
						if (ra.Count == 0)
							continue;

						// Save to storage
						using (Stream stream = storage.CreateFile(string.Format("{0}.xml", ra.Type.Name)))
						{
							if (stream == null)
								continue;

							// Create Xml document from storage stream
							XmlWriter doc = XmlWriter.Create(stream, settings);
							doc.WriteStartDocument(true);
							doc.WriteStartElement("bank");

							// Invoke the generic method like this : provider.Save<[Asset Type]>(XmlNode node);
							//mi = provider.GetType().GetMethod("Save", new Type[] { typeof(XmlWriter) }).MakeGenericMethod(type);
							//mi.Invoke(provider, new object[] { doc });
							ra.Save(doc);

							// Close xml 
							doc.WriteEndElement();
							doc.WriteEndDocument();
							doc.Flush();
						}
					}
				}

				storage.Flush();
				return true;
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}

			return false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Returns a list of all internal resources
		/// </summary>
		/// <returns></returns>
		static public List<string> InternalResources
		{
			get
			{
				List<string> list = new List<string>(Assembly.GetExecutingAssembly().GetManifestResourceNames());

				list.AddRange(Assembly.GetEntryAssembly().GetManifestResourceNames());

				list.Sort();
				return list;
			}
		}


		/// <summary>
		/// Registered assets
		/// </summary>
		public static List<RegisteredAsset> RegisteredAssets
		{
			get;
			private set;
		}


		/// <summary>
		/// Object used for thread locking
		/// </summary>
		static object BinaryLock;


		/// <summary>
		/// Storages
		/// </summary>
		static List<StorageBase> StorageList;


		/// <summary>
		/// Available storage bases
		/// </summary>
		public static StorageBase[] Storages
		{
			get
			{
				return StorageList.ToArray();
			}
		}


		/// <summary>
		/// Failback storage
		/// </summary>
		static FileSystemStorage FailbackStorage;


		/// <summary>
		/// Gets or sets the root directory
		/// </summary>
		static public string RootDirectory
		{
			get
			{
				return FailbackStorage.RootDirectory;
			}
			set
			{
				FailbackStorage.RootDirectory = value;
			}
		}

		#endregion

	}

}
