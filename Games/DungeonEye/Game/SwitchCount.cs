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
using System.Text;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine;
using DungeonEye.EventScript;


namespace DungeonEye
{
	/// <summary>
	/// Switch count
	/// </summary>
	public class SwitchCount
	{

		/// <summary>
		/// 
		/// </summary>
		public SwitchCount()
		{

		}


		/// <summary>
		/// 
		/// </summary>
		public void Activate()
		{
		}



		/// <summary>
		/// 
		/// </summary>
		public void Deactivate()
		{
		}




		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node">Xml node</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Remaining = int.Parse(xml.Attributes["remaining"].Value);
			Needed = int.Parse(xml.Attributes["needed"].Value);
			ResetOnTrigger = bool.Parse(xml.Attributes["reset"].Value);


			return true;
		}


		/// <summary>
		/// Saves location
		/// </summary>
		/// <param name="name">Node's name</param>
		/// <param name="writer">XmlWriter handle</param>
		/// <returns></returns>
		public bool Save(string name, XmlWriter writer)
		{
			if (writer == null || string.IsNullOrEmpty(name))
				return false;



			writer.WriteStartElement(name);
			writer.WriteAttributeString("remaining", Remaining.ToString());
			writer.WriteAttributeString("needed", Needed.ToString());
			writer.WriteAttributeString("reset", ResetOnTrigger.ToString());
			writer.WriteEndElement();

			return true;
		}



		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public int Needed
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		public int Remaining
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		public bool ResetOnTrigger
		{
			get;
			set;
		}

		#endregion

	}
}
