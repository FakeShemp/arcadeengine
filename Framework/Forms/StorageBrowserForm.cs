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
using ArcEngine.Storage;


namespace ArcEngine.Forms
{
	/// <summary>
	/// Storage browser form
	/// </summary>
	public partial class StorageBrowserForm : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public StorageBrowserForm()
		{
			InitializeComponent();

			StorageBox.DataSource = ResourceManager.Storages;
			FilesBox.Focus();
		}


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FilesBox_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			FileNameBox.Text = e.Item.Text;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FilesBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}


		/// <summary>
		/// Change storage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StorageBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Storage = ResourceManager.Storages[StorageBox.SelectedIndex];

			OnStorageChanged();
		}

		#endregion


		#region Internal events

		/// <summary>
		/// Storage changed
		/// </summary>
		void OnStorageChanged()
		{
			if (Storage == null)
				return;

			// Directories
			List<string> directories = Storage.GetDirectoryNames();
			DirectoriesBox.BeginUpdate();
			DirectoriesBox.Nodes.Clear();

			foreach (string dir in directories)
				DirectoriesBox.Nodes.Add(dir);

			DirectoriesBox.EndUpdate();

			// Files
			OnDirectoryChanged();
		}


		/// <summary>
		/// 
		/// </summary>
		void OnDirectoryChanged()
		{
			if (Storage == null)
				return;

			// Rebuilds available files
			List<string> files = Storage.GetFileNames();

			FilesBox.BeginUpdate();
			FilesBox.Items.Clear();
			foreach (string name in files)
				FilesBox.Items.Add(name);

			FilesBox.EndUpdate();
		}

		#endregion


		#region Properties


		/// <summary>
		/// Gets or sets the storage 
		/// </summary>
		public StorageBase Storage
		{
			get;
			private set;
		}



		/// <summary>
		/// Gets or sets the current file name filter string
		/// </summary>
		public string Filter
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets a string containing the file name selected.
		/// </summary>
		public string FileName
		{
			get
			{
				return FileNameBox.Text;
			}
			set
			{
				FileNameBox.Text = value;
			}
		}



		/// <summary>
		/// Gets the current directory
		/// </summary>
		public string CurrentDirectory
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets or sets a value indicating whether the dialog box allows multiple files to be selected. 
		/// </summary>
		public bool MultiSelect
		{
			get
			{
				return FilesBox.MultiSelect;
			}
			set
			{
				FilesBox.MultiSelect = value;
			}
		}
		#endregion


	}
}
