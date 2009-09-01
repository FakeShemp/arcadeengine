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
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.GUI;
using OpenTK.Graphics;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class LayoutForm : AssetEditor
	{

		/// <summary>
		/// 
		/// </summary>
		public LayoutForm(XmlNode node)
		{
			InitializeComponent();

			SelectionBox = new SelectionBox();


			// OpenGL control
			//Device = new Display();
			RenderControl.MakeCurrent();
			//Display.ShareVideoContext();
			Display.Texturing = true;
			Display.Blending = true;
			Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


			CheckerBoard = new Texture(ResourceManager.GetResource("ArcEngine.Resources.checkerboard.png"));



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

		private void LayoutForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Layout Editor", "Save modifications ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

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
		private void RenderControl_Paint(object sender, PaintEventArgs e)
		{
			RenderControl.MakeCurrent();

			// Stop the draw timer
			DrawTimer.Stop();

			// Clear the background
			Display.ClearBuffers();


			// Background texture
		//	Display.Texture = CheckerBoard;
			Rectangle rect = new Rectangle(Point.Empty, RenderControl.Size);
			CheckerBoard.Blit(rect, rect);



			// Draw the layout
			CurrentLayout.Draw();


			// Draw the selection box
			SelectionBox.Draw();

			// If no action and mouse over an element, draw its bounding box
			if (SelectionBox.MouseTool == SelectionBox.MouseTools.NoTool)
			{
				GuiBase elem = FindElementAt(RenderControl.PointToClient(Control.MousePosition));
				if (elem != null)
				{
					Display.Rectangle(elem.Rectangle, false);
				}
			}


			// Start the draw timer
			DrawTimer.Start();

			RenderControl.SwapBuffers();
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
			if (CurrentElement is GUI.Button)
			{
				GUI.Button button = CurrentElement as GUI.Button;
				//button.ResizeToFitTexture();
			}
		}


		#endregion



		#region Main events


		/// <summary>
		/// OnMouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseMove(object sender, MouseEventArgs e)
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
		private GuiBase FindElementAt(Point Location)
		{

			// Find the element under the mouse
			foreach (GuiBase element in CurrentLayout.Elements)
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
		private void RenderControl_MouseDown(object sender, MouseEventArgs e)
		{

			if (e.Button == MouseButtons.Left)
			{
				// Find the element under the mouse
				GuiBase elem = FindElementAt(e.Location);
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
		private void RenderControl_MouseUp(object sender, MouseEventArgs e)
		{
			SelectionBox.OnMouseUp(e);
		}


		/// <summary>
		/// OnMouseClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseClick(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// OnMouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenderControl_MouseDoubleClick(object sender, MouseEventArgs e)
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
		GuiBase CurrentElement;


		/// <summary>
		/// SelectionBox to resize gui elements
		/// </summary>
		SelectionBox SelectionBox;

		/// <summary>
		/// Background texture
		/// </summary>
		Texture CheckerBoard;
		#endregion
	}
}
