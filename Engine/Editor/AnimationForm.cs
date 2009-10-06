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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Editor
{
	/// <summary>
	/// Animation editor
	/// </summary>
	internal partial class AnimationForm : AssetEditor
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AnimationForm(XmlNode node)
		{
			InitializeComponent();


			Animation = new Animation();
			Animation.Load(node);
			PropertyBox.SelectedObject = Animation;


			//Device = new Display();

			GlTilesControl.MakeCurrent();
			//Display.ShareVideoContext();
			Display.ClearColor = Color.Black;
			Display.Texturing = true;
			Display.Blending = true;
			Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


			GlFramesControl.MakeCurrent();
			//Display.ShareVideoContext();
			Display.ClearColor = Color.Black;
			Display.Texturing = true;
			Display.Blending = true;
			//Display.ShareVideoContext();
			Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);



			GlPreviewControl.MakeCurrent();
			//Display.ShareVideoContext();
			Display.ClearColor = Color.Black;
			Display.Texturing = true;
			Display.Blending = true;
			Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);



			Animation.Init();


			// Draw timer
			DrawTimer.Start();

			if (Animation.TileSet != null)
			{

				TilesHScroller.Maximum = Animation.TileSet.Tiles.Count;
			}
			else
			{

			}

			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


			Time = Environment.TickCount;

		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				Animation.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<Animation>(Animation.Name, doc.DocumentElement);
		}




		#region Events

		/// <summary>
		/// Draw Timer tick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawTimer_Tick(object sender, EventArgs e)
		{

			// If not floating panel and the panel is active and AutoRefresh is OK
			if (this.DockAreas != DockAreas.Float && DockPanel.ActiveDocument == this)
			{
		
				int elapsed = Environment.TickCount - Time;

				Time = Environment.TickCount;


				// Stop the drawtimer
				DrawTimer.Stop();

				Animation.Update(elapsed);


				GlFramesControl.Invalidate();
				GlTilesControl.Invalidate();
				GlPreviewControl.Invalidate();

				// Restart the draw timer
				DrawTimer.Start();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AnimationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Animation Editor", "Save modifications ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}





		#endregion



		#region GlPreviewControl

		/// <summary>
		/// OnMouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_MouseDown(object sender, MouseEventArgs e)
		{
			// Middle mouse button ? => the scroll enabled
			if (e.Button == MouseButtons.Left)
			{
				LastMousePos = e.Location;
				Cursor = Cursors.SizeAll;

				return;
			}

		}


		/// <summary>
		/// OnMouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_MouseMove(object sender, MouseEventArgs e)
		{
			// If scrolling with the middle mouse button
			if (e.Button == MouseButtons.Left)
			{
				// Smooth the value
				AnimOffset.X -= LastMousePos.X - e.X;
				AnimOffset.Y -= LastMousePos.Y - e.Y;


				// Store last mouse location
				LastMousePos = e.Location;

				return;
			}

		}


		/// <summary>
		/// OnMouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_MouseUp(object sender, MouseEventArgs e)
		{
			// Stop scrolling with the middle mouse button
			if (e.Button == MouseButtons.Left)
			{
				Cursor = Cursors.Default;
				return;
			}

		}


		/// <summary>
		/// OnResize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_Resize(object sender, EventArgs e)
		{
			GlPreviewControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlPreviewControl.Size);
		}

		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlPreviewControl_Paint(object sender, PaintEventArgs e)
		{
			//
			GlPreviewControl.MakeCurrent();
			Display.ClearBuffers();

			if (Animation.TileSet == null)
				return;


			Tile tile = Animation.CurrentTile;
			if (tile == null)
			{
				GlPreviewControl.SwapBuffers();
				return;
			}
			Rectangle rect = new Rectangle(AnimOffset, tile.Size);

			Animation.TileSet.Draw(Animation.TileID, AnimOffset);

			GlPreviewControl.SwapBuffers();
		}



		/// <summary>
		/// Play anim
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AnimPlay_Click(object sender, EventArgs e)
		{
			AnimPause.Checked = false;
			Animation.Play();
		}


		/// <summary>
		/// Stop animation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AnimStop_Click(object sender, EventArgs e)
		{
			AnimPlay.Checked = false;
			AnimPause.Checked = false;

			Animation.Stop();
		}


		/// <summary>
		/// Pause animation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AnimPause_Click(object sender, EventArgs e)
		{
			AnimPlay.Checked = false;
			Animation.Pause();
		}



		#endregion



		#region GlFramesControl


		/// <summary>
		/// Adds a frame
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FramesAdd_Click(object sender, EventArgs e)
		{
			if (TileID != -1)
			{
				if (FrameID != -1)
				{
					// Inserts the tile
					Animation.Tiles.Insert(FrameID, TileID);
					FrameID++;
				}
				else
				{
					// Adds the tile
					Animation.Tiles.Add(TileID);
				}
			}

			
		}


		/// <summary>
		/// Deletes current frame
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FramesDelete_Click(object sender, EventArgs e)
		{
			if (FrameID == -1)
				return;

			Animation.Tiles.RemoveAt(FrameID);
			FrameID = Math.Min(Animation.Tiles.Count - 1, FrameID);

		}

		/// <summary>
		/// Move frame back
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveLeft_Click(object sender, EventArgs e)
		{
			if (FrameID <= 0 && Animation.Tiles.Count > 1)
				return;

			int tmp = Animation.Tiles[FrameID - 1];
			Animation.Tiles[FrameID - 1] = Animation.Tiles[FrameID];
			Animation.Tiles[FrameID] = tmp;

			FrameID--;
		}


		/// <summary>
		/// Move frame forward
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MoveRight_Click(object sender, EventArgs e)
		{
			if (FrameID >= Animation.Tiles.Count - 1)
				return;
			
			int tmp = Animation.Tiles[FrameID + 1];
			Animation.Tiles[FrameID + 1] = Animation.Tiles[FrameID];
			Animation.Tiles[FrameID] = tmp;

			FrameID++;
		}




		/// <summary>
		/// OnMouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlFramesControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (Animation.TileSet == null)
				return;

			// Find the Frame under the mouse and remove it
			Rectangle rect = Rectangle.Empty;
			for(int id = 0; id < Animation.Tiles.Count; id++)
			{
				Tile tile = Animation.TileSet.GetTile(Animation.Tiles[id]);
				rect.Size = tile.Size;

				if (rect.Contains(e.Location))
				{
					Animation.Tiles.RemoveAt(id);
					break;
				}

				rect.X += tile.Size.Width;
			}
		}



		/// <summary>
		/// OnMouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlFramesControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (Animation.TileSet == null)
				return;

			// Find the FrameID to select
			FrameID = -1;
			Rectangle rect = Rectangle.Empty;
			for (int id = 0; id < Animation.Tiles.Count; id++)
			{
				Tile tile = Animation.TileSet.GetTile(Animation.Tiles[id]);
				rect.Size = tile.Size;

				if (rect.Contains(e.Location))
				{
					FrameID = id;
					break;
				}

				rect.X += tile.Size.Width;
			}

		}


		/// <summary>
		/// OnResize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlFramesControl_Resize(object sender, EventArgs e)
		{
			GlFramesControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlFramesControl.Size);
		}


		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlFramesControl_Paint(object sender, PaintEventArgs e)
		{
			// Cleanup
			GlFramesControl.MakeCurrent();
			Display.ClearBuffers();

			// Oops !
			if (Animation.TileSet == null)
				return;


			// Mouse location
			Point mouse = GlFramesControl.PointToClient(Control.MousePosition);

			// Bind the TileSet
		//	Animation.TileSet.Bind();

	
			// Display each frames
			Rectangle rect = Rectangle.Empty;
			for (int id = 0; id < Animation.Tiles.Count; id++)
				{
				Tile tile = Animation.TileSet.GetTile(Animation.Tiles[id]);
				if (tile == null)
					continue;

				rect.Size = tile.Size;

				Animation.TileSet.Texture.Blit(rect, tile.Rectangle);
				//Display.BlitTexture(Animation.TileSet.Texture, rect, tile.Rectangle);

				if (rect.Contains(mouse) || id == FrameID)
				{
					Display.Color = Color.White;
					Display.Rectangle(rect, false);
				}

				rect.X += tile.Size.Width;

				if (rect.X > Display.ViewPort.Width)
					break;
			}

			GlFramesControl.SwapBuffers();
		}



		#endregion



		#region GlTilesControl

		/// <summary>
		/// OnMouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlTilesControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (TileID != -1)
			{
				if (FrameID != -1)
				{
					// Inserts the tile
					Animation.Tiles.Insert(FrameID, TileID);
					FrameID++;
				}
				else
				{
					// Adds the tile
					Animation.Tiles.Add(TileID);
				}
			}
		}


		/// <summary>
		/// OnMouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlTilesControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (Animation.TileSet == null)
				return;

			// Find the FrameID to select
			int maxheight = 0;
			TileID = -1;
			Rectangle rect = Rectangle.Empty;
			foreach (int id in Animation.TileSet.Tiles)
			{
				// Display rectangle
				Tile tile = Animation.TileSet.GetTile(id);
				rect.Size = tile.Size;

				// Mouse over rectangle ?
				if (rect.Contains(e.Location))
				{
					TileID = id;
					break;
				}

				// Move right
				rect.X += tile.Size.Width;


				// End of line ?
				if (rect.X + rect.Width > GlTilesControl.Width)
				{
					rect.X = 0;
					rect.Y += maxheight + 10;
				}


				// Get the maximum height
				maxheight = Math.Max(maxheight, rect.Height);
			}

		}


		/// <summary>
		/// OnResize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlTilesControl_Resize(object sender, EventArgs e)
		{
			GlTilesControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlTilesControl.Size);
		}

		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlTilesControl_Paint(object sender, PaintEventArgs e)
		{
			// Draws all tiles
			GlTilesControl.MakeCurrent();
			Display.ClearBuffers();

			if (Animation.TileSet == null)
			{
				GlTilesControl.SwapBuffers();
				return;
			}

			// Bind the TileSet
			//Animation.TileSet.Texture.Bind();

			// Find the cursor location
			Point mouse = GlTilesControl.PointToClient(Control.MousePosition);


			int maxheight = 0;
			Rectangle rect = Rectangle.Empty;
			foreach (int id in Animation.TileSet.Tiles)
			{
				// Get the display rectangle
				Tile tile = Animation.TileSet.GetTile(id);
				rect.Size = tile.Size;

				// Blit the tile
			//	Video.Blit(rect, tile.Rectangle);
				Animation.TileSet.Draw(id, new Point(rect.X + tile.HotSpot.X, tile.HotSpot.Y));

				// Is mouse over or selected tile
				if (rect.Contains(mouse) || TileID == id)
				{
					Display.Color = Color.White;
					Display.Rectangle(rect, false);
				}

				// Move right
				rect.X += tile.Size.Width;


				// End of line ?
				if (rect.X + rect.Width > GlTilesControl.Width)
				{
					rect.X = 0;
					rect.Y += maxheight + 10;
				}


				// Get the maximum height
				maxheight = Math.Max(maxheight, rect.Height);
			}


			GlTilesControl.SwapBuffers();
		}



		#endregion

		

		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Animation;
			}
		}

		/// <summary>
		/// Offset in preview control
		/// </summary>
		Point AnimOffset;


		/// <summary>
		/// Last mouse position in the Anim Preview Control
		/// </summary>
		Point LastMousePos;


		/// <summary>
		/// Current animation to edit
		/// </summary>
		Animation Animation;


		/// <summary>
		/// TileID currently under the mouse
		/// </summary>
		int TileID = -1;


		/// <summary>
		/// Frame ID currently selected
		/// </summary>
		int FrameID = -1;


		/// <summary>
		/// 
		/// </summary>
		int Time;


		/// <summary>
		/// Checkerboard texture
		/// </summary>
		Texture CheckerBoard;

		#endregion

	}

}
