using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Collections;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Class representing a layer in an animation
	/// </summary>
	public class AnimationLayer : IComparable<AnimationLayer>
	{

		/// <summary>
		/// Default construcor
		/// </summary>
		public AnimationLayer(Animation anim)
		{

			Animation = anim;

		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node"></param>
		public AnimationLayer(XmlNode node)
		{
			
			Load(node);
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


			//xml.WriteStartElement("animation");
			//xml.WriteAttributeString("name", Name);


			//		base.SaveComment(xml);



			//xml.WriteStartElement("loop");
			//xml.WriteAttributeString("value", type.ToString());
			//xml.WriteEndElement();


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

			if (xml.Name != "layer")
			{
				Trace.WriteLine("Expecting \"layer\" in node header, found \"" + xml.Name + "\" when loading AnimationLayer.");
				return false;
			}


			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{
				switch (node.Name.ToLower())
				{
					case "name":
					{
						Name = node.Attributes["value"].Value;
					}
					break;

					case "viewport":
					{
						Viewport = new Rectangle(int.Parse(node.Attributes["x"].Value),
							int.Parse(node.Attributes["y"].Value),
							int.Parse(node.Attributes["width"].Value),
							int.Parse(node.Attributes["height"].Value));
					}
					break;

					case "id":
					{
						ID = int.Parse(node.Attributes["value"].Value);
					}
					break;

					case "keyframe":
					{
						KeyFrame key = new KeyFrame();
						key.Load(node);
						AddKeyFrame(key);
					}
					break;


					default:
					{
						Trace.WriteLine("AnimationLayer : Unknown node element found (" + node.Name + ")");
					}
					break;
				}
			}

			return true;
		}


		#endregion


		#region Frames management



		/// <summary>
		/// Adds a frame
		/// </summary>
		/// <param name="frame">KeyFrame to add</param>
		public void AddKeyFrame(KeyFrame frame)
		{
			if (frame == null)
				return;

			KeyFrames.Add(frame);
			KeyFrames.Sort();
		}


		/// <summary>
		/// Removes a KeyFrame
		/// </summary>
		/// <param name="frame"></param>
		public void RemoveKeyFrame(KeyFrame frame)
		{
		}



		/// <summary>
		/// Removes the KeyFrame at a given TimeSpan
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public bool RemoveKeyFrame(TimeSpan time)
		{

			return false;
		}


		/// <summary>
		/// Returns the next Keyframe
		/// </summary>
		/// <param name="time"></param>
		/// <returns>The next KeyFrame, or the last KeyFrame</returns>
		public KeyFrame GetNextKeyFrame(TimeSpan time)
		{
			for (int i = 0; i < KeyFrames.Count; i++)
			{
				if (KeyFrames[i].Time > time)
					return KeyFrames[i];
			}

			return LastKeyFrame;
		}


		/// <summary>
		/// Returns the previous Keyframe
		/// </summary>
		/// <param name="time"></param>
		/// <returns>The previous KeyFrame, or the first KeyFrame</returns>
		public KeyFrame GetPreviousKeyFrame(TimeSpan time)
		{
			for (int i = KeyFrames.Count - 1; i >= 0; i--)
			{
				if (KeyFrames[i].Time < time)
					return KeyFrames[i];
			}

			return FirstKeyFrame;
		}



		#endregion


		#region Properties

		/// <summary>
		/// Parent animation
		/// </summary>
		public Animation Animation
		{
			get;
			private set;
		}

		/// <summary>
		/// ID of the layer
		/// </summary>
		public int ID
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the layer
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// ID of the tile
		/// </summary>
		public int TileID
		{
			get;
			set;
		}


		/// <summary>
		/// Location of the tile
		/// </summary>
		public Point Location
		{
			get;
			set;
		}


		/// <summary>
		/// Viewport of the layer
		/// </summary>
		public Rectangle Viewport
		{
			get;
			set;
		}


		/// <summary>
		/// Number of frame in the layer
		/// </summary>
		public TimeSpan Length
		{
			get
			{
				if (KeyFrames.Count == 0)
					return TimeSpan.Zero;

				return KeyFrames[KeyFrames.Count - 1].Time;
			}
		}


		/// <summary>
		/// Keyframes of the layer
		/// </summary>
		List<KeyFrame> KeyFrames = new List<KeyFrame>();

		/// <summary>
		/// Gets the first KeyFrame
		/// </summary>
		/// <returns></returns>
		public KeyFrame FirstKeyFrame
		{
			get
			{
				if (KeyFrames.Count == 0)
					return null;

				return KeyFrames[0];
			}
		}

		/// <summary>
		/// Gets the last KeyFrame
		/// </summary>
		public KeyFrame LastKeyFrame
		{
			get
			{
				if (KeyFrames.Count == 0)
					return null;

				return KeyFrames[KeyFrames.Count - 1];
			}
		}

		#endregion


		#region Comparer

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static IComparer Sorter
		{
			get
			{
				return (IComparer)new AnimationLayerComparer();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(AnimationLayer other)
		{
			if (other == null)
				return 1;

			if (other == this)
				return 0;

			if (ID > other.ID)
				return 1;

			return -1;
		}

		/// <summary>
		/// 
		/// </summary>
		private class AnimationLayerComparer : IComparer<AnimationLayer>
		{
			public int Compare(AnimationLayer x, AnimationLayer y)
			{
				if (x == null && y == null)
					return 0;

				else if (x == null)
					return -1;

				else if (y == null)
					return 1;

				if (x == y)
					return 0;

				return (int)(x.ID - y.ID);
			}

		}


		#endregion	
	}
}
