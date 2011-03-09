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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.PInvoke;


namespace ArcEngine
{
	/// <summary>
	/// Game cursor
	/// </summary>
	static public class GameCursor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		static GameCursor()
		{

		}


		/// <summary>
		/// Change cursor
		/// </summary>
		/// <param name="id">Id of the tile</param>
		/// <returns></returns>
		static public bool SetTile(int id)
		{
			if (Tileset == null)
				return false;

			// Get the tile
			Tile tile = Tileset.GetTile(id);
			if (tile == null)
				return false;

			// Convert the tile to a system icon
			//Bitmap bm = Tileset.Texture.ToBitmap(tile.Rectangle);
			//ToCursor(bm, tile.Origin);

			return true;
		}


				/// <summary>
		/// Creates a new hardware cursor
		/// </summary>
		/// <param name="bmp">Bitmap handle</param>
		/// <param name="hotspot">Cursor hotspot</param>
		static public void ToCursor(Bitmap bmp, Point hotspot)
		{
			User32.IconInfo tmp = new User32.IconInfo();
			User32.GetIconInfo(bmp.GetHicon(), ref tmp);
			tmp.xHotspot = hotspot.X;
			tmp.yHotspot = hotspot.Y;
			tmp.fIcon = false;
			
			Cursor = new Cursor(User32.CreateIconIndirect(ref tmp));
		}



		#region Properties


		/// <summary>
		/// Tileset to use
		/// </summary>
		static public TileSet Tileset
		{
			get;
			set;
		}


		/// <summary>
		/// Cursor handle
		/// </summary>
		static public Cursor Cursor
		{
			get;
			private set;
		}

		#endregion

	}
}
