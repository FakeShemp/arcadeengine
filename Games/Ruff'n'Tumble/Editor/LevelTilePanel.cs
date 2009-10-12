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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using RuffnTumble;
using WeifenLuo.WinFormsUI.Docking;


namespace RuffnTumble.Editor
{
	/// <summary>
	/// 
	/// </summary>
	public partial class LevelTilePanel : DockContent
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public LevelTilePanel()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Initializae the panel
		/// </summary>
		/// <param name="form">Parent LevelForm</param>
		/// <returns>Success or not</returns>
		public bool Init(LevelForm form)
		{
			if (form == null)
				return false;
			Form = form;


			GlControl.MakeCurrent();
			Display.Init();
			GlControl_Resize(null, null);


			// Preload texture resources
			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));

			
			return true;
		}


		/// <summary>
		/// Draws the form
		/// </summary>
		public void Draw()
		{
			GlControl.Invalidate();
		}



		#region GlControl events

		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// TODO Paint tile by tile instead of pasting the texture
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ClearBuffers();
			Display.Color = Color.White;

			// Background texture
			CheckerBoard.Blit(new Rectangle(Point.Empty, GlControl.Size), CheckerBoard.Rectangle, TextureLayout.Tile);

			// No texture, byebye !
			//if (Form.LayerPanel.CurrentLayer == null || Form.LayerPanel.CurrentLayer.Texture == null)
			//   return;

			// Blit the texture
		//	Form.LayerPanel.CurrentLayer.Texture.Blit(new Point(0, 0));

	
			// Draw the selected brush
			if (Form.TileMode == TileMode.Pen)
			{
				Display.Color = Color.Red;
				Rectangle rec = new Rectangle(
					SelectedTiles.Left * Form.Level.BlockSize.Width,
					SelectedTiles.Top * Form.Level.BlockSize.Height,
					SelectedTiles.Width * Form.Level.BlockSize.Width,
					SelectedTiles.Height * Form.Level.BlockSize.Height);
				Display.Rectangle(rec, false);
				Display.Color = Color.White;
			}


			// Draw grid ?
			if (ShowGridButton.Checked)
			{
				Point from = Point.Empty;
				Point to = Point.Empty;
				Display.Color = Color.Red;
/*
				for (int y = 0; y < Form.CurrentLayer.Texture.Size.Height; y += Form.Level.BlockSize.Height)
				{
					from.X = 0; from.Y = y;
					to.X = Form.CurrentLayer.Texture.Size.Width; to.Y = y;
					Display.Line(from, to);
				}
				for (int x = 0; x < Form.CurrentLayer.Texture.Size.Width; x += Form.Level.BlockSize.Width)
				{
					from.X = x; from.Y = 0;
					to.X = x; to.Y = Form.CurrentLayer.Texture.Size.Height;
					Display.Line(from, to);
				}
*/
			}


			GlControl.SwapBuffers();
		}




		/// <summary>
		/// OnMouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (Form.CurrentLayer == null)
				return;


			// No texture selected...
			//if (Form.CurrentLayer.Texture == null)
			//   return;

			// Left button => select tiles
			if (e.Button == MouseButtons.Left)
			{
/*	
				Point pos = e.Location;
				Size size = new Size(Form.CurrentLayer.Texture.Size.Width / Form.Level.BlockSize.Width,
					Form.CurrentLayer.Texture.Size.Height / Form.Level.BlockSize.Height);
					//Form.CurrentLayer.Texture.SizeInBlock(Form.Level.BlockSize);

				SelectedTiles = new Rectangle(
					pos.X / Form.Level.BlockSize.Width,
					pos.Y / Form.Level.BlockSize.Height,
					1,
					1);

				// Size not too width
				if (SelectedTiles.Left + SelectedTiles.Width >= size.Width)
					SelectedTiles.Width = size.Width - SelectedTiles.Left;

				// Size not too low
				if (SelectedTiles.Top + SelectedTiles.Height >= size.Height)
					SelectedTiles.Height = size.Height - SelectedTiles.Top;


				sizingBrush = true;
*/
			}
		}



		/// <summary>
		/// OnMouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_MouseMove(object sender, MouseEventArgs e)
		{

			if (SizingBrush)
			{
				Point pos = e.Location;

				Size size = new Size(pos.X / Form.Level.BlockSize.Width - SelectedTiles.Left + 1,
										pos.Y / Form.Level.BlockSize.Height - SelectedTiles.Top + 1);

/*
				Size tilesize = new Size(Form.CurrentLayer.Texture.Size.Width / Form.Level.BlockSize.Width,
					Form.CurrentLayer.Texture.Size.Height / Form.Level.BlockSize.Height); 
				//Form.CurrentLayer.Texture.SizeInBlock(Form.Level.BlockSize);


				// Too wide
				if (SelectedTiles.Left + size.Width >= tilesize.Width)
					size.Width = tilesize.Width - SelectedTiles.Left;

				// too high
				if (SelectedTiles.Top + size.Height >= tilesize.Height)
					size.Height = tilesize.Height - SelectedTiles.Top;

*/
				SelectedTiles.Size = size;
			}

		}

	
		
		/// <summary>
		/// OnMouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_MouseUp(object sender, MouseEventArgs e)
		{
			// Build the LayerBrush with the right tiles
			if (SizingBrush && e.Button == MouseButtons.Left)
			{
				// Sert the size of the LayerBrush
				Form.LayerBrush.Size = SelectedTiles.Size;
/*
				// Size of each block in pixel
				Size blocksize = new Size(Form.CurrentLayer.Texture.Size.Width / Form.Level.BlockSize.Width,
					Form.CurrentLayer.Texture.Size.Height / Form.Level.BlockSize.Height); 
				//Form.CurrentLayer.Texture.SizeInBlock(Form.Level.BlockSize);

				for (int y = 0; y < SelectedTiles.Height; y++)
				{
					for (int x = 0; x < SelectedTiles.Width; x++)
					{
						int id = (SelectedTiles.Top + y) * blocksize.Width + SelectedTiles.Left + x;
						Form.LayerBrush.Tiles[y][x] = id;
					}
				}

				sizingBrush = false;
*/
				TileMode = TileMode.Pen;
			}
		}


		/// <summary>
		/// GlTilesControl resize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControl.Size);
		}


		#endregion



		#region Events



		/// <summary>
		/// Update the status of the TileMode buttons
		/// </summary>
		void UpdateTileModeButtons()
		{
			switch (Form.TileMode)
			{
				case TileMode.Brush:
				{
					PenButton.Checked = false;
					RectangleButton.Checked = false;
					FloodFillTileButton.Checked = false;
				}
				break;


				case TileMode.Fill:
				{
					PenButton.Checked = false;
					RectangleButton.Checked = false;
					BrushButton.Checked = false;
				}
				break;

				case TileMode.NoAction:
				{
					PenButton.Checked = false;
					RectangleButton.Checked = false;
					FloodFillTileButton.Checked = false;
					BrushButton.Checked = false;
				}
				break;


				case TileMode.Pen:
				{
					RectangleButton.Checked = false;
					FloodFillTileButton.Checked = false;
					BrushButton.Checked = false;
					PenButton.Checked = true;
				}
				break;


				case TileMode.Rectangle:
				{
					PenButton.Checked = false;
					FloodFillTileButton.Checked = false;
					BrushButton.Checked = false;
				}
				break;

			}

		}


		/// <summary>
		/// Paste tile OnClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PasteTileButton_OnClick(object sender, EventArgs e)
		{
			if (PenButton.Checked)
			{
				tileMode = TileMode.Pen;
				UpdateTileModeButtons();
				Cursor = Cursors.Cross;
			}
			else
			{
				tileMode = TileMode.NoAction;
				Cursor = Cursors.Default;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FillTileButton_OnClick(object sender, EventArgs e)
		{
			if (FloodFillTileButton.Checked)
			{
				tileMode = TileMode.Fill;
				UpdateTileModeButtons();
				Cursor = Cursors.Cross;
			}
			else
			{
				tileMode = TileMode.NoAction;
				Cursor = Cursors.Default;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RectangleButton_OnClick(object sender, EventArgs e)
		{
			if (RectangleButton.Checked)
			{
				tileMode = TileMode.Rectangle;
				UpdateTileModeButtons();
				Cursor = Cursors.Cross;
			}
			else
			{
				tileMode = TileMode.NoAction;
				Cursor = Cursors.Default;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BrushButton_OnClick(object sender, EventArgs e)
		{
			if (BrushButton.Checked)
			{
				tileMode = TileMode.Brush;
				UpdateTileModeButtons();
				Cursor = Cursors.Cross;
			}
			else
			{
				Cursor = Cursors.Default;
				tileMode = TileMode.NoAction;
			}
		}

	

		/// <summary>
		/// Display grid or not
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowGridButton_CheckedChanged(object sender, EventArgs e)
		{

	//		foreach (Layer layer in Form.Level.Layers)
	//			layer.ShowGrid = ShowGridButton.Checked;
			
		}



		/// <summary>
		/// OnKeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				// Cancel all mode
				case Keys.Escape:
				{
					TileMode = TileMode.NoAction;
					Cursor = Cursors.Default;
				}
				break;

				// Fill mode
				case Keys.F:
				{
					if (TileMode == TileMode.Fill)
						TileMode = TileMode.NoAction;
					else
						TileMode = TileMode.Fill;

				}
				break;



				// Paint mode
				case Keys.P:
				{
					if (TileMode == TileMode.Pen)
						TileMode = TileMode.NoAction;
					else
						TileMode = TileMode.Pen;

				}
				break;


				// Brush mode
				case Keys.B:
				{
					if (TileMode == TileMode.Brush)
						TileMode = TileMode.NoAction;
					else
						TileMode = TileMode.Brush;
				}
				break;


			}


		}



		#endregion



		#region Properties

		/// <summary>
		/// Parent LevelForm
		/// </summary>
		LevelForm Form;



		/// <summary>
		/// Checkerboard texture
		/// </summary>
		Texture CheckerBoard;



		/// <summary>
		/// Rectangle of the selected tiles
		/// </summary>
		Rectangle SelectedTiles;



		/// <summary>
		/// Are we in sizing a brush ?
		/// </summary>
		public bool SizingBrush
		{
			get
			{
				return sizingBrush;
			}
		}
		bool sizingBrush;


		/// <summary>
		/// Need to draw the grid
		/// </summary>
		public bool ShowGrid
		{
			get
			{
				return ShowGridButton.Checked;
			}
		}


		/// <summary>
		/// Current tiling mode
		/// </summary>
		public TileMode TileMode
		{
			get
			{
				return tileMode;
			}
			set
			{
				tileMode = value;
				UpdateTileModeButtons();
			}
		}
		TileMode tileMode;

		#endregion


	}

}
