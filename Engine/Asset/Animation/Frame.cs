using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ArcEngine.Asset
{

	/// <summary>
	/// Frame animation
	/// </summary>
	public class Frame
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="time">Frame time</param>
		/// <param name="tileid">Tile id</param>
		/// <param name="location">Location</param>
		public Frame(TimeSpan time, int tileid, Point location, Color bgcolor)
		{
			Time = time;
			Location = location;
			TileID = tileid;
			BgColor = bgcolor;
		}


		#region Properties

		/// <summary>
		/// Time of the Frame
		/// </summary>
		public TimeSpan Time
		{
			get;
			private set;
		}


		/// <summary>
		/// Location of the tile
		/// </summary>
		public Point Location
		{
			get;
			private set;
		}


		/// <summary>
		/// Id of the tile
		/// </summary>
		public int TileID
		{
			get;
			private set;
		}


		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor
		{
			get;
			private set;
		}

		#endregion
	}
}
