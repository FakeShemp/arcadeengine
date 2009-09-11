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
						AnimationLayer layer = new AnimationLayer(node);
						AddLayer(layer);
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
				Frame frame = layer.GetFrame(Time);
				if (frame == null)
					break;

				Display.ScissorZone = layer.Viewport;
				TileSet.Draw(frame.TileID, frame.Location);
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
		/// <param name="layer"></param>
		public void AddLayer(AnimationLayer layer)
		{
			if (layer == null)
				return;

			Layers.Add(layer);
			Layers.Sort();	
		}


		/// <summary>
		/// Removes an AnimationLayer
		/// </summary>
		/// <param name="id">Id of the layer</param>
		public void RemoveLayer(int id)
		{
		}


		/// <summary>
		/// Removes an AnimationLayer
		/// </summary>
		/// <param name="layer">Layer</param>
		public void RemoveLayer(AnimationLayer layer)
		{
		}

		#endregion


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
		/// Available layers
		/// </summary>
		List<AnimationLayer> Layers;

		/// <summary>
		/// Pause the animation
		/// </summary>
		public bool Pause
		{
			get;
			set;
		}


		/// <summary>
		/// Current time position in the animation
		/// </summary>
		public TimeSpan Time
		{
			get;
			set;
		}

		#endregion

	}
}
