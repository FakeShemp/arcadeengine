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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.Script;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Control that handle actions
	/// </summary>
	public partial class ActionListControl : UserControl
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public ActionListControl()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Update action list
		/// </summary>
		void UpdateActionList()
		{
			ActionsBox.BeginUpdate();

			ActionsBox.Items.Clear();

			foreach (ScriptBase action in Actions)
				ActionsBox.Items.Add(action.Name);

			ActionsBox.EndUpdate();
		}


		/// <summary>
		/// 
		/// </summary>
		void EditAction()
		{
			if (ActionsBox.SelectedIndex == -1)
				return;


			if (Actions is List<AlcoveScript>)
			{
			}
			else
			{
			}
			EventActionForm form = new EventActionForm(Dungeon);
			ScriptBase action = Actions[ActionsBox.SelectedIndex];
			form.SetAction(action);
			form.ShowDialog();

			UpdateActionList();
		}



		#region Form events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionsBox_DoubleClick(object sender, EventArgs e)
		{
			EditAction();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveUpActionBox_Click(object sender, EventArgs e)
		{
			if (Actions == null)
				return;

			int id = ActionsBox.SelectedIndex;
			if (id <= 0)
				return;
			ScriptBase action = Actions[id];
			Actions.RemoveAt(id);
			Actions.Insert(id - 1, action);

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
			if (Actions == null)
				return;

			int id = ActionsBox.SelectedIndex;
			if (id >= Actions.Count - 1)
				return;

			ScriptBase action = Actions[id];
			Actions.RemoveAt(id);
			Actions.Insert(id + 1, action);

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
			if (Actions == null)
				return;

			if (ActionsBox.SelectedIndex == -1 || ActionsBox.SelectedIndex > Actions.Count)
				return;

			Actions.RemoveAt(ActionsBox.SelectedIndex);

			UpdateActionList();
		}


		/// <summary>
		/// Adds a new action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddActionBox_Click(object sender, EventArgs e)
		{
			// Empty form
			EventActionForm form = new EventActionForm(Dungeon);
			if (form.ShowDialog() != DialogResult.OK)
				return;

			// Add new action to the list
			if (form.Action != null && Actions != null)
				Actions.Add(form.Action);

			UpdateActionList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditBox_Click(object sender, EventArgs e)
		{
			EditAction();
		}

		#endregion



		#region Properties

		/// <summary>
		/// Actions collection
		/// </summary>
		public IList<ScriptBase> Actions
		{
			get
			{
				return actions;
			}
			set
			{
				if (value == null)
					return;

				actions = value;
				UpdateActionList();
			}
		}
		IList<ScriptBase> actions;


		/// <summary>
		/// Dungeon handle
		/// </summary>
		public Dungeon Dungeon
		{
			get;
			set;
		}


		/// <summary>
		/// Text
		/// </summary>
		public string Title
		{
			get
			{
				return groupBox1.Text;
			}
			set
			{
				groupBox1.Text = value;
			}
		}


		#endregion

	}
}
