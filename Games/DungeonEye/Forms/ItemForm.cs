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


			// TileSetNameBox
			TileSetNameBox.BeginUpdate();
			foreach (string name in ResourceManager.GetAssets<TileSet>())
			{
				TileSetNameBox.Items.Add(name);
			}
			TileSetNameBox.EndUpdate();


			// Scripts
			ScriptNameBox.BeginUpdate();
			ScriptNameBox.Items.Clear();
			foreach (string name in ResourceManager.GetAssets<Script>())
			{
				ScriptNameBox.Items.Add(name);
			}
			ScriptNameBox.Items.Insert(0, "");
			ScriptNameBox.EndUpdate();

			

			TypeBox.BeginUpdate();
			TypeBox.Items.Clear();
			foreach(string name in Enum.GetNames(typeof(ItemType)))
				TypeBox.Items.Add(name);
			TypeBox.EndUpdate();


			Item item = new Item();
			item.Load(node);


			#region UI update

			DescriptionBox.Text = item.Description;
			CriticalMinBox.Value = item.Critical.X;
			CriticalMaxBox.Value = item.Critical.Y;
			MultiplierBox.Value = item.CriticalMultiplier;
			SpeedBox.Value = (int)item.AttackSpeed.TotalMilliseconds;
			WeightBox.Value = item.Weight;
			TypeBox.SelectedItem = item.Type.ToString();
			GroundTileBox.SelectedItem = item.GroundTileID;
			InventoryTileBox.SelectedItem = item.TileID;
			ThrownTileBox.SelectedItem = item.ThrowTileID;
			IncomingTileBox.SelectedItem = item.IncomingTileID;


			PrimaryBox.Checked = (item.Slot & BodySlot.Primary) == BodySlot.Primary;
			SecondaryBox.Checked = (item.Slot & BodySlot.Secondary) == BodySlot.Secondary;
			QuiverBox.Checked = (item.Slot & BodySlot.Quiver) == BodySlot.Quiver;
			BodyBox.Checked = (item.Slot & BodySlot.Body) == BodySlot.Body;
			RingBox.Checked = (item.Slot & BodySlot.Ring) == BodySlot.Ring;
			WristBox.Checked = (item.Slot & BodySlot.Wrist) == BodySlot.Wrist;
			FeetBox.Checked = (item.Slot & BodySlot.Feet) == BodySlot.Feet;
			HeadBox.Checked = (item.Slot & BodySlot.Head) == BodySlot.Head;
			WaistBox.Checked = (item.Slot & BodySlot.Waist) == BodySlot.Waist;
			NeckBox.Checked = (item.Slot & BodySlot.Neck) == BodySlot.Neck;

			FighterBox.Checked = (item.AllowedClasses & HeroClass.Fighter) == HeroClass.Fighter;
			PaladinBox.Checked = (item.AllowedClasses & HeroClass.Paladin) == HeroClass.Paladin;
			ClericBox.Checked = (item.AllowedClasses & HeroClass.Cleric) == HeroClass.Cleric;
			MageBox.Checked = (item.AllowedClasses & HeroClass.Mage) == HeroClass.Mage;
			ThiefBox.Checked = (item.AllowedClasses & HeroClass.Thief) == HeroClass.Thief;
			RangerBox.Checked = (item.AllowedClasses & HeroClass.Ranger) == HeroClass.Ranger;

			ScriptNameBox.SelectedItem = item.ScriptName;
			InterfaceNameBox.SelectedItem = item.InterfaceName;

			ACBonusBox.Value = item.ArmorClass;
			DamageBox.Dice = item.Damage;
			DamageVsSmallBox.Dice = item.DamageVsSmall;
			DamageVsBigBox.Dice = item.DamageVsBig;

			PiercingBox.Checked = (item.DamageType & DamageType.Pierce) == DamageType.Pierce;
			SlashBox.Checked = (item.DamageType & DamageType.Slash) == DamageType.Slash;
			BludgeBox.Checked = (item.DamageType & DamageType.Bludge) == DamageType.Bludge;
			#endregion

			Item = item;
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
		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "Item Editor", MessageBoxButtons.YesNoCancel);

			if (result == DialogResult.Yes)
			{
				Save();
				if (TileSet != null) 
					TileSet.Dispose();
				TileSet = null;

				if (SpriteBatch != null) 
					SpriteBatch.Dispose();
				SpriteBatch = null;
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}





		/// <summary>
		/// Draws all visual
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Paint_Tiles(object sender, PaintEventArgs e)
		{
			DrawTiles(GLInventoryTile, InventoryTileBox.SelectedItem == null ? -1 : (int) InventoryTileBox.SelectedItem);
			DrawTiles(GLGroundTile, GroundTileBox.SelectedItem == null ? -1 : (int) GroundTileBox.SelectedItem);
			DrawTiles(GLThrownTile, ThrownTileBox.SelectedItem == null ? -1 : (int) ThrownTileBox.SelectedItem);
			DrawTiles(GLIncomingTile, IncomingTileBox.SelectedItem == null ? -1 : (int) IncomingTileBox.SelectedItem);
		}


		/// <summary>
		/// Paint a control
		/// </summary>
		/// <param name="control">Control to paint</param>
		/// <param name="tileid">Id of the tile to draw</param>
		private void DrawTiles(OpenTK.GLControl control, int tileid)
		{
			control.MakeCurrent();
			Display.ClearBuffers();

			SpriteBatch.Begin();

			// Background texture
			Rectangle dst = new Rectangle(Point.Empty, control.Size);
			SpriteBatch.Draw(CheckerBoard, dst, dst, Color.White);


			// Tile to draw
			if (tileid >= 0)
			{
				Tile tile = TileSet.GetTile(tileid);
				Point location = new Point((control.Width - tile.Size.Width) / 2, (control.Height - tile.Size.Height) / 2);
				SpriteBatch.DrawTile(TileSet, tileid, location);
			}

			SpriteBatch.End();

			control.SwapBuffers();

		}


		/// <summary>
		/// Form load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_Load(object sender, EventArgs e)
		{

			SpriteBatch = new SpriteBatch();

			// Preload background texture resource
			CheckerBoard = new Texture2D(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));
			CheckerBoard.HorizontalWrap = HorizontalWrapFilter.Repeat;
			CheckerBoard.VerticalWrap = VerticalWrapFilter.Repeat;


			if (Item == null)
				return;

			// Script name
			if (!string.IsNullOrEmpty(Item.ScriptName) && ScriptNameBox.Items.Contains(Item.ScriptName))
				ScriptNameBox.SelectedItem = Item.ScriptName;

			// Tileset name
			if (!string.IsNullOrEmpty(Item.TileSetName) && TileSetNameBox.Items.Contains(Item.TileSetName))
				TileSetNameBox.SelectedItem = Item.TileSetName;


			ThrownTileBox.SelectedItem = Item.ThrowTileID;
			IncomingTileBox.SelectedItem = Item.IncomingTileID;
			GroundTileBox.SelectedItem = Item.GroundTileID;
			InventoryTileBox.SelectedItem = Item.TileID;
		}



		#region Control loadings

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLInventoryTile_Load(object sender, EventArgs e)
		{
			GLInventoryTile.MakeCurrent();
			Display.Init();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLGroundTile_Load(object sender, EventArgs e)
		{
			GLGroundTile.MakeCurrent();
			Display.Init();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLThrownTile_Load(object sender, EventArgs e)
		{
			GLThrownTile.MakeCurrent();
			Display.Init();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLIncomingTile_Load(object sender, EventArgs e)
		{
			GLIncomingTile.MakeCurrent();
			Display.Init();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLInventoryTile_Resize(object sender, EventArgs e)
		{
			GLInventoryTile.MakeCurrent();
			Display.Init();
			
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
			Display.Init();

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
			Display.Init();

			Display.ViewPort = new Rectangle(new Point(), GLIncomingTile.Size);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLThrownTile_Resize(object sender, EventArgs e)
		{
			GLThrownTile.MakeCurrent();
			Display.Init();

			Display.ViewPort = new Rectangle(new Point(), GLThrownTile.Size);
		}


		#endregion


		/// <summary>
		/// Change TileSet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetOnSelectedChanged(object sender, EventArgs e)
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
				Item.Slot |= BodySlot.Waist;
			else
				Item.Slot ^= BodySlot.Waist;
		}

		#endregion


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;
			Item.Type = (ItemType)Enum.Parse(typeof(ItemType), (string)TypeBox.SelectedItem);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpeedBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.AttackSpeed = TimeSpan.FromMilliseconds((int)SpeedBox.Value);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				Item.AllowedClasses |= HeroClass.Fighter;
			else
				Item.AllowedClasses ^= HeroClass.Fighter;
		}

		private void PaladinBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (PaladinBox.Checked)
				Item.AllowedClasses |= HeroClass.Paladin;
			else
				Item.AllowedClasses ^= HeroClass.Paladin;

		}

		private void ClericBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (ClericBox.Checked)
				Item.AllowedClasses |= HeroClass.Cleric;
			else
				Item.AllowedClasses ^= HeroClass.Cleric;
		}

		private void RangerBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			if (RangerBox.Checked)
				Item.AllowedClasses |= HeroClass.Ranger;
			else
				Item.AllowedClasses ^= HeroClass.Ranger;
		}

		private void MageBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			if (MageBox.Checked)
				Item.AllowedClasses |= HeroClass.Mage;
			else
				Item.AllowedClasses ^= HeroClass.Mage;
		}

		private void ThiefBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (ThiefBox.Checked)
				Item.AllowedClasses |= HeroClass.Thief;
			else
				Item.AllowedClasses ^= HeroClass.Thief;

		}

		#endregion


		#region Hands

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


			Item.Damage.Modifier = DamageBox.Dice.Modifier;
			Item.Damage.Faces = DamageBox.Dice.Faces;
			Item.Damage.Throws = DamageBox.Dice.Throws;
		}

		/// <summary>
		/// Change damage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DamageVsBigBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			Item.DamageVsBig.Modifier = DamageVsBigBox.Dice.Modifier;
			Item.DamageVsBig.Faces = DamageVsBigBox.Dice.Faces;
			Item.DamageVsBig.Throws = DamageVsBigBox.Dice.Throws;
		}

		/// <summary>
		/// Change damage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DamageVsSmallBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;


			Item.DamageVsSmall.Modifier = DamageVsSmallBox.Dice.Modifier;
			Item.DamageVsSmall.Faces = DamageVsSmallBox.Dice.Faces;
			Item.DamageVsSmall.Throws = DamageVsSmallBox.Dice.Throws;
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

			Item.ArmorClass = (byte)ACBonusBox.Value;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PiercingBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (PiercingBox.Checked)
				Item.DamageType |= DamageType.Pierce;
			else
				Item.DamageType ^= DamageType.Pierce;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BludgeBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (BludgeBox.Checked)
				Item.DamageType |= DamageType.Bludge;
			else
				Item.DamageType ^= DamageType.Bludge;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SlashBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			if (SlashBox.Checked)
				Item.DamageType |= DamageType.Slash;
			else
				Item.DamageType ^= DamageType.Slash;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RangeBox_ValueChanged(object sender, EventArgs e)
		{
			if (Item == null)
				return;

			Item.Range = (int)RangeBox.Value;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InventoryTileID_OnChange(object sender, EventArgs e)
		{
			if (Item != null)
				Item.TileID = InventoryTileBox.SelectedIndex;

			Paint_Tiles(null, null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GroundTileID_OnChange(object sender, EventArgs e)
		{
			if (Item != null)
				Item.GroundTileID = GroundTileBox.SelectedIndex;

			Paint_Tiles(null, null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ThrownID_OnChange(object sender, EventArgs e)
		{
			if (Item != null)
				Item.ThrowTileID = ThrownTileBox.SelectedIndex;

			Paint_Tiles(null, null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IncomingTile_OnChange(object sender, EventArgs e)
		{
			if (Item != null)
				Item.IncomingTileID = IncomingTileBox.SelectedIndex;

			Paint_Tiles(null, null);
		}



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


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Background texture
		/// </summary>
		Texture2D CheckerBoard;

		#endregion



	}
}
