#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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

namespace ArcEngine.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class AutoUpdateForm : Form
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pv">Product version handle</param>
		public AutoUpdateForm(ProductVersion pv)
		{
			InitializeComponent();

			if (pv == null)
			{
				Close();
				return;
			}


			label5.Text = "A new product version is available for " + pv.Name;
			CurrentVersionBox.Text = Application.ProductVersion;
			RecentVersionBox.Text = pv.Version;
			UriBox.Text = pv.Url;
		}


		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpgradeNowBox_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(UriBox.Text);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UriBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(UriBox.Text);
		}

		#endregion


		#region Properties

		#endregion
	}
}
