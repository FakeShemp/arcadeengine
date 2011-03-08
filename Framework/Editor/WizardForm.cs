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
using System.Reflection;
using System.Windows.Forms;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class WizardForm : Form
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dockpanel"></param>
		public WizardForm(DockPanel dockpanel)
		{
			Dockpanel = dockpanel;

			InitializeComponent();


			// Collect all asset type
			TypesBox.SuspendLayout();
			TypesBox.Items.Clear();
			//foreach (Provider provider in ResourceManager.Providers)
			{
				//foreach (Type type in provider.Assets)
				foreach(RegisteredAsset ra in ResourceManager.RegisteredAssets)
				{
					TypesBox.Items.Add(ra.Tag);
				}
			}

			TypesBox.ResumeLayout();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Wizard_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
				return;

			
			// Invalid name
			if (string.IsNullOrEmpty(NameBox.Text))
			{
				MessageBox.Show("Asset name invalid. Use another name !");
				e.Cancel = true;
				return;
			}


			
			// Create the asset
			//foreach (Provider provider in ResourceManager.Providers)
			{
				//foreach (Type type in provider.Assets)
				foreach (RegisteredAsset ra in ResourceManager.RegisteredAssets)
				{
					if (ra.Tag == TypesBox.Text)
					{

						// Invoke the generic method like this : provider.Add<[Asset Type]>(NameBox.Text, null);
						//object[] args = { NameBox.Text, null };
						//MethodInfo mi = provider.GetType().GetMethod("Add").MakeGenericMethod(type);
						//mi.Invoke(provider, args);
						ra.Add(NameBox.Text, null);

						// Open the editor windows
						//args = new object[] { NameBox.Text };
						//mi = provider.GetType().GetMethod("EditAsset").MakeGenericMethod(type);
						//AssetEditorBase form = mi.Invoke(provider, args) as AssetEditorBase;
						AssetEditorBase form = ra.Edit(NameBox.Text);
						if (form == null)
							return;

						// Give a name to the asset
						form.Asset.Name = NameBox.Text;

						// Show the form
						form.DockHandler.Show(Dockpanel, DockState.Document);

						return;
					}
				}
			}


		}




		#region Properties


		/// <summary>
		/// Dockpanel
		/// </summary>
		DockPanel Dockpanel;


		#endregion


	}
}
