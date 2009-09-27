﻿#region Licence
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
using ArcEngine.Forms;
using ArcEngine.Graphic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ArcEngine.Asset;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Providers
{

	/// <summary>
	/// Animation provider
	/// </summary>
	public class AnimationProvider : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public AnimationProvider()
		{
			Animations = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedAnimations = new Dictionary<string, Animation>(StringComparer.OrdinalIgnoreCase);
			Name = "Animation";
			Tags = new string[] { "animation" };
			Assets = new Type[] { typeof(Animation) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetResource("ArcEngine.Data.Icons.Animation.png"));
		}



		#region IO routines


		/// <summary>
		/// Saves all assets
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save(Type type, XmlWriter xml)
		{
			if (type == typeof(Animation))
			{
				foreach (XmlNode node in Animations.Values)
					node.WriteTo(xml);
			}


			return true;
		}




		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{

			if (xml == null)
				return false;

			
			switch (xml.Name.ToLower())
			{
				case "animation":
				{

					string name = xml.Attributes["name"].Value;
					Animations[name] = xml;
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

			if (typeof(T) == typeof(Animation))
			{
				XmlNode node = null;
				if (Animations.ContainsKey(name))
					node = Animations[name];
				form = new ArcEngine.Editor.AnimationForm(node);
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
			Animations[name] = node;
		}

		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>asset's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			if (typeof(T) == typeof(Animation))
			{
				foreach (string key in Animations.Keys)
				{
					list.Add(key);
				}
			}

			list.Sort();
			return list;
		}




		/// <summary>
		/// Creates an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (Animations.ContainsKey(name))
				throw new ArgumentException("Animation already exists", name);

			// Creates an Animation
			Animation anim = new Animation();
			anim.Load(Animations[name]);

			return (T)(object)anim;
		}



		/// <summary>
		/// Returns a <c>Animation</c>
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (!Animations.ContainsKey(name))
				return null;

			return Animations[name];
		}



		/// <summary>
		/// Flush unused scripts
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
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
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Animations.Clear();
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
			if (typeof(T) == typeof(Animation))
			{
				if (SharedAnimations.ContainsKey(name))
					return (T)(object)SharedAnimations[name];

				Animation anim = new Animation();
				SharedAnimations[name] = anim;

				return (T)(object)anim;
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
			if (typeof(T) == typeof(Animation))
			{
				SharedAnimations[name] = null;
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
				SharedAnimations.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedAnimations.Clear();
		}



		#endregion


		#region Progerties


		/// <summary>
		/// Scripts
		/// </summary>
		Dictionary<string, XmlNode> Animations;

		/// <summary>
		/// Shared animations
		/// </summary>
		Dictionary<string, Animation> SharedAnimations;


		#endregion
	}
}