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
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using System.ComponentModel;
using System;
using ArcEngine.Asset;
using ArcEngine.Audio;
using System.Text;

namespace DungeonEye
{

	/// <summary>
	/// Actor containing alcoves
	/// </summary>
	public class AlcoveActor : SquareActor
	{

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="square">Parent square handle</param>
		public AlcoveActor(Square square) : base(square)
		{
			Alcoves = new Alcove[4]
			{
				new Alcove(),
				new Alcove(),
				new Alcove(),
				new Alcove(),
			};
		}


		/// <summary>
		/// Draws all alcoves according to the view point
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		/// <param name="field">Field of view handle</param>
		/// <param name="position">Position in the field of view</param>
		/// <param name="direction">Looking direction</param>
		public override void Draw(SpriteBatch batch, ViewField field, ViewFieldPosition position, CardinalPoint direction)
		{
			if (Square.Maze.Decoration == null)
				return;

			// For each wall side, draws the decoration
			foreach (CardinalPoint side in DisplayCoordinates.DrawingWallSides[(int)position])
			{
				Alcove alcove = GetAlcove(Compass.GetDirectionFromView(direction, side));

				// Get the decoration
				Decoration deco = Square.Maze.Decoration.GetDecoration(alcove.Decoration);
				if (deco == null)
					continue;

				// Draw the decoration
				Square.DrawDecoration(batch, Square.Maze.Decoration, position, deco, side == CardinalPoint.South);


				// Hide items
				if (alcove.HideItems || side != CardinalPoint.South)
					continue;


				// Draw items in the alcove in front of the team
				Point loc = deco.PrepareLocation(position);
				loc.Offset(alcove.ItemLocation);
				foreach (Item item in Square.GetAlcoveItems(direction, side))
				{
					batch.DrawTile(Square.Maze.Dungeon.ItemTileSet, item.GroundTileID, loc,
					    DisplayCoordinates.GetDistantColor(position), 0.0f,
					    DisplayCoordinates.GetScaleFactor(position), SpriteEffects.None, 0.0f);
				}
			}

		}


		/// <summary>
		/// Gets an alcove
		/// </summary>
		/// <param name="side">Wall side</param>
		/// <returns>Alcove handle</returns>
		public Alcove GetAlcove(CardinalPoint side)
		{
			return Alcoves[(int) side];
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Alcove (x)");

			return sb.ToString();
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
					case "side":
					{
						CardinalPoint dir = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.Attributes["name"].Value);
						GetAlcove(dir).Load(node);
					}
					break;

					default:
					{
						Trace.WriteLine("[Alcove] Load() : Unknown node \"" + node.Name + "\" found.");
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

			for (int i = 0; i < 4; i++)
			{			
				writer.WriteStartElement("side");
				writer.WriteAttributeString("name", ((CardinalPoint)i).ToString());
				Alcoves[i].Save(writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string Tag = "alcove";


		/// <summary>
		/// 4 alcoves
		/// </summary>
		Alcove[] Alcoves;

		#endregion
	}
}
