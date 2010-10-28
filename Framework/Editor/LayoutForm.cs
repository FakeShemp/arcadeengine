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
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Interface;
using ArcEngine.Utility.GUI;


namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class LayoutForm : AssetEditorBase
	{

		/// <summary>
		/// 
		/// </summary>
		public LayoutForm(XmlNode node)
		{
			InitializeComponent();

			SelectionBox = new SelectionBox();



			CheckerBoard = new Texture2D(ResourceManager.GetInternalResource("ArcEngine.Resources.checkerboard.png"));



			//
			CurrentLayout = new Layout();
			CurrentLayout.Load(node);

			GuiPropertyBox.SelectedObject = CurrentLayout;
			//TabText = CurrentLayout.Name;

			// Gather all elements
			RebuildElementsBox();

			//
			DrawTimer.Start();

		}





		/// <summary>
		/// Populate the ElementsBox listview
		/// </summary>
		void RebuildElementsBox()
		{
			ElementsBox.Items.Clear();
			ElementsBox.BeginUpdate();
		//	foreach (GuiBase element in CurrentLayout.Elements)
		//		ElementsBox.Items.Add(element.Name);
			ElementsBox.EndUpdate();
		}


		/// <summary>
		/// Select an element by its name
		/// </summary>
		/// <param name="name"></param>
		void SelectElement(string name)
		{
			CurrentElement = CurrentLayout.GetElement(name);

			if (CurrentElement != null)
			{
				SelectionBox.Rectangle = CurrentElement.Rectangle;
				ElementPropertyBox.SelectedObject = CurrentElement;
				ElementsBox.SelectedItem = name;

			}
			else
			{
				SelectionBox.Rectangle = Rectangle.Empty;
				ElementPropertyBox.SelectedObject = null;
				if (ElementsBox.SelectedIndex != -1)
					ElementsBox.SetSelected(ElementsBox.SelectedIndex, false);
			}

		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				CurrentLayout.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<Layout>(CurrentLayout.Name, doc.DocumentElement);
		}




		#region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void LayoutForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Layout Editor", "Save modifications ?", System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question);

			if (result == System.Windows.Forms.DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}

			Save();

            if (Batch != null)
                Batch.Dispose();
            Batch = null;

			if (CheckerBoard != null)
				CheckerBoard.Dispose();
			CheckerBoard = null;

			if (CurrentLayout != null)
				CurrentLayout.Dispose();
			CurrentLayout = null;

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DrawTimer_Tick(object sender, EventArgs e)
		{
			RenderControl.Invalidate();
		}



		/// <summary>
		/// Time to paint my dear !
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			RenderControl.MakeCurrent();

			// Stop the draw timer
			DrawTimer.Stop();

			// Clear the background
			Display.ClearBuffers();

            Batch.Begin();

			// Background texture


            // Background texture
            Rectangle dst = new Rectangle(Point.Empty, RenderControl.Size);
            Batch.Draw(CheckerBoard, dst, dst, Color.White);


            // Draw the layout
			CurrentLayout.Draw(Batch);


			// Draw the selection box
			SelectionBox.Draw(Batch);

			// If no action and mouse over an element, draw its bounding box
			if (SelectionBox.MouseTool == SelectionBox.MouseTools.NoTool)
			{
				Control elem = FindElementAt(RenderControl.PointToClient(System.Windows.Forms.Control.MousePosition));
				if (elem != null)
				{
					//Display.DrawRectangle(elem.Rectangle, Color.White);
				}
			}

            Batch.End();
			RenderControl.SwapBuffers();

			// Start the draw timer
			DrawTimer.Start();

		}



		/// <summary>
		/// Resize the render control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_Resize(object sender, EventArgs e)
		{
			RenderControl.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), RenderControl.Size);
		}


		/// <summary>
		/// Element selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ElementsBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ElementsBox.SelectedIndex != -1)
				SelectElement(ElementsBox.SelectedItem.ToString());
		}


		/// <summary>
		/// Remove the selected element
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (CurrentElement != null)
			{
				CurrentLayout.DeleteElement(CurrentElement);
				SelectElement(null);
				RebuildElementsBox();
			}
		}





		/// <summary>
		/// Resize Element to fit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResizeToFitButton_Click(object sender, EventArgs e)
		{
			// Is it a Button ?
			//if (CurrentElement is Button)
			//{
			//   GUI.Button button = CurrentElement as GUI.Button;
			//   //button.ResizeToFitTexture();
			//}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LayoutForm_Load(object sender, EventArgs e)
		{
			RenderControl.MakeCurrent();
			Display.Init();

            Batch = new SpriteBatch();

		}

		#endregion



		#region Main events


		/// <summary>
		/// OnMouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// If user resized the selectionbox, then resize the element
			if (SelectionBox.OnMouseMove(e))
			{
				CurrentElement.Rectangle = SelectionBox.Rectangle;
			}

		}



		/// <summary>
		/// Find the element at a location
		/// </summary>
		/// <param name="Location">Location to loook at</param>
		/// <rereturns>The GuiBase element or null if nothing</rereturns>
		private Control FindElementAt(Point Location)
		{

			// Find the element under the mouse
			foreach (Control element in CurrentLayout.Elements)
			{
				if (element.Rectangle.Contains(Location))
					return element;
			}


			return null;
		}


		/// <summary>
		/// OnMouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				// Find the element under the mouse
				Control elem = FindElementAt(e.Location);
				if (elem != null)
				{
				//	SelectElement(elem.Name);
				//	SelectionBox.OnMouseDown(e);
				}
				else
				{
					// Unselect the element
					SelectElement(null);
				}
			}
		}



		/// <summary>
		/// OnMouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			SelectionBox.OnMouseUp(e);
		}


		/// <summary>
		/// OnMouseClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{

		}


		/// <summary>
		/// OnMouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{

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
				return CurrentLayout;
			}
		}

		/// <summary>
		/// Layout to edit
		/// </summary>
		Layout CurrentLayout;


		/// <summary>
		/// Current element
		/// </summary>
		Control CurrentElement;


		/// <summary>
		/// SelectionBox to resize gui elements
		/// </summary>
		SelectionBox SelectionBox;

		/// <summary>
		/// Background texture
		/// </summary>
		Texture2D CheckerBoard;


        /// <summary>
        /// SpriteBatch
        /// </summary>
        SpriteBatch Batch;


		#endregion


	}
}
