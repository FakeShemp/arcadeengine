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

namespace ArcEngine.Network
{
	/// <summary>
	/// Configuration for a networked app
	/// </summary>
	public class NetConfiguration
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="applicationIdentifier"></param>
		/// <param name="port"></param>
		public NetConfiguration(string applicationIdentifier, int port)
		{
			Init(applicationIdentifier, port);
		}


		/// <summary>
		/// Application-wide network configuration
		/// </summary>
		/// <param name="applicationIdentifier"></param>
		public NetConfiguration(string applicationIdentifier)
		{
			Init(applicationIdentifier, 0);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="applicationIdentifier"></param>
		/// <param name="port"></param>
		private void Init(string applicationIdentifier, int port)
		{
			//
			// default settings
			//
			isLocked = false;

			SendBufferSize = 65536;
			ReceiveBufferSize = 65536;
			ApplicationIdentifier = applicationIdentifier;
			Port = port;
			DefaultNetMessageBufferSize = 16;

			MaximumTransmissionUnit = 1459;

			// server only
			MaximumConnections = -1;
			ServerName = "No Name";
		}


		#region Fields

		/// <summary>
		/// 
		/// </summary>
		public bool IsLocked
		{
			get
			{
				return isLocked;
			}
			internal set
			{
				isLocked = value;
			}
		}
		bool isLocked;


		/// <summary>
		/// Server name reported by local server discovery
		/// </summary>
		public string ServerName;



		/// <summary>
		/// Server only: Maximum number of connections allowed
		/// </summary>
		public int MaximumConnections
		{
			get { return maxConnections; }
			set
			{
				if (isLocked)
					throw new Exception("Can't change MaximumConnections after creating NetClient/NetServer");
				maxConnections = value;
			}
		}
		private int maxConnections;



		/// <summary>
		/// Size of the send buffer; default 65536
		/// </summary>
		public int SendBufferSize
		{
			get { return sendBufferSize; }
			set
			{
				if (isLocked)
					throw new Exception("Can't change SendBufferSize after creating NetClient/NetServer");
				sendBufferSize = value;
			}
		}
		private int sendBufferSize;

		/// <summary>
		/// Size of the receive buffer; default 65536
		/// </summary>
		public int ReceiveBufferSize
		{
			get { return receiveBufferSize; }
			set
			{
				if (isLocked)
					throw new Exception("Can't change ReceiveBufferSize after creating NetClient/NetServer");
				receiveBufferSize = value;
			}
		}
		private int receiveBufferSize;



		/// <summary>
		/// Identifier for this application; differentiating it from other network apps
		/// </summary>
		public string ApplicationIdentifier
		{
			get { return applicationIdentifier; }
			set { applicationIdentifier = value; }
		}
		private string applicationIdentifier;



		/// <summary>
		/// Local port to bind to
		/// </summary>
		public int Port
		{
			get { return port; }
			set
			{
				if (isLocked)
					throw new Exception("Can't change Port after creating NetClient/NetServer");
				port = value;
			}
		}
		private int port;


		/// <summary>
		/// How many bytes to allocate in new NetMessages by default
		/// </summary>
		public int DefaultNetMessageBufferSize;

		/// <summary>
		/// Maximum number of bytes to send in a single packet
		/// </summary>
		public int MaximumTransmissionUnit;


		#endregion

	}
}
