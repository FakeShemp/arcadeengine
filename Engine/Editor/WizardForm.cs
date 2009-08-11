using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Providers;
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
			foreach (Providers.Provider provider in ResourceManager.Providers)
			{
				foreach (Type type in provider.Assets)
				{
					TypesBox.Items.Add(type.Name);
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
			foreach (Providers.Provider provider in ResourceManager.Providers)
			{
				foreach (Type type in provider.Assets)
				{
					if (type.Name == TypesBox.Text)
					{

						// Invoke the generic method like this : provider.Add<[Asset Type]>(NameBox.Text, null);
						object[] args = { NameBox.Text, null };
						MethodInfo mi = provider.GetType().GetMethod("Add").MakeGenericMethod(type);
						mi.Invoke(provider, args);


						// Open the editor windows
						args = new object[] { NameBox.Text };
						mi = provider.GetType().GetMethod("EditAsset").MakeGenericMethod(type);
						AssetEditor form = mi.Invoke(provider, args) as AssetEditor;
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
