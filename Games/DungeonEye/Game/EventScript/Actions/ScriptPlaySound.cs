using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.EventScript
{
	/// <summary>
	/// Play a sound
	/// </summary>
	public class ScriptPlaySound: ScriptAction
	{

		/// <summary>
		/// 
		/// </summary>
		public ScriptPlaySound()
		{
			Name = "PlaySound";
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
			return false;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			return false;
		}




		#region Properties

		#endregion
	}
}
