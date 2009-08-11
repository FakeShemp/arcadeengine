using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms.Wizards
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
