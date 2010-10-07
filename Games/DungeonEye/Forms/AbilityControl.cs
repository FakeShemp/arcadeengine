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
	/// 
	/// </summary>
	public partial class AbilityControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		public AbilityControl()
		{
			InitializeComponent();
		}



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			get
			{
				return TitleBox.Text;
			}
			set
			{
				TitleBox.Text = value;
			}
		}

		#endregion

	}

}
