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
		/// Constructor
		/// </summary>
		/// <param name="monster">Monster's handle</param>
		public MonsterEditorForm(Monster monster)
		{
			InitializeComponent();

			MonsterBox.Monster = monster;
			Monster = monster;
		}



		/// <summary>
		/// Apply a model to the monster
		/// </summary>
		private void ApplyMonster()
		{
			Monster.Load(ResourceManager.GetAsset<Monster>((string) MonsterModelsBox.SelectedItem));
			MonsterBox.Monster = Monster;
		}


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MonsterEditorForm_Load(object sender, EventArgs e)
		{
			MonsterModelsBox.DataSource = ResourceManager.GetAssets<Monster>();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MonsterEditorForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				Close();
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
