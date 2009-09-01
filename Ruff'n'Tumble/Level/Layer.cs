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



namespace RuffnTumble.Asset
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
		public Layer( Level lvl)
		{
			Level = lvl;
			Visible = true;
			Alpha = 255;
			SpawnPoints = new Dictionary<string, SpawnPoint>();
			Entities = new Dictionary<string, Entity>();
			tileSet = new TileSet();
			brushes = new Dictionary<string, LayerBrush>();
			paths = new Dictionary<string, Path>();


			// Tiles
			Tiles = new List<List<int>>(Level.Size.Height);
			for (int y = 0; y < Level.Size.Height; y++)
			{
				Tiles.Add(new List<int>());
				for (int x = 0; x < Level.Size.Width; x++)
					Tiles[y].Add(0);
			}
		}



		/// <summary>
		///  Inits the layer
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{


			// Load the SpawnPoint texture
			spTexture = new Texture(ResourceManager.GetResource("RuffnTumble.Resources.SpawnPoint.png"));


			// If a texture is present, build tileset
			tileSet.TextureName = textureName;
			if (!string.IsNullOrEmpty(textureName))
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
			if (ScriptInterface != null)
				ScriptInterface.Init(this);


			// Init all entities
			foreach (Entity entity in Entities.Values)
			{
				entity.Init();
			}

			return true;
		}



		/// <summary>
		/// Build the tileset
		/// </summary>
		private void BuildTileSet()
		{
			tileSet.Clear();

			if (tileSet.Texture == null)
				return ;
			
			int start = 0;
			int x = 0;
			int y = 0;
			for (y = 0; y < tileSet.Texture.Size.Height; y += Level.BlockSize.Height)
				for (x = 0; x < tileSet.Texture.Size.Width; x += Level.BlockSize.Width)
				{
					Tile tile = tileSet.AddTile(start++);
					tile.Rectangle = new Rectangle(x, y, Level.BlockSize.Width, Level.BlockSize.Height);
				}
		}



		/// <summary>
		/// Draws the layer
		/// </summary>
		// http://www.gamedev.net/reference/articles/article743.asp
		public void Draw()
		{
			// No tileset or layer not visible
			if (tileSet == null || !Visible)
				return;


			// Taille de rendu du niveau
			int renderwidth = Level.ViewPort.Width / Level.BlockDimension.Width;
			int renderheight = Level.ViewPort.Height / Level.BlockDimension.Height;
			if (renderwidth > Level.Size.Width) renderwidth = Level.Size.Width;
			if (renderheight > Level.Size.Height) renderheight = Level.Size.Height;

			// On trouve la location en block pour le "pixel perfect scrolling"
			int blockx = Level.Location.X / Level.BlockDimension.Width;
			int blocky = Level.Location.Y / Level.BlockDimension.Height;
			int deltax = Level.Location.X % Level.BlockDimension.Width;
			int deltay = Level.Location.Y % Level.BlockDimension.Width;


			// Draw tiles
			//tileSet.Bind();
			tileSet.Scale = Level.Zoom;
			Display.Color = Color;
			for (int yy = 0; yy < renderheight + 2; yy++)
			{
				for (int xx = 0; xx < renderwidth + 2; xx++)
				{
					int id = GetTileAt(new Point(blockx + xx, blocky + yy));

					if (id == -1)
						continue;

					//tileSet.Draw(id, new Point(
					//   (xx * level.BlockDimension.Width) + Level.DisplayZone.X - deltax,
					//   (yy * level.BlockDimension.Height) + Level.DisplayZone.Y - deltay));

					Rectangle rect = new Rectangle(
						new Point((xx * Level.BlockDimension.Width) + Level.ViewPort.X - deltax,
									(yy * Level.BlockDimension.Height) + Level.ViewPort.Y - deltay),
						new Size(32, 32)
						);

					tileSet.Draw(id, rect, TextureLayout.Stretch);

		
				
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
						new Point(Level.ViewPort.Left, (yy * Level.BlockDimension.Height - deltay + Level.ViewPort.Top)),
						new Point(Level.ViewPort.Right, (yy * Level.BlockDimension.Height - deltay + Level.ViewPort.Top))
					);
				}
				for (int xx = 0; xx < renderwidth + 2; xx++)
				{
					Display.Line(
						new Point(xx * Level.BlockDimension.Width - deltax + Level.ViewPort.Left, Level.ViewPort.Top),
						new Point(xx * Level.BlockDimension.Width - deltax + Level.ViewPort.Left, Level.ViewPort.Bottom)
					);
				}
			}




			//
			// Draw entities
			//
			if (RenderEntities)
			{
				Display.Color = Color.White;
				foreach (Entity entity in Entities.Values)
				{
					entity.Draw(Level.LevelToScreen(entity.Location));
				}
			}


			//
			// Draw paths
			//
			if (RenderPaths)
			{
				foreach (Path path in paths.Values)
				{
					path.Draw(Level.Location);
				}
			}



			//
			// Draw Spawnpoints
			//
			if (RenderSpawnPoints)
			{
				SpawnPoint spawn;
				

				foreach (string name in GetSpawnPoints())
				{
					spawn = GetSpawnPoint(name);
					if (spawn == null)
						continue;

					Point pos = Level.LevelToScreen(spawn.Location);
					pos.X = pos.X - 8;
					pos.Y = pos.Y - 8;
					spTexture.Blit(pos);
				}
				//spawn = layerPanel.PropertyGridBox.SelectedObject as SpawnPoint;
				//if (spawn != null)
				//{

				//    rect = SpawnPointTexture.Rectangle;
				//    rect.Offset(spawn.CollisionBoxLocation.Location);
				//    rect.Location = level.LevelToScreen(rect.Location);
				//    Video.Rectangle(rect, false);
				//}

			}



		}



		/// <summary>
		/// Updates layer's logic
		/// </summary>
		public void Update(GameTime time)
		{
			// Ask all entities to update
			foreach (Entity entity in Entities.Values)
				entity.Update(time);
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
			if (newsize.Height > Level.Height)
			{
				for (int y = Level.Height; y < newsize.Height; y++)
					InsertRow(y, 0);
			}
			else if (newsize.Height < Level.Height)
			{
				for (int y = Level.Height - 1; y >= newsize.Height; y--)
					RemoveRow(y);
			}


			// Columns
			if (newsize.Width > Level.Width)
			{
				for (int x = Level.Width; x < newsize.Width; x++)
					InsertColumn(x, 0);
			}
			else if (newsize.Width < Level.Width)
			{
				for (int x = Level.Width - 1; x >= newsize.Width; x--)
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

				// Offset objects
				Rectangle zone = new Rectangle(0, rowid * Level.BlockDimension.Height, Level.Dimension.Width, Level.Dimension.Height);
				OffsetObjects(zone, new Point(0, Level.BlockDimension.Height));
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

			// Offset objects
			Rectangle zone = new Rectangle(0, rowid * Level.BlockDimension.Height, Level.Dimension.Width, Level.Dimension.Height);
			OffsetObjects(zone, new Point(0, -Level.BlockDimension.Height));
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

					// Offset objects
					Rectangle zone = new Rectangle(columnid * Level.BlockDimension.Width, 0, Level.Dimension.Width, Level.Dimension.Height);
					OffsetObjects(zone, new Point(Level.BlockDimension.Width, 0));
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

			// Offset objects
			Rectangle zone = new Rectangle(
				columnid * Level.BlockDimension.Width, 0,
				Level.Dimension.Width, Level.Dimension.Height);
			OffsetObjects(zone, new Point(-Level.BlockDimension.Width, 0));
		}


		/// <summary>
		/// Offsets EVERY objects (entities, spawnpoints...) in the layer
		/// </summary>
		/// <param name="zone">Each object in this rectangle</param>
		/// <param name="offset">Offset to move</param>
		void OffsetObjects(Rectangle zone, Point offset)
		{
			// Move entities
			foreach (Entity entity in Entities.Values)
			{
				if (zone.Contains(entity.Location))
				{
					entity.Location = new Point(
						entity.Location.X + offset.X,
						entity.Location.Y + offset.Y);
				}
			}


			// Mode SpawnPoints
			foreach (SpawnPoint spawn in SpawnPoints.Values)
			{
				if (zone.Contains(spawn.Location))
				{
					spawn.Location = new Point(
						spawn.Location.X + offset.X,
						spawn.Location.Y + offset.Y);
				}
			}

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

	//		base.SaveComment(xml);

	
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

			// Loops throughs level brushes
			foreach (LayerBrush brush in brushes.Values)
				brush.Save(xml);


			// Loops throughs spawnpoints
			foreach (SpawnPoint spawn in SpawnPoints.Values)
				spawn.Save(xml);

			// Loops throughs entity
			foreach (Entity entity in Entities.Values)
				entity.Save(xml);

			// Loops throughs tiles
			xml.WriteStartElement("tiles");

			// Texture to use
			xml.WriteStartElement("texture");
			xml.WriteAttributeString("name", textureName);
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


					// Start point
					case "spawnpoint":
					{
						SpawnPoint point = AddSpawnPoint(node.Attributes["name"].Value);
						if (point != null)
							point.Load(node);

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

					// Add entity to the level
					case "entity":
					{
						Entity ent = AddEntity(node.Attributes["name"].Value);
						if (ent != null)
							ent.Load(node);
					}
					break;

					case "brush":
					{
						LayerBrush brush = CreateBrush(node.Attributes["name"].Value);
						if (brush != null)
						{
							brush.Load(node);
						}
					}
					break;


					case "path":
					{
						Path path = CreatePath(node.Attributes["name"].Value);
						if (path != null)
						{
							path.Load(node);
						}
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



		#region Path


		/// <summary>
		/// Creates a new path
		/// </summary>
		/// <param name="name">Name of the path</param>
		/// <returns></returns>
		public Path CreatePath(string name)
		{
			if (paths.ContainsKey(name))
				return null;


			Path path = new Path();
			paths[name] = path;

			return path;
		}


		/// <summary>
		/// Gets a specific path
		/// </summary>
		/// <param name="name">path name</param>
		/// <returns>path handle or null if no path exists</returns>
		public Path GetPath(string name)
		{
			if (!string.IsNullOrEmpty(name) && paths.ContainsKey(name))
				return paths[name];
			else
				return null;

		}


		/// <summary>
		/// Removes a specific path
		/// </summary>
		/// <param name="name">True if path destroyed, else false</param>
		public bool DestroyPath(string name)
		{
			if (name == null)
				return false;

			return paths.Remove(name);
		}

		/// <summary>
		/// Flush unused paths
		/// </summary>
		/// TODO
		public void FlushPaths()
		{
		}


		/// <summary>
		/// Removes all paths
		/// </summary>
		public void DestroyPaths()
		{
			paths.Clear();
		}




		#endregion


		#region Brushes


		/// <summary>
		/// Creates a new brush
		/// </summary>
		/// <param name="name">Name of the brush</param>
		/// <returns></returns>
		public LayerBrush CreateBrush(string name)
		{
			if (brushes.ContainsKey(name))
				return null;


			LayerBrush brush = new LayerBrush();
			brushes[name] = brush;

			return brush;
		}


		/// <summary>
		/// Gets a specific brush
		/// </summary>
		/// <param name="name">brush name</param>
		/// <returns>brush handle or null if no brush exists</returns>
		public LayerBrush GetBrush(string name)
		{
			if (!string.IsNullOrEmpty(name) && brushes.ContainsKey(name))
				return brushes[name];
			else
				return null;

		}


		/// <summary>
		/// Removes a specific brush
		/// </summary>
		/// <param name="name">True if level destroyed, else false</param>
		public bool DestroyBrush(string name)
		{
			if (name == null)
				return false;

			return brushes.Remove(name);
		}

		/// <summary>
		/// Flush unused brushes
		/// </summary>
		/// TODO
		public void FlushBrushes()
		{
		}


		/// <summary>
		/// Removes all brushes
		/// </summary>
		public void DestroyBrushes()
		{
			brushes.Clear();
		}




		#endregion


		#region Entities

		/// <summary>
		/// Adds an entity to the layer
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Entity AddEntity(string name)
		{
			if (Entities.ContainsKey(name))
				return null;

			Entity ent = new Entity(this);

			Entities[name] = ent;
			return ent;
		}


		/// <summary>
		/// Gets a list of all entities in the level
		/// </summary>
		/// <returns></returns>
		public List<string> GetEntities()
		{
			List<string> list = new List<string>();

			foreach (string key in Entities.Keys)
				list.Add(key);

			list.Sort();
			return list;
		}


		/// <summary>
		/// Finds an entity by his location
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public Entity FindEntity(Point point)
		{
			foreach (Entity ent in Entities.Values)
			{
				// The entity has no sprite defined
				//if (ent.SpriteCell == null)
				//	continue;


				Rectangle pos = ent.CollisionBoxLocation;
				//pos.Offset(ent.Location);

				if (pos.Contains(point))
					return ent;
			}

			return null;
		}



		/// <summary>
		/// Gets an entity
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Entity GetEntity(string name)
		{
			if (Entities.ContainsKey(name))
				return (Entity)Entities[name];
			else
				return null;
		}




		/// <summary>
		/// Removes an entity from the layer
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool RemoveEntity(string name)
		{
			Entity ent = GetEntity(name);
			if (ent == null)
				return false;


			Entities.Remove(name);

			return true;
		}

		#endregion


		#region Spawn points


		/// <summary>
		/// Adds a SpawnPoint to the layer
		/// </summary>
		/// <param name="name"></param>
		public SpawnPoint AddSpawnPoint(string name)
		{
			SpawnPoint point = new SpawnPoint();
			SpawnPoints[name] = point;

			return point;
		}

		/// <summary>
		/// Removes a SpawnPoint
		/// </summary>
		/// <param name="name"></param>
		public void RemoveSpawnPoint(string name)
		{
			if (GetSpawnPoint(name) != null)
				SpawnPoints.Remove(name);
		}


		/// <summary>
		/// Returns a specific SpawnPoint
		/// </summary>
		/// <param name="name">Name of the SpawnPoint</param>
		/// <returns>A SpawnPoint class</returns>
		public SpawnPoint GetSpawnPoint(string name)
		{
			if (name == null && name.Length == 0)
				return null;

			if (SpawnPoints.ContainsKey(name))
				return (SpawnPoint)SpawnPoints[name];
			else
				return null;
		}


		/// <summary>
		/// Returns all SpawnPoints
		/// </summary>
		/// <returns></returns>
		public List<string> GetSpawnPoints()
		{
			List<string> list = new List<string>();

			foreach (string key in SpawnPoints.Keys)
			{
				list.Add(key);
			}

			return list;
		}


		/// <summary>
		/// Finds an entity by his location
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public SpawnPoint FindSpawnPoint(Point point)
		{
			foreach (SpawnPoint spawn in SpawnPoints.Values)
			{
				if (spawn.CollisionBoxLocation.Contains(point))
					return spawn;
			}

			return null;
		}


		#endregion


		#region Tiles


		/// <summary>
		/// Returns the BufferID of the tile at location (in block) (x,y)
		/// </summary>
		/// <param name="point">Point in the layer in block</param>
		/// <returns>ID of the block</returns>
		public int GetTileAt(Point point)
		{
			if (point.Y < 0 || point.Y >= Tiles.Count)
				return -1;

			if (point.X < 0 || point.X >= Tiles[point.Y].Count)
				return -1;

			return Tiles[point.Y][point.X];
		}


		/// <summary>
		/// Returns the BufferID of the tile at location (in pixel) (x,y)
		/// </summary>
		/// <param name="point">Point in the layer in pixel</param>
		/// <returns>ID of the tile</returns>
		public int GetTileAtCoord(Point point)
		{
			if (Level.BlockDimension.Width == 0 || Level.BlockDimension.Height == 0)
				return -1;

			Point p = new Point(point.X / Level.BlockDimension.Width, point.Y / Level.BlockDimension.Height);

			return GetTileAt(p);
		}



		/// <summary>
		/// Sets the tile BufferID at the given location (in block)
		/// </summary>
		/// <param name="point">Offset in block in the layer</param>
		/// <param name="BufferID">ID of the block to paste</param>
		public void SetTileAt(Point point, int id)
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
		public void SetTileAtCoord(Point point, int id)
		{
			Point p = Level.PositionToBlock(point);

			SetTileAt(p, id);
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
			Layer layer = entity.ParentLayer;


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
					res.FinalVelocity.Y = velocity.Y - (entity.Location.Y + velocity.Y) % layer.Level.BlockDimension.Height - 1;
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
						(entity.Location.X + velocity.X + entity.CollisionBox.Width) % layer.Level.BlockDimension.Width;
				}
				else
					res.FinalVelocity.X = velocity.X;
			}
			else if (velocity.X < 0)    // Left
			{
				if (colblock.TopLeft != 0 || colblock.BottomLeft != 0)
				{
					res.FinalVelocity.X = velocity.X +
					   layer.Level.BlockDimension.Width - (entity.CollisionBoxLocation.Left + velocity.X) % layer.Level.BlockDimension.Width;
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
			int targetTile = GetTileAt(pt);

			// First fill
			LinearFill(pt, id);

			// While exists line to fill
			while (ranges.Count > 0)
			{
				FloodFillRange range = ranges.Dequeue();

				for (int x = range.StartX; x <= range.EndX; x++)
				{
					// Check upward
					if (range.Y > 0 && GetTileAt(new Point(x, range.Y - 1)) == targetTile)
						LinearFill(new Point(x, range.Y - 1), id);

					// Check downward
					if (range.Y < Level.Size.Height + 1 && GetTileAt(new Point(x, range.Y + 1)) == targetTile)
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
			int targetId = GetTileAt(pt);
			if (targetId == id)
				return;


			int minx = 0;
			int maxx = 0;


			// Check on the left
			for (minx = pt.X; minx >= 0; minx--)
			{
				Point pos = new Point(minx, pt.Y);
				if (GetTileAt(pos) != targetId)
					break;

				SetTileAt(pos, id);
			}
			minx++;

			// Check on the right
			for (maxx = pt.X + 1; maxx <= Level.Size.Width; maxx++)
			{
				Point pos = new Point(maxx, pt.Y);
				if (GetTileAt(pos) != targetId)
					break;

				SetTileAt(pos, id);
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
		/// All path in the layer
		/// </summary>
		[Browsable(false)]
		public List<Path> Paths
		{
			get
			{
				List<Path> list = new List<Path>();

				return list;
			}
		}
		Dictionary<string, Path> paths;

		/// <summary>
		/// Layer Brushes
		/// </summary>
		[Browsable(false)]
		public List<LayerBrush> Brushes
		{
			get
			{
				List<LayerBrush> list = new List<LayerBrush>();

				foreach (LayerBrush brush in brushes.Values)
					list.Add(brush);

				//list.Sort();
				return list;
			}
		}
		Dictionary<string, LayerBrush> brushes;

		
		
		/// <summary>
		/// Contain all the tile of the layer
		/// </summary>
		List<List<int>> Tiles;

		/// <summary>
		/// Private TileSet
		/// </summary>
		[Browsable(false)]
		public TileSet TileSet
		{
			get { return tileSet; }
		}
		TileSet tileSet;


		/// <summary>
		/// Gets/sets the layer visiblity / shall we draws it ?
		/// </summary>
		[Category("Layer")]
		[Description("Layer's visibility")]
		public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
			}
		}
		bool visible;


		/// <summary>
		/// Gets the current texture
		/// </summary>
		[Browsable(false)]
		public Texture Texture
		{
			get
			{
				if (tileSet == null) return null;

				return tileSet.Texture;
			}
		}


		/// <summary>
		/// Gets/sets the name of the texture to use with this layer
		/// </summary>
		[Category("Tiles")]
		[Description("Texture to use")]
	//	[TypeConverter(typeof(TextureEnumerator))]
		public string TextureName
		{
			get { return textureName; }
			set
			{
				textureName = value;

				tileSet.TextureName = value;
				/*
								if (texture != null)
									texture.Unlock();

								textureName = value;
								texture = ResourceManager.Handle.GetTexture(textureName);
								if (texture != null)
									texture.Lock();
				*/

				// Rebuild the tileset
				BuildTileSet();

			}
		}
		string textureName;


		/// <summary>
		/// Holds every spawn points of the level
		/// </summary>
		Dictionary<string, SpawnPoint> SpawnPoints;


		/// <summary>
		/// Holds a list of all entities in the map (while playing)
		/// </summary>
		Dictionary<string, Entity> Entities;


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


		/// <summary>
		/// Script's handle
		/// </summary>
		ILayer ScriptInterface;

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



		/// <summary>
		/// Displays or not entities in the layer
		/// </summary>
		public bool RenderEntities
		{
			get;
			set;
		}



		/// <summary>
		/// Displays or not paths in the layer
		/// </summary>
		public bool RenderPaths
		{
			get;
			set;
		}

		/// <summary>
		/// Displays or not SpawnPoints in the layer
		/// </summary>
		public bool RenderSpawnPoints
		{
			get;
			set;
		}


		/// <summary>
		/// SpawnPoint texture
		/// </summary>
		Texture spTexture;


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

			if (entity == null || entity.ParentLayer == null)
				return col;

			Layer layer = entity.ParentLayer;

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
			layer = entity.ParentLayer.Level.CollisionLayer;
			if (layer != null)
			{
				col.TopLeft = layer.GetTileAtCoord(topLeft);
				col.TopRight = layer.GetTileAtCoord(topRight);
				col.BottomLeft = layer.GetTileAtCoord(bottomLeft);
				col.BottomRight = layer.GetTileAtCoord(bottomRight);
			}


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

			Layer layer = entity.ParentLayer;
			if (layer == null)
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
			Tile.TopLeft = layer.GetTileAtCoord(Location.TopLeft);
			Tile.TopRight = layer.GetTileAtCoord(Location.TopRight);
			Tile.BottomLeft = layer.GetTileAtCoord(Location.BottomLeft);
			Tile.BottomRight = layer.GetTileAtCoord(Location.BottomRight);


			// Collision blocks
			layer = entity.ParentLayer.Level.CollisionLayer;
			if (layer != null)
			{
				Collision.TopLeft = layer.GetTileAtCoord(Location.TopLeft);
				Collision.TopRight = layer.GetTileAtCoord(Location.TopRight);
				Collision.BottomLeft = layer.GetTileAtCoord(Location.BottomLeft);
				Collision.BottomRight = layer.GetTileAtCoord(Location.BottomRight);
			}


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


}



