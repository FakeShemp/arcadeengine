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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Interface;

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class BitmapFontForm : AssetEditorBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BitmapFontForm(XmlNode node)
		{
			InitializeComponent();

			
			FontPropertyBox.SelectedObject = CurrentFont;

			CurrentFont = new BitmapFont();
			CurrentFont.Load(node);




			// Preload background texture resource
		//	CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));


			UpdatePropertyBoxes();

		}

		/// <summary>
		/// Update all property boxes
		/// </summary>
		private void UpdatePropertyBoxes()
		{

			//SizeBox.Value = CurrentFont.Size;

			FontNameBox.BeginUpdate();
			FontNameBox.Items.Clear();
/*
			foreach (string name in ResourceManager.Binaries)
			{
				if (name.EndsWith(".ttf", true, CultureInfo.CurrentCulture))
					FontNameBox.Items.Add(name);
			}
		//	FontNameBox.SelectedText = CurrentFont.FileName;
*/
			FontNameBox.EndUpdate();

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

			ResourceManager.AddAsset<ArcEngine.Asset.BitmapFont>(CurrentFont.Name, doc.DocumentElement);
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

            Batch.Begin();
            
			// Background texture
            Rectangle dst = new Rectangle(Point.Empty, GlControl.Size);
            Batch.Draw(CheckerBoard, dst, dst, Color.White); ;


            Batch.DrawString(CurrentFont, Point.Empty, Color.White, PreviewTextBox.Text);
            Batch.End();

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
		private void BitmapFontForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (CurrentFont != null)
				CurrentFont.Dispose();
			CurrentFont = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Font2dForm_Load(object sender, EventArgs e)
		{
			GlControl.MakeCurrent();
			Display.Init();

            Batch = new SpriteBatch();

			CheckerBoard = new Texture2D(ResourceManager.GetInternalResource("ArcEngine.Resources.checkerboard.png"));
			CheckerBoard.VerticalWrap = TextureWrapFilter.Repeat;
			CheckerBoard.HorizontalWrap = TextureWrapFilter.Repeat;
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
		BitmapFont CurrentFont = null;


		// Background texture
		Texture2D CheckerBoard;

        /// <summary>
        /// 
        /// </summary>
        SpriteBatch Batch;

		#endregion




	}
}
