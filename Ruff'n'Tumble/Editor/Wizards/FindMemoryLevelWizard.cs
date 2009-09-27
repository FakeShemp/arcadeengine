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
using System.Text;
using System.Windows.Forms;
using ArcEngine.Graphic;

using System.IO;
using ArcEngine.Asset;


namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	/// <summary>
	/// 
	/// </summary>
	public partial class FindMemoryLevelWizard : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public FindMemoryLevelWizard()
		{
			InitializeComponent();


			Tiles = new TileSet();
			
			
			LevelGlControl.MakeCurrent();
			Display.ClearColor = Color.Black;
			Display.Texturing = true;
			Display.Blending = true;

		}




		#region  LevelGlControl


		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelGlControl_Paint(object sender, PaintEventArgs e)
		{
			Display.ClearBuffers();

			if (Data == null)
				return;

			// Draw the level
			for (short y = 0; y < MapSize.Height; y++)
			{
				for (short x = 0; x < MapSize.Width; x++)
				{
					// Off limite
					if (x * BlockSize.Width > Display.ViewPort.Width)
						break;

					ushort pos = (ushort)((y * MapSize.Height) + DataPos + x);

					if (pos < Data.Length)
					{
						int id = Data[pos];
						Tiles.Draw(id, new Point(x * BlockSize.Width, y * BlockSize.Height));
					}

				}
				if (y * BlockSize.Height > LevelGlControl.Height)
					break;
			}
		}


		/// <summary>
		/// OnResize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LevelGlControl_Resize(object sender, EventArgs e)
		{
			LevelGlControl.MakeCurrent();

			Display.ViewPort = new Rectangle(new Point(), LevelGlControl.Size);
		}

		#endregion



		#region Events


		/// <summary>
		/// Loads the tile texture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextureButton_Click(object sender, EventArgs e)
		{
			if (OpenTileDlg.ShowDialog() != DialogResult.OK)
				return;
			
			Tiles.Clear();

			Tiles.Texture = new Texture();
			Tiles.Texture.LoadImage(OpenTileDlg.FileName);
			int id = 0;
			for (int y = 0; y < Tiles.Texture.Size.Height; y += BlockSize.Height)
			{
				for (int x = 0; x < Tiles.Texture.Size.Width; x += BlockSize.Width)
				{
					Tile tile = Tiles.AddTile(id++);
					tile.Rectangle = new Rectangle(new Point(x, y), BlockSize);
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MemoryButton_Click(object sender, EventArgs e)
		{
			if (OpenDatatDlg.ShowDialog() != DialogResult.OK)
				return;

			FileInfo file = new FileInfo(OpenDatatDlg.FileName);
			FileStream stream = file.OpenRead();
			Data = new byte[stream.Length];
			stream.Read(Data, 0, (int)stream.Length);
			stream.Close();



			TrackPosition.Maximum = Data.Length - 1;
		}


		/// <summary>
		/// OnScroll
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TrackPosition_Scroll(object sender, EventArgs e)
		{
			DataPos = TrackPosition.Value;
			MemoryLocationBox.Text = TrackPosition.Value.ToString();
		}



		/// <summary>
		/// TextChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MemoryLocationBox_TextChanged(object sender, EventArgs e)
		{
			int pos = Int32.Parse(MemoryLocationBox.Text);
			DataPos = pos;
			TrackPosition.Value = pos;
		}




		#endregion



		#region Properties

		/// <summary>
		/// Level size
		/// </summary>
		Size MapSize
		{
			get
			{
				return new Size((int)MapWidthBox.Value, (int)MapHeightBox.Value);
			}
		}

		/// <summary>
		/// Block size
		/// </summary>
		Size BlockSize
		{
			get
			{
				return new Size((int)BlockWidthBox.Value, (int)BlockHeightBox.Value);
			}
		}



		/// <summary>
		/// Texture tiles
		/// </summary>
		TileSet Tiles;



		/// <summary>
		/// Data
		/// </summary>
		byte[] Data;


		/// <summary>
		/// 
		/// </summary>
		int DataPos
		{
			get
			{
				return TrackPosition.Value;	
			}

			set
			{
				TrackPosition.Value = value;
				LevelGlControl.Invalidate();
			}
		}


		#endregion


		/// <summary>
		/// Convert NonPowerOfTwoTexture to PowerOfTwoTexture
		/// </summary>
		void ConvertNPOTToPOT()
		{

			Bitmap dstbm = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(dstbm);


			Image srcbm = Image.FromFile(OpenTileDlg.FileName);


			Rectangle src = new Rectangle(0, 0, 16, 16);
			Rectangle dst = new Rectangle(0, 0, 16, 16);

			for (int y = 0; y < dstbm.Height; y += 16)
			{
				for (int x = 0; x < dstbm.Width; x += 16)
				{
					gfx.DrawImage(srcbm, dst, src, System.Drawing.GraphicsUnit.Pixel);

					src.X += 16;
					dst.X += 16;

					if (src.X + src.Width > srcbm.Width)
					{
						src.X = 0;
						src.Y += 16;
					}
				}

				dst.X = 0;
				dst.Y += 16;
			}


			dstbm.Save("data/final.png", System.Drawing.Imaging.ImageFormat.Png);

			gfx.Dispose();
			srcbm.Dispose();
			dstbm.Dispose();

		}



	}
}
