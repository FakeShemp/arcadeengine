using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// 
	/// </summary>
	public class Teleporter
	{

		/// <summary>
		/// 
		/// </summary>
		public Teleporter()
		{
			Target = new DungeonLocation();
		}


		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;
			
			if (xml.Name.ToLower() != "teleporter")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "target":
					{
						Target.Load(node);
					}
					break;
				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;
			

			writer.WriteStartElement("teleporter");


			writer.WriteStartElement("target");
			Target.Save(writer);
			writer.WriteEndElement();

			writer.WriteEndElement();

			return true;
		}



		#endregion



		#region Properties

		/// <summary>
		/// Target of the stair
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the sound to play
		/// </summary>
		public string SoundName
		{
			get;
			set;
		}

		#endregion


	}
}
