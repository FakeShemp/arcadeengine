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



			FrameDimension FrameDim = new FrameDimension(Image.FrameDimensionsList[0]);
			FrameCount = Image.GetFrameCount(FrameDim);


			NamesGroup.Enabled = true;
			TextureGroup.Enabled = true;
			FramesGroup.Enabled = true;

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
				return;

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

	
			
			// Compute the size of the texture
			Size size = new Size();
			for (int i = 0; i < FrameCount; i++)
			{

			}
		}


		/// <summary>
		/// Compute the texture size
		/// </summary>
		/// <returns></returns>
		Size ComputeTextureSize(int width)
		{
			if (Image == null)
				return Size.Empty;


			Size size = new Size();

			for (int i = 0; i < FrameCount; i++)
			{
			}


			return size;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Image GetFrame(int id)
		{
			if (id < 0 || id > FrameCount)
				return null;


			return null;
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

			Size size = ComputeTextureSize(int.Parse(WidthBox.SelectedItem as string));
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



		#endregion




	}
}
