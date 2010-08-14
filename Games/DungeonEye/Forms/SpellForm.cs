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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using DungeonEye.Interfaces;

namespace DungeonEye.Forms
{
	public partial class SpellForm : AssetEditor
	{
		public SpellForm(XmlNode node)
		{
			InitializeComponent();

			Spell = new Spell();
			Spell.Load(node);
		}






		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Spell;
			}
		}




		/// <summary>
		/// Spell to edit
		/// </summary>
		Spell Spell;

		#endregion
	}
}
