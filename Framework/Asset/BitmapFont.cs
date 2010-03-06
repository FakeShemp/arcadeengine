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

using System.Collections.Generic;
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
using System.Text;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Texture font class
	/// </summary>
	/// TODO: A renommer en BitmapFont
	public class BitmapFont : IAsset, IDisposable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BitmapFont()
		{
			GlyphTileset = new TileSet();
			GlyphTileset.Texture = new Texture(OpenTK.Graphics.OpenGL.PixelFormat.LuminanceAlpha);
		//	GlyphTileset.Texture.PixelInternalFormat = PixelInternalFormat.LuminanceAlpha;
			Batch = new Batch(1);
		}



		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}

	

		#region Text drawing

/*
		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="pos">Offset of the text</param>
		/// <param name="text">Text to print</param>
		public void DrawText2(Point pos, string text)
		{
			if (string.IsNullOrEmpty(text))
				return;


			Rectangle rect = new Rectangle(pos, new Size());
			Display.Texture = GlyphTileset.Texture;

			Batch.Clear();
			foreach (char c in text)
			{
				// Get the tile
				Tile tile = GlyphTileset.GetTile(c - GlyphOffset);
				if (tile == null)
					continue;

				// Move the glyph according to its hot spot
				Rectangle tmp = new Rectangle(
					new Point(rect.X - (int)(tile.HotSpot.X * GlyphTileset.Scale.Width), rect.Y - (int)(tile.HotSpot.Y * GlyphTileset.Scale.Height)),
					new Size((int)(tile.Rectangle.Width * GlyphTileset.Scale.Width), (int)(tile.Rectangle.Height * GlyphTileset.Scale.Height)));

				// Add glyph to the batch
				Batch.AddRectangle(tmp, Color, tile.Rectangle);

				// Move to the next glyph
				rect.Offset(tmp.Size.Width + Advance, 0);
			}
			Batch.Apply();


			Display.DrawBatch(Batch, BeginMode.Quads);
		}
*/
		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="pos">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="text">Text to print</param>
		public void DrawText(Point pos, Color color, string text)
		{

			DrawText(new Rectangle(pos, Size.Empty), color, text);
/*
			if (string.IsNullOrEmpty(text))
				return;


			// Encode string to xml
			string msg = "<?xml version=\"1.0\" encoding=\"unicode\" standalone=\"yes\"?><root>" + text + "</root>";
			UnicodeEncoding utf8 = new UnicodeEncoding();
			byte[] buffer = utf8.GetBytes(msg);
			MemoryStream stream = new MemoryStream(buffer);
			XmlTextReader reader = new XmlTextReader(stream);
			reader.WhitespaceHandling = WhitespaceHandling.All;

			Rectangle rect = new Rectangle(pos, new Size());
			Batch.Clear();

			// Extra offset when displaying tile
			int tileoffset = 0;
			try
			{
				// Skip the first tags "<?...?>" and "<root>"
				reader.MoveToContent();

				while (reader.Read())
				{


					switch (reader.NodeType)
					{
						case XmlNodeType.Attribute:
						{
						}
						break;

						case XmlNodeType.Element:
						{
							switch (reader.Name.ToLower())
							{
								case "tile":
								{
									if (TextTileset == null)
										break;

									int id = int.Parse(reader.GetAttribute("id"));
									Tile tile = TextTileset.GetTile(id);
									TextTileset.Draw(id, rect.Location);
									rect.Offset(tile.Size.Width, 0);

									tileoffset = tile.Size.Height - LineHeight;
								}
								break;

								case "br":
								{
									rect.Location = new Point(pos.X, rect.Bottom + LineHeight + tileoffset);
									tileoffset = 0;
								}
								break;
							}
						}
						break;
						case XmlNodeType.EndElement:
						{

						}

						break;

						case XmlNodeType.Text:
						{

							foreach (char c in reader.Value)
							{
								// Get the tile
								Tile tile = GlyphTileset.GetTile(c - GlyphOffset);
								if (tile == null)
									continue;

								// Move the glyph according to its hot spot
								Rectangle tmp = new Rectangle(
									new Point(rect.X - (int)(tile.HotSpot.X * GlyphTileset.Scale.Width), rect.Y - (int)(tile.HotSpot.Y * GlyphTileset.Scale.Height)),
									new Size((int)(tile.Rectangle.Width * GlyphTileset.Scale.Width), (int)(tile.Rectangle.Height * GlyphTileset.Scale.Height)));

								// Add glyph to the batch
								Batch.AddRectangle(tmp, Color, tile.Rectangle);

								// Move to the next glyph
								rect.Offset(tmp.Size.Width + Advance, 0);
							}
						}
						break;

					}

				}
			}
			catch (XmlException e)
			{
			}

			finally
			{
				// Close streams
				reader.Close();
				stream.Close();

				// Draw batch
				Display.Texture = GlyphTileset.Texture;
				Batch.Apply();
				Display.DrawBatch(Batch, BeginMode.Quads);

				Display.DrawRectangle(rect, Color.Red);
			}
*/
		}


		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="pos">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Point pos, Color color, string format, params object[] args)
		{
			DrawText(pos, color, string.Format(format, args));
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with left justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="color">Color</param>
		/// <param name="text">Text to print</param>
		public void DrawText(Rectangle rectangle, Color color, string text)
		{
			DrawText(rectangle, TextJustification.Left, color, text);
		}


		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Rectangle rectangle, Color color, string format, params object[] args)
		{
			DrawText(rectangle, color, string.Format(format, args));
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="color">Text color</param>
		/// <param name="text">Text to print</param>
		public void DrawText(Rectangle rectangle, TextJustification justification, Color color, string text)
		{
			if (string.IsNullOrEmpty(text))
				return;


			// Encode string to xml
			string msg = "<?xml version=\"1.0\" encoding=\"unicode\" standalone=\"yes\"?><root>" + text + "</root>";
			UnicodeEncoding utf8 = new UnicodeEncoding();
			byte[] buffer = utf8.GetBytes(msg);
			MemoryStream stream = new MemoryStream(buffer);
			XmlTextReader reader = new XmlTextReader(stream);
			reader.WhitespaceHandling = WhitespaceHandling.All;


			// Color stack
			Stack<Color> ColorStack = new Stack<Color>();
			ColorStack.Push(color);
			Color currentcolor = color;
			

			Rectangle rect = rectangle; // new Rectangle(pos, new Size());
			Batch.Clear();


			// Extra offset when displaying tile
			int tileoffset = 0;



			try
			{
				// Skip the first tags "<?...?>" and "<root>"
				reader.MoveToContent();

				while (reader.Read())
				{


					switch (reader.NodeType)
					{
						case XmlNodeType.Attribute:
						{
						}
						break;


						#region control tags

						// Special tags
						case XmlNodeType.Element:
						{
							switch (reader.Name.ToLower())
							{
								case "tile":
								{
									if (TextTileset == null)
										break;

									Display.Color = Color.White;

									int id = int.Parse(reader.GetAttribute("id"));
									Tile tile = TextTileset.GetTile(id);
									TextTileset.Draw(id, rect.Location);
									rect.Offset(tile.Size.Width, 0);

									tileoffset = tile.Size.Height - LineHeight;

									Display.Color = currentcolor;
								}
								break;

								case "br":
								{
									rect.X = rectangle.X;
									rect.Y += (int)(LineHeight * GlyphTileset.Scale.Height) + tileoffset;
									tileoffset = 0;
								}
								break;


								// Change the color
								case "color":
								{
									ColorStack.Push(currentcolor);

									currentcolor = Color.FromArgb(int.Parse(reader.GetAttribute("a")),
										int.Parse(reader.GetAttribute("r")),
										int.Parse(reader.GetAttribute("g")),
										int.Parse(reader.GetAttribute("b")));

									Display.Color = currentcolor;
								}
								break;
							}
						}
						break;

						#endregion


						#region closing control tags

						case XmlNodeType.EndElement:
						{
							switch (reader.Name.ToLower())
							{
								case "color":
								{
									currentcolor = ColorStack.Pop();
									Display.Color = currentcolor;
								}
								break;
							}
						}

						break;

						#endregion



						// Raw text
						case XmlNodeType.Text:
						{

							foreach (char c in reader.Value)
							{
								// Get the tile
								Tile tile = GlyphTileset.GetTile(c - GlyphOffset);
								if (tile == null)
									continue;

								// Move the glyph according to its hot spot
								Rectangle tmp = new Rectangle(
									new Point(rect.X - (int)(tile.HotSpot.X * GlyphTileset.Scale.Width), rect.Y - (int)(tile.HotSpot.Y * GlyphTileset.Scale.Height)),
									new Size((int)(tile.Rectangle.Width * GlyphTileset.Scale.Width), (int)(tile.Rectangle.Height * GlyphTileset.Scale.Height)));

								// Out of the bouding box => new line
								if (tmp.Right >= rectangle.Right && !rectangle.Size.IsEmpty)
								{
									tmp.X = rectangle.X;
									tmp.Y = tmp.Y + (int)(LineHeight * GlyphTileset.Scale.Height);

									rect.X = rectangle.X;
									rect.Y += (int)(LineHeight * GlyphTileset.Scale.Height) + tileoffset;
									tileoffset = 0;

								}

								// Add glyph to the batch
								Batch.AddRectangle(tmp, currentcolor, tile.Rectangle);

								// Move to the next glyph
								rect.Offset(tmp.Size.Width + Advance, 0);
							}
						}
						break;

					}

				}
			}
			catch (XmlException e)
			{
			}

			finally
			{
				// Close streams
				reader.Close();
				stream.Close();

				// Draw batch
				Batch.Apply();
				Display.Texture = GlyphTileset.Texture;
				Display.Blending = true;
				Display.DrawBatch(Batch, BeginMode.Quads);

				//Display.DrawRectangle(rectangle, Color.Red);
			}
/*
			Point loc = rectangle.Location;
			Rectangle rect = Rectangle.Empty;

			//Batch.Size = text.Length;
			Display.Texture = GlyphTileset.Texture;


			Batch.Clear();
			foreach (char c in text)
			{
				// New line
				if (c == 10)
				{
					loc.X = rectangle.X;
					loc.Y += (int)(LineHeight * GlyphTileset.Scale.Height);
				}


				// Get the tile
				Tile tile = GlyphTileset.GetTile(c - GlyphOffset);
				if (tile == null)
					continue;


				switch (justification)
				{
					case TextJustification.Left:
					{
						// Move the glyph according to its hot spot
						rect.X = loc.X - (int)(tile.HotSpot.X * GlyphTileset.Scale.Width);
						rect.Y = loc.Y - (int)(tile.HotSpot.Y * GlyphTileset.Scale.Height);
						rect.Width = (int)(tile.Rectangle.Width * GlyphTileset.Scale.Width);
						rect.Height = (int)(tile.Rectangle.Height * GlyphTileset.Scale.Height);


						// Out of the rectangle
						if (rect.Left >= rectangle.Right)
						{
							rect.X = rectangle.X;
							rect.Y = rect.Y + (int)(LineHeight * GlyphTileset.Scale.Height);
							loc.Y += (int)(LineHeight * GlyphTileset.Scale.Height);
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
				Batch.AddRectangle(rect, Color, tile.Rectangle);

				// Move to the next glyph
				loc.X = rect.Right + Advance;
			}
			Batch.Apply();


			Display.DrawBatch(Batch, BeginMode.Quads);


			Display.DrawRectangle(rectangle, Color.Red);
*/
		}

		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Rectangle rectangle, TextJustification justification, Color color, string format, params object[] args)
		{
			DrawText(rectangle, justification, color, string.Format(format, args));
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
		static public BitmapFont CreateFromTTF(string filename, int size, FontStyle style)
		{
			BitmapFont font = new BitmapFont();
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
		static public BitmapFont CreateFromTexture(string name, Size size, Rectangle zone)
		{
			BitmapFont font = new BitmapFont();
			font.LoadFromTexture(name, size, zone);
			return font;
		}


		/// <summary>
		/// Creates a new Font2D from a TileSet
		/// </summary>
		/// <param name="name">Name of the TileSet</param>
		/// <returns></returns>
		static public BitmapFont CreateFromTileSet(string name)
		{
			BitmapFont font = new BitmapFont();
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
			GlyphTileset.Clear();

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
				Tile tile = GlyphTileset.AddTile(i);
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
				//TextRenderer.DrawText(gfx, c, font, pos, Color.White, TextFormatFlags.NoPadding);
				TextRenderer.DrawText(gfx, c, font, pos, Color.White, Color.Transparent, TextFormatFlags.NoPadding);
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
			GlyphTileset.Texture.LoadImage(ms.ToArray());

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
			TileSetName = name;

			if (string.IsNullOrEmpty(name))
				return false;

			GlyphTileset.Load(ResourceManager.GetAsset<TileSet>(name));
			if (GlyphTileset.Count == 0)
				return false;


			// Get the line height
			foreach (int id in GlyphTileset.Tiles)
			{
				Tile tile = GlyphTileset.GetTile(id);
				LineHeight = Math.Max(LineHeight, tile.Size.Height);
			}

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
			GlyphTileset.Clear();

			// Load texture
			GlyphTileset.Texture.LoadImage(name);

			int id = 32;
			for (int y = zone.Top; y < zone.Bottom; y += size.Height)
				for (int x = zone.Left; x < zone.Right; x += size.Width)
				{
					Tile tile = GlyphTileset.AddTile(id++);

					tile.Rectangle = new Rectangle(x, y, size.Width, size.Height);

					LineHeight = Math.Max(LineHeight, tile.Size.Height);
				}

			// Get the line height
			//foreach (int id in GlyphTileset.Tiles)
			//{
			//   Tile tile = GlyphTileset.GetTile(id);
			//}

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


		#region Disposing

		/// <summary>
		/// 
		/// </summary>
		bool disposed;



		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		// Dispose(bool disposing) executes in two distinct scenarios.
		// If disposing equals true, the method has been called directly
		// or indirectly by a user's code. Managed and unmanaged resources
		// can be disposed.
		// If disposing equals false, the method has been called by the
		// runtime from inside the finalizer and you should not reference
		// other objects. Only unmanaged resources can be disposed.
		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				if (Batch != null)
				{
					Batch.Dispose();
					Batch = null;
				}

				// Note disposing has been done.
				disposed = true;
			}
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
				return "bitmapfont";
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
		/// TileSet of the glyphs
		/// </summary>
		[Browsable(false)]
		public TileSet GlyphTileset
		{
			get;
			private set;
		}

		/// <summary>
		/// TileSet for the tiles in text
		/// </summary>
		[Browsable(false)]
		public TileSet TextTileset
		{
			get;
			set;
		}


/*
		/// <summary>
		/// Font Color
		/// </summary>
		[Category("Color")]
		[Description("Color of the font")]
		public Color Color { get; set; }
*/

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
