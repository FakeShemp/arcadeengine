
using System;
using System.Collections.Generic;
using System.ComponentModel;
using RuffnTumble.Asset;
using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

namespace RuffnTumble.Editor.Wizards
{
	public partial class NewLevelWizard : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public NewLevelWizard()
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

			// Level already exists ?
			//if (ResourceManager.GetLevel(LevelName.Text) != null || LevelName.Text == "")
			//{
			//   MessageBox.Show("Level name already in use or invalid. Use another name !");
			//   e.Cancel = true;
			//   return;
			//}


			// Create the level
			//Level level = ResourceManager.CreateLevel(LevelName.Text);
			//if (level == null)
			//{
			//   e.Cancel = true;
			//   return;
			//}
			//level.Size = new Size((int)LevelWidthButton.Value, (int)LevelHeightButton.Value);

			//// Add the layer
			//Layer layer = level.AddLayer("layer_1");
			//if (TextureBox.SelectedItem != null)
			//   layer.TextureName = TextureBox.SelectedItem.ToString();
		}



		/// <summary>
		/// Desired size of the blocks
		/// </summary>
		Size NewBlockSize
		{
			get
			{
				return new Size((int)BlockWidthButton.Value, (int)BlockHeightButton.Value);
			}
		}


		VideoRender Device;


	}
}
