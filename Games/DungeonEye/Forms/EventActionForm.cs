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
			ActionListBox.Items.Add("Play Sound");
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


			if ((string) ActionListBox.SelectedItem == "Teleport")
				ControlHandle = new ScriptTeleportControl();

			else if ((string) ActionListBox.SelectedItem == "Change Picture")
				ControlHandle = new ScriptChangePictureControl();

			else if ((string) ActionListBox.SelectedItem == "Play Sound")
				ControlHandle = new ScriptPlaySoundControl();

			else if ((string) ActionListBox.SelectedItem == "Activate")
				ControlHandle = new ScriptActivateTargetControl();

			else if ((string) ActionListBox.SelectedItem == "End Dialog")
				ControlHandle = new ScriptEndDialogControl();

			else if ((string) ActionListBox.SelectedItem == "End Choice")
				ControlHandle = new ScriptEndChoiceControl();

			else if ((string) ActionListBox.SelectedItem == "Deactivate")
				ControlHandle = new ScriptDeactivateTargetControl();

			else if ((string) ActionListBox.SelectedItem == "Enable Choice")
				ControlHandle = new ScriptEnableChoiceControl();

			else if ((string) ActionListBox.SelectedItem == "Disable Choice")
				ControlHandle = new ScriptDisableChoiceControl();

			else if ((string) ActionListBox.SelectedItem == "Toggle")
				ControlHandle = new ScriptToggleTargetControl();

			else if ((string) ActionListBox.SelectedItem == "Healing")
				ControlHandle = new ScriptHealingControl();

			else if ((string) ActionListBox.SelectedItem == "Give Experience")
				ControlHandle = new ScriptGiveExperienceControl();

			else if ((string) ActionListBox.SelectedItem == "Give Item")
				ControlHandle = new ScriptGiveItemControl();

			else if ((string) ActionListBox.SelectedItem == "Change Text")
				ControlHandle = new ScriptChangeTextControl();

			else if ((string) ActionListBox.SelectedItem == "Join Character")
				ControlHandle = new ScriptJoinCharacterControl();


			if (ControlHandle == null)
				return;

			ControlHandle.Dock = DockStyle.Fill;
			ActionControlBox.Controls.Add(ControlHandle);

		}



		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		ScriptActionControlBase ControlHandle = null;


		/// <summary>
		/// Script action
		/// </summary>
		public IScriptAction Script
		{
			get
			{
				if (ControlHandle == null)
					return null;

				return ControlHandle.Action;
			}
		}
		#endregion

	}
}
