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

			OnCoordinateChanged(EventArgs.Empty);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void ChangedEventHandler(string name, Point coordinate);

		/// <summary>
		/// 
		/// </summary>
		public event ChangedEventHandler CoordinateChanged;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCoordinateChanged(EventArgs e)
		{
			if (CoordinateChanged != null)
				CoordinateChanged(MazeName, Coordinate);
		}


		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FromMapBox_Click(object sender, EventArgs e)
		{
			if (Dungeon == null)
				return;

			DungeonLocationForm form = new DungeonLocationForm(Dungeon, MazeName, Coordinate);
			form.ShowDialog();

			SetTarget(form.MazeName, form.Coordinate);
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
