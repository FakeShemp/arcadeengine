using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.Script
{
	/// <summary>
	/// 
	/// </summary>
	public class ScriptEndDialog : ScriptAction
	{

		/// <summary>
		/// 
		/// </summary>
		public ScriptEndDialog()
		{
			Name = "EndDialog";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns>True on succes</returns>
		public override bool Run()
		{
			if (GameScreen.Dialog == null)
				return false;


			GameScreen.Dialog.Exit();
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns>True on success</returns>
		public override bool Load(XmlNode xml)
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
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteStartElement(Name);
			writer.WriteEndElement();

			return true;
		}




		#region Properties

		#endregion
	}
}
