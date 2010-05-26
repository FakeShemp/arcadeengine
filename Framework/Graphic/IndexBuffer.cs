using System;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using ArcEngine.Asset;


//
// http://www.spec.org/gwpg/gpc.static/vbo_whitepaper.html
// http://bakura.developpez.com/tutoriels/jeux/utilisation-vbo-avec-opengl-3-x/

namespace ArcEngine.Graphic
{
	/// <summary>
	/// This type of buffer is used mainly for the element pointer. It contains only indices of elements. 
	/// </summary>
	public class IndexBuffer : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public IndexBuffer()
		{
			GL.GenBuffers(1, out indexHandle);
			GL.GenBuffers(1, out vertexHandle);
			//GL.GenVertexArrays(1, out vaoHandle);

			UsageMode = BufferUsageHint.StaticDraw;

			Declarations = new List<VertexDeclaration>();
		}


		/// <summary>
		/// Destructor
		/// </summary>
		~IndexBuffer()
		{
			if (indexHandle != -1)
				throw new Exception("IndexBuffer : Call Dispose() !!");
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			GL.DeleteBuffers(1, ref indexHandle);
			indexHandle = -1;

			GL.DeleteBuffers(1, ref vertexHandle);
			vertexHandle = -1;

			//GL.DeleteVertexArrays(1, ref vaoHandle);
			//vaoHandle = -1;

			Count = 0;

			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Updates indices
		/// </summary>
		/// <param name="data"></param>
		public void SetIndices(int[] data)
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);
			GL.BufferData<int>(BufferTarget.ElementArrayBuffer, (IntPtr)(data.Length * sizeof(uint)), data, BufferUsageHint.StaticDraw);

			Count = data.Length;
		}


		/// <summary>
		/// Updates vertices
		/// </summary>
		/// <param name="data"></param>
		public void SetVertices(float[] data)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * sizeof(float)), data, UsageMode);
		}



		/// <summary>
		/// Binds the buffer
		/// </summary>
		/// <param name="shader"></param>
		public void Bind(Shader shader)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexHandle);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexHandle);


			if (shader == null)
				return;

			foreach (VertexDeclaration dec in Declarations)
			{
				int id = shader.GetAttribute(dec.Name);

				Display.EnableBufferIndex(id);
				Display.SetBufferDeclaration(id, dec.Size, dec.Stride, dec.Offset);
			}

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="attribut">Specifies the index of the generic vertex attribute to be modified.</param>
		/// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, or 4.</param>
		/// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. 
		/// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. </param>
		/// <param name="offset">Specifies a pointer to the first component of the first generic vertex attribute in the array. </param>
		public void AddDeclaration(string name, int size, int stride, int offset)
		{
			Declarations.Add(new VertexDeclaration(name, size, stride, offset));
		}


		/// <summary>
		/// Clear vertex declarations
		/// </summary>
		public void ClearDeclaration()
		{
			Declarations.Clear();
		}



		#region Properties

		/// <summary>
		/// Buffer internal handles
		/// </summary>
		int indexHandle;
		int vertexHandle;
		//int vaoHandle;


		/// <summary>
		/// Usage mode
		/// </summary>
		public BufferUsageHint UsageMode
		{
			get;
			set;
		}



		/// <summary>
		/// Number of element
		/// </summary>
		public int Count
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		List<VertexDeclaration> Declarations;

		#endregion


	}

	/// <summary>
	/// 
	/// </summary>
	struct VertexDeclaration
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="size"></param>
		/// <param name="stride"></param>
		/// <param name="offset"></param>
		public VertexDeclaration(string name, int size, int stride, int offset)
		{
			Name = name;
			Size = size;
			Stride = stride;
			Offset = offset;
		}

		public string Name;
		public int Size;
		public int Stride;
		public int Offset;
	}

}
