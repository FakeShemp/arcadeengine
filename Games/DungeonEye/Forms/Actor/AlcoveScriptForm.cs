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
using ArcEngine;


namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class AlcoveScriptForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dungeon">Dungeon handle</param>
		/// <param name="script">Script handle to edit</param>
		public AlcoveScriptForm(Dungeon dungeon, AlcoveScript script)
		{
			InitializeComponent();

			if (script == null)
				Script = new AlcoveScript();
			else
				Script = script;

			Dungeon = dungeon;
		}



		/// <summary>
		/// 
		/// </summary>
		void UpdateUI()
		{

			ItemNameBox.SelectedItem = Script.ItemName;
			ConsumeItemBox.Checked = Script.ConsumeItem;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AlcoveScriptForm_Load(object sender, EventArgs e)
		{
			ItemNameBox.DataSource = ResourceManager.GetAssets<Item>();

			UpdateUI();
		}



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public AlcoveScript Script
		{
			get;
			private set;
		}
		

		/// <summary>
		/// Dungeon handle
		/// </summary>
		Dungeon Dungeon;

		#endregion

	}
}
