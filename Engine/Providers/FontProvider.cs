using ArcEngine.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Providers
{
	/// <summary>
	/// 
	/// </summary>
	public class FontProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public FontProvider()
		{
			Fonts = new Dictionary<string, XmlNode>();
			SharedFonts = new Dictionary<string, TextureFont>();

			Name = "Font";
			Tags = new string[] {"texturefont" };
			Assets = new Type[] { typeof(TextureFont) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetInternalResource("ArcEngine.Data.Icons.Font.png"));

		}



		#region IO routines


		/// <summary>
		/// Saves all textures
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{
			if (type == typeof(TextureFont))
			{
				foreach (XmlNode node in Fonts.Values)
					node.WriteTo(xml);
			}

			return true;
		}



		/// <summary>
		/// Loads a texture
		/// </summary>
		public override bool Load(XmlNode xml)
		{

			if (xml == null)
				return false;

			switch (xml.Name.ToLower())
			{
				case "texturefont":
				{

					string name = xml.Attributes["name"].Value;
					Fonts[name] = xml;

				}
				break;

				default:
				{

				}
				break;
			}


			return true;
		}



		#endregion


		#region Editor


		/// <summary>
		/// Edits an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override AssetEditor EditAsset<T>(string name)
		{
			AssetEditor form = null;

			if (typeof(T) == typeof(TextureFont))
			{
				XmlNode node = null;
				if (Fonts.ContainsKey(name))
					node = Fonts[name];
				form = new ArcEngine.Editor.TextureFontForm(node);
				form.TabText = name;
			}

			return form;
		}


		#endregion


		#region Assets

		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public override void Add<T>(string name, XmlNode node)
		{
			CheckValue<T>(name);

			//if (typeof(T) == typeof(TTFFont))
			//   TTFFonts[name] = node;

			//if (typeof(T) == typeof(TextureFont))
			//   TextureFonts[name] = node;

		}


		/// <summary>
		/// Returns an array of all available fonts
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>List of assets</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			
			foreach (string key in Fonts.Keys)
			{
				list.Add(key);
			}


			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a font asset
		/// </summary>
		/// <typeparam name="T">Type of asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);


			if (!Fonts.ContainsKey(name))
				return default(T);

			TextureFont font = new TextureFont();
			font.Load(Fonts[name]);

			return (T)(object)font;
		}



		/// <summary>
		/// Returns a <c>Font</c> definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (!Fonts.ContainsKey(name))
				return null;

			return Fonts[name];
		}




		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
		}




		/// <summary>
		/// Removes a font
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Fonts.Clear();
		}


		#endregion


		#region Shared assets


		/// <summary>
		/// Creates a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>The resource</returns>
		public override T CreateShared<T>(string name)
		{
			if (typeof(T) == typeof(TextureFont))
			{
				if (SharedFonts.ContainsKey(name))
					return (T)(object)SharedFonts[name];

				TextureFont font = new TextureFont();
				font.Load(Fonts[name]);
				SharedFonts[name] = font;

				return (T)(object)font;
			}

			return default(T);
		}



		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public override void RemoveShared<T>(string name)
		{
			if (typeof(T) == typeof(TextureFont))
			{
				SharedFonts[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(TextureFont))
			{
				SharedFonts.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedFonts.Clear();
		}



		#endregion



		#region Progerties

		/// <summary>
		/// TTFFonts definition
		/// </summary>
		Dictionary<string, XmlNode> Fonts;


		/// <summary>
		/// Shared fonts
		/// </summary>
		Dictionary<string, TextureFont> SharedFonts;

		#endregion
	}

}

