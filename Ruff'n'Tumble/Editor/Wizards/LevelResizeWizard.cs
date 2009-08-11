using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using RuffnTumble.Asset;
using System.Text;
using System.Windows.Forms;


namespace RuffnTumble.Editor.Wizards
{
	public partial class LevelResizeWizard : Form
	{
		public LevelResizeWizard(Level lvl)
		{

			InitializeComponent();


			Level = lvl;



			LevelWidthLabel.Text = Level.Width.ToString();
			LevelHeightLabel.Text = Level.Height.ToString();

			DesiredWidth.Value = Level.Width;
			DesiredHeight.Value = Level.Height;

		}



		#region Events


		/// <summary>
		/// Resize the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeButton_Click(object sender, EventArgs e)
		{
			Level.Size = new Size((int)DesiredWidth.Value, (int)DesiredHeight.Value);
		}


		#endregion



		#region Properties

		/// <summary>
		/// Level to resize
		/// </summary>
		Level Level;

		#endregion

	}
}
