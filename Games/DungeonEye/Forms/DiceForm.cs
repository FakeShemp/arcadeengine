using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Dice control form
	/// </summary>
	public partial class DiceForm : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public DiceForm()
		{
			InitializeComponent();

			Dice = new Dice((int)ThrowBox.Value, (int)FacesBox.Value, (int)BaseBox.Value);
			CalculateMinMax();
		}



		/// <summary>
		/// 
		/// </summary>
		void CalculateMinMax()
		{
			MinimumBox.Text = Dice.Minimum.ToString();
			MaximumBox.Text = Dice.Maximum.ToString();
		}



		#region Events


		/// <summary>
		/// On value changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnValueChanged(object sender, EventArgs e)
		{
			Dice.Base = (int)BaseBox.Value;
			Dice.Faces = (int)FacesBox.Value;
			Dice.Throws = (int)ThrowBox.Value;
			CalculateMinMax();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Dice
		/// </summary>
		public Dice Dice
		{
			get
			{
				return new Dice((int)ThrowBox.Value, (int)FacesBox.Value, (int)BaseBox.Value);
			}
			set
			{
				if (value == null)
					return;
				Dice.Base = value.Base;
				Dice.Faces = value.Faces;
				Dice.Throws = value.Throws;
			}
		}


		/// <summary>
		/// Text to display
		/// </summary>
		public string ControlText
		{
			get
			{
				return groupBox1.Text;
			}
			set
			{
				groupBox1.Text = value;
			}
		}

		#endregion

	}
}
