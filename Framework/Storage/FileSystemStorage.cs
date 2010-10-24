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
using System.Text;
using ArcEngine.Interface;

namespace ArcEngine.Storage
{
	/// <summary>
	/// Filesystem storage
	/// </summary>
	public class FileSystemStorage : IStorage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="path">Directory path</param>
		public FileSystemStorage(string path)
		{
			StoragePath = path;
			Files = new List<string>();

			Process();
		}


		/// <summary>
		/// Dispose resource
		/// </summary>
		public void Dispose()
		{
		}


		/// <summary>
		/// Opens a file for reading
		/// </summary>
		/// <param name="name">File name</param>
		/// <returns>Stream handle or null</returns>
		public Stream Read(string name)
		{
			string filename = Path.Combine(StoragePath, name);

			if (!File.Exists(filename))
				return null;

			return new FileStream(filename, FileMode.Open);
		}



		/// <summary>
		/// Returns a file list from the current bank matching the given search pattern.
		/// </summary>
		/// <returns>File list</returns>
		public List<string> GetFiles()
		{
			return GetFiles("*");
		}



		/// <summary>
		/// Returns a file list from the current bank matching the given search pattern.
		/// </summary>
		/// <param name="pattern">Search pattern</param>
		/// <returns>File list</returns>
		public List<string> GetFiles(string pattern)
		{
			List<string> files = new List<string>();


			return files;
		}


		/// <summary>
		/// Process all file in the storage
		/// </summary>
		/// <returns>True on succes</returns>
		public bool Process()
		{
			Files.Clear();

			DirectoryInfo di = new DirectoryInfo(StoragePath);
			foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
			{
				Files.Add(fi.FullName);
			}

			return true;
		}


		/// <summary>
		/// Opens a file for writing
		/// </summary>
		/// <param name="name">File name</param>
		/// <returns>Stream handle or null</returns>
		public Stream Write(string name)
		{
			return null;
		}


		#region Properties

		/// <summary>
		/// Storage path
		/// </summary>
		string StoragePath;


		/// <summary>
		/// 
		/// </summary>
		List<string> Files;

		#endregion
	}
}
