﻿#region Licence
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

using ArcEngine.Asset;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Interface;

namespace ArcEngine.Forms
{

	/// <summary>
	/// Base classe for asset editor
	/// </summary>
	public class AssetEditorBase : DockContent
	{

		/// <summary>
		/// Save the asset
		/// </summary>
		public virtual void Save()
		{
			throw new System.NotImplementedException("Asset");
		}


		#region Properties


		/// <summary>
		/// Gets the edited asset 
		/// </summary>
		public virtual IAsset Asset
		{
			get
			{
				throw new System.NotImplementedException("Asset");
			}
		}

		#endregion
	}
}