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
		/// 
		/// </summary>
		/// <param name="path"></param>
		public FileSystemStorage(string path)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
		}



		public Stream Read(string name)
		{
			throw new NotImplementedException();
		}




		public List<string> GetFiles()
		{
			throw new NotImplementedException();
		}



		public List<string> GetFiles(string pattern)
		{
			throw new NotImplementedException();
		}

		public bool Process()
		{
			throw new NotImplementedException();
		}


		public Stream Write(string name)
		{
			throw new NotImplementedException();
		}
	}
}
