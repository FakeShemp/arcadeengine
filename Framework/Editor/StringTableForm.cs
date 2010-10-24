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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Interface;

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class StringTableForm : AssetEditorBase 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public StringTableForm(XmlNode node)
		{
			InitializeComponent();

			// Create an instance of a ListView column sorter and assign it to the ListView control.
			Sorter = new ListViewColumnSorter();
			Sorter.Order = StringListBox.Sorting;
			StringListBox.ListViewItemSorter = Sorter;



			StringTable = new StringTable();
			StringTable.Load(node);


			Build();


		}


		/// <summary>
		/// Rebuild the interface
		/// </summary>
		void Build()
		{
			CurrentLanguageBox.Items.Clear();
			CurrentLanguageBox.BeginUpdate();
			DefaultLanguageBox.Items.Clear();
			DefaultLanguageBox.BeginUpdate();
			foreach (string name in StringTable.LanguagesList)
			{
				DefaultLanguageBox.Items.Add(name);
				CurrentLanguageBox.Items.Add(name);
			}
			CurrentLanguageBox.EndUpdate();
			DefaultLanguageBox.EndUpdate();

			// Default language
			if (!string.IsNullOrEmpty(StringTable.Default) && DefaultLanguageBox.Items.Contains(StringTable.Default))
				DefaultLanguageBox.SelectedItem = StringTable.Default;

			//if (!string.IsNullOrEmpty(StringTable.LanguageName) && CurrentLanguageBox.Items.Contains(StringTable.LanguageName))
			//    CurrentLanguageBox.SelectedItem = StringTable.LanguageName;
			if (!string.IsNullOrEmpty(StringTable.Default) && CurrentLanguageBox.Items.Contains(StringTable.Default))
				CurrentLanguageBox.SelectedItem = StringTable.Default;


			BuildListString();
		}


		/// <summary>
		/// Display all string in the current language
		/// </summary>
		void BuildListString()
		{

			StringListBox.BeginUpdate();
			StringListBox.Items.Clear();

			if (CurrentLanguageBox.SelectedIndex == -1)
			{
				StringListBox.EndUpdate();
				return;
			}

			Language language = StringTable.GetLanguage(CurrentLanguageBox.SelectedItem as string);

			foreach (KeyValuePair<int, string> kvp in language.Strings)
			{
				ListViewItem item = new ListViewItem(kvp.Key.ToString());
				item.SubItems.Add(kvp.Value);
				StringListBox.Items.Add(item);
			}

			// Compare the current language and the default language
			if (DefaultLanguageBox.SelectedItem != null && DefaultLanguageBox.SelectedItem as string != StringTable.LanguageName)
			{
				Language deflang = StringTable.GetLanguage(DefaultLanguageBox.SelectedItem as string);


				foreach (KeyValuePair<int, string> kvp in deflang.Strings)
				{
					// If current language already contains this string, skip it
					if (!string.IsNullOrEmpty(language.GetString(kvp.Key)))
						continue;

					// Else add to the listview 
					ListViewItem item = new ListViewItem(kvp.Key.ToString());
					item.ForeColor = Color.Red;

					item.SubItems.Add(kvp.Value);
					StringListBox.Items.Add(item);
				}

			}

			StringListBox.EndUpdate();
			StringListBox.Sort();
		}


		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				StringTable.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<ArcEngine.Asset.StringTable>(StringTable.Name, doc.DocumentElement);
		}


		#region Events

		/// <summary>
		/// Closing form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringTableForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "ArcEngine Editor", MessageBoxButtons.YesNoCancel);

			
			
			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}


		/// <summary>
		/// Change default language
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DefaultLanguageBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DefaultLanguageBox.SelectedIndex == -1)
				return;

			StringTable.Default = DefaultLanguageBox.SelectedItem as string;
	//		Build();
		}



		/// <summary>
		/// Adds a new string
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAddNewString(object sender, EventArgs e)
		{
			if (StringTable.CurrentLanguage == null)
				return;

			// Add to the stringtable
			StringTable.AddString(TranslatedTextBox.Text);
			TranslatedTextBox.Text = string.Empty;

			BuildListString();
		}


		/// <summary>
		/// Removes current string
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteString_Click(object sender, EventArgs e)
		{
			if (StringListBox.FocusedItem == null)
				return;

			CurrentListViewItem = null;
			StringTable.SetString(int.Parse(StringListBox.FocusedItem.Text), string.Empty);
			StringListBox.Items.Remove(StringListBox.FocusedItem);

			TranslatedTextBox.Text = string.Empty;
			OriginalTextBox.Text = string.Empty;

			BuildListString();
		}


		/// <summary>
		/// On language to edit changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CurrentLanguageBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentListViewItem = null;
			StringTable.LanguageName = CurrentLanguageBox.SelectedItem as string;

			TranslatedTextBox.Text = string.Empty;
			OriginalTextBox.Text = string.Empty;

			BuildListString();
		}



		/// <summary>
		/// On string mouse click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringListBox_MouseClick(object sender, MouseEventArgs e)
		{

			WriteStringBox_Click(null, null);


			// Collect the new string
			if (StringListBox.FocusedItem != null)
			{
				CurrentListViewItem = StringListBox.FocusedItem;
				TranslatedTextBox.Text = StringTable.GetString(int.Parse(CurrentListViewItem.Text));
				OriginalTextBox.Text = StringTable.GetString(int.Parse(CurrentListViewItem.Text), StringTable.Default);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			StringListBox_MouseClick(null, null);
		}


		/// <summary>
		/// Change string number
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringListBox_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			// No change in the index value
			if (e.Label == null)
				return;

			// New id of the string
			int newid = int.Parse(e.Label);

			// Check if a string with the same id already exist
			if (!string.IsNullOrEmpty(StringTable.GetString(newid)))
			{
				if (MessageBox.Show("A string with this ID already exists ! Replace it ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				{
					e.CancelEdit = true;
					return;
				}
			}

			// ID of the string to edit
			int id = int.Parse(StringListBox.FocusedItem.Text);

			// Get string
			string str = StringTable.GetString(id);

			// Erase current string
			StringTable.SetString(id, string.Empty);


			// Create new string
			StringTable.SetString(newid, str);

		
		}

	
		/// <summary>
		/// Edit string number
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringListBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			StringListBox.FocusedItem.BeginEdit();
		}


		/// <summary>
		/// On ColumnHeader click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringListBox_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			ColumnHeader header = StringListBox.Columns[e.Column];

			if (header.ListView.Sorting == SortOrder.Ascending)
			{
				header.ListView.Sorting = SortOrder.Descending;
				Sorter.Order = SortOrder.Descending;
			}
			else
			{
				header.ListView.Sorting = SortOrder.Ascending;
				Sorter.Order = SortOrder.Ascending;
			}


			header.ListView.Sort();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearStringBox_Click(object sender, EventArgs e)
		{
			TranslatedTextBox.Text = string.Empty;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TranslatedTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				TranslatedTextBox.Text = StringTable.GetString(int.Parse(CurrentListViewItem.Text));
				TranslatedTextBox.SelectionStart = TranslatedTextBox.Text.Length;
				e.SuppressKeyPress = true;
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WriteStringBox_Click(object sender, EventArgs e)
		{
			// Update listview & backup string
			if (CurrentListViewItem != null && !string.IsNullOrEmpty(TranslatedTextBox.Text))
			{
				StringTable.SetString(int.Parse(CurrentListViewItem.Text), TranslatedTextBox.Text);
				CurrentListViewItem.SubItems[1].Text = TranslatedTextBox.Text;
				CurrentListViewItem.SubItems[0].ForeColor = Color.Black;
			}
		}


		/// <summary>
		/// Adds a new language
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddNewLanguageBox_Click(object sender, EventArgs e)
		{
			StringTable.AddLanguage(NewLanguageBox.Text);

			Build();

			CurrentLanguageBox.SelectedItem = NewLanguageBox.Text;
			NewLanguageBox.Text = string.Empty;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveLanguageBox_Click(object sender, EventArgs e)
		{
			StringTable.RemoveLanguage(CurrentLanguageBox.Text);

			Build();

			if (CurrentLanguageBox.Items.Count > 0)
				CurrentLanguageBox.SelectedItem = CurrentLanguageBox.Items[0];
		}



		#endregion



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return StringTable;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		StringTable StringTable;


		/// <summary>
		/// 
		/// </summary>
		ListViewItem CurrentListViewItem;


		/// <summary>
		/// Column sorter
		/// </summary>
		ListViewColumnSorter Sorter;

		#endregion




	}
}
