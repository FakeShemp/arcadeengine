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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;



namespace RuffnTumble
{


	//
	// QuadTree 2D : http://www.gamedev.net/community/forums/topic.asp?topic_id=512632
	// Collision : http://wiki.allegro.cc/index.php?title=Pixelate:Issue_14/jnrdev#2_-_hills_.26_slopes
	// http://jnrdev.72dpiarmy.com/en/jnrdev1/
	// http://www.tonypa.pri.ee/tbw/start.html
	// http://games.greggman.com/mckids/programming_mc_kids.htm
	//
	//
	//

	/// <summary>
	/// Layer for levels
	/// </summary>
	public class Layer : IDisposable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lvl">Parent level</param>
		public Layer(Level lvl)
		{
			if (lvl == null)
				throw new ArgumentNullException("lvl", "lvl can not be null in Layer constructor !");

			Level = lvl;
			Visible = true;
			TileSet = new TileSet();
			Color = Color.White;
		}


		/// <summary>
		/// Dispose resources
		/// </summary>
		public void Dispose()
		{
			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;
		}


		/// <summary>
		/// Reset the size of the layer
		/// </summary>
		/// <param name="size">Desired size</param>
		public void SetSize(Size size)
		{
			Tiles = new List<List<int>>(size.Height);
			for (int y = 0; y < size.Height; y++)
			{
				Tiles.Add(new List<int>());
				for (int x = 0; x < size.Width; x++)
					Tiles[y].Add(0);
			}
		}


		/// <summary>
		/// Draws the layer
		/// </summary>
		// http://www.gamedev.net/reference/articles/article743.asp
		public void Draw(SpriteBatch batch, Camera camera)
		{
			// No tileset or layer not visible
			if (TileSet == null || !Visible || camera == null || batch == null)
				return;

			batch.Flush();
			Display.PushScissor(camera.ViewPort);
			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					Vector2 loc = new Vector2(x * Level.BlockSize.Width - camera.Location.X , y * Level.BlockSize.Height - camera.Location.Y);
					batch.DrawTile(TileSet, GetTileAtBlock(x, y), loc, Color, 0.0f, new Vector2(1.0f, 1.0f), SpriteEffects.None, 0.0f);
				}
			batch.Flush();
			Display.PopScissor();
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
		public void Resize(Size newsize)
		{
			if (newsize.Width <= 0 || newsize.Height <= 0)
				return;

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
				Tiles.Add(row);
			else
				Tiles.Insert(rowid, row);
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
					row.Add(tileid);
				else
					row.Insert(columnid, tileid);
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
		}



		#endregion


		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement("layer");

			writer.WriteStartElement("texture");
			writer.WriteAttributeString("name", TextureName);
			writer.WriteEndElement();	// Texture

	
			writer.WriteStartElement("visibility");
			writer.WriteAttributeString("value", Visible.ToString());
			writer.WriteEndElement();	// Visibility

			writer.WriteStartElement("alpha");
			writer.WriteAttributeString("value", Color.A.ToString());
			writer.WriteEndElement();	// Alpha


			// Loops throughs tiles
			//writer.WriteStartElement("tiles");
			for (int y = 0; y < Level.Size.Height; y++)
			{
				writer.WriteStartElement("row");
				writer.WriteAttributeString("id", y.ToString());

				for (int x = 0; x < Level.Size.Width; x++)
					writer.WriteString(Tiles[y][x].ToString() + " ");

				writer.WriteEndElement();	// row
			}
			//writer.WriteEndElement();	// tiles


			writer.WriteEndElement();	// layer

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

			if (xml.Attributes["texture"] != null)
				SetTexture(xml.Attributes["texture"].Value);

			if (xml.Attributes["visibility"] != null)
				Visible = Boolean.Parse(xml.Attributes["visibility"].Value);

			if (xml.Attributes["alpha"] != null)
				Color = Color.FromArgb(byte.Parse(xml.Attributes["alpha"].Value), Color.White);


			foreach (XmlNode node in xml.ChildNodes)
			{
				if (node.Name != "row")
					continue;
				
				int rowid = Int32.Parse(node.Attributes["id"].Value);
				int pos = 0;

				string[] val = node.InnerText.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);
				foreach (string id in val)
					Tiles[rowid][pos++] = Int32.Parse(id);


			}

			return true;
		}


		#endregion


		#region Tiles


		/// <summary>
		/// Sets the texture for the tileset
		/// </summary>
		/// <param name="name">Name of the texture</param>
		/// <returns>True on success</returns>
		public bool SetTexture(string name)
		{
			TextureName = name;

			if (string.IsNullOrEmpty(TextureName))
				return false;


			TileSet.LoadTexture(name);
			TileSet.Clear();


			int start = 0;
			int x = 0;
			int y = 0;
			for (y = 0; y < TileSet.Texture.Size.Height; y += Level.BlockSize.Height)
				for (x = 0; x < TileSet.Texture.Size.Width; x += Level.BlockSize.Width)
				{
					Tile tile = TileSet.AddTile(start++);
					tile.Rectangle = new Rectangle(x, y, Level.BlockSize.Width, Level.BlockSize.Height);
				}

			return true;
		}


		/// <summary>
		/// Returns the id of the tile at block location
		/// </summary>
		/// <param name="point">Point in the layer in block</param>
		/// <returns>ID of the block</returns>
		public int GetTileAtBlock(int x, int y)
		{
			if (y < 0 || y >= Tiles.Count)
				return -1;

			if (x < 0 || x >= Tiles[y].Count)
				return -1;

			return Tiles[y][x];
		}


		/// <summary>
		/// Returns the id of the tile in world location
		/// </summary>
		/// <param name="point">Wolrd location</param>
		/// <returns>ID of the tile</returns>
		public int GetTileAtPixel(Vector2 point)
		{
			if (Level.BlockSize.Width == 0 || Level.BlockSize.Height == 0)
				return -1;

			Point p = new Point((int)(point.X / Level.BlockSize.Width), (int)(point.Y / Level.BlockSize.Height));

			return GetTileAtBlock(p.X, p.Y);
		}



		/// <summary>
		/// Sets the tile at the given location (in block)
		/// </summary>
		/// <param name="point">Offset in block in the layer</param>
		/// <param name="BufferID">ID of the block to paste</param>
		public void SetTileAtBlock(Point point, int id)
		{
			// Out of bound
			if (point.Y < 0 || point.Y >= Tiles.Count)
				return;
			if (point.X < 0 || point.X >= Tiles[point.Y].Count)
				return;

			Tiles[point.Y][point.X] = id;
		}



		/// <summary>
		/// Sets the tile at the given world location 
		/// </summary>
		/// <param name="point">Wolrd coordinate</param>
		/// <param name="BufferID">ID of the block to paste</param>
		public void SetTileAtPixel(Vector2 point, int id)
		{
			if (Level == null)
				return;

			SetTileAtBlock(Level.PositionToBlock(point), id);
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
			int targetTile = GetTileAtBlock(pt.X, pt.Y);

			// First fill
			LinearFill(pt, id);

			// While exists line to fill
			while (ranges.Count > 0)
			{
				FloodFillRange range = ranges.Dequeue();

				for (int x = range.StartX; x <= range.EndX; x++)
				{
					// Check upward
					if (range.Y > 0 && GetTileAtBlock(x, range.Y - 1) == targetTile)
						LinearFill(new Point(x, range.Y - 1), id);

					// Check downward
					if (range.Y < Level.Size.Height + 1 && GetTileAtBlock(x, range.Y + 1) == targetTile)
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
			int targetId = GetTileAtBlock(pt.X, pt.Y);
			if (targetId == id)
				return;


			int minx = 0;
			int maxx = 0;


			// Check on the left
			for (minx = pt.X; minx >= 0; minx--)
			{
				Point pos = new Point(minx, pt.Y);
				if (GetTileAtBlock(pos.X, pos.Y) != targetId)
					break;

				SetTileAtBlock(pos, id);
			}
			minx++;

			// Check on the right
			for (maxx = pt.X + 1; maxx <= Level.Size.Width; maxx++)
			{
				Point pos = new Point(maxx, pt.Y);
				if (GetTileAtBlock(pos.X, pos.Y) != targetId)
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
		/// Contain all the tile of the layer
		/// </summary>
		List<List<int>> Tiles;


		/// <summary>
		/// TileSet
		/// </summary>
		TileSet TileSet;


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
		/// Name of the texture to use for the TileSet
		/// </summary>
		public string TextureName
		{
			get;
			private set;
		}

		/// <summary>
		/// Parent level 
		/// </summary>
		[Browsable(false)]
		public Level Level
		{
			get;
			private set;
		}


		/// <summary>
		/// Size of the Layer in block
		/// </summary>
		public Size Size
		{
			get
			{
				if (Tiles == null)
					return Size.Empty;

				return new Size(Tiles[0].Count, Tiles.Count);
			}
		}


		/// <summary>
		/// Layer color
		/// </summary>
		public Color Color
		{
			get;
			set;
		}

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



	#region FloodFill


	/// <summary>
	/// Represents a linear range to be filled and branched from.
	/// </summary>
	internal struct FloodFillRange
	{
		public int StartX;
		public int EndX;
		public int Y;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="startX"></param>
		/// <param name="endX"></param>
		/// <param name="y"></param>
		public FloodFillRange(int startX, int endX, int y)
		{
			StartX = startX;
			EndX = endX;
			Y = y;
		}
	}



	#endregion

}



