#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Interface;
using DungeonEye.Script;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Manages scripts for an Alcove
	/// </summary>
	public partial class AlcoveScriptListControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		public AlcoveScriptListControl()
		{
			InitializeComponent();
		}



		/// <summary>
		/// 
		/// </summary>
		void UpdateUI()
		{
			ScriptListBox.Items.Clear();
			if (Actions == null)
				return;


		}


		#region Control events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveDownBox_Click(object sender, EventArgs e)
		{
			if (Actions == null)
				return;

			int id = ScriptListBox.SelectedIndex;
			if (id >= Actions.Count - 1)
				return;

			AlcoveScript action = Actions[id];
			Actions.RemoveAt(id);
			Actions.Insert(id + 1, action);

			UpdateUI();
			ScriptListBox.SelectedIndex = id + 1;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveUpBox_Click(object sender, EventArgs e)
		{
			if (Actions == null)
				return;

			int id = ScriptListBox.SelectedIndex;
			if (id <= 0)
				return;
			AlcoveScript script = Actions[id];
			Actions.RemoveAt(id);
			Actions.Insert(id - 1, script);

			UpdateUI();
			ScriptListBox.SelectedIndex = id - 1;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveBox_Click(object sender, EventArgs e)
		{
			if (Actions == null)
				return;

			if (ScriptListBox.SelectedIndex == -1 || ScriptListBox.SelectedIndex > Actions.Count)
				return;

			Actions.RemoveAt(ScriptListBox.SelectedIndex);

			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditBox_Click(object sender, EventArgs e)
		{
			if (ScriptListBox.SelectedIndex == -1)
				return;


			new AlcoveScriptForm(Dungeon, Actions[ScriptListBox.SelectedIndex]).ShowDialog();

			UpdateUI();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddBox_Click(object sender, EventArgs e)
		{
			// Empty form
			AlcoveScriptForm form = new AlcoveScriptForm(Dungeon, null);
			if (form.ShowDialog() != DialogResult.OK)
				return;

			// Add new action to the list
			if (form.Script != null && Actions != null)
				Actions.Add(form.Script);

			UpdateUI();
		}

		#endregion


		#region Properties


		/// <summary>
		/// Title of the control
		/// </summary>
		public string Title
		{
			get
			{
				return groupBox5.Text;
			}
			set
			{
				groupBox5.Text = value;
			}
		}


		/// <summary>
		/// Dungeon handle
		/// </summary>
		public Dungeon Dungeon
		{
			get;
			set;
		}


		/// <summary>
		/// Actions
		/// </summary>
		public IList<AlcoveScript> Actions
		{
			get
			{
				return actions;
			}
			set
			{
				actions = value;
				UpdateUI();
			}
		}
		IList<AlcoveScript> actions;

		#endregion



	}
}
