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
using RuffnTumble.Asset;
using WeifenLuo.WinFormsUI.Docking;


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
			GlControl.InitializeContexts();

			Brushes = new Dictionary<Rectangle, LayerBrush>();
		}



		/// <summary>
		/// Initialize the Panel
		/// </summary>
		/// <param name="form"></param>
		/// <returns></returns>
		public bool Init(LevelForm form, VideoRender device)
		{
			Form = form;
			Device = device;


			GlControl.MakeCurrent();
			Device.ShareVideoContext();
			Device.ClearColor = Color.Black;
			Device.Texturing = true;
			Device.Blending = true;






			// Preload texture resources
			CheckerBoard = Device.CreateTexture();
			Stream stream = ResourceManager.GetInternalResource("ArcEngine.Files.checkerboard.png");
			CheckerBoard.LoadImage(stream);
			stream.Close();


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
			Device.ViewPort = new Rectangle(new Point(), GlControl.Size);
		}



		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			Device.ClearBuffers();

			// Mouse coord
			Point mouse = GlControl.PointToClient(Control.MousePosition); 

			// Clear the rectangle brush buffer
			Brushes.Clear();

			// Background texture
		//	Video.Texture = CheckerBoard;
			Rectangle rect = new Rectangle(Point.Empty, GlControl.Size);
			CheckerBoard.Blit(rect, rect);

			Point pos = Point.Empty;
			foreach (LayerBrush brush in Form.CurrentLayer.Brushes)
			{

				// Draw the brush
				brush.Draw(Device, pos, Form.CurrentLayer.TileSet, Form.Level.BlockDimension);
				
				// zone detection
				rect = new Rectangle(pos, new Size(brush.Size.Width * Form.Level.BlockDimension.Width, brush.Size.Height * Form.Level.BlockDimension.Height));
				Brushes[rect] = brush;

				if (brush == SelectedBrush)
				{
					Device.Color = Color.Red;
					Device.Rectangle(rect, false);
					Device.Color = Color.White;
				}


				// Somme blah blah
				if (rect.Contains(mouse))
				{
					Device.Rectangle(rect, false);
				}

				// Move to the next location
				pos.Y += brush.Size.Height * Form.Level.BlockDimension.Height + 32;
			}

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

			new Wizards.NewLayerBrushWizard(Form.LayerBrush, Form.CurrentLayer).ShowDialog();

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


		VideoRender Device;


		/// <summary>
		/// Parent level form
		/// </summary>
		LevelForm Form;



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
