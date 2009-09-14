using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using ArcEngine.Graphic;


namespace ArcEngine.Asset
{

	/// <summary>
	/// Key framed animation
	/// </summary>
	public class Animation : IAsset
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Animation()
		{
			Layers = new List<AnimationLayer>();
		}


		#region IO routines

		///
		///<summary>
		/// Save the animation to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;


			//xml.WriteStartElement("animation");
			//xml.WriteAttributeString("name", Name);


			//		base.SaveComment(xml);

	//		xml.WriteStartElement("loop");
	//		xml.WriteAttributeString("value", type.ToString());
	//		xml.WriteEndElement();


			xml.WriteEndElement();

			return true;
		}

		/// <summary>
		/// Loads the animation from a xml file
		/// </summary>
		/// <param name="xml">XmlNode to load</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name != "keyframeanimation")
			{
				Trace.WriteLine("Expecting \"keyframeanimation\" in node header, found \"" + xml.Name + "\" when loading KeyFrameAnimation.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						TileSetName = node.Attributes["name"].Value;
						TileSet = ResourceManager.CreateAsset<TileSet>(TileSetName);
						if (TileSet == null)
							break;

						SizeF size = new SizeF(1.0f, 1.0f);
						if (node.Attributes["scalew"].Value != null)
							size.Width = float.Parse(node.Attributes["scalew"].Value);
						if (node.Attributes["scaleh"].Value != null)
							size.Height = float.Parse(node.Attributes["scaleh"].Value);

						TileSet.Scale = size;
					}
					break;


					case "layer":
					{
						AnimationLayer layer = CreateLayer();
						layer.Load(node);
					}
					break;


					case "stringtable":
					{
						StringTableName = node.Attributes["name"].Value;
						StringTable = ResourceManager.CreateAsset<StringTable>(StringTableName);
					}
					break;

					case "font":
					{
						FontName = node.Attributes["name"].Value;
						Font = ResourceManager.CreateAsset<Font2d>(FontName);
					}
					break;

					default:
					{
						Trace.WriteLine("Animation : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}


		#endregion



		/// <summary>
		/// Update animation
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			if (!Pause)
			{
				Time += time.ElapsedGameTime;
			}

		}


		/// <summary>
		/// Draws the animation
		/// </summary>
		public void Draw()
		{
			Display.ClearBuffers();

			if (TileSet == null)
				return;


			Display.Scissor = true;
			foreach (AnimationLayer layer in Layers)
			{
				Frame frame = new Frame(layer, Time);
				frame.Draw();
			}

			Display.Scissor = false;
		}


		#region Motion controls

		/// <summary>
		/// Play the animation
		/// </summary>
		public void Play()
		{
		}



		/// <summary>
		/// Stop the animation
		/// </summary>
		public void Stop()
		{
		}


		#endregion


		#region Layer management

		/// <summary>
		/// Add an AnimationLayer
		/// </summary>
		/// <returns>Created layer</returns>
		public AnimationLayer CreateLayer()
		{

			AnimationLayer layer = new AnimationLayer(this);
			Layers.Add(layer);
			Layers.Sort();

			return layer;
		}


		/// <summary>
		/// Removes an AnimationLayer
		/// </summary>
		/// <param name="id">Id of the layer</param>
		public void RemoveLayer(int id)
		{
			Layers.RemoveAt(id);
		}


		/// <summary>
		/// Removes an AnimationLayer
		/// </summary>
		/// <param name="layer">Layer</param>
		public void RemoveLayer(AnimationLayer layer)
		{
			Layers.Remove(layer);
		}


		/// <summary>
		/// Sorts layers
		/// </summary>
		public void SortLayers()
		{
			Layers.Sort();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Name of the animation
		/// </summary>
		public string Name { get; set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "animation";
			}
		}


		/// <summary>
		/// Name of the TileSet to use
		/// </summary>
		public string TileSetName
		{
			get;
			set;
		}

		/// <summary>
		/// Tileset
		/// </summary>
		public TileSet TileSet
		{
			get;
			private set;
		}


		/// <summary>
		/// Name of the Font to use
		/// </summary>
		public string FontName
		{
			get;
			set;
		}

		/// <summary>
		/// Font
		/// </summary>
		public Font2d Font
		{
			get;
			private set;
		}




		/// <summary>
		/// Available layers
		/// </summary>
		List<AnimationLayer> Layers;

		/// <summary>
		/// Pause the animation
		/// </summary>
		public bool Pause { get; set; }


		/// <summary>
		/// Current time position in the animation
		/// </summary>
		public TimeSpan Time { get; set; }


		/// <summary>
		/// Name of the StringTable to use
		/// </summary>
		public string StringTableName  { get; set; }


		/// <summary>
		/// StringTable
		/// </summary>
		public StringTable StringTable
		{
			get;
			private set;
		}

		#endregion

	}
}
