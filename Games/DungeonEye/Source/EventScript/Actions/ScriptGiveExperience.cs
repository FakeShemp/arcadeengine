using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.EventScript
{
	public class ScriptGiveExperience : IScriptAction
	{

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Run()
		{
			Team.AddExperience(Amount);

			return true;
		}


		#region IO


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns>True on success</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != Name)
				return false;

			if (xml.Attributes["value"] != null)
				Amount = int.Parse(xml.Attributes["value"].Value);

			//foreach (XmlNode node in xml)
			//{
			//    if (node.NodeType == XmlNodeType.Comment)
			//        continue;

			//    switch (node.Name.ToLower())
			//    {
			//        case "amount":
			//        {
			//            Amount = int.Parse(node.Attributes["value"].Value);
			//        }
			//        break;
			//    }
			//}
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns>True on success</returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement(Name);
			writer.WriteAttributeString("value", Amount.ToString());
			writer.WriteEndElement();

			return true;
		}



		#endregion



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get
			{
				return "GiveExperience";
			}
		}


		/// <summary>
		/// Amount of experience to give
		/// </summary>
		public int Amount
		{
			get;
			set;
		}


		#endregion
	}
}
