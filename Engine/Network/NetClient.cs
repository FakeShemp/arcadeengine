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

using System.Net.Sockets;
using System.Net;


namespace ArcEngine.Network
{
	/// <summary>
	/// A network client 
	/// </summary>
	public class NetClient : NetBase
	{


		/// <summary>
		/// 
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public NetClient(NetConfiguration config) : base(config)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="elapsed"></param>
		public override void Update(int elapsed)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		public void Connect(string host, int port)
		{
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
