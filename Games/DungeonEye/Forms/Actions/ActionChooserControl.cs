#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using ArcEngine;
using DungeonEye.Script;
using DungeonEye.Script.Actions;


namespace DungeonEye.Forms.Script
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ActionChooserControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dungeon"></param>
		/// <param name="script"></param>
	//	public ScriptActionChooserControl(AlcoveScript script, Dungeon dungeon)
		public ActionChooserControl()
		{
			InitializeComponent();

			//Script = script;
			//Dungeon = dungeon;
		}



		#region Control events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionChooserBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ActionPropertiesBox.Controls.Clear();

			ActionControlBase control = null;
			switch ((string)ActionChooserBox.SelectedItem)
			{
				case "Toggles":
				{
					control = new ToggleTargetControl(null, Dungeon);		
				}
				break;
				case "Activates":
				{
					control = new ActivateTargetControl(null, Dungeon);
				}
				break;
				case "Deactivates":
				{
					control = new DeactivateTargetControl(null, Dungeon);
				}
				break;
				case "Activates / Deactivates":
				{

				}
				break;
				case "Deactivates / Activates":
				{
				}
				break;
				case "Exchanges":
				{

				}
				break;
				case "Set To":
				{
				}
				break;
				case "Play Sound":
				{
				}
				break;
				case "Stop Sounds":
				{
				}
				break;
				default:
				{
					MessageBox.Show("Unhandled action type !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				break;
			}

			if (control != null)
				ActionPropertiesBox.Controls.Add(control);

		}

		#endregion



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Dungeon Dungeon;


		/// <summary>
		/// 
		/// </summary>
		AlcoveScript Script;


		#endregion
	}
}
