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
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;


namespace ArcEngine.Editor
{
	/// <summary>
	/// TileSet form editor
	/// </summary>
	internal partial class TileSetForm : AssetEditor
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		public TileSetForm(XmlNode node)
		{
			InitializeComponent();

			//
			Node = node;
			tileSet = new TileSet();

			TileBox = new SelectionBox();
			CollisionBox = new SelectionBox();
			BgColor = Color.GreenYellow;




			// Available textures
			TexturesBox.BeginUpdate();
			TexturesBox.Items.Clear();
			foreach (string name in ResourceManager.GetBinaries(".png$"))
			{
				TexturesBox.Items.Add(name);
			}
			TexturesBox.EndUpdate();

			// Set zoom value
			ZoomBox.SelectedIndex = 0;

		}



		/// <summary>
		/// Save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				tileSet.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<TileSet>(tileSet.Name, doc.DocumentElement);
		}



		#region Helper



		/// <summary>
		/// Rebuilds the list of all available tiles
		/// </summary>
		void RebuildCellList()
		{
			TilesBox.Items.Clear();

			List<int> list = tileSet.GetTiles();
			foreach (int id in list)
				TilesBox.Items.Add(id);

			if (TilesBox.Items.Count > 0)
			{
				TilesBox.SelectedIndex = 0;
				CurrentTile = tileSet.GetTile((int)TilesBox.SelectedItem);
			}
			else
				CurrentTile = null;
			
			TilePropertyGrid.SelectedObject = CurrentTile;
		}


		#endregion



		#region GlTextureControl
		/// <summary>
		/// Paint event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_Paint(object sender, PaintEventArgs e)
		{
			GLTextureControl.MakeCurrent();

			// Background color
	//		Display.ClearColor = BgColor;
			Display.ClearBuffers();

			// No texture 
			if (tileSet.Texture == null)
			{
				GLTextureControl.SwapBuffers();
				return;
			}


			// Get zoom value
			int zoomvalue = int.Parse((string)ZoomBox.SelectedItem);


			// Background texture
			CheckerBoard.Blit(new Rectangle(Point.Empty, GLTextureControl.Size), TextureLayout.Tile);

			// Draw the texture
			Rectangle zoom = new Rectangle(
			TextureOffset.X, TextureOffset.Y,
			tileSet.Texture.Rectangle.Width * zoomvalue, tileSet.Texture.Rectangle.Height * zoomvalue);
			tileSet.Texture.Blit(zoom, tileSet.Texture.Rectangle);


			// If no tile then byebye
			if (tileSet.Count == 0)
			{
				GLTextureControl.SwapBuffers();
				return;
			}


			// Draw the selection box with sizing handles
			if (CurrentTile != null)
			{
				TileBox.Zoom = zoomvalue;
				TileBox.Offset = TextureOffset;
				CurrentTile.Rectangle = TileBox.Rectangle;
				TileBox.Draw();
			}

	
			
			GLTextureControl.SwapBuffers();
		}


		/// <summary>
		/// OnResize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_Resize(object sender, EventArgs e)
		{
			GLTextureControl.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, GLTextureControl.Size);
		}

		/// <summary>
		/// OnMouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_MouseMove(object sender, MouseEventArgs e)
		{
			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle)
			{

				// Smooth the value
				TextureOffset.X -= (LastMousePos.X - e.X) / 1;
				TextureOffset.Y -= (LastMousePos.Y - e.Y) / 1;

				// Store last mouse location
				LastMousePos = e.Location;

				return;
			}

			// Prints the location of the mouse
			int zoomvalue = int.Parse((string)ZoomBox.SelectedItem);
			PositionLabel.Text = (int)((e.Location.X - TextureOffset.X) / zoomvalue) + "," + (int)((e.Location.Y - TextureOffset.Y) / zoomvalue);

			// Prints the size of the current tile
			SizeLabel.Text = TileBox.Rectangle.Width + "," + TileBox.Rectangle.Height;


			// Resize the tile
			if (TilesBox.Items.Count > 0)
				TileBox.OnMouseMove(e);

		}


		/// <summary>
		/// MouseUp event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_MouseUp(object sender, MouseEventArgs e)
		{
			// Transmit event to the selection box
			if (TileBox != null)
				TileBox.OnMouseUp(e);
		}


		/// <summary>
		/// MouseDown event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_MouseDown(object sender, MouseEventArgs e)
		{
			// Size the selection box
			if (TileBox.MouseTool == SelectionBox.MouseTools.NoTool && e.Button == MouseButtons.Left)
			{
				int zoomvalue = int.Parse((string)ZoomBox.SelectedItem);

				TileBox.Rectangle.X = (e.Location.X - TextureOffset.X) / zoomvalue;
				TileBox.Rectangle.Y = (e.Location.Y - TextureOffset.Y) / zoomvalue;
				TileBox.Rectangle.Width = 0;
				TileBox.Rectangle.Height = 0;

				TileBox.MouseTool = SelectionBox.MouseTools.SizeDownRight;
				
			}


			// Transmit event to the selection box
			if (TileBox != null)
				TileBox.OnMouseDown(e);

			// Pan the texture
			if (e.Button == MouseButtons.Middle)
				LastMousePos = e.Location;
		}


		/// <summary>
		/// On PreviewKeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

			switch (e.KeyData)
			{
				case Keys.Up:
				{
					if (e.Shift)
						TileBox.Rectangle.Height--;
					else
						TileBox.Rectangle.Y--;
				}
				break;
				case Keys.Down:
				{
					if (e.Shift)
						TileBox.Rectangle.Height++;
					else
						TileBox.Rectangle.Y++;
				}
				break;
				case Keys.Left:
				{
					if (e.Shift)
						TileBox.Rectangle.Width--;
					else
						TileBox.Rectangle.X--;
				}
				break;
				case Keys.Right:
				{
					if (e.Shift)
						TileBox.Rectangle.Width++;
					else
						TileBox.Rectangle.X++;
				}
				break;

				// Previous tile
				case Keys.PageUp:
				{
					int id = Math.Max(TilesBox.SelectedIndex -1, 0);
					TilesBox.SelectedIndex = id;
				}
				break;

				// Next tile
				case Keys.PageDown:
				{
					TilesBox.SelectedIndex = Math.Min(TilesBox.SelectedIndex + 1, TilesBox.Items.Count -1);
				}
				break;

				default:
				return;
			}

			e.IsInputKey = true;
		}



		#endregion



		#region GLTileControl

		/// <summary>
		/// OnResize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTileControl_Resize(object sender, EventArgs e)
		{
			GLTileControl.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, GLTileControl.Size);
		}

		/// <summary>
		/// OnPaint event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTileControl_Paint(object sender, PaintEventArgs e)
		{
			GLTileControl.MakeCurrent();


			Display.ClearColor = BgColor;
			Display.ClearBuffers();

			// Get zoom value
			int zoomvalue = int.Parse((string)ZoomBox.SelectedItem);

			// Background texture
			CheckerBoard.Blit(new Rectangle(Point.Empty, GLTileControl.Size), TextureLayout.Tile);

			if (TilesBox.Items.Count == 0)
			{
				GLTileControl.SwapBuffers();
				return;
			}

	
			// Draw the tile
			Rectangle zoom = Rectangle.Empty;
			zoom.X = TileOffset.X;
			zoom.Y = TileOffset.Y;
			zoom.Width = CurrentTile.Rectangle.Width * zoomvalue;
			zoom.Height = CurrentTile.Rectangle.Height * zoomvalue;

			tileSet.Texture.Blit(zoom, CurrentTile.Rectangle);
			// Draw Collision box
			if (CollisionBox != null &&  CollisionButton.Checked)
			{
				CollisionBox.Zoom = zoomvalue;
				CollisionBox.Offset = TileOffset;
				CurrentTile.CollisionBox = CollisionBox.Rectangle;
				CollisionBox.Draw();
			}
			
			// Draw HotSpot
			if (HotSpotButton.Checked)
			{
				Point pos = Point.Empty;
				pos.X = (int) (CurrentTile.HotSpot.X * zoomvalue + TileOffset.X);
				pos.Y = (int) (CurrentTile.HotSpot.Y * zoomvalue + TileOffset.Y);

				Rectangle rect = new Rectangle(pos, new Size((int)zoomvalue, (int)zoomvalue));
				Display.DrawRectangle(rect, Color.Red);

			}

			GLTileControl.SwapBuffers();
		}


		/// <summary>
		/// OnMouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTileControl_MouseMove(object sender, MouseEventArgs e)
		{
			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle)
			{

				// Smooth the value
				//Point pos = Point.Empty;
				TileOffset.X -= (LastMousePos.X - e.X) / 1;
				TileOffset.Y -= (LastMousePos.Y - e.Y) / 1;

				// Store last mouse location
				LastMousePos = e.Location;

				return;
			}

			// Prints the location of the mouse
			//int zoomvalue = Int32.Parse((string)ZoomBox.SelectedItem) / 100;
			int zoomvalue = int.Parse((string)ZoomBox.SelectedItem);
			PositionLabel.Text = (int)((e.Location.X - TileOffset.X) / zoomvalue) + "," + (int)((e.Location.Y - TileOffset.Y) / zoomvalue);

			// Prints the size of the current tile
			SizeLabel.Text = CollisionBox.Rectangle.Width + "," + CollisionBox.Rectangle.Height;

			// Set the collision box
			if (CollisionBox != null && CollisionButton.Checked)
				CollisionBox.OnMouseMove(e);


			// Set the hotspot
			if (HotSpotButton.Checked && e.Button == MouseButtons.Left)
			{
				if (CurrentTile!= null)
				{
					// Get zoom value
					//int zoomvalue = Int32.Parse((string)ZoomBox.SelectedItem) / 100;

					Point pos = Point.Empty;
					pos.X = (e.Location.X - TileOffset.X) / zoomvalue;
					pos.Y = (e.Location.Y - TileOffset.Y) / zoomvalue;

					CurrentTile.HotSpot = pos;
				}

			}

		}


		/// <summary>
		/// OnMouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTileControl_MouseDown(object sender, MouseEventArgs e)
		{
			// Size the selection box
			if (CollisionBox.MouseTool == SelectionBox.MouseTools.NoTool && 
				e.Button == MouseButtons.Left && 
				CollisionButton.Checked)
			{
				int zoomvalue = Int32.Parse((string)ZoomBox.SelectedItem);

				CollisionBox.Rectangle.X = (e.Location.X - TileOffset.X) / zoomvalue;
				CollisionBox.Rectangle.Y = (e.Location.Y - TileOffset.Y) / zoomvalue;
				CollisionBox.Rectangle.Width = 0;
				CollisionBox.Rectangle.Height = 0;

				CollisionBox.MouseTool = SelectionBox.MouseTools.SizeDownRight;
			}


			// Transmit event to the selection box
			if (CollisionBox != null && CollisionButton.Checked)
				CollisionBox.OnMouseDown(e);


			// Set the hotspot
			if (HotSpotButton.Checked && e.Button == MouseButtons.Left)
			{
				if (CurrentTile != null)
				{
					// Get zoom value
					int zoomvalue = Int32.Parse((string)ZoomBox.SelectedItem);

					Point pos = Point.Empty;
					pos.X = (e.Location.X - TileOffset.X) / zoomvalue;
					pos.Y = (e.Location.Y - TileOffset.Y) / zoomvalue;

					CurrentTile.HotSpot = pos;
				}

			}

			// Pan the texture
			if (e.Button == MouseButtons.Middle)
				LastMousePos = e.Location;

		}

		/// <summary>
		/// OnMouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTileControl_MouseUp(object sender, MouseEventArgs e)
		{
			// Transmit event to the selection box
			if (CollisionBox != null && CollisionButton.Checked)
				CollisionBox.OnMouseUp(e);
		}
		
		#endregion



		#region Events

		/// <summary>
		/// Loads the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetForm_Load(object sender, EventArgs e)
		{
			GLTextureControl.MakeCurrent();
			Display.Init();


			GLTileControl.MakeCurrent();
			Display.Init();

			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


			//tileSet = new TileSet();
			tileSet.Load(Node);

			// Build Cell list
			RebuildCellList();
		}

		/// <summary>
		/// Timer Tick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			GLTextureControl.Invalidate();
			GLTileControl.Invalidate();
		}



		/// <summary>
		/// Selected Tile changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TilesBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentTile = tileSet.GetTile((int)TilesBox.SelectedItem);
			TilePropertyGrid.SelectedObject = CurrentTile;

			if (CurrentTile == null)
				return;

			TileBox.Rectangle = CurrentTile.Rectangle;
			CollisionBox.Rectangle = CurrentTile.CollisionBox;


			// Prints the size of the current tile
			SizeLabel.Text = TileBox.Rectangle.Width + "," + TileBox.Rectangle.Height;



		}



		/// <summary>
		/// Zoom in the texture preview panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZoomInButton_Click(object sender, EventArgs e)
		{
			int index = ZoomBox.SelectedIndex + 1;
			if (index >= ZoomBox.Items.Count)
				return;

			ZoomBox.SelectedIndex = index;
		}


		/// <summary>
		/// Zoom out the texture preview panel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZoomOutButton_Click(object sender, EventArgs e)
		{
			int index = ZoomBox.SelectedIndex - 1;
			if (index < 0)
				return;

			ZoomBox.SelectedIndex = index;
		}


		/// <summary>
		/// Changes the background color
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BgColorButton_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.AllowFullOpen = true;
			dlg.AnyColor = true;
			dlg.FullOpen = true;
			dlg.ShowHelp = true;
			dlg.Color = BgColor;


			if (dlg.ShowDialog() != DialogResult.OK)
				return;

			BgColor = dlg.Color;
			GLTextureControl.MakeCurrent();
			Display.ClearColor = dlg.Color;

			//GLTileControl.MakeCurrent();
			//Video.ClearColor = dlg.Color;
			//GLTextureControl.MakeCurrent();
			//Video.ClearColor = dlg.Color;
		}



		/// <summary>
		/// Erases current Tile
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EraseTileButton_Click(object sender, EventArgs e)
		{
			if (TilesBox.Items.Count == 0)
				return;

			if (MessageBox.Show("Removes this tile ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			int id = (int )TilesBox.SelectedItem;
			tileSet.Remove(id);


			int index = TilesBox.SelectedIndex;
			RebuildCellList();

			if (TilesBox.Items.Count > 0)
				TilesBox.SelectedIndex = Math.Max(0, index - 1);

		}


		/// <summary>
		/// Adds a tile
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddTileButton_Click(object sender, EventArgs e)
		{
			int id = 0;

			// If there's tile, then get the last one and add 1
			if (TilesBox.Items.Count > 0)
				id = (int) TilesBox.Items[TilesBox.Items.Count -1] + 1;


			// Create the tile
			Tile tile = tileSet.AddTile(id);


			// Rebuil the list
			RebuildCellList();

			TilesBox.SelectedIndex = TilesBox.Items.Count - 1;
			TilePropertyGrid.SelectedObject = tile;

		}


		/// <summary>
		/// Recenter the view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActualSizeButton_Click(object sender, EventArgs e)
		{
			ZoomBox.SelectedIndex = 0;

			TextureOffset = Point.Empty;
			TileOffset = Point.Empty;
		}




		/// <summary>
		/// HotSpot mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HotSpotButton_Click(object sender, EventArgs e)
		{
			CollisionButton.Checked = false;
		}


		/// <summary>
		/// Collision box mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CollisionButton_Click(object sender, EventArgs e)
		{
			HotSpotButton.Checked = false;
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("TileSet Editor", "Save modifciations ?", MessageBoxButtons.YesNoCancel);
			if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}
			else if (result == DialogResult.Yes)
				Save();

			
		}


		/// <summary>
		/// Change the texture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TexturesBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (TexturesBox.SelectedItem == null)
				return;

			tileSet.LoadTexture(TexturesBox.SelectedItem as string);
		}



		#endregion



		#region Properties

		/// <summary>
		/// Handle to the asset
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return tileSet;
			}
		}


		/// <summary>
		/// Background texture
		/// </summary>
		Texture CheckerBoard;


		/// <summary>
		/// Background color
		/// </summary>
		Color BgColor = Color.Black;


		/// <summary>
		/// Tile to edit
		/// </summary>
		Tile CurrentTile;


		/// <summary>
		/// Current TileSet
		/// </summary>
		TileSet tileSet;


		/// <summary>
		/// Tile selection box
		/// </summary>
		SelectionBox TileBox;

		/// <summary>
		/// Collision box
		/// </summary>
		SelectionBox CollisionBox;


		/// <summary>
		/// Offset of the texture
		/// </summary>
		Point TextureOffset;



		/// <summary>
		/// Offset of the tile
		/// </summary>
		Point TileOffset;


		/// <summary>
		/// Offset of the mouse last update
		/// </summary>
		Point LastMousePos;


		/// <summary>
		/// 
		/// </summary>
		XmlNode Node;



		#endregion




	}


}
