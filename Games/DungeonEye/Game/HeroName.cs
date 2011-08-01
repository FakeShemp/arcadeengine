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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Graphic;
using DungeonEye.Script;



namespace DungeonEye
{

	/// <summary>
	/// Hero naming class
	/// </summary>
	public class HeroName
	{

		/// <summary>
		/// Gets a name for a Hero
		/// </summary>
		/// <param name="thehero">Hero handle</param>
		/// <returns>Name of the Hero</returns>
		public static string GetNameForHero(Hero thehero)
		{
			string name = "";
			switch (thehero.Race)
			{
				case HeroRace.HumanMale:
				name = HumanMaleName();
				break;
				case HeroRace.HumanFemale:
				name = HumanFemaleName();
				break;
				case HeroRace.ElfMale:
				name = ElfMaleName();
				break;
				case HeroRace.ElfFemale:
				name = ElfFemaleName();
				break;
				case HeroRace.HalfElfMale:
				name = HalfElfMaleName();
				break;
				case HeroRace.HalfElfFemale:
				name = HalfElfFemaleName();
				break;
				case HeroRace.DwarfMale:
				name = DwarfMaleName();
				break;
				case HeroRace.DwarfFemale:
				name = DwarfFemaleName();
				break;
				case HeroRace.GnomeMale:
				name = GnomeMaleName();
				break;
				case HeroRace.GnomeFemale:
				name = GnomeFemaleName();
				break;
				case HeroRace.HalflingMale:
				name = HalflingMaleName();
				break;
				case HeroRace.HalflingFemale:
				name = HalflingFemaleName();
				break;
			}


			return name;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string HalflingFemaleName()
		{
			return "Martha";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string HalflingMaleName()
		{
			return "Robbi";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string GnomeFemaleName()
		{
			return "Farmunlundi";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string GnomeMaleName()
		{
			return "Gargodokulunus";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string DwarfFemaleName()
		{
			return "Melinda";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string DwarfMaleName()
		{
			return "Bardarok";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string HalfElfFemaleName()
		{
			return "Gwendolin";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string HalfElfMaleName()
		{
			return "Jeen Klaus";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string ElfFemaleName()
		{
			return "Aelindiale";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string ElfMaleName()
		{
			return "Lindolrin";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string HumanFemaleName()
		{
			return "Katerin";
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string HumanMaleName()
		{
			return "Marcandrus";
		}
	}
}