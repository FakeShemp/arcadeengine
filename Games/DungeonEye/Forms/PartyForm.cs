#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Editor;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Party form editor
	/// </summary>
	public partial class PartyForm : EditorFormBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public PartyForm()
		{
			InitializeComponent();
		}








		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "Hero Editor", MessageBoxButtons.YesNoCancel);

			if (result == DialogResult.Yes)
			{
				//Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}




		#region Properties



		#endregion
	}
}
