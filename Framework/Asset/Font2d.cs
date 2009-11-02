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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Graphic;
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


		#region Text drawing


		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="pos">Offset of the text</param>
		/// <param name="text">Text to print</param>
		public void DrawText(Point pos, string text)
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
		/// Prints some text on the screen
		/// </summary>
		/// <param name="pos">Offset of the text</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Point pos, string format, params object[] args)
		{
			DrawText(pos, string.Format(format, args));
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with left justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="text">Text to print</param>
		public void DrawText(Rectangle rectangle, string text)
		{
			DrawText(rectangle, TextJustification.Left, text);
		}


		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Rectangle rectangle, string format, params object[] args)
		{
			DrawText(rectangle, string.Format(format, args));
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="text">Text to print</param>
		public void DrawText(Rectangle rectangle, TextJustification justification, string text)
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
				if (c == 10)
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
							rect.Y = rect.Y + (int)(14 * TileSet.Scale.Height);
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
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Rectangle rectangle, TextJustification justification, string format, params object[] args)
		{
			DrawText(rectangle, justification, string.Format(format, args));
		}

		#endregion


		#region Statics


		/// <summary>
		/// Creates a new Font2D from a TTF file
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <param name="size">Size of the font</param>
		/// <param name="style">Style of the font</param>
		/// <returns></returns>
		static public Font2d CreateFromTTF(string filename, int size, FontStyle style)
		{
			Font2d font = new Font2d();
			font.LoadTTF(filename, size, style);
			return font;
		}


		/// <summary>
		/// Creates a new Font2D from a texture
		/// </summary>
		/// <param name="name">Name of the texture</param>
		/// <param name="size">Size of each glyph</param>
		/// <param name="zone">Zone in the texture</param>
		/// <returns></returns>
		static public Font2d CreateFromTexture(string name, Size size, Rectangle zone)
		{
			Font2d font = new Font2d();
			font.LoadFromTexture(name, size, zone);
			return font;
		}


		/// <summary>
		/// Creates a new Font2D from a TileSet
		/// </summary>
		/// <param name="name">Name of the TileSet</param>
		/// <returns></returns>
		static public Font2d CreateFromTileSet(string name)
		{
			Font2d font = new Font2d();
			font.LoadFromTileSet(name);
			return font;
		}





		#endregion


		/// <summary>
		/// Loads a TrueType Font
		/// </summary>
		/// <param name="filename">Filename</param>
		/// <param name="size">Size of the font</param>
		/// <param name="style">Style of the font</param>
		/// <returns></returns>
		public bool LoadTTF(string filename, int size, FontStyle style)
		{
			TileSetName = string.Empty;
			TextureName = string.Empty;

			if (string.IsNullOrEmpty(filename))
				return false;

			TTFFileName = filename;
			TTFSize = size;
			TTFStyle = style;


			// Clear TileSet
			TileSet.Clear();

			// Open the font
			Stream data = ResourceManager.LoadResource(filename);
			if (data == null)
			{
				Trace.WriteLine("Can't open TTF Font \"{0}\"", filename);
				return false;
			}

			// GdiFont
			return Generate(new Font(LoadFontFamily(data), size, style));
		
		}



		/// <summary>
		/// Internal font generation
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Graphics.MeasureCharacterRanges 
		/// TextRenderer.MeasureText 
		/// Graphics.MeasureString 
		/// 
		/// </remarks>
		bool Generate(Font font)
		{
			LineHeight = font.Height;
			//	IsGenerated = false;

			// Final Bitmap
			Bitmap bm = new Bitmap(512, LineHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Bitmap tmpbm = null;

			// Offset of the glyph in the texture
			Point pos = Point.Empty;

			// Graphics device
			System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(bm);
			gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
			gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

			SolidBrush brush = new SolidBrush(Color.White);

			// Get informations for each glyph
			for (byte i = 0; i < 255; i++)
			{
				string c = Convert.ToChar(Convert.ToInt32(i)).ToString();


				// Add the tile to the TileSet
				Tile tile = TileSet.AddTile(i);
				tile.Rectangle = new Rectangle(pos, TextRenderer.MeasureText(gfx, c, font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding | TextFormatFlags.ExternalLeading));


				// Check if enough space left
				if (pos.X + tile.Rectangle.Width >= bm.Width)
				{
					// Relocate the location
					pos.X = 0;
					pos.Y += LineHeight;

					// Resize the bitmap
					tmpbm = new Bitmap(bm.Width, bm.Height + LineHeight);
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(tmpbm);
					g.DrawImage(bm, Point.Empty);
					bm = tmpbm;
					gfx = System.Drawing.Graphics.FromImage(bm);
					gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
					gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

					// Relocate the tile rectangle
					tile.Rectangle = new Rectangle(pos, tile.Rectangle.Size);
				}


				// Draw the glyph to the texture
				TextRenderer.DrawText(gfx, c, font, pos, Color.White, TextFormatFlags.NoPadding);
				//gfx.DrawString(c, font, brush, pos);

				// Offset of the new glyph in the texture
				pos.X += tile.Rectangle.Width;

				//bm.Save(i + ".png", ImageFormat.Png);
			}

			// Close the font
			font.Dispose();
			gfx.Dispose();


			// Check the texture size (power of two size)
			int powof2 = 1;
			while (powof2 < bm.Height) powof2 <<= 1;
			tmpbm = new Bitmap(bm.Width, powof2);
			gfx = System.Drawing.Graphics.FromImage(tmpbm);
			gfx.DrawImage(bm, Point.Empty);
			bm = tmpbm;


			// Save the image to the texture
			MemoryStream ms = new MemoryStream();
			bm.Save(ms, ImageFormat.Png);
			TileSet.Texture.LoadImage(ms.ToArray());

			//	bm.Save("final.png", ImageFormat.Png);

			//IsGenerated = true;
			return true;
		}



		/// <summary>
		/// Loads font family from byte array
		/// </summary>
		/// <param name="buffer">Memory of the font</param>
		/// <returns></returns>
		private FontFamily LoadFontFamily(Stream buffer)
		{
			byte[] data = new byte[buffer.Length];
			buffer.Read(data, 0, (int)buffer.Length);


			var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			var pfc = new PrivateFontCollection();

			try
			{
				var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
				pfc.AddMemoryFont(ptr, data.Length);
			}
			finally
			{
				handle.Free();
			}

			return pfc.Families[0];
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
		/// Loads a texture from a XmlNode
		/// </summary>
		/// <param name="xml">Node</param>
		/// <returns></returns>
		bool LoadFromTexture(XmlNode xml)
		{
			if (xml == null)
				return false;

			string name = xml.Attributes["filename"].Value;
			Size size = Size.Empty;
			Rectangle rectangle = Rectangle.Empty;


			foreach (XmlNode node in xml.ChildNodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "rectangle":
					{
						rectangle = new Rectangle(Int32.Parse(node.Attributes["x"].Value), Int32.Parse(node.Attributes["y"].Value),
															Int32.Parse(node.Attributes["width"].Value), Int32.Parse(node.Attributes["height"].Value));
					}
					break;
					case "size":
					{
						size = new Size(Int32.Parse(node.Attributes["width"].Value), Int32.Parse(node.Attributes["height"].Value));
					}
					break;
				}
			}

			return LoadFromTexture(name, size, rectangle);

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


			// Build tile set
			TileSet.Clear();

			// Load texture
			TileSet.Texture.LoadImage(name);

			int id = 32;
			for (int y = zone.Top; y < zone.Bottom; y += size.Height)
				for (int x = zone.Left; x < zone.Right; x += size.Width)
				{
					Tile tile = TileSet.AddTile(id++);

					tile.Rectangle = new Rectangle(x, y, size.Width, size.Height);
				}


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
						LoadFromTexture(node);
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

					case "ttf":
					{
						LoadTTF(node.Attributes["filename"].Value, int.Parse(node.Attributes["size"].Value), (FontStyle)(Enum.Parse(typeof(FontStyle), node.Attributes["style"].Value)));
					}
					break;
				}

			}

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

		/// <summary>
		/// Size of the ttf
		/// </summary>
		int TTFSize;


		/// <summary>
		/// Style of the TTF
		/// </summary>
		FontStyle TTFStyle;

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

*/
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
