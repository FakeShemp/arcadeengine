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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BinaryForm : AssetEditor
	{
		/// <summary>
		/// constructor
		/// </summary>
		public BinaryForm()
		{
			InitializeComponent();

			BuildList();
		}


		/// <summary>
		/// Build up the Binary list
		/// </summary>
		void BuildList()
		{
			ListViewBox.BeginUpdate();
			ListViewBox.Items.Clear();

			foreach (string name in ResourceManager.LoadedBinaries)
			{
				BinaryInformation info = new BinaryInformation(name);

				ListViewItem item = new ListViewItem(name);
				//item.SubItems.Add((info.Size / 1024).ToString("# ### Ko")).BackColor = Color.LightBlue;
				item.SubItems.Add(info.GetFileSizeAsString()).BackColor = Color.LightBlue;
				item.SubItems.Add("type").BackColor = Color.LightBlue;
				item.SubItems.Add("date").BackColor = Color.LightBlue;

				ListViewBox.Items.Add(item);
			}

			ListViewBox.EndUpdate();
		}



	}


}
