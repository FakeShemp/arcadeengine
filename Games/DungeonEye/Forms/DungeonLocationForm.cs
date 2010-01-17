#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using DungeonEye;



namespace DungeonEye.Forms
{
	public partial class DungeonLocationForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dungeon">Dungeon handle</param>
		/// <param name="location">Default location</param>
		public DungeonLocationForm(Dungeon dungeon, DungeonLocation location)
		{
			InitializeComponent();

			DungeonControl.Dungeon = dungeon;
			Target = location;

			DirectionBox.BeginUpdate();
			foreach(string name in Enum.GetNames(typeof(CardinalPoint)))
				DirectionBox.Items.Add(name);
			DirectionBox.EndUpdate();


			Init();
		}


		/// <summary>
		/// Initialization
		/// </summary>
		void Init()
		{
			if (DungeonControl.Dungeon == null)
				return;

			MazeBox.BeginUpdate();
			MazeBox.Items.Clear();
			foreach (Maze maze in DungeonControl.Dungeon.MazeList)
				MazeBox.Items.Add(maze.Name);
			MazeBox.EndUpdate();

			if (!string.IsNullOrEmpty(Target.MazeName))
				MazeBox.SelectedItem = Target.MazeName;
			DirectionBox.SelectedItem = Target.Direction.ToString();



		}


		#region Events


		/// <summary>
		/// Change maze
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MazeBox_Click(object sender, EventArgs e)
		{
			if (DungeonControl.Dungeon == null)
				return;

			DungeonControl.Target.SetMaze((string)MazeBox.SelectedItem);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DirectionBox_Click(object sender, EventArgs e)
		{
			if (DirectionBox.SelectedIndex == -1)
				return;

			Target.Direction = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), (string)DirectionBox.SelectedItem);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectBox_Click(object sender, EventArgs e)
		{

		}



		#endregion



		#region Properties


		/// <summary>
		/// Location in the dungeon
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}

		#endregion

	}
}
