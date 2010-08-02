#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using ArcEngine.Graphic;
using System.Xml;
using System.ComponentModel;
using ArcEngine.Asset;



namespace ArcEngine.Utility.GUI
{


	/// <summary>
	/// Represents a Windows button control.
	/// </summary>
	public class Button : ButtonBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Button()
		{


			// Default button size
			Size = new Size(32, 32);
		}




		#region IO routines

		///
		///<summary>
		/// Save the button to a xml node
		/// </summary>
		///
		public override bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			xml.WriteStartElement("button");



			//xml.WriteStartElement("tileset");
			//xml.WriteAttributeString("name", TileSetName);
			//xml.WriteAttributeString("BufferID", TileID.ToString());
			//xml.WriteEndElement();

			//xml.WriteStartElement("texturelayout");
			//xml.WriteAttributeString("name", TextureLayout.ToString());
			//xml.WriteEndElement();



			base.Save(xml);


			xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads the button from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					// Unknown element, give it to the base
					default:
					{
						base.Load(node);
					}
					break;


					//case "tileset":
					//{
					//   TileSetName = node.Attributes["name"].Value;
					//   TileID = int.Parse(node.Attributes["BufferID"].Value.ToString());
					//}
					//break;
				}
			}

			return true;
		}

		#endregion



		#region Methods


		/// <summary>
		/// 
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="time"></param>
		public override void Update(GuiManager manager, GameTime time)
		{
			base.Update(manager, time);


		}



		/// <summary>
		/// Draws the button
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="batch">SpriteBatch to use</param>
		public override void Draw(GuiManager manager, SpriteBatch batch)
		{
			base.Draw(manager, batch);
/*
			// Background color
			if (BgColor.A > 0)
			{
				device.Color = BgColor;
				device.Rectangle(Rectangle, true);
			}


			// Tile set ?
			if (TileSet != null)
			{
				device.Color = Color;

				TileSet.Draw(TileID, Rectangle, TextureLayout);
			}


			device.Color = Color.White;
*/ 
		}

/*
		/// <summary>
		/// Resize the button to fit the background texture size
		/// </summary>
		public void ResizeToFitTexture()
		{
			if (Tile == null)
				return;

			Size = Tile.Size;
		}

*/

		#endregion



		#region Properties


/*
		/// <summary>
		/// Gets/Sets the TileSet name to use
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		public string TileSetName
		{
			get
			{
				return tileSetName;
			}
			set
			{
				tileSetName = value;

				tileSet = ResourceManager.CreateAsset<TileSet>(value);
			}
		}
		string tileSetName;

         

		/// <summary>
		/// Gets the TileSet to use
		/// </summary>
		[Browsable(false)]
		public TileSet TileSet
		{
			get
			{
				return tileSet;
			}
		}
		TileSet tileSet;



		/// <summary>
		/// TileID in the TileSet to use
		/// </summary>
		public int TileID
		{
			get;
			set;
		}


		/// <summary>
		/// Gets the used tile
		/// </summary>
		public Tile Tile
		{
			get
			{
				if (TileSet == null)
					return null;

				return TileSet.GetTile(TileID);

			}
		}


		/// <summary>
		/// Texture behaviour
		/// </summary>
		[TypeConverter(typeof(TextureLayout))]
		public TextureLayout TextureLayout
		{
			get
			{
				return textureLayout;
			}
			set
			{
				textureLayout = value;
			}
		}
		TextureLayout textureLayout;
 
   */



		/// <summary>
		/// Text to print
		/// </summary>
		public string Text;



		#endregion

	}
}
