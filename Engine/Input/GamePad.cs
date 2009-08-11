using System;
using System.Collections.Generic;
using System.Text;

namespace ArcEngine.Input
{
	/// <summary>
	/// GamePad
	/// </summary>
	public static class GamePad
	{


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal static bool Init()
		{


			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		static public GamePadCapabilities GetCapabilities(int id)
		{
			return new GamePadCapabilities(id);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		static public void GetState(int id)
		{
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		static public void SetVibration(int id, float left, float right)
		{
		}

		#region Properties



		/// <summary>
		/// Returns the number of gamepads connected
		/// </summary>
		public static int Count
		{
			get
			{
				int count = 0; // Winmm.joyGetNumDevs();
				//if (count == Winmm.JOYERR_UNPLUGGED)
				//{
				//   return 0;
				//}


				return count;
			}
		}

		#endregion
	}
}
