using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine.Graphic;
using System.Xml;
using System.ComponentModel;
using ArcEngine.Asset;



namespace ArcEngine.GUI
{


	/// <summary>
	/// A GUI button
	/// </summary>
	public class Button : GuiBase
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
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			base.Update(time);


		}



		/// <summary>
		/// Draws the button
		/// </summary>
		public override void Draw()
		{
			base.Draw();
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
