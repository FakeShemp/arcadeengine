using System;
using System.Collections.Generic;
using System.ComponentModel;
using RuffnTumble.Asset;
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
		public NewLayerBrushWizard(LayerBrush brush, Layer layer)
		{
			InitializeComponent();

			Brush = brush;
			Layer = layer;
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


			// No name selected
			if (Layer.GetBrush(NameBox.Text) != null)
			{
				MessageBox.Show("LayerBrush name invalid, select another name");
				e.Cancel = true;
				return;
			}


			// Create the brush
			LayerBrush brush= Layer.CreateBrush(NameBox.Text);
			brush.Size = Brush.Size;
			brush.Tiles = new List<List<int>>(Brush.Tiles);
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
		/// Layer to work with
		/// </summary>
		Layer Layer;
		#endregion

	}
}
