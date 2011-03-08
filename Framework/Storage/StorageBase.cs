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
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

namespace ArcEngine.Storage
{
	/// <summary>
	/// Storage interface
	/// </summary>
	public abstract class StorageBase : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public StorageBase()
		{
			Files = new List<string>();
			Directories = new List<string>();
		}



		/// <summary>
		/// Process all file in the storage
		/// </summary>
		/// <returns>True on succes</returns>
		public virtual bool Process()
		{
			return false;
		}


		/// <summary>
		/// 
		/// </summary>
		public virtual void Flush()
		{
		}

		/// <summary>
		/// Process an asset definition
		/// </summary>
		/// <param name="xml"></param>
		protected bool ProcessAsset(XmlElement xml)
		{
			if (xml == null || xml.Name.ToLower() != "bank")
				return false;

			// For each nodes, process it
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
					continue;
				}

				//Provider provider = ResourceManager.GetTagProvider(node.Name);
				RegisteredAsset ra = ResourceManager.GetRegisteredByTag(node.Name);
				if (ra== null)
				{
					Trace.WriteLine("[StorageBase::Process()] No registered asset found for tag \"<" + node.Name + ">\"...");
					continue;
				}

				//lock (BinaryLock)
				{
					ra.Load(node);
				}
			}

			return true;
		}

		/// <summary>
		/// Dispose resource
		/// </summary>
		public virtual void Dispose()
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return base.ToString();
		}


		#region Files

		/// <summary>
		/// Opens a file at a specified path 
		/// </summary>
		/// <param name="name">Relative path of the file </param>
		/// <param name="access">Specifies whether the file is opened with read, write, or read/write access</param>
		/// <returns>Stream handle or null if file not found</returns>
		public virtual Stream OpenFile(string name, FileAccess access)
		{
			return null;
		}


		/// <summary>
		/// Creates a new file 
		/// </summary>
		/// <param name="file">The relative path of the file to create</param>
		/// <returns>Stream handle or null</returns>
		public virtual Stream CreateFile(string file)
		{
			return null;
		}


		/// <summary>
		/// Deletes a file 
		/// </summary>
		/// <param name="file">The relative path of the file to delete</param>
		/// <returns>True on success</returns>
		public virtual bool DeleteFile(string file)
		{
			return false;
		}


		/// <summary>
		/// Determines whether the specified path refers to an existing file  
		/// </summary>
		/// <param name="file">The file to test</param>
		/// <returns>True on success</returns>
		public virtual bool FileExists(string file)
		{
			return false;
		}


		/// <summary>
		/// Returns a file list from the current bank
		/// </summary>
		/// <returns>File list</returns>
		public List<string> GetFileNames()
		{
			return GetFileNames("");
		}


		/// <summary>
		/// Returns a file list from the current bank matching the given search pattern.
		/// </summary>
		/// <param name="pattern">Search pattern</param>
		/// <returns>File list</returns>
		public List<string> GetFileNames(string pattern)
		{
			List<string> list = new List<string>();

			foreach (string file in Files)
			{
				if (Regex.IsMatch(file, Regex.Escape(pattern)))
					list.Add(file);
			}

			list.Sort();

			return list;
		}

		#endregion


		#region Directories

		/// <summary>
		/// Creates a new directory 
		/// </summary>
		/// <param name="directory">The relative path of the directory to create</param>
		/// <returns>True on success</returns>
		public bool CreateDirectory(string directory)
		{
			return false;
		}


		/// <summary>
		/// Deletes a directory 
		/// </summary>
		/// <param name="directory">The relative path of the directory to delete</param>
		/// <returns>True on success</returns>
		public virtual bool DeleteDirectory(string directory)
		{
			return false;
		}


		/// <summary>
		/// Determines whether the specified path refers to an existing directory  
		/// </summary>
		/// <param name="directory">The path to test</param>
		/// <returns>True on success</returns>
		public virtual bool DirectoryExists(string directory)
		{
			return false;
		}



		/// <summary>
		/// Enumerates the directories. 
		/// </summary>
		/// <returns>An array of relative paths of directories</returns>
		public virtual List<string> GetDirectoryNames()
		{
			return GetDirectoryNames("*");
		}



		/// <summary>
		/// Enumerates the directories that conform to a search pattern. 
		/// </summary>
		/// <param name="searchPattern">A search pattern.</param>
		/// <returns>An array of relative paths of directories</returns>
		public virtual List<string> GetDirectoryNames(string searchPattern)
		{
			List<string> directories = new List<string>();


			return directories;
		}



		#endregion


		#region Properties

		/// <summary>
		/// Files list
		/// </summary>
		protected List<string> Files;


		/// <summary>
		/// Directories list
		/// </summary>
		protected List<string> Directories;

		#endregion
	}
}
