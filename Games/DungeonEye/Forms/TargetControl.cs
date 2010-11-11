using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{

	/// <summary>
	/// Target location control
	/// </summary>
	public partial class TargetControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		public TargetControl()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="maze"></param>
		/// <param name="coordinate"></param>
		public void SetTarget(string maze, Point coordinate)
		{
			if (Dungeon == null)
				return;

			MazeName = maze;
			Coordinate = coordinate;

			MazeNameBox.Text = MazeName;
			CoordinateBox.Text = Coordinate.X + " x " + Coordinate.Y;
		}

		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FromMapBox_Click(object sender, EventArgs e)
		{

		}

		#endregion


		#region Properties

		/// <summary>
		/// Dungeon to use
		/// </summary>
		public Dungeon Dungeon
		{
			get;
			set;
		}


		/// <summary>
		/// Maze name
		/// </summary>
		public string MazeName
		{
			get;
			private set;
		}

		/// <summary>
		/// Location
		/// </summary>
		public Point Coordinate
		{
			get;
			private set;
		}

		#endregion
	}
}
