#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using ArcEngine.Asset;

namespace ArcEngine.Storage
{
	/// <summary>
	/// Zip bank storage
	/// </summary>
	public class BankStorage : StorageBase
	{

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bankname">Bank name</param>
		/// <param name="access">Acces mode</param>
		public BankStorage(string bankname, FileAccess access)
		{
			Entries = new List<ZipStorer.ZipFileEntry>();

			BankName = bankname;
			Mode = access;

			Process();

		}

		#endregion


		/// <summary>
		/// Dispose
		/// </summary>
		public override void Dispose()
		{
			if (ZipHandle != null)
				ZipHandle.Close();
			ZipHandle = null;
		}



		/// <summary>
		/// Opens a file at a specified path 
		/// </summary>
		/// <param name="name">Relative path of the file </param>
		/// <param name="access">Specifies whether the file is opened with read, write, or read/write access</param>
		/// <returns>Stream handle or null</returns>
		public override Stream OpenFile(string name, FileAccess access)
		{
			foreach (ZipStorer.ZipFileEntry entry in Entries)
			{
				if (entry.FilenameInZip.Equals(name))
				{
					Stream stream = new MemoryStream();
					ZipHandle.ExtractFile(entry, stream);

					// Rewind
					stream.Seek(0, SeekOrigin.Begin);

					return stream;
				}
			}

			return null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Process()
		{
			Trace.WriteLine("Loading resources from file \"" + BankName + "\"...");

			Files.Clear();

			// File exists ??
			if (File.Exists(BankName) == false)
			{
				Trace.WriteLine("File not found !!!");
				return false;
			}

			Stopwatch swatch = new Stopwatch();
			Trace.Indent();

			// Open an existing zip file for reading
			ZipHandle = ZipStorer.Open(BankName, Mode);
			if (ZipHandle == null)
			{
				Trace.WriteLine("Failed to open bank file !");
				return false;
			}

			swatch.Start();

			// Read the central directory collection
			Entries = ZipHandle.ReadCentralDir();

			// Look for the desired file
			foreach (ZipStorer.ZipFileEntry entry in Entries)
			{
				//Trace.Write("+ {0} ({1} octets)", entry.FilenameInZip, entry.FileSize);
				Files.Add(entry.FilenameInZip);

				// Loop back if it's not an xml file
				if (!entry.FilenameInZip.EndsWith(".xml", true, null))
				{
					Trace.WriteLine("");
					continue;
				}

				// Uncompress data to a buffer
				MemoryStream ms = new MemoryStream();
				ZipHandle.ExtractFile(entry, ms);

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


					Provider provider = ResourceManager.GetTagProvider(node.Name);
					if (provider == null)
					{
						Trace.WriteLine("? No Provider found for asset \"<" + node.Name + ">\"...");
						continue;
					}

					//lock (BinaryLock)
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




		#region Properties


		/// <summary>
		/// Bank name
		/// </summary>
		string BankName;


		/// <summary>
		/// Access mode
		/// </summary>
		public FileAccess Mode
		{
			get;
			private set;
		}


		/// <summary>
		/// Archive entries
		/// </summary>
		List<ZipStorer.ZipFileEntry> Entries;


		/// <summary>
		/// Zip handle
		/// </summary>
		ZipStorer ZipHandle;

		#endregion


	}
}
