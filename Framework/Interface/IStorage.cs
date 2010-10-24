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

namespace ArcEngine.Interface
{
	/// <summary>
	/// Storage interface
	/// </summary>
	public interface IStorage : IDisposable
	{

		/// <summary>
		/// Process all file in the storage
		/// </summary>
		/// <returns>True on succes</returns>
		bool Process();


		/// <summary>
		/// Opens a file for reading
		/// </summary>
		/// <param name="name">File name</param>
		/// <returns>Stream handle or null</returns>
		Stream Read(string name);


		/// <summary>
		/// Opens a file for writing
		/// </summary>
		/// <param name="name">File name</param>
		/// <returns>Stream handle or null</returns>
		Stream Write(string name);


		/// <summary>
		/// Returns a file list from the current bank
		/// </summary>
		/// <returns>File list</returns>
		List<string> GetFiles();


		/// <summary>
		/// Returns a file list from the current bank matching the given search pattern.
		/// </summary>
		/// <param name="pattern">Search pattern</param>
		/// <returns>File list</returns>
		List<string> GetFiles(string pattern);
	}
}
