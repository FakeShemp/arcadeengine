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
using ArcEngine;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Monster editor control
	/// </summary>
	public partial class MonsterEditorControl : UserControl
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public MonsterEditorControl()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Sets the monster to edit
		/// </summary>
		/// <param name="monster">Monster handle</param>
		public void SetMonster(Monster monster)
		{
			if (monster == null)
				return;

			MonsterBox.Monster = monster;
			Monster = monster;
		}


		/// <summary>
		/// Apply a model to the monster
		/// </summary>
		private void ApplyMonster()
		{
			Monster.Load(ResourceManager.GetAsset<Monster>((string)MonsterModelsBox.SelectedItem));
			MonsterBox.Monster = Monster;
		}


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MonsterEditorControl_Load(object sender, EventArgs e)
		{
			MonsterModelsBox.Items.AddRange(ResourceManager.GetAssets<Monster>().ToArray());
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ApplyModelBox_Click(object sender, EventArgs e)
		{
			ApplyMonster();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MonsterModelsBox_DoubleClick(object sender, EventArgs e)
		{
			ApplyMonster();
		}



		#endregion


		#region Properties


		/// <summary>
		/// Monster handle
		/// </summary>
		Monster Monster;

		#endregion

	}
}
