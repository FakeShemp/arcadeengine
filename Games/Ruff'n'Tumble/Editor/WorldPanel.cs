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
	/// <summary>
	/// 
	/// </summary>
    public partial class WorldPanel : DockContent
	{
		public WorldPanel()
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

			UpdateLists();

			return true;
		}


		/// <summary>
		/// Update all informations
		/// </summary>
		void UpdateLists()
		{
			if (Form == null)
				return;

			LevelsBox.BeginUpdate();
			LevelsBox.Items.Clear();
			foreach (string name in Form.World.GetLevels())
				LevelsBox.Items.Add(name);
			LevelsBox.EndUpdate();


			if (Form.Level != null)
			{
				PropertyBox.SelectedObject = Form.Level;
			}
		}



		#region Events


		/// <summary>
		/// Change the current layer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LayersBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (LayersBox.SelectedIndex == 0)
				Form.CurrentLayer = Form.Level.TileLayer;
			else
				Form.CurrentLayer = Form.Level.CollisionLayer;

			OptionsBox.SetItemCheckState(0, Form.CurrentLayer.Visible ? CheckState.Checked : CheckState.Unchecked);
			OptionsBox.SetItemCheckState(1, Form.Level.RenderEntities ? CheckState.Checked : CheckState.Unchecked);
			OptionsBox.SetItemCheckState(2, Form.Level.RenderSpawnPoints ? CheckState.Checked : CheckState.Unchecked);

			AlphaBox.Value = Form.CurrentLayer.Alpha;
		}


		/// <summary>
		/// Layer properties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LayerPropertiesBox_Click(object sender, EventArgs e)
		{

		}

		
		/// <summary>
		/// Resize the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeButton_Click(object sender, EventArgs e)
		{
			if (Form.Level == null)
				return;

			new Wizards.LevelResizeWizard(Form.Level).ShowDialog();
			//Form.Level.Resize(new Size((int)DesiredLevelWidth.Value, (int)DesiredLevelHeight.Value));
		}


		/// <summary>
		///  Resize blocks
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeBlockButton_Click(object sender, EventArgs e)
		{
			if (Form.Level == null)
				return;

			//Form.Level.BlockSize = new Size((int)DesiredBlockWidth.Value, (int)DesiredBlockHeight.Value);
		}


		/// <summary>
		/// OnLevelPreview click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelPreviewButton_Click(object sender, EventArgs e)
		{
		//	ResourceManager.OpenRender(PreviewSize, false);

			if (Form.Level == null)
				return;

		}


		/// <summary>
		/// Change level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string name = LevelsBox.SelectedItem as string;
			Form.ChangeLevel(name);

			LayersBox.SelectedIndex = 0;

		//	UpdateLists();
		}


		/// <summary>
		/// Item check
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OptionsBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			switch (e.Index)
			{
				//Draw layer
				case 0:
				{
					Form.CurrentLayer.Visible = e.NewValue == CheckState.Checked;
				}
				break;

				// Draw entities
				case 1:
				{
					Form.Level.RenderEntities = e.NewValue == CheckState.Checked;
				}
				break;

				// Draw spawn points
				case 2:
				{
					Form.Level.RenderSpawnPoints = e.NewValue == CheckState.Checked;
				}
				break;


			}
		}

		/// <summary>
		/// Alpha layer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AlphaBox_Scroll(object sender, EventArgs e)
		{
			if (Form.CurrentLayer == null)
				return;
			Form.CurrentLayer.Alpha = (byte)AlphaBox.Value ;
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
