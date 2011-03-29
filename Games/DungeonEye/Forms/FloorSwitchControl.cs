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
	public partial class FloorSwitchControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="floorswitch"></param>
		/// <param name="dungeon"></param>
		public FloorSwitchControl(FloorSwitch floorswitch, Dungeon dungeon)
		{
			InitializeComponent();

			HiddenBox.Checked = floorswitch.IsHidden;

			FloorSwitch = floorswitch;
		}


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HiddenBox_CheckedChanged(object sender, EventArgs e)
		{
			if (FloorSwitch == null)
				return;

			FloorSwitch.IsHidden = HiddenBox.Checked;
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
