using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Stair form editor
	/// </summary>
	public partial class StairForm : Form
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stair"></param>
		/// <param name="dungeon"></param>
		public StairForm(Stair stair, Dungeon dungeon)
		{
			InitializeComponent();


			TargetBox.SetTarget(dungeon, stair.Target);
			DirectionBox.DataSource = Enum.GetValues(typeof(StairType));
			DirectionBox.SelectedItem = stair.Type;
			

			Stair = stair;
		}




		#region Form events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="target"></param>
		private void TargetBox_TargetChanged(object sender, DungeonLocation target)
		{
			if (Stair == null)
				return;

			Stair.Target = target;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DirectionBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Stair == null)
				return;

			Stair.Type = (StairType)DirectionBox.SelectedItem;
		}

		#endregion


		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Stair Stair;

		#endregion
	}
}
