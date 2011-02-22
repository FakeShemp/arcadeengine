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

namespace DungeonEye.Forms
{
	public partial class EventActionForm : Form
	{
		public EventActionForm()
		{
			InitializeComponent();


			ActionListBox.BeginUpdate();
			ActionListBox.Items.Add("Activate");
			ActionListBox.Items.Add("Deactivate");
			ActionListBox.Items.Add("Give Experience");
			ActionListBox.Items.Add("Give Item");
			ActionListBox.Items.Add("Healing");
			ActionListBox.Items.Add("Teleport");
			ActionListBox.Items.Add("End Choice");
			ActionListBox.Items.Add("End Dialog");
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

			UserControl ctrl = null;

			if ((string) ActionListBox.SelectedItem == "Teleport")
				ctrl = new ScriptTeleportControl();

			else if ((string) ActionListBox.SelectedItem == "Give Experience")
				ctrl = new ScriptGiveExperienceControl();




			if (ctrl == null)
				return;

			ctrl.Dock = DockStyle.Fill;
			ActionControlBox.Controls.Add(ctrl);

		}

		#endregion

	}
}
