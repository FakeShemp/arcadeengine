#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.EventScript;

namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class EventActionForm : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public EventActionForm()
		{
			InitializeComponent();


			ActionListBox.BeginUpdate();
			ActionListBox.Items.Add("Activate");
			ActionListBox.Items.Add("Deactivate");
			ActionListBox.Items.Add("Disable Choice");
			ActionListBox.Items.Add("Enable Choice");
			ActionListBox.Items.Add("Toggle");
			ActionListBox.Items.Add("Change Picture");
			ActionListBox.Items.Add("Give Experience");
			ActionListBox.Items.Add("Give Item");
			ActionListBox.Items.Add("Healing");
			ActionListBox.Items.Add("Teleport");
			ActionListBox.Items.Add("Join Character");
			ActionListBox.Items.Add("End Choice");
			ActionListBox.Items.Add("End Dialog");
			ActionListBox.Items.Add("Change Text");
			ActionListBox.EndUpdate();
		}


		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ActionControlBox.Controls.Clear();

			if (ActionListBox.SelectedIndex == -1)
				return;

			ScriptActionControlBase ctrl = null;

			if ((string) ActionListBox.SelectedItem == "Teleport")
				ctrl = new ScriptTeleportControl();

			else if ((string) ActionListBox.SelectedItem == "Change Picture")
				ctrl = new ScriptChangePictureControl();

			else if ((string) ActionListBox.SelectedItem == "Play Sound")
				ctrl = new ScriptPlaySoundControl();

			else if ((string) ActionListBox.SelectedItem == "Activate")
				ctrl = new ScriptActivateTargetControl();

			else if ((string) ActionListBox.SelectedItem == "Deactivate")
				ctrl = new ScriptDeactivateTargetControl();

			else if ((string) ActionListBox.SelectedItem == "Enable Choice")
				ctrl = new ScriptEnableChoiceControl();

			else if ((string) ActionListBox.SelectedItem == "Disable Choice")
				ctrl = new ScriptDisableChoiceControl();

			else if ((string) ActionListBox.SelectedItem == "Toggle")
				ctrl = new ScriptToggleTargetControl();

			else if ((string) ActionListBox.SelectedItem == "Healing")
				ctrl = new ScriptHealingControl();

			else if ((string) ActionListBox.SelectedItem == "Give Experience")
				ctrl = new ScriptGiveExperienceControl();

			else if ((string) ActionListBox.SelectedItem == "Give Item")
				ctrl = new ScriptGiveItemControl();

			else if ((string) ActionListBox.SelectedItem == "Change Text")
				ctrl = new ScriptChangeTextControl();

			else if ((string) ActionListBox.SelectedItem == "Join Character")
				ctrl = new ScriptJoinCharacterControl();


			if (ctrl == null)
				return;

			ctrl.Dock = DockStyle.Fill;
			ActionControlBox.Controls.Add(ctrl);

		}

		#endregion


		#region Properties


		#endregion

	}
}
