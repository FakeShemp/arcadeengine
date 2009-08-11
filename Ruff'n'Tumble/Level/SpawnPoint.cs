using System;
using System.Drawing;
using System.Xml;
using System.ComponentModel;
using ArcEngine.Asset;
using ArcEngine;


namespace RuffnTumble.Asset
{
	/// <summary>
	/// 
	/// </summary>
	public class SpawnPoint// : ResourceBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public SpawnPoint()//string name) : base(name)
		{
		}




		#region IO
		/// <summary>
		/// Loads a SpawnPoint definition from bank file
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
				//	base.LoadComment(node);
					continue;
				}

				switch (node.Name.ToLower())
				{
					case "location":
					{
						location = new Point(Int32.Parse(node.Attributes["x"].Value),
							Int32.Parse(node.Attributes["y"].Value));
					}
					break;

					default:
					{
						Log.Send(new LogEventArgs(LogLevel.Warning, "SpawnPoint : Unknown node element found (" + node.Name + ")", null));
					}
					break;
				}
			}

			return true;
		}


		/// <summary>
		/// Save SpawnPoint defintion to a file bank
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Save(XmlWriter xml)
		{

			xml.WriteStartElement("spawnpoint");
		//	xml.WriteAttributeString("name", Name);

		//	base.SaveComment(xml);


			xml.WriteStartElement("location");
			xml.WriteAttributeString("x", location.X.ToString());
			xml.WriteAttributeString("y", location.Y.ToString());
			xml.WriteEndElement();

			xml.WriteEndElement();

			
			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// Offset of the SpawnPoint
		/// </summary>
		public Point Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}
		Point location = Point.Empty;


        /// <summary>
        /// Gets the location of the collision box of the entity (CollisionBox translated
        /// to the entity location in screen coordinate)
        /// </summary>
        [Browsable(false)]
        public Rectangle CollisionBoxLocation
        {
            get
            {
                return new Rectangle(
                    Location.X - 8,
                    Location.Y - 8,
                    16,
                    16);
            }
        }


		#endregion
	}
}
