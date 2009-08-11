using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;



namespace ArcEngine.Asset
{
	/// <summary>
	///  Collection of Tile
	/// </summary>
	/// <remarks>
	/// Tile texture flipping : http://www.gamedev.net/community/forums/topic.asp?topic_id=526339
	/// </remarks>
	public class TileSet : IAsset
	{

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public TileSet() 
		{
			Scale = new SizeF(1.0f, 1.0f);
			tiles = new Dictionary<int, Tile>();

			Texture = new Texture();

			// Ca fait tout planter
		//	Batch = Game.Device.CreateBatch();
		}


		#endregion


		#region	IO operations


		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;


			xml.WriteStartElement("tileset");
			xml.WriteAttributeString("name", Name);



			if (textureName != null)
			{
				xml.WriteStartElement("texture");
				xml.WriteAttributeString("file", textureName);
				xml.WriteEndElement();
			}

			// Loops throughs tile
			foreach(KeyValuePair<int, Tile> val in tiles)
			{
				xml.WriteStartElement("tile");

				xml.WriteAttributeString("id", val.Key.ToString());

				xml.WriteStartElement("rectangle");
				xml.WriteAttributeString("x", val.Value.Rectangle.X.ToString());
				xml.WriteAttributeString("y", val.Value.Rectangle.Y.ToString());
				xml.WriteAttributeString("width", val.Value.Rectangle.Width.ToString());
				xml.WriteAttributeString("height", val.Value.Rectangle.Height.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("hotspot");
				xml.WriteAttributeString("x", val.Value.HotSpot.X.ToString());
				xml.WriteAttributeString("y", val.Value.HotSpot.Y.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("collisionbox");
				xml.WriteAttributeString("x", val.Value.CollisionBox.X.ToString());
				xml.WriteAttributeString("y", val.Value.CollisionBox.Y.ToString());
				xml.WriteAttributeString("width", val.Value.CollisionBox.Width.ToString());
				xml.WriteAttributeString("height", val.Value.CollisionBox.Height.ToString());
				xml.WriteEndElement();

				xml.WriteEndElement();
			}


			xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads the collection from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
					//base.LoadComment(node);
					continue;
				}


				switch (node.Name.ToLower())
				{
					// Texture
					case "texture":
					{
						TextureName = node.Attributes["file"].Value;
					}
					break;



					// Found a tile to add
					case "tile":
					{
						Tile tile = new Tile();
						int tileid = Int32.Parse(node.Attributes["id"].Value);


						foreach (XmlNode subnode in node)
						{
							switch (subnode.Name.ToLower())
							{
								case "rectangle":
								{
									Rectangle rect = Rectangle.Empty;
									rect.X = Int32.Parse(subnode.Attributes["x"].Value);
									rect.Y = Int32.Parse(subnode.Attributes["y"].Value);
									rect.Width = Int32.Parse(subnode.Attributes["width"].Value);
									rect.Height = Int32.Parse(subnode.Attributes["height"].Value);

									tile.Rectangle = rect;
								}
								break;

								case "collisionbox":
								{
									Rectangle rect = Rectangle.Empty;
									rect.X = Int32.Parse(subnode.Attributes["x"].Value);
									rect.Y = Int32.Parse(subnode.Attributes["y"].Value);
									rect.Width = Int32.Parse(subnode.Attributes["width"].Value);
									rect.Height = Int32.Parse(subnode.Attributes["height"].Value);

									tile.CollisionBox = rect;
								}
								break;

								case "hotspot":
								{
									Point hotspot = Point.Empty;
									hotspot.X = int.Parse(subnode.Attributes["x"].Value);
									hotspot.Y = int.Parse(subnode.Attributes["y"].Value);

									tile.HotSpot = hotspot;
								}
								break;

								default:
								{
									//Log.Send(new LogEventArgs(LogLevel.Warning, "TileSet : Unknown node element found (" + node.Name + ")", null));
									Trace.WriteLine("TileSet : Unknown node element found (" + node.Name + ")");

								}
								break;
							}
						}



						// Add the tile
						tiles[tileid] = tile;


					}
					break;

					// Auto generate tiles
					case "generate":
					{
						int width = Int32.Parse(node.Attributes["width"].Value);
						int height = Int32.Parse(node.Attributes["height"].Value);
						int start = Int32.Parse(node.Attributes["start"].Value);

						int x = 0;
						int y = 0;
						for (y = 0; y < Texture.Size.Height; y += height)
							for (x = 0; x < Texture.Size.Width; x += width)
							{
								tiles[start++] = new Tile(new Rectangle(x, y, width, height), Point.Empty);
							}
					}
					break;

					default:
					{
						//base.Load(node);
					}
					break;
				}
			}

			return true;
		}

		#endregion



		#region Tiles management

		/// <summary>
		/// Add a new tile to the bank
		/// </summary>
		///<param name="id"></param>
		public Tile AddTile(int id) 
		{
			Tile tile = new Tile();
			tiles[id] = tile;

			return tile;
		}

/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rectangle"></param>
		/// <returns></returns>
		public int AddTile(Rectangle rectangle)
		{
			
			return -1;
		}

*/
		/// <summary>
		/// Returns a list of all availble tiles
		/// </summary>
		/// <returns></returns>
		public List<int> GetTiles()
		{
			List<int> list = new List<int>();

			foreach (int id in tiles.Keys)
				list.Add(id);

			list.Sort();
			return list;
		}


		/// <summary>
		/// Return the information for a tile
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Tile GetTile(int id)
		{
			if (tiles.ContainsKey(id))
				return tiles[id];
			else
				return null;
		}

/*
		/// <summary>
		/// Returns the color data of a tile
		/// </summary>
		/// <param name="BufferID">Tile ID</param>
		/// <returns></returns>
		public void GetCollisionMask(int BufferID)
		{
			if (BufferID < 0 || BufferID > Tiles.Count)
				return;


			Tile tile = GetTile(BufferID);
			if (tile == null)
				return;

			// If colors already gathered
			if (tile.CollisionMask != null)
				return;

			

			//tile.Data = new Color[tile.Rectangle.Width * tile.Rectangle.Height];
			//IntPtr pixels = IntPtr.Zero;
			//Gl.glGetTexImage(Gl.GL_TEXTURE_2D, 0, Gl.GL_ALPHA, Gl.GL_BYTE, pixels);

			//System.Runtime.InteropServices.Marshal.Copy(pixels, tile.Data, 0, tile.Data.Length);

			return;
		}


 
		/// <summary>
		/// Gets collision mask for every tiles
		/// </summary>
		public bool GetCollisionMasks()
		{
			if (texture == null)
				return false;

			// Lock texture
			LockedBitmap lbm = texture.LockBits(System.Drawing.Imaging.ImageLockMode.ReadOnly);


			foreach (Tile tile in Tiles.Values)
			{
				// Collision mask
				if (tile.CollisionMask == null || tile.CollisionMask.Length != tile.Size.Width * tile.Size.Height)
				{
					tile.CollisionMask = new bool[(int)tile.Size.Width, (int)tile.Size.Height];
				}

				for (int y = (int)tile.Rectangle.Top; y < tile.Rectangle.Top + tile.Rectangle.Height; y++)
				{
					for (int x = (int)tile.Rectangle.Left; x < tile.Rectangle.Left + tile.Rectangle.Width; x++)
					{
						tile.CollisionMask[x - (int)tile.Rectangle.Left, y - (int)tile.Rectangle.Top] = lbm.Data[y * lbm.Width * 4 + x * 4 + 3] != 0 ? true : false;
					}
				}	
			}

			// Unlock texture
			texture.UnlockBits(lbm);


			return true;
		}
*/

		/// <summary>
		/// Removes a tile
		/// </summary>
		/// <param name="id"></param>
		public void Remove(int id)
		{
			if (GetTile(id) == null)
				return;
			tiles.Remove(id);
		}


		/// <summary>
		/// Remove all tiles
		/// </summary>
		public void Clear()
		{
			tiles.Clear();
		}

		#endregion



		#region Rendering

		/// <summary>
		/// Draws a tile on the screen
		/// </summary>
		/// <param name="id">Tile ID</param>
		/// <param name="pos">Position on the screen</param>
		public void Draw(int id, Point pos)
		{
			Tile tile = GetTile(id);
			if (tile == null)
				return;

			Display.Texture = Texture;
			//	Rectangle rect = new Rectangle(pos, new Size((int)(tile.Size.Width * Zoom.Width), (int)(tile.Size.Height * Zoom.Height)));
			Rectangle rect = new Rectangle(pos.X - (int)(tile.HotSpot.X * Scale.Width), pos.Y - (int)(tile.HotSpot.Y * Scale.Height),
				(int)(tile.Size.Width * Scale.Width), (int)(tile.Size.Height * Scale.Height));

			//Texture.Blit(rect, new Rectangle(pos, tile.Size)); 
			Texture.Blit(rect, tile.Rectangle); 
		}


		/// <summary>
		/// Draws a tile on the screen and flip it
		/// </summary>
		/// <param name="id">Tile ID</param>
		/// <param name="pos">Location of the tile on the screen</param>
		/// <param name="vflip">Verticaly flip the texture</param>
		/// <param name="hflip">Horizontaly flip the texture</param>
		public void Draw(int id, Point pos, bool hflip, bool vflip)
		{

			Tile tile = GetTile(id);
			if (tile == null)
				return;

			//Display.Texture = Texture;


			Point start = new Point(
				pos.X - (int)(tile.HotSpot.X * Scale.Width),
				pos.Y - (int)(tile.HotSpot.Y * Scale.Height)
				);

			Rectangle dst = new Rectangle(start,
				new Size(
					(int)(tile.Rectangle.Width * Scale.Width),
					(int)(tile.Rectangle.Height * Scale.Height))
					);


			Rectangle tex = tile.Rectangle;

			if (hflip)
			{
				tex.X = tile.Rectangle.Width + tile.Rectangle.X;
				tex.Width = -tile.Rectangle.Width;
			}

			if (vflip)
			{
				tex.Y = tile.Rectangle.Height + tile.Rectangle.Y;
				tex.Height= -tile.Rectangle.Height;
			}



			Texture.Blit(dst, tex);

		}


		/// <summary>
		/// Draws a tile on the screen and stretch it
		/// </summary>
		/// <param name="id">Tile ID</param>
		/// <param name="rect">Rectangle on the screen</param>
		/// <param name="mode">Rendering mode</param>
		public void Draw(int id, Rectangle rect, TextureLayout mode)
		{
			Tile tile = GetTile(id);
			if (tile == null)
				return;


			Display.Texture = Texture;
			Texture.Blit(rect, tile.Rectangle, mode);

		}


		#endregion




		#region Properties

		/// <summary>
		/// Name of the asset
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// List of all tiles in the TileSet
		/// </summary>
		[Browsable(false)]
		public List<int> Tiles
		{
			get
			{
				List<int> list = new List<int>();

				foreach (int id in tiles.Keys)
					list.Add(id);

				return list;
			}
		}
		Dictionary<int, Tile> tiles;


		/// <summary>
		/// Name of the texture
		/// </summary>
		[CategoryAttribute("TileSet")]
		[DescriptionAttribute("Name of the texture to use")]
		[TypeConverter(typeof(BinaryEnumerator))]
		public string TextureName
		{
			set
			{
				textureName = value;
				Texture.LoadImage(textureName);
			}

			get
			{
				return textureName;
			}
		}
		string textureName;

		/// <summary>
		/// Gets / sets the texture
		/// </summary>
		[Browsable(false)]
		public Texture Texture
		{
			get;
			set;
		}


		/// <summary>
		/// Returns the number of tiles in the bank
		/// </summary>
		[CategoryAttribute("TileSet")]
		[DescriptionAttribute("Number of tile in this bank")]
		public int Count
		{
			get
			{
				return tiles.Count;
			}
		}




		/// <summary>
		/// Gets/sets the zoom factor of the tiles
		/// </summary>
		[Browsable(false)]
		public SizeF Scale
		{
			get;
			set;
		}


		/// <summary>
		/// Rendering batch
		/// </summary>
		[Browsable(false)]
		public Batch Batch
		{
			get;
			protected set;
		}

		#endregion


	}


}
