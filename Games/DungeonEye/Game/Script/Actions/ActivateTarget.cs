using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace DungeonEye.Script.Actions
{
	/// <summary>
	/// Activate a target
	/// </summary>
	public class ActivateTarget : ActionBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ActivateTarget()
		{
			Name = XmlTag;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="team"></param>
		/// <returns></returns>
		public override bool Run()
		{
			if (Target == null)
				return false;

			Square square = Target.GetSquare(GameScreen.Dungeon);
			if (square == null)
				return false;

			if (square.Actor != null)
				square.Actor.Activate();

			return true;
		}


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name != XmlTag)
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


			writer.WriteStartElement(XmlTag);

			base.Save(writer);

			writer.WriteEndElement();

			return true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public const string XmlTag = "ActivateTarget";


		#endregion
	}
}
