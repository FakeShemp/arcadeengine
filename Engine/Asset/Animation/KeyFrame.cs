using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;


namespace ArcEngine.Asset
{

	/// <summary>
	/// 
	/// </summary>
	public class KeyFrame : IComparer
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

			if (xml.Name != "keyframe")
			{
				Trace.WriteLine("Expecting \"keyframe\" in node header, found \"" + xml.Name + "\" when loading KeyFrame.");
				return false;
			}


			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				switch (node.Name.ToLower())
				{
					case "id":
					{
						TileID = int.Parse(node.Attributes["id"].Value);
					}
					break;

					case "frame":
					{
						Frame = int.Parse(node.Attributes["id"].Value);
					}
					break;

					case "location":
					{
						Location = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
					}
					break;

				}
			}

			return true;
		}


		#endregion


		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			KeyFrame a = x as KeyFrame;
			KeyFrame b = y as KeyFrame;

			return a.Frame - b.Frame;
		}


		#region Properties

		/// <summary>
		/// Name
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
		/// ID of the frame
		/// </summary>
		public int Frame
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

		#endregion

	}
}
