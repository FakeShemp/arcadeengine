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


namespace ArcEngine.GUI
{

	/// <summary>
	/// Base class for each GUI element
	/// </summary>
	public class GuiBase
	{

		
		#region Methods

		/// <summary>
		/// Updates the gadget
		/// </summary>
		/// <param name="time">Elapsed time</param>
		public virtual void Update(GameTime time)
		{
		}


		/// <summary>
		/// Draws the button
		/// </summary>
		public virtual void Draw()
		{
		}



		#endregion


		#region IO routines
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;


			xml.WriteStartElement("rectangle");
			xml.WriteAttributeString("x", Rectangle.X.ToString());
			xml.WriteAttributeString("y", Rectangle.Y.ToString());
			xml.WriteAttributeString("width", Rectangle.Width.ToString());
			xml.WriteAttributeString("height", Rectangle.Height.ToString());
			xml.WriteEndElement();


			xml.WriteStartElement("color");
			xml.WriteAttributeString("r", Color.R.ToString());
			xml.WriteAttributeString("g", Color.G.ToString());
			xml.WriteAttributeString("b", Color.B.ToString());
			xml.WriteAttributeString("a", Color.A.ToString());
			xml.WriteEndElement();

	

			xml.WriteStartElement("bgcolor");
			xml.WriteAttributeString("r", BgColor.R.ToString());
			xml.WriteAttributeString("g", BgColor.G.ToString());
			xml.WriteAttributeString("b", BgColor.B.ToString());
			xml.WriteAttributeString("a", BgColor.A.ToString());
			xml.WriteEndElement();

	
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual bool Load(XmlNode node)
		{
			if (node == null)
			return false;

			switch (node.Name.ToLower())
			{

				// Main color
				case "color":
				{
					Color = Color.FromArgb(Int32.Parse(node.Attributes["a"].Value),
											Int32.Parse(node.Attributes["r"].Value),
											Int32.Parse(node.Attributes["g"].Value),
											Int32.Parse(node.Attributes["b"].Value));														
				}
				break;

				// Background color
				case "bgcolor":
				{
					BgColor = Color.FromArgb(Int32.Parse(node.Attributes["a"].Value),
											Int32.Parse(node.Attributes["r"].Value),
											Int32.Parse(node.Attributes["g"].Value),
											Int32.Parse(node.Attributes["b"].Value));														
				}
				break;

	
				// Rectangle of the element
				case "rectangle":
				{

					Rectangle rect = Rectangle.Empty;
					rect.X = Int32.Parse(node.Attributes["x"].Value);
					rect.Y = Int32.Parse(node.Attributes["y"].Value);
					rect.Width = Int32.Parse(node.Attributes["width"].Value);
					rect.Height = Int32.Parse(node.Attributes["height"].Value);

					Rectangle = rect;
				}
				break;


				// Unknown...
				default:
				{
					return false;
				}
			}

			return true;
		}

		#endregion


		#region Properties


		/// <summary>
		/// Rectangle
		/// </summary>
		public Rectangle Rectangle;



		/// <summary>
		/// Size of the element
		/// </summary>
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return Rectangle.Size;
			}
			set
			{
				Rectangle.Size = value;
			}
		}



		/// <summary>
		/// Location of the element
		/// </summary>
		[Browsable(false)]
		public Point Location
		{
			get
			{
				return Rectangle.Location;
			}
			set
			{
				Rectangle.Location = value;
			}
		}




		/// <summary>
		/// Gets/Sets the color of the element
		/// </summary>
		[Category("Color")]
		public Color Color
		{
			get;
			set;
		}
 
         
		/// <summary>
		/// Gets/Sets the background color of the element
		/// </summary>
		[Category("Color")]
		public Color BgColor
		{
			get;
			set;
		}

         
 
         

		#endregion
	}

}
