using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class RenameForm : Form
	{
		public RenameForm()
		{
			InitializeComponent();
		}



		/// <summary>
		/// Desired name
		/// </summary>
		public string DesiredName
		{
			get
			{
				return DesiredNameBox.Text;
			}
			set
			{
				DesiredNameBox.Text = value;
			}
		}
	}
}
