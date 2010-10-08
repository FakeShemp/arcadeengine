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
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Control to edit Hero's parameters
	/// </summary>
	public partial class HeroControl : UserControl, IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public HeroControl()
		{
			InitializeComponent();

			Spells = new List<Spell>();

			AlignmentBox.BeginUpdate();
			AlignmentBox.DataSource = Enum.GetValues(typeof(EntityAlignment));
			AlignmentBox.EndUpdate();

			RaceBox.BeginUpdate();
			RaceBox.DataSource = Enum.GetValues(typeof(HeroRace));
			RaceBox.EndUpdate();

			
		}


		/// <summary>
		/// Dispose
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);


			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (Heads != null)
				Heads.Dispose();
			Heads = null;
		}


		/// <summary>
		/// Rebuild data
		/// </summary>
		void Rebuild()
		{
			if (Hero == null)
			{
				#region Inventory
				QuiverBox.Value = 0;
				HelmetBox.SelectedItem = string.Empty;
				PrimaryBox.SelectedItem = string.Empty;
				SecondaryBox.SelectedItem = string.Empty;
				ArmorBox.SelectedItem = string.Empty;
				WristBox.SelectedItem = string.Empty;
				LeftRingBox.SelectedItem = string.Empty;
				RightRingBox.SelectedItem = string.Empty;
				FeetBox.SelectedItem = string.Empty;
				NeckBox.SelectedItem = string.Empty;
				#endregion

				#region Properties
				StrengthBox.Ability = null;
				IntelligenceBox.Ability = null;
				WisdomBox.Ability = null;
				DexterityBox.Ability = null;
				ConstitutionBox.Ability = null;
				CharismaBox.Ability = null;

				ArmorClassBox.Text = "0";

				HPBox.HitPoint = null;

				#endregion


			}
			else
			{
				QuiverBox.Value = Hero.Quiver;

				#region Inventory
				Item item = Hero.GetInventoryItem(InventoryPosition.Helmet);
				HelmetBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Primary);
				PrimaryBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Secondary);
				SecondaryBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Armor);
				ArmorBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Wrist);
				WristBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Ring_Left);
				LeftRingBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Ring_Right);
				RightRingBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Feet);
				FeetBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = Hero.GetInventoryItem(InventoryPosition.Neck);
				NeckBox.SelectedItem = item != null ? item.Name : string.Empty;
				#endregion

				#region Properties
				StrengthBox.Ability = Hero.Strength;
				IntelligenceBox.Ability = Hero.Intelligence;
				WisdomBox.Ability = Hero.Wisdom;
				DexterityBox.Ability = Hero.Dexterity;
				ConstitutionBox.Ability = Hero.Constitution;
				CharismaBox.Ability = Hero.Charisma;

				RaceBox.SelectedItem = Hero.Race;
				AlignmentBox.SelectedItem = Hero.Alignment;

				ArmorClassBox.Text = Hero.ArmorClass.ToString();

				HPBox.HitPoint = Hero.HitPoint;

				#endregion
			}

			ProfessionsBox.Hero = Hero;


			// Spells
			if (!DesignMode)
			{
				Spells.Clear();
				List<string> spells = ResourceManager.GetAssets<Spell>();
				foreach (string name in spells)
					Spells.Add(ResourceManager.CreateAsset<Spell>(name));

				SpellLevel = 1;
			}

			// Properties
			if (Hero != null)
			{
			}

			OpenGLBox.Invalidate();
			
		}


		#region Main control events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HeroControl_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			Rebuild();
			RebuildProperties();
			RebuildSpells();
		}

		#endregion


		#region Spells events


		/// <summary>
		/// check all known spells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckAllLearnedBox_Click(object sender, EventArgs e)
		{
			for (int i = 0 ; i < LearnedSpellBox.Items.Count ; i++)
			{
				if (!LearnedSpellBox.GetItemChecked(i))
					LearnedSpellBox.SetItemChecked(i, true);
			}
		}


		/// <summary>
		/// Uncheck all known spells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UncheckAllLearnedBox_Click(object sender, EventArgs e)
		{
			for (int i = 0 ; i < LearnedSpellBox.Items.Count ; i++)
			{
				if (LearnedSpellBox.GetItemChecked(i))
					LearnedSpellBox.SetItemChecked(i, false);
			}
		}



		/// <summary>
		/// Change spell's level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpellLevelBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RebuildSpells();
		}


		/// <summary>
		/// Rebuild spell list
		/// </summary>
		void RebuildSpells()
		{
			if (Hero == null)
				return;

			// Spells ready
			SpellReadyBox.BeginUpdate();
			SpellReadyBox.Items.Clear();
			foreach (Spell spell in Hero.Spells)
			{
				if (spell.Level == SpellLevel)
					SpellReadyBox.Items.Add(spell.Name);
			}
			SpellReadyBox.EndUpdate();

			AvailableSpellBox.BeginUpdate();
			AvailableSpellBox.Items.Clear();
			LearnedSpellBox.BeginUpdate();
			LearnedSpellBox.Items.Clear();
			foreach (Spell spell in Spells)
			{
				// Learned spells
				if (spell.Class == HeroClass.Mage && spell.Level == SpellLevel)
				{
					bool check = false;

					if (Hero.LearnedSpells.Contains(spell.Name))
						check = true;

					LearnedSpellBox.Items.Add(spell.Name, check);
				}

				// Available spells
				if (spell.Level == SpellLevel)
					AvailableSpellBox.Items.Add(spell.Class + " - " + spell.Name);

			}
			LearnedSpellBox.EndUpdate();
			AvailableSpellBox.EndUpdate();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LearnedSpellBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (Hero == null)
				return;

			CheckedListBox clb = sender as CheckedListBox;

			if (e.NewValue == CheckState.Checked)
			{
				if (!Hero.LearnedSpells.Contains((string) clb.Items[e.Index]))
					hero.LearnedSpells.Add((string) clb.Items[e.Index]);
			}
			else if (e.NewValue == CheckState.Unchecked)
			{
				if (Hero.LearnedSpells.Contains((string) clb.Items[e.Index]))
					hero.LearnedSpells.Remove((string) clb.Items[e.Index]);
			}

		}


		#endregion


		#region Properties events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenGLBox_Load(object sender, EventArgs e)
		{
			if (DesignMode)
				return;

			try
			{
				OpenGLBox.MakeCurrent();
				Display.Init();
				Display.RenderState.ClearColor = Color.Black;



				Heads = ResourceManager.CreateAsset<TileSet>("Heroes");
				if (Heads != null)
				{
					Heads.Scale = new Vector2(2.0f, 2.0f);
				}

				Batch = new SpriteBatch();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenGLBox_Resize(object sender, EventArgs e)
		{
			if (Batch == null || Heads == null)
				return;

			OpenGLBox.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, OpenGLBox.Size);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenGLBox_Paint(object sender, PaintEventArgs e)
		{
			if (Batch == null || Heads == null)
				return;

			OpenGLBox.MakeCurrent();
			Display.ClearBuffers();

			if (Heads != null && Batch != null && Hero != null)
			{

				Batch.Begin();
				Batch.DrawTile(Heads, Hero.Head, Point.Empty);
				Batch.End();
			}


			OpenGLBox.SwapBuffers();
		}



		/// <summary>
		/// Next hero face
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NextFaceBox_Click(object sender, EventArgs e)
		{
			if (Hero == null || Heads == null)
				return;

			int id = Hero.Head + 1;
			if (id >= Heads.Count)
				id = 0;

			Hero.Head = id;
			OpenGLBox.Invalidate();
		}


		/// <summary>
		/// Previous Hero face
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviousFaceBox_Click(object sender, EventArgs e)
		{
			if (Hero == null || Heads == null)
				return;

			int id = Hero.Head - 1;
			if (id <= 0)
				id = Heads.Count - 1;

			Hero.Head = id;
			OpenGLBox.Invalidate();
		}


	
		/// <summary>
		/// Rebuild properties
		/// </summary>
		void RebuildProperties()
		{
			if (this.DesignMode || Hero == null)
				return;

			List<string> list = ResourceManager.GetAssets<Item>();
			list.Insert(0, "");
			HelmetBox.DataSource = new List<string>(list);
			PrimaryBox.DataSource = new List<string>(list);
			SecondaryBox.DataSource = new List<string>(list);
			ArmorBox.DataSource = new List<string>(list);
			WristBox.DataSource = new List<string>(list);
			LeftRingBox.DataSource = new List<string>(list);
			RightRingBox.DataSource = new List<string>(list);
			FeetBox.DataSource = new List<string>(list);
			NeckBox.DataSource = new List<string>(list);

			FoodBox.Value = Hero.Food;
			HPBox.HitPoint = Hero.HitPoint;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FoodBox_ValueChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.Food = (int) FoodBox.Value;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.Quiver = (int) QuiverBox.Value;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ArmorBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Armor, ResourceManager.CreateAsset<Item>((string)ArmorBox.SelectedItem));
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WristBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Wrist, ResourceManager.CreateAsset<Item>((string)WristBox.SelectedItem));

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LeftRingBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Ring_Left, ResourceManager.CreateAsset<Item>((string)LeftRingBox.SelectedItem));

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RightRingBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Ring_Right, ResourceManager.CreateAsset<Item>((string)RightRingBox.SelectedItem));

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrimaryBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Primary, ResourceManager.CreateAsset<Item>((string)PrimaryBox.SelectedItem));
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SecondaryBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Secondary, ResourceManager.CreateAsset<Item>((string)SecondaryBox.SelectedItem));
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FeetBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Feet, ResourceManager.CreateAsset<Item>((string)FeetBox.SelectedItem));
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NeckBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Neck, ResourceManager.CreateAsset<Item>((string)NeckBox.SelectedItem));

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HelmetBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.SetInventoryItem(InventoryPosition.Helmet, ResourceManager.CreateAsset<Item>((string)HelmetBox.SelectedItem));

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AlignmentBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.Alignment = (EntityAlignment)AlignmentBox.SelectedItem;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RaceBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			Hero.Race = (HeroRace) RaceBox.SelectedItem;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Hero to edit
		/// </summary>
		public Hero Hero
		{
			get
			{
				return hero;
			}
			set
			{
				hero = value;
				Rebuild();
			}
		}
		Hero hero;


		/// <summary>
		/// Spells list
		/// </summary>
		List<Spell> Spells;


		/// <summary>
		/// Current spell level
		/// </summary>
		int SpellLevel
		{
			get
			{
				return SpellLevelBox.SelectedIndex + 1;
			}
			set
			{
				if (value < 1 | value > 6)
					return;
				SpellLevelBox.SelectedIndex = value - 1;
			}
		}


		/// <summary>
		/// Heroe's heads
		/// </summary>
		TileSet Heads;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch Batch;

		#endregion

	}
}
