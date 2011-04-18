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
using DungeonEye.Script;
using DungeonEye.Script.Actions;

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
		/// <param name="dungeon">Dungeon handle</param>
		public EventActionForm(Dungeon dungeon)
		{
			InitializeComponent();

			Dungeon = dungeon;

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
			ActionListBox.Items.Add("Display Message");
			ActionListBox.EndUpdate();

		}



		/// <summary>
		/// Set the action
		/// </summary>
		/// <param name="script">Action handle</param>
		/// <returns>True on success</returns>
		public bool SetAction(ActionBase script)
		{
			ControlHandle = null;
			
			if (script is ActionTeleport)
			{
				ActionListBox.SelectedItem = "Teleport";
				ControlHandle = new TeleportControl(script as ActionTeleport, Dungeon);
			}

			else if (script is ScriptActivateTarget)
			{
				ActionListBox.SelectedItem = "Activate";
				ControlHandle = new ActivateTargetControl(script as ScriptActivateTarget, Dungeon);
			}

			else if (script is ActionChangePicture)
			{
				ActionListBox.SelectedItem = "Change Picture";
				ControlHandle = new ChangePictureControl(script as ActionChangePicture);
			}

			else if (script is ActionPlaySound)
			{
				ActionListBox.SelectedItem = "Play Sound";
				ControlHandle = new PlaySoundControl(script as ActionPlaySound);
			}

			else if (script is ScriptEndDialog)
			{
				ActionListBox.SelectedItem = "End Dialog";
				ControlHandle = new EndDialogControl(script as ScriptEndDialog);
			}

			else if (script is ActionEndChoice)
			{
				ActionListBox.SelectedItem = "End Choice";
				ControlHandle = new EndChoiceControl(script as ActionEndChoice);
			}

			else if (script is ActionDeactivateTarget)
			{
				ActionListBox.SelectedItem = "Deactivate";
				ControlHandle = new DeactivateTargetControl(script as ActionDeactivateTarget, Dungeon);
			}

			else if (script is ActionEnableChoice)
			{
				ActionListBox.SelectedItem = "Enable Choice";
				ControlHandle = new EnableChoiceControl(script as ActionEnableChoice);
			}

			else if (script is ActionDisableChoice)
			{
				ActionListBox.SelectedItem = "Disable Choice";
				ControlHandle = new DisableChoiceControl(script as ActionDisableChoice);
			}

			else if (script is ActionToggleTarget)
			{
				ActionListBox.SelectedItem = "Toggle";
				ControlHandle = new ToggleTargetControl(script as ActionToggleTarget, Dungeon);
			}

			else if (script is ActionHealing)
			{
				ActionListBox.SelectedItem = "Healing";
				ControlHandle = new HealingControl(script as ActionHealing);
			}

			else if (script is ActionGiveExperience)
			{
				ActionListBox.SelectedItem = "Give Experience";
				ControlHandle = new GiveExperienceControl(script as ActionGiveExperience);
			}

			else if (script is ActionGiveItem)
			{
				ActionListBox.SelectedItem = "Give Item";
				ControlHandle = new GiveItemControl(script as ActionGiveItem);
			}

			else if (script is ActionChangeText)
			{
				ActionListBox.SelectedItem = "Change Text";
				ControlHandle = new ChangeTextControl(script as ActionChangeText);
			}

			else if (script is ActionJoinCharacter)
			{
				ActionListBox.SelectedItem = "Join Character";
				ControlHandle = new JoinCharacterControl(script as ActionJoinCharacter);
			}


			if (ControlHandle == null)
				return false;


			ControlHandle.Dock = DockStyle.Fill;
			ActionControlBox.Controls.Clear();
			ActionControlBox.Controls.Add(ControlHandle);

			return true;
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
			ControlHandle = null;

			if (ActionListBox.SelectedIndex == -1)
				return;


			if ((string) ActionListBox.SelectedItem == "Teleport")
				ControlHandle = new TeleportControl(null, Dungeon);

			else if ((string) ActionListBox.SelectedItem == "Change Picture")
				ControlHandle = new ChangePictureControl(null);

			else if ((string) ActionListBox.SelectedItem == "Play Sound")
				ControlHandle = new PlaySoundControl(null);

			else if ((string) ActionListBox.SelectedItem == "Activate")
				ControlHandle = new ActivateTargetControl(null, Dungeon);

			else if ((string) ActionListBox.SelectedItem == "End Dialog")
				ControlHandle = new EndDialogControl(null);

			else if ((string) ActionListBox.SelectedItem == "End Choice")
				ControlHandle = new EndChoiceControl(null);

			else if ((string) ActionListBox.SelectedItem == "Deactivate")
				ControlHandle = new DeactivateTargetControl(null, Dungeon);

			else if ((string) ActionListBox.SelectedItem == "Enable Choice")
				ControlHandle = new EnableChoiceControl(null);

			else if ((string) ActionListBox.SelectedItem == "Disable Choice")
				ControlHandle = new DisableChoiceControl(null);

			else if ((string) ActionListBox.SelectedItem == "Toggle")
				ControlHandle = new ToggleTargetControl(null, Dungeon);

			else if ((string) ActionListBox.SelectedItem == "Healing")
				ControlHandle = new HealingControl(null);

			else if ((string) ActionListBox.SelectedItem == "Give Experience")
				ControlHandle = new GiveExperienceControl(null);

			else if ((string) ActionListBox.SelectedItem == "Give Item")
				ControlHandle = new GiveItemControl(null);

			else if ((string) ActionListBox.SelectedItem == "Change Text")
				ControlHandle = new ChangeTextControl(null);

			else if ((string) ActionListBox.SelectedItem == "Join Character")
				ControlHandle = new JoinCharacterControl(null);


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
		ActionControlBase ControlHandle = null;


		/// <summary>
		/// Dungeon handle
		/// </summary>
		Dungeon Dungeon;


		/// <summary>
		/// Script action
		/// </summary>
		public ActionBase Action
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
