using ArcEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Graphic;


namespace DungeonEye.Forms
{
	public partial class MonsterForm : AssetEditor
	{


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node"></param>
		public MonsterForm(XmlNode node)
		{
			Monster = new Monster();
			Monster.Load(node);
		
			//InitControls();

		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				Monster.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<Monster>(Monster.Name, doc.DocumentElement);

		}



		#region Events

		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "Dungeon Editor", MessageBoxButtons.YesNoCancel);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}




		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Monster; ;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		Monster Monster;

		#endregion



	}
}
