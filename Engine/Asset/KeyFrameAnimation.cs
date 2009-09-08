using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace ArcEngine.Asset
{

	/// <summary>
	/// Key framed animation
	/// </summary>
	public class KeyFrameAnimation : IAsset
	{


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
					}
					break;

					case "framerate":
					{
						FrameRate = int.Parse(node.Attributes["value"].Value);
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

		#endregion
	}
}
