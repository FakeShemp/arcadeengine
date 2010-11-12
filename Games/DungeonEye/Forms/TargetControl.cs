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
		/// Constructor
		/// </summary>
		public TargetControl()
		{
			InitializeComponent();
			Target = new DungeonLocation();
		}


		/// <summary>
		/// Changes target
		/// </summary>
		/// <param name="dungeon">Dungeon handle</param>
		/// <param name="target">Target handle</param>
		public void SetTarget(Dungeon dungeon, DungeonLocation target)
		{
			Dungeon = dungeon;
			SetTarget(target);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		public void SetTarget(DungeonLocation target)
		{
			Target = target;

			MazeNameBox.Text = Target.Maze;
			CoordinateBox.Text = Target.Coordinate.X + " x " + Target.Coordinate.Y;

			OnTargetChanged(EventArgs.Empty);
		}


		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public delegate void ChangedEventHandler(object sender, DungeonLocation target);

		/// <summary>
		/// 
		/// </summary>
		public event ChangedEventHandler TargetChanged;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnTargetChanged(EventArgs e)
		{
			if (TargetChanged != null)
				TargetChanged(this, Target);
		}

		#endregion


		#region Form events

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

			SetTarget(form.Target);
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
		/// Target
		/// </summary>
		public DungeonLocation Target
		{
			get;
			private set;
		}


		/// <summary>
		/// Maze name
		/// </summary>
		public string MazeName
		{
			get
			{
				return Target.Maze;
			}			
		}

		/// <summary>
		/// Coordinate
		/// </summary>
		public Point Coordinate
		{
			get
			{
				return Target.Coordinate;
			}
		}

		#endregion
	}
}
