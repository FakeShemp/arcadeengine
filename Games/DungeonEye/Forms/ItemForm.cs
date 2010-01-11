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
using DungeonEye.Interfaces;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Itemset form editor class
	/// </summary>
	public partial class ItemForm : AssetEditor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node">node to edit</param>
		public ItemForm(XmlNode node)
		{
			InitializeComponent();


			Item = new Item();
			Item.Load(node);


			// TileSetNameBox
			TileSetNameBox.BeginUpdate();
			foreach (string name in ResourceManager.GetAssets<TileSet>())
			{
				TileSetNameBox.Items.Add(name);
			}
			TileSetNameBox.EndUpdate();

			// Tileset name
			if (!string.IsNullOrEmpty(Item.TileSetName) && TileSetNameBox.Items.Contains(Item.TileSetName))
				TileSetNameBox.SelectedItem = Item.TileSetName;



			// Scripts
			ScriptNameBox.BeginUpdate();
			ScriptNameBox.Items.Clear();
			foreach (string name in ResourceManager.GetAssets<Script>())
			{
				ScriptNameBox.Items.Add(name);
			}
			ScriptNameBox.Items.Insert(0, "");
			ScriptNameBox.EndUpdate();

			// Script name
		//	if (!string.IsNullOrEmpty(Item.ScriptName) && ScriptNameBox.Items.Contains(Item.ScriptName))
		//		ScriptNameBox.SelectedItem = Item.ScriptName;
			

			TypeBox.BeginUpdate();
			TypeBox.Items.Clear();
			foreach(string name in Enum.GetNames(typeof(ItemType)))
				TypeBox.Items.Add(name);
			TypeBox.EndUpdate();



			#region UI update

			DescriptionBox.Text = Item.Description;
			CriticalMinBox.Value = Item.Critical.X;
			CriticalMaxBox.Value = Item.Critical.Y;
			MultiplierBox.Value = Item.CriticalMultiplier;
			SpeedBox.Value = Item.Speed;
			WeightBox.Value = Item.Weight;
			TypeBox.SelectedItem = Item.Type.ToString();
			GroundTileBox.SelectedItem = Item.GroundTileID;
			InventoryTileBox.SelectedItem = Item.TileID;
			ThrownTileBox.SelectedItem = Item.ThrowTileID;
			IncomingTileBox.SelectedItem = Item.IncomingTileID;
			UseQuiverBox.Checked = Item.UseQuiver;
			TwoHandedBox.Checked = Item.TwoHanded;


			PrimaryBox.Checked = (Item.Slot & BodySlot.Primary) == BodySlot.Primary;
			SecondaryBox.Checked = (Item.Slot & BodySlot.Secondary) == BodySlot.Secondary;
			QuiverBox.Checked = (Item.Slot & BodySlot.Quiver) == BodySlot.Quiver;
			BodyBox.Checked = (Item.Slot & BodySlot.Body) == BodySlot.Body;
			RingBox.Checked = (Item.Slot & BodySlot.Ring) == BodySlot.Ring;
			WristBox.Checked = (Item.Slot & BodySlot.Wrist) == BodySlot.Wrist;
			FeetBox.Checked = (Item.Slot & BodySlot.Feet) == BodySlot.Feet;
			HeadBox.Checked = (Item.Slot & BodySlot.Head) == BodySlot.Head;
			WaistBox.Checked = (Item.Slot & BodySlot.Belt) == BodySlot.Belt;
			NeckBox.Checked = (Item.Slot & BodySlot.Neck) == BodySlot.Neck;

			FighterBox.Checked = (Item.Classes & HeroClass.Fighter) == HeroClass.Fighter;
			PaladinBox.Checked = (Item.Classes & HeroClass.Paladin) == HeroClass.Paladin;
			ClericBox.Checked = (Item.Classes & HeroClass.Cleric) == HeroClass.Cleric;
			MageBox.Checked = (Item.Classes & HeroClass.Mage) == HeroClass.Mage;
			ThiefBox.Checked = (Item.Classes & HeroClass.Thief) == HeroClass.Thief;
			RangerBox.Checked = (Item.Classes & HeroClass.Ranger) == HeroClass.Ranger;

			ScriptNameBox.SelectedItem = Item.ScriptName;
			InterfaceNameBox.SelectedItem = Item.InterfaceName;

			DamageBox.Dice = Item.Damage;
			#endregion

			Paint_Tiles(null, null);
		}




		/// <summary>
		/// Saves the asset to the manager
		/// </summary>
		public override void Save()
		{
			ResourceManager.AddAsset<Item>(Item.Name, ResourceManager.ConvertAsset(Item));
		}


		#region Events



	
		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "Item Editor", MessageBoxButtons.YesNoCancel);

			if (result == DialogResult.Yes)
			{
				Save();
				TileSet.Dispose();
				TileSet = null;
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

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
			if (ThrownTileBox.SelectedItem != null)
			{
				tileid = int.Parse(ThrownTileBox.SelectedItem.ToString());
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
		private void GLThrowTile_Resize(object sender, EventArgs e)
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

			if (TileSet != null)
				TileSet.Dispose();

			Item.TileSetName = TileSetNameBox.SelectedItem as string;
			TileSet = ResourceManager.CreateAsset<TileSet>(Item.TileSetName);

			RebuildTilesList();

			Paint_Tiles(null, null);
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
			ThrownTileBox.BeginUpdate();
			ThrownTileBox.Items.Clear();
			IncomingTileBox.BeginUpdate();
			IncomingTileBox.Items.Clear();
			if (TileSet != null)
			{
				List<int> id = TileSet.GetTiles();
				foreach (int i in id)
				{
					InventoryTileBox.Items.Add(i);
					GroundTileBox.Items.Add(i);
					ThrownTileBox.Items.Add(i);
					IncomingTileBox.Items.Add(i);
				}
			}
			GroundTileBox.EndUpdate();
			InventoryTileBox.EndUpdate();
			IncomingTileBox.EndUpdate();
			ThrownTileBox.EndUpdate();

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
			GLThrowTile_Resize(null, null);
			GLIncomingTile_Resize(null, null);
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
				return Item;
			}
		}
		
		
		/// <summary>
		/// Item 
		/// </summary>
		Item Item;


		/// <summary>
		/// Tileset
		/// </summary>
		TileSet TileSet;

		#endregion


		#region Item slots

		private void PrimaryBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (PrimaryBox.Checked)
				Item.Slot |= BodySlot.Primary;
			else
				Item.Slot ^= BodySlot.Primary;
		}

		private void QuiverBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (QuiverBox.Checked)
				Item.Slot |= BodySlot.Quiver;
			else
				Item.Slot ^= BodySlot.Quiver;
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
				Item.Slot |= BodySlot.Belt;
			else
				Item.Slot ^= BodySlot.Belt;
		}

		#endregion


		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;
			Item.Type = (ItemType)Enum.Parse(typeof(ItemType), (string)TypeBox.SelectedItem);
		}

		private void SpeedBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.Speed = (int)SpeedBox.Value;
		}

		private void WeightBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.Weight = (int)WeightBox.Value;
		}


		#region Profesions

		private void FighterBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (FighterBox.Checked)
				Item.Classes |= HeroClass.Fighter;
			else
				Item.Classes ^= HeroClass.Fighter;
		}

		private void PaladinBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (PaladinBox.Checked)
				Item.Classes |= HeroClass.Paladin;
			else
				Item.Classes ^= HeroClass.Paladin;

		}

		private void ClericBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (ClericBox.Checked)
				Item.Classes |= HeroClass.Cleric;
			else
				Item.Classes ^= HeroClass.Cleric;
		}

		private void RangerBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			if (RangerBox.Checked)
				Item.Classes |= HeroClass.Ranger;
			else
				Item.Classes ^= HeroClass.Ranger;
		}

		private void MageBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			if (MageBox.Checked)
				Item.Classes |= HeroClass.Mage;
			else
				Item.Classes ^= HeroClass.Mage;
		}

		private void ThiefBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (ThiefBox.Checked)
				Item.Classes |= HeroClass.Thief;
			else
				Item.Classes ^= HeroClass.Thief;

		}

		#endregion

		#region Hands

		/// <summary>
		/// Two handed item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TwoHandedBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;
			Item.TwoHanded = TwoHandedBox.Checked;
		}

		#endregion

		#region Critical

		private void CriticalMinBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

		}

		private void CriticalMaxBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

		}

		private void MultiplierBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

		}
		#endregion

		#region scripting events

		private void InterfaceNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.InterfaceName = (string)InterfaceNameBox.SelectedItem;
		}

		/// <summary>
		/// Change script name
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScriptNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ScriptNameBox.SelectedIndex == -1 || Item == null)
				return;

			Item.ScriptName = ScriptNameBox.SelectedItem as string;
			RebuildFoundInterfaces();
		}


		/// <summary>
		/// Rebuild found interfaces
		/// </summary>
		void RebuildFoundInterfaces()
		{
			InterfaceNameBox.Items.Clear();


			Script script = ResourceManager.CreateAsset<Script>(Item.ScriptName);
			if (script != null)
			{
				List<string> list = script.GetImplementedInterfaces(typeof(IItem));
				InterfaceNameBox.Items.AddRange(list.ToArray());

			}
			InterfaceNameBox.Items.Insert(0, "");
		}


		#endregion


		/// <summary>
		/// Change description
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DescriptionBox_TextChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.Description = DescriptionBox.Text;
		}


		/// <summary>
		/// Change damage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DamageBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			Item.Damage.Base = DamageBox.Dice.Base;
			Item.Damage.Faces = DamageBox.Dice.Faces;
			Item.Damage.Throws = DamageBox.Dice.Throws;
		}





		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ACBonusBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

		}


		/// <summary>
		/// The item use quiver
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UseQuiverBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.UseQuiver = UseQuiverBox.Checked;
		}




	}
}
