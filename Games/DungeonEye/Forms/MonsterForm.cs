#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using DungeonEye.Interfaces;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Monster control editor
	/// </summary>
	public partial class MonsterForm : AssetEditor
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MonsterForm(XmlNode node)
		{
			InitializeComponent();

			Monster = new Monster();
			Monster.Load(node);
		}


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns>True if success</returns>
		public bool Init()
		{
			// Items
			ItemsBox.BeginUpdate();
			ItemsBox.Items.Clear();
			foreach (string item in ResourceManager.GetAssets<Item>())
				ItemsBox.Items.Add(item);
			ItemsBox.EndUpdate();


			ScriptNameBox.BeginUpdate();
			foreach (string name in ResourceManager.GetAssets<Script>())
				ScriptNameBox.Items.Add(name);
			ScriptNameBox.Items.Insert(0, "");
			ScriptNameBox.EndUpdate();

			UpdateControls();

			return true;
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



		/// <summary>
		/// Update controls
		/// </summary>
		void UpdateControls()
		{
			if (Monster == null)
			{
				PocketItemsBox.Items.Clear();
				HPActualBox.Value = 0;
				HPMaxBox.Value = 0;
				Visible = false;
				return;
			}


			if (!string.IsNullOrEmpty(Monster.TileSetName))
				TileSet = ResourceManager.CreateAsset<TileSet>(Monster.TileSetName);


			UpdateTileControl();
			UpdateTileSetControl();


			PocketItemsBox.BeginUpdate();
			PocketItemsBox.Items.Clear();
			foreach (string name in Monster.ItemsInPocket)
				PocketItemsBox.Items.Add(name);
			PocketItemsBox.EndUpdate();

			HPActualBox.Value = Monster.Life.Actual;
			HPMaxBox.Value = Monster.Life.Max;

			ScriptNameBox.SelectedItem = Monster.ScriptName;
			InterfaceNameBox.SelectedItem = Monster.InterfaceName;
		}


		/// <summary>
		/// Update TileSet control
		/// </summary>
		void UpdateTileSetControl()
		{
			TileSetBox.BeginUpdate();
			TileSetBox.Items.Clear();
			foreach (string name in ResourceManager.GetAssets<TileSet>())
			{
				TileSetBox.Items.Add(name);
			}
			if (Monster != null)
				TileSetBox.Text = Monster.TileSetName;
			TileSetBox.EndUpdate();
		}


		/// <summary>
		/// Update TileSet control
		/// </summary>
		void UpdateTileControl()
		{
			TileIDBox.BeginUpdate();
			TileIDBox.Items.Clear();
			if (TileSet != null)
			{
				foreach (int id in TileSet.Tiles)
				{
					TileIDBox.Items.Add(id);
				}
				if (Monster != null)
					TileIDBox.SelectedItem = Monster.Tile;
			}
			TileIDBox.EndUpdate();

		}



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Load(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.Init();

			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.TileSetName = TileSetBox.SelectedItem as string;


			TileSet = ResourceManager.CreateAsset<TileSet>(Monster.TileSetName);
			UpdateTileControl();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControl.Size);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ClearBuffers();

			// Background texture
			CheckerBoard.Blit(new Rectangle(Point.Empty, GlControl.Size), TextureLayout.Tile);

			if (Monster == null)
				return;
	
			if (TileSet != null)
			{
				Tile tile = TileSet.GetTile(Monster.Tile);
				Point pos = new Point((GlControl.Width - tile.Size.Width) / 2 + tile.Size.Width / 2, (GlControl.Height - tile.Size.Height) / 2 + tile.Size.Height);
				TileSet.Draw(Monster.Tile, pos);
			}
			GlControl.SwapBuffers();
		}


		/// <summary>
		/// Change tileset ID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileIDBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;
			

			Monster.Tile = (int)TileIDBox.SelectedItem;
			GlControl.Invalidate();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddPocketItemBox_Click(object sender, EventArgs e)
		{
			if (Monster == null || ItemsBox.SelectedItem == null)
				return;
			
			Monster.ItemsInPocket.Add(ItemsBox.SelectedItem as string);
			PocketItemsBox.Items.Add(ItemsBox.SelectedItem as string);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemovePocketItemBox_Click(object sender, EventArgs e)
		{
			if (Monster == null || PocketItemsBox.SelectedItem == null)
				return;
			

			PocketItemsBox.Items.RemoveAt(PocketItemsBox.SelectedIndex);

			Monster.ItemsInPocket.Clear();
			foreach (string name in PocketItemsBox.Items)
				Monster.ItemsInPocket.Add(name);


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HPActualBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;
			Monster.Life.Actual = (short)HPActualBox.Value;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HPMaxBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Life.Max = (short)HPMaxBox.Value;
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MonsterForm_Load(object sender, EventArgs e)
		{
			Init();
		}


		/// <summary>
		/// Change script
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScriptNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ScriptNameBox.SelectedIndex == -1 || Monster == null)
				return;

			Monster.ScriptName = ScriptNameBox.SelectedItem as string;
			RebuildFoundInterfaces();
		}


		/// <summary>
		/// Rebuild found interfaces
		/// </summary>
		void RebuildFoundInterfaces()
		{
			InterfaceNameBox.Items.Clear();


			Script script = ResourceManager.CreateAsset<Script>(Monster.ScriptName);
			if (script != null)
			{
				List<string> list = script.GetImplementedInterfaces(typeof(IMonster));
				InterfaceNameBox.Items.AddRange(list.ToArray());

			}
			InterfaceNameBox.Items.Insert(0, "");
		}


		/// <summary>
		/// Change script interface
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InterfaceNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (InterfaceNameBox.SelectedIndex == -1 || Monster == null)
				return;

			Monster.InterfaceName = (string)InterfaceNameBox.SelectedItem;
		}


		/// <summary>
		/// Damage value changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Damage.Base = DamageBox.Dice.Base;
			Monster.Damage.Throws= DamageBox.Dice.Throws;
			Monster.Damage.Faces = DamageBox.Dice.Faces;
		}


		/// <summary>
		/// Remove selected item from backpack
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PocketItemsBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (Monster == null)
				return;

			PocketItemsBox.Items.RemoveAt(PocketItemsBox.SelectedIndex);

			Monster.ItemsInPocket.Clear();
			foreach (string name in PocketItemsBox.Items)
				Monster.ItemsInPocket.Add(name);
		}

		#endregion


		#region Properties

		/// <summary>
		/// Current monster
		/// </summary>
		public Monster Monster
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		TileSet TileSet;


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


		/// <summary>
		/// Background texture
		/// </summary>
		Texture CheckerBoard;


		#endregion



	}
}
