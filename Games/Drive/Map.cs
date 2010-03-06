using System.Drawing;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using ArcEngine.Graphic;
using ArcEngine.Asset;


namespace Drive
{
	/// <summary>
	/// http://stackoverflow.com/questions/1110844/multiple-texture-images-blended-together-onto-3d-ground
	/// http://www.opengl.org/wiki_132/index.php?title=Texture_Combiners&oldid=2119#Example_:_Blend_tex1_and_tex2_based_on_alpha_of_tex0
	/// </summary>
	public class Map
	{
		/// <summary>
		/// 
		/// </summary>
		public Map()
		{
			Size = new Size(32, 32);
			Scale = 1.0f;

			// Generate layers
			int layercount = 2;
			Layers = new List<byte[,]>();
			for (int id = 0; id < layercount; id++)
				Layers.Add(new byte[Size.Width, Size.Height]);
			
			
			
			// Load textures
			Textures = new Texture[3];
			Textures[0] = new Texture("data/grass_01.png");
			Textures[1] = new Texture("data/dirt_01.png");
			Textures[2] = new Texture("data/road_01.png");

			// Generate the batch
			Batch = new Batch(1);
			for (int y = 0; y < Size.Height; y++)
				for (int x = 0; x < Size.Width; x++)
				{
					Batch.AddRectangle(new Rectangle(x * 32, y * 32, 32, 32), Color.White, new Rectangle((x * 32) % 128, (y * 32) % 128, 32, 32));
				}
			Batch.Apply();

			Shader = new Shader();
			Shader.Compile();

		}



		/// <summary>
		/// 
		/// </summary>
		public void Draw()
		{
			Display.Shader = Shader;
			Display.DrawBatch(Batch, OpenTK.Graphics.OpenGL.BeginMode.Quads);
			Display.Shader = null;
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
		List<byte[,]> Layers;


		/// <summary>
		/// 
		/// </summary>
		Texture[] Textures;


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
		Batch Batch;

		/// <summary>
		/// 
		/// </summary>
		Shader Shader;


		#endregion

	}
}
