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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Graphic;

namespace DungeonEye.Forms
{
	public partial class DungeonLocationControl : UserControl
	{

		/// <summary>
		/// 
		/// </summary>
		public DungeonLocationControl()
		{
			InitializeComponent();
		}





		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Resize(object sender, EventArgs e)
		{
			GlControlBox.MakeCurrent();
			Display.ViewPort = new Rectangle(new Point(), GlControlBox.Size);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			GlControlBox.MakeCurrent();

			Display.ClearBuffers();


			GlControlBox.SwapBuffers();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_DoubleClick(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_MouseUp(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_MouseDown(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlControlBox_MouseMove(object sender, MouseEventArgs e)
		{

		}


		#endregion



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public Dungeon Dungeon
		{
			get
			{
				return dungeon;
			}
			set
			{
				dungeon = value;
			}
		}
		Dungeon dungeon;


		/// <summary>
		/// 
		/// </summary>
		public Maze Maze
		{
			get;
			set;
		}

		/// <summary>
		/// 
		/// </summary>
		public Point Target
		{
			get;
			set;
		}
		#endregion
	}
}
