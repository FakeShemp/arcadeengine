using RuffnTumble.Asset;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

namespace RuffnTumble.Editor.Wizards
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

			// Populate the texture available
			List<string> textures = ResourceManager.GetAssets(typeof(Texture));
			TextureBox.Items.Add("");
			foreach (string texture in textures)
			{
				TextureBox.Items.Add(texture);
			}


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
