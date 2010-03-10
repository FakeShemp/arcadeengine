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
	/// A new client is connected
	/// </summary>
	/// <param name="client"></param>
	/// <returns>Return false if the client should be kicked, or true if keep the client</returns>
	public delegate bool OnPlayerJoinHandler(NetPlayer client);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="client"></param>
	public delegate void OnPlayerLeaveHandler(NetPlayer client);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="client"></param>
	public delegate void OnPlayerChatHandler(NetPlayer client);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="packet"></param>
	public delegate void OnMessageHandler(NetPacket packet);

	
	
	/// <summary>
	/// 
	/// </summary>
	public abstract class NetworkBase
	{

		/// <summary>
		/// 
		/// </summary>
		public NetworkBase()
		{
			Buffer = new byte[65556];
			Packet = new NetPacket();

		}



		/// <summary>
		/// Shutdown server
		/// </summary>
		public void Shutdown()
		{

			Trace.WriteLine("Shutting down network manager.");
			if (Socket != null)
			{
				Socket.Shutdown(SocketShutdown.Both);
				Socket.Close();
				Socket = null;
			}

		}



		#region Message sending

		/// <summary>
		/// Sends a message to the server
		/// </summary>
		/// <param name="packet">Packet to send</param>
		/// <returns>True if packet sent</returns>
		/// <remarks>Works ONLY in client mode</remarks>
		public bool SendMessage(NetPacket packet)
		{


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

			int size = Socket.SendTo(packet.Data, packet.Size, SocketFlags.None, dest);
			if (size != packet.Size)
				return false;

			return true;
		}


		#endregion




		#region Events


		/// <summary>
		/// 
		/// </summary>
		public event OnPlayerChatHandler OnPlayerChat;


		/// <summary>
		/// 
		/// </summary>
		public event OnPlayerLeaveHandler OnPlayerLeave;



		/// <summary>
		/// A new client is connected
		/// </summary>
		public event OnPlayerJoinHandler OnPlayerJoin;


		/// <summary>
		/// A new message is arrived
		/// </summary>
		public event OnMessageHandler OnMessage;


		#endregion




		#region Properties


		/// <summary>
		/// Socket
		/// </summary>
		protected Socket Socket;



		/// <summary>
		/// 
		/// </summary>
		protected byte[] Buffer;



		/// <summary>
		/// 
		/// </summary>
		protected NetPacket Packet;



		/// <summary>
		/// Is manager running
		/// </summary>
		public bool IsRunning
		{
			get;
			protected set;
		}


		#endregion
	}
}
