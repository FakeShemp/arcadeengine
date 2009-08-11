using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Providers;
using ArcEngine.Asset;
using DungeonEye;



namespace DungeonEye.Forms
{
	public partial class DungeonLocationForm : Form
	{
		public DungeonLocationForm(Dungeon dungeon, DungeonLocation location)
		{
			Dungeon = dungeon;
			DungeonLocation = location;

			InitializeComponent();

			//DungeonControl
		}





		#region Properties

		/// <summary>
		/// Dungeon
		/// </summary>
		public Dungeon Dungeon
		{
			get;
			private set;
		}


		/// <summary>
		/// Location in the dungeon
		/// </summary>
		public DungeonLocation DungeonLocation
		{
			get;
			set;
		}

		#endregion

	}
}
