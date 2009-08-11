using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Asset;
using System.Xml;


namespace DungeonEye.Source
{
	/// <summary>
	/// Spell class
	/// </summary>
	class Spell : IAsset
	{




		#region Load & Save

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml.Name != "spell")
				return false;

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
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			writer.WriteStartElement("spell");
			writer.WriteAttributeString("name", Name);





			//writer.WriteStartElement("speed");
			//writer.WriteAttributeString("value", Speed.ToString());
			//writer.WriteEndElement();



			writer.WriteEndElement();

			return true;
		}

		#endregion



		#region Properties

		/// <summary>
		/// Name of the spell
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		///// <summary>
		///// Description of the spell
		///// </summary>
		//public string Description
		//{
		//   get;
		//   set;
		//}



		/// <summary>
		/// Spell's duration
		/// </summary>
		public TimeSpan Duration
		{
			get;
			set;
		}

		/// <summary>
		/// Action range
		/// </summary>
		/// <remarks>0 means the team only</remarks>
		public int Range
		{
			get;
			set;
		}


		/// <summary>
		/// Required level for this spell
		/// </summary>
		public int Level
		{
			get;
			set;
		}


		
		#endregion
	}
}
