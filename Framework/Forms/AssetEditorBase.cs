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
using ArcEngine.Asset;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Interface;
using System.Windows.Forms;

namespace ArcEngine.Forms
{

	/// <summary>
	/// Base classe for asset editor
	/// </summary>
	public class AssetEditorBase : DockContent
	{

		/// <summary>
		/// 
		/// </summary>
		public AssetEditorBase()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Save the asset
		/// </summary>
		public virtual void Save()
		{
			//throw new System.NotImplementedException("Asset");
		}


		/// <summary>
		/// 
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// AssetEditorBase
			// 
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.Name = "AssetEditorBase";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssetEditorBase_FormClosing);
			this.ResumeLayout(false);

		}


		#region Events

		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AssetEditorBase_FormClosing(object sender, FormClosingEventArgs e)
		{

			DialogResult result = MessageBox.Show("Save modifications ?", "Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}

			Dispose();
		}

		#endregion


		#region Properties


		/// <summary>
		/// Gets the edited asset 
		/// </summary>
		public virtual IAsset Asset
		{
			get
			{
				throw new System.NotImplementedException("Asset");
			}
		}

		#endregion
	}
}
