using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using System.Drawing.Imaging;

//
//
// http://madskristensen.net/post/Examine-animated-Gife28099s-in-C.aspx
//

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ImportGIFForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public ImportGIFForm()
		{
			InitializeComponent();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadAnimationBox_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Multiselect = false;
			dlg.Title = "Select GIF animation to import...";
			dlg.Filter = "GIF Anim (*.gif)|*.gif|All files (*.*)|*.*";
			if (dlg.ShowDialog() != DialogResult.OK)
				return;

			Image = Image.FromFile(dlg.FileName);
			if (Image == null)
				return;

			// Not a GIF image
			if (!Image.RawFormat.Equals(ImageFormat.Gif))
			{
				Image.Dispose();
				Image = null;
				return;
			}

			// Not an animated GIF
			if (!ImageAnimator.CanAnimate(Image))
			{
				Image.Dispose();
				Image = null;
				return;
			}



			FrameDim = new FrameDimension(Image.FrameDimensionsList[0]);
			FrameCount = Image.GetFrameCount(FrameDim);


			NamesGroup.Enabled = true;
			TextureGroup.Enabled = true;
			FramesGroup.Enabled = true;

			string name = dlg.SafeFileName.Substring(0, dlg.SafeFileName.Length - 4);
			TextureNameBox.Text = name;
			TileSetNameBox.Text = name;
			AnimationNameBox.Text = name;

			PreviewBox.Image = Image;

			DisplayStats();
		}



		/// <summary>
		/// Display informations
		/// </summary>
		void DisplayStats()
		{
			if (Image == null)
				return;

			WidthBox.SelectedItem = "256";
			AnimSizeLabel.Text = "Animation size : " + Image.Width + " x " + Image.Height;
			FrameCountLabel.Text = "Frame count : " + FrameCount;

			FirstFrameBox.Maximum = FrameCount;
			FirstFrameBox.Value = 0;

			LastFrameBox.Maximum = FrameCount;
			LastFrameBox.Value = FrameCount;


		}


		/// <summary>
		/// Process GIF to animation
		/// </summary>
		void Process()
		{
			if (Image == null)
			{
				MessageBox.Show("Load an image first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (string.IsNullOrEmpty(TileSetNameBox.Text))
			{
				MessageBox.Show("Invalid TileSet name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (string.IsNullOrEmpty(AnimationNameBox.Text))
			{
				MessageBox.Show("Invalid Animation name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (string.IsNullOrEmpty(TextureNameBox.Text))
			{
				MessageBox.Show("Invalid Texture name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}


			Texture texture = new Texture(ComputeTextureSize());
			TileSet tileset = new TileSet();
			tileset.TextureName = TextureNameBox.Text;
			tileset.Texture = texture;
			Animation animation = new Animation();

			int maxwidth = int.Parse(WidthBox.SelectedItem as string);
			Point location = Point.Empty;
			for (int f = 0; f < FrameCount; f++)
			{
				// Select the frame
				SetFrame(f);
				Bitmap bm = new Bitmap(Image);


				//
				// Blit the frame to the texture
				texture.Blit(bm, location);
				
				// Move to the next free location
				location.Offset(bm.Width, 0);
				if (location.X > maxwidth)
				{
					location.X = 0;
					location.Y += bm.Height;
				}

				// Add in the tileset
				tileset.AddTile(f, new Rectangle(location, bm.Size));
	
			
			}

		//	texture.Save("texture.png");


/*
			int delay = 0;
			int this_delay = 0;
			int index = 0;
			for (int f = 0; f < FrameCount; f++)
			{
				this_delay = BitConverter.ToInt32(Image.GetPropertyItem(20736).Value, index) * 10;
				delay += (this_delay < 100 ? 100 : this_delay);  // Minimum delay is 100 ms
				index += 4;
			}
*/

			//
			// Save assets to the manager
			texture.SaveToBank(TextureNameBox.Text + ".png");
			ResourceManager.AddAsset<TileSet>(TileSetNameBox.Text, tileset);
			//ResourceManager.AddAsset<Animation>(AnimationNameBox.Text, animation);

			Close();
		}


		/// <summary>
		/// Compute the texture size
		/// </summary>
		/// <returns></returns>
		Size ComputeTextureSize()
		{
			if (Image == null)
				return Size.Empty;

			int maxwidth = int.Parse(WidthBox.SelectedItem as string);

			Size size = new Size(0, Image.Height);
			for (int i = 0; i < FrameCount; i++)
			{
				size.Width += Image.Width;

				if (size.Width > maxwidth)
				{
					size.Height += Image.Height;
					size.Width = 0;
				}
			}

			size.Width = maxwidth;

			return Texture.GetNextPOT(size);
		}



		/// <summary>
		/// Set the current frame
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		void SetFrame(int id)
		{
			if (id < 0 || id > FrameCount)
				return ;
			
			Image.SelectActiveFrame(FrameDim, id);
		}


		/// <summary>
		/// Process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OkBox_Click(object sender, EventArgs e)
		{
			Process();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WidthBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (Image == null)
				return;

			Size size = ComputeTextureSize();
			TextureSizeLabel.Text = "Texture size : " + size.Width + " x " + size.Height;
		}



		#region Properties

		/// <summary>
		/// Length of a frame
		/// </summary>
		public TimeSpan AnimationLength
		{
			get;
			private set;
		}



		/// <summary>
		/// Number of fram in the animation
		/// </summary>
		public int FrameCount
		{
			get;
			private set;
		}
	
		/// <summary>
		/// Currrent animation
		/// </summary>
		Image Image;

		/// <summary>
		/// 
		/// </summary>
		FrameDimension FrameDim;

		#endregion




	}
}
