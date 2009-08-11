using System;
using System.Net;
using System.Net.Sockets;


namespace ArcEngine.Network
{
	/// <summary>
	/// Base class for NetClient and NetServer
	/// </summary>
	public abstract class NetBase
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="config"></param>
		public NetBase(NetConfiguration config)
		{
			configuration = config;
			configuration.IsLocked = true;
			sendBuffer = new NetBuffer(configuration.SendBufferSize);
			receiveBuffer = new NetBuffer(configuration.ReceiveBufferSize);


			Sender = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
		}


		/// <summary>
		/// Reads incoming packets
		/// </summary>
		/// <returns></returns>
		protected bool ReadPacket()
		{

			if (Socket == null || Socket.Available < 1)
				return false;


			try
			{
				int bytesReceived = Socket.ReceiveFrom(receiveBuffer.Data,
					0,
					receiveBuffer.Data.Length,
					SocketFlags.None,
					ref Sender);
				receiveBuffer.SetDataLength(bytesReceived);

				HandlePacket(receiveBuffer, (IPEndPoint)Sender);

				return true;
			}
			catch (SocketException sex)
			{
				Log.Send(new LogEventArgs(LogLevel.Error, "ReadPacket socket exception: " + sex.SocketErrorCode, null));
				return false;
			}
			catch (Exception ex)
			{
				throw new Exception("ReadPacket() exception", ex);
			}

		}


		/// <summary>
		/// Handles received packets
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="sender"></param>
		internal abstract void HandlePacket(NetBuffer buffer, IPEndPoint sender);

		#region Fields

		/// <summary>
		/// 
		/// </summary>
		private EndPoint Sender;

		/// <summary>
		/// 
		/// </summary>
		protected Socket Socket;

		/// <summary>
		/// 
		/// </summary>
		internal NetBuffer sendBuffer;

		/// <summary>
		/// 
		/// </summary>
		internal NetBuffer receiveBuffer;


		/// <summary>
		/// 
		/// </summary>
		public NetConfiguration Configuration
		{
			get
			{
				return configuration;
			}
		}
		NetConfiguration configuration;

		#endregion



		#region Events
		/// <summary>
		/// Event fired every time the status of any connection associated with this network changes
		/// </summary>
		public event EventHandler<NetStatusEventArgs> StatusChanged;

		#endregion



		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="elapsed"></param>
		public abstract void Update(int elapsed);

		#endregion

	}
}
