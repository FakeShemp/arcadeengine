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
using ArcEngine;
using RuffnTumble.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using ArcEngine.Graphic;
using System.IO;
using ArcEngine.Asset;



namespace RuffnTumble
{


	//
	// QuadTree 2D : http://www.gamedev.net/community/forums/topic.asp?topic_id=512632
	// Collision : http://wiki.allegro.cc/index.php?title=Pixelate:Issue_14/jnrdev#2_-_hills_.26_slopes
	//
	//
	//

	/// <summary>
	/// Layer for levels
	/// </summary>
	public class Layer
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lvl">Level in which the layer is</param>
		public Layer(Level lvl)
		{
			Level = lvl;
			Visible = true;


			// Tiles
			Tiles = new List<List<int>>(Level.Size.Height);
			for (int y = 0; y < Level.Size.Height; y++)
			{
				Tiles.Add(new List<int>());
				for (int x = 0; x < Level.Size.Width; x++)
					Tiles[y].Add(0);
			}
		}


/*
		/// <summary>
		///  Inits the layer
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{



			// If a texture is present, build tileset
			TileSet.LoadTexture(TextureName);
			BuildTileSet();


			// Creates the script
			if (!string.IsNullOrEmpty(ScriptName))
			{
				Script script = ResourceManager.CreateAsset<Script>(ScriptName);
				if (script != null)
				{
					//if (script.Compile())
					//	ScriptInterface = (ILayer)script.FindInterface("RuffnTumble.Interface.ILayer");

				}
			}
			

			// Init the script
			//if (ScriptInterface != null)
			//    ScriptInterface.Init(this);


			return true;
		}
*/


		/// <summary>
		/// Build the tileset
		/// </summary>
		private void BuildTileSet()
		{
			TileSet.Clear();

			if (TileSet.Texture == null)
				return ;
			
			int start = 0;
			int x = 0;
			int y = 0;
			for (y = 0; y < TileSet.Texture.Size.Height; y += Level.BlockSize.Height)
				for (x = 0; x < TileSet.Texture.Size.Width; x += Level.BlockSize.Width)
				{
					Tile tile = TileSet.AddTile(start++);
					tile.Rectangle = new Rectangle(x, y, Level.BlockSize.Width, Level.BlockSize.Height);
				}
		}



		/// <summary>
		/// Draws the layer
		/// </summary>
		// http://www.gamedev.net/reference/articles/article743.asp
		public void Draw(Camera camera)
		{
			// No tileset or layer not visible
			if (TileSet == null || !Visible || camera == null)
				return;


			// Taille de rendu du niveau
			int renderwidth = camera.ViewPort.Width / Level.BlockDimension.Width;
			int renderheight = camera.ViewPort.Height / Level.BlockDimension.Height;
			if (renderwidth > Level.Size.Width) renderwidth = Level.Size.Width;
			if (renderheight > Level.Size.Height) renderheight = Level.Size.Height;

			// On trouve la location en block pour le "pixel perfect scrolling"
			int blockx = Level.Location.X / Level.BlockDimension.Width;
			int blocky = Level.Location.Y / Level.BlockDimension.Height;
			int deltax = Level.Location.X % Level.BlockDimension.Width;
			int deltay = Level.Location.Y % Level.BlockDimension.Width;


			// Draw tiles
			TileSet.Scale = Level.Scale;
			Display.Color = Color;
			for (int yy = 0; yy < renderheight + 2; yy++)
			{
				for (int xx = 0; xx < renderwidth + 2; xx++)
				{
					int id = GetTileAtBlock(new Point(blockx + xx, blocky + yy));

					if (id == -1)
						continue;

					//tileSet.Draw(id, new Point(
					//   (xx * level.BlockDimension.Width) + Level.DisplayZone.X - deltax,
					//   (yy * level.BlockDimension.Height) + Level.DisplayZone.Y - deltay));

					Rectangle rect = new Rectangle(
						new Point((xx * Level.BlockDimension.Width) + camera.ViewPort.X - deltax,
									(yy * Level.BlockDimension.Height) + camera.ViewPort.Y - deltay),
						new Size(32, 32)
						);

					TileSet.Draw(id, rect, TextureLayout.Stretch);

		
				
				}
			}

			//
			// Display tile grid
			//
			if (ShowGrid)
			{
				Display.Color = Color.FromArgb(Alpha, Color.Red);

				for (int yy = 0; yy < renderheight + 2; yy++)
				{
					Display.Line(
						new Point(camera.ViewPort.Left, (yy * Level.BlockDimension.Height - deltay + camera.ViewPort.Top)),
						new Point(camera.ViewPort.Right, (yy * Level.BlockDimension.Height - deltay + camera.ViewPort.Top))
					);
				}
				for (int xx = 0; xx < renderwidth + 2; xx++)
				{
					Display.Line(
						new Point(xx * Level.BlockDimension.Width - deltax + camera.ViewPort.Left, camera.ViewPort.Top),
						new Point(xx * Level.BlockDimension.Width - deltax + camera.ViewPort.Left, camera.ViewPort.Bottom)
					);
				}

				Display.Color = Color.White;
			}








		}



		/// <summary>
		/// Updates layer's logic
		/// </summary>
		public void Update(GameTime time)
		{
		}



		#region Resize

		/// <summary>
		/// Resize the layer
		/// </summary>
		/// <param name="newsize">new size</param>
		/// <returns></returns>
		internal void Resize(Size newsize)
		{
			// Rows
			if (newsize.Height > Level.Size.Height)
			{
				for (int y = Level.Size.Height; y < newsize.Height; y++)
					InsertRow(y, 0);
			}
			else if (newsize.Height < Level.Size.Height)
			{
				for (int y = Level.Size.Height - 1; y >= newsize.Height; y--)
					RemoveRow(y);
			}


			// Columns
			if (newsize.Width > Level.Size.Width)
			{
				for (int x = Level.Size.Width; x < newsize.Width; x++)
					InsertColumn(x, 0);
			}
			else if (newsize.Width < Level.Size.Width)
			{
				for (int x = Level.Size.Width - 1; x >= newsize.Width; x--)
					RemoveColumn(x);
			}


		}


		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="rowid">Position of insert</param>
		/// <param name="tileid">ID of the tile to insert</param>
		public void InsertRow(int rowid, int tileid)
		{
			// Build the row
			List<int> row = new List<int>(Level.Size.Width);
			for (int x = 0; x < Level.Size.Width; x++)
				row.Add(0);

			// Adds the row at the end
			if (rowid >= Tiles.Count)
			{
				Tiles.Add(row);
			}

			// Or insert the row
			else
			{
				Tiles.Insert(rowid, row);

				//// Offset objects
				//Rectangle zone = new Rectangle(0, rowid * Level.BlockDimension.Height, Level.Dimension.Width, Level.Dimension.Height);
				//OffsetObjects(zone, new Point(0, Level.BlockDimension.Height));
			}
		}


		/// <summary>
		/// Removes a row
		/// </summary>
		/// <param name="rowid">Position of remove</param>
		public void RemoveRow(int rowid)
		{
			// Removes the row
			if (rowid >= Tiles.Count)
				return;

			Tiles.RemoveAt(rowid);

			//// Offset objects
			//Rectangle zone = new Rectangle(0, rowid * Level.BlockDimension.Height, Level.Dimension.Width, Level.Dimension.Height);
			//OffsetObjects(zone, new Point(0, -Level.BlockDimension.Height));
		}


		/// <summary>
		/// Inserts a column
		/// </summary>
		/// <param name="columnid">Position of remove</param>
		/// <param name="tileid">ID of the tile to insert</param>
		public void InsertColumn(int columnid, int tileid)
		{
			// Insert the column
			foreach (List<int> row in Tiles)
			{
				if (columnid >= row.Count)
				{
					row.Add(tileid);
				}
				else
				{
					row.Insert(columnid, tileid);

					//// Offset objects
					//Rectangle zone = new Rectangle(columnid * Level.BlockDimension.Width, 0, Level.Dimension.Width, Level.Dimension.Height);
					//OffsetObjects(zone, new Point(Level.BlockDimension.Width, 0));
				}
			}
		}


		/// <summary>
		/// Removes a column
		/// </summary>
		/// <param name="columnid">Position of remove</param>
		public void RemoveColumn(int columnid)
		{
			// Remove the column
			foreach (List<int> row in Tiles)
			{
				row.RemoveAt(columnid);
			}

			//// Offset objects
			//Rectangle zone = new Rectangle(
			//    columnid * Level.BlockDimension.Width, 0,
			//    Level.Dimension.Width, Level.Dimension.Height);
			//OffsetObjects(zone, new Point(-Level.BlockDimension.Width, 0));
		}



		#endregion


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

			xml.WriteStartElement("layer");
			xml.WriteAttributeString("name", Name);

	
			xml.WriteStartElement("visibility");
			xml.WriteAttributeString("value", Visible.ToString());
			xml.WriteEndElement();	// Visibility

			xml.WriteStartElement("zindex");
			xml.WriteAttributeString("value", ZIndex.ToString());
			xml.WriteEndElement();		// zindex

			xml.WriteStartElement("alpha");
			xml.WriteAttributeString("value", Color.A.ToString());
			xml.WriteEndElement();		// alpha

			if (!string.IsNullOrEmpty(ScriptName))
			{
				xml.WriteStartElement("script");
				xml.WriteAttributeString("name", ScriptName);
				xml.WriteEndElement();		// script
			}

			if (ShowGrid)
			{
				xml.WriteStartElement("showgrid");
				xml.WriteAttributeString("value", ShowGrid.ToString());
				xml.WriteEndElement();
			}



			// Loops throughs tiles
			xml.WriteStartElement("tiles");

			// Texture to use
			xml.WriteStartElement("texture");
			xml.WriteAttributeString("name", TextureName);
			xml.WriteEndElement();


			for (int y = 0; y < Level.Size.Height; y++)
			{
				xml.WriteStartElement("row");
				xml.WriteAttributeString("BufferID", y.ToString());

				for (int x = 0; x < Level.Size.Width; x++)
					xml.WriteString(Tiles[y][x].ToString() + " ");

				xml.WriteEndElement();	// row
			}


			xml.WriteEndElement();	// tiles

			xml.WriteEndElement();	// layer

			return true;
		}

		/// <summary>
		/// Loads a layer form a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;

			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
				//	base.LoadComment(node);
					continue;
				}


				switch (node.Name.ToLower())
				{
					// Tiles of the map
					case "tiles":
					{

						foreach (XmlNode subnode in node)
						{
							switch (subnode.Name.ToLower())
							{
								// Texture to use
								case "texture":
								{
									TextureName = subnode.Attributes["name"].Value;
								}
								break;

								// Add a row
								case "row":
								{
									int rowid = Int32.Parse(subnode.Attributes["id"].Value);
									int pos = 0;

									string[] val = subnode.InnerText.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);
									foreach (string id in val)
										Tiles[rowid][pos++] = Int32.Parse(id);
								}
								break;
							}
						}
					}
					break;

					// Shows/hides the grid
					case "showgrid":
					{
						ShowGrid = Boolean.Parse(node.Attributes["value"].Value.ToString());
					}
					break;



					// alpha transparency of the layer
					case "alpha":
					{
						Alpha = Byte.Parse(node.Attributes["value"].Value);
					}
					break;

					// Script name
					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
					}
					break;

					// Zindex
					case "zindex":
					{
						ZIndex = Int32.Parse(node.Attributes["value"].Value);

					}
					break;

					// Layer visibility
					case "visibility":
					{
						Visible = Boolean.Parse(node.Attributes["value"].Value);
					}
					break;



					default:
					{
						Trace.WriteLine("Layer : Unknown node element \"" + node.Name + "\"");
					}
					break;
				}
			}
			return true;

		}

		#endregion


		#region Tiles


		/// <summary>
		/// Returns the BufferID of the tile at block location
		/// </summary>
		/// <param name="point">Point in the layer in block</param>
		/// <returns>ID of the block</returns>
		public int GetTileAtBlock(Point point)
		{
			if (point.Y < 0 || point.Y >= Tiles.Count)
				return -1;

			if (point.X < 0 || point.X >= Tiles[point.Y].Count)
				return -1;

			return Tiles[point.Y][point.X];
		}


		/// <summary>
		/// Returns the id of the tile at pixel location
		/// </summary>
		/// <param name="point">Point in the layer in pixel</param>
		/// <returns>ID of the tile</returns>
		public int GetTileAtPixel(Point point)
		{
			if (Level.BlockDimension.Width == 0 || Level.BlockDimension.Height == 0)
				return -1;

			Point p = new Point(point.X / Level.BlockDimension.Width, point.Y / Level.BlockDimension.Height);

			return GetTileAtBlock(p);
		}



		/// <summary>
		/// Sets the tile at the given location (in block)
		/// </summary>
		/// <param name="point">Offset in block in the layer</param>
		/// <param name="BufferID">ID of the block to paste</param>
		public void SetTileAtBlock(Point point, int id)
		{
			// No tile
			if (id < 0)
				return;

			// Out of bound
			if (point.Y < 0 || point.Y >= Tiles.Count)
				return;
			if (point.X < 0 || point.X >= Tiles[point.Y].Count)
				return;

			Tiles[point.Y][point.X] = id;
		}



		/// <summary>
		/// Sets the tile BufferID at the given location (in pixel)
		/// </summary>
		/// <param name="point">Point in the layer in pixel</param>
		/// <param name="BufferID">ID of the block to paste</param>
		public void SetTileAtPixel(Point point, int id)
		{
			Point p = Level.PositionToBlock(point);

			SetTileAtBlock(p, id);
		}


		#endregion


		#region Collision

		/// <summary>
		/// Check for collision
		/// </summary>
		/// <param name="entity">Entity to check</param>
		/// <param name="velocity">Direction offset</param>
		/// <returns>Collision result</returns>
		// http://jnrdev.72dpiarmy.com/en/jnrdev1/
		// http://www.tonypa.pri.ee/tbw/start.html
		// http://games.greggman.com/mckids/programming_mc_kids.htm
		public CollisionResult CheckForCollision(Entity entity, Point velocity)
		{
			// Adds the gravity
			velocity.Offset(entity.Gravity);

			// Jumping ?
			if (entity.Jump > 0)
			{
				velocity.Offset(0, -entity.Jump / 2);
				entity.Jump -= entity.Gravity.Y / 2;
			}

			// Collision result
			CollisionResult res = new CollisionResult(entity, velocity);





			// Check for vertical collision
			Rectangle colrect = entity.CollisionBoxLocation;
			colrect.Offset(0, velocity.Y);
			CollisionBlock colblock = res.CheckForCollision(colrect);
			if (velocity.Y > 0)
			{
				if (colblock.BottomLeft != 0 || colblock.BottomRight != 0)
				{
					res.FinalVelocity.Y = velocity.Y - (entity.Location.Y + velocity.Y) % Level.BlockDimension.Height - 1;
				}
				else
					res.FinalVelocity.Y = velocity.Y;
			}
			else if (velocity.Y < 0)
			{
				if (colblock.TopLeft != 0 || colblock.TopRight != 0)
				{
					res.FinalVelocity.Y = 0;
				}
				else
					res.FinalVelocity.Y = velocity.Y;
			}



			// Check for horizontal collision
			colrect = entity.CollisionBoxLocation;
			colrect.Offset(velocity.X, res.FinalVelocity.Y);
			colblock = res.CheckForCollision(colrect);
			if (velocity.X > 0)  // Right
			{
				if (colblock.TopRight != 0 || colblock.BottomRight != 0)
				{
					res.FinalVelocity.X = velocity.X -
						(entity.Location.X + velocity.X + entity.CollisionBox.Width) % Level.BlockDimension.Width;
				}
				else
					res.FinalVelocity.X = velocity.X;
			}
			else if (velocity.X < 0)    // Left
			{
				if (colblock.TopLeft != 0 || colblock.BottomLeft != 0)
				{
					res.FinalVelocity.X = velocity.X +
					   Level.BlockDimension.Width - (entity.CollisionBoxLocation.Left + velocity.X) % Level.BlockDimension.Width;
				}
				else
					res.FinalVelocity.X = velocity.X;
			}



			// Is entity falling ?
			entity.IsFalling = res.FinalVelocity.Y > 0.0f;
			entity.IsJumping = res.FinalVelocity.Y < 0.0f;



			return res;
		}



		#endregion


		#region Flood Fill
		//
		//http://www.codeproject.com/KB/GDI-plus/queuelinearfloodfill.aspx
		//

		/// <summary>
		/// Queue of floodfill ranges.
		/// </summary>
		Queue<FloodFillRange> ranges = new Queue<FloodFillRange>();

		/// <summary>
		/// Fills the specified point on the layer with the a selected tile.
		/// </summary>
		/// <param name="pt">The starting point for the fill</param>
		/// <param name="brush">LayerBrush</param>
		public void FloodFill(Point pt, LayerBrush brush)
		{
			// HACK
			int id = brush.Tiles[0][0];

			// Clear tools
			ranges.Clear();

			// Target tile
			int targetTile = GetTileAtBlock(pt);

			// First fill
			LinearFill(pt, id);

			// While exists line to fill
			while (ranges.Count > 0)
			{
				FloodFillRange range = ranges.Dequeue();

				for (int x = range.StartX; x <= range.EndX; x++)
				{
					// Check upward
					if (range.Y > 0 && GetTileAtBlock(new Point(x, range.Y - 1)) == targetTile)
						LinearFill(new Point(x, range.Y - 1), id);

					// Check downward
					if (range.Y < Level.Size.Height + 1 && GetTileAtBlock(new Point(x, range.Y + 1)) == targetTile)
						LinearFill(new Point(x, range.Y + 1), id);
				}

			}

		}


		/// <summary>
		/// Finds the furthermost left and right boundaries of the fill area
		/// on a given y coordinate, starting from a given x coordinate, 
		/// filling as it goes.
		/// Adds the resulting horizontal range to the queue of floodfill ranges,
		/// to be processed in the main loop.
		/// </summary>
		/// <param name="pt">The coordinate to start</param>
		/// <param name="BufferID">Id of the tile to fill</param>
		private void LinearFill(Point pt, int id)
		{

			// ID of the tile to fill
			int targetId = GetTileAtBlock(pt);
			if (targetId == id)
				return;


			int minx = 0;
			int maxx = 0;


			// Check on the left
			for (minx = pt.X; minx >= 0; minx--)
			{
				Point pos = new Point(minx, pt.Y);
				if (GetTileAtBlock(pos) != targetId)
					break;

				SetTileAtBlock(pos, id);
			}
			minx++;

			// Check on the right
			for (maxx = pt.X + 1; maxx <= Level.Size.Width; maxx++)
			{
				Point pos = new Point(maxx, pt.Y);
				if (GetTileAtBlock(pos) != targetId)
					break;

				SetTileAtBlock(pos, id);
			}
			maxx--;

			// add range to queue
			ranges.Enqueue(new FloodFillRange(minx, maxx, pt.Y));
		}


		#endregion


		#region Properties


		/// <summary>
		/// Layer's name
		/// </summary>
		public string Name;

		
		
		/// <summary>
		/// Contain all the tile of the layer
		/// </summary>
		List<List<int>> Tiles;


/*
		/// <summary>
		/// Private TileSet
		/// </summary>
		[Browsable(false)]
		public TileSet TileSet
		{
			get;
			private set;
		}

*/
		/// <summary>
		/// Gets/sets the layer visiblity / shall we draws it ?
		/// </summary>
		[Category("Layer")]
		[Description("Layer's visibility")]
		public bool Visible
		{
			get;
			set;
		}


		/// <summary>
		/// Gets the current texture
		/// </summary>
	//	[Browsable(false)]
	//	public Texture Texture;

/*
		/// <summary>
		/// Gets/sets the name of the texture to use with this layer
		/// </summary>
		[Category("Tiles")]
		[Description("Texture to use")]
	//	[TypeConverter(typeof(TextureEnumerator))]
		public string TextureName
		{
			get { return TileSet.TextureName; }
			set
			{
			//	textureName = value;

				TileSet.LoadTexture(value);
				
								//if (texture != null)
								//    texture.Unlock();

								//textureName = value;
								//texture = ResourceManager.Handle.GetTexture(textureName);
								//if (texture != null)
								//    texture.Lock();
				

				// Rebuild the tileset
				BuildTileSet();

			}
		}
	//	string textureName;
*/



		/// <summary>
		/// Who is my parent level ?
		/// </summary>
		[Browsable(false)]
		public Level Level
		{
			get;
			private set;
		}


		/// <summary>
		/// Draws the grid of the layer
		/// </summary>
		[Browsable(false)]
		public bool ShowGrid
		{
			get;
			set;
		}

/*
		/// <summary>
		/// Gets/sets the transparency of the layer
		/// </summary>
		[CategoryAttribute("Layer")]
		[Description("Transparency of the layer")]
		[Editor(typeof(SliderEditor), typeof(UITypeEditor))]
		public byte Alpha
		{
			get
			{
				return Color.A;
			}
			set
			{
				//alpha = value;
				Color = Color.FromArgb(value, Color.White);
			}
		}
		Color Color;

*/
/*
		/// <summary>
		/// Gets/sets the script to use
		/// </summary>
		[CategoryAttribute("Script")]
		[Description("Script's name to handle the layer events")]
		[TypeConverter(typeof(ScriptEnumerator))]
		public string ScriptName
		{
			set;
			get;
		}
*/

		/// <summary>
		/// Script's handle
		/// </summary>
	//	ILayer ScriptInterface;

/*
		/// <summary>
		/// Draw Order of the layer
		/// </summary>
		[CategoryAttribute("Layer")]
		[Description("Layer draw order")]
		public int ZIndex
		{
			get;
			set;
		}

*/

		#endregion
	}



	/// <summary>
	/// Affiche un slider dans le propertygrid.
	/// </summary>
	public class SliderEditor : UITypeEditor
	{
		private ITypeDescriptorContext context = null;
		/// <summary>
		/// 
		/// </summary>
		public SliderEditor() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			// détermine le style : DropDown
			return UITypeEditorEditStyle.DropDown;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (edSvc == null)
			{
				this.context = null;
				return value;
			}
			this.context = context;

			// Initialise le slider
			TrackBar trackbar = new TrackBar();
			trackbar.AutoSize = false;
			trackbar.LargeChange = 16;
			trackbar.Maximum = 255;
			trackbar.Minimum = 0;
			trackbar.Orientation = Orientation.Horizontal;
			trackbar.SmallChange = 1;
			trackbar.TickFrequency = 32;
			trackbar.TickStyle = TickStyle.TopLeft;
			trackbar.Value = (byte)value;
			trackbar.ValueChanged += new EventHandler(trackbar_ValueChanged);
			edSvc.DropDownControl(trackbar);

			// retourne la valeur sélectionnée
			return (byte)trackbar.Value;
		}



		/// <summary>
		/// quand la valeur change, on met à jour le propertygrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trackbar_ValueChanged(object sender, EventArgs e)
		{
			TrackBar tb = sender as TrackBar;
			if (tb == null || this.context == null)
				return;

			context.PropertyDescriptor.SetValue(context.Instance, (byte)tb.Value);
		}

	}


	#region Collision


	/// <summary>
	/// Result of an entity collision test
	/// </summary>
	public class CollisionResult
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ent"></param>
		/// <param name="offset"></param>
		public CollisionResult(Entity ent, Point offset)
		{
			entity = ent;
			InitialVelocity = offset;
			Location = new CollisionCoord();
			Tile = new CollisionBlock();
			Collision = new CollisionBlock();
		}

		/// <summary>
		/// Checks for vertical collision
		/// </summary>
		public CollisionBlock CheckForCollision(Rectangle rect)
		{
			CollisionBlock col = new CollisionBlock();

			if (entity == null)
				return col;


			// Translate the collision box
			//Rectangle rect = entity.CollisionBoxLocation;
			//rect.Offset(0, InitialVelocity.Y);
			//rect.Offset(0, entity.Gravity.Y);


			// Collision points
			Point topLeft = new Point(rect.Left, rect.Top);
			Point topRight = new Point(rect.Left + rect.Width, topLeft.Y);
			Point bottomLeft = new Point(topLeft.X, topLeft.Y + rect.Height);
			Point bottomRight = new Point(topRight.X, bottomLeft.Y);


			// Collision blocks
			//col.TopLeft = Level.CollisionLayer.GetTileAtCoord(topLeft);
			//col.TopRight = Level.CollisionLayer.GetTileAtCoord(topRight);
			//col.BottomLeft = Level.CollisionLayer.GetTileAtCoord(bottomLeft);
			//col.BottomRight = Level.CollisionLayer.GetTileAtCoord(bottomRight);


			//
			return col;
		}


		/// <summary>
		/// 
		/// </summary>
		public void Check()
		{
			if (entity == null)
				return;

			// Translate the collision box
			Rectangle col = entity.CollisionBoxLocation;
			col.Offset(InitialVelocity);
			col.Offset(entity.Gravity);


			// Collision points
			Location.TopLeft = new Point(col.Left, col.Top);
			Location.TopRight = new Point(col.Left + col.Width, Location.TopLeft.Y);
			Location.BottomLeft = new Point(Location.TopLeft.X, Location.TopLeft.Y + col.Height);
			Location.BottomRight = new Point(Location.TopRight.X, Location.BottomLeft.Y);


			// Tile block under the collision points
			//Tile.TopLeft = layer.GetTileAtCoord(Location.TopLeft);
			//Tile.TopRight = layer.GetTileAtCoord(Location.TopRight);
			//Tile.BottomLeft = layer.GetTileAtCoord(Location.BottomLeft);
			//Tile.BottomRight = layer.GetTileAtCoord(Location.BottomRight);


			// Collision blocks
			//layer = entity.ParentLayer.Level.CollisionLayer;
			//if (layer != null)
			//{
			//    Collision.TopLeft = layer.GetTileAtCoord(Location.TopLeft);
			//    Collision.TopRight = layer.GetTileAtCoord(Location.TopRight);
			//    Collision.BottomLeft = layer.GetTileAtCoord(Location.BottomLeft);
			//    Collision.BottomRight = layer.GetTileAtCoord(Location.BottomRight);
			//}


			//
			FinalVelocity = InitialVelocity;
		}


		#region Properties



		/// <summary>
		/// Entity
		/// </summary>
		public Entity Entity
		{
			get
			{
				return entity;
			}
		}
		Entity entity;




		/// <summary>
		/// Initial velocity of the entity before the collision test
		/// </summary>
		public Point InitialVelocity;



		/// <summary>
		/// Velocity of the entity after the collision test
		/// </summary>
		public Point FinalVelocity;


		/// <summary>
		/// List of entities the entity collided with
		/// </summary>
		public List<Entity> CollideWith;


		/// <summary>
		/// Tiles block ID
		/// </summary>
		public CollisionBlock Tile;


		/// <summary>
		/// Collisions block ID
		/// </summary>
		public CollisionBlock Collision;


		/// <summary>
		/// Collision point location
		/// </summary>
		public CollisionCoord Location;

		#endregion




	}

	/// <summary>
	/// Collision block ID
	/// </summary>
	public class CollisionBlock
	{
		/// <summary>
		/// 
		/// </summary>
		public int TopLeft;
		/// <summary>
		/// 
		/// </summary>
		public int TopRight;
		/// <summary>
		/// 
		/// </summary>
		public int BottomLeft;
		/// <summary>
		/// 
		/// </summary>
		public int BottomRight;
	}


	/// <summary>
	/// Collision point location
	/// </summary>
	public class CollisionCoord
	{
		/// <summary>
		/// 
		/// </summary>
		public Point TopLeft;
		/// <summary>
		/// 
		/// </summary>
		public Point TopRight;
		/// <summary>
		/// 
		/// </summary>
		public Point BottomLeft;
		/// <summary>
		/// 
		/// </summary>
		public Point BottomRight;
	}


	#endregion


	#region FloodFill


	/// <summary>
	/// Represents a linear range to be filled and branched from.
	/// </summary>
	internal struct FloodFillRange
	{
		public int StartX;
		public int EndX;
		public int Y;

		public FloodFillRange(int startX, int endX, int y)
		{
			StartX = startX;
			EndX = endX;
			Y = y;
		}
	}



	#endregion



/*
	/// <summary>
	/// Layer enumerator for PropertyGrids
	/// </summary>
	internal class LayerEnumerator : StringConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{

			List<Layer> layers = ((Level)context.Instance).Layers;

			List<string> list = new List<string>();
			list.Insert(0, "");

		//	foreach (Layer layer in layers)
		//		list.Add(layer.Name);

			return new StandardValuesCollection(list);
		}

	}

*/
}



