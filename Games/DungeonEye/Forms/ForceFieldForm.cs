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
	/// Force field form editor
	/// </summary>
	public partial class ForceFieldForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="field"></param>
		/// <param name="dungeon"></param>
		public ForceFieldForm(ForceField field, Dungeon dungeon)
		{
			InitializeComponent();

			switch (field.Type)
			{
				case ForceFieldType.Spin:
				SpinRadioBox.Checked = true;
				break;
				case ForceFieldType.Move:
				MoveRadioBox.Checked = true;
				break;
				case ForceFieldType.Block:
				BlockRadioBox.Checked = true;
				break;
			}

			AffectItemsBox.Checked = field.AffectItems;
			AffectMonstersBox.Checked = field.AffectMonsters;
			AffectTeamBox.Checked = field.AffectTeam;
			MoveDirectionBox.DataSource = Enum.GetValues(typeof(CardinalPoint));
			MoveDirectionBox.SelectedItem = field.Move;
			SpinDirectionBox.DataSource = Enum.GetValues(typeof(CompassRotation));
			SpinDirectionBox.SelectedItem = field.Spin;


			Field = field;
		}



		#region Form events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ForceFieldForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}


		private void MoveDirectionBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.Move = (CardinalPoint) MoveDirectionBox.SelectedItem;
		}

		private void SpinDirectionBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.Spin = (CompassRotation)SpinDirectionBox.SelectedItem;
		}

		private void MoveRadioBox_CheckedChanged(object sender, EventArgs e)
		{
			MoveDirectionBox.Visible = true;
			SpinDirectionBox.Visible = false;

			if (Field == null)
				return;

			Field.Type = ForceFieldType.Move;
		}

		private void SpinRadioBox_CheckedChanged(object sender, EventArgs e)
		{
			MoveDirectionBox.Visible = false;
			SpinDirectionBox.Visible = true;

			if (Field == null)
				return;

			Field.Type = ForceFieldType.Spin;
		}

		private void BlockBox_CheckedChanged(object sender, EventArgs e)
		{
			MoveDirectionBox.Visible = false;
			SpinDirectionBox.Visible = false;

			if (Field == null)
				return;

			Field.Type = ForceFieldType.Block;
		}

		private void AffectTeamBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.AffectTeam = AffectTeamBox.Checked;
		}

		private void AffectMonstersBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.AffectMonsters = AffectMonstersBox.Checked;
		}

		private void AffectItemsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.AffectItems = AffectItemsBox.Checked;
		}


		#endregion


		#region Properties


		/// <summary>
		/// 
		/// </summary>
		ForceField Field;

		#endregion



	}
}
