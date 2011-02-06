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
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine.Graphic;

namespace DungeonEye
{
	/// <summary>
	/// Invisible force moving the team
	/// </summary>
	public class ForceField : SquareActor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public ForceField(Square square) : base(square)
		{
			Type = ForceFieldType.Spin;
			Spin = CompassRotation.Rotate180;
			Move = CardinalPoint.North;

			AffectTeam = true;
			AffectMonsters = true;
			AffectItems = true;

			AcceptItems = true;
			CanPassThrough = true;
			IsBlocking = false;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("ForceField (");

			switch (Type)
			{
				case ForceFieldType.Spin:
					sb.Append("Spin " + Spin);
				break;
				case ForceFieldType.Move:
					sb.Append("Move " + Move);
				break;
				case ForceFieldType.Block:
					sb.Append("Block");
			break;
			}

			sb.Append(". Affect :");
			if (AffectTeam)
				sb.Append("Team ");
			if (AffectMonsters)
				sb.Append("monsters ");
			if (AffectItems)
				sb.Append("items ");

			sb.Append(")");
			return sb.ToString();
		}


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name.ToLower() != "forcefield")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "type":
					{
						Type = (ForceFieldType)Enum.Parse(typeof(ForceFieldType), node.Attributes["value"].Value);
					}
					break;

					case "spin":
					{
						Spin = (CompassRotation)Enum.Parse(typeof(CompassRotation), node.Attributes["value"].Value);
					}
					break;

					case "move":
					{
						Move = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["value"].Value);
					}
					break;

				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("forcefield");


			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("spin");
			writer.WriteAttributeString("value", Spin.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("move");
			writer.WriteAttributeString("value", Move.ToString());
			writer.WriteEndElement();


			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Script

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool OnTeamEnter()
		{
			switch (Type)
			{
				case ForceFieldType.Spin:
				{
					Team.Location.Direction = Compass.Rotate(Team.Location.Direction, Spin);
				}
				break;

				case ForceFieldType.Move:
				{
					Team.Handle.Offset(Move, 1);
				}
				break;
			}


			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		/// <returns></returns>
		public override bool OnMonsterEnter(Monster monster)
		{
			if (monster == null)
				return false;

			switch (Type)
			{
				case ForceFieldType.Spin:
				{
					monster.Location.Direction = Compass.Rotate(monster.Location.Direction, Spin);
				}
				break;

				case ForceFieldType.Move:
				{

					switch (Move)
					{
						case CardinalPoint.North:
						monster.Location.Coordinate.Offset(0, -1);
						break;
						case CardinalPoint.South:
						monster.Location.Coordinate.Offset(0, 1);
						break;
						case CardinalPoint.West:
						monster.Location.Coordinate.Offset(-1, 0);
						break;
						case CardinalPoint.East:
						monster.Location.Coordinate.Offset(1, 0);
						break;
					}
				}
				break;
			}

			return true;
		}
		#endregion


		#region Properties



		/// <summary>
		/// Type of force field
		/// </summary>
		public ForceFieldType Type
		{
			get;
			set;
		}

		/// <summary>
		/// Type of turn
		/// </summary>
		public CompassRotation Spin
		{
			get;
			set;
		}


		/// <summary>
		/// Moving value
		/// </summary>
		public CardinalPoint Move
		{
			get;
			set;
		}


		/// <summary>
		/// Does affect monsters
		/// </summary>
		public bool AffectMonsters
		{
			get;
			set;
		}


		/// <summary>
		/// Does affect team
		/// </summary>
		public bool AffectTeam
		{
			get;
			set;
		}


		/// <summary>
		/// Does affect items
		/// </summary>
		public bool AffectItems
		{
			get;
			set;
		}

		#endregion
	}


	/// <summary>
	/// Type of force field
	/// </summary>
	public enum ForceFieldType
	{
		/// <summary>
		/// Team is turning
		/// </summary>
		Spin,

		/// <summary>
		/// Team is moved to a direction
		/// </summary>
		Move,

		/// <summary>
		/// Team can't go through
		/// </summary>
		Block,
	}
}
