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
using System.Text;


// http://www.gamers.org/dEngine/quake/QDP/qnp.html

namespace ArcEngine.Network
{
	/// <summary>
	/// Network manager
	/// </summary>
	public class NetworkManager
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public NetworkManager()
		{
			Buffer = new byte[65556];
			Packet = new NetPacket();
			Clients = new List<NetPlayer>();

			Mode = NetworkManagerMode.Down;
		}


		/// <summary>
		/// Shutdown server
		/// </summary>
		public void Shutdown()
		{
			if (!IsRunning)
				return;

			Trace.WriteLine("Shutting down network manager.");
			if (Socket != null)
			{
				Socket.Shutdown(SocketShutdown.Both);
				Socket.Close();
				Socket = null;
			}

			Mode = NetworkManagerMode.Down;
		}



		/// <summary>
		/// Updates the server
		/// </summary>
		public void Update(TimeSpan elapsed)
		{
			switch (Mode)
			{
				case NetworkManagerMode.Server:
				UpdateServerMode(elapsed);
				break;
				case NetworkManagerMode.Client:
				UpdateClientMode(elapsed);
				break;
			}
		}



		#region Server mode

		/// <summary>
		/// Setups the server mode
		/// </summary>
		/// <param name="config">Server configuration</param>
		/// <returns></returns>
		public bool Server(ServerConfig config)
		{
			if (IsRunning ||config == null)
				return false;

			ServerConfig = config;

			Trace.WriteLine("Initializing server \"" + config.Name + "\".");

	
			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint end = new IPEndPoint(ServerConfig.IP, ServerConfig.Port);
			Socket.Bind(end);

			Mode = NetworkManagerMode.Server;
			return true;
		}


		/// <summary>
		/// Update in server mode
		/// </summary>
		/// <param name="elapsed">Elapsed time since last update</param>
		void UpdateServerMode(TimeSpan elapsed)
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


									if (OnPlayerJoin != null)
										OnPlayerJoin(newclient);
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
						if (OnMessage != null)
							OnMessage(Packet);
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



		#endregion



		#region Client mode

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

			ServerConfig = null;

			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			Socket.Connect(host, port);
			Mode = NetworkManagerMode.Client;

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
		void UpdateClientMode(TimeSpan elapsed)
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
						if (OnMessage != null)
							OnMessage(Packet);
					}
					break;
					#endregion
				}

			}
			#endregion
		}


		#endregion



		#region Message sending

		/// <summary>
		/// Sends a message to the server
		/// </summary>
		/// <param name="packet">Packet to send</param>
		/// <returns>True if packet sent</returns>
		/// <remarks>Works ONLY in client mode</remarks>
		public bool SendMessage(NetPacket packet)
		{
			if (Mode != NetworkManagerMode.Client)
				return false;

			int size = Socket.Send(packet.Data, packet.Offset, SocketFlags.None);
			if (size != packet.Offset)
			{
				return false;
			}

			return true;
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="packet"></param>
		/// <param name="client"></param>
		/// <returns></returns>
		public bool SendMessage(NetPacket packet, NetPlayer client)
		{
			return SendMessage(packet, client.EndPoint);
		}


		/// <summary>
		/// Sends a message
		/// </summary>
		/// <param name="packet">Packet</param>
		/// <param name="dest">Destination</param>
		/// <returns></returns>
		public bool SendMessage(NetPacket packet, IPEndPoint dest)
		{
			if (Mode != NetworkManagerMode.Server)
				return false;

			int size = Socket.SendTo(packet.Data, packet.Size, SocketFlags.None, dest);
			if (size != packet.Size)
				return false;

			return true;
		}


		#endregion



		#region Events


		public delegate void OnPlayerChatHandler(NetPlayer client);
		public event OnPlayerChatHandler OnPlayerChat;

		public delegate void OnPlayerLeaveHandler(NetPlayer client);
		public event OnPlayerLeaveHandler OnPlayerLeave;



		/// <summary>
		/// A new client is connected
		/// </summary>
		/// <param name="client"></param>
		/// <returns>Return false if the client should be kicked, or true if keep the client</returns>
		public delegate bool OnPlayerJoinHandler(NetPlayer client);

		/// <summary>
		/// A new client is connected
		/// </summary>
		public event OnPlayerJoinHandler OnPlayerJoin;



		/// <summary>
		/// 
		/// </summary>
		/// <param name="packet"></param>
		public delegate void OnMessageHandler(NetPacket packet);

		/// <summary>
		/// A new message is arrived
		/// </summary>
		public event OnMessageHandler OnMessage;

/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		public delegate void LogDelegate(string msg);


		/// <summary>
		/// 
		/// </summary>
		public event LogDelegate OnLog;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		void Log(string msg)
		{
			if (OnLog != null)
				OnLog(msg + Environment.NewLine);
		}
*/
		#endregion



		#region Properties

		byte[] Buffer;


		/// <summary>
		/// 
		/// </summary>
		NetPacket Packet;


		/// <summary>
		/// Socket connection
		/// </summary>
		Socket Socket;


		/// <summary>
		/// Is manager running
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return Mode != NetworkManagerMode.Down;
			}
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



		/// <summary>
		/// Current mode
		/// </summary>
		public NetworkManagerMode Mode
		{
			get;
			private set;
		}



		/// <summary>
		/// Server configuration
		/// </summary>
		public ServerConfig ServerConfig
		{
			get;
			private set;
		}

		#endregion
	}



	/// <summary>
	/// Current mode of the network manager
	/// </summary>
	public enum NetworkManagerMode
	{
		/// <summary>
		/// No action, idel mode
		/// </summary>
		Down,

		/// <summary>
		/// Act as a server
		/// </summary>
		Server,


		/// <summary>
		/// Act as a client
		/// </summary>
		Client,
	}
}
