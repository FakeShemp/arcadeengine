#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.PInvoke;

namespace ArcEngine.Graphic
{

	/// <summary>
	/// Hardware cursor
	/// </summary>
	public class HardwareCursor
	{
		/// <summary>
		/// Creates a new hardware cursor
		/// </summary>
		/// <param name="bmp">Bitmap handle</param>
		/// <param name="hotspot">Cursor hotspot</param>
		public HardwareCursor(Bitmap bmp, Point hotspot)
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
		/// Cursor handle
		/// </summary>
		public Cursor Cursor
		{
			get;
			private set;
		}

		#endregion

	}
}
