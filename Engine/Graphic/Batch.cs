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

using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
//using Tao.OpenGl;
using System.Drawing;


namespace ArcEngine.Graphic
{
	/// <summary>
	/// http://www.taoframework.com/node/463
	/// http://go-mono.com/forums/#nabble-td4124137
	/// 
	/// 
	/// 
	/// </summary>
	public class Batch : IDisposable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Batch()
		{
			BufferID = new int[] { -1, -1, -1 };

			GL.GenBuffers(3, out BufferID[0]);
			Size = 0;
		}


		/// <summary>
		/// 
		/// </summary>
		~Batch()
		{
			Dispose(false);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Begin()
		{
			Offset = 0;
		}




		/// <summary>
		/// 
		/// </summary>
		public void End()
		{
			try
			{
				// Update Vertex buffer
				GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID[0]);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(int) * Vertex.Length), Vertex, BufferUsageHint.StaticDraw);

				// Update Texture buffer
				GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID[1]);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(int) * Texture.Length), Texture, BufferUsageHint.StaticDraw);

				// Update Color buffer
				GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID[2]);
				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(byte) * Color.Length), Color, BufferUsageHint.StaticDraw);
			}
			catch (Exception e)
			{
				bool er = GL.IsBuffer(BufferID[0]);
				
				//Log.Send(new LogEventArgs(LogLevel.Fatal, e.Message, e.StackTrace));
				Trace.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
			}


		}


/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		public void Draw9(BeginMode mode)
		{

			if (true)
			{
				// Vertex
				GL.EnableClientState(EnableCap.VertexArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID[0]);
				GL.VertexPointer(2, VertexPointerType.Int, 0, IntPtr.Zero);


				// Texture
				GL.EnableClientState(EnableCap.TextureCoordArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID[1]);
				GL.TexCoordPointer(2, TexCoordPointerType.Int, 0, IntPtr.Zero);

				// Color
				GL.EnableClientState(EnableCap.ColorArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer, BufferID[2]);
				GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, IntPtr.Zero);



		
				GL.DrawArrays(mode, 0, Size * 8);


				GL.DisableClientState(EnableCap.VertexArray);
				GL.DisableClientState(EnableCap.TextureCoordArray);
				GL.DisableClientState(EnableCap.ColorArray);
			}
			else
			{
				GL.VertexPointer(2, VertexPointerType.Int, 0, Vertex);
				GL.TexCoordPointer(2, TexCoordPointerType.Int, 0, Texture);
				GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, Color);

				GL.DrawArrays(mode, 0, Size);
			}



		}
*/

		/// <summary>
		/// Adds a rectangle to the batch
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="tex"></param>
		/// <param name="color"></param>
		public void Blit(Rectangle rect, Rectangle tex, Color color)
		{
			if (Offset >= Size)
			{
				int newlength = Size + Size / 2;
				Array.Resize<int>(ref Vertex, newlength * 8);
				Array.Resize<int>(ref Texture, newlength * 8);
				Array.Resize<byte>(ref Color, newlength * 16);
			}
			
			Vertex[Offset * 8] = rect.Left;
			Vertex[Offset * 8 + 1] = rect.Top;
			Vertex[Offset * 8 + 2] = rect.Left;
			Vertex[Offset * 8 + 3] = rect.Bottom;
			Vertex[Offset * 8 + 4] = rect.Right;
			Vertex[Offset * 8 + 5] = rect.Bottom;
			Vertex[Offset * 8 + 6] = rect.Right;
			Vertex[Offset * 8 + 7] = rect.Top;



			Texture[Offset * 8] = tex.Left;
			Texture[Offset * 8 + 1] = tex.Top;
			Texture[Offset * 8 + 2] = tex.Left;
			Texture[Offset * 8 + 3] = tex.Bottom;
			Texture[Offset * 8 + 4] = tex.Right;
			Texture[Offset * 8 + 5] = tex.Bottom;
			Texture[Offset * 8 + 6] = tex.Right;
			Texture[Offset * 8 + 7] = tex.Top;




			Color[Offset * 16 + 0] = color.R;
			Color[Offset * 16 + 1] = color.G;
			Color[Offset * 16 + 2] = color.B;
			Color[Offset * 16 + 3] = color.A;

			Color[Offset * 16 + 4] = color.R;
			Color[Offset * 16 + 5] = color.G;
			Color[Offset * 16 + 6] = color.B;
			Color[Offset * 16 + 7] = color.A;

			Color[Offset * 16 + 8] = color.R;
			Color[Offset * 16 + 9] = color.G;
			Color[Offset * 16 + 10] = color.B;
			Color[Offset * 16 + 11] = color.A;

			Color[Offset * 16 + 12] = color.R;
			Color[Offset * 16 + 13] = color.G;
			Color[Offset * 16 + 14] = color.B;
			Color[Offset * 16 + 15] = color.A;
	
			Offset++;
		}




		#region disposing

		/// <summary>
		/// 
		/// </summary>
		bool disposed;



		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		// Dispose(bool disposing) executes in two distinct scenarios.
		// If disposing equals true, the method has been called directly
		// or indirectly by a user's code. Managed and unmanaged resources
		// can be disposed.
		// If disposing equals false, the method has been called by the
		// runtime from inside the finalizer and you should not reference
		// other objects. Only unmanaged resources can be disposed.
		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				BufferID[0] = -1;
				BufferID[1] = -1;
				BufferID[2] = -1;

				// Note disposing has been done.
				disposed = true;
			}
		}


		#endregion


		#region Properties


		/// <summary>
		/// Gets / sets the size of the buffer
		/// </summary>
		public int Size
		{
			get
			{
				return Vertex.Length / 8;
			}

			set
			{
				Offset = 0;
				Vertex = new int[value * 8];
				Texture = new int[value * 8];
				Color = new byte[value * 16];
			}
		}


		/// <summary>
		/// Offset in the buffer
		/// </summary>
		public int Offset
		{
			get;
			protected set;
		}


		/// <summary>
		/// Buffer ID
		/// </summary>
		/// <remarks>Buffer 0 => Vertex
		/// Buffer 1 => Textures
		/// Buffer 2 => Color</remarks>
		internal int[] BufferID
		{
			get;
			private set;
		}

		
		/// <summary>
		/// 
		/// </summary>
		int[] Vertex;
		
		/// <summary>
		/// 
		/// </summary>
		int[] Texture;


		/// <summary>
		/// 
		/// </summary>
		byte[] Color;

		#endregion

	}
}
