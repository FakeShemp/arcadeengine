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
			IncomingBuffer = new byte[65556];
			Clients = new List<NetClient>();

			Mode = NetworkManagerMode.Down;
		}



		/// <summary>
		/// Updates the server
		/// </summary>
		public void Update()
		{
			if (!IsRunning)
				return;

			EndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
			while (Socket.Available != 0)
			{

				int size = Socket.ReceiveFrom(IncomingBuffer, ref endpoint);
				IncomingBuffer[size] = 0;


				
				OnLog(Encoding.ASCII.GetString(IncomingBuffer) + Environment.NewLine);
			}

		}




		/// <summary>
		/// Setups the server mode
		/// </summary>
		/// <param name="host">Host to bind to</param>
		/// <param name="port">Port number</param>
		/// <returns></returns>
		public bool Listen(IPAddress host, int port)
		{
			if (IsRunning)
				return false;

			ListeningPort = port;
			Host = host;

			Log("Initializing server.");

	
			Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint end = new IPEndPoint(Host, ListeningPort);
			Socket.Bind(end);

			Mode = NetworkManagerMode.Server;
			return true;
		}



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

			IPEndPoint ipep = new IPEndPoint(host, port);
			string str = "Hello world";
			byte[] data = Encoding.ASCII.GetBytes(str);
			Socket.SendTo(data, data.Length, SocketFlags.None, ipep);

			NetMessage msg = new NetMessage();
			msg.Write("toto fait du velo sur le dos");
			SendMessage(msg);


			return true;
		}



		/// <summary>
		/// Shutdown server
		/// </summary>
		public void Shutdown()
		{
			if (!IsRunning)
				return;

			Log("Shutting down server.");
			Socket.Shutdown(SocketShutdown.Both);
			Socket.Close();
			Socket = null;

	
			Mode = NetworkManagerMode.Down;
		}




		/// <summary>
		/// Sends a message
		/// </summary>
		/// <param name="msg"></param>
		public void SendMessage(NetMessage msg)
		{

		}


		#region Events

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

		#endregion


		#region Properties

		/// <summary>
		/// Recieve buffer
		/// </summary>
		byte[] IncomingBuffer;


		/// <summary>
		/// 
		/// </summary>
		Socket Socket;


		/// <summary>
		/// 
		/// </summary>
		public int ListeningPort
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public IPAddress Host
		{
			get;
			private set;
		}

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
		List<NetClient> Clients;



		/// <summary>
		/// Current mode
		/// </summary>
		public NetworkManagerMode Mode
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
