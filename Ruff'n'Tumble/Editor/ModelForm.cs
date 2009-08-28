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
using WeifenLuo.WinFormsUI.Docking;
using System.Text;
using System.Windows.Forms;



namespace RuffnTumble.Editor
{
	public partial class ModelForm : DockContent
	{
		public ModelForm()
		{
			InitializeComponent();
		}

/*

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool Init(Model model)
		{

			TabText = model.Name;
			PropertyBox.SelectedObject = model;

			return true;
		}

*/
	}
}
