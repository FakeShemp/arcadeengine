using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ArcEngine.Examples.LightTest
{
	public class Wall
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public Wall(Point from, Point to)
		{
			From = from;
			To = to;
		}




		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public Point From;


		/// <summary>
		/// 
		/// </summary>
		public Point To;

		#endregion
	}
}
