using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;


namespace DungeonEye
{
	public class FloorPlate
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public FloorPlate()
		{
			Script = new Script();


			// HACK: Move this somewhere else
			Script.AddReference(System.Reflection.Assembly.GetExecutingAssembly().Location);
			
		}


		/// <summary>
		/// The team or an item is walking / falling on the plate
		/// </summary>
		/// <param name="team">Handle to the team</param>
		/// <param name="block">Mazeblock of the fllor plate</param>
		public void OnTouch(Team team, MazeBlock block)
		{
			// No script defined
			if (string.IsNullOrEmpty(OnEnterScript) ||Script == null)
				return;

			Script.Compile();

			Script.Invoke(OnEnterScript, team, block);


		}



		/// <summary>
		/// Team /item is leaving the Floor Plate
		/// </summary>
		/// <param name="team"></param>
		/// <param name="item"></param>
		public void OnLeave(Team team, MazeBlock block)
		{
			// No script defined
			if (string.IsNullOrEmpty(OnEnterScript) || Script == null)
				return;

			Script.Compile();

			Script.Invoke(OnLeaveScript, team, block);

		}

		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name.ToLower() != "floorplate")
				return false;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{

					case "invisible":
					{
						Invisible = true;
					}
					break;

					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
						Script = ResourceManager.CreateAsset<Script>(ScriptName);
						Script.Compile();
					}
					break;

					case "onleave":
					{
						OnLeaveScript = node.Attributes["name"].Value;
					}
					break;

					case "onenter":
					{
						OnEnterScript = node.Attributes["name"].Value;
					}
					break;

				}

			}

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;


			writer.WriteStartElement("floorplate");

			if (Invisible)
			{
				writer.WriteStartElement("invisible");
				writer.WriteEndElement();
			}


			writer.WriteStartElement("script");
			writer.WriteAttributeString("name", ScriptName);
			writer.WriteEndElement();

			writer.WriteStartElement("onleave");
			writer.WriteAttributeString("name", OnLeaveScript);
			writer.WriteEndElement();

			writer.WriteStartElement("onenter");
			writer.WriteAttributeString("name", OnEnterScript);
			writer.WriteEndElement();

			writer.WriteEndElement();

			return true;
		}



		#endregion


		#region Properties


		/// <summary>
		/// Is the floor plate visible
		/// </summary>
		public bool Invisible
		{
			get;
			set;
		}


		/// <summary>
		///  Action to execute
		/// </summary>
		public string OnEnterScript
		{
			get;
			set;
		}

		/// <summary>
		///  Action to execute
		/// </summary>
		public string OnLeaveScript
		{
			get;
			set;
		}


		/// <summary>
		///  Action to execute
		/// </summary>
		public string ScriptName
		{
			get;
			set;
		}

		/// <summary>
		/// Script
		/// </summary>
		Script Script;



		#endregion


		
		





	}
}
