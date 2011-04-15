using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{

	/// <summary>
	/// Switch count control
	/// </summary>
	public partial class SwitchCountControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public SwitchCountControl()
		{
			InitializeComponent();
		}



		#region Properties


		/// <summary>
		/// Title of the control
		/// </summary>
		public string Title
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
