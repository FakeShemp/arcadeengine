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
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ArcEngine.Asset;

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

			//UnknownAssets = new List<XmlNode>();
			AssetProviders = new Dictionary<Type, Provider>();
			Providers = new List<Provider>();
			RegistredTags = new Dictionary<string, Provider>();
			//Binaries = new Dictionary<string, byte[]>();
			KnownAssets = new Dictionary<string, ResourceReference>();

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
			{
				provider.Dispose();
			}
		}


		/// <summary>
		/// Returns a list of binary matching a pattern using regular expression
		/// </summary>
		/// <param name="pattern">Pattern to apply (ie *.png, *.txt)</param>
		/// <returns>A list of binaries found</returns>
		static public List<string> GetBinaries(string pattern)
		{
			Trace.WriteDebugLine("[ResourceManager] GetBinaries (pattern = {0}", pattern);
			
			List<string> list = new List<string>();

			// No need to lock() because LoadedBinaries do the work for us
			foreach (string name in LoadedBinaries)
			{
				if (Regex.IsMatch(name, pattern))
					list.Add(name);
			}

			list.Sort();

			return list;
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

			Trace.WriteDebugLine("[ResourceManager] ConvertAsset");

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
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <param name="asset"></param>
		static public void AddAsset<T>(string name, IAsset asset) where T : IAsset
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				asset.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

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



/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">Name of the asset</param>
		/// <returns></returns>
		static public Stream GetAsset(string name)
		{
			if (string.IsNullOrEmpty(name))
				return Stream.Null;

			lock (BinaryLock)
			{
				return null;
			}
		}
*/


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

		//	Trace.WriteDebugLine("[ResourceManager] CreateAsset (name = {0})", name);

			lock (BinaryLock)
			{
				if (!AssetProviders.ContainsKey(typeof(T)))
					throw new ArgumentException("Unknown asset type");

				IAsset asset = AssetProviders[typeof(T)].Create<T>(name);
				//if (asset != null)
				//    asset.Init();

				return (T)(object)asset;
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

				//Binaries.Clear();
				//UnknownAssets.Clear();
				KnownAssets.Clear();

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


		#region I/O operations

		/// <summary>
		/// Loads ressource from file
		/// </summary>
		/// <param name="filename">Name of the file to load</param>
		/// <returns>True if loaded, otherwise false</returns>
		static public bool LoadBank(string filename)
		{
			return LoadBank(filename, string.Empty);
		}


		/// <summary>
		/// Loads ressource from file
		/// </summary>
		/// <param name="filename">Name of the file to load</param>
		/// <param name="password">Password</param>
		/// <returns>True if loaded, otherwise false</returns>
		static public bool LoadBank(string filename, string password)
		{

			Trace.WriteLine("Loading resources from file \"" + filename + "\"...");

			// File exists ??
			if (File.Exists(filename) == false)
			{
				Trace.WriteLine("File not found !!!");
				return false;
			}

			Stopwatch swatch = new Stopwatch();
			Trace.Indent();

			// Open an existing zip file for reading
			ZipStorer zip = ZipStorer.Open(filename, FileAccess.Read);
			if (zip == null)
			{
				Trace.WriteLine("Unable to open bank file !");
				return false;
			}

			swatch.Start();

			// Read the central directory collection
			List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

			// Look for the desired file
			foreach (ZipStorer.ZipFileEntry entry in dir)
			{

				// Adds entry to the list
				KnownAssets[entry.FilenameInZip] = new ResourceReference(entry.FilenameInZip, filename, password);

				Trace.Write("+ {0} ({1} octets)", entry.FilenameInZip, entry.FileSize);

				// Loop back if it's not an xml file
				if (!entry.FilenameInZip.EndsWith(".xml", true, null))
				{
					Trace.WriteLine("");
					continue;
				}

				// Uncompress data to a buffer
				MemoryStream ms = new MemoryStream();
				zip.ExtractFile(entry, ms);

				// Convert to xml
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(ASCIIEncoding.ASCII.GetString(ms.ToArray()));


				// Check the root node
				XmlElement xml = doc.DocumentElement;
				if (xml.Name.ToLower() != "bank")
				{
					Trace.WriteLine("");
					continue;
				}


				// For each nodes, process it
				XmlNodeList nodes = xml.ChildNodes;
				foreach (XmlNode node in nodes)
				{
					if (node.NodeType == XmlNodeType.Comment)
						continue;


					Provider provider = GetTagProvider(node.Name);
					if (provider == null)
					{
						Trace.WriteLine("? No Provider found for asset \"<" + node.Name + ">\"...");
						continue;
					}

					lock (BinaryLock)
					{
						provider.Load(node);
					}
				}

				Trace.WriteLine("");
			}

			swatch.Stop();
			Trace.Unindent();
			Trace.WriteLine("Loading finished ! ({0} ms)", swatch.ElapsedMilliseconds);
			return true;
		}



		/// <summary>
		/// Loads a bank asynchronously
		/// </summary>
		/// <param name="filename">Name of the file to load</param>
		/// <returns></returns>
		static public bool LoadBankAsync(string filename)
		{


			return false;
		}


		/// <summary>
		/// Checks if a Binary is already present
		/// </summary>
		/// <param name="name">Name of the binary</param>
		/// <returns>True if present, or false</returns>
		static public bool BinaryExist(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;
			bool ret;

			lock (BinaryLock)
			{
				ret = KnownAssets.ContainsKey(name);
			}
			return ret;
		}


		/// <summary>
		/// Loads a resource from a bank first, and if not found, load it from disk.
		/// </summary>
		/// <param name="resourcename">Name of the file to load</param>
		/// <returns>Stream to the resource. Don't forget to close it !</returns>
		static public Stream LoadResource(string resourcename)
		{
			if (string.IsNullOrEmpty(resourcename))
				return null;


			Stream stream = null;

			//
			// 1° Look in binaries
			//
			if (BinaryExist(resourcename))
			{
				ResourceReference resref = KnownAssets[resourcename];

				// Open an existing zip file for reading
				ZipStorer zip = ZipStorer.Open(resref.FileName, FileAccess.Read);
				if (zip == null)
					return null;


				// Read the central directory collection
				List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

				// Look for the desired file
				foreach (ZipStorer.ZipFileEntry entry in dir)
				{
					if (entry.FilenameInZip != resref.Name)
						continue;

					stream = new MemoryStream();
					zip.ExtractFile(entry, stream);

					break;
				}

				zip.Close();

				// Rewind...
				stream.Seek(0, SeekOrigin.Begin);

				return stream;
			}

			//
			// 2° try to load it from disk
			//
			if (File.Exists(resourcename))
			{
				stream = File.Open(resourcename, FileMode.Open, FileAccess.Read);
			}

			return stream;
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
		/// Saves all resources to a bank file
		/// </summary>
		/// <param name="filename">The file name of the bank on disk</param>
		/// <returns>True if everything went ok</returns>
		static public bool SaveResources(string filename)
		{
			return SaveResources(filename, string.Empty);
		}


		/// <summary>
		/// Saves all resources to a bank file
		/// </summary>
		/// <param name="filename">The file name of the bank on disk</param>
		/// <param name="password">Password</param>
		/// <returns>True if everything went ok</returns>
		static public bool SaveResources(string filename, string password)
		{
			// Return value
			bool retval = false;

			Trace.WriteLine("Saving all resources to bank \"" + filename + "\"...");


			try
			{
				ZipStorer zip = ZipStorer.Open(filename, FileAccess.Write);

				// Save all providers
				XmlWriter doc;
				MemoryStream ms;

				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.OmitXmlDeclaration = false;
				settings.IndentChars = "\t";
				settings.Encoding = ASCIIEncoding.ASCII;

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

						ms = new MemoryStream();
						doc = XmlWriter.Create(ms, settings);
						doc.WriteStartDocument(true);
						doc.WriteStartElement("bank");


						// Invoke the generic method like this : provider.Save<[Asset Type]>(XmlNode node);
						object[] args = { doc };
						Type[] types = new Type[] { typeof(XmlWriter) };
						mi = provider.GetType().GetMethod("Save", types).MakeGenericMethod(type);
						mi.Invoke(provider, args);

						doc.WriteEndElement();
						doc.WriteEndDocument();
						doc.Flush();

						zip.AddStream(ZipStorer.Compression.Deflate, filename, ms, DateTime.Now, string.Empty);
							
						ms.Dispose();
					}
				}

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
		/// Known assets
		/// </summary>
		static Dictionary<string, ResourceReference> KnownAssets;


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
		/// List of unknown loaded assets
		/// </summary>
	//	static List<XmlNode> UnknownAssets;


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



		#endregion

	}


	/// <summary>
	/// Reference to binary files in banks
	/// </summary>
	internal class ResourceReference
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="file"></param>
		/// <param name="pwd"></param>
		public ResourceReference(string name, string file, string pwd)
		{
			Name = name;
			FileName = file;
			Password = pwd;
		}


		/// <summary>
		/// Asset name
		/// </summary>
		public string Name;


		/// <summary>
		/// File name
		/// </summary>
		public string FileName;


		/// <summary>
		/// Password to read the file
		/// </summary>
		public string Password;
	}

}
