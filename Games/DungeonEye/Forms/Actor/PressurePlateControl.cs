using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Pressure plate control editor
	/// </summary>
	public partial class PressurePlateControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pressureplate">Pressure plate handle</param>
		/// <param name="dungeon">Dungeon handle</param>
		public PressurePlateControl(PressurePlate pressureplate, Dungeon dungeon)
		{
			InitializeComponent();

			//ActionBox.Actions = pressureplate.Actions;
			ActionBox.Dungeon = dungeon;
			
			PressurePlate = pressureplate;
		}


		/// <summary>
		/// Update user interface
		/// </summary>
		void UpdateUI()
		{
			HiddenBox.Checked = PressurePlate.IsHidden;
		}


		#region Control events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PressurePlateControl_Load(object sender, EventArgs e)
		{

			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HiddenBox_CheckedChanged(object sender, EventArgs e)
		{
			if (PressurePlate == null)
				return;

			PressurePlate.IsHidden = HiddenBox.Checked;
		}



		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		PressurePlate PressurePlate;


		#endregion


	}
}
