using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.Script
{
	/// <summary>
	/// Play a sound
	/// </summary>
	public class ScriptPlaySound: ScriptBase
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
		public override bool Run()
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
