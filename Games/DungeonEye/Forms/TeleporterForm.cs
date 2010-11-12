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
	/// Teleporter form editor
	/// </summary>
	public partial class TeleporterForm : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="teleporter">Teleporter handle</param>
		/// <param name="dungeon">Dungeon handle</param>
		public TeleporterForm(Teleporter teleporter, Dungeon dungeon)
		{
			InitializeComponent();

			targetControl1.Dungeon = dungeon;

			TeamBox.Checked = teleporter.CanTeleportTeam;
			ItemsBox.Checked = teleporter.CanTeleportItems;
			MonsterBox.Checked = teleporter.CanTeleportMonsters;
			VisibleBox.Checked = teleporter.IsVisible;
			ReusableBox.Checked = teleporter.Reusable;
			ActiveBox.Checked = teleporter.IsActive;
			UseSoundBox.Checked = teleporter.UseSound;
			SoundNameBox.Text = teleporter.SoundName;

			if (teleporter != null)
			{
				targetControl1.SetTarget(teleporter.Target);
				
			}

			Teleporter = teleporter;
		}


		#region Events

		private void TeleporterForm_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.Escape)
				Close();
		}

		private void TeamBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.CanTeleportTeam = TeamBox.Checked;
		}

		private void MonsterBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.CanTeleportMonsters = MonsterBox.Checked;
		}

		private void ItemsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.CanTeleportItems = ItemsBox.Checked;
		}

		private void VisibleBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.IsVisible = VisibleBox.Checked;
		}

		private void ReusableBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.Reusable = ReusableBox.Checked;
		}

		private void ActiveBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.IsActive = ActiveBox.Checked;
		}

		private void PlaySoundBox_Click(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

		}

		private void SoundNameBox_TextChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.SoundName = SoundNameBox.Text;
		}

		private void LoadSoundBox_Click(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

		}

		private void UseSoundBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Teleporter == null)
				return;

			Teleporter.UseSound = UseSoundBox.Checked;
		}


		private void targetControl1_TargetChanged(object sender, DungeonLocation location)
		{
			if (Teleporter == null)
				return;

			Teleporter.Target = location;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Teleporter
		/// </summary>
		Teleporter Teleporter;


		#endregion



	}
}
