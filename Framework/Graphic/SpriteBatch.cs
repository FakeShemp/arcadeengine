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
using ArcEngine.Asset;


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
			BackToFrontComp = new BackToFrontComparer();
			FrontToBackComp = new FrontToBackComparer();
			TextureComp = new TextureComparer();


			// Detect the good shader
			ShaderVersion version = Shader.CurrentVersion;
			string vertname = "";
			string fragname = "";

			switch (version)
			{
				default:
				case ShaderVersion.GLSL_1_20:
					vertname = "ArcEngine.Graphic.Shaders.V1_20.SpriteBatch.vert";
					fragname = "ArcEngine.Graphic.Shaders.V1_20.SpriteBatch.frag";
				break;
				case ShaderVersion.GLSL_1_30:
				case ShaderVersion.GLSL_1_40:
				case ShaderVersion.GLSL_1_50:
				case ShaderVersion.GLSL_3_30:
				case ShaderVersion.GLSL_4_00:
				case ShaderVersion.GLSL_4_10:
				{
					vertname = "ArcEngine.Graphic.Shaders.V1_30.SpriteBatch.vert";
					fragname = "ArcEngine.Graphic.Shaders.V1_30.SpriteBatch.frag";
				}
				break;
			}

			Shader = new Shader();
			using (Stream stream = ResourceManager.GetInternalResource(vertname))
			{
				if (stream != null)
				{
					StreamReader reader = new StreamReader(stream);
					string src = reader.ReadToEnd();
					Shader.SetSource(ShaderType.VertexShader, src);
				}
			}

			using (Stream stream = ResourceManager.GetInternalResource(fragname))
			{
				if (stream != null)
				{
					StreamReader reader = new StreamReader(stream);
					string src = reader.ReadToEnd();
					Shader.SetSource(ShaderType.FragmentShader, src);
				}
			}
			Shader.Compile();


			// Create a 1x1 empty texture
			WhiteTexture = new Texture2D(new Size(1, 1));
			Bitmap bm = new Bitmap(1, 1);
			bm.SetPixel(0, 0, Color.White);
			WhiteTexture.SetData(bm, Point.Empty);
			bm.Dispose();
			WhiteTexture.MinFilter = TextureMinFilter.Nearest;
			WhiteTexture.MagFilter = TextureMagFilter.Nearest;
		}

		
		/// <summary>
		/// Destructor
		/// </summary>
		~SpriteBatch()
		{
			if (!IsDisposed)
			{
				//throw new Exception(this + " not disposed, Call Dispose() !!");
				System.Windows.Forms.MessageBox.Show(this + " not disposed, Call Dispose() !!", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			}
		}


		/// <summary>
		/// Immediately releases the unmanaged resources used by this object.
		/// </summary>
		public void Dispose()
		{
			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

			if (WhiteTexture != null)
				WhiteTexture.Dispose();
			WhiteTexture = null;

			if (Shader != null)
				Shader.Dispose();
			Shader = null;

			IsDisposed = true;
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

		#endregion


		#region Internal

		/// <summary>
		/// Flush all pending data
		/// </summary>
		public void Flush()
		{
			if (spriteQueueCount == 0)
				return;

			if (SortMode == SpriteSortMode.Immediate)
			{
				RenderBatch(CurrentTexture, Sprites, 0, spriteQueueCount);
			}
			else
			{
				// Sort sprites
				if (SortMode != SpriteSortMode.Deferred)
					Sort();

				int offset = 0;
				PrimitiveType type = Sprites[0].Type;
				CurrentTexture = Sprites[0].Texture;
				for (int i = 0; i < spriteQueueCount; i++)
				{
					if (CurrentTexture != Sprites[i].Texture || Sprites[i].Type != type)
					{
						RenderBatch(CurrentTexture, Sprites, offset, i - offset);
						CurrentTexture = Sprites[i].Texture;
						type = Sprites[i].Type;
						offset = i;
					}
				}
				RenderBatch(CurrentTexture, Sprites, offset, spriteQueueCount - offset);
			}

			spriteQueueCount = 0;
			CurrentTexture = null;
		}


		/// <summary>
		/// Sort sprite list
		/// </summary>
		void Sort()
		{
			IComparer<SpriteVertex> comparer = null;

			// Enough space ?
			if (sortedSprites == null || sortedSprites.Length < spriteQueueCount)
			{
				sortedSprites = new SpriteVertex[spriteQueueCount];
			}

			switch (SortMode)
			{
				case SpriteSortMode.Texture:
				comparer = TextureComp;
				break;

				case SpriteSortMode.BackToFront:
				comparer = BackToFrontComp;
				break;

				case SpriteSortMode.FrontToBack:
				comparer = FrontToBackComp;
				break;
			}

			Array.Sort<SpriteVertex>(Sprites, 0, spriteQueueCount, comparer);
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
			Display.RenderState.MultiSample = false;
		}


		/// <summary>
		/// Renders sprites
		/// </summary>
		/// <param name="texture">Texture to use</param>
		/// <param name="vertices">Sprite array</param>
		/// <param name="offset">First element in the array</param>
		/// <param name="count">Number of element in the array</param>
		void RenderBatch(Texture2D texture, SpriteVertex[] vertices, int offset, int count)
		{
			if (count == 0 || texture == null)
				return;

			Display.Shader = Shader;

			// Build matrix
			Matrix4 ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Display.ViewPort.Width, Display.ViewPort.Height, 0.0f, -1.0f, 1.0f);
			Shader.SetUniform("projection_matrix", ProjectionMatrix);

			Matrix4 textureMatrix = Matrix4.Scale(1.0f / texture.Size.Width, 1.0f / texture.Size.Height, 1.0f);
			Shader.SetUniform("texture_matrix", textureMatrix);

			Display.TextureUnit = 0;
			Display.Texture = texture;
			Shader.SetUniform("texture", 0);

			for (int i = offset; i < offset + count; i++)
			{
                // Destination on the screen
				Vector4 dst = new Vector4(
					Sprites[i].Destination.X - Sprites[i].Origin.X * Sprites[i].Scale.X,
					Sprites[i].Destination.Y - Sprites[i].Origin.Y * Sprites[i].Scale.Y,
					Sprites[i].Destination.Width * Sprites[i].Scale.X, 
                    Sprites[i].Destination.Height * Sprites[i].Scale.Y);

				// Four vertex
				Vector3 A = new Vector3(dst.X, dst.Y, Sprites[i].Depth);
				Vector3 B = new Vector3(dst.Right, dst.Top, Sprites[i].Depth);
				Vector3 C = new Vector3(dst.Right, dst.Bottom, Sprites[i].Depth);
				Vector3 D = new Vector3(dst.X, dst.Bottom, Sprites[i].Depth);

				// Apply rotation
				if (Sprites[i].Rotation != 0.0f)
				{
					// Degree to radian
					float theta = Sprites[i].Rotation * (float) Math.PI / 180.0f;

					Vector2 o = dst.Xy + Vector2.Multiply(Sprites[i].Origin, Sprites[i].Scale);

					Vector3 p = A;
					A.X = (float) Math.Cos(theta) * (p.X - o.X) - (float) Math.Sin(theta) * (p.Y - o.Y) + o.X;
					A.Y = (float) Math.Sin(theta) * (p.X - o.X) + (float) Math.Cos(theta) * (p.Y - o.Y) + o.Y;

					p = B;
					B.X = (float) Math.Cos(theta) * (p.X - o.X) - (float) Math.Sin(theta) * (p.Y - o.Y) + o.X;
					B.Y = (float) Math.Sin(theta) * (p.X - o.X) + (float) Math.Cos(theta) * (p.Y - o.Y) + o.Y;

					p = C;
					C.X = (float) Math.Cos(theta) * (p.X - o.X) - (float) Math.Sin(theta) * (p.Y - o.Y) + o.X;
					C.Y = (float) Math.Sin(theta) * (p.X - o.X) + (float) Math.Cos(theta) * (p.Y - o.Y) + o.Y;

					p = D;
					D.X = (float) Math.Cos(theta) * (p.X - o.X) - (float) Math.Sin(theta) * (p.Y - o.Y) + o.X;
					D.Y = (float) Math.Sin(theta) * (p.X - o.X) + (float) Math.Cos(theta) * (p.Y - o.Y) + o.Y;

				}


				// Color
				Color color = Sprites[i].Color;

                // Texture coordinate
                Vector4 uv = Sprites[i].UV;

				// Texture flip
				if ((Sprites[i].Effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally)
                {
                    uv.X += uv.Z;
                    uv.Z = -uv.Z;
                }
                if ((Sprites[i].Effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically)
                {
                    uv.Y += uv.W;
                    uv.W = -uv.W;
                }

				// Rendering type
				switch (Sprites[i].Type)
				{

					// Lines
					case PrimitiveType.Lines:
					{
						Buffer.AddPoint(A, color, uv.Xy);									// A
						Buffer.AddPoint(C, color, new Vector2(uv.Right, uv.Bottom));		// C
					}
					break;

					// Rectangles
					case PrimitiveType.LineStrip:
					{
						Buffer.AddPoint(A, color, uv.Xy);									// A
						Buffer.AddPoint(D, color, new Vector2(uv.X, uv.Bottom));			// D
						Buffer.AddPoint(C, color, new Vector2(uv.Right, uv.Bottom));		// C
						Buffer.AddPoint(B, color, new Vector2(uv.Right, uv.Top));			// B
					
					}
					break;

					case PrimitiveType.Points:
					{
						Buffer.AddPoint(A, color, uv.Xy);									// A
					}
					break;

					default:
					{
						Buffer.AddPoint(A, color, uv.Xy);									// A
						Buffer.AddPoint(B, color, new Vector2(uv.Right, uv.Top));			// B
						Buffer.AddPoint(C, color, new Vector2(uv.Right, uv.Bottom));		// C

						Buffer.AddPoint(A, color, uv.Xy);									// A
						Buffer.AddPoint(C, color, new Vector2(uv.Right, uv.Bottom));		// C
						Buffer.AddPoint(D, color, new Vector2(uv.X, uv.Bottom));			// D
					}
					break;
				}
				
			}


			count = Buffer.Update();
			Display.DrawBatch(Buffer, Sprites[offset].Type, 0, count);
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
		/// <param name="scale">Scaling factor</param>
		/// <param name="effect">Rotations to apply prior to rendering.</param>
		/// <param name="depth">The sorting depth of the sprite</param>
		/// <param name="type">Type of rendering</param>
		void InternalDraw(Texture2D texture, ref Rectangle destination, ref Rectangle source, Color color, float rotation, Point origin, Vector2 scale, SpriteEffects effect, float depth, PrimitiveType type)
		{
			Vector4 src = new Vector4(source.X, source.Y, source.Width, source.Height);

			Vector4 dst = new Vector4(destination.X, destination.Y, destination.Width, destination.Height);

			Vector2 ori = new Vector2(origin.X, origin.Y);

			InternalDraw(texture, ref dst, ref src, color, rotation, ori, scale, effect, depth, type);
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
		/// <param name="scale">Scale factor</param>
		/// <param name="effect">Rotations to apply prior to rendering.</param>
		/// <param name="depth">The sorting depth of the sprite</param>
		/// <param name="type">Type of rendering</param>
		void InternalDraw(Texture2D texture, ref Vector4 destination, ref Vector4 source, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float depth, PrimitiveType type)
		{
			if (texture == null || !InUse) 
				return;

			// If immediate mode AND texture is not the same
			if (SortMode == SpriteSortMode.Immediate && CurrentTexture != texture)
			{
				if (spriteQueueCount > 0)
					Flush();
				CurrentTexture = texture;
			}


			// Buffer too small ?
			if (spriteQueueCount >= Sprites.Length)
			{
				Array.Resize<SpriteVertex>(ref Sprites, Sprites.Length * 2);
			}

			


			// Texture rectangle
			if (source == Vector4.Zero)
			{
				Sprites[spriteQueueCount].UV.X = 0.0f;
				Sprites[spriteQueueCount].UV.Y = 0.0f;
				Sprites[spriteQueueCount].UV.Z = texture.Size.Width;
				Sprites[spriteQueueCount].UV.W = texture.Size.Height;
			}
			else
			{
				Sprites[spriteQueueCount].UV.X = source.X;
				Sprites[spriteQueueCount].UV.Y = source.Y;
				Sprites[spriteQueueCount].UV.Z = source.Width;
				Sprites[spriteQueueCount].UV.W = source.Height;
            }


            // Add to queue
			Sprites[spriteQueueCount].Destination.X = destination.X;
			Sprites[spriteQueueCount].Destination.Y = destination.Y;
			Sprites[spriteQueueCount].Destination.Z = destination.Width;
			Sprites[spriteQueueCount].Destination.W = destination.Height;
			Sprites[spriteQueueCount].Scale = scale;
			Sprites[spriteQueueCount].Color = color;
			Sprites[spriteQueueCount].Depth = depth;
			Sprites[spriteQueueCount].Effects = effect;
			Sprites[spriteQueueCount].Origin.X = origin.X;
			Sprites[spriteQueueCount].Origin.Y = origin.Y;
			Sprites[spriteQueueCount].Rotation = rotation;
			Sprites[spriteQueueCount].Texture = texture;
			Sprites[spriteQueueCount].Type = type;

			spriteQueueCount++;
		}

		#endregion


		#region Draw

		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, and color tint. 
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White  for full color with no tinting. </param>
		public void Draw(Texture2D texture, Point position, Color color)
		{
			if (texture == null)
				return;

			Rectangle destination = new Rectangle(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			Rectangle source = Rectangle.Empty;

			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, and color tint. 
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White  for full color with no tinting. </param>
		public void Draw(Texture2D texture, Vector2 position, Color color)
		{
			if (texture == null)
				return;

			Vector4 dst = new Vector4(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			Vector4 src = Vector4.Zero;

			InternalDraw(texture, ref dst, ref src, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination rectangle, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="destination">A rectangle specifying, in screen coordinates, where the sprite will be drawn. 
		/// If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting</param>
		public void Draw(Texture2D texture, Rectangle destination, Color color)
		{
			Rectangle source = Rectangle.Empty;

			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination and source rectangles, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. 
		/// Use Rectangle.Empty to draw the entire texture.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void Draw(Texture2D texture, Point position, Rectangle source, Color color)
		{
			if (texture == null)
				return;

			Rectangle destination = new Rectangle(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}



		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination and source rectangles, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. 
		/// Use Rectangle.Empty to draw the entire texture.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void Draw(Texture2D texture, Vector2 position, Vector4 source, Color color)
		{
			if (texture == null)
				return;

			Vector4 dst = new Vector4(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			Vector4 src = new Vector4(source.X, source.Y, source.Width, source.Height);

			InternalDraw(texture, ref dst, ref src, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}



		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination and source rectangles, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="destination">A rectangle specifying, in screen coordinates, where the sprite will be drawn.
		/// If this rectangle is not the same size as sourcerectangle the sprite will be scaled to fit</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. 
		/// Use Rectangle.Empty to draw the entire texture.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void Draw(Texture2D texture, Rectangle destination, Rectangle source, Color color)
		{
			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination and source rectangles, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="destination">A rectangle specifying, in screen coordinates, where the sprite will be drawn.
		/// If this rectangle is not the same size as sourcerectangle the sprite will be scaled to fit</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. 
		/// Use Rectangle.Empty to draw the entire texture.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void Draw(Texture2D texture, Vector4 destination, Vector4 source, Color color)
		{
			InternalDraw(texture, ref destination, ref source, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}


        /// <summary>
        /// Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position,
        /// optional source rectangle, color tint, rotation, origin, scale, effects, and sort depth.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
        /// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. Use Rectangle.Empty to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="rotation">The angle, in radians, to rotate the sprite around the origin.</param>
        /// <param name="origin">The origin of the sprite. Specify (0,0) for the upper-left corner.</param>
        /// <param name="scale">Value by which to scale the sprite width and height</param>
        /// <param name="effects">Rotations to apply prior to rendering.</param>
        /// <param name="layerDepth">The sorting depth of the sprite, between 0 (front) and 1 (back).</param>
        public void Draw(Texture2D texture, Point position, Rectangle source, Color color, float rotation, Point origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
			if (texture == null)
				return;

			Rectangle src = new Rectangle();
            src.X = position.X;
            src.Y = position.Y;
            src.Width = texture.Size.Width;
            src.Height = texture.Size.Height;

			InternalDraw(texture, ref src, ref source, color, rotation, origin, Vector2.One, effects, layerDepth, PrimitiveType.Triangles);
        }


        /// <summary>
        /// Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position,
        /// optional source rectangle, color tint, rotation, origin, scale, effects, and sort depth.
        /// </summary>
        /// <param name="texture">The sprite texture.</param>
        /// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. Use Vector4.Zero to draw the entire texture.</param>
        /// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
        /// <param name="rotation">The angle, in radians, to rotate the sprite around the origin.</param>
        /// <param name="origin">The origin of the sprite. Specify (0,0) for the upper-left corner.</param>
        /// <param name="scale">Value by which to scale the sprite width and height</param>
        /// <param name="effects">Rotations to apply prior to rendering.</param>
        /// <param name="layerDepth">The sorting depth of the sprite, between 0 (front) and 1 (back).</param>
        public void Draw(Texture2D texture, Vector2 position, Vector4 source, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
			if (texture == null)
				return;

			Vector4 src = new Vector4();
            src.X = position.X;
            src.Y = position.Y;
            src.Z = texture.Size.Width;
            src.W = texture.Size.Height;

			InternalDraw(texture, ref src, ref source, color, rotation, origin, scale, effects, layerDepth, PrimitiveType.Triangles);
			//InternalDraw(texture, ref src, ref source, color, rotation, origin, scale, effects, layerDepth, PrimitiveType.LineStrip);
		}

 

 

		#endregion


		#region Text drawing

		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="pos">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="text">Text to print</param>
		public void DrawString(BitmapFont font, Vector2 pos, Color color, string text)
		{
			DrawString(font, new Vector4(pos.X, pos.Y, 0.0f, 0.0f), color, text);
		}


		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="position">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="text">Text to print</param>
		public void DrawString(BitmapFont font, Point position, Color color, string text)
		{
			DrawString(font, new Vector4(position.X, position.Y, 0.0f, 0.0f), color, text);
		}



		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="position">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawString(BitmapFont font, Point position, Color color, string format, params object[] args)
		{
			DrawString(font, position, color, string.Format(format, args));
		}



		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="position">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawString(BitmapFont font, Vector2 position, Color color, string format, params object[] args)
		{
			DrawString(font, position, color, string.Format(format, args));
		}


		/// <summary>
		/// Prints some text on the screen within a zone with left justification
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="color">Color</param>
		/// <param name="text">Text to print</param>
		public void DrawString(BitmapFont font, Vector4 rectangle, Color color, string text)
		{
			DrawString(font, rectangle, TextJustification.Left, color, text);
		}


		/// <summary>
		/// Prints some text on the screen
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawString(BitmapFont font, Vector4 rectangle, Color color, string format, params object[] args)
		{
			DrawString(font, rectangle, color, string.Format(format, args));
		}


		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="color">Text color</param>
		/// <param name="text">Text to print</param>
		public void DrawString(BitmapFont font, Vector4 rectangle, TextJustification justification, Color color, string text)
		{
            if (font == null)
                return;

			font.DrawText(this, rectangle, justification, color, text);
		}



		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawString(BitmapFont font, Rectangle rectangle, TextJustification justification, Color color, string format, params object[] args)
		{
			DrawString(font, rectangle, justification, color, string.Format(format, args));
		}


		/// <summary>
        /// Prints some text on the screen 
        /// </summary>
        /// <param name="font">Font to use</param>
        /// <param name="rectangle">Rectangle of the text</param>
        /// <param name="color">Color</param>
        /// <param name="text">Text to print</param>
        public void DrawString(BitmapFont font, Rectangle rectangle, Color color, string text)
		{
            Vector4 zone = new Vector4();
            zone.X = rectangle.X;
            zone.Y = rectangle.Y;
            zone.Z = rectangle.Right;
            zone.W = rectangle.Width;

            DrawString(font, zone, TextJustification.Left, color, text);
		}

		#endregion


		#region Tiles

		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void DrawTile(TileSet tileset, int id, Point position, Color color)
		{
			DrawTile(tileset, id, new Vector2(position.X, position.Y), color, 0.0f, SpriteEffects.None, 0.0f);
		}


		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		public void DrawTile(TileSet tileset, int id, Point position)
		{
			DrawTile(tileset, id, new Vector2(position.X, position.Y), Color.White, 0.0f, SpriteEffects.None, 0.0f);
		}

		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="depth">Depth offset</param>
		public void DrawTile(TileSet tileset, int id, Point position, float depth)
		{
			DrawTile(tileset, id, position, Color.White, 0.0f, SpriteEffects.None, depth);
		}


		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		/// <param name="depth"></param>
		/// <param name="effect"></param>
		/// <param name="rotation"></param>
		public void DrawTile(TileSet tileset, int id, Point position, Color color, float rotation, SpriteEffects effect, float depth)
		{
			DrawTile(tileset, id, new Vector2(position.X, position.Y), color, rotation, effect, depth);
		}


		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		/// <param name="rotation"></param>
		/// <param name="scale">Scaling factor</param>
		/// <param name="effect"></param>
		/// <param name="depth"></param>
		public void DrawTile(TileSet tileset, int id, Point position, Color color, float rotation, Vector2 scale, SpriteEffects effect, float depth)
		{
			DrawTile(tileset, id, new Vector2(position.X, position.Y), color, rotation, scale, effect, depth);
		}


		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		/// <param name="rotation">Angle of rotation in radian</param>
		/// <param name="effect">Visual effect to apply</param>
		/// <param name="depth">Depth offset</param>
		public void DrawTile(TileSet tileset, int id, Vector2 position, Color color, float rotation, SpriteEffects effect, float depth)
		{
			DrawTile(tileset, id, position, color, rotation, Vector2.One, effect, depth);
		}


		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		/// <param name="rotation">Angle of rotation in radian</param>
		/// <param name="scale">Scaling factor</param>
		/// <param name="effect">Visual effect to apply</param>
		/// <param name="depth">Depth offset</param>
		public void DrawTile(TileSet tileset, int id, Vector2 position, Color color, float rotation, Vector2 scale, SpriteEffects effect, float depth)
		{
			if (tileset == null || id < 0)
				return;

			Tile tile = tileset.GetTile(id);
			if (tile == null)
				return;

			Vector4 dst = new Vector4();
			dst.X = position.X;
			dst.Y = position.Y;
			dst.Z = tile.Rectangle.Width * tileset.Scale.X;
			dst.W = tile.Rectangle.Height * tileset.Scale.Y;

			Vector4 src = new Vector4(tile.Rectangle.X, tile.Rectangle.Y, tile.Rectangle.Width, tile.Rectangle.Height);

			Vector2 origin = new Vector2(tile.Origin.X * tileset.Scale.X, tile.Origin.Y * tileset.Scale.Y);

			InternalDraw(tileset.Texture, ref dst, ref src, color, rotation, origin, scale, effect, depth, PrimitiveType.Triangles);
		}



		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void DrawTile(TileSet tileset, int id, Vector2 position, Color color)
		{
			DrawTile(tileset, id, position, color, 0.0f, SpriteEffects.None, 0.0f);
		}



		/// <summary>
		/// Draws a tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void DrawTile(TileSet tileset, int id, Vector3 position, Color color)
		{
			DrawTile(tileset, id, new Vector2(position), color, 0.0f, SpriteEffects.None, position.Z);
		}



		/// <summary>
		/// Draws a stretched tile from a TileSet
		/// </summary>
		/// <param name="tileset">TileSet to use</param>
		/// <param name="id">Tile id</param>
		/// <param name="position">Location on the screen</param>
		/// <param name="rectangle">Destination zone</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void DrawTile(TileSet tileset, int id, Point position, Rectangle rectangle, Color color)
		{
			if (tileset == null || id < 0)
				return;

			Tile tile = tileset.GetTile(id);
			if (tile == null)
				return;

			Vector4 dst = new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

			Vector4 src = new Vector4(tile.Rectangle.X, tile.Rectangle.Y, tile.Rectangle.Width, tile.Rectangle.Height);

			InternalDraw(tileset.Texture, ref dst, ref src, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Triangles);
		}

		#endregion


		#region Untextured

		#region Fills

		/// <summary>
		/// Draw a filled rectangle
		/// </summary>
		/// <param name="rect">Rectangle</param>
		/// <param name="color">Color</param>
		public void FillRectangle(Rectangle rect, Color color)
		{
			Draw(WhiteTexture, rect, new Rectangle(0, 0, 1, 1), color);
			//InternalDraw(WhiteTexture, 
		}


		/// <summary>
		/// Draw a filled rectangle
		/// </summary>
		/// <param name="rect">Rectangle</param>
		/// <param name="color">Color</param>
		public void FillRectangle(Vector4 rect, Color color)
		{
			Draw(WhiteTexture, rect, rect, color);
		}

		#endregion

		#region Draws

		/// <summary>
		/// Draws a colored rectangle
		/// </summary>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color</param>
		public void DrawRectangle(Rectangle rect, Color color)
		{
			Point[] points = new Point[5];
			points[0] = rect.Location;
			points[1] = new Point(rect.X, rect.Bottom);
			points[2] = new Point(rect.Right, rect.Bottom);
			points[3] = new Point(rect.Right, rect.Top);
			points[4] = rect.Location;

			DrawLines(points, color);
		}



		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="destination">Destination</param>
		/// <param name="color">Color</param>
		/// <param name="rotation">Rotation angle in radian</param>
		/// <param name="origin">Origin of rotation</param>
		public void DrawRectangle(Vector4 destination, Color color, float rotation, Vector2 origin)
		{
			InternalDraw(WhiteTexture, ref destination, ref destination, color, rotation, origin, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.LineStrip);
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="destination">Destination</param>
		/// <param name="color">Color</param>
		/// <param name="rotation">Rotation angle in radian</param>
		/// <param name="origin">Origin of rotation</param>
		/// <param name="scale">Scaling factor</param>
		public void DrawRectangle(Vector4 destination, Color color, float rotation, Vector2 origin, Vector2 scale)
		{
			InternalDraw(WhiteTexture, ref destination, ref destination, color, rotation, origin, scale, SpriteEffects.None, 0.0f, PrimitiveType.LineStrip);
		}


		/// <summary>
		/// Draws a line from point "from" to point "to"
		/// </summary>
		/// <param name="from">Starting point</param>
		/// <param name="to">Ending point</param>
		/// <param name="color">Color</param>
		public void DrawLine(Point from, Point to, Color color)
		{
			DrawLine(from.X, from.Y, to.X, to.Y, color);
		}


		/// <summary>
		/// Draws a line from point "from" to point "to"
		/// </summary>
		/// <param name="x1">X start</param>
		/// <param name="x2">Y start</param>
		/// <param name="y1">X end</param>
		/// <param name="y2">Y end</param>
		/// <param name="color">Color of the line</param>
		public void DrawLine(int x1, int y1, int x2, int y2, Color color)
		{
			Rectangle rect = new Rectangle(x1, y1, x2 - x1, y2 - y1);

			InternalDraw(WhiteTexture, ref rect, ref rect, color, 0.0f, Point.Empty, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Lines);
		}


		/// <summary>
		/// Draws a line from point "from" to point "to"
		/// </summary>
		/// <param name="from">Starting point</param>
		/// <param name="to">Ending point</param>
		/// <param name="color">Color</param>
		public void DrawLine(Vector2 from, Vector2 to, Color color)
		{
			DrawLine(from.X, from.Y, to.X, to.Y, color);
		}


		/// <summary>
		/// Draws a line from point "from" to point "to"
		/// </summary>
		/// <param name="x1">X start</param>
		/// <param name="x2">Y start</param>
		/// <param name="y1">X end</param>
		/// <param name="y2">Y end</param>
		/// <param name="color">Color</param>
		public void DrawLine(float x1, float y1, float x2, float y2, Color color)
		{
			Vector4 rect = new Vector4(x1, y1, x2 - x1, y2 - y1);

			InternalDraw(WhiteTexture, ref rect, ref rect, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Lines);
		}


		/// <summary>
		/// Draws a bunch of connected lines. The last point and the first point are not connected. 
		/// </summary>
		/// <param name="points">Points</param>
		/// <param name="color">Color</param>
		public void DrawLines(Point[] points, Color color)
		{
			int pos = 0;
			for (pos = 0; pos < points.Length - 1; pos++)
			{
				DrawLine(points[pos], points[pos + 1], color);
			}
		}


		/// <summary>
		/// Draws a bunch of line segments. Each pair of points represents a line segment which is drawn.
		/// No connections between the line segments are made, so there must be an even number of points. 
		/// </summary>
		/// <param name="points">Points</param>
		/// <param name="color">Color</param>
		public void DrawLineSegments(Point[] points, Color color)
		{
			int pos = 0;
			for (pos = 0; pos < points.Length - 1; pos += 2)
			{
				DrawLine(points[pos], points[pos + 1], color);
			}
		}



		/// <summary>
		/// Draws a bunch of connected lines. The last point and the first point are not connected. 
		/// </summary>
		/// <param name="points">Points</param>
		/// <param name="color">Color</param>
		public void DrawLines(Vector2[] points, Color color)
		{
			int pos = 0;
			for (pos = 0; pos < points.Length - 1; pos++)
			{
				DrawLine(points[pos], points[pos + 1], color);
			}
		}



		/// <summary>
		/// Draws a bunch of line segments. Each pair of points represents a line segment which is drawn.
		/// No connections between the line segments are made, so there must be an even number of points. 
		/// </summary>
		/// <param name="points">Points</param>
		/// <param name="color">Color</param>
		public void DrawLineSegments(Vector2[] points, Color color)
		{
			int pos = 0;
			for (pos = 0; pos < points.Length - 1; pos += 2)
			{
				DrawLine(points[pos], points[pos + 1], color);
			}
		}


		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="point">Location of the point</param>
		/// <param name="color">Color</param>
		public void DrawPoint(Point point, Color color)
		{
			DrawPoint(point.X, point.Y, color);
		}



		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="color">Color</param>
		public void DrawPoint(int x, int y, Color color)
		{
			DrawPoint((float)x, (float)y, color);
		}


		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="point">Location of the point</param>
		/// <param name="color">Color</param>
		public void DrawPoint(Vector2 point, Color color)
		{
			DrawPoint(point.X, point.Y, color);
		}



		/// <summary>
		/// Draws a point
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="color">Color</param>
		public void DrawPoint(float x, float y, Color color)
		{
			Vector4 rect = new Vector4(x, y, 0.0f, 0.0f);

			InternalDraw(WhiteTexture, ref rect, ref rect, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Points);
		}


		/// <summary>
		/// Draws a circle
		/// </summary>
		/// <param name="location">Location of the circle on the screen</param>
		/// <param name="radius">Circle radius</param>
		/// <param name="color">Color</param>
		public void DrawCircle(Point location, Point radius, Color color)
		{
			Vector4 dst = new Vector4();
			for (int i = 0; i < 180; i++)
			{
				dst.X = (float)(radius.X * Math.Cos(i) + location.X);
				dst.Y = (float)(radius.Y * Math.Sin(i) + location.Y);

				dst.Z = (float)(radius.X * Math.Cos(i + 0.1) + location.X) - dst.X;
				dst.W = (float)(radius.Y * Math.Sin(i + 0.1) + location.Y) - dst.Y;

				InternalDraw(WhiteTexture, ref dst, ref dst, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Lines);
			}
		}


		/// <summary>
		/// Draws a circle
		/// </summary>
		/// <param name="location">Location of the circle on the screen</param>
		/// <param name="radius">Circle radius</param>
		/// <param name="color">Color</param>
		public void DrawCircle(Vector2 location, Vector2 radius, Color color)
		{
			Vector4 dst = new Vector4();
			for (int i = 0; i < 180; i++)
			{
				dst.X = (float)(radius.X * Math.Cos(i) + location.X);
				dst.Y = (float)(radius.Y * Math.Sin(i) + location.Y);

				dst.Z = (float)(radius.X * Math.Cos(i + 0.1) + location.X) - dst.X;
				dst.W = (float)(radius.Y * Math.Sin(i + 0.1) + location.Y) - dst.Y;

				InternalDraw(WhiteTexture, ref dst, ref dst, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f, PrimitiveType.Lines);
			}
		}

		#endregion

		#endregion


		#region Properties

		/// <summary>
		/// Resource disposed
		/// </summary>
		public bool IsDisposed
		{
			get;
			private set;
		}


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
		Texture2D CurrentTexture;


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
		/// Sorted sprite queue
		/// </summary>
		SpriteVertex[] sortedSprites;


		/// <summary>
		/// Number of sprite in the buffer
		/// </summary>
	    public	int spriteQueueCount;


		/// <summary>
		/// 
		/// </summary>
		BackToFrontComparer BackToFrontComp;

		/// <summary>
		/// 
		/// </summary>
		FrontToBackComparer FrontToBackComp;

		/// <summary>
		/// 
		/// </summary>
		TextureComparer TextureComp;


		/// <summary>
		/// Uniform texture
		/// </summary>
		Texture2D WhiteTexture;

		#endregion


		#region Comparers

		/// <summary>
		/// Compare by depth (back first)
		/// </summary>
		private class BackToFrontComparer : IComparer<SpriteVertex>
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="left"></param>
			/// <param name="right"></param>
			/// <returns></returns>
			public int Compare(SpriteVertex left, SpriteVertex right)
			{
				if (left.Depth > right.Depth)
					return -1;

				if (left.Depth < right.Depth)
					return 1;

				return 0;
			}
		}


		/// <summary>
		/// Compair by depth (front first)
		/// </summary>
		private class FrontToBackComparer : IComparer<SpriteVertex>
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="left"></param>
			/// <param name="right"></param>
			/// <returns></returns>
			public int Compare(SpriteVertex left, SpriteVertex right)
			{
				if (left.Depth > right.Depth)
					return 1;

				if (left.Depth < right.Depth)
					return -1;

				return 0;
			}
		}


		/// <summary>
		/// Compare by texture
		/// </summary>
		private class TextureComparer : IComparer<SpriteVertex>
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="left"></param>
			/// <param name="right"></param>
			/// <returns></returns>
			public int Compare(SpriteVertex left, SpriteVertex right)
			{
				int ret = 0;

				if (left.Texture.Handle > right.Texture.Handle)
					ret = -1;

				if (left.Texture.Handle < right.Texture.Handle)
					ret = 1;

				return ret;
			}
		}


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
		public Vector4 UV;

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

		/// <summary>
		/// Texture
		/// </summary>
		public Texture2D Texture;

		/// <summary>
		/// Type of rendering
		/// </summary>
		public PrimitiveType Type;

		/// <summary>
		/// Scaling factor
		/// </summary>
		public Vector2 Scale;
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
