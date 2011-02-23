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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.EventScript;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Event choice form editor
	/// </summary>
	public partial class EventChoiceForm : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="choice">choice to edit</param>
		public EventChoiceForm(ScriptChoice choice)
		{
			InitializeComponent();

			if (choice == null)
				throw new ArgumentNullException("choice");

			NameBox.Text = choice.Name;

			Choice = choice;

			UpdateActionList();
		}



		/// <summary>
		/// Update action list
		/// </summary>
		void UpdateActionList()
		{
			ActionsBox.BeginUpdate();

			ActionsBox.Items.Clear();

			foreach (IScriptAction action in Choice.Actions)
				ActionsBox.Items.Add(action.Name);

			ActionsBox.EndUpdate();
		}



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EventChoiceForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			if (Choice == null)
				return;

			Choice.Name = NameBox.Text;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveUpActionBox_Click(object sender, EventArgs e)
		{
			int id = ActionsBox.SelectedIndex;
			if (id <= 0)
				return;
			IScriptAction action = Choice.Actions[id];
			Choice.Actions.RemoveAt(id);
			Choice.Actions.Insert(id -1, action);

			UpdateActionList();
			ActionsBox.SelectedIndex = id - 1;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveDownActionBox_Click(object sender, EventArgs e)
		{
			int id = ActionsBox.SelectedIndex;
			if (id >= Choice.Actions.Count - 1)
				return;

			IScriptAction action = Choice.Actions[id];
			Choice.Actions.RemoveAt(id);
			Choice.Actions.Insert(id + 1, action);

			UpdateActionList();
			ActionsBox.SelectedIndex = id + 1;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveActionBox_Click(object sender, EventArgs e)
		{
			if (ActionsBox.SelectedIndex == -1 || ActionsBox.SelectedIndex > Choice.Actions.Count)
				return;

			Choice.Actions.RemoveAt(ActionsBox.SelectedIndex);

			UpdateActionList();
		}


		/// <summary>
		/// Adds a new action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddActionBox_Click(object sender, EventArgs e)
		{
			EventActionForm form =  new EventActionForm();
			if (form.ShowDialog() != DialogResult.OK)
				return;

			if (form.Script != null)
				Choice.Actions.Add(form.Script);
	
			UpdateActionList();
		}

	
		private void AddItemBox_Click(object sender, EventArgs e)
		{

		}

		private void RemoveItemBox_Click(object sender, EventArgs e)
		{

		}

		private void KeepItemBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void VisibleBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void AutoTriggerBox_CheckedChanged(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionsBox_DoubleClick(object sender, EventArgs e)
		{
			EventActionForm form =  new EventActionForm();

			IScriptAction action = Choice.Actions[ActionsBox.SelectedIndex];

			form.SetAction(action);
			
			form.ShowDialog();

			UpdateActionList();
		}


		#endregion


		#region Properties

		ScriptChoice Choice;

		#endregion

	}
}
