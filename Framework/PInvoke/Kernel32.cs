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
using System.Runtime.InteropServices;
using System.Security;

namespace ArcEngine.PInvoke
{
	internal class Kernel32
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lpFileName"></param>
		/// <returns></returns>
		[DllImport("kernel32", SetLastError=true)]
		internal static extern IntPtr LoadLibrary(string lpFileName);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hModule"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);
	}
}