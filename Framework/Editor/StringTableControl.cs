using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine;

namespace DungeonEye.Forms
{

	/// <summary>
	/// StringTable control
	/// </summary>
	public partial class StringTableControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public StringTableControl()
		{
			InitializeComponent();


		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringTableControl_Load(object sender, EventArgs e)
		{
			if (this.DesignMode)
				return;

			RefreshAvailableStringTables();
		}




		/// <summary>
		/// Gets all available stringtables
		/// </summary>
		void RefreshAvailableStringTables()
		{
			object previous = StringTableBox.SelectedItem;

			StringTableBox.BeginUpdate();
			StringTableBox.Items.Clear();
			StringTableBox.Items.Add("");
			StringTableBox.Items.AddRange(ResourceManager.GetAssets<StringTable>().ToArray());
			StringTableBox.EndUpdate();

			if (previous != null && StringTableBox.Items.Contains(previous))
				StringTableBox.SelectedItem = previous;
		}


		/// <summary>
		/// Refresh preview string label
		/// </summary>
		void RefreshPreviewString()
		{
			if (Table == null)
				return;

			PreviewBox.Text = Table.GetString(StringID);

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		void ChangeLanguage(string name)
		{
			if (Table == null)
				return;

			if (string.IsNullOrEmpty(name))
			{
				Table.LanguageName = Table.Default;
				name = Table.Default;
			}
			else
				Table.LanguageName = name;

			if (LanguageBox.Items.Contains(name))
				LanguageBox.SelectedItem = name;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshBox_Click(object sender, EventArgs e)
		{
			RefreshAvailableStringTables();
			RefreshPreviewString();
		}


		/// <summary>
		/// Change string ID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringIDBox_ValueChanged(object sender, EventArgs e)
		{
			RefreshPreviewString();
		}


		/// <summary>
		/// Change String Table
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StringTableBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string previouslanguage = (string)LanguageBox.SelectedItem;

			Table = ResourceManager.CreateAsset<StringTable>((string)StringTableBox.SelectedItem);

			LanguageBox.BeginUpdate();
			LanguageBox.Items.Clear();
			if (Table != null)
				LanguageBox.Items.AddRange(Table.LanguagesList.ToArray());
			LanguageBox.EndUpdate();

			ChangeLanguage(previouslanguage);

			RefreshPreviewString();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Table != null)
				Table.LanguageName = (string)LanguageBox.SelectedItem;

			RefreshPreviewString();
		}



		#region Properties

		/// <summary>
		/// Title of the control
		/// </summary>
		public string Title
		{
			get
			{
				return GroupBox.Text;
			}
			set
			{
				GroupBox.Text = value;
			}
		}

		/// <summary>
		/// Shows / hides the preview string
		/// </summary>
		public bool ShowPreview
		{
			get
			{
				return PreviewBox.Visible;
			}
			set
			{
				PreviewBox.Visible = value;
				PreviewLabelBox.Visible = value;
			}
		}


		/// <summary>
		/// Translated string
		/// </summary>
		public string TranslatedString
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets or sets the string ID
		/// </summary>
		public int StringID
		{
			get
			{
				return (int)StringIDBox.Value;
			}
			set
			{
				StringIDBox.Value = value;
			}
		}



		/// <summary>
		/// StringTable handle
		/// </summary>
		StringTable Table;


		#endregion
	}
}
