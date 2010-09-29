using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine.Graphic;

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
			ClearStats();


			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			dlg.Multiselect = false;
			dlg.Title = "Select GIF animation to import...";
			dlg.Filter = "GIF Anim (*.gif)|*.gif|All files (*.*)|*.*";
			if (dlg.ShowDialog() != DialogResult.OK)
				return;


			try
			{
				Image = Image.FromFile(dlg.FileName);
			}
			catch (OutOfMemoryException)
			{
				MessageBox.Show("The file does not have a valid image format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
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
			ImportBox.Enabled = true;

			DisplayStats();
		}


		/// <summary>
		/// Clear statistics
		/// </summary>
		private void ClearStats()
		{
			Image = null;

			PreviewBox.Image = null;
			NamesGroup.Enabled = false;
			TextureGroup.Enabled = false;
			FramesGroup.Enabled = false;

			WidthBox.SelectedItem = "256";
			AnimSizeLabel.Text = "Animation size : ";
			FrameCountLabel.Text = "Frame count : ";

			FirstFrameBox.Value = 0;
			LastFrameBox.Value = 0;

			TextureNameBox.Text = string.Empty;
			TileSetNameBox.Text = string.Empty;
			AnimationNameBox.Text = string.Empty;
			FrameCount = 0;
			FrameDim = null;

			ImportBox.Enabled = false;
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


			// The texture
			Texture2D texture = new Texture2D(ComputeTextureSize());

			// The tileset
			TileSet tileset = new TileSet();
			tileset.Name = TileSetNameBox.Text;
			tileset.TextureName = TextureNameBox.Text + ".png";
			tileset.Texture = texture;

			// The animation
			Animation animation = new Animation();
			animation.Name = AnimationNameBox.Text;
			animation.SetTileSet(TileSetNameBox.Text);
			SetFrame(0);
			//Bitmap bm = new Bitmap(Image);
			animation.FrameRate = TimeSpan.FromMilliseconds(BitConverter.ToInt32(Image.GetPropertyItem(20736).Value, 0) * 10);
			if (animation.FrameRate < TimeSpan.FromMilliseconds(100))
				animation.FrameRate = TimeSpan.FromMilliseconds(100);
			if (BitConverter.ToInt16(Image.GetPropertyItem(20737).Value, 0) != 1)
				animation.Type = AnimationType.Loop;



			Point location = Point.Empty;
			int id = 0;
			for (int f = int.Parse(FirstFrameBox.Value.ToString()); f < int.Parse(LastFrameBox.Value.ToString()); f++)
			{
				// Select the frame
				SetFrame(f);
				Bitmap bm = new Bitmap(Image);

				if (location.X + bm.Width > texture.Size.Width)
				{
					location.X = 0;
					location.Y += bm.Height;
				}

				//
				// Blit the frame to the texture
				texture.SetData(bm, location);

				// Add in the tileset
				tileset.AddTile(id, new Rectangle(location, bm.Size));
				
				// Move to the next free location
				location.Offset(bm.Width, 0);

				animation.AddFrame(id);

				// Next id
				id++;
			}


	



			//
			// Save assets to the manager
			//texture.SaveToBank(TextureNameBox.Text + ".png");
			ResourceManager.AddAsset<TileSet>(TileSetNameBox.Text, tileset);
			ResourceManager.AddAsset<Animation>(AnimationNameBox.Text, animation);

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

			int maxwidth = int.Parse(WidthBox.Text);

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

			return Texture2D.GetNextPOT(size);
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
