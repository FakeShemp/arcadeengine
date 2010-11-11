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
	/// 
	/// </summary>
	public partial class TeleporterForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public TeleporterForm(Teleporter teleporter)
		{
			InitializeComponent();


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
				targetControl1.Dungeon = teleporter.Square.Location.Dungeon;
				targetControl1.SetTarget(teleporter.Square.Location.MazeName, teleporter.Square.Location.Coordinate);
				
			}

			Teleporter = teleporter;
		}


		#region Events

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

		#endregion


		#region Properties

		/// <summary>
		/// Teleporter
		/// </summary>
		Teleporter Teleporter;

		#endregion


	}
}
