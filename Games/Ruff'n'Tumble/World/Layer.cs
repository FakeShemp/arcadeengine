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
			Alpha = 255;
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

 * 
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

*/


		/// <summary>
		/// Resize the layer
		/// </summary>
		/// <param name="size">New size</param>
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
					Vector2 loc = new Vector2((x * Level.BlockSize.Width) - camera.Location.X, (y * Level.BlockSize.Height) - camera.Location.Y);
					batch.DrawTile(TileSet, GetTileAtBlock(new Point(x, y)), loc, Color.FromArgb(Alpha, Color.White), 0.0f, new Vector2(1.0f, 1.0f), SpriteEffects.None, 0.0f);
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
		/// <returns></returns>
		public void Resize(Size newsize)
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
			writer.WriteAttributeString("value", Alpha.ToString());
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
				Alpha = byte.Parse(xml.Attributes["alpha"].Value);


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
		/// <param name="name"></param>
		/// <returns></returns>
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
		public int GetTileAtPixel(Vector2 point)
		{
			if (Level.BlockSize.Width == 0 || Level.BlockSize.Height == 0)
				return -1;

			Point p = new Point((int)(point.X / Level.BlockSize.Width), (int)(point.Y / Level.BlockSize.Height));

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
		public void SetTileAtPixel(Vector2 point, int id)
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
		public CollisionResult CheckForCollision(Entity entity, Vector2 velocity)
		{
			// Adds the gravity
			//velocity.Offset(entity.Gravity);

			// Jumping ?
			//if (entity.Jump > 0)
			//{
			//    velocity.Offset(0, -entity.Jump / 2);
			//    entity.Jump -= entity.Gravity.Y / 2;
			//}

			// Collision result
			CollisionResult res = new CollisionResult(entity, velocity);





			// Check for vertical collision
			Vector4 colrect = new Vector4(0.0f, velocity.Y, 0.0f, 0.0f);
			Vector4 colblock = res.CheckForCollision(colrect);
			if (velocity.Y > 0)
			{
				if (colblock.Bottom != 0 || colblock.Right + colblock.Height != 0)
				{
					res.FinalVelocity.Y = velocity.Y - (entity.Location.Y + velocity.Y) % Level.BlockSize.Height - 1;
				}
				else
					res.FinalVelocity.Y = velocity.Y;
			}
			else if (velocity.Y < 0)
			{
				if (colblock.Top != 0 || colblock.Right != 0)
				{
					res.FinalVelocity.Y = 0;
				}
				else
					res.FinalVelocity.Y = velocity.Y;
			}

/*

			// Check for horizontal collision
			colrect = Rectangle.Empty; //entity.CollisionBoxLocation;
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

*/

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
		/// Contain all the tile of the layer
		/// </summary>
		List<List<int>> Tiles;


		/// <summary>
		/// Transparency of the layer
		/// </summary>
		public byte Alpha
		{
			get;
			set;
		}

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
		/// Size of the Layer
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
		public CollisionResult(Entity ent, Vector2 offset)
		{
			entity = ent;
			InitialVelocity = offset;
			Location = new Vector4();
			Tile = new Vector4();
			Collision = new Vector4();
		}

		/// <summary>
		/// Checks for vertical collision
		/// </summary>
		public Vector4 CheckForCollision(Vector4 rect)
		{
			Vector4 col = new Vector4();

			if (entity == null)
				return col;


			// Translate the collision box
			//Rectangle rect = entity.CollisionBoxLocation;
			//rect.Offset(0, InitialVelocity.Y);
			//rect.Offset(0, entity.Gravity.Y);


			// Collision points
			Vector2 topLeft = new Vector2(rect.Left, rect.Top);
			Vector2 topRight = new Vector2(rect.Left + rect.Width, topLeft.Y);
			Vector2 bottomLeft = new Vector2(topLeft.X, topLeft.Y + rect.Height);
			Vector2 bottomRight = new Vector2(topRight.X, bottomLeft.Y);


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
			Vector4 col = new Vector4(); //entity.CollisionBoxLocation;
			col.Offset(InitialVelocity);
			//col.Offset(entity.Gravity);


			// Collision points
			Location = col;
			//Location.TopLeft = new Vector2(col.Left, col.Top);
			//Location.TopRight = new Vector2(col.Left + col.Width, Location.TopLeft.Y);
			//Location.BottomLeft = new Vector2(Location.TopLeft.X, Location.TopLeft.Y + col.Height);
			//Location.BottomRight = new Vector2(Location.TopRight.X, Location.BottomLeft.Y);


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
		public Vector2 InitialVelocity;



		/// <summary>
		/// Velocity of the entity after the collision test
		/// </summary>
		public Vector2 FinalVelocity;


		/// <summary>
		/// List of entities the entity collided with
		/// </summary>
		public List<Entity> CollideWith;


		/// <summary>
		/// Tiles block ID
		/// </summary>
		public Vector4 Tile;


		/// <summary>
		/// Collisions block ID
		/// </summary>
		public Vector4 Collision;


		/// <summary>
		/// Collision point location
		/// </summary>
		public Vector4 Location;

		#endregion




	}


/*
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

*/
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



