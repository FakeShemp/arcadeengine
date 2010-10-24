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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Interface;


namespace ArcEngine.Editor
{
	/// <summary>
	/// Edit script
	/// </summary>
	internal partial class InputSchemeForm : AssetEditorBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public InputSchemeForm(XmlNode node)
		{
			InitializeComponent();



			Scheme = new InputScheme();
			Scheme.Load(node);

			KeyBox.BeginUpdate();
			foreach (string s in Enum.GetNames(typeof(Keys)))
				KeyBox.Items.Add(s);
			KeyBox.EndUpdate();


			BuildList();
		}


		/// <summary>
		/// Display all input
		/// </summary>
		void BuildList()
		{
			ListViewBox.BeginUpdate();
			ListViewBox.Items.Clear();

			foreach (string name in Scheme.Inputs)
			{
				ListViewItem item = new ListViewItem(name);
				item.SubItems.Add(Scheme[name].ToString());

				ListViewBox.Items.Add(item);
			}


			ListViewBox.EndUpdate();
		}



		/// <summary>
		/// Save the asset to the manager
		/// </summary>
		public override void Save()
		{

			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				Scheme.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<InputScheme>(Scheme.Name, doc.DocumentElement);
		}




		#region Events



		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyboardSchemeForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Keyboard Scheme Editor", "Save modifications ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}
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
				return Scheme;
			}
		}

		/// <summary>
		/// KeyboardScheme to edit
		/// </summary>
		InputScheme Scheme;

		#endregion



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			NameBox.Text = ListViewBox.FocusedItem.Text;
			KeyBox.SelectedItem = Scheme[ListViewBox.FocusedItem.Text].ToString();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Apply_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(NameBox.Text) || KeyBox.SelectedItem == null)
				return;

			Scheme.AddInput(NameBox.Text, (Keys)Enum.Parse(typeof(Keys), KeyBox.SelectedItem.ToString()));

			BuildList();
		}


		/// <summary>
		/// Removes an input
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Delete_Click(object sender, EventArgs e)
		{

			foreach(ListViewItem item in ListViewBox.SelectedItems)
				Scheme.RemoveInput(item.Text);

			BuildList();

			NameBox.Text = string.Empty;
			KeyBox.SelectedItem = null;
		}


		#endregion

	}




}
