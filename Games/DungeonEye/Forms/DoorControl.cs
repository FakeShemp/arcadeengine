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
	public partial class DoorControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="door">Door handle</param>
		public DoorControl(Door door)
		{
			InitializeComponent();


			DoorTypeBox.DataSource = Enum.GetValues(typeof(DoorType));
			DoorStateBox.DataSource = Enum.GetValues(typeof(DoorState));

			DoorStateBox.SelectedItem = door.State;
			DoorTypeBox.SelectedItem = door.Type;
			ItemConsumBox.Checked = door.ConsumeItem;
			DoorTypeBox.SelectedItem = door.Type;
			PicklockBox.Value = door.PickLock;
			SpeedBox.Value = (int)door.Speed.TotalSeconds;


			IsBreakableBox.Checked = door.IsBreakable;
			BreakValueBox.Value = door.Strength;
			BreakValueBox.Visible = door.IsBreakable;

			switch (door.OpenType)
			{
				case DoorOpenType.Button:
				ButtonRadioBox.Checked = true;
				break;
				case DoorOpenType.Item:
				ItemRadioBox.Checked = true;
				break;
				case DoorOpenType.Event:
				EventRadioBox.Checked = true;
				break;
			}


			if (door.OpenType == DoorOpenType.Item)
			{

				// Populate item list
				if (ItemNameBox.Items.Count == 0 && !DesignMode)
					ItemNameBox.DataSource = ResourceManager.GetAssets<Item>();

				ItemNameBox.SelectedItem = door.OpenItemName;

				ItemPanel.Visible = true;
			}
			else
				ItemPanel.Visible = false;
			

			Door = door;
		}



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IsBreakableBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.IsBreakable = IsBreakableBox.Checked;
			BreakValueBox.Visible = Door.IsBreakable;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BreakValueBox_ValueChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.Strength = (int)BreakValueBox.Value;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PicklockBox_ValueChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.PickLock = (int)PicklockBox.Value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemDisappearBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.ConsumeItem = ItemConsumBox.Checked;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.OpenItemName = (string)ItemNameBox.SelectedItem;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpensBy_CheckedChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			RadioButton button = sender as RadioButton;

			if (button.Checked)
			{

				if (button == ButtonRadioBox)
				{
					Door.OpenType = DoorOpenType.Button;
					Door.OpenItemName = string.Empty;
					Door.ConsumeItem = false;
					Door.PickLock = 0;
				}
				else if (button == ItemRadioBox)
				{
					Door.OpenType = DoorOpenType.Item;
					Door.OpenItemName = (string)ItemNameBox.SelectedItem;
					Door.ConsumeItem = ItemConsumBox.Checked;
					Door.PickLock = (int)PicklockBox.Value;

					// Populate item list
					if (ItemNameBox.Items.Count == 0 && !DesignMode)
						ItemNameBox.DataSource = ResourceManager.GetAssets<Item>();

					ItemNameBox.SelectedItem = Door.OpenItemName;
				}
				else if (button == EventRadioBox)
				{
					Door.OpenType = DoorOpenType.Event;
					Door.OpenItemName = string.Empty;
					Door.ConsumeItem = false;
					Door.PickLock = 0;
				}
			}

			ItemPanel.Visible = Door.OpenType == DoorOpenType.Item;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		private void SpeedBox_ValueChanged(object sender, EventArgs e)
		{
			if (Door == null)
				return;

			Door.Speed = TimeSpan.FromSeconds((int)SpeedBox.Value);
		}

		#endregion



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Door Door;

		#endregion


	}
}
