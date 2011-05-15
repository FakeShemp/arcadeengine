using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.Script.Actions
{
	/// <summary>
	/// 
	/// </summary>
	public class GiveItem : ActionBase
	{

		/// <summary>
		/// 
		/// </summary>
		public GiveItem()
		{
			Name = XmlTag;
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
		/// <returns></returns>
		public override string ToString()
		{
			return "Give : " + Name;
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
		public const string XmlTag = "GiveItem";


		/// <summary>
		/// 
		/// </summary>
		string Name;



		#endregion
	}
}
