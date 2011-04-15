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
using System.Linq;
using System.Text;
using ArcEngine;
using System.Xml;

namespace DungeonEye.EventScript
{
	/// <summary>
	/// Abstract base class for event script actions
	/// </summary>
	public class ScriptAction
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ScriptAction()
		{
			Target = new DungeonLocation();
		}

		/// <summary>
		/// Run the script
		/// </summary>
		/// <returns>True on success</returns>
		public virtual bool Run()
		{
			return false;
		}


		#region IO


		/// <summary>
		/// Loads a party
		/// </summary>
		/// <param name="filename">Xml data</param>
		/// <returns>True if team successfuly loaded, otherwise false</returns>
		public virtual bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			switch (xml.Name)
			{
				case "target":
				{
					if (Target == null)
						Target = new DungeonLocation();

					Target.Load(xml);
				}
				break;

				default:
				{
					Trace.WriteLine("[ScriptAction] Load() : Unknown node \"" + xml.Name + "\" found.");
				}
				break;
			}


			return true;
		}


		/// <summary>
		/// Saves the party
		/// </summary>
		/// <param name="filename">XmlWriter</param>
		/// <returns></returns>
		public virtual bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			Target.Save("target", writer);


			return true;
		}


		#endregion



		#region Properties

		/// <summary>
		/// Target
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the action
		/// </summary>
		public string Name
		{
			get;
			protected set;
		}



		#endregion

	}
}
