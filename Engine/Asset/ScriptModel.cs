using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Model of script
	/// </summary>
	public class ScriptModel : IAsset
	{


		#region	IO operations


		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			xml.WriteStartElement("model");
			xml.WriteAttributeString("name", Name);

			xml.WriteStartElement("source");
			xml.WriteRaw(Source);
			xml.WriteEndElement();

			xml.WriteEndElement();



			return true;
		}


		/// <summary>
		/// Loads the collection from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;

				switch (node.Name.ToLower())
				{
					// Texture
					case "source":
					{
						Source = node.InnerText;
					}
					break;



				}
			}

			return true;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Name of the asset
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Source code of the model
		/// </summary>
		public string Source;


		#endregion
	}


}
