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

using System.Collections.Generic;
using System;
using System.Xml;
using System.ComponentModel;



//
// http://www.ziggyware.com/readarticle.php?article_id=138
// http://www.ziggyware.com/readarticle.php?article_id=141
// http://www.indielib.com/documentation/class_i_n_d___animation.html
// http://www.indielib.com/wiki/index.php?title=Tutorial_04_IND_Animation
//
//
// http://wiki.themanaworld.org/index.php/Animations
// http://www.cutoutpro.com/docs/Time%20Line/index.html
//
//
// http://www.sdltutorials.com/sdl-animation/
//
namespace ArcEngine.Asset
{

	/// <summary>
	/// Defines an animation set.
	/// </summary>
	public class Animation : IAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Animation()//string name)//: base(name)
		{
			Tiles = new List<int>();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{

			return true;
		}


		/// <summary>
		/// Update the animation
		/// </summary>
		/// <param name="elapsed">Milliseconds elapsed since last update</param>
		public void Update(int elapsed)
		{
			if (State != AnimationState.Play)
				return;

			// Update the chrono
			Elapsed += elapsed;

			// Not the time to change frame
			if (Elapsed < FrameRate)
				return;

			while (Elapsed >= FrameRate)
			{

				// Next frame
				frameID++;

				switch (Type)
				{

					// Loop
					case AnimationType.Loop:
					{
						if (frameID >= Tiles.Count)
							frameID = 0;
					}
					break;


					// One way
					case AnimationType.OneWay:
					{
						frameID = Math.Min(Tiles.Count - 1, frameID);
					}
					break;

					// Ping pong
					case AnimationType.PingPong:
					{

					}
					break;
				}

				Elapsed -= FrameRate;
			}

		}


		#region Animation control


		/// <summary>
		/// Play the animation
		/// </summary>
		public void Play()
		{
			State = AnimationState.Play;
		}

		/// <summary>
		/// ¨Pause the animation
		/// </summary>
		public void Pause()
		{
			State = AnimationState.Pause;
		}



		/// <summary>
		/// Stop the animation
		/// </summary>
		public void Stop()
		{
			State = AnimationState.Stop;
		}


		#endregion


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


			//xml.WriteStartElement("animation");
			//xml.WriteAttributeString("name", Name);


	//		base.SaveComment(xml);


			xml.WriteStartElement("framerate");
			xml.WriteAttributeString("value", FrameRate.ToString());
			xml.WriteEndElement();


			xml.WriteStartElement("tileset");
			xml.WriteAttributeString("name", tileSetName);
			xml.WriteEndElement();

			xml.WriteStartElement("loop");
			xml.WriteAttributeString("value", type.ToString());
			xml.WriteEndElement();


			foreach (int id in Tiles)
			{
				xml.WriteStartElement("tile");
				xml.WriteAttributeString("BufferID", id.ToString());
				xml.WriteEndElement();
			}


			xml.WriteEndElement();

			return true;
		}

		/// <summary>
		/// Loads the animation from a xml file
		/// </summary>
		/// <param name="xml">XmlNode to save to</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;


			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
				{
				//	base.LoadComment(node);
					continue;
				}


				switch (node.Name.ToLower())
				{
					// loop
					case "loop":
					{
						Type = (AnimationType)Enum.Parse(typeof(AnimationType), node.Attributes["value"].Value, true); ;
					}
					break;

					// framerate
					case "framerate":
					{
						FrameRate = int.Parse(node.Attributes["value"].Value);
					}
					break;

					// TileSet
					case "tileset":
					{
						TileSetName = node.Attributes["name"].Value;
					}
					break;

					
					case "tile":
					{
						Tiles.Add(int.Parse(node.Attributes["id"].Value));
					}
					break;


					default:
					{
						//Log.Send(new LogEventArgs(LogLevel.Warning, "Animation : Unknown node element found (" + node.Name + ")", null));
						Trace.WriteLine("Animation : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}


		#endregion


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
		/// Current Tile ID
		/// </summary>
		[Browsable(false)]
		public int TileID
		{
			get
			{
				if (TileSet == null || FrameID < 0)
					return -1;


				return Tiles[FrameID];
			}
		}
 
         

		/// <summary>
		/// Current animation Tile
		/// </summary>
		[Browsable(false)]
		public Tile CurrentTile
		{
			get
			{
				if (TileSet == null || FrameID < 0)
					return null;


				return TileSet.GetTile(Tiles[FrameID]);
			}
		}
 
         
		/// <summary>
		/// Framerate of the animation in miliseconds
		/// </summary>
		public int FrameRate
		{
			get
			{
				return frameRate;
			}
			set
			{
				frameRate = value;
				if (frameRate <= 0)
					frameRate = 1;
			}
		}
		int frameRate = 100;


		/// <summary>
		/// Number of ms elapsed since last update
		/// </summary>
		int Elapsed;


		/// <summary>
		/// Current frame BufferID
		/// </summary>
		[Browsable(false)]
		public int FrameID
		{
			get
			{
				return frameID;
			}
		}
		int frameID = -1;


		/// <summary>
		/// Animation state
		/// </summary>
		public AnimationState State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;

				if (state == AnimationState.Stop)
					frameID = 0;
			}
		}
		AnimationState state;
 
         


		/// <summary>
		/// Animation type
		/// </summary>
		public AnimationType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
		AnimationType type;




		/// <summary>
		/// TileSet
		/// </summary>
		[Browsable(false)]
		public TileSet TileSet
		{
			get

			{
				return tileSet;
			}
		}
		TileSet tileSet;



		/// <summary>
		/// TileSet Name to use
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[DescriptionAttribute("TileSet name to use")]
		public string TileSetName
		{
			get
			{
				return tileSetName;
			}
			set
			{
				tileSetName = value;
				tileSet = ResourceManager.CreateAsset<TileSet>(value);
			}
		}
		string tileSetName;




		/// <summary>
		/// List of tiles
		/// </summary>
		public List<int> Tiles
		{
			get
			{
				return tiles;
			}
			set
			{
				tiles = value;
			}
		}
		List<int> tiles;

												

		#endregion
	}


	/// <summary>
	/// Animation state
	/// </summary>
	public enum AnimationState
	{
		/// <summary>
		/// Animation stoped
		/// </summary>
		Stop = 0,

		/// <summary>
		/// Currently playing the animation
		/// </summary>
		Play = 1,


		/// <summary>
		/// Animation on hold
		/// </summary>
		Pause = 2
	}


	/// <summary>
	/// Type of animation
	/// </summary>
	public enum AnimationType
	{
		/// <summary>
		/// 
		/// </summary>
		OneWay = 0,

		/// <summary>
		/// 
		/// </summary>
		Loop,


		/// <summary>
		/// 
		/// </summary>
		PingPong
	}
}
