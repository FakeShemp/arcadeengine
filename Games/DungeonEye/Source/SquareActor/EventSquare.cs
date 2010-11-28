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
	/// Event square
	/// </summary>
	public class EventSquare : SquareActor
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square handle</param>
		public EventSquare(Square square) : base(square)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Event";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="team">Team handle</param>
		/// <returns></returns>
		public override bool OnTeamEnter(Team team)
		{
			// Some message to display
			if (!string.IsNullOrEmpty(Message))
			{
				foreach (Hero hero in team.Heroes)
				{
					if (hero == null)
						continue;

					if (hero.SavingThrow(SavingThrowType.Will) > Dice.GetD20(1))
					{
						Team.AddMessage(hero.Name + ": " + Message, MessageColor);
						break;
					}
				}
			}

			return false;
		}


		#region IO

		/// <summary>
		/// Loads the door's definition from a bank
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "messagecolor":
					{
						MessageColor = Color.FromArgb(int.Parse(node.Attributes["value"].Value));
					}
					break;

					case "mustface":
					{
						MustFace = Boolean.Parse(node.Attributes["value"].Value);
					}
					break;

					case "direction":
					{
						Direction = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["value"].Value);
					}
					break;

					case "soundname":
					{
						SoundName = node.Attributes["value"].Value;
					}
					break;

					case "loopsound":
					{
						LoopSound = Boolean.Parse(node.Attributes["value"].Value);
					}
					break;

					case "displaybackground":
					{
						DisplayBackground = Boolean.Parse(node.Attributes["value"].Value);
					}
					break;

					case "message":
					{
						Message = node.Attributes["value"].Value;
					}
					break;

					case "picturename":
					{
						PictureName = node.Attributes["value"].Value;
					}
					break;

					case "intelligence":
					{
						Intelligence = int.Parse(node.Attributes["value"].Value);
					}
					break;


					default:
					{
						Trace.WriteLine("[EventSquare] Load() : Unknown node \"" + node.Name + "\" found.");
					}
					break;
				}
			}



			return true;
		}


		/// <summary>
		/// Saves the door definition
		/// </summary>
		/// <param name="writer">XML writer handle</param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("eventsquare");

			writer.WriteStartElement("messagecolor");
			writer.WriteAttributeString("value", MessageColor.ToArgb().ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("mustface");
			writer.WriteAttributeString("value", MustFace.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("direction");
			writer.WriteAttributeString("value", Direction.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("soundname");
			writer.WriteAttributeString("value", SoundName);
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("loopsound");
			writer.WriteAttributeString("value", LoopSound.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("displaybackground");
			writer.WriteAttributeString("value", DisplayBackground.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("intelligence");
			writer.WriteAttributeString("value", Intelligence.ToString());
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("message");
			writer.WriteAttributeString("value", Message);
			writer.WriteEndElement();

			// 
			writer.WriteStartElement("picturename");
			writer.WriteAttributeString("value", PictureName);
			writer.WriteEndElement();




			writer.WriteEndElement();

			return true;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Team must face a direction
		/// </summary>
		public bool MustFace
		{
			get;
			set;
		}


		/// <summary>
		/// Message color
		/// </summary>
		public Color MessageColor
		{
			get;
			set;
		}

		/// <summary>
		/// Direction Team must face to trigger
		/// </summary>
		public CardinalPoint Direction
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the sound name
		/// </summary>
		public string SoundName
		{
			get;
			set;
		}


		/// <summary>
		/// Loop sound
		/// </summary>
		public bool LoopSound
		{
			get;
			set;
		}



		/// <summary>
		/// Gets or sets the background
		/// </summary>
		public bool DisplayBackground
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets the minimum intelligence needed to trigger the event
		/// </summary>
		public int Intelligence
		{
			get;
			set;
		}


		/// <summary>
		/// Message to display
		/// </summary>
		public string Message
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the picture name
		/// </summary>
		public string PictureName
		{
			get;
			set;
		}

		#endregion

	}
}
