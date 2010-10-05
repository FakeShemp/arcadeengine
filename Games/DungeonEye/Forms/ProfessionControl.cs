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
	public partial class ProfessionControl : UserControl
	{
		public ProfessionControl()
		{
			InitializeComponent();

			ClassBox.BeginUpdate();
			ClassBox.DataSource = Enum.GetNames(typeof(HeroClass));
			ClassBox.EndUpdate();
		}


		#region Properties


		/// <summary>
		/// Gets or sets hero Class
		/// </summary>
		public HeroClass Class
		{
			get
			{
				return (HeroClass) Enum.Parse(typeof(HeroClass), (string)ClassBox.SelectedItem);
			}
			set
			{
				ClassBox.SelectedItem = value;
			}
		}


		#endregion
	}
}
