using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Describes a custom vertex format structure that contains position, color, and one set of texture coordinates. 
	/// </summary>
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public struct VertexPositionColorTexture
	{

		#region Properties

		/// <summary>
		/// The vertex position.
		/// </summary>
		public Vector3 Position;


		/// <summary>
		/// 	The vertex color.
		/// </summary>
		public Color Color;


		/// <summary>
		/// 	The texture coordinates.
		/// </summary>
		public Vector2 TextureCoordinate;

		#endregion


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="position">Position</param>
		/// <param name="color">Color</param>
		/// <param name="textureCoordinate">Texture coordinate</param>
		public VertexPositionColorTexture(Vector3 position, Color color, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.Color = color;
			this.TextureCoordinate = textureCoordinate;
		}


		/// <summary>
		/// Size of the structure in bytes
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 36;
			}
		}



	}

}
