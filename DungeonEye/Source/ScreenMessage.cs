using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace DungeonEye
{

	/// <summary>
	/// Messages to display on the screen
	/// </summary>
	public class ScreenMessage
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="col"></param>
		public ScreenMessage(string msg, Color col)
		{
			Message = msg;
			Color = col;
			Life = 5000;
		}


		/// <summary>
		/// Message to display
		/// </summary>
		public string Message
		{
			get;
			private set;
		}


		/// <summary>
		/// Color to use
		/// </summary>
		public Color Color
		{
			get;
			private set;
		}


		/// <summary>
		/// Life time of the message
		/// </summary>
		public int Life;

	}
}
