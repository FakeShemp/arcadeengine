using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Pit form editor
	/// </summary>
	public partial class PitForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pit"></param>
		/// <param name="dungeon"></param>
		public PitForm(Pit pit, Dungeon dungeon)
		{
			InitializeComponent();

			IsHiddenBox.Checked = pit.IsHidden;
			IsIllusionBox.Checked = pit.IsIllusion;
			TargetBox.Dungeon = dungeon;
			TargetBox.SetTarget(pit.Target);
			DamageBox.Dice = pit.Damage;
			DifficultyBox.Value = pit.Difficulty;

			Pit = pit;
		}


		#region Form events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PitForm_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.Escape)
				Close();
		}

		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		Pit Pit;

		/// <summary>
		/// 
		/// </summary>
		Dungeon Dungeon;

		#endregion

		private void TargetBox_TargetChanged(object sender, DungeonLocation location)
		{
			if (Pit == null)
				return;

			Pit.Target = location;
		}

		private void IsIllusionBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Pit == null)
				return;

			Pit.IsIllusion = IsIllusionBox.Checked;
		}

		private void IsHiddenBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Pit == null)
				return;

			Pit.IsHidden = IsHiddenBox.Checked;
		}

		private void DifficultyBox_ValueChanged(object sender, EventArgs e)
		{
			if (Pit == null)
				return;
			Pit.Difficulty = (int)DifficultyBox.Value;
		}

		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			if (Pit == null)
				return;

			Pit.Damage = DamageBox.Dice;
		}

	}
}
