using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ArcEngine.Editor
{
	internal partial class PreferencesForm : Form
	{
		public PreferencesForm()
		{
			InitializeComponent();



			//LevelRefreshRateButton.Value = Config.LevelRefreshRate;
		}



		/// <summary>
		/// OnFormClosing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e)
		{

			if (DialogResult == DialogResult.Cancel)
				return;

			// Apply settings
			//Config.LevelRefreshRate = (int)LevelRefreshRateButton.Value;


			//if (DialogResult == DialogResult.OK)
			//	Config.Save();
		}


	}
}
