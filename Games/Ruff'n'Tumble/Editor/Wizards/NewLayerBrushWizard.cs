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
using RuffnTumble;
using System.Drawing;
using System.Text;
using ArcEngine.Asset;
using System.Windows.Forms;


namespace RuffnTumble.Editor.Wizards
{
	public partial class NewLayerBrushWizard : Form
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public NewLayerBrushWizard(LayerBrush brush, Level level)
		{
			InitializeComponent();

			Brush = brush;
			Level = level;
		}



		/// <summary>
		/// OnFormClosing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewLayerBrushWizard_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 
			if (DialogResult != DialogResult.OK)
				return;

/*
			// No name selected
			if (Layer.GetBrush(NameBox.Text) != null)
			{
				MessageBox.Show("LayerBrush name invalid, select another name");
				e.Cancel = true;
				return;
			}
*/

			// Create the brush
			//LayerBrush brush= Layer.CreateBrush(NameBox.Text);
			//brush.Size = Brush.Size;
			//brush.Tiles = new List<List<int>>(Brush.Tiles);
		}



		/// <summary>
		/// Shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewLayerBrushWizard_Shown(object sender, EventArgs e)
		{
			NameBox.Focus();
		}

	
		
		#region Properties


		/// <summary>
		/// LayerBrush to process
		/// </summary>
		LayerBrush Brush;


		/// <summary>
		/// Level to work with
		/// </summary>
		Level Level;


		#endregion

	}
}
