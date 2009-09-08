using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;



namespace ArcEngine.Asset
{

	/// <summary>
	/// 
	/// </summary>
	public class AnimationKeyFrame
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

			if (xml.Name != "layer")
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
					// loop
					case "loop":
					{
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


		#endregion

	}
}
