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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcEngine;
using System.Xml;


namespace DungeonEye.EventScript
{
	/// <summary>
	/// Teleport the team
	/// </summary>
	public class ScriptTeleport : IScriptAction
	{


		/// <summary>
		/// Run actions
		/// </summary>
		/// <returns></returns>
		public bool Run()
		{
			if (Target == null)
				return false;

			if (Team.Handle.Teleport(Target))
			{
				if (ChangeDirection)
					Team.Handle.Direction = Target.Direction;
				return true;
			}

			return false;
		}

		#region IO


		/// <summary>
		/// Loads a party
		/// </summary>
		/// <param name="filename">Xml data</param>
		/// <returns>True if team successfuly loaded, otherwise false</returns>
		public bool Load(XmlNode xml)
		{
			return true;
		}



		/// <summary>
		/// Saves the party
		/// </summary>
		/// <param name="filename">XmlWriter</param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			return true;
		}


		#endregion



		#region Properties


		/// <summary>
		/// Target destination
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}


		/// <summary>
		/// Change direction
		/// </summary>
		public bool ChangeDirection
		{
			get;
			set;
		}


		/// <summary>
		/// Action's name
		/// </summary>
		public string Name
		{
			get
			{
				return "Teleport";
			}
		}


		#endregion
	}
}
