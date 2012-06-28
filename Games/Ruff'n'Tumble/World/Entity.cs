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
using RuffnTumble.Interface;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;

//
//- Colision detection
//  http://supertux.lethargik.org/wiki/Collision_detection
//  http://www.harveycartel.org/metanet/tutorials/tutorialA.html
//  http://www.indielib.com/wiki/index.php?title=Tutorial_08_Collisions
//
//
//
//
//
//
//  Current_Annimation  Get Or Set The Current Animation 
//  Current_Frame  Get Or Set The Curent Frame 
//
//
//
//
namespace RuffnTumble
{

	/// <summary>
	/// Base class for every entity in the game
	/// </summary>
	public abstract class Entity : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="level">Level handle</param>
		public Entity(Level level)
		{
			if (level == null)
				throw new ArgumentNullException("level", "Level handle is null");

			Level = level;
		}


		/// <summary>
		/// Disposes resources
		/// </summary>
		public virtual void Dispose()
		{
		}


		/// <summary>
		/// Initialize the entity
		/// </summary>
		/// <returns>Success or not</returns>
		public virtual bool LoadContent()
		{
			return true;
		}


		#region IO


		/// <summary>
		/// Saves the entity
		/// </summary>
		/// <param name="xml">XmlWriter handle</param>
		/// <returns></returns>
		public virtual bool Save(XmlWriter xml)
		{
			return true;
		}
/*
		{

			xml.WriteStartElement("entity");
			xml.WriteAttributeString("name", Name);


	//		base.SaveComment(xml);


			xml.WriteStartElement("location");
			xml.WriteAttributeString("x", Location.X.ToString());
			xml.WriteAttributeString("y", Location.Y.ToString());
			xml.WriteEndElement();


			//xml.WriteStartElement("model");
			//xml.WriteAttributeString("name", ModelName);
			//xml.WriteEndElement();
			//xml.WriteEndElement();


			//if (ScriptInterface != null)
			//    ScriptInterface.Save(this, xml);

			xml.WriteEndElement();

			return true;
		}
*/


		/// <summary>
		/// Loads the entity
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		/// <returns></returns>
		public virtual bool Load(XmlNode xml)
		{
			return true;
		}
/*		{
			if (xml == null)
				return false;

			Name = xml.Attributes["name"].Value;


			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
				//	base.LoadComment(node);
					continue;
				}

				switch (node.Name.ToLower())
				{
					case "location":
					{
						Location = new Vector2(float.Parse(node.Attributes["x"].Value), float.Parse(node.Attributes["y"].Value));
					}
					break;

					//case "model":
					//{
					//    ModelName = node.Attributes["name"].Value;
					//}
					//break;

					default:
					{
						Trace.WriteLine("Entity : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}


			//if (ScriptInterface != null)
			//    ScriptInterface.Load(this, xml);

			return true;
		}
*/
		#endregion


		#region Rendering  & update

		/// <summary>
		/// Update the entity
		/// </summary>
		public virtual void Update(GameTime time)
		{
		}


		/// <summary>
		/// Renders the entity
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		public virtual void Draw(SpriteBatch batch, Camera camera)
		{
		}

		#endregion


		#region


		/// <summary>
		/// Called when the entity
		/// <param name="killedBy">
		/// The enemy who killed the entity. This parameter is null if the entity was
		/// killed by self.
		/// </param>
		public virtual void OnKilled(Entity killedBy)
		{
		}



		#endregion

		#region Properties



		/// <summary>
		/// Level handle
		/// </summary>
		protected Level Level;


		/// <summary>
		/// Gets / sets the entity debug mode
		/// </summary>
		[Browsable(false)]
		public bool Debug
		{
			get;
			set;
		}




		/// <summary>
		/// Gets/sets the entity a god (cheating)
		/// </summary>
		[Category("Cheat")]
		[Description("Entity is like a God")]
		public bool IsInvincible
		{
			get;
			set;
		}



		/// <summary>
		/// Gets / sets entity position in world space
		/// </summary>
		public Vector2 Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}
		protected Vector2 position;



		/// <summary>
		/// Player location in world space
		/// </summary>
		public Point LayerCoordinate
		{
			get
			{
				if (Level == null)
					return Point.Empty;

				return new Point((int)(position.X / Level.BlockSize.Width), (int)(position.Y / Level.BlockSize.Height));
			}
		}


		/// <summary>
		/// Gets / sets entity velocity in the level
		/// </summary>
		public Vector2 Velocity
		{
			get
			{
				return velocity;
			}
			set
			{
				velocity = value;
			}
		}
		protected Vector2 velocity;



		/// <summary>
		/// Gets a rectangle which bounds the entity in world space.
		/// </summary>
		public Rectangle BoundingRectangle
		{
			get
			{
				int left = (int)Math.Round(Position.X - 16) + localBounds.X;
				int top = (int)Math.Round(Position.Y - 64) + localBounds.Y;

				return new Rectangle(left, top, localBounds.Width, localBounds.Height);
			}
		}

		/// <summary>
		/// Entity's collision rectangle in world space
		/// </summary>
		protected Rectangle localBounds;


		/// <summary>
		/// Gets whether or not the entity's feet are on the ground.
		/// </summary>
		public bool IsOnGround
		{
			get { return isOnGround; }
		}
		protected bool isOnGround;


		/// <summary>
		/// Gets whether or not the entity's feet are inside a slope.
		/// </summary>
		public bool IsInSlope
		{
			get { return isInSlope; }
		}
		protected bool isInSlope;
		protected bool wasInSlope;


		#endregion
	}

}
