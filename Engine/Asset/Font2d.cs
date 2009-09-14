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

using OpenTK.Graphics;
using System.ComponentModel;
using ArcEngine.Asset;
using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Graphic;
using System.Xml;
using System.Drawing;
using OpenTK.Graphics.OpenGL;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Texture font class
	/// </summary>
	public class Font2d : IAsset
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Font2d()
		{
			Color = Color.White;
			TileSet = new TileSet();
			Batch = new Batch();
		}



		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="text">Texte to print</param>
		/// <param name="pos">Offset of the text</param>
		public void DrawText(string text, Point pos)
		{
			if (string.IsNullOrEmpty(text))
				return;


			Rectangle rect = new Rectangle(pos, new Size());

			Batch.Size = text.Length;
			Display.Texture = TileSet.Texture;

			Batch.Begin();
			foreach (char c in text)
			{
				// Get the tile
				Tile tile = TileSet.GetTile(c - GlyphOffset);
				if (tile == null)
					continue;

				// Move the glyph according to its hot spot
				Rectangle tmp = new Rectangle(
					new Point(rect.X - (int)(tile.HotSpot.X * TileSet.Scale.Width), rect.Y - (int)(tile.HotSpot.Y * TileSet.Scale.Height)), 
					new Size((int)(tile.Rectangle.Width * TileSet.Scale.Width), (int)(tile.Rectangle.Height * TileSet.Scale.Height)));

				// Add glyph to the batch
				Batch.Blit(tmp, tile.Rectangle, Color);

				// Move to the next glyph
				rect.Offset(tmp.Size.Width + Advance, 0);
			}
			Batch.End();


			Display.DrawBatch(Batch, BeginMode.Quads);
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with left justification
		/// </summary>
		/// <param name="text">Text to print</param>
		/// <param name="rectangle">Rectangle of the text</param>
		public void DrawText(string text, Rectangle rectangle)
		{
			DrawText(text, rectangle, TextJustification.Left);
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="text">Text to print</param>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		public void DrawText(string text, Rectangle rectangle, TextJustification justification)
		{
			if (string.IsNullOrEmpty(text))
				return;

			Point loc = rectangle.Location;
			Rectangle rect = Rectangle.Empty;

			Batch.Size = text.Length;
			Display.Texture = TileSet.Texture;


			Batch.Begin();
			foreach (char c in text)
			{
				// New line
				if (c ==  10)
				{
					loc.X = rectangle.X;
					loc.Y += (int)(14 * TileSet.Scale.Height);
				}


				// Get the tile
				Tile tile = TileSet.GetTile(c - GlyphOffset);
				if (tile == null)
					continue;


				switch (justification)
				{
					case TextJustification.Left:
					{
						// Move the glyph according to its hot spot
						rect.X = loc.X - (int)(tile.HotSpot.X * TileSet.Scale.Width);
						rect.Y = loc.Y - (int)(tile.HotSpot.Y * TileSet.Scale.Height);
						rect.Width = (int)(tile.Rectangle.Width * TileSet.Scale.Width);
						rect.Height = (int)(tile.Rectangle.Height * TileSet.Scale.Height);


						// Out of the rectangle
						if (rect.Left > rectangle.Right)
						{
							rect.X = rectangle.X;
							rect.Y = rect.Y + (int) (14 * TileSet.Scale.Height);
							loc.Y += (int)(14 * TileSet.Scale.Height);
						}
					}
					break;
					case TextJustification.Center:
					{
					}
					break;
					case TextJustification.Right:
					{
					}
					break;
					case TextJustification.Justify:
					{
					}
					break;
				}


				// Add glyph to the batch
				Batch.Blit(rect, tile.Rectangle, Color);

				// Move to the next glyph
				loc.X = rect.Right + Advance;
			}
			Batch.End();


			Display.DrawBatch(Batch, BeginMode.Quads);
		}



		/// <summary>
		/// Loads a TrueType Font
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <param name="size">Size of the font</param>
		/// <returns></returns>
		public bool LoadTTF(string filename, int size)
		{
			TileSetName = string.Empty;
			TextureName = string.Empty;

			if (string.IsNullOrEmpty(filename))
				return false;

			return false;
		}


		/// <summary>
		/// Loads a TileSet
		/// </summary>
		/// <param name="name">Name of the TileSet</param>
		/// <returns></returns>
		public bool LoadFromTileSet(string name)
		{
			TTFFileName = string.Empty;
			TextureName = string.Empty;

			if (string.IsNullOrEmpty(name))
				return false;

			TileSetName = name;
			TileSet.Load(ResourceManager.GetAsset<TileSet>(name));
			if (TileSet.Count == 0)
				return false;

			return true;
		}


		/// <summary>
		/// Loads a Texture
		/// </summary>
		/// <param name="name">Name of the texture</param>
		/// <param name="size">Size of each glyph</param>
		/// <param name="zone">Zone in the texture</param>
		/// <returns></returns>
		public bool LoadFromTexture(string name, Size size, Rectangle zone)
		{

			TTFFileName = string.Empty;
			TileSetName = string.Empty;

			if (string.IsNullOrEmpty(name))
				return false;


			return false;
		}


		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			xml.WriteStartElement(XmlTag);
			xml.WriteAttributeString("name", Name);



			if (!string.IsNullOrEmpty(TextureName))
			{
			}
			else if (!string.IsNullOrEmpty(TTFFileName))
			{
			}
			else if (!string.IsNullOrEmpty(TileSetName))
			{
				xml.WriteStartElement("tileset");
				xml.WriteAttributeString("name", TileSetName);
				xml.WriteEndElement();
			}


			xml.WriteStartElement("offset");
			xml.WriteAttributeString("value", GlyphOffset.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("advance");
			xml.WriteAttributeString("value", Advance.ToString());
			xml.WriteEndElement();

			xml.WriteEndElement();
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			string texture = string.Empty;
			Rectangle rectangle = Rectangle.Empty;

			foreach (XmlNode node in xml.ChildNodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "texture":
					{
			//			Size = new Size(Int32.Parse(node.Attributes["width"].Value), Int32.Parse(node.Attributes["height"].Value));
						//rectangle = new Rectangle(Int32.Parse(node.Attributes["x"].Value), Int32.Parse(node.Attributes["y"].Value),
						//                                Int32.Parse(node.Attributes["width"].Value), Int32.Parse(node.Attributes["height"].Value));
						LoadFromTexture(node.Attributes["filename"].Value, Size.Empty, Rectangle.Empty);
					}
					break;

					case "tileset":
					{
						LoadFromTileSet(node.Attributes["name"].Value);
					}
					break;

					case "advance":
					{
						Advance = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "offset":
					{
						GlyphOffset = int.Parse(node.Attributes["value"].Value);
					}
					break;
				}

			}

/*
			// Load texture
			TileSet.LoadTexture(texture);

			// Build tile set
			TileSet.Clear();
			int id = 32;
			for(int y = rectangle.Top; y < rectangle.Bottom; y += Size.Height)
				for (int x = rectangle.Left; x < rectangle.Right; x += Size.Width)
				{
					Tile tile = TileSet.AddTile(id++);

					tile.Rectangle = new Rectangle(x, y, Size.Width, Size.Height);
				}


*/
			return true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Font name (ie: data/fonts/verdana.ttf)
		/// </summary>
		[Category("Font")]
		[Description("Name of the TTF font")]
		public string Name { get; set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "texturefont";
			}
		}


		#region From TileSet

		/// <summary>
		/// Name of the TileSet to use
		/// </summary>
		string TileSetName;

		#endregion

		#region From TrueTypeFont

		/// <summary>
		/// Name of the TileSet to use
		/// </summary>
		string TTFFileName;

		#endregion

		#region From Texture

		/// <summary>
		/// Name of the TileSet to use
		/// </summary>
		string TextureName;

		#endregion

/*
		/// <summary>
		/// Gets/sets the rendering style of the font
		/// </summary>
		[Category("Font")]
		[Description("Style of the font")]
		public FontStyle Style
		{
			get;
			protected set;
		}


		/// <summary>
		/// Size of the font
		/// </summary>
		[Category("Font")]
		[Description("Size of the caracters")]
		public int Size
		{
			get;
			protected set;

		}


		/// <summary>
		/// Height of a line of text
		/// </summary>
		[Category("Font")]
		[Description("Height of a line of text in pixel")]
		public int LineHeight
		{
			get;
			protected set;
		}
*/
		/// <summary>
		/// Size of a glyph
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Drawing batch
		/// </summary>
		Batch Batch;


		/// <summary>
		/// TileSet of the font
		/// </summary>
		[Browsable(false)]
		public TileSet TileSet
		{
			get;
			private set;
		}

		/// <summary>
		/// Font Color
		/// </summary>
		[Category("Color")]
		[Description("Color of the font")]
		public Color Color { get; set; }


		/// <summary>
		/// Horizontal advance between each glyph
		/// </summary>
		public int Advance { get; set; }


		/// <summary>
		/// Glyph offset in the ASCII table
		/// </summary>
		public int GlyphOffset { get; set; }

		#endregion
	}


	/// <summary>
	/// Text justification
	/// </summary>
	public enum TextJustification
	{
		/// <summary>
		/// 
		/// </summary>
		Left,

		/// <summary>
		/// 
		/// </summary>
		Center,

		/// <summary>
		/// 
		/// </summary>
		Right,

		/// <summary>
		/// 
		/// </summary>
		Justify
	}
}
