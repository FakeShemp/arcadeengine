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
using DungeonEye.EventScript;


namespace DungeonEye
{
	/// <summary>
	/// Wall switch actor
	/// </summary>
	public class WallSwitch : SquareActor
	{


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Parent square handle</param>
		public WallSwitch(Square square) : base(square)
		{
			ActivatedDecoration = -1;
			DeactivatedDecoration = -1;

			Actions = new List<ScriptAction>();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="side"></param>
		/// <returns></returns>
		public override bool OnClick(Point location, CardinalPoint side)
		{
			if (side != Side)
				return false;

			IsActivated = !IsActivated;

			return false;
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
			DecorationSet decoset = Square.Maze.Decoration;
			if (decoset == null)
				return;

			Decoration deco = decoset.GetDecoration(IsActivated ? ActivatedDecoration : DeactivatedDecoration);
			if (deco == null)
				return;


	//		if (Compass.GetOppositeDirection(direction) == Side)
		//		batch.DrawTile(decoset.Tileset, deco.GetTileId(position), deco.GetLocation(position));
			//batch.DrawTile(decoset.Tileset, deco.GetTileId(position), deco.GetLocation(position),
			//Color.White, 0.0f,
			//deco.GetSwap(position) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
			//0.0f);
	

		}



		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != Tag)
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "decoration":
					{
						ActivatedDecoration = int.Parse(node.Attributes["activated"].Value);
						DeactivatedDecoration = int.Parse(node.Attributes["deactivated"].Value);
					}
					break;

					case "side":
					{
						Side = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), node.InnerText);
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
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement(Tag);

			base.Save(writer);

			writer.WriteStartElement("decoration");
			writer.WriteAttributeString("activated", ActivatedDecoration.ToString());
			writer.WriteAttributeString("deactivated", DeactivatedDecoration.ToString());
			writer.WriteEndElement();

			writer.WriteElementString("side", Side.ToString());

			foreach (ScriptAction action in Actions)
				action.Save(writer);

			writer.WriteEndElement();

			return true;
		}



		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string Tag = "wallswitch";


		/// <summary>
		/// Activated decoration id
		/// </summary>
		public int ActivatedDecoration
		{
			get;
			set;
		}


		/// <summary>
		/// Deactivated decoration id
		/// </summary>
		public int DeactivatedDecoration
		{
			get;
			set;
		}



		/// <summary>
		/// On activate actions
		/// </summary>
		public List<ScriptAction> Actions
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public CardinalPoint Side
		{
			get;
			set;
		}

		#endregion
	}
}
