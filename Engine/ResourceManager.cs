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
using System.Text.RegularExpressions;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Providers;
using ICSharpCode.SharpZipLib.Zip;

// Drawing 2D circle and line : http://forums.xna.com/thread/39157.aspx
// GamePad : http://sourceforge.net/projects/xnadirectinput/
//
//
//
//
//
//
//
//
//
/*
public static void compressTo(Stream s,byte[] b)
{
	ZipOutputStream zip=new ZipOutputStream(s);
	zip.SetLevel(9);
	ZipEntry zipentry=new ZipEntry("image.jpg");
	zip.PutNextEntry(zipentry);
	zip.Write(b,0,b.Length);
	zip.Flush();
	zip.Close();
}
public static byte[] decompressTo(Stream s,int unpaksize)
{
	ZipInputStream zipin=new ZipInputStream(s);
	ZipEntry zipentry=zipin.GetNextEntry();
	byte[] b=new byte[unpaksize];
	int readindex=0;

	while ( readindex < unpaksize )
	readindex+=zipin.Read(b,readindex,unpaksize-readindex);

	return b;
}

*/


namespace ArcEngine
{

	/// <summary>
	/// Resource manager
	/// </summary>
	static public class ResourceManager
	{

		#region Constructor


		/// <summary>
		/// Constructor
		/// </summary>
		static ResourceManager()
		{
			UnknownAssets = new List<XmlNode>();
			Assets = new Dictionary<Type, Provider>();
			Providers = new List<Provider>();
			Tags = new Dictionary<string, Provider>();
			Binaries = new Dictionary<string, byte[]>();

			AddProvider(new FontProvider());
			AddProvider(new ScriptProvider());
			AddProvider(new TileSetProvider());
			AddProvider(new StringTableProvider());
			AddProvider(new AnimationProvider());
			AddProvider(new LayoutProvider());
			AddProvider(new KeyboardSchemeProvider());
			AddProvider(new AudioProvider());
		}



		#endregion


		/// <summary>
		/// Returns a list of binary matching a pattern using regular expression
		/// </summary>
		/// <param name="pattern">Pattern to apply (ie *.png, *.txt)</param>
		/// <returns>A list of binaries found</returns>
		static public List<string> GetBinaries(string pattern)
		{
			List<string> list = new List<string>();

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
				throw new ArgumentNullException("provider");

			if (Providers.Contains(provider))
				throw new ArgumentException("Provider already present", "provider");

			Trace.Write("Adding new provider : " + provider.ToString() + " {");

			// Adds the provider to the list
			Providers.Add(provider);

			// Register asset's tags
			foreach (string tag in provider.Tags)
			{
				Tags[tag] = provider;
			}
			// Register assets
			foreach (Type type in provider.Assets)
			{
				Assets[type] = provider;
				Trace.Write(type.ToString() + ", ");
			}

			Trace.WriteLine("}");
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
				throw new ArgumentNullException("tag");

			if (!Tags.ContainsKey(tag))
				return null;

			return Tags[tag];
		}


		/// <summary>
		/// Returns the <see cref="Provider"/> of an asset
		/// </summary>
		/// <param name="type">asset type</param>
		/// <returns>Provider of the asset or null</returns>
		static public Provider GetAssetProvider(Type type)
		{
			if (type == null)
			//    throw new ArgumentNullException("type");
			return null;

			if (Assets.ContainsKey(type))
				return Assets[type];

			return null;
		}



		/// <summary>
		/// Removes a specific provider
		/// </summary>
		/// <param name="provider">Provider to remove</param>
		static public void RemoveProvider(Provider provider)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");


			// Remove tags
			foreach (string tag in provider.Tags)
				Tags.Remove(tag);


			// Remove provider
			Providers.Remove(provider);
		}


		#endregion


		#region Assets


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

			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			Assets[typeof(T)].Add<T>(name, node);
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


			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			return Assets[typeof(T)].Get<T>(name);
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

			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			return Assets[typeof(T)].Create<T>(name);
		}


		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset</param>
		static public void RemoveAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			Assets[typeof(T)].Remove<T>(name);
		}



		/// <summary>
		/// Removes a specific asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		static public void RemoveAsset<T>() where T : IAsset
		{
			Provider provider = GetAssetProvider(typeof(T));
			if (provider == null)
				return;

			provider.Remove<T>();
		}



		/// <summary>
		/// Removes all assets
		/// </summary>
		static public void ClearAssets()
		{
			foreach (Provider provider in Providers)
				provider.Clear();

			Binaries.Clear();
			UnknownAssets.Clear();
		}



		/// <summary>
		/// Returns a list of all availabe asset
		/// </summary>
		/// <typeparam name="T">Asset's type</typeparam>
		/// <returns>Sorted list of available asset</returns>
		static public List<string> GetAssets<T>() where T : IAsset
		{
			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			return Assets[typeof(T)].GetAssets<T>();
		}

		#endregion


		#region Shared assets

		/// <summary>
		/// Creates and retruns a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">ASset name</param>
		/// <returns>The asset or null</returns>
		static public T CreateSharedAsset<T>(string name) where T : IAsset
		{
			if (string.IsNullOrEmpty(name))
				return default(T);

			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			return Assets[typeof(T)].CreateShared<T>(name);
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

			if (!Assets.ContainsKey(typeof(T)))
				throw new ArgumentException("Unknown asset type");

			Assets[typeof(T)].RemoveShared<T>(name);
		}



		/// <summary>
		/// Removes a specific shared asset type
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		static public void RemoveSharedAsset<T>() where T : IAsset
		{
			Provider provider = GetAssetProvider(typeof(T));
			if (provider == null)
				return;

			provider.RemoveShared<T>();
		}



		/// <summary>
		/// Removes all shared assets
		/// </summary>
		static public void ClearSharedAssets()
		{
			foreach (Provider provider in Providers)
				provider.ClearShared();
		}




		#endregion


		#region I/O operations


		/// <summary>
		/// Loads ALL ressource from file in memory
		/// </summary>
		/// <param name="filename">Name of the file to load</param>
		/// <returns></returns>
		///TODO Detecter si il s'agit d'un fichier .zip ou d'un fichier .xml 
		///afin de permettre de chager juste un fichier xml tout seul
		static public bool LoadBank(string filename)
		{
			Trace.WriteLine("Loading resources from file \"" + filename + "\"...");

			// File exists ??
			if (File.Exists(filename) == false)
			{
				Trace.WriteLine("File not found !!!");
				return false;
			}

			Trace.Indent();
			using (FileStream fs = File.OpenRead(filename))
			{
				//TODO check if the bank is a zip file
				ZipInputStream zip = new ZipInputStream(fs);
				

				// Foreach file in the bank
				ZipEntry entry;
				while ((entry = zip.GetNextEntry()) != null)
				{
					//Log.Send(new LogEventArgs(LogLevel.Info, "+ " + entry.Name, null));
					Trace.WriteLine("+ " + entry.Name);

					// If it isn't a file, skip it
					if (!entry.IsFile)
						continue;


					// Uncompress data to a buffer
					byte[] data = new byte[entry.Size];
					zip.Read(data, 0, (int)entry.Size);


					// If it ends with .xml, then adds it to the asset list to process
					if (entry.Name.EndsWith(".xml", true, null))
					{

						XmlDocument doc = new XmlDocument();
						doc.LoadXml(ASCIIEncoding.ASCII.GetString(data));

						// Check the root node
						XmlElement xml = doc.DocumentElement;
						if (xml.Name.ToLower() != "bank")
						{
							Trace.WriteLine("\"" + filename + "\" is not a valid bank file !");
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
								UnknownAssets.Add(node);
								continue;
							}

							provider.Load(node);
						}
	
					}
					else
					{
						// Adds data to the list
						Binaries[entry.Name] = data;
					}
				}
			}



			Trace.Unindent();
			Trace.WriteLine("Loading finished !");
			return true;
		}


/*
		/// <summary>
		/// Loads a resource from a bank first, and if not found, load it from disk.
		/// </summary>
		/// <param name="resourcename">Name of the file to load</param>
		/// <returns>Binary of the resource</returns>
		static public byte[] LoadResource(string resourcename)
		{
			if (string.IsNullOrEmpty(resourcename))
				return null;

			byte[] data = null;

			//
			// 1° Look in binaries
			//
			if (Binaries.ContainsKey(resourcename))
				return Binaries[resourcename];
			

			//
			// 2° try to load it from disk
			//
			if (!File.Exists(resourcename))
				return null;

			// Opens the file and copy it to memory
			FileStream stream = File.Open(resourcename, FileMode.Open, FileAccess.Read);
			if (stream == null)
				return null;

			data = new byte[stream.Length];
			stream.Read(data, 0, (int)stream.Length);
			stream.Close();
			return data;
		}
*/

		/// <summary>
		/// Loads a resource from a bank first, and if not found, load it from disk.
		/// </summary>
		/// <param name="resourcename">Name of the file to load</param>
		/// <returns>Stream to the resource. Don't forget to close it !</returns>
		static public Stream LoadResource(string resourcename)
		{
			if (string.IsNullOrEmpty(resourcename))
				return null;

			//
			// 1° Look in binaries
			//
			if (Binaries.ContainsKey(resourcename))
			{
				return new MemoryStream(Binaries[resourcename]);
			}

			//
			// 2° try to load it from disk
			//
			if (File.Exists(resourcename))
			{
				return File.Open(resourcename, FileMode.Open, FileAccess.Read);
			}

			return null;
		}

		/// <summary>
		/// Unloads a binary resource
		/// </summary>
		/// <param name="name">Name of the binary to remove</param>
		static public void UnloadResource(string name)
		{
			if (Binaries.ContainsKey(name))
				Binaries.Remove(name);
		}


		/// <summary>
		/// Gets an internal resource
		/// </summary>
		/// <param name="name">Name of the resource</param>
		/// <returns>Resource stream or null if not found</returns>
		/// <remarks>Don't forget to Dispose the stream !!</remarks>
		static public Stream GetResource(string name)
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
		/// Saves a single resource in a bank file
		/// </summary>
		/// <param name="stream">Bank stream</param>
		/// <param name="resourcename">full path name of the resource in the bank</param>
		/// <param name="data">Data to save</param>
		/// <returns>True if everything went ok</returns>
		static internal bool SaveResource(ZipOutputStream stream, string resourcename, byte[] data)
		{
			// Bad args
			if (stream == null || string.IsNullOrEmpty(resourcename) || data == null)
				return false;

			ZipEntry entry = new ZipEntry(resourcename);
			stream.PutNextEntry(entry);
			stream.Write(data, 0, data.Length);
			return true;
		}



		/// <summary>
		/// Saves all resources to a bank file
		/// </summary>
		/// <param name="filename">The file name of the bank on disk</param>
		/// <returns>True if everything went ok</returns>
		static public bool SaveResources(string filename)
		{
			// Return value
			bool retval = false;

			//Log.Send(new LogEventArgs(LogLevel.Info, "Saving all resources to bank \"" + filename + "\"...", null));
			Trace.WriteLine("Saving all resources to bank \"" + filename + "\"...");

			FileStream stream = null;
			ZipOutputStream zip = null;
			try
			{

				stream = File.Create(filename);
				zip = new ZipOutputStream(stream);
				zip.SetLevel(5);



				// Save all Binaries
				foreach (KeyValuePair<string, byte[]> kvp in Binaries)
					SaveResource(zip, kvp.Key, kvp.Value);

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
						int count = (int)mi.Invoke(provider, null);
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
						SaveResource(zip, type.Name + ".xml", ms.ToArray());
					}
				}

				retval = true;
				Trace.WriteLine("Done !");

			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}
			finally
			{
				if (zip != null)
				{
					zip.Finish();
					zip.Close();
				}
				if (stream != null)
					stream.Close();
			}

			return retval;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Binary files
		/// </summary>
		static Dictionary<string, byte[]> Binaries;


		/// <summary>
		/// Returns a list of loaded binaries
		/// </summary>
		static public List<string> LoadedBinaries
		{
			get
			{
				List<string> list = new List<string>();

				foreach (string name in Binaries.Keys)
					list.Add(name);

				list.Sort();

				return list;
			}
		}


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
		static List<XmlNode> UnknownAssets;


		/// <summary>
		/// Asset providers
		/// </summary>
		static Dictionary<Type, Provider> Assets;



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
		static Dictionary<string, Provider> Tags;

		#endregion


	}



}
