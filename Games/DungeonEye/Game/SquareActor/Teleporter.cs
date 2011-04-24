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

namespace DungeonEye
{
	/// <summary>
	/// Teleporter object
	/// </summary>
	public class Teleporter : SquareActor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Teleporter(Square block) : base(block)
		{
			if (block == null)
				throw new ArgumentNullException("block");

			AcceptItems = true;
			CanPassThrough = true;
			IsBlocking = false;

			TeleportTeam = true;
			TeleportMonsters = true;
			TeleportItems = true;
			IsVisible = true;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Teleporter (target " + Target + ")");

			return sb.ToString();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override DungeonLocation[] GetTargets()
		{
			DungeonLocation[] target = new DungeonLocation[]
			{
				Target
			};
			return target;
		}
		

		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != Tag)
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "target":
					{
						Target = new DungeonLocation(Square.Location);
						Target.Load(node);
					}
					break;

					case "visible":
					{
						IsVisible = bool.Parse(node.InnerText);
					}
					break;


					case "teleportteam":
					{
						TeleportTeam = bool.Parse(node.InnerText);
					}
					break;

					case "teleportmonsters":
					{
						TeleportMonsters = bool.Parse(node.InnerText);
					}
					break;

					case "teleportitems":
					{
						TeleportItems = bool.Parse(node.InnerText);
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
			

			writer.WriteStartElement(Tag);
			
			base.Save(writer);

			Target.Save("target", writer);

			writer.WriteElementString("teleportteam", TeleportTeam.ToString());
			writer.WriteElementString("teleportmonsters", TeleportMonsters.ToString());
			writer.WriteElementString("teleportitems", TeleportItems.ToString());
			writer.WriteElementString("visible", IsVisible.ToString());

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
			if (!TeleportTeam || Target == null)
				return false;

			return GameScreen.Team.Teleport(Target);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="monster"></param>
		/// <returns></returns>
		public override bool OnMonsterEnter(Monster monster)
		{
			if (!TeleportMonsters || monster == null)
				return false;

			if (Target == null)
				return false;

			monster.Teleport(Target);

			return true;
		}

		
		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string Tag = "teleporter";

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
		/// Does the teleport can be used more than once
		/// </summary>
		public bool Reusable
		{
			get;
			set;
		}


		/// <summary>
		/// Does the teleporter visible
		/// </summary>
		public bool IsVisible
		{
			get;
			set;
		}


		/// <summary>
		/// Can teleport monsters
		/// </summary>
		public bool TeleportMonsters
		{
			get;
			set;
		}


		/// <summary>
		/// Can teleport the team
		/// </summary>
		public bool TeleportTeam
		{
			get;
			set;
		}


		/// <summary>
		/// Can teleport items
		/// </summary>
		public bool TeleportItems
		{
			get;
			set;
		}

/*
		/// <summary>
		/// Does teleporter is active
		/// </summary>
		public bool IsActivated
		{
			get;
			set;
		}
*/


		/// <summary>
		/// Target
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}

		#endregion


	}

}
