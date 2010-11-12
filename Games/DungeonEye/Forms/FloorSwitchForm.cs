using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Floor switch form editor
	/// </summary>
	public partial class FloorSwitchForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="floorswitch"></param>
		/// <param name="dungeon"></param>
		public FloorSwitchForm(FloorSwitch floorswitch, Dungeon dungeon)
		{
			InitializeComponent();

			HiddenBox.Checked = floorswitch.IsHidden;

			FloorSwitch = floorswitch;
		}


		#region Form events


		private void HiddenBox_CheckedChanged(object sender, EventArgs e)
		{
			if (FloorSwitch == null)
				return;

			FloorSwitch.IsHidden = HiddenBox.Checked;
		}

		private void FloorSwitchForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}

		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		FloorSwitch FloorSwitch;


		#endregion

	}
}
