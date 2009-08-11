using ArcEngine.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Graphic;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Providers
{

	/// <summary>
	/// Tileset provider
	/// </summary>
	public class TileSetProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public TileSetProvider()
		{
			TileSets = new Dictionary<string, XmlNode>();
			SharedTileSets = new Dictionary<string, TileSet>();

			Name = "TileSet";
			Tags = new string[] { "tileset" };
			Assets = new Type[] { typeof(TileSet) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetInternalResource("ArcEngine.Data.Icons.TileSet.png"));

		}



		#region IO routines


		/// <summary>
		/// Saves all scripts
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{

			if (type == typeof(TileSet))
			{
				foreach (XmlNode node in TileSets.Values)
					node.WriteTo(xml);
			}

/*	
			foreach (KeyValuePair<string, XmlNode> kvp in TileSets)
			{
				xml.WriteStartElement("tileset");
				xml.WriteAttributeString("name", kvp.Key);

				kvp.Value.WriteContentTo(xml);

				xml.WriteEndElement();

			}
*/

			return true;
		}




		/// <summary>
		/// Loads a TileSet
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			string name = xml.Name.ToLower();

			switch (name)
			{
				case "tileset":
				{

					TileSets[xml.Attributes["name"].Value] = xml;
					//string name = xml.Attributes["name"].Value;
					//TileSet tileset = Create<TileSet>(name);
					//if (tileset != null)
					//   tileset.Load(xml);
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

			if (typeof(T) == typeof(TileSet))
			{
				XmlNode node = null;
				if (TileSets.ContainsKey(name))
					node = TileSets[name];

				form = new TileSetForm(node);
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
			TileSets[name] = node;
		}


		/// <summary>
		/// Returns an array of all available Tilesets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Script's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(TileSet))
			{
				foreach (string key in TileSets.Keys)
				{
					list.Add(key);
				}
			}

			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a new TileSet
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (!TileSets.ContainsKey(name))
				return default(T);

			TileSet tileset = new TileSet();
			tileset.Load(TileSets[name]);

			return (T)(object)tileset;
		}



		/// <summary>
		/// Returns a <c>Script</c>
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (!TileSets.ContainsKey(name))
				return null;

			//TileSet ts = new TileSet();
			//ts.Load(TileSets[name]);

			return TileSets[name];
		}



		/// <summary>
		/// Removes a script
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
		}




		/// <summary>
		/// Removes a script
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
			TileSets.Clear();
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
			if (typeof(T) == typeof(TileSet))
			{
				if (SharedTileSets.ContainsKey(name))
					return (T)(object)SharedTileSets[name];

				TileSet ts = new TileSet();
				ts.Load(TileSets[name]);		
				SharedTileSets[name] = ts;

				return (T)(object)ts;
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
			if (typeof(T) == typeof(TileSet))
			{
				SharedTileSets[name] = null;
			}
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(StringTable))
			{
				SharedTileSets.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedTileSets.Clear();
		}



		#endregion



		#region Progerties


		/// <summary>
		/// Scripts
		/// </summary>
		Dictionary<string, XmlNode> TileSets;

		/// <summary>
		/// Shared tilesets
		/// </summary>
		Dictionary<string, TileSet> SharedTileSets;

		#endregion
	}
}
