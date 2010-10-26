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
using ArcEngine.Interface;
using ArcEngine.Storage;
//
//
//
//
//

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

			AssetProviders = new Dictionary<Type, Provider>();
			Providers = new List<Provider>();
			RegistredTags = new Dictionary<string, Provider>();
			Storages = new List<StorageBase>();
			FailbackStorage = new FileSystemStorage(Directory.GetCurrentDirectory());

			AddProvider(new Providers());
		}



		#endregion


		/// <summary>
		/// Initialize each providers
		/// </summary>
		static public void Dispose()
		{
			Trace.WriteDebugLine("[ResourceManager] Dispose()");
			
			foreach (Provider provider in Providers)
				provider.Dispose();
			Providers.Clear();

			foreach (StorageBase storage in Storages)
				storage.Dispose();
			Storages.Clear();

		}


		/// <summary>
		/// Gets an internal resource
		/// </summary>
		/// <param name="name">Name of the resource</param>
		/// <returns>Resource stream or null if not found</returns>
		/// <remarks>Don't forget to Dispose the stream !!</remarks>
		static public Stream GetInternalResource(string name)
		{
			List<string> list = new List<string>(Assembly.GetExecutingAssembly().GetManifestResourceNames());
			if (list.Contains(name))
				return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

			list = new List<string>(Assembly.GetEntryAssembly().GetManifestResourceNames());
			if (list.Contains(name))
				return Assembly.GetEntryAssembly().GetManifestResourceStream(name);

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

			foreach (StorageBase storage in Storages)
			{
				Stream stream = storage.OpenFile(filename);
				if (stream != null)
					return stream;
			}

			// Last chance...
			return FailbackStorage.OpenFile(filename);
		}

	
		#region Providers


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
		/// Adds an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml description of the asset</param>
		static public void AddAsset<T>(string name, XmlNode node) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return;

			Trace.WriteDebugLine("[ResourceManager] AddAsset (name = {0})", name);

			lock (BinaryLock)
			{
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				AssetProviders[typeof(T)].Add<T>(name, node);
			}
		}


		/// <summary>
		/// Adds an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="asset">Asset handle</param>
		static public void AddAsset<T>(string name, IAsset asset) where T : IAsset
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				asset.Save(writer);

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(sb.ToString());

			AddAsset<T>(name, doc.DocumentElement);
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
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				return AssetProviders[typeof(T)].Get<T>(name);
			}
		}


		/// <summary>
		/// Creates an asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">ASset name</param>
		/// <returns>The asset or null</returns>
		static public T CreateAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return default(T);

			lock (BinaryLock)
			{
				// Unknown asset type
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				// Create the asset
				return (T)AssetProviders[typeof(T)].Create<T>(name);
			}
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
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				AssetProviders[typeof(T)].Remove<T>(name);
			}
		}


		/// <summary>
		/// Removes a specific asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		static public void RemoveAsset<T>() where T : IAsset
		{
			lock (BinaryLock)
			{
				Provider provider = GetAssetProvider(typeof(T));
				if (provider == null)
					return;

				provider.Remove<T>();
			}
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		static public void ClearAssets()
		{
			Trace.WriteDebugLine("[ResourceManager] ClearAssets");


			lock (BinaryLock)
			{
				foreach (Provider provider in Providers)
					provider.Clear();

				//BankName = null;
				//BankPassword = null;

				Trace.WriteLine("Clearing assets !");
			}
		}


		/// <summary>
		/// Returns a list of all availabe asset
		/// </summary>
		/// <typeparam name="T">Asset's type</typeparam>
		/// <returns>Sorted list of available asset</returns>
		static public List<string> GetAssets<T>() where T : IAsset
		{
			lock (BinaryLock)
			{
				if (!AssetProviders.ContainsKey(typeof(T)))
					return new List<string>();

				return AssetProviders[typeof(T)].GetAssets<T>();
			}
		}

		#endregion


		#region Shared assets

		/// <summary>
		/// Creates and retruns a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset name</param>
		/// <returns>Handle to the asset</returns>
		static public T CreateSharedAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return default(T);

			lock (BinaryLock)
			{
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				T asset = AssetProviders[typeof(T)].CreateShared<T>(name);
				if (asset.IsDisposed)
				{
					RemoveSharedAsset<T>(name);
					return CreateSharedAsset<T>(name);
				}

				return asset;
			}
		}


		/// <summary>
		/// Creates and loads a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">New asset name</param>
		/// <param name="asset">Asset to load</param>
		/// <returns>Handle to the asset</returns>
		static public T CreateSharedAsset<T>(string name, string asset) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return default(T);

			// Create the asset
			T tmp = CreateAsset<T>(asset);
			if (tmp == null)
				return default(T);

			// Add the asset to the shared list
			AddSharedAsset<T>(name, tmp);

			return tmp;
		}


		/// <summary>
		/// Adds an asset to the Shared List
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
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				AssetProviders[typeof(T)].AddShared<T>(name, asset);
			}

		}


		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset</param>
		static public void RemoveSharedAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return;

			lock (BinaryLock)
			{
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				AssetProviders[typeof(T)].RemoveShared<T>(name);
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
				Provider provider = GetAssetProvider(typeof(T));
				if (provider == null)
					return;

				provider.RemoveShared<T>();
			}
		}


		/// <summary>
		/// Removes all shared assets
		/// </summary>
		static public void ClearSharedAssets()
		{
			lock (BinaryLock)
			{
				foreach (Provider provider in Providers)
					provider.ClearShared();
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
		/// Saves all assets to a bank file
		/// </summary>
		/// <param name="bankname">The file name of the bank on disk</param>
		/// <param name="password">Password</param>
		/// <returns>True on success</returns>
		static public bool SaveAssetsToBank(string bankname, string password)
		{
			// Return value
			bool retval = false;

			Trace.WriteLine("Saving all resources to bank \"" + bankname + "\"...");


			try
			{
				ZipStorer zip = ZipStorer.Open(bankname, FileAccess.Write);
				List<ZipFileEntry> dir = zip.ReadCentralDir();

				// Save all providers
				XmlWriter doc;
				MemoryStream ms;


				// Xml settings
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.OmitXmlDeclaration = false;
				settings.IndentChars = "\t";
				settings.Encoding = ASCIIEncoding.ASCII;

				Dictionary<string, Stream> streams = new Dictionary<string, Stream>();
				List<ZipFileEntry> remove = new List<ZipFileEntry>();

				// For each Provider
				foreach (Provider provider in Providers)
				{
					// for each registred asset
					foreach (Type type in provider.Assets)
					{
						// Get the number of asset
						MethodInfo mi = provider.GetType().GetMethod("Count").MakeGenericMethod(type);
						int count = (int) mi.Invoke(provider, null);
						if (count == 0)
							continue;

						// Create Xml document from a memory stream
						ms = new MemoryStream();
						doc = XmlWriter.Create(ms, settings);
						doc.WriteStartDocument(true);
						doc.WriteStartElement("bank");

						// Invoke the generic method like this : provider.Save<[Asset Type]>(XmlNode node);
						object[] args = { doc };
						Type[] types = new Type[] { typeof(XmlWriter) };
						mi = provider.GetType().GetMethod("Save", types).MakeGenericMethod(type);
						mi.Invoke(provider, args);

						// Close xml 
						doc.WriteEndElement();
						doc.WriteEndDocument();
						doc.Flush();

						// Rewind
						ms.Seek(0, SeekOrigin.Begin);

						string zipfilename = string.Format("{0}.xml", type.Name);

						// Remove duplicate name before insert
						foreach (ZipFileEntry ent in dir)
						{
							if (ent.FilenameInZip == zipfilename)
							{
								remove.Add(ent);
								break;
							}
						}

						// store the stream
						streams[zipfilename] = ms;
					}
				}


				// remove duplicate entries
				ZipStorer.RemoveEntries(ref zip, remove);

				// Add streams
				foreach (KeyValuePair<string, Stream> kvp in streams)
				{
					zip.AddStream(Compression.Deflate, kvp.Key, kvp.Value, DateTime.Now, string.Empty);
					kvp.Value.Dispose();
				}

				// Close the file
				zip.Close();

				retval = true;
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}

			Trace.WriteLine("Done !");
			return retval;
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
		/// Asset providers
		/// </summary>
		static Dictionary<Type, Provider> AssetProviders;


		/// <summary>
		/// Registred providers
		/// </summary>
		public static List<Provider> Providers
		{
			get;
			private set;
		}


		/// <summary>
		/// Registred tags
		/// </summary>
		static Dictionary<string, Provider> RegistredTags;


		/// <summary>
		/// Object used for thread locking
		/// </summary>
		static object BinaryLock;


		/// <summary>
		/// Storages
		/// </summary>
		static public List<StorageBase> Storages
		{
			get;
			private set;
		}


		/// <summary>
		/// Failback storage
		/// </summary>
		static FileSystemStorage FailbackStorage;


		#endregion

	}

}
