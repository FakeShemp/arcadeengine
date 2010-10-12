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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;

namespace ArcEngine.Asset
{
	/// <summary>
	/// A file containing asset
	/// </summary>
	public class AssetBank
	{

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="filename">File name</param>
		public AssetBank(string filename) : this(filename, string.Empty)
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filename">File name</param>
		/// <param name="password">Password</param>
		public AssetBank(string filename, string password)
		{
			Binaries = new List<ZipStorer.ZipFileEntry>();

			Load(filename, password);			
		}

		/// <summary>
		/// Loads ressource from file
		/// </summary>
		/// <param name="filename">Name of the file to load</param>
		/// <param name="password">Password</param>
		/// <returns>True if loaded, otherwise false</returns>
		bool Load(string filename, string password)
		{
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentNullException("filename");

			FileName = filename;
			Password = password;
			Binaries.Clear();

			Trace.WriteLine("Loading bank from file \"{0}\"...", FileName);

			// File exists ??
			if (File.Exists(filename) == false)
			{
				Trace.WriteLine("File not found !!!");
				return false;
			}

			Trace.Indent();

			// Open an existing zip file for reading
			ZipStorer zip = ZipStorer.Open(FileName, FileAccess.Read);
			if (zip == null)
			{
				Trace.WriteLine("Unable to open bank file !");
				return false;
			}


			// Read the central directory collection
			Binaries = zip.ReadCentralDir();


			// Look for the desired file
			foreach (ZipStorer.ZipFileEntry entry in Binaries)
			{
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


					Provider provider = ResourceManager.GetTagProvider(node.Name);
					if (provider == null)
					{
						Trace.WriteLine("? No Provider found for asset \"<{0}>\"...", node.Name);
						continue;
					}

					//lock (BinaryLock)
					{
						provider.Load(node);
					}
				}

				Trace.WriteLine("");
			}
			zip.Close();

			Trace.Unindent();
			Trace.WriteLine("Loading finished !");
			return true;
		}




		#region File management


		/// <summary>
		/// Opens a file
		/// </summary>
		/// <param name="filename">File name</param>
		/// <param name="mode">Open mode</param>
		/// <returns>Stream handle or null</returns>
		public Stream OpenFile(string filename, FileMode mode)
		{
			return null;
		}



		#endregion


		#region Properties

		/// <summary>
		/// Filename of t he bank
		/// </summary>
		public string FileName
		{
			get;
			private set;
		}


		/// <summary>
		/// Password to open the bank
		/// </summary>
		string Password;


		/// <summary>
		/// Returns a list of binaries present in the bank
		/// </summary>
		/// <returns>A list of binaries found</returns>
		List<ZipStorer.ZipFileEntry> Binaries;

		#endregion

	}
}
