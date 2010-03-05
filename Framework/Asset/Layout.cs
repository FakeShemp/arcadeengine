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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Utility.GUI;
using ArcEngine.Asset;



namespace ArcEngine.Asset
{


	/// <summary>
	/// A layout includes the title screen and dialog boxes, among other things,
	/// </summary>
	public class Layout : IAsset
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Layout()
		{
			elements = new Dictionary<string,Control>();
		}




		/// <summary>
		/// Initializes the asset
		/// </summary>
		/// <returns>True on success</returns>
		public bool Init()
		{
			return true;
		}

	

		#region Gui Elements


		/// <summary>
		/// Returns an element by its name
		/// </summary>
		/// <param name="name">Name of the Gui Element</param>
		/// <returns>The GuiBase Element or null if not found</returns>
		public Control GetElement(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			if (elements.ContainsKey(name))
				return elements[name];

			return null;
		}


		/// <summary>
		/// Clear all gui elements
		/// </summary>
		public void Clear()
		{
			elements.Clear();
		}


		/// <summary>
		/// Adds a button to the layout
		/// </summary>
		/// <param name="name">Name of the button</param>
		/// <returns>Created Button or NULL</returns>
		public Button CreateButton(string name)
		{
			// Element's name already in use
			if (string.IsNullOrEmpty(name) || GetElement(name) != null)
				return null;


			Button button = new Button();
			elements[name] = button;


			return button;
		}



		/// <summary>
		/// Deletes an element by its name
		/// </summary>
		/// <param name="name">Name of the element to delete</param>
		public void DeleteElement(string name)
		{
			if (string.IsNullOrEmpty(name))
				return;

			elements.Remove(name);
		}



		/// <summary>
		/// Deletes an element
		/// </summary>
		/// <param name="gui">Gui Element to delete</param>
		public void DeleteElement(Control gui)
		{
			if (gui == null)
				return;


			//DeleteElement(gui.Name);
		}




		#endregion



		#region IO routines

		///
		///<summary>
		/// Save the layout to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			//xml.WriteStartElement("layout");
			//xml.WriteAttributeString("name", Name);


		//	base.SaveComment(xml);


			xml.WriteStartElement("color");
			xml.WriteAttributeString("r", Color.R.ToString());
			xml.WriteAttributeString("g", Color.G.ToString());
			xml.WriteAttributeString("b", Color.B.ToString());
			xml.WriteAttributeString("a", Color.A.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("texture");
			xml.WriteAttributeString("name", TextureName);
			xml.WriteEndElement();

			//xml.WriteStartElement("tileset");
			//xml.WriteAttributeString("name", TileSetName);
			//xml.WriteEndElement();


			foreach (Control element in elements.Values)
				element.Save(xml);


		//	xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads the layout from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
					//base.LoadComment(node);
					continue;
				}

				switch (node.Name.ToLower())
				{

					// Adds a button
					case "button":
					{
						Button button = CreateButton(node.Attributes["name"].Value);
						if (button != null)
							button.Load(node);
					}
					break;


					// Background color
					case "color":
					{
					}
					break;


					// Background texture
					case "texture":
					{
						TextureName = node.Attributes["name"].Value.ToString();
					}
					break;

				}
			}

			return true;
		}

		#endregion



		/// <summary>
		/// Draw the layout
		/// </summary>
		public void Draw()
		{
			if (Color.A > 0)
			{
				Display.ClearColor = Color;
				Display.ClearBuffers();
			}


			if (Texture != null)
			{
				//device.Texture = Texture;
				//Texture.Blit(Texture.Rectangle, TextureLayout);
				Texture.Blit(Texture.Rectangle, TextureLayout);
			}

			foreach (Control gui in elements.Values)
			{
				gui.Draw();
			}

		}


		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "layout";
			}
		}


		/// <summary>
		/// Background Color
		/// </summary>
		public Color Color
		{
			get;
			set;
		}




		/// <summary>
		/// Background texture name
		/// </summary>
	//	[TypeConverter(typeof(TextureEnumerator))]
		public string TextureName
		{
			get
			{
				return textureName;
			}
			set
			{
				textureName = value;
				if (string.IsNullOrEmpty(value))
					Texture = null;
				else
					//texture = ResourceManager.CreateAsset<Texture>(value);
					Texture = new Texture(value);
			}
		}
		string textureName;




		/// <summary>
		/// Background texture
		/// </summary>
		[Browsable(false)]
		public Texture Texture
		{
			get;
			private set;
		}



		/// <summary>
		/// Texture layout mode
		/// </summary>
		public TextureLayout TextureLayout
		{
			get;
			private set;
		}


		/// <summary>
		/// All GUI Elements
		/// </summary>
		[Browsable(false)]
		public List<Control> Elements
		{
			get
			{
				List<Control> list = new List<Control>();

				foreach (Control element in elements.Values)
					list.Add(element);
				return list;
			}
		}
		Dictionary<string, Control> elements;

		#endregion

	}
}
