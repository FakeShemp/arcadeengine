using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;

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
			Layers = new Dictionary<string, AnimationLayer>();


			AnimationLayer layer = new AnimationLayer();
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
					}
					break;

					case "framerate":
					{
						FrameRate = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "layer":
					{
						AnimationLayer layer = new AnimationLayer(node);
						Layers[layer.Name] = layer;
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
		}


		/// <summary>
		/// Draws the animation
		/// </summary>
		public void Draw()
		{
			if (TileSet == null)
				return;

			TileSet.Draw(0, new Point(100, 100));
		}

		#region Properties

		/// <summary>
		/// Name of the animation
		/// </summary>
		public string Name
		{
			get;
			set;
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
		TileSet TileSet;


		/// <summary>
		/// Frame rate
		/// </summary>
		public int FrameRate
		{
			get;
			set;
		}


		/// <summary>
		/// Available layers
		/// </summary>
		Dictionary<string, AnimationLayer> Layers;
		#endregion

	}
}
