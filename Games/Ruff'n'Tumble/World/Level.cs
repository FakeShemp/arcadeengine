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
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine;
using System;
using ArcEngine.Input;

//
// - Ajouter un tag <gravity x="0.0" y="0.0" /> dans le tag <layer> pour definir la gravite du layer
//
//
// http://citrusengine.com/manual/manual/props
// http://citrusengine.com/manual/manual/levels
//
//
namespace RuffnTumble
{
	/// <summary>
	/// A level
	/// </summary>
	public class Level : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Level()
		{
			ForegroundLayer = new Layer(this);
			CollisionLayer = new Layer(this);
			CollisionLayer.Visible = false;

			Paths = new Dictionary<string, Path>();

			Entities = new Dictionary<string, Entity>();
			Camera = new Camera(this);
			Brushes = new Dictionary<string,LayerBrush>();
			SpawnPoints = new Dictionary<string, SpawnLocation>();
			BlockSize = new Size(16, 16);

			Player = new Player(this);
		}


		/// <summary>
		/// Dispose all resources
		/// </summary>
		public void Dispose()
		{
			if (Player != null)
				Player.Dispose();
			Player = null;

			if (ForegroundLayer != null)
				ForegroundLayer.Dispose();
			ForegroundLayer = null;

			if (spTexture != null)
				spTexture.Dispose();
			spTexture = null;

			if (CollisionLayer != null)
				CollisionLayer.Dispose();
			CollisionLayer = null;

			Brushes.Clear();
			Paths.Clear();
			SpawnPoints.Clear();



			foreach (var entity in Entities)
			{
				if (entity.Value != null)
					entity.Value.Dispose(); 
			}
			Entities.Clear();

	
			Size = Size.Empty;
			Camera = null;
			BlockSize = Size.Empty;
		}


		/// <summary>
		/// Gets the collision of the tile at a given location
		/// </summary>
		/// <param name="location">Tile location</param>
		/// <returns>TileCollision value</returns>
		public TileCollision GetCollision(int x, int y)
		{
			// Prevent escaping past the level ends.
			if (x < 0 || x >= Size.Width)
				return TileCollision.Impassable;

			// Allow jumping past the level top and falling through the bottom.
			if (y < 0 || y >= Size.Height)
				return TileCollision.Passable;

			int res = CollisionLayer.GetTileAtBlock(x, y);

			if (res == 1)
				return TileCollision.Impassable;

			if (res == 2)
				return TileCollision.Platform;

			return TileCollision.Passable;
		}


		/// <summary>
		/// Gets the bounding rectangle of a tile in world space.
		/// </summary>     
		/// <param name="x">X block coordinate</param>
		/// <param name="y">Y block coordinate</param>
		public Rectangle GetBounds(int x, int y)
		{
			return new Rectangle(x * BlockSize.Width, y * BlockSize.Height, BlockSize.Width, BlockSize.Height);
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


			xml.WriteStartElement("level");
			xml.WriteAttributeString("name", Name);


			xml.WriteStartElement("size");
			xml.WriteAttributeString("width", Size.Width.ToString());
			xml.WriteAttributeString("height", Size.Height.ToString());
			xml.WriteEndElement();

			//xml.WriteStartElement("zoom");
			//xml.WriteAttributeString("width", Scale.Width.ToString());
			//xml.WriteAttributeString("height", Scale.Height.ToString());
			//xml.WriteEndElement();

			xml.WriteStartElement("blocksize");
			xml.WriteAttributeString("width", BlockSize.Width.ToString());
			xml.WriteAttributeString("height", BlockSize.Height.ToString());
			xml.WriteEndElement();


			xml.WriteStartElement("tiles");
			ForegroundLayer.Save(xml);
			xml.WriteEndElement();
			
			xml.WriteStartElement("collisions");
			CollisionLayer.Save(xml);
			xml.WriteEndElement();

			// Loops throughs layers
			//foreach (Layer layer in Layers)
			//    layer.Save(xml);

			// Loops through level brushes
			foreach (LayerBrush brush in Brushes.Values)
				brush.Save(xml);

			// Loops through spawnpoints
			foreach (SpawnLocation spawn in SpawnPoints.Values)
				spawn.Save(xml);


			// Loops through entity
			foreach (Entity entity in Entities.Values)
				entity.Save(xml);


			xml.WriteEndElement();

			return true;
		}

		/// <summary>
		/// Loads the level from a bank
		/// </summary>
		/// <param name="xml">xml node</param>
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
					continue;

				switch (node.Name.ToLower())
				{
					// Start point
					case "spawnpoint":
					{
						SpawnLocation point = AddSpawnPoint(node.Attributes["name"].Value);
						if (point != null)
							point.Load(node);
					}
					break;
					
					case "brush":
					{
						LayerBrush brush = CreateBrush(node.Attributes["name"].Value);
						if (brush != null)
							brush.Load(node);
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

					case "path":
					{
						Path path = CreatePath(node.Attributes["name"].Value);
						if (path != null)
						{
							path.Load(node);
						}
					}
					break;

					// Tiles of the level
					case "tiles":
					{
						ForegroundLayer.Load(node);
					}
					break;

					case "collision":
					{
						CollisionLayer.Load(node);
					}
					break;


					// Size
					case "size":
					{
						Resize(new Size(int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value)));
					}
					break;


					case "blocksize":
					{
						BlockSize = new Size(int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value));
					}
					break;

					default:
					{
						Trace.WriteLine("Level '{0}' : Unknown node element found (\"{1}\")", Name, node.Name);
					}
					break;
				}
			}

			return true;
		}

		#endregion


		#region Layers
/*

		/// <summary>
		/// Adds a layer to the level
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Layer AddLayer(string name)
		{
			Layer layer = new Layer(this);

			Layers.Add(layer);

			return layer;
		}



		/// <summary>
		/// Removes a layer from the level
		/// </summary>
		/// <param name="layer"></param>
		public void RemoveLayer(Layer layer)
		{
			Layers.Remove(layer);
		}



		/// <summary>
		/// Gets  a layer by his bame
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Layer GetLayer(string name)
		{
			foreach (Layer layer in Layers)
			{
				if (layer.Name == name)
				   return layer;
			}

			return null;
		}



		/// <summary>
		///  Reorders layers
		/// </summary>
		/// <param name="layer"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public void SetLayerZIndex(Layer layer, int index)
		{

			Layers.Insert(index, layer);
			Layers.Remove(layer);
		}
*/
		#endregion


		#region Resize, Insert and Delete

		/// <summary>
		/// Resize the level. Erase all layers
		/// </summary>
		/// <param name="newsize">Desired size</param>
		public void Resize(Size newsize)
		{
			// Resize
			Size = newsize;

			ForegroundLayer.SetSize(Size);
			CollisionLayer.SetSize(Size);
		}



		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="row">Rowid to remove</param>
		/// <param name="tileid">Tile BufferID</param>
		public void InsertRow(int row, int tileid)
		{
			ForegroundLayer.InsertRow(row, tileid);
			CollisionLayer.InsertRow(row, tileid);

			// Resize the level
			Size = new Size(Size.Width, Size.Height + 1);

			// Offset objects
			Vector4 zone = new Vector4(0, row * BlockSize.Height, SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Vector2(0, BlockSize.Height));
		}



		/// <summary>
		/// Removes a row
		/// </summary>
		/// <param name="row">Row ID to remove</param>
		public void RemoveRow(int row)
		{
			ForegroundLayer.RemoveRow(row);
			CollisionLayer.RemoveRow(row);

			Size = new Size(Size.Width, Size.Height - 1);

			// Offset objects
			Vector4 zone = new Vector4(0, row * BlockSize.Height, SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Vector2(0, -BlockSize.Height));

		}



		/// <summary>
		/// Inserts a column
		/// </summary>
		/// <param name="column">Column BufferID to insert</param>
		/// <param name="tileid">Tile BufferID</param>
		public void InsertColumn(int column, int tileid)
		{
			ForegroundLayer.InsertColumn(column, tileid);
			CollisionLayer.InsertColumn(column, tileid);

			Size = new Size(Size.Width + 1, Size.Height);

			// Offset objects
			Vector4 zone = new Vector4(column * BlockSize.Width, 0, SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Vector2(BlockSize.Width, 0));

		}


		/// <summary>
		/// Removes a column
		/// </summary>
		/// <param name="column">Columns ID to remove</param>
		public void RemoveColumn(int column)
		{
			ForegroundLayer.RemoveColumn(column);
			CollisionLayer.RemoveColumn(column);

			Size = new Size(Size.Width - 1, Size.Height);

			// Offset objects
			Vector4 zone = new Vector4(
				column * BlockSize.Width, 0,
				SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Vector2(-BlockSize.Width, 0));

		}


		/// <summary>
		/// Offsets EVERY objects (entities, spawnpoints...) in the layer
		/// </summary>
		/// <param name="zone">Each object in this rectangle</param>
		/// <param name="offset">Offset to move</param>
		void OffsetObjects(Vector4 zone, Vector2 offset)
		{

			//// Move entities
			foreach (Entity entity in Entities.Values)
			{
				if (zone.Contains(entity.Position))
				{
					entity.Position = new Vector2(
						 entity.Position.X + offset.X,
						 entity.Position.Y + offset.Y);
				}
			}

			// Mode SpawnPoints
			foreach (SpawnLocation spawn in SpawnPoints.Values)
			{
				if (zone.Contains(spawn.Location))
				{
					spawn.Location = new Vector2(
						spawn.Location.X + offset.X,
						spawn.Location.Y + offset.Y);
				}
			}
		}

		#endregion


		#region Methods

		/// <summary>
		/// Call this function to load all resources needed by the level.
		/// </summary>
		/// <returns>If returns true, all resources are found and locked. Level is ready to be played</returns>
		public bool Init()
		{
			if (LevelReady)
				return true;


			// Init all entities
			foreach (Entity entity in Entities.Values)
				if (entity != null)
					entity.Init();


			// Load the SpawnPoint texture
			spTexture = new Texture2D(ResourceManager.GetInternalResource("RuffnTumble.Resources.SpawnPoint.png"));

			Camera.Target = Player;

			// All ok
			LevelReady = true;
			return true;
		}


		/// <summary>
		/// Draws the level
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		public void Draw(SpriteBatch batch)
		{
			if (batch == null)
				return;

			// Draw the layer
			ForegroundLayer.Draw(batch, Camera);

			Player.Draw(batch, Camera);


			// Draw collision layer
			CollisionLayer.Draw(batch, Camera);

			//
			// Draw Spawnpoints
			//
			if (RenderSpawnPoints)
			{
				SpawnLocation spawn;


				foreach (string name in GetSpawnPoints())
				{
					spawn = GetSpawnPoint(name);
					if (spawn == null)
						continue;

					Vector2 pos = LevelToScreen(spawn.Location);
					pos.X = pos.X - 8;
					pos.Y = pos.Y - 8;
					batch.Draw(spTexture, pos, Color.White);
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

			//
			// Draw entities
			//
			if (RenderEntities)
			{
				foreach (Entity entity in Entities.Values)
				{
					if (entity != null)
						entity.Draw(batch, Camera);
				}
			}


			//
			// Draw paths
			//
			if (RenderPaths)
			{
				foreach (Path path in Paths.Values)
				{
					path.Draw(batch, Camera);
				}
			}


		}



		/// <summary>
		/// Updates all layers
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{

			Player.Update(time);
			
			// Ask all entities to update
			foreach (Entity entity in Entities.Values)
				if (entity != null)
					entity.Update(time);


			// Update the camera
			Camera.Update(time);


			if (Keyboard.IsNewKeyPress(System.Windows.Forms.Keys.Space))
				CollisionLayer.Visible = !CollisionLayer.Visible;
		}







		/// <summary>
		/// Converts level location to screen location
		/// </summary>
		/// <param name="pos">Point in pixel in the level</param>
		/// <returns>Position in screen coordinate</returns>
		public Vector2 LevelToScreen(Vector2 pos)
		{
			return new Vector2(pos.X - Camera.Location.X + Camera.ViewPort.Left,
							pos.Y - Camera.Location.Y + Camera.ViewPort.Top);
		}


		/// <summary>
		/// Converts screen location to level location
		/// </summary>
		/// <param name="pos">Position in screen coordinate</param>
		/// <returns>Point in pixel in the level</returns>
		public Vector2 ScreenToLevel(Point pos)
		{
			return new Vector2(
				Camera.Location.X + pos.X - Camera.ViewPort.Left,
				Camera.Location.Y + pos.Y - Camera.ViewPort.Top);
		}



		/// <summary>
		/// Returns the block coordinate at screen location
		/// </summary>
		/// <param name="point">GameScreen location</param>
		/// <returns>Block coordinate in the layer</returns>
		public Point PositionToBlock(Vector2 point)
		{
			if (BlockSize.Width == 0 || BlockSize.Height == 0)
				return Point.Empty;

			return new Point((int)point.X / BlockSize.Width, (int)point.Y / BlockSize.Height);
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
			if (Brushes.ContainsKey(name))
				return null;


			LayerBrush brush = new LayerBrush();
			Brushes[name] = brush;

			return brush;
		}


		/// <summary>
		/// Gets a specific brush
		/// </summary>
		/// <param name="name">brush name</param>
		/// <returns>brush handle or null if no brush exists</returns>
		public LayerBrush GetBrush(string name)
		{
			if (!string.IsNullOrEmpty(name) && Brushes.ContainsKey(name))
				return Brushes[name];
			else
				return null;

		}


		/// <summary>
		/// Gets a list of all <see cref="Brush"/>es
		/// </summary>
		/// <returns></returns>
		public List<string> GetBrushes()
		{
			List<string> list = new List<string>();

			foreach (string name in Brushes.Keys)
				list.Add(name);

			return list;
		}


		/// <summary>
		/// Removes a specific brush
		/// </summary>
		/// <param name="name">True if level destroyed, else false</param>
		public bool DestroyBrush(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;

			return Brushes.Remove(name);
		}


		/// <summary>
		/// Removes all brushes
		/// </summary>
		public void DestroyBrushes()
		{
			Brushes.Clear();
		}




		#endregion


		#region Spawn points


		/// <summary>
		/// Adds a SpawnPoint to the layer
		/// </summary>
		/// <param name="name"></param>
		public SpawnLocation AddSpawnPoint(string name)
		{
			SpawnLocation point = new SpawnLocation();
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
		public SpawnLocation GetSpawnPoint(string name)
		{
			if (name == null && name.Length == 0)
				return null;

			if (SpawnPoints.ContainsKey(name))
				return (SpawnLocation)SpawnPoints[name];
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
		public SpawnLocation FindSpawnPoint(Vector2 point)
		{
			foreach (SpawnLocation spawn in SpawnPoints.Values)
			{
				if (spawn.CollisionBoxLocation.Contains(point))
					return spawn;
			}

			return null;
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

			Entity ent = null; // new Entity(this);

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
		/// <param name="point">Location in the level</param>
		/// <returns></returns>
		public Entity FindEntity(Vector2 point)
		{
			foreach (Entity ent in Entities.Values)
			{
				// The entity has no sprite defined
				//if (ent.SpriteCell == null)
				//	continue;


				Vector4 pos = Vector4.Zero; //ent.CollisionBoxLocation;
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


		#region Path


		/// <summary>
		/// Creates a new path
		/// </summary>
		/// <param name="name">Name of the path</param>
		/// <returns></returns>
		public Path CreatePath(string name)
		{
			if (Paths.ContainsKey(name))
				return null;


			Path path = new Path();
			Paths[name] = path;

			return path;
		}


		/// <summary>
		/// Gets a specific path
		/// </summary>
		/// <param name="name">path name</param>
		/// <returns>path handle or null if no path exists</returns>
		public Path GetPath(string name)
		{
			if (!string.IsNullOrEmpty(name) && Paths.ContainsKey(name))
				return Paths[name];
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

			return Paths.Remove(name);
		}


		/// <summary>
		/// Removes all paths
		/// </summary>
		public void DestroyPaths()
		{
			Paths.Clear();
		}




		#endregion


		#region Properties

		/// <summary>
		/// Name of the level
		/// </summary>
		public string Name
		{
			get;
			set;
		}



		/// <summary>
		/// Player handle
		/// </summary>
		public Player Player
		{
			get;
			private set;
		}



		/// <summary>
		/// If true, level is ready to be used. 
		/// Else call Init() to make it ready to use.
		/// </summary>
		[Browsable(false)]
		public bool LevelReady
		{
			get;
			private set;
		}


		/// <summary>
		/// Size of the level in block
		/// </summary>
		[CategoryAttribute("Tiles")]
		[Description("Size of the layer in block")]
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Dimension of the level in pixel
		/// </summary>
		public Size SizeInPixel
		{
			get
			{
				return new Size(BlockSize.Width * Size.Width, BlockSize.Height * Size.Height);
			}
		}


		/// <summary>
		/// Size of the blocks in pixel
		/// </summary>
		[CategoryAttribute("Tiles")]
		[Description("Size of the block in the tile texture")]
		public Size BlockSize
		{
			get;
			set;
		}


		/// <summary>
		/// Foreground tiles layer
		/// </summary>
		[Browsable(false)]
		public Layer ForegroundLayer
		{
			get;
			private set;
		}



		/// <summary>
		/// Background tiles layer
		/// </summary>
		[Browsable(false)]
		public Layer BackgroundLayer
		{
			get;
			private set;
		}


		/// <summary>
		/// Collision layer
		/// </summary>
		[Browsable(false)]
		public Layer CollisionLayer
		{
			get;
			private set;
		}



		/// <summary>
		/// Camera of the level
		/// </summary>
		public Camera Camera
		{
			get;
			private set;
		}




		/// <summary>
		///  Brushes
		/// </summary>
		Dictionary<string, LayerBrush> Brushes;



		/// <summary>
		/// Holds every spawn points of the level
		/// </summary>
		Dictionary<string, SpawnLocation> SpawnPoints;


		/// <summary>
		/// SpawnPoint texture
		/// </summary>
		Texture2D spTexture;


		/// <summary>
		/// Displays or not SpawnPoints in the layer
		/// </summary>
		public bool RenderSpawnPoints
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
		/// Holds a list of all entities in the map (while playing)
		/// </summary>
		Dictionary<string, Entity> Entities;



		/// <summary>
		/// All path in the level
		/// </summary>
		Dictionary<string, Path> Paths;



		#endregion
	}

}
