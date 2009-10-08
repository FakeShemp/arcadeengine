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
			Scenes = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedScenes = new Dictionary<string, Scene>(StringComparer.OrdinalIgnoreCase);
			Name = "Animation";
			Tags = new string[] { "animation", "scene" };
			Assets = new Type[] { typeof(Animation), typeof(Scene) };
			Version = new Version(0, 1);
			EditorImage = new Bitmap(ResourceManager.GetResource("ArcEngine.Data.Icons.Animation.png"));
		}


		#region Init & Close


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			return false;
		}



		/// <summary>
		/// Close all opened resources
		/// </summary>
		public override void Close()
		{

		}

		#endregion


		#region IO routines


		/// <summary>
		/// Saves all assets
		/// </summary>
		///<typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{
			if (typeof(T) == typeof(Animation))
			{
				foreach (XmlNode node in Animations.Values)
					node.WriteTo(xml);
			}

			if (typeof(T) == typeof(Scene))
			{
				foreach (XmlNode node in Scenes.Values)
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


				case "scene":
				{
					string name = xml.Attributes["name"].Value;
					Scenes[name] = xml;
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

			if (typeof(T) == typeof(Scene))
			{
				XmlNode node = null;
				if (Scenes.ContainsKey(name))
					node = Scenes[name];
				form = new ArcEngine.Editor.SceneForm(node);
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

			if (typeof(T) == typeof(Animation))
				Animations[name] = node;
			if (typeof(T) == typeof(Scene))
				Scenes[name] = node;
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
				foreach (string key in Animations.Keys)
					list.Add(key);

			if (typeof(T) == typeof(Scene))
				foreach (string key in Scenes.Keys)
					list.Add(key);

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

			if (typeof(T) == typeof(Animation) && Animations.ContainsKey(name))
			{
				Animation anim = new Animation();
				anim.Load(Animations[name]);

				return (T)(object)anim;
			}


			if (typeof(T) == typeof(Scene) && Scenes.ContainsKey(name))
			{
				Scene scene = new Scene();
				scene.Load(Scenes[name]);

				return (T)(object)scene;
			}



			return default(T);
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

			if (typeof(T) == typeof(Animation) && Animations.ContainsKey(name))
				return Animations[name];

			if (typeof(T) == typeof(Scene) && Scenes.ContainsKey(name))
				return Scenes[name];

			return null;
		}



		/// <summary>
		/// Flush unused assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
			if (typeof(T) == typeof(Animation))
				Animations.Clear();
			if (typeof(T) == typeof(Scene))
				Scenes.Clear();
		}

	
		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(Animation) && Animations.ContainsKey(name))
				Animations.Remove(name);

			if (typeof(T) == typeof(Scene) && Scenes.ContainsKey(name))
				Scenes.Remove(name);
		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Animations.Clear();
			Scenes.Clear();
		}


		/// <summary>
		/// Returns the number of asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <returns>Number of asset</returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(Animation))
				return Animations.Count;


			if (typeof(T) == typeof(Scene))
				return Scenes.Count;

			return 0;
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

			if (typeof(T) == typeof(Scene))
			{
				if (SharedScenes.ContainsKey(name))
					return (T)(object)SharedScenes[name];

				Scene scene = new Scene();
				SharedScenes[name] = scene;

				return (T)(object)scene;
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
			if (typeof(T) == typeof(Scene))
				SharedScenes.Remove(name);

			if (typeof(T) == typeof(Animation))
				SharedAnimations.Remove(name);
		}




		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(Animation))
				SharedAnimations.Clear();

			if (typeof(T) == typeof(Scene))
				SharedScenes.Clear();
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedAnimations.Clear();
			SharedScenes.Clear();
		}



		#endregion


		#region Properties


		/// <summary>
		/// Animations
		/// </summary>
		Dictionary<string, XmlNode> Animations;

		/// <summary>
		/// Shared animations
		/// </summary>
		Dictionary<string, Animation> SharedAnimations;



		/// <summary>
		/// Scenes
		/// </summary>
		Dictionary<string, XmlNode> Scenes;


		/// <summary>
		/// Shared scenes
		/// </summary>
		Dictionary<string, Scene> SharedScenes;

		#endregion
	}
}
