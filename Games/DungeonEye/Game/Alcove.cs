﻿#region Licence
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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Audio;
using ArcEngine.Graphic;
using DungeonEye.Script;
using DungeonEye.Script.Actions;


namespace DungeonEye
{
	/// <summary>
	/// All informations about an alcove
	/// </summary>
	public class Alcove
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Alcove()
		{
			Decoration = -1;
			HideItems = false;
			ItemLocation = Point.Empty;
			AcceptBigItems = false;
			OnAddedItem = new List<AlcoveScript>();
			OnRemovedItem = new List<AlcoveScript>();
		}



		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Decoration = int.Parse(xml.Attributes["deco"].Value);
			HideItems = bool.Parse(xml.Attributes["hide"].Value);
			AcceptBigItems = bool.Parse(xml.Attributes["bigitems"].Value);
			ItemLocation = new Point(int.Parse(xml.Attributes["x"].Value), 
									 int.Parse(xml.Attributes["y"].Value));

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "onaddeditem":
					{
					}
					break;

					case "onremoveditem":
					{
					}
					break;

					default:
					{
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
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteAttributeString("deco", Decoration.ToString());
			writer.WriteAttributeString("hide", HideItems.ToString());
			writer.WriteAttributeString("bigitems", AcceptBigItems.ToString());
			writer.WriteAttributeString("x", ItemLocation.X.ToString());
			writer.WriteAttributeString("y", ItemLocation.Y.ToString());


			if (OnAddedItem.Count > 0)
			{
				writer.WriteStartElement("onaddeditem");
				foreach (AlcoveScript action in OnAddedItem)
					action.Save(writer);

				writer.WriteEndElement();
			}


			if (OnRemovedItem.Count > 0)
			{
				writer.WriteStartElement("onremoveditem");
				foreach (AlcoveScript action in OnRemovedItem)
					action.Save(writer);

				writer.WriteEndElement();
			}

			return true;
		}



		#endregion


		#region Properties

		/// <summary>
		/// Decoration id
		/// </summary>
		public int Decoration;

		/// <summary>
		/// Hide items in the alcove
		/// </summary>
		public bool HideItems;


		/// <summary>
		/// Items location on the screen
		/// </summary>
		public Point ItemLocation;


		/// <summary>
		/// Accept big items
		/// </summary>
		public bool AcceptBigItems;


		/// <summary>
		/// Scripts to execute when an item is added
		/// </summary>
		public List<AlcoveScript> OnAddedItem
		{
			get;
			private set;
		}


		/// <summary>
		/// Scripts to execute when an item is removed
		/// </summary>
		public List<AlcoveScript> OnRemovedItem
		{
			get;
			private set;
		}

		#endregion
	}
}
