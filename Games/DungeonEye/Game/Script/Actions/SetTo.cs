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
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Input;
using DungeonEye;
using DungeonEye.Script;

namespace DungeonEye.Script.Actions
{
	/// <summary>
	/// Activate a target
	/// </summary>
	public class SetTo : ActionBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public SetTo(Dungeon dungeon)
		{
			Name = XmlTag;

			Square = new Square(null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Run()
		{
			if (Target == null || Square == null)
				return false;

			Maze maze = Target.GetMaze(GameScreen.Dungeon);
			if (maze == null)
				return false;

			maze.SetSquare(Square, Target.Coordinate);

			return true;
		}


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != XmlTag)
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case Square.Tag:
					{
						if (Square == null)
							Square = new Square(null);

						Square.Load(node);
					}
					break;

					default:
					{
						base.Load(node);
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
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement(XmlTag);

			if (Square != null)
				Square.Save(writer);

			base.Save(writer);

			writer.WriteEndElement();

			return true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string XmlTag = "SetTo";



		/// <summary>
		/// Square model to set
		/// </summary>
		public Square Square
		{
			get;
			set;
		}

		#endregion
	}
}
