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
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Interface;

namespace ArcEngine.Editor
{
	public partial class SceneForm : AssetEditorBase
	{
		/// <summary>
		/// 
		/// </summary>
		public SceneForm(XmlNode node)
		{
			InitializeComponent();
		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Scene;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		Scene Scene;

		#endregion
	}
}
