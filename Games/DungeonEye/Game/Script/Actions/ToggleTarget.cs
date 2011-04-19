using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.Script.Actions
{
	/// <summary>
	/// Toggle action
	/// </summary>
	public class ToggleTarget : ActionBase
	{

		/// <summary>
		/// 
		/// </summary>
		public ToggleTarget()
		{
			Name = XmlTag;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Run()
		{
			if (Target == null)
				return false;

			Square square = Target.GetSquare(GameScreen.Dungeon);
			if (square == null)
				return false;

			if (square.Actor != null)
				square.Actor.Toggle();
			
			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					default:
					{
						base.Load(node);
					}
					break;
				}
			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public override bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement(Name);

			base.Save(writer);

			writer.WriteEndElement();

			return true;
		}




		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public const string XmlTag = "ToggleTarget";



		#endregion
	}
}
