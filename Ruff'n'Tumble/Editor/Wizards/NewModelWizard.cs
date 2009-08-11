using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Datat;
using System.Drawing;

using System.Text;
using System.Windows.Forms;



namespace RuffnTumble.Editor.Wizards
{
	public partial class NewModelWizard : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public NewModelWizard()
		{
			InitializeComponent();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewModelWizard_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
				return;

			// Model already exists ?
			//if (ResourceManager.GetModel(ModelNameBox.Text) != null || ModelNameBox.Text == "")
			//{
			//   MessageBox.Show("Model name already in use or invalid. Use another name !");
			//   e.Cancel = true;
			//   return;
			//}


			//ResourceManager.CreateModel(ModelNameBox.Text);

		}


	}
}
