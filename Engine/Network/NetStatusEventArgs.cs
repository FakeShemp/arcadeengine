using System;

namespace ArcEngine.Network
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class NetStatusEventArgs : EventArgs
	{
		/// <summary>
		/// The connection for which status changed
		/// </summary>
		public NetConnection Connection;

		/// <summary>
		/// Previous status of the connection
		/// </summary>
		public NetConnectionStatus PreviousStatus;

		/// <summary>
		/// A human readable reason for the status change
		/// </summary>
		public string Reason;
	}



}
