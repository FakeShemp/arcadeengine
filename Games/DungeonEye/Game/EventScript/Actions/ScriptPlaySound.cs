using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.EventScript
{
	public class ScriptPlaySound: ScriptAction
	{

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


		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get
			{
				return "PlaySound";
			}
		}

		#endregion
	}
}
