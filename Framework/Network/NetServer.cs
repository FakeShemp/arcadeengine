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


//
// http://www.gamers.org/dEngine/quake/QDP/qnp.html
//
namespace ArcEngine.Network
{
	/// <summary>
	/// 
	/// </summary>
	public class NetServer : NetworkBase
	{
		/// <summary>
		/// 
		/// </summary>
		public NetServer()
		{


			Clients = new List<NetPlayer>();

		}



		/// <summary>
		/// Setups the server mode
		/// </summary>
		/// <param name="config">Server configuration</param>
		/// <returns></returns>
		public bool Server(ServerConfig config)
		{
			//if (IsRunning || config == null)
			//    return false;

			ServerConfig = config;

			Trace.WriteLine("Initializing server \"" + config.Name + "\".");


			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint end = new IPEndPoint(ServerConfig.IP, ServerConfig.Port);
			Socket.Bind(end);

			return true;
		}


		/// <summary>
		/// Update in server mode
		/// </summary>
		/// <param name="elapsed">Elapsed time since last update</param>
		public void Update(TimeSpan elapsed)
		{

			EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			//NetClient client = null;

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

							//
							// Connection request
							case RequestType.ConnectionRequest:
							{
								NetPacket packet = new NetPacket();
								packet.Type = PacketType.ControlPacket;


								if (ClientCount >= ServerConfig.MaxClient)
								{
									// Too many clients
									packet.Write((byte)RequestType.ConnectionReject);
									packet.Write("Server full.");
									Trace.WriteLine("Server : Too many connection request, discarding client.");
								}
								else
								{
									// Add client
									NetPlayer newclient = new NetPlayer(sender as IPEndPoint);
									Clients.Add(newclient);

									// Accept connection
									packet.Write((byte)RequestType.ConnectionAccept);
									Trace.WriteLine("Server : Adding new client.");


									//if (OnPlayerJoin != null)
									//    OnPlayerJoin(newclient);
								}


								// Send the response
								SendMessage(packet, sender as IPEndPoint);


								packet.Reset();
								packet.Type = PacketType.UserPacket;
								packet.Write("Toto 1");
								packet.Write((byte)2);
								packet.Write("Toto 2");
								packet.Write(3.5f);
								packet.Write("Toto 3");
								SendMessage(packet, sender as IPEndPoint);
							}
							break;


							//
							// Ask for server informations
							case RequestType.ServerInfo:
							{
							}
							break;


							//
							// Request informations about a player
							case RequestType.PlayerInfoRequest:
							{
							}
							break;


							default:
							{
								Trace.WriteLine("Server : Wrong control packet type received !");
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


			#region Process clients


			#endregion
		}


		/// <summary>
		/// Gets the client handle from an IPEndPoint
		/// </summary>
		/// <param name="ip">Ip address</param>
		/// <returns>Client handle or null</returns>
		NetPlayer GetClientFromAddress(IPEndPoint ip)
		{
			if (ip == null)
				return null;

			foreach (NetPlayer client in Clients)
			{
				if (client.EndPoint == ip)
					return client;
			}


			return null;
		}


		#region Properties


		/// <summary>
		/// Server configuration
		/// </summary>
		public ServerConfig ServerConfig
		{
			get;
			private set;
		}



		/// <summary>
		/// Number of connected clients
		/// </summary>
		public int ClientCount
		{
			get
			{
				return Clients.Count;
			}
		}


		/// <summary>
		/// List of clients
		/// </summary>
		List<NetPlayer> Clients;

		#endregion
	}
}
