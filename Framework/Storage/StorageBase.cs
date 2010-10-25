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
		/// Dispose resource
		/// </summary>
		public virtual void Dispose()
		{
		}


		/// <summary>
		/// Opens a file for reading
		/// </summary>
		/// <param name="name">File name</param>
		/// <returns>Stream handle or null</returns>
		public virtual Stream Read(string name)
		{
			return null;
		}


		/// <summary>
		/// Opens a file for writing
		/// </summary>
		/// <param name="name">File name</param>
		/// <returns>Stream handle or null</returns>
		public virtual Stream Write(string name)
		{
			return null;
		}


		/// <summary>
		/// Returns a file list from the current bank
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
			List<string> list = new List<string>();

			foreach (string file in Files)
			{
				if (Regex.IsMatch(file, pattern))
					list.Add(file);
			}

			list.Sort();

			return list;
		}


		#region Properties

		/// <summary>
		/// Files list
		/// </summary>
		protected List<string> Files;

		#endregion
	}
}
