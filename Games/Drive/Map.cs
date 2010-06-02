using System.Drawing;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using OpenTK.Graphics.OpenGL;

namespace Drive
{
	/// <summary>
	/// http://stackoverflow.com/questions/1110844/multiple-texture-images-blended-together-onto-3d-ground
	/// http://www.opengl.org/wiki_132/index.php?title=Texture_Combiners&oldid=2119#Example_:_Blend_tex1_and_tex2_based_on_alpha_of_tex0
	/// </summary>
	public class Map : IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		public Map()
		{
			Size = new Size(256, 256);
			Scale = 4.0f;
			Location = Point.Empty;

			// Load alphamaps
			AlphaMaps = new Texture[3];
			AlphaMaps[0] = new Texture("data/blendmap1.png");
			AlphaMaps[1] = new Texture("data/blendmap2.png");
			AlphaMaps[2] = new Texture("data/blendmap3.png");
	
			
			// Load textures
			Textures = new Texture[3];
			Textures[0] = new Texture("data/grass.png");
			Textures[0].MagFilter = TextureMagFilter.Linear;
			Textures[0].MinFilter = TextureMinFilter.Linear;
			Textures[1] = new Texture("data/graydirt.png");
			Textures[2] = new Texture("data/rock.png");

			// Generate the batch
			Size gridsize = new Size(16,16);
			Batch = new BatchBuffer();
			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					Batch.AddRectangle(new Rectangle(x * gridsize.Width, y * gridsize.Height, gridsize.Width, gridsize.Height), Color.White,
						//new Rectangle((x * gridsize.Width) % 1024, (y * gridsize.Height) % 1024, gridsize.Width, gridsize.Height));
						//new Rectangle(x * gridsize.Width, y * gridsize.Height, gridsize.Width, gridsize.Height));
						//new Rectangle(0, 0, gridsize.Width, gridsize.Height));
						new Rectangle(x, y, 1, 1));
				}
			int count = Batch.Update();



			Shader = new Shader();
			Shader.SetSource(ShaderType.VertexShader,
				@"
				varying vec4 texCoord0;

				void main()
				{
				   texCoord0 = gl_TextureMatrix[0] * gl_MultiTexCoord0;
				 
				   gl_Position = ftransform();
				}
				");

			Shader.SetSource(ShaderType.FragmentShader,
				@"
				uniform sampler2D Alpha;
				uniform sampler2D Grass;
				uniform sampler2D Stone;
				uniform sampler2D Rock;
				uniform float scale;

				varying vec4 texCoord0;

				void main(void)
				{
				   //float scale = 5.0f;
				   vec4 alpha   = texture2D( Alpha, texCoord0.xy );
				   vec4 tex0    = texture2D( Grass, texCoord0.xy * scale ); // Tile
				   vec4 tex1    = texture2D( Rock,  texCoord0.xy * scale ); // Tile
				   vec4 tex2    = texture2D( Stone, texCoord0.xy * scale ); // Tile

				   tex0 *= alpha.r;                            // Red channel
				   tex1 = mix(tex0, tex1, alpha.g);            // Green channel
				   vec4 outColor = mix(tex1, tex2, alpha.b);   // Blue channel
				   
				   gl_FragColor = outColor;
				}
				");
			Shader.Compile();


		}


		public void Dispose()
		{
			Shader.Dispose();
		}



		/// <summary>
		/// 
		/// </summary>
		public void Draw()
		{
			
			for (int id = 0; id < 3; id++)
			{
				Display.TextureUnit = id;
				Display.Texture = Textures[id];
			}
			Display.TextureUnit = 3;
			Display.Texture = AlphaMaps[0];

			Display.Shader = Shader;
			Shader.SetUniform(Shader.GetUniform("Grass"), 0); 
			Shader.SetUniform(Shader.GetUniform("Stone"), 1); 
			Shader.SetUniform(Shader.GetUniform("Rock"), 2);
			Shader.SetUniform(Shader.GetUniform("Alpha"), 3);
			Shader.SetUniform(Shader.GetUniform("scale"), Scale);


			Display.Translate(-Location.X, -Location.Y);
			// Dummy value
			Display.DrawBatch(Batch, 0, 400000);
			Display.Translate(Location.X, Location.Y);

			Display.Shader = null;

			//for (int id = 3; id == 1; id--)
			//{
			//    Display.TextureUnit = id;
			//    Display.Texturing = false;
			//}

			Display.TextureUnit = 0;
			//Display.Texturing = true;

		}


		#region IO

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			return false;


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Load(XmlNode node)
		{


			return false;
		}

		#endregion



		#region Properties



		/// <summary>
		/// 
		/// </summary>
		Texture[] Textures;


		/// <summary>
		/// 
		/// </summary>
		Texture[] AlphaMaps;

		/// <summary>
		/// Size of the level in block
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}


		/// <summary>
		/// Scale size
		/// </summary>
		public float Scale
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		BatchBuffer Batch;

		/// <summary>
		/// 
		/// </summary>
		Shader Shader;


		/// <summary>
		/// 
		/// </summary>
		public Point Location;

		#endregion

	}
}
