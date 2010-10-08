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
	/// Ability control
	/// </summary>
	public partial class AbilityControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AbilityControl()
		{
			InitializeComponent();
		}



		/// <summary>
		/// Rebuild interface
		/// </summary>
		void Rebuild()
		{
			if (Ability == null)
			{
				AbilityBox.Value = 0;
				ModifierBox.Text = "0";
			}
			else
			{
				AbilityBox.Value = Ability.Value;
			}
		}



		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AbilityBox_ValueChanged(object sender, EventArgs e)
		{
			if (Ability == null)
				return;

			Ability.Value = (int)AbilityBox.Value;
			ModifierBox.Text = Ability.Modifier.ToString();
		}

		#endregion


		#region Properties


		/// <summary>
		/// Ability title
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


		/// <summary>
		/// Ability
		/// </summary>
		public Ability Ability
		{
			get
			{
				return ability;
			}
			set
			{
				ability = value;
				Rebuild();
			}
		}
		Ability ability;

		#endregion


	}

}
