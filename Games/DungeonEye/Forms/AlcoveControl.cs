#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Alcove control
	/// </summary>
	public partial class AlcoveControl : UserControl
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="alcove">Alcove handle</param>
		/// <param name="dungeon">Dungeon handle</param>
		public AlcoveControl(Alcove alcove, Dungeon dungeon)
		{
			InitializeComponent();


			NorthBox.Checked = alcove.GetSideState(CardinalPoint.North);
			SouthBox.Checked = alcove.GetSideState(CardinalPoint.South);
			WestBox.Checked = alcove.GetSideState(CardinalPoint.West);
			EastBox.Checked = alcove.GetSideState(CardinalPoint.East);

			Alcove = alcove;
		}




		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NorthBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Alcove == null)
				return;

			Alcove.SetSideState(CardinalPoint.North, NorthBox.Checked);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EastBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Alcove == null)
				return;

			Alcove.SetSideState(CardinalPoint.East, EastBox.Checked);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SouthBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Alcove == null)
				return;

			Alcove.SetSideState(CardinalPoint.South, SouthBox.Checked);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WestBox_CheckedChanged(object sender, EventArgs e)
		{
			if (Alcove == null)
				return;

			Alcove.SetSideState(CardinalPoint.West, WestBox.Checked);
		}
		#endregion



		#region Properties

		/// <summary>
		/// Alcove
		/// </summary>
		Alcove Alcove;

		#endregion

	}
}
