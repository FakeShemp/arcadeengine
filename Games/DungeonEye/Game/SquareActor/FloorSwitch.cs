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
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;

namespace DungeonEye
{
	/// <summary>
	/// Floor switch
	/// </summary>
	public class FloorSwitch : SquareActor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square handle</param>
		public FloorSwitch(Square square) : base(square)
		{
			//Script = new Script();			
			AcceptItems = true;
			CanPassThrough = true;
			IsBlocking = false;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="batch"></param>
		/// <param name="field"></param>
		/// <param name="position"></param>
		/// <param name="direction"></param>
		public override void Draw(SpriteBatch batch, ViewField field, ViewFieldPosition position, CardinalPoint direction)
		{
			if (Decoration == null)
				return;

			if (IsHidden)
				return;

			TileDrawing td = DisplayCoordinates.GetFloorPlate(position);
		//TODO
		//	if (td != null)
		//		batch.DrawTile(TileSet, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Floor switch (");


			if (IsHidden)
				sb.Append("Hidden. ");

			sb.Append("Affect ");
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

			if (xml.Name.ToLower() != "floorswitch")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					case "invisible":
					{
						IsHidden = true;
					}
					break;

					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
						//Script = Script.LoadFromBank(ScriptName);
					}
					break;

					case "onleave":
					{
						OnLeaveScript = node.Attributes["name"].Value;
					}
					break;

					case "onenter":
					{
						OnEnterScript = node.Attributes["name"].Value;
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


			writer.WriteStartElement("floorswitch");

			if (IsHidden)
			{
				writer.WriteStartElement("invisible");
				writer.WriteEndElement();
			}


			writer.WriteStartElement("script");
			writer.WriteAttributeString("name", ScriptName);
			writer.WriteEndElement();

			writer.WriteStartElement("onleave");
			writer.WriteAttributeString("name", OnLeaveScript);
			writer.WriteEndElement();

			writer.WriteStartElement("onenter");
			writer.WriteAttributeString("name", OnEnterScript);
			writer.WriteEndElement();

			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Script

		/// <summary>
		/// 
		/// </summary>
		/// <param name="team"></param>
		/// <returns></returns>
		public override bool OnTeamEnter()
		{
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


			return true;
		}


		/// <summary>
		/// Team /item is leaving the Floor Plate
		/// </summary>
		/// <param name="team"></param>
		/// <param name="item"></param>
		public override bool OnTeamLeave()
		{
			// No script defined
			if (string.IsNullOrEmpty(OnEnterScript) || Script == null)
				return false;

			return false;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		public override bool OnMonsterLeave(Monster monster)
		{
			if (monster == null)
				return false;


			return false;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Decoration handle
		/// </summary>
		DecorationSet Decoration
		{
			get
			{
				if (Square == null)
					return null;

				return Square.Maze.Decoration;
			}
		}


		/// <summary>
		/// Is the floor plate visible
		/// </summary>
		public bool IsHidden
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

	
		/// <summary>
		///  Action to execute
		/// </summary>
		public string OnEnterScript
		{
			get;
			set;
		}

		/// <summary>
		///  Action to execute
		/// </summary>
		public string OnLeaveScript
		{
			get;
			set;
		}


		/// <summary>
		///  Action to execute
		/// </summary>
		public string ScriptName
		{
			get;
			set;
		}

		/// <summary>
		/// Script
		/// </summary>
		Script Script;



		#endregion

	}
}
