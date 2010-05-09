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
	/// These buffers contain vertex attributes, such as vertex coordinates, texture coordinate data, per vertex-color data, and normals.
	/// </summary>
	public class ArrayBuffer<T> : IDisposable where T : struct
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ArrayBuffer()
		{
			GL.GenBuffers(1, out Handle);
			Index = -1;
			ElementSize = Marshal.SizeOf(typeof(T));


			if (typeof(T) == typeof(int))
				Type = VertexAttribPointerType.Int;
			else if (typeof(T) == typeof(sbyte))
				Type = VertexAttribPointerType.Byte;
			else if (typeof(T) == typeof(double))
				Type = VertexAttribPointerType.Double;
			else if (typeof(T) == typeof(short))
				Type = VertexAttribPointerType.Short;
			else if (typeof(T) == typeof(byte))
				Type = VertexAttribPointerType.UnsignedByte;
			else if (typeof(T) == typeof(uint))
				Type = VertexAttribPointerType.UnsignedInt;
			else if (typeof(T) == typeof(ushort))
				Type = VertexAttribPointerType.UnsignedShort;
			else
				Type = VertexAttribPointerType.Float;
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
		/// Apply updates
		/// </summary>
		public void Update(T[] data)
		{
			try
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
				GL.BufferData<T>(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * ElementSize), data, BufferUsageHint.StaticDraw);
			}
			catch (Exception e)
			{
				bool er = GL.IsBuffer(Handle);
				Trace.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
			}


		}



		/// <summary>
		/// Defines an array of generic vertex attribute data
		/// </summary>
		/// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, or 4.</param>
		public void Bind(int index, int size)
		{
			GL.EnableVertexAttribArray(index);
			GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
			GL.VertexAttribPointer(index, size, Type, true, ElementSize, 0);
		}



		#region Properties

		/// <summary>
		/// Buffer internal handle
		/// </summary>
		int Handle;



		/// <summary>
		/// Size of an element
		/// </summary>
		int ElementSize;



		/// <summary>
		/// Enable index
		/// </summary>
		public int Index
		{
			get;
			private set;
		}


		/// <summary>
		/// Type of the data
		/// </summary>
		VertexAttribPointerType Type;
		
		#endregion
	}
}
