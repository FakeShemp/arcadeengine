using OpenTK.Graphics;
using System.ComponentModel;
using ArcEngine.Asset;
using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Graphic;
using System.Xml;
using System.Drawing;

namespace ArcEngine.Asset
{
	/// <summary>
	/// 
	/// </summary>
	public class TextureFont : IAsset
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TextureFont()
		{
			Color = Color.White;
			TileSet = new TileSet();

			Batch = new Batch();
		}





		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="pos">Offset of the text</param>
		/// <param name="text">Texte to print</param>
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
				Tile tile = TileSet.GetTile(c);
				if (tile == null)
					continue;


				rect.Size = new Size((int)(tile.Rectangle.Width * TileSet.Scale.Width), (int)(tile.Rectangle.Height * TileSet.Scale.Height));


				Batch.Blit(rect, tile.Rectangle, Color);

				rect.Offset(rect.Size.Width + Advance, 0);
			}
			Batch.End();


			Display.DrawBatch(Batch, BeginMode.Quads);

		}


		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			/*
						if (xml == null)
							return false;

						xml.WriteStartElement("ttffont");
						xml.WriteAttributeString("name", Name);


						base.SaveComment(xml);


						xml.WriteStartElement("file");
						xml.WriteAttributeString("name", FileName);
						xml.WriteEndElement();

						xml.WriteStartElement("size");
						xml.WriteAttributeString("value", Size.ToString());
						xml.WriteEndElement();

						xml.WriteStartElement("style");
						xml.WriteAttributeString("value", Style.ToString());
						xml.WriteEndElement();

						xml.WriteStartElement("color");
						xml.WriteAttributeString("a", color.A.ToString());
						xml.WriteAttributeString("r", color.R.ToString());
						xml.WriteAttributeString("g", color.G.ToString());
						xml.WriteAttributeString("b", color.B.ToString());
						xml.WriteEndElement();

						xml.WriteEndElement();
			*/
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
			Size size = new Size();

			foreach (XmlNode node in xml.ChildNodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
					//base.LoadComment(node);
					continue;
				}


				switch (node.Name.ToLower())
				{
					case "texture":
					{
						texture = node.Attributes["filename"].Value;
					}
					break;

					case "size":
					{
						size = new Size(Int32.Parse(node.Attributes["width"].Value), Int32.Parse(node.Attributes["height"].Value));
					}
					break;

					case "rectangle":
					{
						rectangle = new Rectangle(Int32.Parse(node.Attributes["x"].Value), Int32.Parse(node.Attributes["y"].Value),
														Int32.Parse(node.Attributes["width"].Value), Int32.Parse(node.Attributes["height"].Value));
					}
					break;

					case "color":
					{
						Color = Color.FromArgb(Int32.Parse(node.Attributes["a"].Value),
												Int32.Parse(node.Attributes["r"].Value),
												Int32.Parse(node.Attributes["g"].Value),
												Int32.Parse(node.Attributes["b"].Value));
					}
					break;

					case "advance":
					{
						Advance = int.Parse(node.Attributes["value"].Value);
					}
					break;
				}

			}


			// Load texture
			TileSet.Texture.LoadImage(texture);
			TileSet.Scale = new SizeF(2.0f, 2.0f);

			// Build tile set
			TileSet.Clear();
			int id = 32;
			for(int y = rectangle.Top; y < rectangle.Bottom; y += size.Height)
				for (int x = rectangle.Left; x < rectangle.Right; x += size.Width)
				{
					Tile tile = TileSet.AddTile(id++);

					tile.Rectangle = new Rectangle(x, y, size.Width, size.Height);
				}



			return true;
		}

		#endregion


		#region Properties



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
		/// Font name (ie: data/fonts/verdana.ttf)
		/// </summary>
		[Category("Font")]
		[Description("Name of the TTF font")]
		public string Name
		{
			get;
			set;
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
		public Color Color;



		/// <summary>
		/// Horizontal advance between each glyph
		/// </summary>
		public int Advance;
		#endregion
	}
}
