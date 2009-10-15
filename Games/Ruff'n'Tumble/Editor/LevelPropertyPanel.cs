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
using ArcEngine;

using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Graphic;
using RuffnTumble;


namespace RuffnTumble.Editor
{
    public partial class LevelPropertyPanel : DockContent
	{
		public LevelPropertyPanel()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Init the form
		/// </summary>
		/// <param name="lvl">Level to edit</param>
		/// <returns></returns>
		public bool Init(WorldForm form)
		{
			if (form == null)
				return false;

			Form = form;

		//	PropertyBox.SelectedObject = Form.World.CurrentLevel;
			//LevelWidthLabel.Text = Form.World.CurrentLevel.Size.Width.ToString();
			//DesiredLevelWidth.Value = Form.World.CurrentLevel.Size.Width;
			//LevelHeightLabel.Text = Form.World.CurrentLevel.Size.Height.ToString();
			//DesiredLevelHeight.Value = Form.World.CurrentLevel.Size.Height;

			//BlockWidthLabel.Text = Form.World.CurrentLevel.BlockSize.Width.ToString();
			//DesiredBlockWidth.Value = Form.World.CurrentLevel.BlockSize.Width;
			//BlockHeightLabel.Text = Form.World.CurrentLevel.BlockSize.Height.ToString();
			//DesiredBlockHeight.Value = Form.World.CurrentLevel.BlockSize.Height;
			return true;
		}



		#region Events

		/// <summary>
		/// Resize the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeButton_Click(object sender, EventArgs e)
		{
			Form.World.CurrentLevel.Resize(new Size((int)DesiredLevelWidth.Value, (int)DesiredLevelHeight.Value));
		}


		/// <summary>
		///  Resize blocks
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeBlockButton_Click(object sender, EventArgs e)
		{
			Form.World.CurrentLevel.BlockSize = new Size((int)DesiredBlockWidth.Value, (int)DesiredBlockHeight.Value);
		}


		/// <summary>
		/// OnLevelPreview click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelPreviewButton_Click(object sender, EventArgs e)
		{
		//	ResourceManager.OpenRender(PreviewSize, false);


		}

		#endregion




		#region Properties



		/// <summary>
		/// Level form
		/// </summary>
		WorldForm Form;


		/// <summary>
		/// Level preview size
		/// </summary>
		public Size PreviewSize
		{
			get
			{
				return new Size((int)PreviewWidth.Value, (int)PreviewHeight.Value);
			}
			set
			{
				PreviewWidth.Value = value.Width;
				PreviewHeight.Value = value.Height;
			}
		}

		#endregion

	}
}
