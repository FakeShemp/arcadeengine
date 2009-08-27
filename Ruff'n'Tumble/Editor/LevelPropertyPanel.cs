using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ArcEngine;

using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Graphic;
using RuffnTumble.Asset;


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
		public bool Init(LevelForm form)
		{
			if (form == null)
				return false;

			Form = form;

			PropertyBox.SelectedObject = Form.Level;
			LevelWidthLabel.Text = Form.Level.Width.ToString();
			DesiredLevelWidth.Value = Form.Level.Width;
			LevelHeightLabel.Text = Form.Level.Height.ToString();
			DesiredLevelHeight.Value = Form.Level.Height;

			BlockWidthLabel.Text = Form.Level.BlockSize.Width.ToString();
			DesiredBlockWidth.Value = Form.Level.BlockSize.Width;
			BlockHeightLabel.Text = Form.Level.BlockSize.Height.ToString();
			DesiredBlockHeight.Value = Form.Level.BlockSize.Height;
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
			Form.Level.Resize(new Size((int)DesiredLevelWidth.Value, (int)DesiredLevelHeight.Value));
		}


		/// <summary>
		///  Resize blocks
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeBlockButton_Click(object sender, EventArgs e)
		{
			Form.Level.BlockSize = new Size((int)DesiredBlockWidth.Value, (int)DesiredBlockHeight.Value);
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
		LevelForm Form;


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
