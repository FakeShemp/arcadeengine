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
			TileLayer = new Layer(this);
			CollisionLayer = new Layer(this);
			Paths = new Dictionary<string, Path>();

			Entities = new Dictionary<string, Entity>();
			Camera = new Camera(this);
			Brushes = new Dictionary<string,LayerBrush>();
			SpawnPoints = new Dictionary<string, SpawnLocation>();
			BlockSize = new Size(16, 16);
		}


		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			if (TileLayer != null)
				TileLayer.Dispose();
			TileLayer = null;

			if (spTexture != null)
				spTexture.Dispose();
			spTexture = null;

			if (CollisionLayer != null)
				CollisionLayer.Dispose();
			CollisionLayer = null;


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
			TileLayer.Save(xml);
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
						TileLayer.Load(node.FirstChild);
					}
					break;

					case "collision":
					{
						CollisionLayer.Load(node.FirstChild);
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

			TileLayer.SetSize(Size);
			CollisionLayer.SetSize(Size);
		}



		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="row">Rowid to remove</param>
		/// <param name="tileid">Tile BufferID</param>
		public void InsertRow(int row, int tileid)
		{
			TileLayer.InsertRow(row, tileid);
			CollisionLayer.InsertRow(row, tileid);

			// Resize the level
			Size = new Size(Size.Width, Size.Height + 1);

			// Offset objects
			Rectangle zone = new Rectangle(0, row * BlockDimension.Height, SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Point(0, BlockDimension.Height));
		}



		/// <summary>
		/// Removes a row
		/// </summary>
		/// <param name="row">Row ID to remove</param>
		public void RemoveRow(int row)
		{
			TileLayer.RemoveRow(row);
			CollisionLayer.RemoveRow(row);

			Size = new Size(Size.Width, Size.Height - 1);

			// Offset objects
			Rectangle zone = new Rectangle(0, row * BlockDimension.Height, SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Point(0, -BlockDimension.Height));

		}



		/// <summary>
		/// Inserts a column
		/// </summary>
		/// <param name="column">Column BufferID to insert</param>
		/// <param name="tileid">Tile BufferID</param>
		public void InsertColumn(int column, int tileid)
		{
			TileLayer.InsertColumn(column, tileid);
			CollisionLayer.InsertColumn(column, tileid);

			Size = new Size(Size.Width + 1, Size.Height);

			// Offset objects
			Rectangle zone = new Rectangle(column * BlockDimension.Width, 0, SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Point(BlockDimension.Width, 0));

		}


		/// <summary>
		/// Removes a column
		/// </summary>
		/// <param name="column">Columns ID to remove</param>
		public void RemoveColumn(int column)
		{
			TileLayer.RemoveColumn(column);
			CollisionLayer.RemoveColumn(column);

			Size = new Size(Size.Width - 1, Size.Height);

			// Offset objects
			Rectangle zone = new Rectangle(
				column * BlockDimension.Width, 0,
				SizeInPixel.Width, SizeInPixel.Height);
			OffsetObjects(zone, new Point(-BlockDimension.Width, 0));

		}


		/// <summary>
		/// Offsets EVERY objects (entities, spawnpoints...) in the layer
		/// </summary>
		/// <param name="zone">Each object in this rectangle</param>
		/// <param name="offset">Offset to move</param>
		void OffsetObjects(Rectangle zone, Point offset)
		{

			//// Move entities
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
			foreach (SpawnLocation spawn in SpawnPoints.Values)
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
				entity.Init();


			// Load the SpawnPoint texture
			spTexture = new Texture2D(ResourceManager.GetInternalResource("RuffnTumble.Resources.SpawnPoint.png"));


			// All ok
			LevelReady = true;
			return true;
		}


		/// <summary>
		/// Draws the level
		/// </summary>
		/// <param name="batch"></param>
		public void Draw(SpriteBatch batch)
		{
			if (batch == null)
				return;

			// Begin the draw
			//Video.ScissorZone = displayZone;
			//Video.Scissor = true;
			//Video.Translate = new Point(displayZone.X, displayZone.Y);



			TileLayer.Draw(batch, Camera);

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

					Point pos = LevelToScreen(spawn.Location);
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
					entity.Draw(batch, LevelToScreen(entity.Location));
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


			//Video.Offset = new Point();
			//Video.Scissor = false;
		}



		/// <summary>
		/// Updates all layers
		/// </summary>
		public void Update(GameTime time)
		{
			// Ask all entities to update
			foreach (Entity entity in Entities.Values)
				entity.Update(time);

		}







		/// <summary>
		/// Converts level location to screen location
		/// </summary>
		/// <param name="pos">Point in pixel in the level</param>
		/// <returns>Position in screen coordinate</returns>
		public Point LevelToScreen(Point pos)
		{
			return new Point(pos.X - Camera.Location.X + Camera.ViewPort.Left,
				pos.Y - Camera.Location.Y + Camera.ViewPort.Top);
		}


		/// <summary>
		/// Converts screen location to level location
		/// </summary>
		/// <param name="pos">Position in screen coordinate</param>
		/// <returns>Point in pixel in the level</returns>
		public Point ScreenToLevel(Point pos)
		{
			return new Point(
				Camera.Location.X + pos.X - Camera.ViewPort.Left,
				Camera.Location.Y + pos.Y - Camera.ViewPort.Top);
		}



		/// <summary>
		/// Returns the block coordinate at screen location
		/// </summary>
		/// <param name="point">GameScreen location</param>
		/// <returns>Block coordinate in the layer</returns>
		public Point PositionToBlock(Point point)
		{
			if (BlockDimension.Width == 0 || BlockDimension.Height == 0)
				return Point.Empty;

			return new Point(point.X / BlockDimension.Width, point.Y / BlockDimension.Height);
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
		public SpawnLocation FindSpawnPoint(Point point)
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


				Rectangle pos = Rectangle.Empty; //ent.CollisionBoxLocation;
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
		/// If true, level is ready to be used. Else call Init() to make it ready to use.
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
				return new Size(BlockDimension.Width * Size.Width, BlockDimension.Height * Size.Height);
			}
		}


		/// <summary>
		/// Size of the blocks in pixel (not including the zoom factor)
		/// </summary>
		[CategoryAttribute("Tiles")]
		[Description("Size of the block in the tile texture")]
		public Size BlockSize
		{
			get;
			set;
		}


		/// <summary>
		/// Gets the dimension in pixel of a block (including the zoom factor)
		/// </summary>
		[Browsable(false)]
		public Size BlockDimension
		{
			get
			{
				return new Size((int)(BlockSize.Width * Camera.Scale.X), (int)(BlockSize.Height * Camera.Scale.Y));
			}
		}

/*
		/// <summary>
		/// Gets/sets the location (in pixel) in the layer (aka : scrolling).
		/// </summary>
		[Browsable(false)]
		public Point Location
		{
			get { return location; }
			set
			{
				location = value;

				if (location.X > (Size.Width * BlockDimension.Width) - Camera.ViewPort.Width)
					location.X = Size.Width * BlockDimension.Width - Camera.ViewPort.Width;
				if (location.Y > (Size.Height * BlockDimension.Height) - Camera.ViewPort.Height)
					location.Y = Size.Height * BlockDimension.Height - Camera.ViewPort.Height;

				if (location.X < 0) location.X = 0;
				if (location.Y < 0) location.Y = 0;
			}
		}
		Point location;
*/




		/// <summary>
		/// Tiles layer
		/// </summary>
		[Browsable(false)]
		public Layer TileLayer
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
