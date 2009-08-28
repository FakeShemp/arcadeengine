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
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using RuffnTumble.Asset;

//
// MD5 :
// - http://www.flowgroup.fr/fr/kb/technical/md5.aspx
// - http://www.codyx.org/snippet_hashage-md5_113.aspx
//
//
//
//
//
//

namespace RuffnTumble.Editor.Wizards
{
	public partial class ImportLevelWizard : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public ImportLevelWizard()
		{
			InitializeComponent();

            ReportLabelBox.Text = "";
		}



		#region Events

		/// <summary>
		/// Generate the level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonGenerate_Click(object sender, EventArgs e)
		{
			// Canceling current import
			if (BgWorker.IsBusy)
			{
				if (MessageBox.Show("Cancel import ?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					BgWorker.CancelAsync();

				return;
			}


			//if (ResourceManager.GetAsset<Texture>(TextureNameBox.Text) != null || TextureNameBox.Text == "")
			//{
			//   MessageBox.Show("Texture name already in use or invalid. Use another name !");
			//   return;
			//}


			// Level already exists ?
			//if (ResourceManager.GetLevel(LevelNameBox.Text) != null || LevelNameBox.Text == "")
			//{
			//   MessageBox.Show("Level name already in use or invalid. Use another name !");
			//   return;
			//}


			// Visuals
			ReportLabelBox.Text = "Importing level...";
			ProgressBarBox.Maximum = MapSize.Width * MapSize.Height;
			ProgressBarBox.Value = 0;
			ProgressBarBox.Visible = true;
			LoadPictureButton.Enabled = false;
			LevelNameBox.Enabled = false;
			TextureNameBox.Enabled = false;
			ButtonClose.Enabled = false;
			BlockWidthBox.Enabled = false;
			BlockHeightBox.Enabled = false;
			ButtonGenerate.Text = "Cancel";



			// Create the Level
			//Level level = ResourceManager.CreateLevel(LevelNameBox.Text);
			//if (level == null)
			//   return;
			//level.Size = MapSize;

			//// Add the layer
			//Layer layer = level.AddLayer("layer_1");
			//if (layer == null)
			//   return;

			//layer.TextureName = TextureNameBox.Text;


			//// Launch the thread
			//Time = DateTime.Now;
			//BgWorker.RunWorkerAsync(layer);

		}



		/// <summary>
		/// OnClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadPictureButton_Click(object sender, EventArgs e)
		{
			if (dlg.ShowDialog() != DialogResult.OK)
				return;

			PreviewBox.Load(dlg.FileName);
			MapSize = new Size(PreviewBox.Image.Width / BlockSize.Width, PreviewBox.Image.Height / BlockSize.Height);
		}


		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PreviewBox_Paint(object sender, PaintEventArgs e)
		{
			if (PreviewBox.Image == null)
				return;

			System.Drawing.Graphics g = e.Graphics;

			// Draws grid lines
			Pen p = new Pen(Color.Red);

			for (int x = 0; x < PreviewBox.Width; x+= (int)BlockWidthBox.Value)
				g.DrawLine(p, x, 0, x, PreviewBox.Height);

			for (int y = 0; y < PreviewBox.Height; y+= (int)BlockHeightBox.Value)
				g.DrawLine(p, 0, y, PreviewBox.Width, y);

			p.Dispose();
		}



		/// <summary>
		/// Invalidate the PreviewBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BlockSize_ValueChanged(object sender, EventArgs e)
		{
			PreviewBox.Invalidate();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <todo>Faire en sorte que la hauteur de la texture des tiles ne soit pas limitéé
		/// a du 512x512 pixels. Rendre cette taille dynamique.</todo>
		private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			Layer layer = e.Argument as Layer;


			// Load the image
			Bitmap FromTexture = new Bitmap(dlg.FileName);

			// The final texture containing tiles
			Bitmap FinalTexture = new Bitmap(512, 512);

			// Offset of the next block in the FinalTexture
			Rectangle Dest = new Rectangle(Point.Empty, BlockSize);


			// Table for each tile
			Dictionary<string, int> TilesMD5;
			TilesMD5 = new Dictionary<string, int>();

			// Pixels of the texture
			byte[] pixels = new byte[BlockSize.Width * BlockSize.Height * 4];

			//Create the md5 hash provider.
			MD5 md5 = new MD5CryptoServiceProvider();


			try
			{
				// Scan the image
				for (int y = 0; y < FromTexture.Height; y += BlockSize.Height)
				{
					for (int x = 0; x < FromTexture.Width; x += BlockSize.Width)
					{

						// Current tile
						Rectangle Src = new Rectangle(x, y, BlockSize.Width, BlockSize.Height);


						// Check for limit boundary
						if (Src.X + Src.Width > FromTexture.Width || Src.Y + Src.Height > FromTexture.Height)
							continue;

						worker.ReportProgress(1);


						

						// Collect pixels
						BitmapData bmdata = FromTexture.LockBits(Src, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						Marshal.Copy(bmdata.Scan0, pixels, 0, BlockSize.Width * BlockSize.Height * 4);

						// Unlock the texture
						FromTexture.UnlockBits(bmdata);


						//Compute md5 hash
						string hash = BitConverter.ToString(md5.ComputeHash(pixels));



						// Is this hash already exists ?
						if (!TilesMD5.ContainsKey(hash))
						{
							// Add the tile to the table
							TilesMD5[hash] = TilesMD5.Count;


							// Add the tile to the FinalTexture
							System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(FinalTexture);
							g.DrawImage(FromTexture, Dest, Src, GraphicsUnit.Pixel);
							g.Dispose();

							// Compute the next block destination
							Dest.X += BlockSize.Width;
							if (Dest.X >= FinalTexture.Width)
							{
								Dest.X = 0;
								Dest.Y += BlockSize.Height;
							}

						}

						layer.SetTileAt(new Point(x / BlockSize.Width, y / BlockSize.Height), TilesMD5[hash]);



						// Cancel the job ?
						if (worker.CancellationPending)
							return;
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine( "Failed to import level from game map \"" + dlg.FileName + "\" ! " + ex.StackTrace);
				return;
			}

			// Unlock source file
			FromTexture.Dispose();

			//FinalTexture.Save("FinalTexture.png", ImageFormat.Png);


			// Add the texture as a binary
			string binname = TextureNameBox.Text;
			if (!binname.EndsWith(".png"))
				binname += ".png";
/*
			Binary bin = ResourceManager.CreateBinary(binname);
			if (bin == null)
			{
				Trace.WriteLine( "Failed to create binary \"" + binname + "\" !", null));
				return;
			}
			MemoryStream ms = new MemoryStream();
			using (ms)
			{
				FinalTexture.Save(ms, ImageFormat.Png);
				bin.Data = ms.ToArray();
			}
*/
			ResourceManager.LoadResource(binname);

			e.Result = true;
		}


		/// <summary>
		/// Job's done cap'tain !
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

			TimeSpan elapsed = DateTime.Now - Time;


			LoadPictureButton.Enabled = true;
			LevelNameBox.Enabled = true;
			TextureNameBox.Enabled = true;
			ButtonClose.Enabled = true;
			BlockWidthBox.Enabled = true;
			BlockHeightBox.Enabled = true;
			ButtonGenerate.Text = "Generate !";
			ProgressBarBox.Visible = false;


			if (e.Result == null)
			{
				// Remove the level
				ResourceManager.RemoveAsset<Level>(LevelNameBox.Text);


				// Report failure
				ReportLabelBox.Text = "Importing failed !";
				MessageBox.Show("Import failed.", "Failure");

	
				return;
			}


			// Create the final texture
			Texture texture = new Texture();//ResourceManager.CreateAsset<Texture>(TextureNameBox.Text);
			if (texture != null)
			{
				string binname = TextureNameBox.Text;
				if (!binname.EndsWith(".png"))
					binname += ".png";

				texture.LoadImage(binname);
			}


			ReportLabelBox.Text = "Importing done !";
			MessageBox.Show("Import successful in " + elapsed.TotalSeconds + " seconds.", "Success");
		}


		/// <summary>
		/// Still alive..
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BgWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			ProgressBarBox.Value += 1;
		}


		#endregion


		#region Properties



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
		/// Size of the map in block
		/// </summary>
		Size MapSize;




		/// <summary>
		/// Timer
		/// </summary>
		DateTime Time;

		#endregion


	}

}
