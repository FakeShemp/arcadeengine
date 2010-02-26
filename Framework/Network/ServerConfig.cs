using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ArcEngine.Network
{
	/// <summary>
	/// Server parameters
	/// </summary>
	public class ServerConfig
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ip">IP to bind the server to</param>
		/// <param name="port">Listening port</param>
		/// <param name="name">Name of the server</param>
		/// <param name="maxclient">Maximum number of client allowed</param>
		public ServerConfig(IPAddress ip, int port, string name, int maxclient)
		{
			IP = ip;
			Port = port;
			Name = name;
			MaxClient = maxclient;
		}



		#region Properties

		/// <summary>
		/// Server ip to bind to
		/// </summary>
		public IPAddress IP
		{
			get;
			private set;
		}



		/// <summary>
		/// Listening port
		/// </summary>
		public int Port
		{
			get;
			private set;
		}


		/// <summary>
		/// Server name
		/// </summary>
		public string Name
		{
			get;
			private set;
		}


		/// <summary>
		/// Maximum number of clients
		/// </summary>
		public int MaxClient
		{
			get;
			private set;
		}

		#endregion
	}
}
