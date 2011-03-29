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
	public partial class ForceFieldControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="field"></param>
		/// <param name="dungeon"></param>
		public ForceFieldControl(ForceField field, Dungeon dungeon)
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
		private void MoveDirectionBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.Move = (CardinalPoint) MoveDirectionBox.SelectedItem;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpinDirectionBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.Spin = (CompassRotation)SpinDirectionBox.SelectedItem;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveRadioBox_CheckedChanged(object sender, EventArgs e)
		{
			MoveDirectionBox.Visible = true;
			SpinDirectionBox.Visible = false;

			if (Field == null)
				return;

			Field.Type = ForceFieldType.Move;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpinRadioBox_CheckedChanged(object sender, EventArgs e)
		{
			MoveDirectionBox.Visible = false;
			SpinDirectionBox.Visible = true;

			if (Field == null)
				return;

			Field.Type = ForceFieldType.Spin;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BlockBox_CheckedChanged(object sender, EventArgs e)
		{
			MoveDirectionBox.Visible = false;
			SpinDirectionBox.Visible = false;

			if (Field == null)
				return;

			Field.Type = ForceFieldType.Block;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AffectTeamBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.AffectTeam = AffectTeamBox.Checked;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AffectMonstersBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Field == null)
				return;

			Field.AffectMonsters = AffectMonstersBox.Checked;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
