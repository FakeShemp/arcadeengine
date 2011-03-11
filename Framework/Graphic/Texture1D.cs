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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using ArcEngine.Asset;
using Imaging = System.Drawing.Imaging;
using TK = OpenTK.Graphics.OpenGL;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// 1D texture
	/// </summary>
	public class Texture1D : Texture
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Texture1D()
		{
			Target = TextureTarget.Texture1D;
			throw new NotImplementedException();
		}


		/// <summary>
		/// 
		/// </summary>
		public override void Dispose()
		{
			IsDisposed = true;
			InUse.Remove(this);
		}
	}
}
