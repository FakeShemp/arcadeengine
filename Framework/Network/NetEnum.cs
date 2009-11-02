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


namespace ArcEngine.Network
{

	/// <summary>
	/// Status for a connection
	/// </summary>
	public enum NetConnectionStatus
	{
		/// <summary>
		/// 
		/// </summary>
		Disconnected,

		/// <summary>
		/// 
		/// </summary>
		Connecting,

		/// <summary>
		/// 
		/// </summary>
		Connected,

		/// <summary>
		/// 
		/// </summary>
		Reconnecting,

		/// <summary>
		/// 
		/// </summary>
		Disconnecting,
	}


	/// <summary>
	/// 3 bits
	/// </summary>
	internal enum NetMessageType : int
	{
		/// <summary>
		/// No message; packet padding due to encryption
		/// </summary>
		None=0,

		/// <summary>
		/// Application message
		/// </summary>
		User=1,

		/// <summary>
		/// Application message
		/// </summary>
		UserFragmented=2,

		/// <summary>
		/// 
		/// </summary>
		Acknowledge=3,

		/// <summary>
		/// Currently not implemented
		/// </summary>
		AcknowledgeBitField=4,

		/// <summary>
		/// connect, connectresponse, connectestablished, disconnected (incl. server full)
		/// </summary>
		Handshake=5,

		/// <summary>
		/// ping, pong, optimizeinfo
		/// </summary>
		PingPong=6,

		/// <summary>
		/// request, response
		/// </summary>
		Discovery=7,
	}


	/// <summary>
	/// 
	/// </summary>
	internal enum NetHandshakeType : byte
	{
		/// <summary>
		/// 
		/// </summary>
		Connect,

		/// <summary>
		/// 
		/// </summary>
		ConnectResponse,

		/// <summary>
		/// 
		/// </summary>
		ConnectionEstablished,

		/// <summary>
		/// 
		/// </summary>
		Disconnected
	}

}
