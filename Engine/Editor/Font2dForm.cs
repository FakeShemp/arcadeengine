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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class Font2dForm : AssetEditor
	{
		/// <summary>
		/// 
		/// </summary>
		public Font2dForm(XmlNode node)
		{
			InitializeComponent();

			
			FontPropertyBox.SelectedObject = CurrentFont;

			CurrentFont = new ArcEngine.Asset.Font2d();
			CurrentFont.Load(node);


			GlControl.MakeCurrent();
			Display.Init();
			Display.ClearColor = Color.Green;
			Display.Texturing = true;
			Display.Blending = true;
			Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


			// Preload background texture resource
			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


			UpdatePropertyBoxes();

		}

		/// <summary>
		/// Update all property boxes
		/// </summary>
		private void UpdatePropertyBoxes()
		{

			//SizeBox.Value = CurrentFont.Size;

			FontNameBox.Items.Clear();
			foreach (string name in ResourceManager.LoadedBinaries)
			{
				if (name.EndsWith(".ttf", true, CultureInfo.CurrentCulture))
					FontNameBox.Items.Add(name);
			}
		//	FontNameBox.SelectedText = CurrentFont.FileName;

			StyleBox.Items.Clear();
			StyleBox.DataSource = Enum.GetNames(typeof(FontStyle));
		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				CurrentFont.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<ArcEngine.Asset.Font2d>(CurrentFont.Name, doc.DocumentElement);
		}






		#region Events
		/// <summary>
		/// OnPaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			DrawTimer.Stop();

			GlControl.MakeCurrent();
			Display.ClearBuffers();

			// Background texture
			//Device.Texture = CheckerBoard;
		//	Rectangle rect = new Rectangle(Point.Empty, GlControl.Size);
		//	CheckerBoard.Blit(rect, rect);


			// Prints the text
		//	Device.Font = CurrentFont;
			if (CurrentFont != null)
			{
				CurrentFont.DrawText(PreviewTextBox.Text, Point.Empty);
			}

			DrawTimer.Start();

			GlControl.SwapBuffers();
		}


		/// <summary>
		/// Control resize
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControl.Size);
		}


		/// <summary>
		/// OnClick, generate the font
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GenerateButton_Click(object sender, EventArgs e)
		{
			if (CurrentFont == null)
				return;

			FontStyle style = (FontStyle)Enum.Parse(typeof(FontStyle), (string)StyleBox.SelectedItem, true);
		//	CurrentFont.LoadFromTTF(FontNameBox.Text, (int)SizeBox.Value, style);
		}


		/// <summary>
		/// Draw Timer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawTimer_Tick(object sender, EventArgs e)
		{
			GlControl.Invalidate();
		}



		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Texture Font Editor", "Save modifciations ?", MessageBoxButtons.YesNoCancel);

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



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return CurrentFont;
			}
		}

		/// <summary>
		/// Current font
		/// </summary>
		ArcEngine.Asset.Font2d CurrentFont = null;


		// Background texture
		Texture CheckerBoard;


		#endregion


	}
}
