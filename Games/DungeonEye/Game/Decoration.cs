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
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Interface;


namespace DungeonEye
{

	/// <summary>
	/// Decoration
	/// </summary>
	public class Decoration : IAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Decoration()
		{
			Decorations = new List<DecorationInfo>();
			IsDisposed = false;
		}



		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			Tileset = ResourceManager.LockSharedAsset<TileSet>(TileSetName);

			return true;
		}



		/// <summary>
		/// Disposes resources
		/// </summary>
		public void Dispose()
		{
			if (Tileset != null)
				ResourceManager.UnlockSharedAsset<TileSet>(Tileset);
			Tileset = null;

			Decorations = null;
			IsDisposed = true;
		}


		/// <summary>
		/// Gets a decoration description
		/// </summary>
		/// <param name="id">Decoration number</param>
		/// <returns>Decoration information or null</returns>
		public DecorationInfo GetDecoration(int id)
		{
			if (id > Decorations.Count - 1)
				return null;

			DecorationInfo deco = Decorations[id];

			if (Tileset != null)
				deco.Tile = Tileset.GetTile(deco.TileId);

			return deco;
		}



		/// <summary>
		/// Get screen location of a decoration
		/// </summary>
		/// <param name="id">Decoration id</param>
		/// <returns>On screen location</returns>
		public Point GetLocation(int id)
		{
			Point point = Point.Empty;

			return point;
		}




		/// <summary>
		/// Get Tile handle a decoration
		/// </summary>
		/// <param name="id">Decoration id</param>
		/// <returns>Tile handle</returns>
		public Tile GetTile(int id)
		{
			Tile tile = null;

			return tile;
		}



		/// <summary>
		/// Loads a TileSet
		/// </summary>
		/// <param name="name">TileSet name</param>
		/// <returns>True on success</returns>
		public bool LoadTileSet(string name)
		{
			TileSetName = name;

			if (string.IsNullOrEmpty(name))
				return false;

			Tileset = ResourceManager.CreateAsset<TileSet>(TileSetName);

			return Tileset != null;
		}


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{

			if (xml == null)
				return false;

			if (xml.Name != XmlTag)
				return false;

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						LoadTileSet(node.Attributes["name"].Value);
					}
					break;

					case "deco":
					{
						DecorationInfo deco = new DecorationInfo();
						deco.TileId = int.Parse(node.Attributes["id"].Value);
						deco.Location = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
					}
					break;

					case "editor":
					{
						BackgroundTileset = node.Attributes["tileset"].Value;
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


			writer.WriteStartElement(XmlTag);
			writer.WriteAttributeString("name", Name);

			writer.WriteStartElement("tileset");
			writer.WriteAttributeString("name", TileSetName);
			writer.WriteEndElement();

			writer.WriteStartElement("editor");
			writer.WriteAttributeString("tileset", BackgroundTileset);
			writer.WriteEndElement();

			foreach (DecorationInfo deco in Decorations)
			{
				writer.WriteStartElement("deco");
				writer.WriteAttributeString("id", deco.TileId.ToString());
				writer.WriteAttributeString("x", deco.Location.X.ToString());
				writer.WriteAttributeString("y", deco.Location.Y.ToString());
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			return true;
		}


		#endregion


		#region properties


		/// <summary>
		/// Name of the decoration
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Is asset disposed
		/// </summary>
		public bool IsDisposed { get; private set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "decoration";
			}
		}


		/// <summary>
		/// TileSet of the decoration
		/// </summary>
		public TileSet Tileset
		{
			get;
			protected set;
		}


		/// <summary>
		/// TileSet name to use for this decoration
		/// </summary>
		public string TileSetName
		{
			get;
			set;
		}



		/// <summary>
		/// List of decorations
		/// </summary>
		List<DecorationInfo> Decorations;


		/// <summary>
		/// Name of the tileset to use for the editor
		/// </summary>
		internal string BackgroundTileset
		{
			get;
			set;
		}

		#endregion
	}


}
