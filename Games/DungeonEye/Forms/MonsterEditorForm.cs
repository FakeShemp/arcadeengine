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
	/// Monster editor form
	/// </summary>
	public partial class MonsterEditorForm : Form
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		public MonsterEditorForm(Monster monster)
		{
			InitializeComponent();

			if (monster == null)
			{
				Close();
				return;
			}

			MonsterBox.SetMonster(monster);

			Monster = monster;
		}



		#region Form events



		private void MonsterEditorForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
		}


		private void ApplyModelBox_Click(object sender, EventArgs e)
		{
			Monster = ResourceManager.CreateAsset<Monster>((string)MonsterModelsBox.SelectedItem);
			MonsterBox.SetMonster(Monster);
		}


		private void MonsterModelsBox_DoubleClick(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Load(ResourceManager.GetAsset<Monster>((string)MonsterModelsBox.SelectedItem));
			MonsterBox.SetMonster(Monster);
		}

		private void DoneBox_Click(object sender, EventArgs e)
		{
			Close();
		}


		private void MonsterEditorForm_Load(object sender, EventArgs e)
		{
			MonsterModelsBox.DataSource = ResourceManager.GetAssets<Monster>();
		}

		#endregion

		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Monster Monster;

		#endregion
	}
}
