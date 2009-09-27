#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Text;
using System.Windows.Forms;

namespace ArcEngine.Games.DungeonEye.Forms.Wizards
{
	/// <summary>
	/// 
	/// </summary>
	public partial class NewItemWizard : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public NewItemWizard(ItemSet set)
		{
			InitializeComponent();

			ItemSet = set;
		}




		/// <summary>
		/// FormClosing event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			// 
			if (DialogResult != DialogResult.OK)
				return;

			// Maze already exists ?
			if (string.IsNullOrEmpty(ItemNameBox.Text) || ItemSet.GetItem(ItemNameBox.Text) != null)
			{
				MessageBox.Show("Item name already in use or invalid. Use another name !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}


			// Create the maze
			Item item = new Item();
			item.Name = ItemNameBox.Text;
			ItemSet.Items.Add(ItemNameBox.Text, item);
		}



		#region Properties



		/// <summary>
		/// 
		/// </summary>
		ItemSet ItemSet;

		#endregion

	}
}
