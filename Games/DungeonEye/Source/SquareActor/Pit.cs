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
using System.ComponentModel;
using ArcEngine.Asset;
using ArcEngine.Graphic;


namespace DungeonEye
{
	/// <summary>
	/// Pit class
	/// </summary>
	public class Pit : SquareActor
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public Pit(Square block) : base(block)
		{
			if (block == null)
				throw new ArgumentNullException("block");

			Damage = new Dice();
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

			TileDrawing td = MazeDisplayCoordinates.GetPit(position);
			if (td != null && !IsHidden)
				batch.DrawTile(TileSet, td.ID, td.Location, Color.White, 0.0f, td.Effect, 0.0f);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Pit (");
			if (Target != null)
				sb.Append(Target);

			if (Damage != null)
				sb.Append(" " + Damage);
			
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

			if (xml.Name.ToLower() != "pit")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "target":
					{
						if (Target == null)
							Target = new DungeonLocation(Square.Location);

						Target.Load(node);
					}
					break;

					case "damage":
					{
						Damage.Load(node);
					}
					break;

					case "hidden":
					{
						IsHidden = bool.Parse(node.Attributes["value"].Value);
					}
					break;

					case "difficulty":
					{
						Difficulty = int.Parse(node.Attributes["value"].Value);
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


			writer.WriteStartElement("pit");

			if (Target != null)
				Target.Save("target", writer);

			writer.WriteStartElement("hidden");
			writer.WriteAttributeString("value", IsHidden.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("difficulty");
			writer.WriteAttributeString("value", Difficulty.ToString());
			writer.WriteEndElement();

			Damage.Save("damage", writer);


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

			if (Target == null)
				return false;

			if (team.Teleport(Target))
				team.Damage(Damage, SavingThrowType.Reflex, Difficulty);

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

			if (Target == null)
				return false;

			monster.Teleport(Target);
			monster.Damage(Damage, SavingThrowType.Reflex, Difficulty);

			return true;
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

				return Square.Maze.OverlayTileset;
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
		/// Target of the pit
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
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
		/// An illusion pit
		/// </summary>
		public bool IsIllusion
		{
			get
			{
				return Target == null;
			}
		}


		/// <summary>
		/// A hidden pit
		/// </summary>
		public bool IsHidden
		{
			get;
			set;
		}


		/// <summary>
		/// Difficulty Class
		/// </summary>
		public int Difficulty
		{
			get;
			set;
		}


		/// <summary>
		/// Damage
		/// </summary>
		public Dice Damage
		{
			get;
			set;
		}

		#endregion


	}
}
