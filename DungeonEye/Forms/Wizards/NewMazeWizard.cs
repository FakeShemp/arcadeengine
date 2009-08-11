﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;

namespace DungeonEye.Forms.Wizards
{
	public partial class NewMazeWizard : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public NewMazeWizard(Dungeon dungeon)
		{
			InitializeComponent();

			Dungeon = dungeon;
	
		}


		/// <summary>
		/// FormClosing event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			// 
			if (DialogResult != DialogResult.OK)
				return;

			// Maze already exists ?
			if (string.IsNullOrEmpty(MazeName.Text) || Dungeon.GetMaze(MazeName.Text) != null)
			{
				MessageBox.Show("Maze name already in use or invalid. Use another name !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Cancel = true;
				return;
			}


			// Create the maze
			Maze maze = new Maze(Dungeon);
			maze.Name = MazeName.Text;
			maze.Size = new Size((int)MazeWidthBox.Value, (int)MazeHeightBox.Value);
			Dungeon.AddMaze(maze);

		}



		///// <summary>
		///// Desired size of the blocks
		///// </summary>
		//Size NewBlockSize
		//{
		//   get
		//   {
		//      return new Size((int)BlockWidthBox.Value, (int)BlockHeightBox.Value);
		//   }
		//}



		Dungeon Dungeon;

	}
}
