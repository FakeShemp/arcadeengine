using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Asset;
using System.Xml;

namespace DungeonEye
{

	/// <summary>
	/// Button class
	/// </summary>
	public class WallButton : IAsset
	{

		public WallButton()
		{
			AccpetedItems = new List<Item>();
		}



		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}

	

		/// <summary>
		/// Draws the button
		/// </summary>
		public void Draw()
		{

		}


		#region Load & Save

		/// <summary>
		/// Loads an item definition
		/// </summary>
		/// <param name="xml">Xml handle</param>
		/// <returns>True if loaded, or false</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != "item")
				return false;


			//ID = int.Parse(xml.Attributes["id"].Value);
			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					case "type":
					{
						//Type = (ItemType)Enum.Parse(typeof(ItemType), node.Attributes["value"].Value, true);
					}
					break;

				}
			}



			return true;
		}



		/// <summary>
		/// Saves the item definition
		/// </summary>
		/// <param name="writer">Xml writer handle</param>
		/// <returns>True if saved, or false</returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("item");
			writer.WriteAttributeString("name", Name);


			writer.WriteStartElement("type");
		//	writer.WriteAttributeString("value", Type.ToString());
			writer.WriteEndElement();



			writer.WriteEndElement();

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
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "wallbutton";
			}
		}

		/// <summary>
		/// Is button hidden
		/// </summary>
		public bool IsHidden
		{
			get;
			set;
		}


		/// <summary>
		/// Accepted items
		/// </summary>
		public List<Item> AccpetedItems
		{
			get;
			private set;
		}

		#endregion
	}
}
