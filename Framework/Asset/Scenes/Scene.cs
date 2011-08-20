#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Text;
using System.Xml;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine.Interface;


// http://dmweb.free.fr/?q=node/216

namespace ArcEngine.Asset
{

	/// <summary>
	/// Key framed animation
	/// </summary>
	public class Scene : IAsset, IDisposable
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Scene()
		{
			Layers = new List<SceneLayer>();
			IsDisposed = false;
		}


		~Scene()
		{
			if (!IsDisposed)
				System.Windows.Forms.MessageBox.Show("[Scene] : Call Dispose() !!");
		}

		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}


		/// <summary>
		/// Dispose asset
		/// </summary>
		public void Dispose()
		{
			if (TileSet != null)
				TileSet.Dispose();
			TileSet = null;

			if (Font != null)
				Font.Dispose();
			Font = null;

			IsDisposed = true;
			GC.SuppressFinalize(this);
		}


		#region IO routines

		///
		///<summary>
		/// Save the animation to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;


			xml.WriteStartElement(XmlTag);
			xml.WriteAttributeString("name", Name);


			//		base.SaveComment(xml);

	//		xml.WriteStartElement("loop");
	//		xml.WriteAttributeString("value", type.ToString());
	//		xml.WriteEndElement();


			xml.WriteEndElement();

			return true;
		}

		/// <summary>
		/// Loads the animation from a xml file
		/// </summary>
		/// <param name="xml">XmlNode to load</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			if (xml.Name != XmlTag)
			{
				Trace.WriteLine("Expecting \"" + XmlTag + "\" in node header, found \"" + xml.Name + "\" when loading Scene.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						TileSetName = node.Attributes["name"].Value;
						TileSet = ResourceManager.CreateAsset<TileSet>(TileSetName);
						if (TileSet == null)
							break;

                        Vector2 size = new Vector2(1.0f, 1.0f);
						if (node.Attributes["scalew"].Value != null)
							size.X = float.Parse(node.Attributes["scalew"].Value);
						if (node.Attributes["scaleh"].Value != null)
							size.Y= float.Parse(node.Attributes["scaleh"].Value);

						TileSet.Scale = size;
					}
					break;


					case "layer":
					{
						SceneLayer layer = CreateLayer();
						layer.Load(node);
					}
					break;


					case "stringtable":
					{
						StringTableName = node.Attributes["name"].Value;
						StringTable = ResourceManager.CreateAsset<StringTable>(StringTableName);
					}
					break;

					case "font":
					{
						FontName = node.Attributes["name"].Value;
						Font = ResourceManager.CreateAsset<BitmapFont>(FontName);
					}
					break;

					default:
					{
						Trace.WriteLine("Animation : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}


		#endregion



		/// <summary>
		/// Update animation
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
			if (!Pause)
			{
				Time += time.ElapsedGameTime;
			}

		}


		/// <summary>
		/// Draws the animation
		/// </summary>
		public void Draw(SpriteBatch batch)
		{
			Display.ClearBuffers();

			if (TileSet == null)
				return;


			Display.RenderState.Scissor = true;
			foreach (SceneLayer layer in Layers)
			{
				SceneFrame frame = new SceneFrame(layer, Time);
				frame.Draw();
			}

			Display.RenderState.Scissor = false;
		}


		#region Motion controls

		/// <summary>
		/// Play the animation
		/// </summary>
		public void Play()
		{
		}



		/// <summary>
		/// Stop the animation
		/// </summary>
		public void Stop()
		{
		}


		#endregion


		#region Layer management

		/// <summary>
		/// Add an AnimationLayer
		/// </summary>
		/// <returns>Created layer</returns>
		public SceneLayer CreateLayer()
		{

			SceneLayer layer = new SceneLayer(this);
			Layers.Add(layer);
			Layers.Sort();

			return layer;
		}


		/// <summary>
		/// Removes an AnimationLayer
		/// </summary>
		/// <param name="id">Id of the layer</param>
		public void RemoveLayer(int id)
		{
			Layers.RemoveAt(id);
		}


		/// <summary>
		/// Removes an AnimationLayer
		/// </summary>
		/// <param name="layer">Layer</param>
		public void RemoveLayer(SceneLayer layer)
		{
			Layers.Remove(layer);
		}


		/// <summary>
		/// Sorts layers
		/// </summary>
		public void SortLayers()
		{
			Layers.Sort();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Name of the animation
		/// </summary>
		public string Name { get; set; }


		/// <summary>
		/// Is asset disposed
		/// </summary>
		public bool IsDisposed { get; private set; }


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "scene";
			}
		}


		/// <summary>
		/// Name of the TileSet to use
		/// </summary>
		public string TileSetName
		{
			get;
			set;
		}

		/// <summary>
		/// Tileset
		/// </summary>
		public TileSet TileSet
		{
			get;
			private set;
		}


		/// <summary>
		/// Name of the Font to use
		/// </summary>
		public string FontName
		{
			get;
			set;
		}

		/// <summary>
		/// Font
		/// </summary>
		public BitmapFont Font
		{
			get;
			private set;
		}




		/// <summary>
		/// Available layers
		/// </summary>
		List<SceneLayer> Layers;

		/// <summary>
		/// Pause the animation
		/// </summary>
		public bool Pause { get; set; }


		/// <summary>
		/// Current time position in the animation
		/// </summary>
		public TimeSpan Time { get; set; }


		/// <summary>
		/// Name of the StringTable to use
		/// </summary>
		public string StringTableName  { get; set; }


		/// <summary>
		/// StringTable
		/// </summary>
		public StringTable StringTable
		{
			get;
			private set;
		}

		#endregion

	}
}
