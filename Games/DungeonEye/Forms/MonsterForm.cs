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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;

namespace DungeonEye.Forms
{
	public partial class MonsterForm : AssetEditor
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		public MonsterForm(XmlNode node)
		{
			InitializeComponent();

			Monster = new Monster(null);
			Monster.Load(node);
			MonsterBox.SetMonster(Monster);
		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			ResourceManager.AddAsset<Monster>(Monster.Name, ResourceManager.ConvertAsset(Monster));
		}



		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "Monster Editor", MessageBoxButtons.YesNoCancel);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Monster;
			}
		}


		public Monster Monster
		{
			get;
			set;
		}

		#endregion


	}
}
