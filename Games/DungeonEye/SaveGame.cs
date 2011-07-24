#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Forms;
using DungeonEye;

namespace DungeonEye
{
	/// <summary>
	/// Savegame management class
	/// </summary>
	public class SaveGame
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filename">Savegame filename</param>
		public SaveGame(string filename)
		{
			FileName = filename;
			Slots = new SaveGameSlot[GameSettings.MaxGameSaveSlot];
		}



		#region I/O


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Load()
		{
			// File not found
			if (!System.IO.File.Exists(FileName))
			{
				Trace.WriteLine("[SaveGame]Read() : Unable to find file \"" + FileName + "\" !");
				return false;
			}

			// Flush slots
			for (int i = 0; i < GameSettings.MaxGameSaveSlot; i++)
				Slots[i] = new SaveGameSlot();

			// Open document
			XmlDocument xml = new XmlDocument();
			xml.Load(FileName);

			// Bad tag
			if (xml.DocumentElement.Name != "savegame")
			{
				Trace.WriteLine("[SaveGame]Read() : Bad file format !");
				return false;
			}


			// Load the right slot
			foreach (XmlNode node in xml.DocumentElement)
			{
				switch (node.Name)
				{
					#region slot
					case "slot":
					{
						int id = int.Parse(node.Attributes["id"].Value);
						Slots[id].Name = node.Attributes["name"].Value;

						foreach (XmlNode sub in node)
						{

							switch (sub.Name)
							{
								case "team":
								{
									Slots[id].Team = sub;
								}
								break;

								case "dungeon":
								{
									Slots[id].Dungeon = sub;
								}
								break;
							}
						}
					}
					break;
					#endregion


					case "dungeon":
					{
						DungeonName = node.Attributes["name"].Value;
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
		bool Write()
		{
			if (string.IsNullOrEmpty(FileName))
				return false;

			return false;
		}

		#endregion



		#region Properties


		/// <summary>
		/// Filename of the savegame
		/// </summary>
		public string FileName
		{
			get;
			private set;
		}


		/// <summary>
		/// Default dungeon name
		/// </summary>
		public string DungeonName;


		/// <summary>
		/// Slots
		/// </summary>
		public SaveGameSlot[] Slots
		{
			get;
			private set;
		}

		#endregion
	}
}
