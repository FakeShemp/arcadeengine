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
		/// <param name="monster">Monster handle</param>
		public void SetMonster(Monster monster)
		{

			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;


			Monster = null;

			UpdateControls(monster);

			Monster = monster;
		}


		/// <summary>
		/// Update controls
		/// </summary>
		void UpdateControls(Monster monster)
		{
			// Populate comboboxes
			if (ItemsBox.Items.Count == 0)
				ItemsBox.DataSource = ResourceManager.GetAssets<Item>();
			if (TileSetBox.Items.Count == 0)
				TileSetBox.DataSource = ResourceManager.GetAssets<TileSet>();
			if (DefaultBehaviourBox.Items.Count == 0)
				DefaultBehaviourBox.DataSource = Enum.GetValues(typeof(MonsterBehaviour));
			if (CurrentBehaviourBox.Items.Count == 0)
				CurrentBehaviourBox.DataSource = Enum.GetValues(typeof(MonsterBehaviour));
			if (DirectionBox.Items.Count == 0)
				DirectionBox.DataSource = Enum.GetValues(typeof(CardinalPoint));
			if (WeaponNameBox.Items.Count == 0)
			{
				WeaponNameBox.BeginUpdate();
				foreach (string name in ResourceManager.GetAssets<Item>())
				{
					Item item = ResourceManager.CreateAsset<Item>(name);
					if (item.Type == ItemType.Weapon)
						WeaponNameBox.Items.Add(name);
				}
				WeaponNameBox.Items.Insert(0, "");
				WeaponNameBox.EndUpdate();
			}



			EntityBox.Entity = monster;

			if (monster == null)
			return;

			TileSetBox.Text = monster.TileSetName;
			TileIDBox.Value = monster.Tile;
			PocketItemsBox.DataSource = monster.ItemsInPocket;
			XPRewardBox.Value = monster.Reward;
			ArmorClassBox.Value = monster.ArmorClass;
			ScriptBox.SetValues<IMonster>(monster.Script);
			DamageBox.Dice = monster.DamageDice;
			FleesBox.Checked = monster.FleesAfterAttack;
			FlyingBox.Checked = monster.Flying;
			FillSquareBox.Checked = monster.FillSquare;
			NonMaterialBox.Checked = monster.NonMaterial;
			UseStairsBox.Checked = monster.UseStairs;
			BackRowAttackBox.Checked = monster.BackRowAttack;
			PoisonImmunityBox.Checked = monster.PoisonImmunity;
			ThrowWeaponsBox.Checked = monster.ThrowWeapons;
			SmartAIBox.Checked = monster.SmartAI;
			CanSeeInvisibleBox.Checked = monster.CanSeeInvisible;
			SightRangeBox.Value = monster.SightRange;
			PickupBox.Value = (decimal)monster.PickupRate * 100;
			StealBox.Value = (decimal)monster.StealRate * 100;
			CanSeeInvisibleBox.Checked = monster.CanSeeInvisible;
			TeleportsBox.Checked = monster.Teleports;
			DefaultBehaviourBox.SelectedItem = monster.DefaultBehaviour;
			CurrentBehaviourBox.SelectedItem = monster.CurrentBehaviour;
			DirectionBox.SelectedItem = monster.Direction;
			NameBox.Text = monster.Name;
			AttackSpeedBox.Value = (int)monster.AttackSpeed.TotalMilliseconds;
			WeaponNameBox.SelectedItem = monster.WeaponName;
		}


		/// <summary>
		/// 
		/// </summary>
		void Draw()
		{
			if (DesignMode || SpriteBatch == null)
				return;

			GlControl.MakeCurrent();

			Display.ClearBuffers();

			SpriteBatch.Begin();


			// Background texture
			Rectangle dst = new Rectangle(Point.Empty, GlControl.Size);
			SpriteBatch.Draw(CheckerBoard, dst, dst, Color.White);

			if (Monster != null && TileSet != null)
			{
				Tile tile = TileSet.GetTile(Monster.Tile);
				if (tile != null)
				{
					Point pos = new Point((GlControl.Width - tile.Size.Width) / 2, (GlControl.Height - tile.Size.Height) / 2);
					pos.Offset(tile.Origin);
					SpriteBatch.DrawTile(TileSet, Monster.Tile, pos);
				}
			}

			SpriteBatch.End();

			GlControl.SwapBuffers();
		}

		#region Events



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MonsterControl_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			if (ParentForm != null)
				ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);


			if (Monster != null && !string.IsNullOrEmpty(Monster.TileSetName) && TileSet == null)
				TileSet = ResourceManager.CreateAsset<TileSet>(Monster.TileSetName);
		
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Monster = null;

			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			GlControl.MakeCurrent();

			Display.Init();
			Display.ClearBuffers();

			SpriteBatch = new SpriteBatch();

			// Preload background texture resource
			CheckerBoard = new Texture2D(ResourceManager.GetInternalResource("ArcEngine.Resources.checkerboard.png"));
			CheckerBoard.HorizontalWrap = TextureWrapFilter.Repeat;
			CheckerBoard.VerticalWrap = TextureWrapFilter.Repeat;
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

			Monster.TileSetName = (string)TileSetBox.SelectedItem;

			// Reload tileset
			if (TileSet != null)
				TileSet.Dispose();
			TileSet = ResourceManager.CreateAsset<TileSet>(Monster.TileSetName);

			Draw();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

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
			Draw();
		}



		/// <summary>
		/// Change tileset ID
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileIDBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Tile = (int)TileIDBox.Value;
			Draw();
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


		#endregion


		#region Visual Tab



		#endregion


		#region Magic Tab

		private void CastingLevelBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.MagicCastingLevel = (int)CastingLevelBox.Value;
		}

		/// <summary>
		/// Has Magic
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HasMagicBox_CheckedChanged(object sender, EventArgs e)
		{
			MagicGroupBox.Enabled = HasMagicBox.Checked;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MagicGroupBox_EnabledChanged(object sender, EventArgs e)
		{
			if (Monster == null)
			{
				HealMagicBox.Checked = false;
				HasDrainMagicBox.Checked = false;
				CastingLevelBox.Value = 0;
				KnownSpellsBox.Items.Clear();
			}
			else
			{
				HealMagicBox.Checked = Monster.HasHealMagic;
				HasDrainMagicBox.Checked = Monster.HasDrainMagic;
				CastingLevelBox.Value = Monster.MagicCastingLevel;
				KnownSpellsBox.Items.Clear();
			}
		}


		#endregion


		#region Properties Tab

		private void NameBox_TextChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Name = NameBox.Text;
		}


		private void AttackSpeedBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.AttackSpeed = TimeSpan.FromMilliseconds((int)AttackSpeedBox.Value);
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

			Monster.DamageDice.Clone(DamageBox.Dice);
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

		private void DirectionBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Direction = (CardinalPoint)DirectionBox.SelectedItem;
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

		/// <summary>
		/// Flees after attack
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FleesBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.FleesAfterAttack = FleesBox.Checked;
		}

		private void FillSquareBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.FillSquare = FillSquareBox.Checked;

		}

		private void NonMaterialBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.NonMaterial = NonMaterialBox.Checked;

		}

		private void PoisonImmunityBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.PoisonImmunity = PoisonImmunityBox.Checked;

		}

		private void ThrowWeaponsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.ThrowWeapons = ThrowWeaponsBox.Checked;

		}

		private void PickupBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.PickupRate = (float) PickupBox.Value / 100.0f;

		}

		private void StealBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.StealRate = (float) StealBox.Value / 100.0f;

		}

		private void UseStairsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.UseStairs = UseStairsBox.Checked;

		}

		private void FlyingBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Flying = FlyingBox.Checked;

		}

		private void SmartAIBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.SmartAI = SmartAIBox.Checked;

		}

		private void TeleportsBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.Teleports = TeleportsBox.Checked;

		}

		private void BackRowAttackBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.BackRowAttack = BackRowAttackBox.Checked;
		}

		private void CanSeeInvisibleBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.CanSeeInvisible = CanSeeInvisibleBox.Checked;
		}

		private void SightRangeBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.SightRange= (int)SightRangeBox.Value;

		}


		private void DefaultBehaviourBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.DefaultBehaviour = (MonsterBehaviour) DefaultBehaviourBox.SelectedItem;

		}

		private void CurrentBehaviourBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.CurrentBehaviour = (MonsterBehaviour)CurrentBehaviourBox.SelectedItem;
		}
		
		private void WeaponNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.WeaponName = (string)WeaponNameBox.SelectedItem;
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
