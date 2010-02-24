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

namespace ArcEngine.Network
{
	/// <summary>
	/// Network packet
	/// </summary>
	public class NetPacket
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public NetPacket()
		{
			Data = new byte[65556];
			Reset();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="size"></param>
		public void SetData(byte[] data, int size)
		{
			Buffer.BlockCopy(data, 0, Data, 0, size);
			Size = size;
		}


		/// <summary>
		/// Resets 
		/// </summary>
		public void Reset()
		{
			Size = 4;
		}


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



		#region Reads

		/// <summary>
		/// Reads a string
		/// </summary>
		public string ReadString()
		{
			string msg = ASCIIEncoding.ASCII.GetString(Data, Size, 16000);
			Size += msg.Length;

			return msg;
		}


		/// <summary>
		/// REads a float
		/// </summary>
		public float ReadFloat()
		{
			float value = BitConverter.ToSingle(Data, Size);
			Size += sizeof(float);

			return value;
		}


		/// <summary>
		/// Reads 4 bytes
		/// </summary>
		public int ReadInt()
		{
			int value = BitConverter.ToInt32(Data, Size);
			Size += sizeof(int);

			return value;
		}


		/// <summary>
		/// Reads 2 bytes
		/// </summary>
		/// <returns></returns>
		public short ReadShort()
		{
			short value = BitConverter.ToInt16(Data, Size);
			Size += sizeof(short);

			return value;
		}


		/// <summary>
		/// Reads 1 byte
		/// </summary>
		public byte ReadByte()
		{
			return Data[Size++];
		}



		#endregion


		#region Properties


		/// <summary>
		/// Size of the user data
		/// </summary>
		public int Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Data
		/// </summary>
		internal byte[] Data
		{
			get;
			private set;
		}


		/// <summary>
		/// Packet type
		/// </summary>
		public PacketType Type
		{
			get
			{
				return (PacketType)Data[0];
			}

			set
			{
				Data[0] = (byte)value;
			}
		}

		#endregion
	}
}
