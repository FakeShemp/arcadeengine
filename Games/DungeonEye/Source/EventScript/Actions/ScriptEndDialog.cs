using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.EventScript
{
	public class ScriptEndDialog : IScriptAction
	{

		/// <summary>
		/// 
		/// </summary>
		/// <returns>True on succes</returns>
		public bool Run()
		{
			if (InGameScreen.Dialog == null)
				return false;


			InGameScreen.Dialog.Exit();
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns>True on success</returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != Name)
				return false;


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
			writer.WriteEndElement();

			return true;
		}




		#region Properties


		/// <summary>
		/// Name of the script
		/// </summary>
		public string Name
		{
			get
			{
				return "EndDialog";
			}
		}

		#endregion
	}
}
