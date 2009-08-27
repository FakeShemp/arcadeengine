using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine;
//
// - Ajouter un tag <gravity x="0.0" y="0.0" /> dans le tag <layer> pour definir la gravite du layer
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
namespace RuffnTumble.Asset
{
	/// <summary>
	/// 
	/// </summary>
	public class Level : IAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of the level</param>
		public Level()//string name) : base(name)
		{
			layers = new List<Layer>();
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
			//xml.WriteAttributeString("name", Name);

			//base.SaveComment(xml);


			xml.WriteStartElement("size");
			xml.WriteAttributeString("width", Size.Width.ToString());
			xml.WriteAttributeString("height", Size.Height.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("zoom");
			xml.WriteAttributeString("width", Zoom.Width.ToString());
			xml.WriteAttributeString("height", Zoom.Height.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("blocksize");
			xml.WriteAttributeString("width", BlockSize.Width.ToString());
			xml.WriteAttributeString("height", BlockSize.Height.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("collisionlayer");
			xml.WriteAttributeString("name", collisionLayerName);
			xml.WriteEndElement();


			// Loops throughs layers
			foreach (Layer layer in layers)
				layer.Save(xml);


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


			// Process datas
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
					// Layer
					case "layer":
					{
						string name = node.Attributes["name"].Value;
						Layer layer = AddLayer(name);
						if (layer != null)
							layer.Load(node);

					}
					break;

					// Collision layer
					case "collisionlayer":
					{
						collisionLayerName = node.Attributes["name"].Value.ToString();
					}
					break;


					// Size
					case "size":
					{
						size = new Size(int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value));
					}
					break;

					case "zoom":
					{
						zoom.Width = float.Parse(node.Attributes["width"].Value);
						zoom.Height = float.Parse(node.Attributes["height"].Value);
					}
					break;

					case "blocksize":
					{
						blockSize = new Size(int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value));
					}
					break;

					default:
					{
						Trace.WriteLine("Level : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}

		#endregion



		#region Layers


		/// <summary>
		/// Adds a layer to the level
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Layer AddLayer(string name)
		{
			Layer layer = new Layer(this);

			layers.Add(layer);

			return layer;
		}



		/// <summary>
		/// Removes a layer from the level
		/// </summary>
		/// <param name="layer"></param>
		public void RemoveLayer(Layer layer)
		{
			layers.Remove(layer);
		}



		/// <summary>
		/// Gets  a layer by his bame
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Layer GetLayer(string name)
		{
			foreach (Layer layer in layers)
			{
				//if (layer.Name == name)
				//   return layer;
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

			layers.Insert(index, layer);
			layers.Remove(layer);
		}

		#endregion


		#region Resize, Insert and Delete
		/// <summary>
		/// Resize the level
		/// </summary>
		/// <param name="newsize">Desired size</param>
		public void Resize(Size newsize)
		{
			// Resize layers first
			foreach (Layer layer in layers)
				layer.Resize(newsize);

			// Resize
			size = newsize;
		}



		/// <summary>
		/// Inserts a row
		/// </summary>
		/// <param name="row">Rowid to remove</param>
		/// <param name="tileid">Tile BufferID</param>
		public void InsertRow(int row, int tileid)
		{
			// Insert the row
			foreach (Layer layer in Layers)
				layer.InsertRow(row, tileid);

			// Resize the level
			size.Height += 1;
			
		}



		/// <summary>
		/// Removes a row
		/// </summary>
		/// <param name="row">Row ID to remove</param>
		public void RemoveRow(int row)
		{
			foreach (Layer layer in Layers)
				layer.RemoveRow(row);

			size.Height -= 1;

		}



		/// <summary>
		/// Inserts a column
		/// </summary>
		/// <param name="column">Column BufferID to insert</param>
		/// <param name="tileid">Tile BufferID</param>
		public void InsertColumn(int column, int tileid)
		{
			foreach (Layer layer in Layers)
				layer.InsertColumn(column, tileid);

			size.Width += 1;
		}


		/// <summary>
		/// Removes a column
		/// </summary>
		/// <param name="column">Columns ID to remove</param>
		public void RemoveColumn(int column)
		{
			foreach (Layer layer in Layers)
				layer.RemoveColumn(column);

			size.Width -= 1;
		}


		#endregion


		#region Methods

		/// <summary>
		/// Call this function to load all resources needed by the level.
		/// </summary>
		/// <returns>If returns true, all resources are found and locked. Level is ready to be played</returns>
		public bool Init()
		{
			if (levelReady)
				return true;

			// Initialize all layers
			foreach(Layer layer in layers)
				layer.Init();

			// All ok
			levelReady = true;
			return true;
		}


		/// <summary>
		/// Draws the level
		/// </summary>
		public void Draw()
		{
			// Begin the draw
			Display.Texturing = true;
			Display.Blending = true;
			//Video.ScissorZone = displayZone;
			//Video.Scissor = true;
			//Video.Translate = new Point(displayZone.X, displayZone.Y);

			foreach (Layer layer in layers)
				layer.Draw();

			//Video.Offset = new Point();
			//Video.Scissor = false;
		}



		/// <summary>
		/// Updates all layers
		/// </summary>
		public void Update(GameTime time)
		{
			foreach (Layer layer in layers)
				layer.Update(time);
		}







		/// <summary>
		/// Converts level location to screen location
		/// </summary>
		/// <param name="pos">Point in pixel in the level</param>
		/// <returns>Position in screen coordinate</returns>
		public Point LevelToScreen(Point pos)
		{
			return new Point(pos.X - Location.X + ViewPort.Left,
				pos.Y - Location.Y + ViewPort.Top);
		}


		/// <summary>
		/// Converts screen location to level location
		/// </summary>
		/// <param name="pos">Position in screen coordinate</param>
		/// <returns>Point in pixel in the level</returns>
		public Point ScreenToLevel(Point pos)
		{
			return new Point(
				Location.X + pos.X - ViewPort.Left,
				Location.Y + pos.Y - ViewPort.Top);
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


		#region Properties

		/// <summary>
		/// Name of the dungeon
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
			get{return levelReady;}
		}
		bool levelReady;


		/// <summary>
		/// Gets a list of all layers
		/// </summary>
		/// <returns></returns>
		[Browsable(false)]
		public List<Layer> Layers
		{
			get
			{
				return layers;
			}
		}


		/// <summary>
		/// Layer list
		/// </summary>
		/// TODO utiliser un SortedDictionary !
		List<Layer> layers;


		/// <summary>
		/// Size of the level in block
		/// </summary>
		[CategoryAttribute("Tiles")]
		[Description("Size of the layer in block")]
		public Size Size
		{
			get
			{
				return size;
			}
			set
			{
				Resize(value);
			}
		}
		Size size;


		/// <summary>
		/// Height of the level in block
		/// </summary>
		[Browsable(false)]
		public int Height
		{
			get
			{
				return Size.Height;
			}
		}


		/// <summary>
		/// Width of the level in block
		/// </summary>
		[Browsable(false)]
		public int Width
		{
			get
			{
				return Size.Width;
			}
		}


		/// <summary>
		/// Dimension of the level in pixel
		/// </summary>
		[ReadOnly(true)]
		public Size Dimension
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
			get { return blockSize; }
			set { blockSize = value; }
		}
		Size blockSize = new Size(16, 16);


		/// <summary>
		/// Gets the dimension in pixel of a block (including the zoom factor)
		/// </summary>
		[Browsable(false)]
		public Size BlockDimension
		{
			get
			{
				return new Size((int)(BlockSize.Width * Zoom.Width), (int)(BlockSize.Height * Zoom.Height));
			}
		}

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

				if (location.X > (Size.Width * BlockDimension.Width) - ViewPort.Width)
					location.X = Size.Width * BlockDimension.Width - ViewPort.Width;
				if (location.Y > (Size.Height * BlockDimension.Height) - ViewPort.Height)
					location.Y = Size.Height * BlockDimension.Height - ViewPort.Height;

				if (location.X < 0) location.X = 0;
				if (location.Y < 0) location.Y = 0;
			}
		}
		Point location;


		/// <summary>
		/// Niveau de zoom du layer
		/// </summary>
		[CategoryAttribute("Layer")]
		[Description("Zoom")]
		public SizeF Zoom
		{
			get
			{
				return zoom;
			}
			set
			{
				zoom = value;
				if (zoom.Width == 0.0f)
					zoom.Width = 1.0f;
				if (zoom.Height == 0.0f)
					zoom.Height = 1.0f;
			}
		}
		SizeF zoom = new SizeF(1.0f, 1.0f);

		/// <summary>
		/// Gets/sets the collision layer name
		/// </summary>
		[TypeConverter(typeof(LayerEnumerator))]
		[CategoryAttribute("Collision")]
		[Description("Defines the name of the collision layer")]
		public string CollisionLayerName
		{
			get
			{
				return collisionLayerName;
			}
			set
			{
				collisionLayerName = value;
			}
		}
		string collisionLayerName;



		/// <summary>
		/// Collision layer
		/// </summary>
		[Browsable(false)]
		public Layer CollisionLayer
		{
			get
			{
				return GetLayer(collisionLayerName);
			}
		}

/*
		/// <summary>
		/// Rendering zone of the level on the screen
		/// ODO: Checks value for validity !
		/// </summary>
		public static Rectangle DisplayZone
		{
			get
			{
				if (displayZone.IsEmpty)
					return Video.ViewPort;
				else
					return displayZone;
			}
			set
			{
				displayZone = value;
			}
		}
		static Rectangle displayZone;
*/
		/// <summary>
		/// Rendering zone of the level on the screen
		/// </summary>
		public static Rectangle ViewPort
		{
			get;
			set;
		}

		#endregion


	}

}
