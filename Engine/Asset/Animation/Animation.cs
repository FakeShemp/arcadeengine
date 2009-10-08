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
using System.Xml;
using System.Drawing;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;


//
// http://www.ziggyware.com/readarticle.php?article_id=138
// http://www.ziggyware.com/readarticle.php?article_id=141
// http://www.indielib.com/documentation/class_i_n_d___animation.html
// http://www.indielib.com/wiki/index.php?title=Tutorial_04_IND_Animation
//
//
// http://wiki.themanaworld.org/index.php/Animations
//
//
// http://www.sdltutorials.com/sdl-animation/
// http://citrusengine.com/manual/manual/animation_system
//
//
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
		public Animation()
		{
			Frames = new List<int>();
			Time = TimeSpan.Zero;
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
		/// <param name="time">Milliseconds elapsed since last update</param>
		public void Update(TimeSpan time)
		{
			if (State != AnimationState.Play)
				return;

			// Update the chrono
			Time += time;

			// Not the time to change frame
			if (Time < FrameRate)
				return;

			while (Time >= FrameRate)
			{

				// Next frame
				CurrentFrame++;

				switch (Type)
				{

					// Loop
					case AnimationType.Loop:
					{
						if (CurrentFrame >= Frames.Count)
							CurrentFrame = 0;
					}
					break;


					// One way
					case AnimationType.OneWay:
					{
						CurrentFrame = Math.Min(Frames.Count - 1, CurrentFrame);
					}
					break;

					// Ping pong
					case AnimationType.PingPong:
					{
						if (CurrentFrame >= Frames.Count)
							CurrentFrame = 0;
					}
					break;
				}

				Time -= FrameRate;
			}

		}




		/// <summary>
		/// Draws the animation
		/// </summary>
		/// <param name="location">Location on the screen</param>
		public void Draw(Point location)
		{
			Draw(location, 0.0f, false, false);
		}



		/// <summary>
		/// Draws the animation
		/// </summary>
		/// <param name="location">Location on the scren</param>
		/// <param name="rotate">Angle of rotation</param>
		/// <param name="flipx">Horizontal flip</param>
		/// <param name="flipy">Vertical flip</param>
		public void Draw(Point location, float rotate, bool flipx, bool flipy)
		{
			if (TileSet == null)
				return;


			if (CurrentTile == null)
				return;

			//Rectangle rect = new Rectangle(location, CurrentTile.Size);

			TileSet.Draw(CurrentFrame, location);

		}

		/// <summary>
		/// Defines the tileset to use
		/// </summary>
		/// <param name="name">Name of the TileSet</param>
		/// <returns>True if loaded, otherwise false</returns>
		public bool SetTileSet(string name)
		{
			TileSetName = name;

			TileSet = ResourceManager.CreateAsset<TileSet>(name);

			return TileSet != null;
		}


		#region Frame management

		/// <summary>
		/// Adds a frame at the end of the animtion
		/// </summary>
		/// <param name="id">ID of the tile</param>
		public void AddFrame(int id)
		{
			Frames.Add(id);
		}



		/// <summary>
		/// Adds a frame in the animtion
		/// </summary>
		/// <param name="id">ID of the tile</param>
		/// <param name="index">Position in the animation</param>
		public void AddFrame(int id, int index)
		{
			Frames.Insert(index, id);
		}


		/// <summary>
		/// Removes a frame
		/// </summary>
		/// <param name="index">Index of the frame</param>
		public void RemoveFrame(int index)
		{
			Frames.Remove(index);
		}


		/// <summary>
		/// Clear all frames
		/// </summary>
		public void ClearFrames()
		{
			Frames.Clear();
		}

		#endregion


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


			xml.WriteStartElement(XmlTag);
			xml.WriteAttributeString("name", Name);


	//		base.SaveComment(xml);


			xml.WriteStartElement("framerate");
			xml.WriteAttributeString("value", FrameRate.ToString());
			xml.WriteEndElement();


			xml.WriteStartElement("tileset");
			xml.WriteAttributeString("name", TileSetName);
			xml.WriteEndElement();

			xml.WriteStartElement("loop");
			xml.WriteAttributeString("value", Type.ToString());
			xml.WriteEndElement();


			foreach (int id in Frames)
			{
				xml.WriteStartElement("tile");
				xml.WriteAttributeString("id", id.ToString());
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

			if (xml.Name != XmlTag)
			{
				Trace.WriteLine("Expecting \"" + XmlTag + "\" in node header, found \"" + xml.Name + "\" when loading Animation.");
				return false;
			}

			Name = xml.Attributes["name"].Value;

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


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
						FrameRate = TimeSpan.Parse(node.Attributes["value"].Value);
					}
					break;

					// TileSet
					case "tileset":
					{
						SetTileSet(node.Attributes["name"].Value);
					}
					break;

					
					case "tile":
					{
						Frames.Add(int.Parse(node.Attributes["id"].Value));
					}
					break;


					default:
					{
						Trace.WriteLine("Animation : Unknown node element found (\"{0}\")", node.Name);
					}
					break;
				}
			}

			if (FrameRate == TimeSpan.Zero)
				FrameRate = TimeSpan.FromMilliseconds(250);


			return true;
		}


		#endregion


		#region Properties

		/// <summary>
		/// Name of the animation
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		[Browsable(false)]
		public string XmlTag
		{
			get
			{
				return "animation";
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
				if (TileSet == null)
					return null;


				return TileSet.GetTile(Frames[CurrentFrame]);
			}
		}
 


		/// <summary>
		/// Frame rate of the animation
		/// </summary>
		public TimeSpan FrameRate
		{
			get;
			set;
		}


		/// <summary>
		/// Time of the animation
		/// </summary>
		TimeSpan Time; 


		/// <summary>
		/// Current frame ID
		/// </summary>
		[Browsable(false)]
		public int CurrentFrame
		{
			get;
			private set;
		}


		/// <summary>
		/// Animation state
		/// </summary>
		public AnimationState State
		{
			get;
			private set;
		}
 
         


		/// <summary>
		/// Animation type
		/// </summary>
		public AnimationType Type
		{
			get;
			set;
		}




		/// <summary>
		/// TileSet
		/// </summary>
		[Browsable(false)]
		public TileSet TileSet
		{
			get;
			private set;
		}



		/// <summary>
		/// TileSet Name to use
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[DescriptionAttribute("TileSet name to use")]
		public string TileSetName
		{
			get;
			private set;
		}




		/// <summary>
		/// List of frames
		/// </summary>
		[Browsable(false)]
		public List<int> Frames
		{
			get;
			private set;
		}

												

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
