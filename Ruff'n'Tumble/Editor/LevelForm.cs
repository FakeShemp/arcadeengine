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
using ArcEngine.Games.RuffnTumble.Asset;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Games.RuffnTumble.Editor
{
	public partial class LevelForm : AssetEditor
	{

		#region Misc


		/// <summary>
		/// Constructor
		/// </summary>
		public LevelForm(XmlNode node)
		{
			InitializeComponent();

			GlControl.MakeCurrent();
			Display.Init();
			GlLevelControl_Resize(null, null);


			//layerBrush = new LayerBrush("internal");

			TilePanel = new LevelTilePanel();
			LayerPanel = new LevelLayerPanel();
			PropertyPanel = new LevelPropertyPanel();
			BrushPanel = new LevelBrushPanel();


			Level = new Level();
			Level.Load(node);
			Level.Init();

		//	CurrentLayer = Level.Layers[0];

			//Brush = new BrushTool(level);



			// Preload texture resources
			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));

			//Assembly a = Assembly.GetExecutingAssembly();
			//Stream stream = a.GetManifestResourceStream("ArcEngine.Files.checkerboard.png");
			//CheckerBoard = new Texture("CheckerBoard");
			//CheckerBoard.LoadImage(stream);
			//stream.Close();

			//stream = a.GetManifestResourceStream("ArcEngine.Editor.Resources.SpawnPoint.png");
			//SpawnPointTexture = new Texture("SpawnPoint");
			//SpawnPointTexture.LoadImage(stream);
			//stream.Dispose();

			TilePanel.Init(this);
			LayerPanel.Init(this);
			PropertyPanel.Init(this);
			BrushPanel.Init(this);



			// Rebuild ComboBoxes
			RebuildEntityList(null, null);
			RebuildSpawnPointsList(null, null);


			//
			//	DrawTimer.Interval = Config.LevelRefreshRate;
			//	DrawTimer.Start();


		}


		/// <summary>
		/// Resizes scrollers
		/// </summary>
		void ResizeScrollers()
		{
			// No layer selected
			if (CurrentLayer == null)
				return;

			// Blocks in layer are invalid
			if (Level.BlockSize.IsEmpty)
				return;

			if (LevelHScroller != null)
			{
				LevelHScroller.LargeChange = GlControl.Width / (Level.BlockSize.Width);
				LevelHScroller.Maximum = (Level.Dimension.Width - GlControl.Width) / (Level.BlockSize.Width * (int)Level.Zoom.Width) + LevelHScroller.LargeChange;
				LevelHScroller.Maximum = 1000;
			}


			if (LevelVScroller != null)
			{
				LevelVScroller.LargeChange = GlControl.Height / Level.BlockSize.Height; //GlControl.Height;
				LevelVScroller.Maximum = (Level.Dimension.Height - GlControl.Height) / (Level.BlockSize.Height * (int)Level.Zoom.Height) + LevelVScroller.LargeChange;
				LevelVScroller.Maximum = 1000;
			}
		}


		/// <summary>
		/// Sets the location in the layer
		/// </summary>
		public void ScrollLayer(Point pos)
		{
			Level.Location = new Point(pos.X * Level.BlockSize.Width, pos.Y * Level.BlockSize.Height);

			// Update the display to be smoothy
			GlControl.Invalidate();
		}




		#endregion


		#region Events

		/// <summary>
		/// First time the form is shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelForm_Shown(object sender, EventArgs e)
		{
			TilePanel.Show(dockPanel);
			LayerPanel.Show(dockPanel);
			PropertyPanel.Show(dockPanel);
			BrushPanel.Show(dockPanel);
		}



		/// <summary>
		/// Selects the clear color
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearColor_OnClick(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = Display.ClearColor;
			dlg.AllowFullOpen = true;
			dlg.AnyColor = true;
			dlg.FullOpen = true;
			dlg.ShowDialog();

			Display.ClearColor = Color.FromArgb(dlg.Color.R, dlg.Color.G, dlg.Color.B);
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
		/// GlLevelControl resize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlLevelControl_Resize(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControl.Size);
			if (Level != null)
				Level.ViewPort = new Rectangle(new Point(), GlControl.Size);
			//	ResourceManager.DisplayZone = new Rectangle(new Point(), GlControl.Size);
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

			new Wizards.NewEntityWizard(CurrentLayer, pos).ShowDialog();
		}


		/// <summary>
		/// List all available entities in the layer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RebuildEntityList(object sender, EventArgs e)
		{
			ListEntitiesButton.Items.Clear();

			if (CurrentLayer == null)
				return;

			foreach (string name in CurrentLayer.GetEntities())
				ListEntitiesButton.Items.Add(name);
		}


		/// <summary>
		/// Rebuild Spanwlist dropdown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RebuildSpawnPointsList(object sender, EventArgs e)
		{
			ListSpawnPointsBox.Items.Clear();

			if (CurrentLayer == null)
				return;

			foreach (string name in CurrentLayer.GetSpawnPoints())
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

			SpawnPoint spawn = CurrentLayer.GetSpawnPoint((string)ListSpawnPointsBox.SelectedItem);

			LayerPanel.PropertyGridBox.SelectedObject = spawn;


		}


		/// <summary>
		/// New entity selected by the selection dropdown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListEntitiesButton_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (LayerPanel.PropertyGridBox.SelectedObject != null)
			{
				((Entity)LayerPanel.PropertyGridBox.SelectedObject).Debug = false;
			}

			// No index selected
			if (ListEntitiesButton.SelectedIndex == -1)
				return;

			Entity entity = CurrentLayer.GetEntity((string)ListEntitiesButton.SelectedItem);

			LayerPanel.PropertyGridBox.SelectedObject = entity;

			entity.Debug = true;

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

			SpawnPoint spawn = CurrentLayer.GetSpawnPoint((string)ListSpawnPointsBox.SelectedItem);

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
			if (ListEntitiesButton.SelectedIndex == -1)
				return;

			Entity entity = CurrentLayer.GetEntity((string)ListEntitiesButton.SelectedItem);

			int x = (entity.Location.X - GlControl.Width / 2) / Level.BlockSize.Width;
			int y = (entity.Location.Y - GlControl.Height / 2) / Level.BlockSize.Height;
			ScrollLayer(new Point(x, y));
		}



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
		private void GlLevelControl_Paint(object sender, PaintEventArgs e)
		{
			// Stop the drawtimer
			DrawTimer.Stop();

			GlControl.MakeCurrent();
			Display.ClearBuffers();

			// Background texture
			//Video.Texture = CheckerBoard;
			Rectangle rect = new Rectangle(Point.Empty, GlControl.Size);
			CheckerBoard.Blit(rect, rect);


			// Draw the level
			Level.Draw();


			// No layer selected
			if (CurrentLayer != null)
			{
/*
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

						Point pos = level.LevelToScreen(spawn.Location);
						pos.X = pos.X - 8;
						pos.Y = pos.Y - 8;
						Video.Blit(SpawnPointTexture, pos);
					}
					spawn = layerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
					if (spawn != null)
					{

						rect = SpawnPointTexture.Rectangle;
						rect.Offset(spawn.CollisionBoxLocation.Location);
						rect.Location = level.LevelToScreen(rect.Location);
						Video.Rectangle(rect, false);
					}

				}
*/ 
			}

			// Draw a rectangle showing the brush selection in the layer
			if (sizingBrush)
			{

				Display.Color = Color.Green;
				Rectangle rec = new Rectangle(
					BrushRectangle.Left * Level.BlockDimension.Width,
					BrushRectangle.Top * Level.BlockDimension.Height,
					BrushRectangle.Width * Level.BlockDimension.Width,
					BrushRectangle.Height * Level.BlockDimension.Height);
				rec.Location = Level.LevelToScreen(rec.Location);
				Display.Rectangle(rec, false);
				Display.Color = Color.White;

			}


			// If in paint mode, show the brush
			if (TileMode == TileMode.Pen && !TilePanel.SizingBrush)
			{

				TileSet tileset = CurrentLayer.TileSet;
				//tileset.Bind();

				// Find the good location
				Point pos = GlControl.PointToClient(Control.MousePosition);
				pos.X -= (Level.BlockDimension.Width - Level.Location.X % Level.BlockDimension.Width + pos.X) % Level.BlockDimension.Width;
				pos.Y -= (Level.BlockDimension.Height - Level.Location.Y % Level.BlockDimension.Height + pos.Y) % Level.BlockDimension.Height;

				// Draw the tile on the screen only if the cursor is in the form
				if (pos.X > -Level.BlockDimension.Width)
					LayerBrush.Draw(pos, CurrentLayer.TileSet, Level.BlockDimension);
			}

			GlControl.SwapBuffers();

			// Restart the draw timer
			DrawTimer.Start();
		}



		/// <summary>
		/// KeyDown on the level form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlLevelControl_KeyDown(object sender, KeyEventArgs e)
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
					if (LayerPanel.PropertyGridBox.SelectedObject == null)
						break;

					Entity ent = LayerPanel.PropertyGridBox.SelectedObject as Entity;
					if (ent != null)
					{
						//	CurrentLayer.RemoveEntity(ent.Name);
						LayerPanel.PropertyGridBox.SelectedObject = null;
						RebuildEntityList(null, null);

						return;
					}

					SpawnPoint spawn = LayerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
					if (spawn != null)
					{
						//	CurrentLayer.RemoveSpawnPoint(spawn.Name);
						LayerPanel.PropertyGridBox.SelectedObject = null;

						RebuildSpawnPointsList(null, null);
						return;
					}



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
		private void GlLevelControl_OnMouseDown(object sender, MouseEventArgs e)
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
				pos.Offset(Level.Location);

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
				Entity entity = LayerPanel.PropertyGridBox.SelectedObject as Entity;
				if (entity != null) entity.Debug = false;

				// Find the entity under the mouse
				Point pos = e.Location;
				pos.X += Level.Location.X;
				pos.Y += Level.Location.Y;
				//pos.Offset(CurrentLayer.Offset);

				entity = CurrentLayer.FindEntity(pos);
				LayerPanel.PropertyGridBox.SelectedObject = entity;
				if (entity != null)
				{
					entity.Debug = true;
					DragObjectByMouse = true;


					// Select it in the dropdown
					//	if (ListEntitiesButton.Items.Contains(entity.Name))
					//		ListEntitiesButton.SelectedIndex = ListEntitiesButton.Items.IndexOf(entity.Name);

					//return;
				}


				SpawnPoint spawn = CurrentLayer.FindSpawnPoint(pos);
				if (spawn != null)
				{
					LayerPanel.PropertyGridBox.SelectedObject = spawn;
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
		private void GlLevelControl_OnMouseMove(object sender, MouseEventArgs e)
		{
			Entity entity;

			// No layer ??
			if (Size.IsEmpty)
				return;


			// Display the tile coord
			MousePos = e.Location;
			Point pos = Level.ScreenToLevel(MousePos);

			if (CurrentLayer != null)
			{
				MouseCoordLabel.Text = "Mouse coord : " + pos.X + "x" + pos.Y;
				TileIDLabel.Text = "Tile ID : " + CurrentLayer.GetTileAtCoord(pos).ToString();
				pos = Level.PositionToBlock(pos);
				TileCoordLabel.Text = "Tile coord : " + pos.X + "x" + pos.Y;
			}

			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Middle)
			{
				// Find the new coord for the level
				pos = Level.Location;
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

				// Store last mouse location
				LastMousePos = e.Location;

				return;
			}


			// Left mouse button and PasteTileButton checked ? => Paste tile in the layer
			if (e.Button == MouseButtons.Left && TileMode == TileMode.Pen)
			{
				PasteBrush(e.Location);
				//GlLevelControl.Draw();

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
				entity = LayerPanel.PropertyGridBox.SelectedObject as Entity;
				if (entity != null)
				{
					pos = entity.Location;
					pos.X += offset.X;
					pos.Y += offset.Y;
					//pos.Offset(offset);
					entity.Location = pos;
				}
				SpawnPoint spawn = LayerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
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
				entity = CurrentLayer.FindEntity(pos);
				if (entity != null)
				{
					Cursor = Cursors.Hand;
					return;
				}

				SpawnPoint spawn = CurrentLayer.FindSpawnPoint(pos);
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
		private void GlLevelControl_OnMouseUp(object sender, MouseEventArgs e)
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
						LayerBrush.Tiles[y][x] = CurrentLayer.GetTileAt(new Point(BrushRectangle.Left + x, BrushRectangle.Top + y));
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
			Wizards.LevelResizeWizard wizard = new Wizards.LevelResizeWizard(Level);
			wizard.ShowDialog();
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



		#endregion



		#region Properties

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
			get
			{
				return LayerPanel.CurrentLayer;
			}
			set
			{
				LayerPanel.CurrentLayer = value;
			}
		}


		/// <summary>
		/// Level currently edited
		/// </summary>
		public Level Level
		{
			get;
			protected set;
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
		Texture CheckerBoard;



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


		/// <summary>
		/// LayerPanel
		/// </summary>
		public LevelLayerPanel LayerPanel
		{
			get;
			private set;
		}



		/// <summary>
		/// Level property panel
		/// </summary>
		public LevelPropertyPanel PropertyPanel
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
	/// Current tile mdoe
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
