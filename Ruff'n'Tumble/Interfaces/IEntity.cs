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
using RuffnTumble.Asset;
using ArcEngine;

namespace RuffnTumble.Interface
{
	/// <summary>
	/// Interface pour la gestion des entite par scripting
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Initializes the entity 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		bool Init(Entity entity);


		/// <summary>
		/// Updates the entity every frame
		/// </summary>
		/// <param name="entity"></param>
		void Update(Entity entity, GameTime time);


		/// <summary>
		/// Saves the entity parameters to the bank
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		bool Save(Entity entity, XmlWriter xml);


		/// <summary>
		/// Loads entity parameters from a bank
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		bool Load(Entity entity, XmlNode xml);

	}

}
