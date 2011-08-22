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
	public interface IAsset : IDisposable
	{



		#region IO

		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		/// <returns>True on success</returns>
		bool Load(XmlNode xml);


		/// <summary>
		/// Saves asset definition
		/// </summary>
		/// <param name="writer">XmlWriter handle</param>
		/// <returns>True on success</returns>
		bool Save(XmlWriter writer);

		#endregion


		#region Properties


		/// <summary>
		/// Name of the asset used when creating it with ResourceManager
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
