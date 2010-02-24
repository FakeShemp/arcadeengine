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
using System.Text;

namespace ArcEngine.Network
{

	/// <summary>
	/// 
	/// </summary>
	public enum PacketType
	{
		/// <summary>
		/// System control packet
		/// </summary>
		ControlPacket = 0x1,


		/// <summary>
		/// User packet
		/// </summary>
		UserPacket = 0x10,

	}


	/// <summary>
	/// 
	/// </summary>
	public enum RequestType
	{
		/// <summary>
		/// This is the first packet sent by a client to the server when requesting for a connection.
		/// This is only used when joining a game. 
		/// </summary>
		ConnectionRequest = 0x1,


		/// <summary>
		/// Server accpets a connection request
		/// </summary>
		ConnectionAccept = 0x2,


		/// <summary>
		/// Server rejects a connection request
		/// </summary>
		ConnectionReject = 0x3,



		/// <summary>
		/// This is the request broadcasted by the client when you use the slist console command. 
		/// </summary>
		ServerInfo = 0x10,


		/// <summary>
		/// You can get all the information on the players on a game
		/// </summary>
		PlayerInfoRequest = 0x20,
	}
}
