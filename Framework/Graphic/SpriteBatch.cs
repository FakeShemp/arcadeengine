using System.Drawing;
using System;
using System.Collections.Generic;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Enables a group of sprites to be drawn using the same settings
	/// </summary>
	public class SpriteBatch : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the class
		/// </summary>
		public SpriteBatch()
		{
			ModelViewMatrix = new Matrix4();
			ProjectionMatrix = new Matrix4();
			TextureMatrix = new Matrix4();

			Buffer = BatchBuffer.CreatePositionColorTextureBuffer();
			Shader = new Shader();
		}


		/// <summary>
		/// Immediately releases the unmanaged resources used by this object.
		/// </summary>
		public void Dispose()
		{
			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;
		}


		/// <summary>
		/// Prepares the graphics device for drawing sprites 
		/// </summary>
		public void Begin()
		{
			Begin(false);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="immediate"></param>
		public void Begin(bool immediate)
		{
			if (InUse)
				throw new InvalidOperationException("End must be called before Begin");
			InUse = true;
		}



		/// <summary>
		/// Flushes the sprite batch and restores the device state to how it was before Begin  was called.
		/// </summary>
		public void End()
		{
			if (!InUse)
				throw new InvalidOperationException("Begin must be called before End");


			InUse = false;

		}



		/// <summary>
		/// Flush all pending data
		/// </summary>
		void Flush()
		{
		}


		#region Properties


		/// <summary>
		/// In Begin/End pair
		/// </summary>
		bool InUse;


		/// <summary>
		/// Batch buffer
		/// </summary>
		BatchBuffer Buffer;


		/// <summary>
		/// Shader
		/// </summary>
		Shader Shader;

		/// <summary>
		/// Model view matrix
		/// </summary>
		Matrix4 ModelViewMatrix;


		/// <summary>
		/// Projection matrix
		/// </summary>
		Matrix4 ProjectionMatrix;


		/// <summary>
		/// Texture matrix
		/// </summary>
		Matrix4 TextureMatrix;


		#endregion

	}

	/// <summary>
	/// Sprite vertex definition
	/// </summary>
	struct SpriteVertex
	{
		/// <summary>
		/// 
		/// </summary>
		public Vector4 Source;

		/// <summary>
		/// 
		/// </summary>
		public Vector4 Destination;

		/// <summary>
		/// 
		/// </summary>
		public Vector2 Origin;

		/// <summary>
		/// 
		/// </summary>
		public float Rotation;

		/// <summary>
		/// 
		/// </summary>
		public float Depth;

		/// <summary>
		/// 
		/// </summary>
		public SpriteEffects Effects;


		/// <summary>
		/// Color
		/// </summary>
		public Color Color;
	}





	/// <summary>
	/// Defines sprite mirroring options.
	/// </summary>
	[Flags]
	public enum SpriteEffects
	{
		/// <summary>
		/// Rotate 180 degrees about the Y axis before rendering.
		/// </summary>
		FlipHorizontally = 0x1,

		/// <summary>
		/// Rotate 180 degrees about the X axis before rendering.
		/// </summary>
		FlipVertically = 0x2,

		/// <summary>
		/// No rotations specified.
		/// </summary>
		None = 0x0,
	}

 


}
