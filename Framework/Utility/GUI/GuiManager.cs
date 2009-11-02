#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Text;
using ArcEngine.Graphic;



namespace ArcEngine.Utility.GUI
{

	/// <summary>
	/// 
	/// </summary>
	public class GuiManager
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public GuiManager()
		{
			Elements = new List<GuiBase>();
		}




		#region Update and Draw

		/// <summary>
		/// Updates elements
		/// </summary>
		/// <param name="time"></param>
		internal void Update(GameTime time)
		{
			foreach (GuiBase element in Elements)
				element.Update(time);
		}



		/// <summary>
		/// Draws elements
		/// </summary>
		internal void Draw()
		{
			foreach (GuiBase element in Elements)
				element.Draw();
		}


		#endregion


		
		#region Element management

		/// <summary>
		/// Removes all gui elements
		/// </summary>
		public void Clear()
		{
			Elements.Clear();
		}


		/// <summary>
		/// Adds an element
		/// </summary>
		/// <param name="element"></param>
		public void Add(GuiBase element)
		{
			Elements.Add(element);
		}


		/// <summary>
		/// Removes an element
		/// </summary>
		/// <param name="element"></param>
		public void Remove(GuiBase element)
		{
			Elements.Remove(element);
		}


		#endregion



		#region Properties


		/// <summary>
		/// List of all gui elements
		/// </summary>
		List<GuiBase> Elements;

		#endregion

	}
}
