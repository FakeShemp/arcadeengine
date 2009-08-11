
using System;
using System.Net;
using System.Net.Sockets;


namespace ArcEngine.Network
{
	/// <summary>
	/// A network server
	/// </summary>
	public class NetServer : NetBase
	{
		#region Fields
		/// <summary>
		/// List of all connections to this server; may contain null entries and
		/// entries may have status disconnected
		/// </summary>
		public NetConnection[] Connections
		{
			get
			{
				return connections;
			}
		}
		private NetConnection[] connections;


		/// <summary>
		/// Gets the number of client that are connected (or connecting!)
		/// </summary>
		public int NumConnected
		{
			get
			{
				int retval = 0;
				for (int i = 0; i < Connections.Length; i++)
					if (Connections[i] != null && Connections[i].Status != NetConnectionStatus.Disconnected)
						retval++;
				return retval;
			}
		}

	
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public NetServer(NetConfiguration config) : base(config)
		{
			if (config == null)
			{
				Log.Send(new LogEventArgs(LogLevel.Error, "config missing !", null));
				return;
			}


			connections = new NetConnection[config.MaximumConnections];


			try
			{
				IPEndPoint iep = new IPEndPoint(IPAddress.Any, Configuration.Port);

				// Binds to the port
				Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				Socket.Blocking = false;
				Socket.Bind((EndPoint)iep);
			}
			catch (SocketException sex)
			{
				if (sex.SocketErrorCode != SocketError.AddressAlreadyInUse)
					throw new Exception("Failed to bind to port " + Configuration.Port + " - Address already in use!", sex);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to bind to port " + Configuration.Port, ex);
			}

			Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, Configuration.ReceiveBufferSize);
			Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, Configuration.SendBufferSize);

		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="elapsed"></param>
		public override void Update(int elapsed)
		{
			// Read all incoming packets
			ReadPacket();


			// Update all connections

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="sender"></param>
		internal override void HandlePacket(NetBuffer buffer, IPEndPoint sender)
		{ 
		}

	}
}
