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
