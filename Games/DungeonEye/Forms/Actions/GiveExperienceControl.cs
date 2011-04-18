using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.Script.Actions;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Gives experience to the team
	/// </summary>
	public partial class GiveExperienceControl : ActionControlBase
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="script"></param>
		public GiveExperienceControl(GiveExperience script)
		{
			InitializeComponent();


			if (script != null)
				Action = script;
			else
				Action = new GiveExperience();

			UpdateUI();
		}


		/// <summary>
		/// Updates user interface
		/// </summary>
		void UpdateUI()
		{
			GiveExperience script = Action as GiveExperience;
			if (script == null)
				return;

			AmountBox.Value = script.Amount;
		}


		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AmountBox_ValueChanged(object sender, EventArgs e)
		{
			(Action as GiveExperience).Amount = (int)AmountBox.Value;
		}

		#endregion


		#region Properties


		#endregion
	}
}
