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
using System.Windows.Forms;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class EditorForm : Form
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public EditorForm()
		{

			// Form initialize
			InitializeComponent();


			//Config.Load();


			// Log panel
			logPanel = new LogForm(this);
			logPanel.Show(dockPanel, DockState.DockBottomAutoHide);

			// Resource panel
			ResourcePanel = new ResourceForm();
			ResourcePanel.Show(dockPanel, DockState.DockLeft);
			ResourcePanel.RebuildResourceTree();


		}



		#region Menus envents

		/// <summary>
		/// Website
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WebSiteMenu_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.mimicprod.net/");

		}


		/// <summary>
		/// Collapse resource tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CollapseTreeMenu_Click(object sender, EventArgs e)
		{
			ResourcePanel.CollapseTree();
		}


		/// <summary>
		/// Quit the application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}


		//
		// About - About
		//
		private void MenuAboutAbout_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}




		/// <summary>
		///  Adds resource to the manager
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuTool_AddExistingResource_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Resource bank (*.bnk)|*.bnk|All Files (*.*)|*.*";
			dlg.Title = "Select resource to open...";
			dlg.DefaultExt = ".bnk";

			DialogResult res = dlg.ShowDialog();
			if (res != DialogResult.OK)
				return;

			// Stay back in the good directory
			Environment.CurrentDirectory = Application.StartupPath;


			ResourceManager.LoadBank(dlg.FileName);

			ResourcePanel.RebuildResourceTree();

		}


		/// <summary>
		/// Closes the selected TabPage from the menu File/Close
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseCurrentTab_OnClick(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// Closes all open TabPage 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseAllTabs_OnClick(object sender, EventArgs e)
		{
			List<DockContent> windows = new List<DockContent>();

			foreach (DockContent window in dockPanel.Contents)
				windows.Add(window);

			foreach (DockContent window in windows)
			{
				if (window is AssetEditor)
					window.Close();
			}
		}


		/// <summary>
		/// Import an animation from a GIF image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportAnimationFromGIF(object sender, EventArgs e)
		{
			new ImportGIFForm().ShowDialog();
			ResourcePanel.RebuildResourceTree();

		}


		/// <summary>
		/// Saves the resource under a new name from menu File/Save As
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileSaveAs_OnClick(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Resource bank (*.bnk)|*.bnk|All Files (*.*)|*.*";
			dlg.Title = "Save resource as...";
			dlg.DefaultExt = ".bnk";
			dlg.RestoreDirectory = true;
			dlg.AddExtension = true;
			dlg.CheckPathExists = true;
			dlg.OverwritePrompt = true;
			dlg.ValidateNames = true;

			DialogResult res = dlg.ShowDialog();
			if (res != DialogResult.OK)
				return;

			BankName = dlg.FileName;

			ResourceManager.SaveResources(BankName);

			ResourcePanel.RebuildResourceTree();

		}


		#endregion


		#region Form Events


		/// <summary>
		/// Expand resource tree 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExpandTreeMenu_Click(object sender, EventArgs e)
		{
			ResourcePanel.ExpandTree();
		}





		/// <summary>
		/// Load resources from a file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenBank(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Resource bank (*.bnk)|*.bnk|All Files (*.*)|*.*";
			dlg.Title = "Select resource to open...";
			dlg.DefaultExt = ".bnk";
			dlg.RestoreDirectory = true;
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Multiselect = false;


			DialogResult res = dlg.ShowDialog();
			if (res != DialogResult.OK)
				return;

			// Erases all resources
			ResourceManager.ClearAssets();
			CloseAllTabs_OnClick(null, null);

			// Load bank
			BankName = dlg.FileName;
			ResourceManager.LoadBank(BankName);

			dlg.Dispose();
			dlg = null;

			ResourcePanel.RebuildResourceTree();

			//if (Log.ErrorCount > 0)
			//{
			//    ReportLabel.Text = Log.ErrorCount + " Error(s) occured !";
			//}
			//else
			//    ReportLabel.Text = "";
		}


		/// <summary>
		/// Save all resources to a bank
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveResourcesToFile(object sender, EventArgs e)
		{
			// Save all opened asset
			foreach (DockContent window in dockPanel.Contents)
			{
				if (window is AssetEditor)
				{
					AssetEditor asseteditor = window as AssetEditor;
					asseteditor.Save();
				}
			}




			if (string.IsNullOrEmpty(BankName))
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Resource bank (*.bnk)|*.bnk|All Files (*.*)|*.*";
				dlg.Title = "Save resource as...";
				dlg.DefaultExt = ".bnk";
				dlg.RestoreDirectory = true;
				dlg.AddExtension = true;
				dlg.CheckPathExists = true;
				dlg.OverwritePrompt = true;
				dlg.ValidateNames = true;

				DialogResult res = dlg.ShowDialog();
				if (res != DialogResult.OK)
					return;

				BankName = dlg.FileName;
			}


			ResourceManager.SaveResources(BankName);

			//RebuildResourceTree();
		}




		/// <summary>
		/// Creates an empty resource bank
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateNewBank(object sender, EventArgs e)
		{
			DialogResult res = MessageBox.Show("Erase all resources ?", "", MessageBoxButtons.YesNo);

			if (res != DialogResult.Yes)
				return;

			CloseAllTabs_OnClick(null, null);
			ResourceManager.ClearAssets();

			// New bank name
			BankName = "";

			//
			ResourcePanel.RebuildResourceTree();
		}



		/// <summary>
		/// Shows preference dialog box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreferenceMenu_Click(object sender, EventArgs e)
		{
			PreferencesForm pref = new PreferencesForm();
			if (pref.ShowDialog() == DialogResult.Cancel)
				return;

		}


		/// <summary>
		/// Adds a binary to the bank
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddBinary_OnClick(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "All Files (*.*)|*.*";
			dlg.Title = "Select a binary file...";
			dlg.DefaultExt = ".*";
			dlg.Multiselect = true;
			dlg.RestoreDirectory = true;

			DialogResult res = dlg.ShowDialog();
			if (res != DialogResult.OK)
				return;

			// for each selected file, add it to the bank file
			for (int i = 0; i < dlg.FileNames.Length; i++)
				ResourceManager.LoadBinary(dlg.FileNames[i]);

			ResourcePanel.RebuildResourceTree();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
		{


			CloseAllTabs_OnClick(null, null);

			//foreach (DockContent window in list)
			//{
			//   if (window is AssetEditor)
			//      window.Close();
			//}
		}


		#endregion


		#region Add new asset

		/// <summary>
		/// Adds a binary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddBinaryMenu_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "All Files (*.*)|*.*";
			dlg.Title = "Select a binary file...";
			dlg.DefaultExt = ".*";
			dlg.Multiselect = true;
			dlg.RestoreDirectory = true;

			DialogResult res = dlg.ShowDialog();
			if (res != DialogResult.OK)
				return;

			// for each selected file, add it to the bank file
			for (int i = 0; i < dlg.FileNames.Length; i++)
				ResourceManager.LoadResource(dlg.SafeFileNames[i]);

			ResourcePanel.RebuildResourceTree();
		}


		/// <summary>
		/// Adds a bank to the resourcemanager
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InsertBankMenu_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Resource bank (*.bnk)|*.bnk|All Files (*.*)|*.*";
			dlg.Title = "Select resource to open...";
			dlg.DefaultExt = ".bnk";

			if (dlg.ShowDialog() != DialogResult.OK)
				return;

			ResourceManager.LoadBank(dlg.FileName);

			ResourcePanel.RebuildResourceTree();
		}



		/// <summary>
		/// Close the bank
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuFileClose_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// Creates a new asset
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewAssetBox_Click(object sender, EventArgs e)
		{
			new WizardForm(dockPanel).ShowDialog();
			ResourcePanel.RebuildResourceTree();

		}



		#endregion


		#region Properties

		/// <summary>
		/// Resource panel
		/// </summary>
		ResourceForm ResourcePanel;


		/// <summary>
		/// Log panel
		/// </summary>
		LogForm logPanel;


		/// <summary>
		/// Name of the opened bank
		/// </summary>
		public string BankName;


		/// <summary>
		/// DockPanel for asset forms
		/// </summary>
		public DockPanel AssetPanel
		{
			get
			{
				return dockPanel;
			}
		}

	
		#endregion

	}
}