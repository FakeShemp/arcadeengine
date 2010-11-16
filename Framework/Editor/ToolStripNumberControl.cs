using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Text;

namespace ArcEngine.Forms
{
	/// <summary>
	/// NumericUpDown in ToolStrip
	/// </summary>
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public class ToolStripNumberControl : NumericUpDownControlHost
	{
		/// <summary>
		/// 
		/// </summary>
		public ToolStripNumberControl() : base(new NumericUpDown())
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="control"></param>
		protected override void OnSubscribeControlEvents(Control control)
		{
			base.OnSubscribeControlEvents(control);
			((NumericUpDown) control).ValueChanged += new EventHandler(OnValueChanged);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="control"></param>
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
			((NumericUpDown) control).ValueChanged -= new EventHandler(OnValueChanged);
		}


		/// <summary>
		/// 
		/// </summary>
		public Control NumericUpDown
		{
			get
			{
				return Control as NumericUpDown;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public decimal Value
		{
			get
			{
				return (Control as NumericUpDown).Value;
			}
			set
			{
				(Control as NumericUpDown).Value = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public decimal Maximum
		{
			get
			{
				return (Control as NumericUpDown).Maximum;
			}
			set
			{
				(Control as NumericUpDown).Maximum = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public decimal Minimum
		{
			get
			{
				return (Control as NumericUpDown).Minimum;
			}
			set
			{
				(Control as NumericUpDown).Minimum = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public event EventHandler ValueChanged;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnValueChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(this, e);
		}
	}


	/// <summary>
	/// 
	/// </summary>
	public class NumericUpDownControlHost : ToolStripControlHost
	{

		/// <summary>
		/// 
		/// </summary>
		public NumericUpDownControlHost() : base(new Control())
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		public NumericUpDownControlHost(Control c) : base(c)
		{

		}

	}
}
