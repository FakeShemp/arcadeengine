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
using System.Data;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Entity editor control
	/// </summary>
	public partial class EntityControl : UserControl
	{
		public EntityControl()
		{
			InitializeComponent();

			AlignmentBox.DataSource = Enum.GetValues(typeof(EntityAlignment));
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
		}


		/// <summary>
		/// Rebuild data
		/// </summary>
		void Rebuild()
		{
			if (entity == null)
			{
				hitPointControl1.HitPoint = null;
				StrengthBox.Value = 0;
				DexterityBox.Value = 0;
				ConstitutionBox.Value = 0;
				IntelligenceBox.Value = 0;
				WisdomBox.Value = 0;
				CharismaBox.Value = 0;
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
			}
			else
			{
				hitPointControl1.HitPoint = entity.HitPoint;
				StrengthBox.Value = entity.Strength.Value;
				DexterityBox.Value = entity.Dexterity.Value;
				ConstitutionBox.Value = entity.Constitution.Value;
				IntelligenceBox.Value = entity.Intelligence.Value;
				WisdomBox.Value = entity.Wisdom.Value;
				CharismaBox.Value = entity.Charisma.Value;
				AlignmentBox.SelectedItem = entity.Alignment;
				QuiverBox.Value = entity.Quiver;

				Item item = entity.GetInventoryItem(InventoryPosition.Helmet);
				HelmetBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Primary);
				PrimaryBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Secondary);
				SecondaryBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Armor);
				ArmorBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Wrist);
				WristBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Ring_Left);
				LeftRingBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Ring_Right);
				RightRingBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Feet);
				FeetBox.SelectedItem = item != null ? item.Name : string.Empty;
				item = entity.GetInventoryItem(InventoryPosition.Neck);
				NeckBox.SelectedItem = item != null ? item.Name : string.Empty;
			}
		}


		#region Properties

		/// <summary>
		/// Entity to edit
		/// </summary>
		public Entity Entity
		{
			get
			{
				return entity;
			}

			set
			{
				entity = value;
				Rebuild();
			}
		}
		Entity entity;

		#endregion


		#region Events

		private void StrengthBox_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Strength.Value = (int)StrengthBox.Value;
		}

		private void DexterityBox_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Dexterity.Value = (int)DexterityBox.Value;
		}

		private void ConstitutionBox_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Constitution.Value = (int)ConstitutionBox.Value;
		}

		private void IntelligenceBox_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Intelligence.Value = (int)IntelligenceBox.Value;
		}

		private void WisdomBox_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Wisdom.Value = (int)WisdomBox.Value;
		}

		private void CharismaBox_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Charisma.Value = (int)CharismaBox.Value;
		}


		private void AlignmentBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Alignment = (EntityAlignment)AlignmentBox.SelectedItem;
		}



		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.Quiver = (int)QuiverBox.Value;
		}

		private void ArmorBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Armor, ResourceManager.CreateAsset<Item>((string)ArmorBox.SelectedItem));
		}

		private void WristBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Wrist, ResourceManager.CreateAsset<Item>((string)WristBox.SelectedItem));

		}

		private void LeftRingBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Ring_Left, ResourceManager.CreateAsset<Item>((string)LeftRingBox.SelectedItem));

		}

		private void RightRingBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Ring_Right, ResourceManager.CreateAsset<Item>((string)RightRingBox.SelectedItem));

		}

		private void PrimaryBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Primary, ResourceManager.CreateAsset<Item>((string)PrimaryBox.SelectedItem));
		}

		private void SecondaryBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Secondary, ResourceManager.CreateAsset<Item>((string)SecondaryBox.SelectedItem));

		}

		private void FeetBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Feet, ResourceManager.CreateAsset<Item>((string)FeetBox.SelectedItem));
		}

		private void NeckBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Neck, ResourceManager.CreateAsset<Item>((string)NeckBox.SelectedItem));

		}

		private void HelmetBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.SetInventoryItem(InventoryPosition.Helmet, ResourceManager.CreateAsset<Item>((string)HelmetBox.SelectedItem));

		}


		private void RollAbilitiesBox_Click(object sender, EventArgs e)
		{
			if (entity == null)
				return;

			entity.ReRollAbilities();
			Rebuild();
		}


		#endregion

	}
}
