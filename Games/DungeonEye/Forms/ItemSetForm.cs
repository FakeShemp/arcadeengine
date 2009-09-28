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
using ArcEngine.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Text;


namespace DungeonEye.Forms
{
	public partial class ItemSetForm : AssetEditor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node">node to edit</param>
		public ItemSetForm(XmlNode node)
		{
			InitializeComponent();


			ItemSet = new ItemSet();
			ItemSet.Load(node);


			// TileSetNameBox
			TileSetNameBox.BeginUpdate();
			TileSetNameBox.Items.Clear();
			foreach (string name in ResourceManager.GetAssets<TileSet>())
			{
				TileSetNameBox.Items.Add(name);
			}
			TileSetNameBox.EndUpdate();
			if (!string.IsNullOrEmpty(ItemSet.TileSetName) && TileSetNameBox.Items.Contains(ItemSet.TileSetName))
				TileSetNameBox.SelectedItem = ItemSet.TileSetName;



			// TileSetNameBox
			ScriptNameBox.BeginUpdate();
			ScriptNameBox.Items.Clear();
			foreach (string name in ResourceManager.GetAssets<Script>())
			{
				ScriptNameBox.Items.Add(name);
			}

			ScriptNameBox.EndUpdate();

			if (!string.IsNullOrEmpty(ItemSet.ScriptName) && ScriptNameBox.Items.Contains(ItemSet.ScriptName))
				ScriptNameBox.SelectedItem = ItemSet.ScriptName;
			

			RebuildLists();
			RebuildTilesList();

		}



		/// <summary>
		/// Rebuild availble tileset
		/// </summary>
		void RebuildLists()
		{

			// Items 
			ItemsBox.BeginUpdate();
			ItemsBox.Items.Clear();
			foreach (KeyValuePair<string, Item> kvp in ItemSet.Items)
				ItemsBox.Items.Add(kvp.Key);
			ItemsBox.EndUpdate();

			// Types
			TypeBox.BeginUpdate();
			TypeBox.Items.Clear();
			foreach(string name in Enum.GetNames(typeof(ItemType)))
			{
				TypeBox.Items.Add(name);
			}
			TypeBox.EndUpdate();


		}


		/// <summary>
		/// When changing TileSet, rebuild tiles list
		/// </summary>
		void RebuildTilesList()
		{

			// Tiles
			GroundTileBox.BeginUpdate();
			GroundTileBox.Items.Clear();
			InventoryTileBox.BeginUpdate();
			InventoryTileBox.Items.Clear();
			MoveAwayTileBox.BeginUpdate();
			MoveAwayTileBox.Items.Clear();
			IncomingTileBox.BeginUpdate();
			IncomingTileBox.Items.Clear();
			if (TileSet != null)
			{
				List<int> id = TileSet.GetTiles();
				foreach (int i in id)
				{
					InventoryTileBox.Items.Add(i);
					GroundTileBox.Items.Add(i);
					MoveAwayTileBox.Items.Add(i);
					IncomingTileBox.Items.Add(i);
				}
			}
			GroundTileBox.EndUpdate();
			InventoryTileBox.EndUpdate();
			IncomingTileBox.EndUpdate();
			MoveAwayTileBox.EndUpdate();

		}

		/// <summary>
		/// New item selected
		/// </summary>
		void ItemSelected()
		{
			NameBox.Text = "";
			DescriptionBox.Text = "";
			ThrowBox.Value = 0;
			FaceBox.Value = 0;
			CriticalMinBox.Value = 0;
			CriticalMaxBox.Value = 0;
			MultiplierBox.Value = 0;
			TypeBox.SelectedText = "";
			SpeedBox.Value = 0;
			WeightBox.Value = 0;
			PrimaryBox.Checked = false;
			SecondaryBox.Checked = false;
			AmmoBox.Checked = false;
			BodyBox.Checked = false;
			RingBox.Checked = false;
			WristBox.Checked = false;
			FeetBox.Checked = false;
			HeadBox.Checked = false;
			WaistBox.Checked = false;
			NeckBox.Checked = false;
			
			
			
			if (Item != null)
			{
				NameBox.Text = Item.Name;
				DescriptionBox.Text = Item.Description;
				ThrowBox.Value = Item.DiceThrow;
				FaceBox.Value = Item.DiceFace;
				CriticalMinBox.Value = Item.Critical.X;
				CriticalMaxBox.Value = Item.Critical.Y;
				MultiplierBox.Value = Item.CriticalMultiplier;
				SpeedBox.Value = Item.Speed;
				WeightBox.Value = Item.Weight;
				TypeBox.SelectedItem = Item.Type.ToString();
				GroundTileBox.SelectedItem = Item.GroundTileID;
				InventoryTileBox.SelectedItem = Item.TileID;
				MoveAwayTileBox.SelectedItem = Item.MoveAwayTileID;
				IncomingTileBox.SelectedItem = Item.IncomingTileID;

				PrimaryBox.Checked = (Item.Slot & ItemSlot.Primary) == ItemSlot.Primary;
				SecondaryBox.Checked = (Item.Slot & ItemSlot.Secondary) == ItemSlot.Secondary;
				AmmoBox.Checked = (Item.Slot & ItemSlot.Ammo) == ItemSlot.Ammo;
				BodyBox.Checked = (Item.Slot & ItemSlot.Body) == ItemSlot.Body;
				RingBox.Checked = (Item.Slot & ItemSlot.Ring) == ItemSlot.Ring;
				WristBox.Checked = (Item.Slot & ItemSlot.Wrist) == ItemSlot.Wrist;
				FeetBox.Checked = (Item.Slot & ItemSlot.Feet) == ItemSlot.Feet;
				HeadBox.Checked = (Item.Slot & ItemSlot.Head) == ItemSlot.Head;
				WaistBox.Checked = (Item.Slot & ItemSlot.Waist) == ItemSlot.Waist;
				NeckBox.Checked = (Item.Slot & ItemSlot.Neck) == ItemSlot.Neck;
			}

			Paint_Tiles(null, null);
		}




		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				ItemSet.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			//HACK !!!!!!
			ResourceManager.AddAsset<ItemSet>("Main", doc.DocumentElement);
		}


		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Item = ItemSet.GetItem(ItemsBox.SelectedItem as string);
			ItemSelected();
		}


	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
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


		/// <summary>
		/// Adds a new item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddItemBox_Click(object sender, EventArgs e)
		{
			new Wizards.NewItemWizard(ItemSet).ShowDialog();

			RebuildLists();
			ItemSelected();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Paint_Tiles(object sender, PaintEventArgs e)
		{
			if (GLGroundTile.Context == null)
				return;

			Point location;
			Tile tile;
			int tileid;

			GLGroundTile.MakeCurrent();
			Display.ClearBuffers();
			if (GroundTileBox.SelectedItem != null)
			{
				tileid = int.Parse(GroundTileBox.SelectedItem.ToString());
				tile = TileSet.GetTile(tileid);
				location = new Point((GLGroundTile.Width - tile.Size.Width) / 2, (GLGroundTile.Height - tile.Size.Height) / 2);
				TileSet.Draw(tileid, location);
			}
			GLGroundTile.SwapBuffers();



			GLInventoryTile.MakeCurrent();
			Display.ClearBuffers();
			if (InventoryTileBox.SelectedItem != null)
			{
				tileid = int.Parse(InventoryTileBox.SelectedItem.ToString());
				tile = TileSet.GetTile(tileid);
				location = new Point((GLInventoryTile.Width - tile.Size.Width) / 2, (GLInventoryTile.Height - tile.Size.Height) / 2);
				TileSet.Draw(tileid, location);
			}
			GLInventoryTile.SwapBuffers();


			GLIncomingTile.MakeCurrent();
			Display.ClearBuffers();
			if (IncomingTileBox.SelectedItem != null)
			{
				tileid = int.Parse(IncomingTileBox.SelectedItem.ToString());
				tile = TileSet.GetTile(tileid);
				location = new Point((GLIncomingTile.Width - tile.Size.Width) / 2, (GLIncomingTile.Height - tile.Size.Height) / 2);
				TileSet.Draw(tileid, location);
			}
			GLIncomingTile.SwapBuffers();


			GLMoveAwayTile.MakeCurrent();
			Display.ClearBuffers();
			if (MoveAwayTileBox.SelectedItem != null)
			{
				tileid = int.Parse(MoveAwayTileBox.SelectedItem.ToString());
				tile = TileSet.GetTile(tileid);
				location = new Point((GLMoveAwayTile.Width - tile.Size.Width) / 2, (GLMoveAwayTile.Height - tile.Size.Height) / 2);
				TileSet.Draw(tileid, location);
			}
			GLMoveAwayTile.SwapBuffers();




		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLInventoryTile_Resize(object sender, EventArgs e)
		{
			GLInventoryTile.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GLInventoryTile.Size);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLGroundTile_Resize(object sender, EventArgs e)
		{
			GLGroundTile.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GLGroundTile.Size);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLIncomingTile_Resize(object sender, EventArgs e)
		{
			GLIncomingTile.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GLIncomingTile.Size);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLMoveAwayTile_Resize(object sender, EventArgs e)
		{
			GLMoveAwayTile.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GLMoveAwayTile.Size);
		}


		/// <summary>
		/// Change TileSet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSelectedTileIDChanged(object sender, EventArgs e)
		{
			Paint_Tiles(null, null);
		}


		/// <summary>
		/// Change TileSet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSelectedTileSetChanged(object sender, EventArgs e)
		{
			if (TileSetNameBox.SelectedIndex == -1)
				return;

			ItemSet.TileSetName = TileSetNameBox.SelectedItem as string;
			TileSet = ResourceManager.CreateAsset<TileSet>(ItemSet.TileSetName);
			RebuildTilesList();
			Paint_Tiles(null, null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemSetForm_Shown(object sender, EventArgs e)
		{
			GLGroundTile_Resize(null, null);
			GLInventoryTile_Resize(null, null);
			GLMoveAwayTile_Resize(null, null);
			GLIncomingTile_Resize(null, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ApplyBox_Click(object sender, EventArgs e)
		{
			if (Item == null || string.IsNullOrEmpty(NameBox.Text))
				return;

			// Save values
			Item.Name = NameBox.Text;
			Item.Description = DescriptionBox.Text;
			Item.DiceThrow = int.Parse(ThrowBox.Value.ToString());
			Item.DiceFace = int.Parse(FaceBox.Value.ToString());
			Item.Critical = new Point(int.Parse(CriticalMinBox.Value.ToString()), int.Parse(CriticalMaxBox.Value.ToString()));
			Item.CriticalMultiplier = int.Parse(MultiplierBox.Value.ToString());
			Item.Type = (ItemType)Enum.Parse(typeof(ItemType), TypeBox.SelectedItem.ToString(), true);
			Item.Speed = int.Parse(SpeedBox.Value.ToString());
			Item.Weight = int.Parse(WeightBox.Value.ToString());
			Item.GroundTileID = int.Parse(GroundTileBox.SelectedItem.ToString());
			Item.TileID = int.Parse(InventoryTileBox.SelectedItem.ToString());
			Item.IncomingTileID = int.Parse(IncomingTileBox.SelectedItem.ToString());
			Item.MoveAwayTileID = int.Parse(MoveAwayTileBox.SelectedItem.ToString());

			if (PrimaryBox.Checked)
				Item.Slot |= ItemSlot.Primary;			
			if (SecondaryBox.Checked)
				Item.Slot |= ItemSlot.Secondary;			
			if (AmmoBox.Checked)
				Item.Slot |= ItemSlot.Ammo;				
			if (BodyBox.Checked)
				Item.Slot |= ItemSlot.Body;				
			if (RingBox.Checked)
				Item.Slot |= ItemSlot.Ring;				
			if (WristBox.Checked)
				Item.Slot |= ItemSlot.Wrist;				
			if (FeetBox.Checked)
				Item.Slot |= ItemSlot.Feet;				
			if (HeadBox.Checked)
				Item.Slot |= ItemSlot.Head;				
			if (WaistBox.Checked)
				Item.Slot |= ItemSlot.Waist;
			if (NeckBox.Checked)
				Item.Slot |= ItemSlot.Neck;				


			// Add to the tileset
			ItemSet.Items[Item.Name] = Item;

			int id = ItemsBox.SelectedIndex;
			RebuildLists();
			ItemsBox.SelectedIndex = id;
		}


		/// <summary>
		/// Removes an item
		/// 
		/// TODO
		/// Quand on a effacé tous les items et qu'il ne reste plus rien, 
		/// effacer toutes les box contenant des informations sur le dernier item.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveItem(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			// Warning box
			if (MessageBox.Show("Remove item \"" + Item.Name + "\" ?", "Remove item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			// Get position index
			int pos = ItemsBox.SelectedIndex - 1;

			// Remove item
			ItemSet.Items.Remove(Item.Name);

			// Rebuild item list
			RebuildLists();
			ItemSelected();

			// Select previous item
			if (ItemsBox.Items.Count > 0)
				ItemsBox.SelectedIndex = Math.Max(pos, 0);

		}



		/// <summary>
		/// Change script name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScriptNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ScriptNameBox.SelectedIndex == -1)
				return;


			ItemSet.ScriptName = ScriptNameBox.SelectedItem as string;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemSetForm_Load(object sender, EventArgs e)
		{

			GLInventoryTile.MakeCurrent();
			Display.Init();

			GLGroundTile.MakeCurrent();
			Display.Init();

			GLMoveAwayTile.MakeCurrent();
			Display.Init();

			GLIncomingTile.MakeCurrent();
			Display.Init();

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
				return ItemSet;
			}
		}
		
		
		/// <summary>
		/// Current item
		/// </summary>
		ItemSet ItemSet;


		/// <summary>
		/// Item selected
		/// </summary>
		Item Item;


		/// <summary>
		/// 
		/// </summary>
		TileSet TileSet;

		#endregion



	}
}
