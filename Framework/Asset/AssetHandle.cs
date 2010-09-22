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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace ArcEngine.Asset
{
	/// <summary>
	/// Streaming asset
	/// </summary>
	public class AssetHandle : IDisposable
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		internal AssetHandle()
		{
			Stream = null;
		}


		/// <summary>
		/// Opens a binary from a bank
		/// </summary>
		/// <param name="reference">Reference to the binary</param>
		internal AssetHandle(ResourceReference reference)
		{
			if (reference == null)
				throw new ArgumentNullException("reference");

			// File exists ??
			if (File.Exists(reference.FileName) == false)
				return;


			FS = File.OpenRead(reference.FileName);

			Zip = new ZipInputStream(FS);
			Zip.Password = reference.Password;


			// Foreach entry in the bank
			ZipEntry entry;
			while (true)
			{
				entry = Zip.GetNextEntry();
				
				// EOF
				if (entry == null)
					throw new InvalidDataException("Zip entry = null !");

				// If it isn't a file, skip it
				if (!entry.IsFile)
					continue;

				// Not or file
				if (entry.Name.ToLower() == reference.Name.ToLower())
				{
					Stream = Zip;
					return;
				}
			}

			//throw new FileNotFoundException("Zip entry \"" + reference.Name + "\" not found", reference.FileName);
		}


		/// <summary>
		/// Opens a file
		/// </summary>
		/// <param name="filename">Filename to open</param>
		internal AssetHandle(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentNullException("reference");


			// File exists ??
			if (File.Exists(filename) == false)
				return;

			FS = File.OpenRead(filename);
			Stream = FS;
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		public void Dispose()
		{
			if (Zip != null)
				Zip.Dispose();
			Zip = null;

			if (FS != null)
				FS.Dispose();
			FS = null;
		}



		#region Properties

		/// <summary>
		/// Handle to the stream
		/// </summary>
		public Stream Stream
		{
			get;
			private set;
		}


		/// <summary>
		/// Zip handle
		/// </summary>
		ZipInputStream Zip;


		/// <summary>
		/// FileStream handle
		/// </summary>
		FileStream FS;

		#endregion
	}
}
