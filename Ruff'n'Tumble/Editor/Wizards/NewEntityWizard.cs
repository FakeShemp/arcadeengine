using System;
using System.Collections.Generic;
using System.ComponentModel;
using RuffnTumble.Asset;
using System.Drawing;
using ArcEngine.Asset;
using System.Text;
using System.Windows.Forms;





namespace RuffnTumble.Editor.Wizards
{
	public partial class NewEntityWizard : Form
	{

		public NewEntityWizard(Layer layer, Point pos)
		{
			InitializeComponent();

			Layer = layer;
			Position = pos;

			// Gathers model names
			ModelBox.Items.Clear();
			//foreach (string name in ResourceManager.GetModels())
			//   ModelBox.Items.Add(name);

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


			// No model selected
			if (string.IsNullOrEmpty(ModelBox.Text))
			{
				MessageBox.Show("Select a model first!");
				e.Cancel = true;
				return;
			}

			// Entity already exists ?
			if (string.IsNullOrEmpty(EntityBox.Text) || Layer.GetEntity(EntityBox.Text) != null)
			{
				MessageBox.Show("Entity name already in use or invalid. Use another name !");
				e.Cancel = true;
				return;
			}


			// Create entity
			Entity ent = Layer.AddEntity(EntityBox.Text);
			if (ent == null)
				return;

			ent.ModelName = ModelBox.Text;
			ent.Location = Layer.Level.ScreenToLevel(Position);
			ent.TileId = 0;

		}

		/// <summary>
		/// Current layer
		/// </summary>
		Layer Layer;


		/// <summary>
		/// Location of the entity in the level
		/// </summary>
		Point Position;

	}
}
