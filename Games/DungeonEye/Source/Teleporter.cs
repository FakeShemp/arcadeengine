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
using System.Text;
using System.Drawing;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// Teleporter object
	/// </summary>
	public class Teleporter
	{

		/// <summary>
		/// 
		/// </summary>
		public Teleporter(MazeBlock block)
		{
			if (block == null)
				throw new ArgumentNullException("block");

			Block = block;
			Target = new DungeonLocation(Block.Location);
		}


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;
			
			if (xml.Name.ToLower() != "teleporter")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "target":
					{
						Target.Load(node);
					}
					break;
				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;
			

			writer.WriteStartElement("teleporter");
			Target.Save("target", writer);
			writer.WriteEndElement();

			return true;
		}



		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		MazeBlock Block;

	
		/// <summary>
		/// Target of the stair
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the sound to play
		/// </summary>
		public string SoundName
		{
			get;
			set;
		}


		/// <summary>
		/// Silent teleporter
		/// </summary>
		public bool UseSound
		{
			get;
			set;
		}


		/// <summary>
		/// Scope
		/// </summary>
		public TeleporterScope Scope
		{
			get;
			set;
		}

		#endregion


	}


	/// <summary>
	/// Affected object by a teleporter
	/// </summary>
	[Flags]
	public enum TeleporterScope
	{
		/// <summary>
		/// Everything
		/// </summary>
		Everything,

		/// <summary>
		/// Only items
		/// </summary>
		Items,

		/// <summary>
		/// Only monsters
		/// </summary>
		Monsters,

		/// <summary>
		/// Only the team
		/// </summary>
		Team,

	}
}
