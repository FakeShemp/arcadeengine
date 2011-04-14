using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.EventScript
{

	/// <summary>
	/// 
	/// </summary>
	public class ScriptEndChoice : ScriptAction
	{

		/// <summary>
		/// 
		/// </summary>
		public ScriptEndChoice()
		{
			Name = "EndChoice";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Run(Team team)
		{


			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			return true;
		}




		#region Properties

		#endregion
	}
}
