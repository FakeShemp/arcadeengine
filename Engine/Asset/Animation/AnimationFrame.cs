using System;
using System.Collections.Generic;
using System.Text;

namespace ArcEngine.Asset
{
	/// <summary>
	/// Frame in an <see cref="Animation"/>
	/// </summary>
	public struct AnimationFrame
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="length"></param>
		public AnimationFrame(int id, TimeSpan length)
		{
			ID = id;
			Length = length;
		}




		#region Properties

		/// <summary>
		/// ID of the tile
		/// </summary>
		public int ID;

		/// <summary>
		/// Length
		/// </summary>
		public TimeSpan Length;

		#endregion
	}
}
