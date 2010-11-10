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
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Interface;

namespace DungeonEye.Forms
{

	/// <summary>
	/// Door editor form
	/// </summary>
	public partial class DoorForm : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node"></param>
		public DoorForm(Door door)
		{
			InitializeComponent();


			DoorTypeBox.DataSource = Enum.GetValues(typeof(DoorType));
			DoorStateBox.DataSource = Enum.GetValues(typeof(DoorState));

			DoorStateBox.SelectedItem = door.State;
			DoorTypeBox.SelectedItem = door.Type;
			IsBreakableBox.Checked = door.IsBreakable;
			BreakValueBox.Value = door.Strength;
			IsBrokenBox.Checked = door.IsBroken;
			ItemDisappearBox.Checked = door.ItemDisappear;
			DoorTypeBox.SelectedItem = door.Type;
			BreakValueBox.Value = door.Strength;
			PicklockBox.Value = door.PickLock;

			Door = door;
		}






		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Door Door;

		#endregion

		private void IsBreakableBox_CheckedChanged(object sender, EventArgs e)
		{

			if (Door == null)
				return;

			Door.IsBreakable = IsBreakableBox.Checked;
		}

		private void BreakValueBox_ValueChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.Strength = (int)PicklockBox.Value;
		}

		private void IsBrokenBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.IsBroken = IsBrokenBox.Checked;
		}

		private void PicklockBox_ValueChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.PickLock = (int)PicklockBox.Value;
		}

		private void ItemDisappearBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.ItemDisappear = ItemDisappearBox.Checked;

		}

		private void ItemNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void ItemRadioBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void ButtonRadioBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void DoorTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.Type = (DoorType)DoorTypeBox.SelectedItem;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DoorStateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;
			
			Door.State = (DoorState)DoorStateBox.SelectedItem;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DoorForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();

		}
	}
}
