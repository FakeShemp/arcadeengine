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
using ArcEngine.Graphic;
using ArcEngine;
using RuffnTumble;
using WeifenLuo.WinFormsUI.Docking;
using OpenTK;

namespace RuffnTumble.Editor
{
	/// <summary>
	/// 
	/// </summary>
	public partial class LevelBrushPanel : DockContent
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public LevelBrushPanel()
		{
			InitializeComponent();

			Brushes = new Dictionary<Rectangle, LayerBrush>();
		}



		/// <summary>
		/// Initialize the Panel
		/// </summary>
		/// <param name="form"></param>
		/// <returns></returns>
		public bool Init(WorldForm form)
		{
			Form = form;


			GlControl.MakeCurrent();
			Display.Init();
			GlControl_Resize(null, null);





			// Preload texture resources
			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


			return true;
		}




		#region GlControl events

		/// <summary>
		/// OnResize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControl.Size);
		}



		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ClearBuffers();

			// Mouse coord
			Point mouse = GlControl.PointToClient(Control.MousePosition); 

			// Clear the rectangle brush buffer
			Brushes.Clear();

			// Background texture
			Rectangle rect = new Rectangle(Point.Empty, GlControl.Size);
			CheckerBoard.Blit(rect, rect);

			if (Form.World.CurrentLevel != null)
			{
				Point pos = Point.Empty;
				foreach (string name in Form.World.CurrentLevel.GetBrushes())
				{
					LayerBrush brush = Form.World.CurrentLevel.GetBrush(name);

					// Draw the brush
				//	brush.Draw(pos, Form.CurrentLayer.TileSet, Form.World.CurrentLevel.BlockDimension);

					// zone detection
					rect = new Rectangle(pos, new Size(brush.Size.Width * Form.World.CurrentLevel.BlockDimension.Width, brush.Size.Height * Form.World.CurrentLevel.BlockDimension.Height));
					Brushes[rect] = brush;

					if (brush == SelectedBrush)
					{
						//Display.Color = Color.Red;
						Display.DrawRectangle(rect, Color.Red);
						//Display.Color = Color.White;
					}


					// Somme blah blah
					if (rect.Contains(mouse))
					{
						Display.DrawRectangle(rect, Color.White);
					}

					// Move to the next location
					pos.Y += brush.Size.Height * Form.World.CurrentLevel.BlockDimension.Height + 32;
				}
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
			// Left click, so select the brush under
			if (e.Button == MouseButtons.Left)
			{

				SelectedBrush = null;

				// Find the brush under the mouse
				foreach (Rectangle rect in Brushes.Keys)
				{
					if (rect.Contains(e.Location))
					{
						SelectedBrush = Brushes[rect];
						Form.LayerBrush = SelectedBrush;
						Form.TileMode = TileMode.Pen;
					}
				}
			}

		}


		/// <summary>
		/// Draws the form
		/// </summary>
		public void Draw()
		{
			GlControl.Invalidate();
		}


		#endregion



		#region Events


		/// <summary>
		/// Adds a new brush
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddBrushButton_Click(object sender, EventArgs e)
		{
			if (Form.TileMode == TileMode.NoAction)
				return;


			// Disable auto refresh on the form
			Form.AutoRefresh = false;

			new Wizards.NewLayerBrushWizard(Form.LayerBrush, Form.World.CurrentLevel).ShowDialog();

			Form.AutoRefresh = true;

			GlControl.Invalidate();
		}


		/// <summary>
		/// Removes selected brush
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteBrushButton_Click(object sender, EventArgs e)
		{
			// Oops !
			if (SelectedBrush == null)
				return;

			// Remove brush from layer
		//	Form.CurrentLayer.DestroyBrush(SelectedBrush.Name);
			SelectedBrush = null;

			// Erase the brush in the level form
			Form.LayerBrush.Size = Size.Empty;
			Form.TileMode = TileMode.NoAction;

		}

		#endregion



		#region Properties


		/// <summary>
		/// Parent level form
		/// </summary>
		WorldForm Form;



		/// <summary>
		/// Background texture
		/// </summary>
		Texture CheckerBoard;


		/// <summary>
		/// Remember each zone for each Layerbrush
		/// </summary>
		Dictionary<Rectangle, LayerBrush> Brushes;


		/// <summary>
		/// Selected brush
		/// </summary>
		LayerBrush SelectedBrush;

		#endregion




	}
}
