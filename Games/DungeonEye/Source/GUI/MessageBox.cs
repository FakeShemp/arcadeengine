using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;


namespace DungeonEye.Gui
{
	/// <summary>
	/// Dialog box window asking for a simple yes no question
	/// </summary>
	public class MessageBox
	{

		/// <summary>
		/// 
		/// </summary>
		public void Draw()
		{
			Display.FillRectangle(new Rectangle(10, 10, 200, 200), Color.FromArgb(101, 105, 182));

		}


		/// <summary>
		/// Updates the window
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
		}


		#region Properties

		/// <summary>
		/// Message
		/// </summary>
		public string Message
		{
			get;
			set;
		}


		/// <summary>
		/// Yes string
		/// </summary>
		public string Yes
		{
			get;
			set;
		}

		/// <summary>
		/// No string
		/// </summary>
		public string No
		{
			get;
			set;
		}

		#endregion
	}
}
