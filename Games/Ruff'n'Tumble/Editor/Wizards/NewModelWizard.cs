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
//using System.Datat;
using System.Drawing;

using System.Text;
using System.Windows.Forms;



namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
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
