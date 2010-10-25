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
using System.Text;
using System.IO;
using ArcEngine;

namespace ArcEngine.Asset
{
	/// <summary>
	/// Collect informations about a loaded binary
	/// </summary>
	public class BinaryInformation
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of the binary</param>
		public BinaryInformation(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Stream stream = ResourceManager.Load(name, FileAccess.Read);
			if (stream == null)
				return;

			Size = stream.Length;
			stream.Dispose();
		}


		/// <summary>
		/// Returns a user friendly file size as a string
		/// </summary>
		/// <returns></returns>
		/// <see>http://blogs.interakting.co.uk/brad/archive/2008/01/24/c-getting-a-user-friendly-file-size-as-a-string.aspx</see>
		public string GetFileSizeAsString()
		{
			double s = Size;
			string[] format = new string[] { "{0} bytes", "{0} KB", "{0} MB", "{0} GB", "{0} TB", "{0} PB", "{0} EB", "{0} ZB", "{0} YB" };
			int i = 0;
			while (i < format.Length - 1 && s >= 1024)
			{
				s = (100 * s / 1024) / 100.0;
				i++;
			}

			return string.Format(format[i], s.ToString("###,###,##0.##"));
		}



		#region Properties

		/// <summary>
		/// Name of the Binary
		/// </summary>
		public string Name
		{
			get;
			private set;
		}


		/// <summary>
		/// Size in byte
		/// </summary>
		public long Size
		{
			get;
			private set;
		}


		///// <summary>
		///// Last modification
		///// </summary>
		//public DateTime LastModification
		//{
		//    get;
		//    private set;
		//}

	
		
		
		#endregion
	}
}
