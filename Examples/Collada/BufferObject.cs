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
using OpenTK.Graphics.OpenGL;
using ArcEngine.Graphic;
using System.Runtime.InteropServices;


namespace ArcEngine.Examples
{
	/// <summary>
	/// Stores user datas in graphic card's memory
	/// </summary>
	public class BufferObject<T> : IDisposable where T : struct
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BufferObject()
		{
			GL.GenBuffers(1, out Handle);

			Buffer = new List<T>();
			ElementSize = Marshal.SizeOf(typeof(T));
		}



		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Handle != 0)
			{
				GL.DeleteBuffers(1, ref Handle);
				Handle = 0;
			}

		}



		/// <summary>
		/// Clears the buffer
		/// </summary>
		public void Clear()
		{
			Buffer.Clear();
		}


		/// <summary>
		/// Adds a value to the buffer
		/// </summary>
		/// <param name="value">Value to add</param>
		public void Add(T value)
		{
			Buffer.Add(value);
		}



		/// <summary>
		/// Apply updates
		/// </summary>
		public void Apply()
		{
			if (!Display.Capabilities.HasVBO)
				return;

			try
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Count * ElementSize), Buffer.ToArray(), BufferUsageHint.StaticDraw);
			}
			catch (Exception e)
			{
				bool er = GL.IsBuffer(Handle);
				Trace.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
			}


		}




		#region Properties

		/// <summary>
		/// Buffer internal handle
		/// </summary>
		int Handle;


		/// <summary>
		/// Internal buffer
		/// </summary>
		List<T> Buffer;


		/// <summary>
		/// Number of elements in the buffer
		/// </summary>
		public int Count
		{
			get
			{
				return Buffer.Count;
			}
		}


		/// <summary>
		/// Size of an element
		/// </summary>
		int ElementSize;


		#endregion
	}
}
