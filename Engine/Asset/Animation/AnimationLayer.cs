using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Class representing a layer in an animation
	/// </summary>
	public class AnimationLayer
	{

		/// <summary>
		/// Default construcor
		/// </summary>
		public AnimationLayer()
		{

		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node"></param>
		public AnimationLayer(XmlNode node)
		{
			Load(node);
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



			//xml.WriteStartElement("loop");
			//xml.WriteAttributeString("value", type.ToString());
			//xml.WriteEndElement();


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

			if (xml.Name != "layer")
			{
				Trace.WriteLine("Expecting \"layer\" in node header, found \"" + xml.Name + "\" when loading AnimationLayer.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				switch (node.Name.ToLower())
				{
					case "viewport":
					{
						Viewport = new Rectangle(int.Parse(node.Attributes["x"].Value),
							int.Parse(node.Attributes["y"].Value),
							int.Parse(node.Attributes["width"].Value),
							int.Parse(node.Attributes["height"].Value));
					}
					break;

					default:
					{
						Trace.WriteLine("AnimationLayer : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Name of the layer
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// ID of the tile
		/// </summary>
		public int TileID
		{
			get;
			set;
		}


		/// <summary>
		/// Location of the tile
		/// </summary>
		public Point Location
		{
			get;
			set;
		}

		/// <summary>
		/// Viewport of the layer
		/// </summary>
		public Rectangle Viewport
		{
			get;
			set;
		}





		#endregion
	}
}
