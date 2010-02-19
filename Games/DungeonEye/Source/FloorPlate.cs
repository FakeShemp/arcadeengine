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


namespace DungeonEye
{
	public class FloorPlate
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public FloorPlate()
		{
			Script = new Script();			
		}


		/// <summary>
		/// The team or an item is walking / falling on the plate
		/// </summary>
		/// <param name="team">Handle to the team</param>
		/// <param name="block">Mazeblock of the fllor plate</param>
		public void OnTeamTouch(Team team, MazeBlock block)
		{
			// No script defined
			if (string.IsNullOrEmpty(OnEnterScript) || Script == null)
				return;

			Script.Compile();

			Script.Invoke(OnEnterScript, team, block);


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		public void OnMonsterTouch(Monster monster)
		{
			if (monster == null)
				return;
		}

		/// <summary>
		/// Team /item is leaving the Floor Plate
		/// </summary>
		/// <param name="team"></param>
		/// <param name="item"></param>
		public void OnTeamLeave(Team team, MazeBlock block)
		{
			// No script defined
			if (string.IsNullOrEmpty(OnEnterScript) || Script == null)
				return;

			Script.Compile();

			Script.Invoke(OnLeaveScript, team, block);

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		public void OnMonsterLeave(Monster monster)
		{
			if (monster == null)
				return;
		}


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
						Script = Script.LoadFromBank(ScriptName);
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
		public bool Save(XmlWriter writer)
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


		#region Properties


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
