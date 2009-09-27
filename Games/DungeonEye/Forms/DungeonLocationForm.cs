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
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Providers;
using ArcEngine.Asset;
using ArcEngine.Games.DungeonEye;



namespace ArcEngine.Games.DungeonEye.Forms
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
