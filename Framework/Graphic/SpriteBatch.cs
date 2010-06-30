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
				SpriteVertex[] queue;
				if (SortMode == SpriteSortMode.Deferred)
					queue = Sprites;
				else
				{
					Sort();
					//queue = sortedSprites;
				}

				int offset = 0;
				Texture texture = CurrentTexture;
				for (int i = 0; i < spriteQueueCount; i++)
				{
					if (Sprites[i].Texture != texture)
					{
						RenderBatch(texture, Sprites, offset, i - offset);
						texture = Sprites[i].Texture;
						offset = i;
					}
				}
				RenderBatch(texture, Sprites, offset, this.spriteQueueCount - offset);

			}

			spriteQueueCount = 0;
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
			if (count == 0 ||texture == null)
				return;

			Display.TextureUnit = 0;
			Display.Texture = texture;
			Display.Shader = Shader;

			Matrix4 modelViewMatrix = Matrix4.Identity;
			Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, Display.ViewPort.Width, Display.ViewPort.Height, 0.0f, -1.0f, 1.0f); ;
			Shader.SetUniform("modelview_matrix", modelViewMatrix * projectionMatrix);

			Matrix4 textureMatrix = Matrix4.Scale(1.0f / texture.Size.Width, 1.0f / texture.Size.Height, 1.0f);
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

			// If immediate mode AND texture is not the same
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
			sprite.Texture = texture;

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



		#region Text drawing

		/// <summary>
		/// Prints some text on the screen
		/// </summary>
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
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="color">Text color</param>
		/// <param name="text">Text to print</param>
		public void DrawString(BitmapFont font, Vector4 rectangle, TextJustification justification, Color color, string text)
		{
			if (string.IsNullOrEmpty(text))
				return;

/*
			// Encode string to xml
			string msg = "<?xml version=\"1.0\" encoding=\"unicode\" standalone=\"yes\"?><root>" + text + "</root>";
			UnicodeEncoding utf8 = new UnicodeEncoding();
			byte[] buffer = utf8.GetBytes(msg);
			MemoryStream stream = new MemoryStream(buffer);
			XmlTextReader reader = new XmlTextReader(stream);
			reader.WhitespaceHandling = WhitespaceHandling.All;


			// Color stack
			Stack<Color> ColorStack = new Stack<Color>();
			ColorStack.Push(color);
			Color currentcolor = color;


			Rectangle rect = rectangle;
			Display.Buffer.Clear();


			// Extra offset when displaying tile
			int tileoffset = 0;

			try
			{
				// Skip the first tags "<?...?>" and "<root>"
				reader.MoveToContent();

				while (reader.Read())
				{


					switch (reader.NodeType)
					{
						case XmlNodeType.Attribute:
						{
						}
						break;


						#region control tags

						// Special tags
						case XmlNodeType.Element:
						{
							switch (reader.Name.ToLower())
							{
								case "tile":
								{
									if (TextTileset == null)
										break;

									int id = int.Parse(reader.GetAttribute("id"));
									Tile tile = TextTileset.GetTile(id);
									TextTileset.Draw(id, rect.Location);
									rect.Offset(tile.Size.Width, 0);

									tileoffset = tile.Size.Height - LineHeight;
								}
								break;

								case "br":
								{
									rect.X = rectangle.X;
									rect.Y += (int)(LineHeight * GlyphTileset.Scale.Height) + tileoffset;
									tileoffset = 0;
								}
								break;


								// Change the color
								case "color":
								{
									ColorStack.Push(currentcolor);

									currentcolor = Color.FromArgb(int.Parse(reader.GetAttribute("a")),
										int.Parse(reader.GetAttribute("r")),
										int.Parse(reader.GetAttribute("g")),
										int.Parse(reader.GetAttribute("b")));
								}
								break;
							}
						}
						break;

						#endregion


						#region closing control tags

						case XmlNodeType.EndElement:
						{
							switch (reader.Name.ToLower())
							{
								case "color":
								{
									currentcolor = ColorStack.Pop();
								}
								break;
							}
						}

						break;

						#endregion


						#region Raw text
						case XmlNodeType.Text:
						{

							foreach (char c in reader.Value)
							{
								// Get the tile
								Tile tile = GlyphTileset.GetTile(c - GlyphOffset);
								if (tile == null)
									continue;

								// Move the glyph according to its hot spot
								Rectangle tmp = new Rectangle(
									new Point(rect.X - (int)(tile.HotSpot.X * GlyphTileset.Scale.Width), rect.Y - (int)(tile.HotSpot.Y * GlyphTileset.Scale.Height)),
									new Size((int)(tile.Rectangle.Width * GlyphTileset.Scale.Width), (int)(tile.Rectangle.Height * GlyphTileset.Scale.Height)));

								// Out of the bouding box => new line
								if (tmp.Right >= rectangle.Right && !rectangle.Size.IsEmpty)
								{
									tmp.X = rectangle.X;
									tmp.Y = tmp.Y + (int)(LineHeight * GlyphTileset.Scale.Height);

									rect.X = rectangle.X;
									rect.Y += (int)(LineHeight * GlyphTileset.Scale.Height) + tileoffset;
									tileoffset = 0;

								}

								// Add glyph to the batch
								Display.Buffer.AddRectangle(tmp, currentcolor, tile.Rectangle);

								// Move to the next glyph
								rect.Offset(tmp.Size.Width + Advance, 0);
							}
						}
						break;
						#endregion

					}

				}
			}
			catch (XmlException ex)
			{
			}

			finally
			{
				// Close streams
				reader.Close();
				stream.Close();

				// Draw batch
				int count = Display.Buffer.Update();
				Display.TextureUnit = 0;
				Display.Texture = GlyphTileset.Texture;


				//		Display.PushOrtho();

				//		Display.Shader.SetUniform("texture", 0);
				Display.DrawBatch(Display.Buffer, 0, count);

				//		Display.PopMatrices();
			}
*/
		}



		/// <summary>
		/// Prints some text on the screen within a rectangle with justification
		/// </summary>
		/// <param name="rectangle">Rectangle of the text</param>
		/// <param name="justification">Needed justifcation</param>
		/// <param name="color">Color</param>
		/// <param name="format">Text to print</param>
		/// <param name="args"></param>
		public void DrawText(Rectangle rectangle, TextJustification justification, Color color, string format, params object[] args)
		{
			DrawText(rectangle, justification, color, string.Format(format, args));
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
		int spriteQueueCount;


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
