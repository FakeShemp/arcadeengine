
namespace ArcEngine.Network
{

	/// <summary>
	/// A network connection between two endpoints 
	/// </summary>
	public class NetConnection
	{
		/// <summary>
		/// Status for this connection
		/// </summary>
		public NetConnectionStatus Status
		{
			get
			{
				return status;
			}
		}
		private NetConnectionStatus status;

	}
}
