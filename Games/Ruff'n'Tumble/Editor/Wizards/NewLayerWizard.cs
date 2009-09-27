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
using ArcEngine.Games.RuffnTumble.Asset;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	/// <summary>
	/// 
	/// </summary>
	public partial class NewLayerWizard : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public NewLayerWizard()
		{
			InitializeComponent();
/*
			// Populate the texture available
			List<string> textures = ResourceManager.GetAssets<Texture>();
			TextureBox.Items.Add("");
			foreach (string texture in textures)
			{
				TextureBox.Items.Add(texture);
			}

*/
		}


		/// <summary>
		/// FormClosing event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			// 
			if (DialogResult != DialogResult.OK)
				return;

			Level level = (Level)Tag;

			// Level already exists ?
			if (level.GetLayer(LayerNameBox.Text) != null || LayerNameBox.Text == "")
			{
				MessageBox.Show("Layer name already in use or invalid. Use another name !");
				e.Cancel = true;
			}

			Layer layer = level.AddLayer(LayerNameBox.Text);
			if (layer != null)
			{
				layer.TextureName = TextureBox.Text;
				layer.Init();
			}


		}


	}
}
