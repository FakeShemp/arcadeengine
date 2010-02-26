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

namespace ArcEngine.Network
{
	/// <summary>
	/// Network player connected to a NetServer
	/// </summary>
	public class NetPlayer
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="endpoint">IP address</param>
		public NetPlayer(IPEndPoint endpoint)
		{
			EndPoint = endpoint;
			State = NetClientState.Connecting;
		}



		/// <summary>
		/// Update client status
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{

		}


		#region Properties


		/// <summary>
		/// Status of the client
		/// </summary>
		public NetClientState State
		{
			get;
			private set;
		}


		/// <summary>
		/// IP address of the client
		/// </summary>
		internal IPEndPoint EndPoint
		{
			get;
			private set;
		}

		#endregion
	}



	/// <summary>
	/// Status of a NetClient
	/// </summary>
	public enum NetClientState
	{
		/// <summary>
		/// Try to connect
		/// </summary>
		Connecting,


		/// <summary>
		/// Client is connected
		/// </summary>
		Connected,
	}
}
