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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Drawing;

namespace ArcEngine.Utility.GUI
{

	/// <summary>
	/// Graphical User Interface manager
	/// </summary>
	/// <remarks>The GUIManager handles resource deallocation of all Control's it contains. 
	/// Once you add a Control you don't own anymore the pointer. Deleting the Control outside of the GUIManager will result in crashes.</remarks>
	public class GuiManager : IDisposable
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public GuiManager()
		{
			Controls = new List<Control>();

			Batch = new SpriteBatch();
		}




		#region Update and Draw

		/// <summary>
		/// Updates elements
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public void Update(GameTime time)
		{
			foreach (Control control in Controls)
				control.Update(this, time);
		}



		/// <summary>
		/// Draws elements
		/// </summary>
		public void Draw()
		{
			Batch.Begin();

			foreach (Control element in Controls)
				element.Draw(this, Batch);

			Batch.End();
		}


		#endregion


		
		#region Element management

		/// <summary>
		/// Removes all gui elements
		/// </summary>
		public void Clear()
		{
			Controls.Clear();
		}


		/// <summary>
		/// Adds an element
		/// </summary>
		/// <param name="element"></param>
		public void Add(Control element)
		{
			if (element == null)
				return;


			Controls.Add(element);
		}


		/// <summary>
		/// Removes an element
		/// </summary>
		/// <param name="element"></param>
		public void Remove(Control element)
		{
			if (element == null)
				return;

			Controls.Remove(element);
		}


		#endregion


		/// <summary>
		/// Disposes resources
		/// </summary>
		public void Dispose()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

			if (Batch != null)
				Batch.Dispose();
			Batch = null;


			IsDisposed= true;
		}


		#region Properties


		/// <summary>
		/// List of all gui elements
		/// </summary>
		List<Control> Controls;



		/// <summary>
		/// Font to draw text
		/// </summary>
		public BitmapFont Font;


		/// <summary>
		/// Tileset to use
		/// </summary>
		public TileSet TileSet;


		/// <summary>
		/// Resource disposed
		/// </summary>
		public bool IsDisposed
		{
			get;
			private set;
		}


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch Batch;

		#endregion

	}
}
