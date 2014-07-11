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
using ArcEngine.Interface;
using ArcEngine.Input;

namespace ArcEngine.Editor
{
	/// <summary>
	/// TileSet form editor
	/// </summary>
	internal partial class TileSetForm : AssetEditorBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node">Xml node</param>
		public TileSetForm(XmlNode node)
		{
			InitializeComponent();

			//
			Node = node;
			TileSet = new TileSet();

			SelectionTool = new SelectionTool();
			CollisionSelection = new SelectionTool();


			GLTileControl.MouseWheel += new MouseEventHandler(GLTileControl_MouseWheel);
			GLTextureControl.MouseWheel += new MouseEventHandler(GLTextureControl_MouseWheel);


			SurroundTileColor = Color.Yellow;
			SurroundColorBox.BackColor = SurroundTileColor;

			EdgeDetectColor = Color.Red;
			ColorPanelBox.BackColor = EdgeDetectColor;

			// Set zoom value
			ZoomBox.SelectedIndex = 0;


		}


		/// <summary>
		/// Save the asset to the manager
		/// </summary>
		public override void Save()
		{
			ResourceManager.AddAsset<TileSet>(TileSet.Name, ResourceManager.ConvertAsset(TileSet));
		}


		/// <summary>
		/// Uncheck all checkboxes
		/// </summary>
		/// <param name="exclude">Control to exclude</param>
		void UncheckButtons(ToolStripButton exclude)
		{
			ToolStripButton[] boxes = new ToolStripButton[]
			{
				SelectionBox,
				HotSpotBox,
				ColisionBox,
			};

			foreach (ToolStripButton box in boxes)
			{
				if (box != exclude)
					box.Checked = false;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		bool SameColor(Color left, Color right)
		{
			return (
				left.A == right.A &&
				left.R == right.R &&
				left.G == right.G &&
				left.B == right.B);
		}


		/// <summary>
		/// Detect edges of a tile
		/// </summary>
		/// <param name="start">Location</param>
		/// <returns></returns>
		private Rectangle DetectTileEdges(Point start)
		{
			if (TileSet.Texture == null || !TileSet.Texture.Bounds.Contains(start))
				return Rectangle.Empty;


			Cursor cursor = Cursor;
			Cursor = Cursors.WaitCursor;

			// Bound
			Rectangle bound = new Rectangle(start.X, start.Y, 1, 1);

			// Get colors
			Color[,] colors = TileSet.Texture.GetColors();

			// Find left border
			while (bound.X > 0)
			{
				if (SameColor(colors[bound.X - 1, bound.Y], EdgeDetectColor))			
					break;
				bound.X--;
			}

			// Find top
			while (bound.Y > 0)
			{
				if (SameColor(colors[bound.X, bound.Y - 1], EdgeDetectColor))
					break;
				bound.Y--;
			}

			// Find right
			while (bound.Right < TileSet.Texture.Size.Width - 1)
			{
				if (SameColor(colors[bound.Right + 1, bound.Bottom], EdgeDetectColor))
					break;

				bound.Width++;
			}

			// Find bottom
			while (bound.Bottom < TileSet.Texture.Size.Height - 1)
			{
				if (SameColor(colors[bound.Right, bound.Bottom + 1], EdgeDetectColor))
					break;

				bound.Height++;
			}

			bound.Width++;
			bound.Height++;

			Cursor = cursor;
			return bound;
		}


		/// <summary>
		/// Remove a tile
		/// </summary>
		/// <param name="id"></param>
		void RemoveTile(int id)
		{
			if (MessageBox.Show("Remove this tile ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			TileSet.Remove(id);

			SelectionTool.Rectangle = Rectangle.Empty;
			CurrentTile = null;
		}


		/// <summary>
		/// Change the tile to edit
		/// </summary>
		/// <param name="tile">Tile handle</param>
		void SetCurrentTile(Tile tile)
		{
			CurrentTile = tile;
			TilePropertyGrid.SelectedObject = CurrentTile;

			if (CurrentTile != null)
			{


				SelectionTool.Rectangle = CurrentTile.Rectangle;
				CollisionSelection.Rectangle = CurrentTile.CollisionBox;


				// Prints the size of the current tile
				SizeLabel.Text = SelectionTool.Rectangle.Width + "," + SelectionTool.Rectangle.Height;
			}
			else
			{
				SelectionTool.Rectangle = Rectangle.Empty;
			}
		}




		#region GlTextureControl


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void GLTextureControl_MouseWheel(object sender, MouseEventArgs e)
		{

			if (e.Delta > 0)
			{
				ZoomInButton.PerformClick();
			}
			else if (e.Delta < 0)
			{
				ZoomOutButton.PerformClick();
			}



			Point loc = e.Location;
			int zoomvalue = int.Parse((string) ZoomBox.SelectedItem);
			Point point = new Point(((e.Location.X - TextureOffset.X) / zoomvalue) - loc.X,
				(int) ((e.Location.Y - TextureOffset.Y) / zoomvalue) - loc.Y);

			Point point2 = new Point(((e.Location.X - TextureOffset.X) / zoomvalue),
				(int) ((e.Location.Y - TextureOffset.Y) / zoomvalue));

			//			TextureOffset.Offset(point);
		}


		/// <summary>
		/// Paint event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_Paint(object sender, PaintEventArgs e)
		{
			GLTextureControl.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, GLTextureControl.Size);


			// Background color
			Display.ClearBuffers();


			Batch.Begin();


			// Background texture
			Rectangle dst = new Rectangle(Point.Empty, GLTextureControl.Size);
			Batch.Draw(CheckerBoard, dst, dst, Color.White);



			float ZoomFactor = float.Parse((string) ZoomBox.SelectedItem);

			if (TileSet.Texture != null)
			{

				// Draw the tile texture
				Vector4 zoom = new Vector4(
				TextureOffset.X,
				TextureOffset.Y,
				TileSet.Texture.Bounds.Width * ZoomFactor,
				TileSet.Texture.Bounds.Height * ZoomFactor);

				Vector4 src = new Vector4(0.0f, 0.0f, TileSet.Texture.Size.Width, TileSet.Texture.Size.Height);

				Batch.Draw(TileSet.Texture, zoom, src, Color.White);



				// Draw the selection box with sizing handles
				if (CurrentTile != null)
				{
					SelectionTool.Zoom = ZoomFactor;
					SelectionTool.Offset = TextureOffset;
					CurrentTile.Rectangle = SelectionTool.Rectangle;
					SelectionTool.Draw(Batch);
				}

			}

			// Suround all tiles
			if (SurroundTilesBox.Checked)
			{

				foreach (int id in TileSet.Tiles)
				{
					Tile tile = TileSet.GetTile(id);
					if (tile == null)
						continue;

					Rectangle rect = new Rectangle(
						tile.Rectangle.X * (int) ZoomFactor + TextureOffset.X,
						tile.Rectangle.Y * (int) ZoomFactor + TextureOffset.Y,
						tile.Rectangle.Width * (int) ZoomFactor,
						tile.Rectangle.Height * (int) ZoomFactor);

					Batch.DrawRectangle(rect, SurroundTileColor);

					Batch.DrawString(BMFont, rect.Location, SurroundTileColor, id.ToString());
				}
			}



			Batch.End();
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
			int zoomvalue = int.Parse((string) ZoomBox.SelectedItem);
			Point position = new Point((e.Location.X - TextureOffset.X) / zoomvalue, (e.Location.Y - TextureOffset.Y) / zoomvalue);


			#region Pan the texture
			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Right)
			{

				// Smooth the value
				TextureOffset.X -= (LastMousePos.X - e.X) / 1;
				TextureOffset.Y -= (LastMousePos.Y - e.Y) / 1;

				// Store last mouse location
				LastMousePos = e.Location;
			}
			#endregion

			#region Left mouse button
			else if (e.Button == MouseButtons.Left)
			{
			}
			#endregion



			// Update selection
			if (SelectionTool != null && SelectionBox.Checked)// || HotSpotBox.Checked || ColisionBox.Checked))
				SelectionTool.OnMouseMove(e);

			// Prints the location of the mouse
			//PositionLabel.Text = (int) ((e.Location.X - TextureOffset.X) / zoomvalue) + "," + (int) ((e.Location.Y - TextureOffset.Y) / zoomvalue);
			PositionLabel.Text = position.X + "," + position.Y;
			SizeLabel.Text = SelectionTool.Rectangle.Width + "," + SelectionTool.Rectangle.Height;


		}


		/// <summary>
		/// MouseUp event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
			}

			// Transmit event to the selection box
			if (SelectionTool != null && (SelectionBox.Checked || HotSpotBox.Checked || ColisionBox.Checked))
				SelectionTool.OnMouseUp(e);
		}


		/// <summary>
		/// MouseDown event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GLTextureControl_MouseDown(object sender, MouseEventArgs e)
		{
			int zoomvalue = int.Parse((string) ZoomBox.SelectedItem);
			Point position = new Point((e.Location.X - TextureOffset.X) / zoomvalue, (e.Location.Y - TextureOffset.Y) / zoomvalue);

			#region Left
			if (e.Button == MouseButtons.Left)
			{
				// Select a tile by left click + ctrl
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					// Get the tile under the cursor
					foreach (int id in TileSet.Tiles)
					{
						Tile tile = TileSet.GetTile(id);
						if (tile != null && tile.Rectangle.Contains(position))
						{
							TileIDBox.Value = id;
							//SetCurrentTile(tile);
							return;
						}
					}
				}

				// Change tile selection
				else if (SelectionBox.Checked)
				{

					// Size the selection box
					if (SelectionTool.MouseTool == MouseTools.NoTool)
					{
						// No tile, so create one
						if (CurrentTile == null)
						{
							SetCurrentTile(TileSet.AddTile((int) TileIDBox.Value));
						}

						SelectionTool.Rectangle = new Rectangle(position, Size.Empty);
						SelectionTool.MouseTool = MouseTools.SizeDownRight;
					}

				}
				else if (HotSpotBox.Checked)
				{
				}
				else if (ColisionBox.Checked)
				{
				}

			}
			#endregion

			#region Right

			// Auto detect tile border
			else if ((e.Button == MouseButtons.Right))
			{
				// Auto detect edges
				if (SelectionBox.Checked)
				{
					// No texture
					if (TileSet.Texture == null)
						return;

					// No tile, so create one
					if (CurrentTile == null)
					{
						SetCurrentTile(TileSet.AddTile((int) TileIDBox.Value));
					}

					Point start = new Point(
						(int) ((e.Location.X - TextureOffset.X) / zoomvalue),
						(int) ((e.Location.Y - TextureOffset.Y) / zoomvalue));

					SelectionTool.Rectangle = DetectTileEdges(start);
				}
				else
				{
					// Pan the texture
					LastMousePos = e.Location;
				}
			}

			#endregion

			#region Middle
			// Pan the texture
			else if (e.Button == MouseButtons.Middle)
			{
				LastMousePos = e.Location;
			}
			#endregion


			// Transmit event to the selection box
			if (SelectionTool != null && (SelectionBox.Checked || HotSpotBox.Checked || ColisionBox.Checked))
				SelectionTool.OnMouseDown(e);
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
						SelectionTool.Rectangle.Height--;
					else
						SelectionTool.Rectangle.Y--;
				}
				break;
				case Keys.Down:
				{
					if (e.Shift)
						SelectionTool.Rectangle.Height++;
					else
						SelectionTool.Rectangle.Y++;
				}
				break;
				case Keys.Left:
				{
					if (e.Shift)
						SelectionTool.Rectangle.Width--;
					else
						SelectionTool.Rectangle.X--;
				}
				break;
				case Keys.Right:
				{
					if (e.Shift)
						SelectionTool.Rectangle.Width++;
					else
						SelectionTool.Rectangle.X++;
				}
				break;

				// Previous tile
				case Keys.PageUp:
				{
					TileIDBox.Value = Math.Max(TileIDBox.Value - 1, 0);
				}
				break;

				// Next tile
				case Keys.PageDown:
				{
					TileIDBox.Value = Math.Min(TileIDBox.Value + 1, TileIDBox.Maximum);
				}
				break;

				// Remove current tile
				case Keys.Delete:
				{
					RemoveTile((int) TileIDBox.Value);
				}
				break;
			}

			e.IsInputKey = true;
		}


		#endregion



		#region GLTileControl

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void GLTileControl_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				ZoomInButton.PerformClick();
			}
			else if (e.Delta < 0)
			{
				ZoomOutButton.PerformClick();
			}

		}

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
			Display.ClearBuffers();


			Batch.Begin();


			// Background texture
			Rectangle dst = new Rectangle(Point.Empty, GLTileControl.Size);
			Batch.Draw(CheckerBoard, dst, dst, Color.White);


			// No tiles, no draw !
			if (CurrentTile != null)
			{
				// Get zoom value
				float zoomvalue = float.Parse((string) ZoomBox.SelectedItem);

				// Draw the tile
				Vector4 zoom = new Vector4();
				zoom.X = TileOffset.X;
				zoom.Y = TileOffset.Y;
				zoom.Z = CurrentTile.Rectangle.Width * zoomvalue;
				zoom.W = CurrentTile.Rectangle.Height * zoomvalue;

				// Texture source
				Vector4 src = new Vector4(
					CurrentTile.Rectangle.X, CurrentTile.Rectangle.Y,
					CurrentTile.Size.Width, CurrentTile.Size.Height);

				Batch.Draw(TileSet.Texture, zoom, src, Color.White);


				// Draw Collision box
				if (CollisionSelection != null && ColisionBox.Checked)
				{
					CollisionSelection.Zoom = zoomvalue;
					CollisionSelection.Offset = TileOffset;
					CurrentTile.CollisionBox = CollisionSelection.Rectangle;
					CollisionSelection.Draw(Batch);
				}

				// Draw origin
				if (HotSpotBox.Checked)
				{
					Point pos = Point.Empty;
					pos.X = (int) (CurrentTile.Pivot.X * zoomvalue + TileOffset.X);
					pos.Y = (int) (CurrentTile.Pivot.Y * zoomvalue + TileOffset.Y);

					Rectangle rect = new Rectangle(pos, new Size((int) zoomvalue, (int) zoomvalue));
					Batch.FillRectangle(rect, Color.Red);

				}
			}


			Batch.End();
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
			if (e.Button == MouseButtons.Middle ||e.Button == MouseButtons.Right)
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
			int zoomvalue = int.Parse((string) ZoomBox.SelectedItem);
			PositionLabel.Text = (int) ((e.Location.X - TileOffset.X) / zoomvalue) + "," + (int) ((e.Location.Y - TileOffset.Y) / zoomvalue);

			// Prints the size of the current tile
			SizeLabel.Text = CollisionSelection.Rectangle.Width + "," + CollisionSelection.Rectangle.Height;

			// Set the collision box
			if (CollisionSelection != null && ColisionBox.Checked)
				CollisionSelection.OnMouseMove(e);


			// Set the hotspot
			if (HotSpotBox.Checked && e.Button == MouseButtons.Left)
			{
				if (CurrentTile != null)
				{
					// Get zoom value
					//int zoomvalue = Int32.Parse((string)ZoomBox.SelectedItem) / 100;

					Point pos = Point.Empty;
					pos.X = (e.Location.X - TileOffset.X) / zoomvalue;
					pos.Y = (e.Location.Y - TileOffset.Y) / zoomvalue;

					CurrentTile.Pivot = pos;
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
			if (CollisionSelection.MouseTool == MouseTools.NoTool &&
				e.Button == MouseButtons.Left &&
				ColisionBox.Checked)
			{
				int zoomvalue = Int32.Parse((string) ZoomBox.SelectedItem);

				CollisionSelection.Rectangle.X = (e.Location.X - TileOffset.X) / zoomvalue;
				CollisionSelection.Rectangle.Y = (e.Location.Y - TileOffset.Y) / zoomvalue;
				CollisionSelection.Rectangle.Width = 0;
				CollisionSelection.Rectangle.Height = 0;

				CollisionSelection.MouseTool = MouseTools.SizeDownRight;
			}


			// Transmit event to the selection box
			if (CollisionSelection != null && ColisionBox.Checked)
				CollisionSelection.OnMouseDown(e);


			// Set the hotspot
			if (HotSpotBox.Checked && e.Button == MouseButtons.Left)
			{
				if (CurrentTile != null)
				{
					// Get zoom value
					int zoomvalue = Int32.Parse((string) ZoomBox.SelectedItem);

					Point pos = Point.Empty;
					pos.X = (e.Location.X - TileOffset.X) / zoomvalue;
					pos.Y = (e.Location.Y - TileOffset.Y) / zoomvalue;

					CurrentTile.Pivot = pos;
				}

			}

			// Pan the texture
			if (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Right)
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
			if (CollisionSelection != null && ColisionBox.Checked)
				CollisionSelection.OnMouseUp(e);
		}

		#endregion



		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TrimBox_Click(object sender, EventArgs e)
		{
			MessageBox.Show("To do !");
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			ColorDialogBox.Color = EdgeDetectColor;

			if (ColorDialogBox.ShowDialog() != DialogResult.OK)
				return;

			EdgeDetectColor = ColorDialogBox.Color;
			ColorPanelBox.BackColor = EdgeDetectColor;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SurroundTileColorBox_Click(object sender, EventArgs e)
		{
			ColorDialogBox.Color = SurroundTileColor;

			if (ColorDialogBox.ShowDialog() != DialogResult.OK)
				return;

			SurroundTileColor = ColorDialogBox.Color;
			SurroundColorBox.BackColor = SurroundTileColor;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ChangeTextureBox_Click(object sender, EventArgs e)
		{
			StorageBrowserForm form = new StorageBrowserForm();
			form.MultiSelect = false;
			form.FileName = TileSet.TextureName;
			if (form.ShowDialog() != DialogResult.OK)
				return;

			TextureNameBox.Text = form.FileName;
			TileSet.TextureName = form.FileName;

			if (TileSet.Texture != null)
				TileSet.Texture.Dispose();

			TileSet.Texture = new Texture2D(form.FileName);
		}


		/// <summary>
		/// Loads the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetForm_Load(object sender, EventArgs e)
		{
			GLTileControl.MakeCurrent();
			Display.RenderState.Blending = true;
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GLTextureControl.MakeCurrent();
			Display.RenderState.Blending = true;
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


			Batch = new SpriteBatch();

			CheckerBoard = new Texture2D(ResourceManager.GetInternalResource("ArcEngine.Resources.checkerboard.png"));
			CheckerBoard.HorizontalWrap = TextureWrapFilter.Repeat;
			CheckerBoard.VerticalWrap = TextureWrapFilter.Repeat;

			TileSet.Load(Node);

			TextureNameBox.Text = TileSet.TextureName;

			BMFont = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 12, FontStyle.Bold);
		}



		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{

			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;

			if (BMFont != null)
				BMFont.Dispose();
			BMFont = null;

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
			if (TileSet == null)
				return;


			SetCurrentTile(TileSet.GetTile((int) TileIDBox.Value));
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
		/// Erases current Tile
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EraseTileButton_Click(object sender, EventArgs e)
		{
			RemoveTile((int) TileIDBox.Value);
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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToggleStripButtons(object sender, EventArgs e)
		{
			ToolStripButton button = sender as ToolStripButton;

			if (button.Checked)
				UncheckButtons(button);
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
				return TileSet;
			}
		}


		/// <summary>
		/// Background texture
		/// </summary>
		Texture2D CheckerBoard;


		/// <summary>
		/// Tile to edit
		/// </summary>
		Tile CurrentTile;


		/// <summary>
		/// Current TileSet
		/// </summary>
		TileSet TileSet;


		/// <summary>
		/// Tile selection box
		/// </summary>
		SelectionTool SelectionTool;

		/// <summary>
		/// Collision box
		/// </summary>
		SelectionTool CollisionSelection;


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


		/// <summary>
		/// Spritebatch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont BMFont;


		/// <summary>
		/// Color for auto edge detect
		/// </summary>
		Color EdgeDetectColor;


		/// <summary>
		/// Color for surrounding tiles
		/// </summary>
		Color SurroundTileColor;

		#endregion

	}
}
