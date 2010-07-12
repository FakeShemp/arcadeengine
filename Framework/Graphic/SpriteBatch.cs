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

			Shader = new Shader();
			using (Stream stream = ResourceManager.GetResource("ArcEngine.Graphic.Shaders.V1_30.SpriteBatch.vert"))
			{
				StreamReader reader = new StreamReader(stream);
				string src = reader.ReadToEnd();
				Shader.SetSource(ShaderType.VertexShader, src);
			}

			using (Stream stream = ResourceManager.GetResource("ArcEngine.Graphic.Shaders.V1_30.SpriteBatch.frag"))
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
				CurrentTexture = Sprites[0].Texture;
				for (int i = 0; i < spriteQueueCount; i++)
				{
					if (CurrentTexture != Sprites[i].Texture)
					{
						RenderBatch(CurrentTexture, Sprites, offset, i - offset);
						CurrentTexture = Sprites[i].Texture;
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
			Display.Texturing = true;

		}


		/// <summary>
		/// Renders sprites
		/// </summary>
		/// <param name="texture">Texture to use</param>
		/// <param name="vertices">Sprite array</param>
		/// <param name="offset">First element in the array</param>
		/// <param name="count">Number of element in the array</param>
		void RenderBatch(Texture texture, SpriteVertex[] vertices, int offset, int count)
		{
			if (count == 0 || texture == null)
				return;

			Display.TextureUnit = 0;
			Display.Texture = texture;
			Display.Shader = Shader;

			Matrix4 modelViewMatrix = Matrix4.Identity;
			Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Display.ViewPort.Width, Display.ViewPort.Height, 0.0f, -1.0f, 1.0f); ;
			Shader.SetUniform("modelview_matrix", modelViewMatrix * projectionMatrix);

			Matrix4 textureMatrix = Matrix4.Scale(1.0f / texture.Size.Width, 1.0f / texture.Size.Height, 1.0f);
			Shader.SetUniform("texture_matrix", textureMatrix);

			for (int i = offset; i < offset + count; i++)
			{
				Vector4 dst = new Vector4(
					Sprites[i].Destination.X - Sprites[i].Origin.X, Sprites[i].Destination.Y - Sprites[i].Origin.Y,
					Sprites[i].Destination.Width, Sprites[i].Destination.Height);

				Buffer.AddRectangle(dst, Sprites[i].Color, Sprites[i].Source);
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
		void InternalDraw(Texture texture, ref Rectangle destination, ref Rectangle source, Color color, float rotation, Point origin, SpriteEffects effect, float depth)
		{
			Vector4 src = new Vector4(source.X, source.Y, source.Width, source.Height);

			Vector4 dst = new Vector4(destination.X, destination.Y, destination.Width, destination.Height);

			Vector2 ori = new Vector2(origin.X, origin.Y);
			

			InternalDraw(texture, ref dst, ref src, color, rotation, ori, effect, depth);
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
				Sprites[spriteQueueCount].Source.X = 0.0f;
				Sprites[spriteQueueCount].Source.Y = 0.0f;
				Sprites[spriteQueueCount].Source.Z = texture.Size.Width;
				Sprites[spriteQueueCount].Source.W = texture.Size.Height;
			}
			else
			{
				Sprites[spriteQueueCount].Source.X = source.X;
				Sprites[spriteQueueCount].Source.Y = source.Y;
				Sprites[spriteQueueCount].Source.Z = source.Width;
				Sprites[spriteQueueCount].Source.W = source.Height;
			}

			// Add to queue
			Sprites[spriteQueueCount].Destination.X = destination.X;
			Sprites[spriteQueueCount].Destination.Y = destination.Y;
			Sprites[spriteQueueCount].Destination.Z = destination.Width;
			Sprites[spriteQueueCount].Destination.W = destination.Height;
			Sprites[spriteQueueCount].Color = color;
			Sprites[spriteQueueCount].Depth = depth;
			Sprites[spriteQueueCount].Effects = effect;
			Sprites[spriteQueueCount].Origin.X = origin.X;
			Sprites[spriteQueueCount].Origin.Y = origin.Y;
			Sprites[spriteQueueCount].Rotation = rotation;
			Sprites[spriteQueueCount].Texture = texture;

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
		public void Draw(Texture texture, Point position, Color color)
		{
			Rectangle destination = new Rectangle(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			Rectangle source = Rectangle.Empty;

			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, SpriteEffects.None, 0.0f);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, and color tint. 
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White  for full color with no tinting. </param>
		public void Draw(Texture texture, Vector2 position, Color color)
		{
			Vector4 dst = new Vector4(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			Vector4 src = Vector4.Zero;

			InternalDraw(texture, ref dst, ref src, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination rectangle, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="destination">A rectangle specifying, in screen coordinates, where the sprite will be drawn. 
		/// If this rectangle is not the same size as sourcerectangle, the sprite is scaled to fit.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting</param>
		public void Draw(Texture texture, Rectangle destination, Color color)
		{
			Rectangle source = Rectangle.Empty;

			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, SpriteEffects.None, 0.0f);
		}


		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination and source rectangles, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. 
		/// Use Rectangle.Empty to draw the entire texture.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void Draw(Texture texture, Point position, Rectangle source, Color color)
		{
			Rectangle destination = new Rectangle(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, SpriteEffects.None, 0.0f);
		}



		/// <summary>
		/// Adds a sprite to the batch of sprites to be rendered, specifying the texture, destination and source rectangles, and color tint
		/// </summary>
		/// <param name="texture">The sprite texture</param>
		/// <param name="position">The location, in screen coordinates, where the sprite will be drawn.</param>
		/// <param name="source">A rectangle specifying, in texels, which section of the rectangle to draw. 
		/// Use Rectangle.Empty to draw the entire texture.</param>
		/// <param name="color">The color channel modulation to use. Use Color.White for full color with no tinting.</param>
		public void Draw(Texture texture, Vector2 position, Vector4 source, Color color)
		{
			Vector4 dst = new Vector4(position.X, position.Y, texture.Size.Width, texture.Size.Height);

			Vector4 src = new Vector4(source.X, source.Y, source.Width, source.Height);

			InternalDraw(texture, ref dst, ref src, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
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
		public void Draw(Texture texture, Rectangle destination, Rectangle source, Color color)
		{
			InternalDraw(texture, ref destination, ref source, color, 0.0f, Point.Empty, SpriteEffects.None, 0.0f);
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
		public void Draw(Texture texture, Vector4 destination, Vector4 source, Color color)
		{
			InternalDraw(texture, ref destination, ref source, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
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
		/// <param name="scale">Uniform multiple by which to scale the sprite width and height</param>
		/// <param name="effects">Rotations to apply prior to rendering.</param>
		/// <param name="layerDepth">The sorting depth of the sprite, between 0 (front) and 1 (back).</param>
		public void Draw(Texture texture, Point position, Rectangle source, Color color, float rotation, Point origin, float scale, SpriteEffects effects, float layerDepth)
		{
			Rectangle src = new Rectangle();
			src.X = position.X;
			src.Y = position.Y;
			src.Width = texture.Size.Width;
			src.Height = texture.Size.Height;

			InternalDraw(texture, ref src, ref source, color, rotation, origin, effects, layerDepth);
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
		/// <param name="pos">Offset of the text</param>
		/// <param name="color">Color</param>
		/// <param name="text">Text to print</param>
		public void DrawString(BitmapFont font, Point pos, Color color, string text)
		{
			DrawString(font, new Vector4(pos.X, pos.Y, 0.0f, 0.0f), color, text);
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
		//	RectangleF zone = new RectangleF(rectangle.X, rectangle.Y, rectangle.Z, rectangle.W);

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
		/// 
		/// </summary>
		/// <param name="Font"></param>
		/// <param name="rectangle"></param>
		/// <param name="color"></param>
		/// <param name="Text"></param>
		public void DrawString(BitmapFont Font, Rectangle rectangle, Color color, string Text)
		{

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
			DrawTile(tileset, id, position, Color.White);
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
		/// <param name="depth"></param>
		/// <param name="effect"></param>
		/// <param name="rotation"></param>
		public void DrawTile(TileSet tileset, int id, Vector2 position, Color color, float rotation, SpriteEffects effect, float depth)
		{
			if (tileset == null || id < 0)
				return;

			Tile tile = tileset.GetTile(id);
			if (tile == null)
				return;

			Vector4 dst = new Vector4();
			dst.X = position.X;
			dst.Y = position.Y;
			dst.Z = tile.Rectangle.Width * tileset.Scale.Width;
			dst.W = tile.Rectangle.Height * tileset.Scale.Height;

			Vector4 src = new Vector4(tile.Rectangle.X, tile.Rectangle.Y, tile.Rectangle.Width, tile.Rectangle.Height);

			Draw(tileset.Texture, dst, src, color);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="tileset"></param>
		/// <param name="id"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		public void DrawTile(TileSet tileset, int id, Vector2 position, Color color)
		{
			DrawTile(tileset, id, position, color, 0.0f, SpriteEffects.None, 0.0f);
		}


		#endregion


		#region Comparers

		/// <summary>
		/// 
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
		/// 
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
		/// 
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
				if (left.Texture.Handle > right.Texture.Handle)
					return -1;

				if (left.Texture.Handle < right.Texture.Handle)
					return 1;

				return 0;
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

		/// <summary>
		/// Texture
		/// </summary>
		public Texture Texture;
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
