using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace DungeonEye
{
	/// <summary>
	/// A hit that strikes a vital area and therefore deals double damage or more.
	/// </summary>
	public class CriticalHit
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public CriticalHit()
		{
			Minimum = 20;
			Maximum = 20;
			Multiplier = 2;
		}


		/// <summary>
		/// ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}-{1}(x{2})", Minimum, Maximum, Multiplier); 
		}

		#region IO


		/// <summary>
		/// Loads properties
		/// </summary>
		/// <param name="node">Node</param>
		/// <returns></returns>
		public bool Load(XmlNode node)
		{
			if (node == null)
				return false;

			if (node.NodeType != XmlNodeType.Element)
				return false;


			if (node.Attributes["minimum"] != null)
				Minimum = int.Parse(node.Attributes["minimum"].Value);

			if (node.Attributes["maximum"] != null)
				Maximum = int.Parse(node.Attributes["maximum"].Value);

			if (node.Attributes["multiplier"] != null)
				Multiplier = int.Parse(node.Attributes["multiplier"].Value);

			return true;
		}



		/// <summary>
		/// Saves properties
		/// </summary>
		/// <param name="writer">XmlWriter</param>
		public void Save(XmlWriter writer)
		{
			if (writer == null)
				return;

			bool writeheader = false;
			if (writer.WriteState != WriteState.Element)
				writeheader = true;

			if (writeheader)
				writer.WriteStartElement("criticalhit");
			writer.WriteAttributeString("minimum", Minimum.ToString());
			writer.WriteAttributeString("maximum", Maximum.ToString());
			writer.WriteAttributeString("multiplier", Multiplier.ToString());
			if (writeheader)
				writer.WriteEndElement();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Minimum value
		/// </summary>
		public int Minimum
		{
			get;
			set;
		}

		/// <summary>
		/// Maximum value
		/// </summary>
		public int Maximum
		{
			get;
			set;
		}


		/// <summary>
		/// Multiplier
		/// </summary>
		public int Multiplier
		{
			get;
			set;
		}

		#endregion

	}
}
