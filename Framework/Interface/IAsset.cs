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
using System.Text;
using System.Xml;



namespace ArcEngine.Interface
{
	/// <summary>
	/// Asset base interface
	/// </summary>
	public interface IAsset
	{



		#region IO
		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		bool Load(XmlNode xml);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		bool Save(XmlWriter writer);

		#endregion


		#region Properties


		/// <summary>
		/// Name of the asset
		/// </summary>
		string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Xml tag of the asset in bank file
		/// </summary>
		string XmlTag
		{
			get;
		}


		/// <summary>
		/// Is asset disposed
		/// </summary>
		bool IsDisposed
		{
			get;
		}

		#endregion
	}
}
