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


namespace ArcEngine.Games.DungeonEye
{
	/// <summary>
	/// Life struct
	/// </summary>
	public class Life
	{


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name.ToLower() != "life")
				return false;

			Actual = short.Parse(xml.Attributes["actual"].Value);
			Max = short.Parse(xml.Attributes["max"].Value);

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("life");
			writer.WriteAttributeString("actual", Actual.ToString());
			writer.WriteAttributeString("max", Max.ToString());
			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Helper


		public override string ToString()
		{
			return Actual.ToString() + "/" + Max.ToString();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Actual life 
		/// </summary>
		public short Actual;

		/// <summary>
		/// Maximum life 
		/// </summary>
		public short Max;


		/// <summary>
		/// Returns if the entity is dead (life <= 0)
		/// </summary>
		public bool IsDead
		{
			get
			{
				return Actual <= 0 ? true : false;
			}
		}

		#endregion
	}
}
