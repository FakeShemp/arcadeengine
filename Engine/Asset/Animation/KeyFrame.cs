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
	public class KeyFrame : IComparable<KeyFrame>
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public KeyFrame()
		{
			TileID = -1;
			TextID = -1;
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
					case "tile":
					{
						TileID = int.Parse(node.Attributes["id"].Value);
						TileLocation = new Point(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value));
					}
					break;

					case "time":
					{
						Time = TimeSpan.Parse(node.Attributes["value"].Value);

					}
					break;

					case "bgcolor":
					{
						BgColor = Color.FromArgb(Byte.Parse(node.Attributes["r"].Value),
							Byte.Parse(node.Attributes["g"].Value),
							Byte.Parse(node.Attributes["b"].Value),
							Byte.Parse(node.Attributes["a"].Value));
					}
					break;


					case "text":
					{
						TextID = int.Parse(node.Attributes["id"].Value);
						TextColor = Color.FromArgb(
							byte.Parse(node.Attributes["a"].Value),
							byte.Parse(node.Attributes["r"].Value),
							byte.Parse(node.Attributes["g"].Value),
							byte.Parse(node.Attributes["b"].Value));
						TextRectangle = new Rectangle(int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value),
							int.Parse(node.Attributes["width"].Value), int.Parse(node.Attributes["height"].Value));
					}
					break;

				}
			}

			return true;
		}


		#endregion



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
		/// Time of the KeyFrame
		/// </summary>
		public TimeSpan Time
		{
			get;
			set;
		}


		/// <summary>
		/// Location of the tile
		/// </summary>
		public Point TileLocation
		{
			get;
			set;
		}


		/// <summary>
		/// Background color
		/// </summary>
		public Color BgColor
		{
			get;
			set;
		}

		/// <summary>
		/// Text to use in the StringTable
		/// </summary>
		public int TextID
		{
			get;
			set;
		}


		/// <summary>
		/// Rectangle of the text
		/// </summary>
		public Rectangle TextRectangle
		{
			get;
			set;
		}


		/// <summary>
		/// Color of the text
		/// </summary>
		public Color TextColor
		{
			get;
			set;
		}
		#endregion



		#region Comparer

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static IComparer Sorter
		{
			get
			{
				return (IComparer)new KeyFrameComparer();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(KeyFrame other)
		{
			if (other == null)
				return 1;

			if (other == this)
				return 0;

			if (Time > other.Time)
				return 1;

			return -1;
		}

		/// <summary>
		/// 
		/// </summary>
		private class KeyFrameComparer : IComparer<KeyFrame>
		{
			public int Compare(KeyFrame x, KeyFrame y)
			{
				if (x == null && y == null)
					return 0;
		
				else if (x == null)
					return -1;
				
				else if (y == null)
					return 1;

				if (x == y)
					return 0;

				return (int) (x.Time.Ticks - y.Time.Ticks);
			}

		}


		#endregion

	}

}

