#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Forms;
using DungeonEye;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Party form editor
	/// </summary>
	public partial class PartyForm : EditorFormBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public PartyForm()
		{
			InitializeComponent();

			Team = new Team(null);

			LoadParty();
		}


		/// <summary>
		/// Select a hero from his position in the team
		/// </summary>
		/// <param name="position"></param>
		void SelectHero(HeroPosition position)
		{
			if (Team == null)
			{
				return;
			}

			Hero hero = Team.GetHeroFromPosition(position);
			if (hero == null)
			{
				HeroBox.Hero = null;
				HeroBox.Enabled = false;
				RemoveHeroBox.Enabled = false;
				CreateHeroBox.Enabled = true;
			}
			else
			{
				HeroBox.Enabled = true;
				HeroBox.Hero = hero;
				RemoveHeroBox.Enabled = true;
				CreateHeroBox.Enabled = false;
			}
		}


		/// <summary>
		/// Rebuild interface
		/// </summary>
		void Rebuild()
		{
			if (Team == null)
				return;


			#region Heroes


			#endregion


			#region Messages

			MessageListBox.BeginUpdate();
			MessageListBox.Items.Clear();
			foreach (ScreenMessage msg in Team.Messages)
				MessageListBox.Items.Add(msg.Message);
			MessageListBox.EndUpdate();

			#endregion
		}


		/// <summary>
		/// Load a party
		/// </summary>
		void LoadParty()
		{
			if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

			if (string.IsNullOrEmpty(openFileDialog1.FileName))
				return;

			XmlDocument xml = new XmlDocument();
			xml.Load(openFileDialog1.FileName);

			foreach (XmlNode node in xml)
			{
				if (node.Name.ToLower() == "team")
					Team.Load(node);
			}
			xml = null;

			Rebuild();
			SelectHero(HeroPosition.FrontLeft);
		}


		/// <summary>
		/// Save the party
		/// </summary>
		void SaveParty()
		{
			if (Team == null)
				return;

			if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

			if (string.IsNullOrEmpty(saveFileDialog1.FileName))
				return;

			Team.SaveParty(saveFileDialog1.FileName);
		}



		#region Main form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PartyForm_Load(object sender, EventArgs e)
		{
			FrontLeftBox.Checked = true;
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "Hero Editor", MessageBoxButtons.YesNoCancel);
			
			if (result == DialogResult.Yes)
			{
				SaveParty();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}

		#endregion


		#region Heroe's events

		/// <summary>
		/// Adds a Hero
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateHeroBox_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// Removes a Hero
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveHeroBox_Click(object sender, EventArgs e)
		{
			if (Team == null)
				return;

			HeroPosition pos = HeroPosition.FrontLeft;
			if (FrontLeftBox.Checked)
				pos = HeroPosition.FrontLeft;
			if (FrontRightBox.Checked)
				pos = HeroPosition.FrontRight;
			if (MiddleLeftBox.Checked)
				pos = HeroPosition.MiddleLeft;
			if (MiddleRightBox.Checked)
				pos = HeroPosition.MiddleRight;
			if (RearLeftBox.Checked)
				pos = HeroPosition.RearLeft;
			if (RearRightBox.Checked)
				pos = HeroPosition.RearRight;

			Team.RemoveHero(pos);
			SelectHero(pos);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrontLeftBox_CheckedChanged(object sender, EventArgs e)
		{
			SelectHero(HeroPosition.FrontLeft);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrontRightBox_CheckedChanged(object sender, EventArgs e)
		{
			SelectHero(HeroPosition.FrontRight);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MiddleRightBox_CheckedChanged(object sender, EventArgs e)
		{
			SelectHero(HeroPosition.MiddleRight);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MiddleLeftBox_CheckedChanged(object sender, EventArgs e)
		{
			SelectHero(HeroPosition.MiddleLeft);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RearLeftBox_CheckedChanged(object sender, EventArgs e)
		{
			SelectHero(HeroPosition.RearLeft);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RearRightBox_CheckedChanged(object sender, EventArgs e)
		{
			SelectHero(HeroPosition.RearRight);
		}

		#endregion


		#region Messages

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MessageColorBox_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearMessageBox_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteMessageBox_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddMessageBox_Click(object sender, EventArgs e)
		{

		}

		#endregion


		#region Properties

		/// <summary>
		/// Team
		/// </summary>
		Team Team;

		#endregion

	}
}
