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

using System.Drawing;
using RuffnTumble.Asset;
using System.Text;
using System.Windows.Forms;


namespace RuffnTumble.Editor.Wizards
{
	public partial class LevelResizeWizard : Form
	{
		public LevelResizeWizard(Level lvl)
		{

			InitializeComponent();


			Level = lvl;



			LevelWidthLabel.Text = Level.Width.ToString();
			LevelHeightLabel.Text = Level.Height.ToString();

			DesiredWidth.Value = Level.Width;
			DesiredHeight.Value = Level.Height;

		}



		#region Events


		/// <summary>
		/// Resize the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeButton_Click(object sender, EventArgs e)
		{
			Level.Size = new Size((int)DesiredWidth.Value, (int)DesiredHeight.Value);
		}


		#endregion



		#region Properties

		/// <summary>
		/// Level to resize
		/// </summary>
		Level Level;

		#endregion

	}
}
