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
	/// Floor plate
	/// </summary>
	public class FloorPlate : SquareActor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public FloorPlate(Square square) : base(square)
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
			if (TileSet == null)
				return;

			TileDrawing td = MazeDisplayCoordinates.GetFloorPlate(position);
			if (td != null)
				batch.DrawTile(TileSet, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
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

			if (xml.Name.ToLower() != "floorplate")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					case "invisible":
					{
						Invisible = true;
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


			writer.WriteStartElement("floorplate");

			if (Invisible)
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
		public override bool OnTeamEnter(Team team)
		{
			if (team == null)
				return false;

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
		public override bool OnTeamLeave(Team team)
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
		/// Tileset for the drawing
		/// </summary>
		TileSet TileSet
		{
			get
			{
				if (Square == null)
					return null;

				return Square.Location.Maze.OverlayTileset;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public override bool IsBlocking
		{
			get
			{
				return false;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public override bool CanPassThrough
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Is the floor plate visible
		/// </summary>
		public bool Invisible
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
