using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ArcEngine.Graphic;


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
			Sprites = new SpriteVertex[2048];
			Buffer = BatchBuffer.CreatePositionColorTextureBuffer();

			Shader = new Shader();
			using (Stream stream = ResourceManager.GetResource("ArcEngine.Graphic.Shaders.SpriteBatch.vert"))
			{
				StreamReader reader = new StreamReader(stream);
				string src = reader.ReadToEnd();
				Shader.SetSource(ShaderType.VertexShader, src);
			}

			using (Stream stream = ResourceManager.GetResource("ArcEngine.Graphic.Shaders.SpriteBatch.frag"))
			{
				StreamReader reader = new StreamReader(stream);
				string src = reader.ReadToEnd();
				Shader.SetSource(ShaderType.FragmentShader, src);
			}
			Shader.Compile();
		}


		/// <summary>
		/// Immediately releases the unmanaged resources used by this object.
		/// </summary>
		public void Dispose()
		{
			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;
		}


		#region Begin

		/// <summary>
		/// Prepares the graphics device for drawing sprites 
		/// </summary>
		public void Begin()
		{
			Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, false);
		}


		/// <summary>
		/// Prepares the graphics device for drawing sprites with specified blending options
		/// </summary>
		/// <param name="blend">Blending options to use when rendering</param>
		public void Begin(SpriteBlendMode blend)
		{
			this.Begin(blend, SpriteSortMode.Deferred, false);
		}

 		/// <summary>
		/// Prepares the graphics device for drawing sprites with specified blending, sorting, and render state options
		/// </summary>
		/// <param name="blendMode">Blending options to use when rendering</param>
		/// <param name="sortMode">Sorting options to use when rendering</param>
		/// <param name="save">Preserve rendering state options</param>
		public void Begin(SpriteBlendMode blendMode, SpriteSortMode sortMode, bool save)
		{
			if (InUse)
				throw new InvalidOperationException("End() must be called before Begin()");

			BlendMode = blendMode;
			SortMode = sortMode;
			SaveState = save;

			if (SortMode == SpriteSortMode.Immediate)
			{
				SetRenderState();
			}

			InUse = true;

		}

		#endregion


		/// <summary>
		/// Flushes the sprite batch and restores the device state to how it was before Begin was called.
		/// </summary>
		public void End()
		{
			if (!InUse)
				throw new InvalidOperationException("Begin must be called before End");

			if (SortMode == SpriteSortMode.Immediate)
			{
				SetRenderState();
			}

			if (spriteQueueCount > 0)
				Flush();


			// Restore graphic states
			if (SaveState)
				Display.RenderState.Apply(StateBlock);


			InUse = false;
		}


		#region Internal

		/// <summary>
		/// Flush all pending data
		/// </summary>
		void Flush()
		{
			if (SortMode == SpriteSortMode.Immediate)
			{
				RenderBatch(CurrentTexture, Sprites, 0, spriteQueueCount);
				CurrentTexture = null;
			}
			else
			{
				List<SpriteVertex> queue;


			}

			spriteQueueCount = 0;
		}


		/// <summary>
		/// Apply render states
		/// </summary>
		void SetRenderState()
		{
			// Preserve render states
			if (SaveState)
			{
				StateBlock = Display.RenderState.Capture();
			}

			Display.RenderState.Culling = true;
			Display.RenderState.DepthTest = false;
			Display.RenderState.Blending = true;
			Display.Texturing = true;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="vertices"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		void RenderBatch(Texture texture, SpriteVertex[] vertices, int offset, int count)
		{
			Display.TextureUnit = 0;
			Display.Texture = texture;
			Display.Shader = Shader;

			Matrix4 modelViewMatrix = Matrix4.Identity;
			Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Display.ViewPort.Width, Display.ViewPort.Height, 0.0f, -1.0f, 1.0f); ;
			Shader.SetUniform("modelview_matrix", modelViewMatrix * projectionMatrix);

			Matrix4 textureMatrix = Matrix4.Scale(1.0f / CurrentTexture.Size.Width, 1.0f / CurrentTexture.Size.Height, 1.0f);
			Shader.SetUniform("texture_matrix", textureMatrix);


			for (int i = 0; i < count; i++)
			{
				float cos = 1.0f;
				float sin = 0.0f;
				if (Sprites[i].Rotation != 0)
				{
					sin = (float)Math.Sin(Sprites[i].Rotation);
					cos = (float)Math.Cos(Sprites[i].Rotation);
				}

				Buffer.AddRectangle(Sprites[i].Destination, Sprites[i].Color, Sprites[i].Source);
			}

			count = Buffer.Update();
			Display.DrawBatch(Buffer, 0, count);

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="texture">Source texture</param>
		/// <param name="destination">The destination, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="source">Texture uv rectangle</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		/// <param name="rotation">The angle, in radians, to rotate the sprite around the origin.</param>
		/// <param name="origin">The origin of the sprite. Specify (0,0) for the upper-left corner.</param>
		/// <param name="effect">Rotations to apply prior to rendering.</param>
		/// <param name="depth">The sorting depth of the sprite</param>
		void InternalDraw(Texture texture, ref Vector4 destination, ref Vector4 source, Color color, float rotation, Vector2 origin, SpriteEffects effect, float depth)
		{
			if (texture == null || !InUse) 
				return;

			if (SortMode == SpriteSortMode.Immediate && CurrentTexture != texture)
			{
				if (spriteQueueCount > 0)
					Flush();
				CurrentTexture = texture;
			}

			// Buffer too short ?
			if (spriteQueueCount >= Sprites.Length)
			{
				Array.Resize<SpriteVertex>(ref Sprites, Sprites.Length * 2);
			}
			
			SpriteVertex sprite = new SpriteVertex();
			sprite.Source = source;
			sprite.Destination = destination;
			sprite.Color = color;
			sprite.Depth = depth;
			sprite.Effects = effect;
			sprite.Origin = origin;
			sprite.Rotation = rotation;

			Sprites[spriteQueueCount++] = sprite;
		}

		#endregion



		#region Draw

		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, and color tint. 
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White  for full color with no tinting. </param>
		public void Draw(Texture texture, Vector2 position, Color color)
		{
			if (texture == null)
				return;

			Vector4 destination;
			destination.X = position.X;
			destination.Y = position.Y;
			destination.Z = texture.Size.Width;
			destination.W = texture.Size.Height;

			Vector4 source;
			source.X = 0.0f;
			source.Y = 0.0f;
			source.Z = texture.Size.Width;
			source.W = texture.Size.Height;

			this.InternalDraw(texture, ref destination, ref source, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

		}

		#endregion





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
		/// Current texture in use
		/// </summary>
		Texture CurrentTexture;


		/// <summary>
		/// Sort mode
		/// </summary>
		SpriteSortMode SortMode;


		/// <summary>
		/// Sprite blend mode
		/// </summary>
		SpriteBlendMode BlendMode;


		/// <summary>
		/// Preserve state or not
		/// </summary>
		bool SaveState;


		/// <summary>
		/// Preserve renderstate
		/// </summary>
		StateBlock StateBlock;


		/// <summary>
		/// Queue of sprites to draw
		/// </summary>
		SpriteVertex[] Sprites;

		/// <summary>
		/// Number of sprite in the buffer
		/// </summary>
		int spriteQueueCount;

		#endregion

	}

	/// <summary>
	/// Sprite vertex definition
	/// </summary>
	struct SpriteVertex
	{
		/// <summary>
		/// Texture uv
		/// </summary>
		public Vector4 Source;

		/// <summary>
		/// The destination, in screen coordinates, where the sprite will be drawn.
		/// </summary>
		public Vector4 Destination;

		/// <summary>
		/// The origin of the sprite
		/// </summary>
		public Vector2 Origin;

		/// <summary>
		/// Rotation to apply
		/// </summary>
		public float Rotation;

		/// <summary>
		/// The sorting depth of the sprite
		/// </summary>
		public float Depth;

		/// <summary>
		/// Rotations to apply prior to rendering
		/// </summary>
		public SpriteEffects Effects;

		/// <summary>
		/// The color channel modulation to use
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


	/// <summary>
	/// Defines sprite sort-rendering options.
	/// </summary>
	public enum SpriteSortMode
	{
		/// <summary>
		/// Begin will apply new graphics device settings, and sprites will be drawn within each Draw call. 
		/// In Immediate mode there can only be one active SpriteBatch instance without introducing conflicting device settings.
		/// </summary>
		Immediate,

		/// <summary>
		/// Sprites are not drawn until End is called. End will apply graphics device settings 
		/// and draw all the sprites in one batch, in the same order calls to Draw were received.
		/// </summary>
		Deferred,

		/// <summary>
		/// Sprites are sorted by texture prior to drawing. This can improve performance 
		/// when drawing non-overlapping sprites of uniform depth.
		/// </summary>
		Texture,
		
		/// <summary>
		/// Same as Deferred mode, except sprites are sorted by depth in back-to-front order prior to drawing. 
		/// </summary>
		BackToFront,

		/// <summary>
		/// Sprites are sorted by depth in front-to-back order prior to drawing. 
		/// </summary>
		FrontToBack
	}


	/// <summary>
	/// specify sprite blending rendering options to the flags parameter in Begin
	/// </summary>
	public enum SpriteBlendMode
	{
		/// <summary>
		/// No blending specified
		/// </summary>
		None,

		/// <summary>
		/// Enable alpha blending
		/// </summary>
		AlphaBlend,

		/// <summary>
		/// Enable additive blending.
		/// </summary>
		Additive
	}




}
