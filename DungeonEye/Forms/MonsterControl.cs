using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;


namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class MonsterControl : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		public MonsterControl()
		{
			InitializeComponent();
		}


		public bool Init()
		{
	
			GlControl.MakeCurrent();
			Display.Init();
			GlControl_Resize(null, null);
			Display.ClearBuffers();
			GlControl.SwapBuffers();


			// Facing box
			FacingBox.BeginUpdate();
			FacingBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(CardinalPoint)))
			{
				FacingBox.Items.Add(name);
			}
			FacingBox.EndUpdate();



			// Items
			ItemSet itemset = ResourceManager.CreateAsset<ItemSet>("");
			ItemsBox.BeginUpdate();
			ItemsBox.Items.Clear();
			foreach (string item in itemset.Items.Keys)
				ItemsBox.Items.Add(item);
			ItemsBox.EndUpdate();


			UpdateControls();

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		void UpdateControls()
		{
			if (monster == null)
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


			FacingBox.SelectedIndex = (int)monster.Location.Direction;
			HPActualBox.Value = monster.Life.Actual;
			HPMaxBox.Value = monster.Life.Max;

			Visible = true;

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
			if (Monster == null)
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
			if (Monster == null)
				return;
			
			if (PocketItemsBox.SelectedIndex == -1)
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
		private void FacingBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (monster == null)
				return;

			monster.Location.Direction = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), FacingBox.SelectedItem as string, true);
		}



		#endregion



		#region Properties

		/// <summary>
		/// Current monster
		/// </summary>
		public Monster Monster
		{
			get
			{
				return monster;
			}

			set
			{
				monster = value;
				UpdateControls();
			}
		}

		Monster monster;


		/// <summary>
		/// 
		/// </summary>
		TileSet TileSet;

		#endregion
	}
}
