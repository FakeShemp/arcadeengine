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
	public partial class MonsterControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public MonsterControl()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Sets the monster to edit
		/// </summary>
		/// <param name="monster"></param>
		public void SetMonster(Monster monster)
		{
			Monster = monster;
			EntityBox.Entity = monster;
			UpdateControls();
		}


		/// <summary>
		/// Update controls
		/// </summary>
		void UpdateControls()
		{
			// Items
			ItemsBox.BeginUpdate();
			ItemsBox.Items.Clear();
			foreach (string item in ResourceManager.GetAssets<Item>())
				ItemsBox.Items.Add(item);
			ItemsBox.EndUpdate();


			// Scripts
			ScriptNameBox.BeginUpdate();
			foreach (string name in ResourceManager.GetAssets<Script>())
				ScriptNameBox.Items.Add(name);
			ScriptNameBox.Items.Insert(0, "");
			ScriptNameBox.EndUpdate();


			if (Monster == null)
			{
				ArmorClassBox.Value = 0;
				XPRewardBox.Value = 0;
				PocketItemsBox.Items.Clear();
				ScriptNameBox.Items.Clear();
				InterfaceNameBox.Items.Clear();
				TileSetBox.Items.Clear();
				TileIDBox.Items.Clear();
				DamageBox.Dice.Reset();
				if (TileSet != null)
				{
					TileSet.Dispose();
					TileSet = null;
				}
			}
			else
			{

				if (!string.IsNullOrEmpty(Monster.TileSetName))
					TileSet = ResourceManager.CreateAsset<TileSet>(Monster.TileSetName);


				UpdateTileControl();
				UpdateTilesControl();


				PocketItemsBox.BeginUpdate();
				PocketItemsBox.Items.Clear();
				foreach (string name in Monster.ItemsInPocket)
					PocketItemsBox.Items.Add(name);
				PocketItemsBox.EndUpdate();

				XPRewardBox.Value = Monster.Reward;
				ArmorClassBox.Value = Monster.ArmorClass;

				ScriptNameBox.SelectedItem = Monster.ScriptName;
				InterfaceNameBox.SelectedItem = Monster.InterfaceName;

				DamageBox.Dice = Monster.Damage;
			}
		}

		/// <summary>
		/// Update TileSet control
		/// </summary>
		void UpdateTilesControl()
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
		private void MonsterControl_Load(object sender, EventArgs e)
		{
			// Parent closing form
			if (!DesignMode)
			{
				ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
			}
		}

		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (TileSet != null)
			{
				TileSet.Dispose();
				TileSet = null;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Load(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.Init();
			Display.ClearBuffers();

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
			try
			{
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
			}
			finally
			{
				GlControl.SwapBuffers();
			}
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

			Monster.Damage.Clone(DamageBox.Dice);
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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExperienceBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;
			Monster.Reward = (int)XPRewardBox.Value;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ArmorClassBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.ArmorClass = (int)ArmorClassBox.Value;
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
		/// Tileset
		/// </summary>
		TileSet TileSet;



		/// <summary>
		/// Background texture
		/// </summary>
		Texture CheckerBoard;


		#endregion


	}
}
