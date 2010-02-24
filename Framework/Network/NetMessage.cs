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
using System.Net;

namespace ArcEngine.Network
{
	/// <summary>
	/// Net message
	/// </summary>
	public class NetMessage : NetPacket
	{
		


		#region Writes

		/// <summary>
		/// Writes a string
		/// </summary>
		/// <param name="msg"></param>
		public void Write(string msg)
		{
			ASCIIEncoding.ASCII.GetBytes(msg).CopyTo(Data, Size);
			Size += msg.Length;
		}


		/// <summary>
		/// Writes 4 bytes
		/// </summary>
		/// <param name="value"></param>
		public void Write(float value)
		{
			BitConverter.GetBytes(value).CopyTo(Data, Size);
			Size += sizeof(float);
		}


		/// <summary>
		/// Writes 4 bytes
		/// </summary>
		/// <param name="value"></param>
		public void Write(int value)
		{
			BitConverter.GetBytes(value).CopyTo(Data, Size);
			Size += sizeof(int);
		}


		/// <summary>
		/// Writes 2 bytes
		/// </summary>
		/// <param name="value"></param>
		public void Write(short value)
		{
			BitConverter.GetBytes(value).CopyTo(Data, Size);
			Size += sizeof(short);
		}


		/// <summary>
		/// Writes 1 byte
		/// </summary>
		/// <param name="value"></param>
		public void Write(byte value)
		{
			Data[Size++] = value;
		}



		#endregion



		#region Properties


	
		#endregion
	}
}
