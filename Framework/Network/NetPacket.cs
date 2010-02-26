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
			Offset = 0;
		}


		/// <summary>
		/// Resets 
		/// </summary>
		public void Reset()
		{
			Offset = 0;
			Array.Clear(Data, 0, Data.Length);
			Data[0] = 0xFF;
			Data[1] = 0xFF;
			Data[2] = 0xFF;
			Data[3] = 0xFF;
		}


		#region Writes

		/// <summary>
		/// Writes a string
		/// </summary>
		/// <param name="msg"></param>
		public void Write(string msg)
		{
			ASCIIEncoding.ASCII.GetBytes(msg).CopyTo(Data, Offset + HeaderSize);
			Offset += msg.Length;
		}


		/// <summary>
		/// Writes 4 bytes
		/// </summary>
		/// <param name="value"></param>
		public void Write(float value)
		{
			BitConverter.GetBytes(value).CopyTo(Data, Offset + HeaderSize);
			Offset += sizeof(float);
		}


		/// <summary>
		/// Writes 4 bytes
		/// </summary>
		/// <param name="value"></param>
		public void Write(int value)
		{
			BitConverter.GetBytes(value).CopyTo(Data, Offset + HeaderSize);
			Offset += sizeof(int);
		}


		/// <summary>
		/// Writes 2 bytes
		/// </summary>
		/// <param name="value"></param>
		public void Write(short value)
		{
			BitConverter.GetBytes(value).CopyTo(Data, Offset + HeaderSize);
			Offset += sizeof(short);
		}


		/// <summary>
		/// Writes 1 byte
		/// </summary>
		/// <param name="value"></param>
		public void Write(byte value)
		{
			Data[Offset + HeaderSize] = value;
			Offset++;
		}



		/// <summary>
		/// Writes 1 byte
		/// </summary>
		/// <param name="value"></param>
		public void Write(bool value)
		{
			Data[Offset + HeaderSize] = Convert.ToByte(value);
			Offset++;
		}





		#endregion



		#region Reads

		/// <summary>
		/// Reads a string
		/// </summary>
		public string ReadString()
		{
			string msg = ASCIIEncoding.ASCII.GetString(Data, Offset + HeaderSize, 16000);
			Offset += msg.Length;

			return msg;
		}


		/// <summary>
		/// REads a float
		/// </summary>
		public float ReadFloat()
		{
			float value = BitConverter.ToSingle(Data, Offset + HeaderSize);
			Offset += sizeof(float);

			return value;
		}


		/// <summary>
		/// Reads 4 bytes
		/// </summary>
		public int ReadInt()
		{
			int value = BitConverter.ToInt32(Data, Offset + HeaderSize);
			Offset += sizeof(int);

			return value;
		}


		/// <summary>
		/// Reads 2 bytes
		/// </summary>
		/// <returns></returns>
		public short ReadShort()
		{
			short value = BitConverter.ToInt16(Data, Offset + HeaderSize);
			Offset += sizeof(short);

			return value;
		}


		/// <summary>
		/// Reads 1 byte
		/// </summary>
		public byte ReadByte()
		{
			byte value = Data[Offset + HeaderSize];
			Offset++;
			return value;
		}


		/// <summary>
		/// Reads 1 byte
		/// </summary>
		public bool ReadBoolean()
		{
			bool value = Convert.ToBoolean(Data[Offset + HeaderSize]);
			Offset++;
			return value;
		}



		#endregion


		#region Properties


		/// <summary>
		/// Size of the user data
		/// </summary>
		public int Offset
		{
			get;
			private set;
		}


		/// <summary>
		/// Size of the whole packet
		/// </summary>
		public int Size
		{
			get
			{
				return Offset + HeaderSize;
			}
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


		/// <summary>
		/// Size of the header
		/// </summary>
		public int HeaderSize
		{
			get
			{
				return 4;
			}
		}


		#endregion
	}
}
