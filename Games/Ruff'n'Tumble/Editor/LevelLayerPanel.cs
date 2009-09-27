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
using ArcEngine.Games.RuffnTumble.Asset;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Graphic;
using ArcEngine.Asset;


using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Games.RuffnTumble.Editor
{
	public partial class LevelLayerPanel : DockContent
	{
		/// <summary>
		/// 
		/// </summary>
		public LevelLayerPanel()
		{
			InitializeComponent();
		}






		/// <summary>
		/// Initialization
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public bool Init(LevelForm form)
		{
			Form = form;

			UpdateLayerBox();

			// Update Layerbox
			if (Form.Level.Layers.Count > 0 && LayersBox.Items.Count > 0)
			{
				CurrentLayer = Form.Level.GetLayer(LayersBox.Items[0].ToString());
				LayersBox.SelectedIndex = 0;
			}

			return true;
		}


		/// <summary>
		/// Updates LayerBox content
		/// </summary>
		void UpdateLayerBox()
		{
			// LayerBox
			LayersBox.Items.Clear();
			foreach (Layer layer in Form.Level.Layers)
			{
				LayersBox.Items.Add(layer.Name);
			}

			if (CurrentLayer != null)
				LayersBox.SelectedItem = CurrentLayer.Name;

		}



		#region Events


		/// <summary>
		/// Changes the current layer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LayerBox_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			// Change the current layer
			currentLayer = Form.Level.GetLayer(LayersBox.SelectedItem.ToString());

			LayerPropertyGrid.SelectedObject = CurrentLayer;

			//ShowEntityButton.Checked = CurrentLayer.RenderEntities;
			//RebuildSpawnPointsList(null, null);
			//RebuildEntityList(null, null);

			propertyGridBox.SelectedObject = null;
		}


		/// <summary>
		/// Adds a layer to the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolAddLayer_Click(object sender, EventArgs e)
		{

			Wizards.NewLayerWizard dlg = new Wizards.NewLayerWizard();
		//	dlg.Tag = Form.Level;

			dlg.ShowDialog();

			UpdateLayerBox();
		}


		/// <summary>
		/// Finds the currently edited object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FindButton_Click(object sender, EventArgs e)
		{
			Point pos = Point.Empty;
/*
			if (PropertyGridBox.SelectedObject is Entity)
			{
				Entity entity = PropertyGridBox.SelectedObject as Entity;

				pos.X = (entity.Location.X - Video.ViewPort.Width / 2) / Form.Level.BlockSize.Width;
				pos.Y = (entity.Location.Y - Video.ViewPort.Height / 2) / Form.Level.BlockSize.Height;
			}
			else if (PropertyGridBox.SelectedObject is SpawnPoint)
			{
				SpawnPoint spawn = PropertyGridBox.SelectedObject as SpawnPoint;

				pos.X = (spawn.Location.X - Video.ViewPort.Width / 2) / Form.Level.BlockSize.Width;
				pos.Y = (spawn.Location.Y - Video.ViewPort.Height / 2) / Form.Level.BlockSize.Height;
			}
			else
				return;
*/			
			Form.ScrollLayer(pos);
		}




		/// <summary>
		/// Removes current layer of the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolRemoveLayer_Click(object sender, EventArgs e)
		{
			//if (MessageBox.Show("Do you really want to remove layer \"" + currentLayer.Name + "\" ?", "", MessageBoxButtons.YesNo)
			//   != DialogResult.Yes)
			//   return;

	//		Form.Level.RemoveLayer(currentLayer);


			// Double update of the LayerBox
			UpdateLayerBox();
		//	currentLayer = Form.Level.GetLayer((string)LayersBox.Items[0]);
			UpdateLayerBox();
		}


		/// <summary>
		/// Deletes the currently edited object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			//if (PropertyGridBox.SelectedObject is Entity)
			//{
			//   Entity entity = PropertyGridBox.SelectedObject as Entity;
			//   CurrentLayer.RemoveEntity(entity.Name);
			//}
			//else if (PropertyGridBox.SelectedObject is SpawnPoint)
			//{
			//   SpawnPoint spawn = PropertyGridBox.SelectedObject as SpawnPoint;
			//   CurrentLayer.RemoveSpawnPoint(spawn.Name);
			//}
			//else
			//   return;

			PropertyGridBox.SelectedObject = null;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Parent form
		/// </summary>
		LevelForm Form;


		/// <summary>
		/// Gets/sets the current layer
		/// </summary>
		public Layer CurrentLayer
		{
			get
			{
				return currentLayer;
			}

			set
			{
				currentLayer = value;
				UpdateLayerBox();
			}
		}
		Layer currentLayer;


		/// <summary>
		/// PropertyGrid for object edition
		/// </summary>
		public PropertyGrid PropertyGridBox
		{
			get
			{
				return propertyGridBox;
			}
		}



		#endregion
	}
}
