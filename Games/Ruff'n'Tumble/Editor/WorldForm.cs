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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using RuffnTumble;
using WeifenLuo.WinFormsUI.Docking;


namespace RuffnTumble.Editor
{
	/// <summary>
	/// 
	/// </summary>
	public partial class WorldForm : AssetEditorBase
	{

		#region Misc


		/// <summary>
		/// Constructor
		/// </summary>
		public WorldForm(XmlNode node)
		{
			InitializeComponent();



			//layerBrush = new LayerBrush("internal");

			TilePanel = new LevelTilePanel();
			WorldPanel = new WorldPanel();
			BrushPanel = new LevelBrushPanel();



			World = new World();
			World.Load(node);
			World.Init();

			//Brush = new BrushTool(level);

			TilePanel.Init(this);
			BrushPanel.Init(this);
			WorldPanel.Init(this);


			DrawTimer.Start();
		}



		/// <summary>
		/// Save the world
		/// </summary>
		public override void Save()
		{
			ResourceManager.AddAsset<World>(World.Name, ResourceManager.ConvertAsset(World));
		}


		/// <summary>
		/// Resizes scrollers
		/// </summary>
		void ResizeScrollers()
		{
			// No layer selected
			if (Level == null)
				return;

			// Blocks in layer are invalid
			if (Level.BlockSize.IsEmpty)
				return;

			if (LevelHScroller != null)
			{
				LevelHScroller.LargeChange = GlControl.Width / (Level.BlockSize.Width);
				LevelHScroller.Maximum = (Level.SizeInPixel.Width - GlControl.Width) / (Level.BlockSize.Width * (int)Level.Camera.Scale.X) + LevelHScroller.LargeChange;
				LevelHScroller.Maximum = 1000;
			}


			if (LevelVScroller != null)
			{
				LevelVScroller.LargeChange = GlControl.Height / Level.BlockSize.Height; //GlControl.Height;
				LevelVScroller.Maximum = (Level.SizeInPixel.Height - GlControl.Height) / (Level.BlockSize.Height * (int)Level.Camera.Scale.Y) + LevelVScroller.LargeChange;
				LevelVScroller.Maximum = 1000;
			}
		}


		/// <summary>
		/// Sets the location in the layer
		/// </summary>
		public void ScrollLayer(Point pos)
		{
			Level.Camera.Location = new Point(pos.X * Level.BlockSize.Width, pos.Y * Level.BlockSize.Height);

			// Update the display to be smoothy
			GlControl.Invalidate();
		}


		/// <summary>
		/// Change the level
		/// </summary>
		/// <param name="name"></param>
		public void ChangeLevel(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (World == null)
				return;

			World.SetLevel(name);

		}


		#endregion



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorldForm_Load(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.Init();
			GlControl_Resize(null, null);

			Batch = new SpriteBatch();

			// Preload texture resources
			CheckerBoard = new Texture2D(ResourceManager.GetInternalResource("ArcEngine.Resources.checkerboard.png"));
			CheckerBoard.HorizontalWrap = TextureWrapFilter.Repeat;
			CheckerBoard.VerticalWrap = TextureWrapFilter.Repeat;


			//stream = a.GetManifestResourceStream("ArcEngine.Editor.Resources.SpawnPoint.png");
			//SpawnPointTexture = new Texture("SpawnPoint");
			//SpawnPointTexture.LoadImage(stream);
			//stream.Dispose();


		}

		
		/// <summary>
		/// First time the form is shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelForm_Shown(object sender, EventArgs e)
		{
			TilePanel.Show(dockPanel);
			BrushPanel.Show(dockPanel);
			WorldPanel.Show(dockPanel);
		}



		/// <summary>
		/// Selects the clear color
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearColor_OnClick(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = Display.RenderState.ClearColor;
			dlg.AllowFullOpen = true;
			dlg.AnyColor = true;
			dlg.FullOpen = true;
			dlg.ShowDialog();

			Display.RenderState.ClearColor = Color.FromArgb(dlg.Color.R, dlg.Color.G, dlg.Color.B);
			GlControl.Invalidate();
		}


		/// <summary>
		/// HScroller resize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Scroller_OnResize(object sender, EventArgs e)
		{
			ResizeScrollers();
		}



		/// <summary>
		/// Scroller scroll
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Scroller_OnScroll(object sender, ScrollEventArgs e)
		{
			ScrollLayer(new Point(LevelHScroller.Value, LevelVScroller.Value));
		}




		/// <summary>
		/// GlControl resize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControl.Size);
			if (Level != null)
				Level.Camera.ViewPort = new Rectangle(new Point(), GlControl.Size);
		}



		/// <summary>
		/// Draw timer tick. Time to refresh the display
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawTimer_Tick(object sender, EventArgs e)
		{
			// If not floating panel and the panel is active and AutoRefresh is OK
			if (this.DockAreas != DockAreas.Float && DockPanel.ActiveDocument == this && AutoRefresh)
			{
				GlControl.Invalidate();
				TilePanel.Invalidate();
				BrushPanel.Invalidate();
			}


		}




		/// <summary>
		/// Adds an entity to the layer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddEntityMenu_Click(object sender, EventArgs e)
		{
			Point pos = new Point(LevelContextMenuStrip.Left, LevelContextMenuStrip.Top);

			new Wizards.NewEntityWizard(Level, pos).ShowDialog();
		}

/*
		/// <summary>
		/// List all available entities in the layer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RebuildLevelList(object sender, EventArgs e)
		{
			LevelsBox.Items.Clear();

	//		if (CurrentLayer == null)
	//			return;

			foreach (string name in World.GetLevels())
				LevelsBox.Items.Add(name);
		}


		/// <summary>
		/// Rebuild Spanwlist dropdown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RebuildSpawnPointsList(object sender, EventArgs e)
		{
			ListSpawnPointsBox.Items.Clear();

			if (Level == null)
				return;

			foreach (string name in Level.GetSpawnPoints())
				ListSpawnPointsBox.Items.Add(name);
		}


		/// <summary>
		/// Change the selected SpawnPoint from the box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListSpawnPointsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ListSpawnPointsBox.SelectedIndex == -1)
				return;

			SpawnPoint spawn = Level.GetSpawnPoint((string)ListSpawnPointsBox.SelectedItem);

			LayerPanel.PropertyGridBox.SelectedObject = spawn;


		}


		/// <summary>
		/// New entity selected by the selection dropdown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListEntitiesButton_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (LayerPanel.PropertyGridBox.SelectedObject != null)
			//{
			//    ((Entity)LayerPanel.PropertyGridBox.SelectedObject).Debug = false;
			//}

			// No index selected
			if (LevelsBox.SelectedIndex == -1)
				return;

			//Entity entity = Level.GetEntity((string)LevelsBox.SelectedItem);

			//LayerPanel.PropertyGridBox.SelectedObject = entity;

			//entity.Debug = true;


			if (World == null)
				return;

			World.SetLevel((string)LevelsBox.SelectedItem);

		}


		/// <summary>
		/// Center the view on a specified spawn point
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FindSpawnPointButton_Click(object sender, EventArgs e)
		{
			// No index selected
			if (ListSpawnPointsBox.SelectedIndex == -1)
				return;

			SpawnPoint spawn = Level.GetSpawnPoint((string)ListSpawnPointsBox.SelectedItem);

			int x = (spawn.Location.X - GlControl.Width / 2) / Level.BlockSize.Width;
			int y = (spawn.Location.Y - GlControl.Height / 2) / Level.BlockSize.Height;
			ScrollLayer(new Point(x, y));
		}



		/// <summary>
		/// Center the view on a specified entity
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FindEntityButton_Click(object sender, EventArgs e)
		{
			// No index selected
			if (LevelsBox.SelectedIndex == -1)
				return;

			Entity entity = Level.GetEntity((string)LevelsBox.SelectedItem);

			int x = (entity.Location.X - GlControl.Width / 2) / Level.BlockSize.Width;
			int y = (entity.Location.Y - GlControl.Height / 2) / Level.BlockSize.Height;
			ScrollLayer(new Point(x, y));
		}

*/

		#endregion



		#region Level Control


		/// <summary>
		/// Paste the brush on the layer
		/// </summary>
		private void PasteBrush(Point location)
		{
			// Find the location and paste it
			Point pos = Level.ScreenToLevel(location);
			pos = Level.PositionToBlock(pos);
			LayerBrush.Paste(CurrentLayer, pos);
		}




		/// <summary>
		/// Level paint event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ClearBuffers();

			Batch.Begin();

			// Background texture
			Rectangle rect = new Rectangle(Point.Empty, GlControl.Size);
			Batch.Draw(CheckerBoard, rect, rect,  Color.White);
			//Batch.Draw(CheckerBoard, rect,  Color.White);


			// Draw the level
			if (Level!= null)
				Level.Draw(Batch);

/*
			// No layer selected
			if (CurrentLayer != null)
			{

				//
				// Draw Spawnpoints
				//
				if (CurrentLayer.RenderSpawnPoints)
				{
					SpawnPoint spawn;

					foreach (string name in CurrentLayer.GetSpawnPoints())
					{
						spawn = CurrentLayer.GetSpawnPoint(name);
						if (spawn == null)
							continue;

						Point pos = Level.LevelToScreen(spawn.Location);
						pos.X = pos.X - 8;
						pos.Y = pos.Y - 8;
						Video.Blit(SpawnPointTexture, pos);
					}
					spawn = layerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
					if (spawn != null)
					{

						rect = SpawnPointTexture.Rectangle;
						rect.Offset(spawn.CollisionBoxLocation.Location);
						rect.Location = Level.LevelToScreen(rect.Location);
						Video.Rectangle(rect, false);
					}

				}
 
			}
*/
			// Draw a rectangle showing the brush selection in the layer
			if (sizingBrush)
			{

				//Display.Color = Color.Green;
				Rectangle rec = new Rectangle(
					BrushRectangle.Left * Level.BlockDimension.Width,
					BrushRectangle.Top * Level.BlockDimension.Height,
					BrushRectangle.Width * Level.BlockDimension.Width,
					BrushRectangle.Height * Level.BlockDimension.Height);
				rec.Location = Level.LevelToScreen(rec.Location);
				Batch.DrawRectangle(rec, Color.Green);
				//Display.Color = Color.White;

			}


			// If in paint mode, show the brush
			if (TileMode == TileMode.Pen && !TilePanel.SizingBrush)
			{
/*
				TileSet tileset = CurrentLayer.TileSet;
				//tileset.Bind();

				// Find the good location
				Point pos = GlControl.PointToClient(Control.MousePosition);
				pos.X -= (Level.BlockDimension.Width - Level.Location.X % Level.BlockDimension.Width + pos.X) % Level.BlockDimension.Width;
				pos.Y -= (Level.BlockDimension.Height - Level.Location.Y % Level.BlockDimension.Height + pos.Y) % Level.BlockDimension.Height;

				// Draw the tile on the screen only if the cursor is in the form
				if (pos.X > -Level.BlockDimension.Width)
					LayerBrush.Draw(pos, CurrentLayer.TileSet, Level.BlockDimension);
*/
			}

			Batch.End();
			GlControl.SwapBuffers();
		}



		/// <summary>
		/// KeyDown on the level form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_KeyDown(object sender, KeyEventArgs e)
		{

			switch (e.KeyCode)
			{
				// Cancel all mode
				case Keys.Escape:
				{
					TilePanel.TileMode = TileMode.NoAction;
					//TilePanel.UpdateTileModeButtons();
					Cursor = Cursors.Default;
				}
				break;

				// Fill mode
				case Keys.F:
				{
					if (TilePanel.TileMode == TileMode.Fill)
						TilePanel.TileMode = TileMode.NoAction;
					else
						TilePanel.TileMode = TileMode.Fill;

					//TilePanel.UpdateTileModeButtons();
				}
				break;


				// Grid mode
				//case Keys.G:
				//{
				//    ShowGridButton.PerformClick();
				//}
				//break;


				// Delete selected object
				case Keys.Delete:
				{
/*					
					if (LayerPanel.PropertyGridBox.SelectedObject == null)
						break;

					Entity ent = LayerPanel.PropertyGridBox.SelectedObject as Entity;
					if (ent != null)
					{
						//	CurrentLayer.RemoveEntity(ent.Name);
						LayerPanel.PropertyGridBox.SelectedObject = null;
						//RebuildLevelList(null, null);

						return;
					}

					SpawnPoint spawn = LayerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
					if (spawn != null)
					{
						//	CurrentLayer.RemoveSpawnPoint(spawn.Name);
						LayerPanel.PropertyGridBox.SelectedObject = null;

						//RebuildSpawnPointsList(null, null);
						return;
					}
*/


				}
				break;

				// Paint mode
				case Keys.P:
				{
					if (TileMode == TileMode.Pen)
						TilePanel.TileMode = TileMode.NoAction;
					else
						TilePanel.TileMode = TileMode.Pen;

				}
				break;


				// Brush mode
				case Keys.S:
				{
					if (TileMode == TileMode.Brush)
						TilePanel.TileMode = TileMode.NoAction;
					else
						TilePanel.TileMode = TileMode.Brush;
				}
				break;


			}



		}



		/// <summary>
		/// Mouse down on the level form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_OnMouseDown(object sender, MouseEventArgs e)
		{
			LastMousePos = e.Location;

			// Middle mouse button ? => the scroll enabled
			if (e.Button == MouseButtons.Middle)
			{
				LastMousePos = e.Location;
				Cursor = Cursors.SizeAll;

				return;
			}


			if (CurrentLayer == null)
				return;


			// Left mouse button and PasteTileButton checked ? => Paste tile in the layer
			if (e.Button == MouseButtons.Left && TileMode == TileMode.Pen)
			{
				PasteBrush(e.Location);
				return;
			}




			// Left mouse button and FloodFill checked ? => Fill tile in the layer
			if (e.Button == MouseButtons.Left && TileMode == TileMode.Fill)
			{
				Point pos = Level.ScreenToLevel(e.Location);

				CurrentLayer.FloodFill(Level.PositionToBlock(pos), LayerBrush);
				return;
			}



			// Brush tool in action
			if (e.Button == MouseButtons.Left && TileMode == TileMode.Brush)
			{

				Point pos = e.Location;
				pos.Offset(Level.Camera.Location);

				BrushRectangle = new Rectangle(
					pos.X / Level.BlockDimension.Width,
					pos.Y / Level.BlockDimension.Height,
					1,
					1);

				sizingBrush = true;

				return;
			}



			// Select entity
			if (e.Button == MouseButtons.Left && TileMode != TileMode.Pen && TileMode != TileMode.Fill)
			{
				// UnDebug previous entity
				Entity entity = null; //LayerPanel.PropertyGridBox.SelectedObject as Entity;
				if (entity != null) entity.Debug = false;

				// Find the entity under the mouse
				Point pos = e.Location;
				pos.X += Level.Camera.Location.X;
				pos.Y += Level.Camera.Location.Y;
				//pos.Offset(CurrentLayer.Offset);

				entity = Level.FindEntity(pos);
				//LayerPanel.PropertyGridBox.SelectedObject = entity;
				if (entity != null)
				{
					entity.Debug = true;
					DragObjectByMouse = true;


					// Select it in the dropdown
					//	if (ListEntitiesButton.Items.Contains(entity.Name))
					//		ListEntitiesButton.SelectedIndex = ListEntitiesButton.Items.IndexOf(entity.Name);

					//return;
				}


				SpawnLocation spawn = Level.FindSpawnPoint(pos);
				if (spawn != null)
				{
				//	LayerPanel.PropertyGridBox.SelectedObject = spawn;
					DragObjectByMouse = true;

					// Select it in the dropdown
					//	if (ListSpawnPointsBox.Items.Contains(spawn.Name))
					//		ListSpawnPointsBox.SelectedIndex = ListSpawnPointsBox.Items.IndexOf(spawn.Name);
				}

			}


		}



		/// <summary>
		/// Mouse move events handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_OnMouseMove(object sender, MouseEventArgs e)
		{
			Entity entity;

			// No layer ??
			if (Size.IsEmpty || Level == null)
				return;


			// Display the tile coord
			MousePos = e.Location;
			Point pos = Level.ScreenToLevel(MousePos);

			if (CurrentLayer != null)
			{
				MouseCoordLabel.Text = "Mouse coord : " + pos.X + "x" + pos.Y;
				TileIDLabel.Text = "Tile ID : " + CurrentLayer.GetTileAtPixel(pos).ToString();
				pos = Level.PositionToBlock(pos);
				TileCoordLabel.Text = "Tile coord : " + pos.X + "x" + pos.Y;
			}

			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle)
			{
				// Find the new coord for the level
				pos = Level.Camera.Location;
				pos.X /= Level.BlockSize.Width;
				pos.Y /= Level.BlockSize.Height;

				// Smooth the value
				pos.X += (LastMousePos.X - e.X) / 3;
				pos.Y += (LastMousePos.Y - e.Y) / 3;


				// Scroll the layer
				ScrollLayer(pos);

				// Scroll sliders
				LevelHScroller.Value = Math.Max(0, pos.X);
				LevelVScroller.Value = Math.Max(0, pos.Y);


				DebugLabel.Text = LevelHScroller.Value.ToString() + " " + LevelVScroller.Value.ToString();

				// Store last mouse location
				LastMousePos = e.Location;

				return;
			}


			// Left mouse button and PasteTileButton checked ? => Paste tile in the layer
			if (e.Button == MouseButtons.Left && TileMode == TileMode.Pen)
			{
				PasteBrush(e.Location);
				//GlControl.Draw();

				return;
			}


			// Drag an entity
			if (e.Button == MouseButtons.Left && DragObjectByMouse)
			{
				// Find the delta mouse movement
				Point offset = e.Location;
				offset.X -= LastMousePos.X;
				offset.Y -= LastMousePos.Y;


				// Set entity location
				entity = null; //LayerPanel.PropertyGridBox.SelectedObject as Entity;
				if (entity != null)
				{
					pos = entity.Location;
					pos.X += offset.X;
					pos.Y += offset.Y;
					//pos.Offset(offset);
					entity.Location = pos;
				}
				SpawnLocation spawn = null; //LayerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
				if (spawn != null)
				{
					pos = spawn.Location;
					pos.Offset(offset);
					spawn.Location = pos;
				}

				// Update last mouse location
				LastMousePos = e.Location;

				return;
			}


			// Sizing the brush ?
			if (sizingBrush)
			{
				pos = Level.ScreenToLevel(e.Location);

				BrushRectangle.Size = new Size(pos.X / Level.BlockDimension.Width - BrushRectangle.Left + 1,
												pos.Y / Level.BlockDimension.Height - BrushRectangle.Top + 1);

				return;
			}



			// Something under the mouse ?
			if (CurrentLayer != null && TileMode == TileMode.NoAction)
			{
				pos = Level.ScreenToLevel(e.Location);

				// Entity under the mouse ?
				entity = Level.FindEntity(pos);
				if (entity != null)
				{
					Cursor = Cursors.Hand;
					return;
				}

				SpawnLocation spawn = Level.FindSpawnPoint(pos);
				if (spawn != null)
				{
					Cursor = Cursors.Hand;
					return;
				}


				// Nothing under the mouse...
				Cursor = Cursors.Default;

			}
		}


		/// <summary>
		/// OnMouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_OnMouseUp(object sender, MouseEventArgs e)
		{

			// Stop scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle)
			{
				switch (TileMode)
				{
					case TileMode.Brush:
					{
						Cursor = Cursors.Cross;
					}
					break;

					case TileMode.Fill:
					{
						Cursor = Cursors.Cross;
					}
					break;

					case TileMode.Pen:
					{
						Cursor = Cursors.Cross;
					}
					break;

					case TileMode.Rectangle:
					{
						Cursor = Cursors.Cross;
					}
					break;


					default:
					{
						Cursor = Cursors.Default;
					}
					break;
				}
				return;
			}


			// Entity no more selected
			if (e.Button == MouseButtons.Left && DragObjectByMouse)
			{
				DragObjectByMouse = false;
			}


			// Stop resizing the brush tool
			if (e.Button == MouseButtons.Left && sizingBrush)
			{

				LayerBrush.Size = BrushRectangle.Size;

				for (int y = 0; y < BrushRectangle.Height; y++)
				{
					for (int x = 0; x < BrushRectangle.Width; x++)
					{
						//int BufferID = (BrushRectangle.Top + y) + BrushRectangle.Left + x;
						LayerBrush.Tiles[y][x] = CurrentLayer.GetTileAtBlock(new Point(BrushRectangle.Left + x, BrushRectangle.Top + y));
					}
				}

				/*
								BrushTiles = new List<List<int>>();
								for (int y = 0; y < Brush.Height; y++)
								{
									BrushTiles.Add(new List<int>());
									for (int x = 0; x < Brush.Width; x++)
									{
										int BufferID = CurrentLayer.GetTileAt(new Point(
											Brush.Left + x,
											Brush.Top + y));
										BrushTiles[y].Add(BufferID);
									}
								}

				*/
				sizingBrush = false;

				// Change to pen mode
				TileMode = TileMode.Pen;
			}

		}

		#endregion



		#region Level Context menu

		/// <summary>
		/// Resize the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeLevelMenu_Click(object sender, EventArgs e)
		{
			new Wizards.LevelResizeWizard(Level).ShowDialog();
		}



		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InsertRow_Click(object sender, EventArgs e)
		{
			Point pos = Level.ScreenToLevel(MousePos);

			Level.InsertRow(Level.PositionToBlock(pos).Y, 0);
		}


		/// <summary>
		/// Removes a row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteRow_Click(object sender, EventArgs e)
		{
			Point pos = Level.ScreenToLevel(MousePos);

			Level.RemoveRow(Level.PositionToBlock(pos).Y);

		}

		/// <summary>
		/// Inserts a column
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InsertColumn_Click(object sender, EventArgs e)
		{
			Point pos = Level.ScreenToLevel(MousePos);

			Level.InsertColumn(Level.PositionToBlock(pos).X, 0);
		}

		/// <summary>
		/// Deletes a column
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteColumn_Click(object sender, EventArgs e)
		{
			Point pos = Level.ScreenToLevel(MousePos);

			Level.RemoveColumn(Level.PositionToBlock(pos).X);

		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WorldForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DrawTimer.Stop();
			DialogResult result = MessageBox.Show("Save modifications ?", "Tile Map Editor", MessageBoxButtons.YesNoCancel);


			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
				DrawTimer.Start();
				return;
			}

			if (BrushPanel != null)
				BrushPanel.Close();
			BrushPanel = null;

			if (TilePanel != null)
				TilePanel.Close();
			TilePanel = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;

			if (World != null)
				World.Dispose();
			World = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();

			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			
		}

		#endregion



		#region Properties


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Auto refresh the level
		/// </summary>
		public bool AutoRefresh = true;


		/// <summary>
		/// True if an object is currently selected
		/// </summary>
		bool DragObjectByMouse;



		/// <summary>
		/// Current selected layer
		/// </summary>
		public Layer CurrentLayer
		{
			get;
			set;
		}



		/// <summary>
		/// Return the current level
		/// </summary>
		public Level Level
		{
			get
			{
				if (World == null)
					return null;

				return World.CurrentLevel;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public World World
		{
			get;
			private set;
		}

		/// <summary>
		/// Offset of the mouse when scrolling with middle mouse button
		/// </summary>
		Point LastMousePos;

		/// <summary>
		/// Current position of the mouse
		/// </summary>
		Point MousePos;


		/// <summary>
		/// Checkerboard texture
		/// </summary>
		Texture2D CheckerBoard;



		/// <summary>
		/// Current LayerBrush
		/// </summary>
		public LayerBrush LayerBrush
		{
			get;
			set;
		}


		/// <summary>
		/// Brush rectangle in the layer
		/// </summary>
		Rectangle BrushRectangle;


		/// <summary>
		/// Are we sizng a brush in the layer
		/// </summary>
		bool sizingBrush;


		/// <summary>
		/// Current tile mode
		/// </summary>
		public TileMode TileMode
		{
			get
			{
				return TilePanel.TileMode;
			}
			set
			{
				TilePanel.TileMode = value;
			}
		}


		/// <summary>
		/// TilePanel form
		/// </summary>
		public LevelTilePanel TilePanel
		{
			get;
			private set;
		}
/*

		/// <summary>
		/// LayerPanel
		/// </summary>
		public LevelLayerPanel LayerPanel
		{
			get;
			private set;
		}

*/

		/// <summary>
		/// Level property panel
		/// </summary>
		public WorldPanel WorldPanel
		{
			get;
			private set;
		}


		/// <summary>
		/// Level brush panel
		/// </summary>
		public LevelBrushPanel BrushPanel
		{
			get;
			private set;
		}


		#endregion
	}





	/// <summary>
	/// Current tile mode
	/// </summary>
	public enum TileMode
	{
		/// <summary>
		/// No action
		/// </summary>
		NoAction,

		/// <summary>
		/// Paste tile mode
		/// </summary>
		Pen,

		/// <summary>
		/// Rectangle mode
		/// </summary>
		Rectangle,


		/// <summary>
		/// Flood file mode
		/// </summary>
		Fill,

		/// <summary>
		/// Selection a rectangle of tiles in the current layer
		/// </summary>
		Brush,


	}


}
