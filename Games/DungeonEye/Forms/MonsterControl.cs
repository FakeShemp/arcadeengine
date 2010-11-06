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

			DefaultBehaviourBox.BeginUpdate();
			DefaultBehaviourBox.DataSource = Enum.GetNames(typeof(MonsterBehaviour));
			DefaultBehaviourBox.EndUpdate();
		}


		/// <summary>
		/// Sets the monster to edit
		/// </summary>
		/// <param name="monster"></param>
		public void SetMonster(Monster monster)
		{
			//if (Monster != null)
			//    Monster.Dispose();
			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;

			Monster = monster;
			EntityBox.Entity = Monster;
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


			if (Monster == null)
			{
				ArmorClassBox.Value = 0;
				XPRewardBox.Value = 0;
				PocketItemsBox.Items.Clear();
				ScriptBox.SetValues<IMonster>(null);
				TileSetBox.Items.Clear();
				TileIDBox.Items.Clear();
				DamageBox.Dice.Reset();
				if (TileSet != null)
				{
					TileSet.Dispose();
					TileSet = null;
				}

				HasMagicBox.Checked = false;
			}
			else
			{

				if (!string.IsNullOrEmpty(Monster.TileSetName) && TileSet == null)
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

				ScriptBox.SetValues<IMonster>(Monster.Script);

				DamageBox.Dice = Monster.DamageDice;
				FleesBox.Checked = Monster.FleesAfterAttack;
				FlyingBox.Checked = Monster.Flying;
				FillSquareBox.Checked = Monster.FillSquare;
				NonMaterialBox.Checked = Monster.NonMaterial;
				UseStairsBox.Checked = Monster.UseStairs;
				BackRowAttackBox.Checked = Monster.BackRowAttack;
				PoisonImmunityBox.Checked = Monster.PoisonImmunity;
				ThrowWeaponsBox.Checked = Monster.ThrowWeapons;
				SmartAIBox.Checked = Monster.SmartAI;
				CanSeeInvisibleBox.Checked = Monster.CanSeeInvisible;
				SightRangeBox.Value = Monster.SightRange;
				PickupBox.Value = (decimal)Monster.PickupRate * 100;
				StealBox.Value = (decimal)Monster.StealRate * 100;
				CanSeeInvisibleBox.Checked = Monster.CanSeeInvisible;
				TeleportsBox.Checked = Monster.Teleports;
				DefaultBehaviourBox.SelectedItem = Monster.DefaultBehaviour.ToString();
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
			if (DesignMode)
				return;
	
			if (ParentForm != null)
				ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);

			SpriteBatch = new SpriteBatch();
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;

			Monster = null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Load(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			if (DesignMode)
				return;

			Display.Init();
			Display.ClearBuffers();

			if (DesignMode)
				return;

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

			Monster.TileSetName = TileSetBox.SelectedItem as string;

			if (TileSet != null)
				TileSet.Dispose();

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
			if (DesignMode)
				return;
			
			GlControl.MakeCurrent();

			Display.ClearBuffers();

			SpriteBatch.Begin();


			// Background texture
			SpriteBatch.Draw(CheckerBoard, new Rectangle(Point.Empty, GlControl.Size), Color.White);

			if (Monster != null && TileSet != null)
			{
				Tile tile = TileSet.GetTile(Monster.Tile);
				Point pos = new Point((GlControl.Width - tile.Size.Width) / 2, (GlControl.Height - tile.Size.Height) / 2);
				pos.Offset(tile.Origin);
				SpriteBatch.DrawTile(TileSet, Monster.Tile, pos);
			}

			SpriteBatch.End();

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
		/// Spritebatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Background texture
		/// </summary>
		Texture2D CheckerBoard;


		#endregion



		#region Magic Tab

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

			Monster.DefaultBehaviour = (MonsterBehaviour) Enum.Parse(typeof(MonsterBehaviour), (string)DefaultBehaviourBox.SelectedItem);

		}

		#endregion

		private void CastingLevelBox_ValueChanged(object sender, EventArgs e)
		{
			if (Monster == null)
				return;

			Monster.MagicCastingLevel = (int) CastingLevelBox.Value;
		}


	}
}
