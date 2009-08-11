using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// Life struct
	/// </summary>
	public class Life
	{


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

			if (xml.Name.ToLower() != "life")
				return false;

			Actual = short.Parse(xml.Attributes["actual"].Value);
			Max = short.Parse(xml.Attributes["max"].Value);

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


			writer.WriteStartElement("life");
			writer.WriteAttributeString("actual", Actual.ToString());
			writer.WriteAttributeString("max", Max.ToString());
			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Helper


		public override string ToString()
		{
			return Actual.ToString() + "/" + Max.ToString();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Actual life 
		/// </summary>
		public short Actual;

		/// <summary>
		/// Maximum life 
		/// </summary>
		public short Max;


		/// <summary>
		/// Returns if the entity is dead (life <= 0)
		/// </summary>
		public bool IsDead
		{
			get
			{
				return Actual <= 0 ? true : false;
			}
		}

		#endregion
	}
}
