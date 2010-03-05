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
using System.Net;
using System.Net.Sockets;



namespace ArcEngine.Network
{
	/// <summary>
	/// 
	/// </summary>
	public class NetClient : NetworkBase
	{

		/// <summary>
		/// Connects to a server
		/// </summary>
		/// <param name="host">Server host</param>
		/// <param name="port">Port</param>
		/// <returns></returns>
		public bool Connect(string host, int port)
		{
			IPHostEntry hostInfo = Dns.GetHostEntry(host);
			return Connect(hostInfo.AddressList[1], port);
		}


		/// <summary>
		/// Connects to a server
		/// </summary>
		/// <param name="host">Server host</param>
		/// <param name="port">Port</param>
		/// <returns></returns>
		public bool Connect(IPAddress host, int port)
		{
			if (IsRunning)
				return false;


			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			Socket.Connect(host, port);

			// Send initial handshake
			NetPacket msg = new NetPacket();
			msg.Type = PacketType.ControlPacket;
			msg.Write((byte)RequestType.ConnectionRequest);
			msg.Write("Game Name : Toto");
			SendMessage(msg);


			return true;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="elapsed"></param>
		public void Update(TimeSpan elapsed)
		{
			EndPoint sender = new IPEndPoint(IPAddress.Any, 0);

			#region Receive datas

			// While data available
			while (Socket.Available != 0)
			{
				// Collect data
				int size = Socket.ReceiveFrom(Buffer, ref sender);
				Packet.SetData(Buffer, size);


				// Process message
				switch (Packet.Type)
				{
					#region Control Packet
					case PacketType.ControlPacket:
					{
						switch ((RequestType)Packet.ReadByte())
						{

							case RequestType.ConnectionAccept:
							{
							}
							break;

							case RequestType.ConnectionReject:
							{
								Trace.WriteLine("Client : Connection rejected :");
								Packet.ReadByte();
								Trace.WriteLine(Packet.ReadString());
							}
							break;

							default:
							{
								Trace.WriteLine("Client : Wrong control packet type received !");
								continue;
							}
						}




					}
					break;
					#endregion



					#region User packet
					case PacketType.UserPacket:
					{
						//if (OnMessage != null)
						//    OnMessage(Packet);
					}
					break;
					#endregion
				}

			}
			#endregion
		}
	
	}
}
