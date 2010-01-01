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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Itemset form editor class
	/// </summary>
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
			foreach (string name in ResourceManager.GetAssets<TileSet>())
			{
				TileSetNameBox.Items.Add(name);
			}
			TileSetNameBox.EndUpdate();

			// Tileset name
			if (!string.IsNullOrEmpty(ItemSet.TileSetName) && TileSetNameBox.Items.Contains(ItemSet.TileSetName))
				TileSetNameBox.SelectedItem = ItemSet.TileSetName;



			// Scripts
			ScriptNameBox.BeginUpdate();
			ScriptNameBox.Items.Clear();
			foreach (string name in ResourceManager.GetAssets<Script>())
			{
				ScriptNameBox.Items.Add(name);
			}
			ScriptNameBox.EndUpdate();

			// Script name
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
		/// <param name="name">item name</param>
		void ItemSelected(string name)
		{
			Item = null;
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
			UseQuiverBox.Checked = false;
			
			Item = ItemSet.GetItem(name);
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
				MoveAwayTileBox.SelectedItem = Item.ThrowTileID;
				IncomingTileBox.SelectedItem = Item.IncomingTileID;
				UseQuiverBox.Checked = Item.UseQuiver;


				PrimaryBox.Checked = (Item.Slot & BodySlot.Primary) == BodySlot.Primary;
				SecondaryBox.Checked = (Item.Slot & BodySlot.Secondary) == BodySlot.Secondary;
				AmmoBox.Checked = (Item.Slot & BodySlot.Ammo) == BodySlot.Ammo;
				BodyBox.Checked = (Item.Slot & BodySlot.Body) == BodySlot.Body;
				RingBox.Checked = (Item.Slot & BodySlot.Ring) == BodySlot.Ring;
				WristBox.Checked = (Item.Slot & BodySlot.Wrist) == BodySlot.Wrist;
				FeetBox.Checked = (Item.Slot & BodySlot.Feet) == BodySlot.Feet;
				HeadBox.Checked = (Item.Slot & BodySlot.Head) == BodySlot.Head;
				WaistBox.Checked = (Item.Slot & BodySlot.Waist) == BodySlot.Waist;
				NeckBox.Checked = (Item.Slot & BodySlot.Neck) == BodySlot.Neck;
			}

			Paint_Tiles(null, null);
		}




		/// <summary>
		/// Saves the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				ItemSet.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<ItemSet>(ItemSet.Name, doc.DocumentElement);
		}


		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ItemSelected(ItemsBox.SelectedItem as string);
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
			ItemSelected(string.Empty);
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
			Item.ThrowTileID = int.Parse(MoveAwayTileBox.SelectedItem.ToString());

			if (PrimaryBox.Checked)
				Item.Slot |= BodySlot.Primary;			
			if (SecondaryBox.Checked)
				Item.Slot |= BodySlot.Secondary;			
			if (AmmoBox.Checked)
				Item.Slot |= BodySlot.Ammo;				
			if (BodyBox.Checked)
				Item.Slot |= BodySlot.Body;				
			if (RingBox.Checked)
				Item.Slot |= BodySlot.Ring;				
			if (WristBox.Checked)
				Item.Slot |= BodySlot.Wrist;				
			if (FeetBox.Checked)
				Item.Slot |= BodySlot.Feet;				
			if (HeadBox.Checked)
				Item.Slot |= BodySlot.Head;				
			if (WaistBox.Checked)
				Item.Slot |= BodySlot.Waist;
			if (NeckBox.Checked)
				Item.Slot |= BodySlot.Neck;				


			// Add to the tileset
			ItemSet.Items[Item.Name] = Item;

			//int id = ItemsBox.SelectedIndex;
			//RebuildLists();
			//ItemsBox.SelectedIndex = id;
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
			ItemSelected(string.Empty);

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
			RebuildScripts();

		}


		/// <summary>
		/// 
		/// </summary>
		void RebuildScripts()
		{
			OnUseBox.Items.Clear();
			OnDropBox.Items.Clear();
			OnCollectBox.Items.Clear();


			Script script = ResourceManager.CreateAsset<Script>(ItemSet.ScriptName);
			if (script == null)
				return;

			List<string> list = null;

			// OnUse
			list = script.GetMethods(new Type[] { typeof(Item), typeof(Hero) }, typeof(void));
			OnUseBox.Items.AddRange(list.ToArray());

			// OnDrop
			list = script.GetMethods(new Type[] { typeof(Item), typeof(Hero), typeof(MazeBlock) }, typeof(void));
			OnDropBox.Items.AddRange(list.ToArray());

			// OnCollect
			list = script.GetMethods(new Type[] { typeof(Item), typeof(Hero) }, typeof(void));
			OnCollectBox.Items.AddRange(list.ToArray());



		}


		/// <summary>
		/// Form load
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


		private void UseQuiverBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.UseQuiver = UseQuiverBox.Checked;
		}

		private void ThrowBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void FaceBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{

		}

		private void PrimaryBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (PrimaryBox.Checked)
				Item.Slot |= BodySlot.Primary;
			else
				Item.Slot ^= BodySlot.Primary;
		}

		private void AmmoBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (AmmoBox.Checked)
				Item.Slot |= BodySlot.Ammo;
			else
				Item.Slot ^= BodySlot.Ammo;
		}

		private void NeckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (NeckBox.Checked)
				Item.Slot |= BodySlot.Neck;
			else
				Item.Slot ^= BodySlot.Neck;

		}

		private void WristBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (WristBox.Checked)
				Item.Slot |= BodySlot.Wrist;
			else
				Item.Slot ^= BodySlot.Wrist;
		}

		private void HeadBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (HeadBox.Checked)
				Item.Slot |= BodySlot.Head;
			else
				Item.Slot ^= BodySlot.Head;
		}

		private void SecondaryBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (SecondaryBox.Checked)
				Item.Slot |= BodySlot.Secondary;
			else
				Item.Slot ^= BodySlot.Secondary;
		}

		private void BodyBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (BodyBox.Checked)
				Item.Slot |= BodySlot.Body;
			else
				Item.Slot ^= BodySlot.Body;
		}

		private void RingBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (RingBox.Checked)
				Item.Slot |= BodySlot.Ring;
			else
				Item.Slot ^= BodySlot.Ring;
		}

		private void FeetBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (FeetBox.Checked)
				Item.Slot |= BodySlot.Feet;
			else
				Item.Slot ^= BodySlot.Feet;

		}

		private void WaistBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (WaistBox.Checked)
				Item.Slot |= BodySlot.Waist;
			else
				Item.Slot ^= BodySlot.Waist;
		}

		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void SpeedBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void WeightBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void FighterBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void PaladinBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void ClericBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void RangerBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void MageBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void ThiefBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void SecondaryHandBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void PrimaryHandBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void TwoHandedBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void CriticalMinBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void CriticalMaxBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void MultiplierBox_ValueChanged(object sender, EventArgs e)
		{

		}

		private void DescriptionBox_TextChanged(object sender, EventArgs e)
		{

		}


		#region scripting events

		private void OnUseBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.OnUseScript = (string)OnUseBox.SelectedItem;

		}

		private void OnDropBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;
			Item.OnDropScript = (string)OnDropBox.SelectedItem;

		}

		private void OnCollectBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;
			Item.OnCollectScript = (string)OnCollectBox.SelectedItem;
		}

		#endregion


	}
}
