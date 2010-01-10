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

		public event EventHandler ValueChanged;


		#endregion


		#region Form events


		/// <summary>
		/// On value changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnValueChanged(object sender, EventArgs e)
		{
			CalculateMinMax();

			if (ValueChanged != null)
				ValueChanged(this, null);
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
				BaseBox.Value = value.Base;
				FacesBox.Value = value.Faces;
				ThrowBox.Value = value.Throws;
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
