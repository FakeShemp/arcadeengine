using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;


namespace DungeonEye
{

	/// <summary>
	/// Decoration on wall
	/// </summary>
	public class WallDecoration : IAsset
	{

		/// <summary>
		/// 
		/// </summary>
		public WallDecoration()
		{

		}


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{

			if (xml == null)
				return false;

			if (xml.Name.ToLower() != "walldecoration")
				return false;

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{

					case "tiles":
					{
						TileSetName = node.Attributes["name"].Value;
						Tile = int.Parse(node.Attributes["id"].Value);
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


			writer.WriteStartElement("walldecoration");
			writer.WriteAttributeString("name", Name);

			writer.WriteStartElement("tiles");
			writer.WriteAttributeString("name", TileSetName);
			writer.WriteAttributeString("id", Tile.ToString());
			writer.WriteEndElement();


			writer.WriteEndElement();

			return true;
		}

/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool Save(string filename)
		{
			return false;

		}
*/
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
		/// TileSet of the decoration
		/// </summary>
		[Browsable(false)]
		public TileSet Tileset
		{
			get;
			protected set;
		}


		/// <summary>
		/// Tile id for this decoration
		/// </summary>
		[Category("TileSet")]
		public int Tile
		{
			get;
			set;
		}


		/// <summary>
		/// TileSet name to use for this decoration
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string TileSetName
		{
			get;
			set;
		}


		#endregion
	}
}
