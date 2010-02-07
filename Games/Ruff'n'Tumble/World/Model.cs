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
using ArcEngine;
using System.Drawing;
using System.Xml;
using ArcEngine.Asset;
using System.ComponentModel;

namespace RuffnTumble
{
	/// <summary>
	/// 
	/// </summary>
	public class Model : IAsset
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public Model()//string name) : base(name)
		{
		}


		public bool Init()
		{
			return true;
		}

		#region IO routines

		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;

			xml.WriteStartElement("model");
			//xml.WriteAttributeString("name", Name);


			//base.SaveComment(xml);

			if (TileSetName != null && TileSetName.Length > 0)
			{
				xml.WriteStartElement("tileset");
				xml.WriteAttributeString("name", TileSetName);
				xml.WriteEndElement();
			}

			if (ScriptName != null && ScriptName.Length > 0)
			{
				xml.WriteStartElement("script");
				xml.WriteAttributeString("name", ScriptName);
				xml.WriteEndElement();
			}

			xml.WriteStartElement("zoom");
			xml.WriteAttributeString("width", Zoom.Width.ToString());
			xml.WriteAttributeString("height", Zoom.Height.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("gravity");
			xml.WriteAttributeString("x", Gravity.X.ToString());
			xml.WriteAttributeString("y", Gravity.Y.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("maxvelocity");
			xml.WriteAttributeString("value", MaxVelocity.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("acceleration");
			xml.WriteAttributeString("value", Acceleration.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("deceleration");
			xml.WriteAttributeString("value", Deceleration.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("jump");
			xml.WriteAttributeString("value", JumpHeight.ToString());
			xml.WriteEndElement();

			xml.WriteStartElement("life");
			xml.WriteAttributeString("value", Life.ToString());
			xml.WriteEndElement();
			
		//	xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
				//	base.LoadComment(node);
					continue;
				}

				switch (node.Name.ToLower())
				{
					case "tileset":
					{
						TileSetName = node.Attributes["name"].Value;
					}
					break;

					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
					}
					break;

					case "zoom":
					{
						zoom.Width = float.Parse(node.Attributes["width"].Value);
						zoom.Height= float.Parse(node.Attributes["height"].Value);
					}
					break;


					case "gravity":
					{
						Gravity = new Point(
							int.Parse(node.Attributes["x"].Value),
							int.Parse(node.Attributes["y"].Value)
							);
					}
					break;


					case "jump":
					{
						JumpHeight = int.Parse(node.Attributes["value"].Value);
					}
					break;


					case "maxvelocity":
					{
						MaxVelocity = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "acceleration":
					{
						acceleration = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "deceleration":
					{
						deceleration = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "life":
					{
						Life = int.Parse(node.Attributes["value"].Value);
					}
					break;
					
					
					default:
					{
						Trace.WriteLine("Model : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}

		#endregion




		#region Properties


		/// <summary>
		/// Name of the dungeon
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
				return "model";
			}
		}


		/// <summary>
		/// Nom du script a utiliser
		/// </summary>
		[CategoryAttribute("Script")]
		[Description("Script's name to handle the model events")]
		[TypeConverter(typeof(ScriptEnumerator))]
		public string ScriptName
		{
			get
			{
				return scriptName;
			}
			set
			{
				scriptName = value;
			}
		}
		string scriptName;


		/// <summary>
		/// Nom du sprite a utiliser
		/// </summary>
		[CategoryAttribute("Sprite")]
		[Description("TileSet's name of the model")]
		[TypeConverter(typeof(TileSetEnumerator))]
		public string TileSetName
		{
			get
			{
				return tileSetName;
			}
			set
			{
				tileSetName = value;
			}
		}
		string tileSetName;




		/// <summary>
		/// Gets/sets the zoom of the tileset
		/// </summary>
		public SizeF Zoom
		{
			get
			{
				if (zoom.IsEmpty)
					return new SizeF(1, 1);
				else
					return zoom;
			}
			set
			{
				zoom = value;
			}
		}
		SizeF zoom;




		/// <summary>
		/// Maximum maxVelocity of the model
		/// (pixels per second)
		/// </summary>
		[CategoryAttribute("Movement")]
		[Description("Maximum maxVelocity of the entity")]
		public int MaxVelocity
		{
			get
			{
				return maxVelocity;
			}
			set
			{
				maxVelocity = value;
			}
		}
		int maxVelocity;



		/// <summary>
		/// Gets / sets the acceleration of the entity
		/// (pixels per second)
		/// </summary>
		[CategoryAttribute("Movement")]
		[Description("Acceleration of the eneity")]
		public int Acceleration
		{
			get
			{
				return acceleration;
			}
			set
			{
				acceleration = value;
			}
		}
		int acceleration;



		/// <summary>
		/// 
		/// </summary>
		[CategoryAttribute("Movement")]
		[Description("Deceleration of the entity")]
		public int Deceleration
		{
			get
			{
				return deceleration;
			}
			set
			{
				deceleration = value;
			}
		}
		int deceleration;
 
         

		/// <summary>
		/// Gravity of the model
		/// (pixels per second)
		/// </summary>
		[CategoryAttribute("Movement")]
		[Description("Gravity of the entity")]
		public Point Gravity
		{
			get
			{
				return gravity;
			}
			set
			{
				gravity = value;
			}
		}
		Point gravity;


		/// <summary>
		/// Height of the jump, 0 to disable jump
		/// </summary>
		[CategoryAttribute("Movement")]
		[Description("Initial value of a jump")]
		public int JumpHeight
		{
			get
			{
				return jumpHeight;
			}
			set
			{
				jumpHeight = value;
			}
		}
		int jumpHeight;


		/// <summary>
		/// Life point
		/// </summary>
		[CategoryAttribute("Life")]
		[Description("Initial life point")]
		public int Life
		{
			get
			{
				return life;
			}
			set
			{
				life = value;
			}
		}
		int life;
 
		
		#endregion
	}



	/// <summary>
	/// Model Enumerator for PropertyGrids
	/// </summary>
	internal class ModelEnumerator : StringConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de tous les models pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(null);
			//return new StandardValuesCollection(ResourceManager.GetAssets<Model>);
		}

	}


}
