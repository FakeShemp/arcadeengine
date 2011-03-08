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
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Interface;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Decoration form
	/// </summary>
	public partial class DecorationForm : AssetEditorBase
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		public DecorationForm(XmlNode node)
		{
			InitializeComponent();


			Decoration = new Decoration();
			Decoration.Load(node);
		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Decoration;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		Decoration Decoration;


		#endregion

	}
}
