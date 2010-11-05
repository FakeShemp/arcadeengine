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
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Audio;
using ArcEngine.Interface;

namespace ArcEngine
{

	/// <summary>
	/// Tileset provider
	/// </summary>
	public class Providers : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Providers()
		{
			Models = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);



			TileSets = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Strings = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Animations = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Scenes = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Fonts = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Scripts = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Audios = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Schemes = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Layouts = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Shaders = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);


			SharedSchemes = new Dictionary<string, InputScheme>(StringComparer.OrdinalIgnoreCase);
			SharedFonts = new Dictionary<string, BitmapFont>(StringComparer.OrdinalIgnoreCase);
			SharedStrings = new Dictionary<string, StringTable>(StringComparer.OrdinalIgnoreCase);
			SharedTileSets = new Dictionary<string, TileSet>(StringComparer.OrdinalIgnoreCase);
			SharedAnimations = new Dictionary<string, Animation>(StringComparer.OrdinalIgnoreCase);
			SharedScripts = new Dictionary<string, Script>(StringComparer.OrdinalIgnoreCase);
			SharedAudios = new Dictionary<string, AudioSample>(StringComparer.OrdinalIgnoreCase);
			SharedScenes = new Dictionary<string, Scene>(StringComparer.OrdinalIgnoreCase);
			SharedLayouts = new Dictionary<string, Layout>(StringComparer.OrdinalIgnoreCase);
			SharedShaders = new Dictionary<string, Shader>(StringComparer.OrdinalIgnoreCase);

			Name = "ArcEngine providers";
			Tags = new string[] { 
				"tileset", "stringtable", "animation", "scene", "bitmapfont",
				"script", "scriptmodel", "audio", "inputscheme", "layout", "shader", "audiosample", "audiostream"
			};
			Assets = new Type[] {
				typeof(TileSet), typeof(StringTable), typeof(Animation), typeof(Scene),typeof(Layout),
				typeof(BitmapFont), typeof(Script), typeof(ScriptModel), typeof(AudioSample), typeof(InputScheme),
				typeof(Shader),
			};




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
		public override void Dispose()
		{

		}

		#endregion


		#region IO routines


		/// <summary>
		/// Saves all assets
		/// </summary>
		/// <typeparam name="T">Type of asset</typeparam>
		/// <param name="xml">Xml</param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter xml)
		{

			if (typeof(T) == typeof(TileSet))
			{
				foreach (XmlNode node in TileSets.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(StringTable))
			{
				foreach (XmlNode node in Strings.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(Animation))
			{
				foreach (XmlNode node in Animations.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(Scene))
			{
				foreach (XmlNode node in Scenes.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(BitmapFont))
			{
				foreach (XmlNode node in Fonts.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(Script))
			{
				foreach (XmlNode node in Scripts.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(ScriptModel))
			{
				foreach (XmlNode node in Models.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(AudioSample))
			{
				foreach (XmlNode node in Audios.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(InputScheme))
			{
				foreach (XmlNode node in Schemes.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(Layout))
			{
				foreach (XmlNode node in Layouts.Values)
					node.WriteTo(xml);
			}
			else if (typeof(T) == typeof(Shader))
			{
				foreach (XmlNode node in Shaders.Values)
					node.WriteTo(xml);
			}
			else
			{
				return false;
			}

			return true;
		}


		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml">Xml</param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			string name = xml.Attributes["name"].Value;
			switch (xml.Name.ToLower())
			{
				case "tileset":
				{
					TileSets[name] = xml;
				}
				break;

				case "stringtable":
				{
					Strings[name] = xml;
				}
				break;

				case "animation":
				{
					Animations[name] = xml;
				}
				break;

				case "shader":
				{
					Shaders[name] = xml;
				}
				break;

				case "scene":
				{
					Scenes[name] = xml;
				}
				break;

				case "bitmapfont":
				{
					Fonts[name] = xml;
				}
				break;
				case "script":
				{
					Scripts[name] = xml;
				}
				break;

				case "scriptmodel":
				{
					Models[name] = xml;
				}
				break;

				case "audio":
				{
					Audios[name] = xml;
				}
				break;

				case "inputscheme":
				{
					Schemes[name] = xml;
				}
				break;

				case "layout":
				{
					Layouts[name] = xml;
				}
				break;

				default:
				{
					Trace.WriteLine("Unknown asset \"{0}\"", name);
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
		/// <typeparam name="T">Type of asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public override AssetEditorBase EditAsset<T>(string name)
		{
			AssetEditorBase form = null;
			XmlNode node = null;

			if (typeof(T) == typeof(TileSet))
			{
				if (TileSets.ContainsKey(name))
					node = TileSets[name];
				form = new TileSetForm(node);
			}
			else if (typeof(T) == typeof(StringTable))
			{
				if (Strings.ContainsKey(name))
					node = Strings[name];
				form = new ArcEngine.Editor.StringTableForm(node);
			}
			else if (typeof(T) == typeof(Shader))
			{
				if (Shaders.ContainsKey(name))
					node = Shaders[name];
				//form = new ArcEngine.Editor.ShaderForm(node);
			}
			else if (typeof(T) == typeof(Animation))
			{
				if (Animations.ContainsKey(name))
					node = Animations[name];
				form = new ArcEngine.Editor.AnimationForm(node);
			}

			else if (typeof(T) == typeof(Scene))
			{
				if (Scenes.ContainsKey(name))
					node = Scenes[name];
				form = new ArcEngine.Editor.SceneForm(node);
			}
			else if (typeof(T) == typeof(BitmapFont))
			{
				if (Fonts.ContainsKey(name))
					node = Fonts[name];
				form = new ArcEngine.Editor.BitmapFontForm(node);
			}
			else if (typeof(T) == typeof(Script))
			{
				if (Scripts.ContainsKey(name))
					node = Scripts[name];
				form = new ArcEngine.Editor.ScriptForm(node);
			}
			else if (typeof(T) == typeof(ScriptModel))
			{
				if (Models.ContainsKey(name))
					node = Models[name];
				form = new ArcEngine.Editor.ScriptModelForm(node);
			}
			else if (typeof(T) == typeof(AudioSample))
			{
				if (Audios.ContainsKey(name))
					node = Audios[name];
				form = new AudioForm(node);
			}
			else if (typeof(T) == typeof(InputScheme))
			{
				if (Schemes.ContainsKey(name))
					node = Schemes[name];
				form = new ArcEngine.Editor.InputSchemeForm(node);
			}

			else if (typeof(T) == typeof(Layout))
			{
				if (Layouts.ContainsKey(name))
					node = Layouts[name];
				form = new ArcEngine.Editor.LayoutForm(node);
			}


			if (form == null)
				return null;

			form.TabText = name;
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

			if (typeof(T) == typeof(TileSet))
				TileSets[name] = node;
			else if (typeof(T) == typeof(StringTable))
				Strings[name] = node;
			else if (typeof(T) == typeof(Shader))
				Shaders[name] = node;
			else if (typeof(T) == typeof(Animation))
				Animations[name] = node;
			else if (typeof(T) == typeof(Scene))
				Scenes[name] = node;
			else if (typeof(T) == typeof(BitmapFont))
				Fonts[name] = node;
			else if (typeof(T) == typeof(Script))
				Scripts[name] = node;
			else if (typeof(T) == typeof(ScriptModel))
				Models[name] = node;
			else if (typeof(T) == typeof(AudioSample))
				Audios[name] = node;
			else if (typeof(T) == typeof(InputScheme))
				Schemes[name] = node;
			else if (typeof(T) == typeof(Layout))
				Layouts[name] = node;

		}


		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <typeparam name="T">Type of asset</typeparam>
		/// <returns>asset's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();


			if (typeof(T) == typeof(TileSet))
				foreach (string key in TileSets.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(StringTable))
				foreach (string key in Strings.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Shader))
				foreach (string key in Shaders.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Animation))
				foreach (string key in Animations.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Scene))
				foreach (string key in Scenes.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(BitmapFont))
				foreach (string key in Fonts.Keys)
					list.Add(key);
			
			else if (typeof(T) == typeof(Script))
				foreach (string key in Scripts.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(ScriptModel))
				foreach (string key in Models.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(AudioSample))
				foreach (string key in Audios.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(InputScheme))
				foreach (string key in Schemes.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Layout))
				foreach (string key in Layouts.Keys)
					list.Add(key);


			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates a new asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns>Asset or null</returns>
		public override T Create<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(TileSet) && TileSets.ContainsKey(name))
			{
				TileSet tileset = new TileSet();
				tileset.Load(TileSets[name]);
				return (T)(object)tileset;
			}

			else if (typeof(T) == typeof(StringTable) && Strings.ContainsKey(name))
			{
				StringTable str = new StringTable();
				str.Load(Strings[name]);
				return (T)(object)str;
			}

			else if (typeof(T) == typeof(Shader) && Shaders.ContainsKey(name))
			{
				Shader shader = new Shader();
				shader.Load(Shaders[name]);
				return (T)(object)shader;
			}

			else if (typeof(T) == typeof(Animation) && Animations.ContainsKey(name))
			{
				Animation anim = new Animation();
				anim.Load(Animations[name]);
				return (T)(object)anim;
			}


			else if (typeof(T) == typeof(Scene) && Scenes.ContainsKey(name))
			{
				Scene scene = new Scene();
				scene.Load(Scenes[name]);
				return (T)(object)scene;
			}

			else if (typeof(T) == typeof(BitmapFont) && Fonts.ContainsKey(name))
			{
				BitmapFont font = new BitmapFont();
				font.Load(Fonts[name]);
				return (T)(object)font;
			}

			if (typeof(T) == typeof(Script) && Scripts.ContainsKey(name))
			{
				Script script = new Script();
				script.Load(Scripts[name]);
				return (T)(object)script;
			}

			else if (typeof(T) == typeof(ScriptModel) && Models.ContainsKey(name))
			{
				ScriptModel model = new ScriptModel();
				model.Load(Models[name]);
				return (T)(object)model;
			}

			else if (typeof(T) == typeof(AudioSample) && Audios.ContainsKey(name))
			{
				AudioSample sound = new AudioSample();
				sound.Load(Audios[name]);
				return (T)(object)sound;
			}

			else if (typeof(T) == typeof(InputScheme) && Schemes.ContainsKey(name))
			{
				InputScheme scheme = new InputScheme();
				scheme.Load(Schemes[name]);
				return (T)(object)scheme;
			}

			else if (typeof(T) == typeof(Layout) && Layouts.ContainsKey(name))
			{
				Layout layout = new Layout();
				layout.Load(Layouts[name]);
				return (T)(object)layout;
			}

			return default(T);
		}


		/// <summary>
		/// Returns an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns>Xml definition</returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(TileSet) && TileSets.ContainsKey(name))
				return TileSets[name];

			else if (typeof(T) == typeof(StringTable) && Strings.ContainsKey(name))
				return Strings[name];

			else if (typeof(T) == typeof(Shader) && Shaders.ContainsKey(name))
				return Shaders[name];

			else if (typeof(T) == typeof(Animation) && Animations.ContainsKey(name))
				return Animations[name];

			else if (typeof(T) == typeof(Scene) && Scenes.ContainsKey(name))
				return Scenes[name];

			else if (typeof(T) == typeof(BitmapFont) && Fonts.ContainsKey(name))
				return Fonts[name];

			else if (typeof(T) == typeof(Script) && Scripts.ContainsKey(name))
				return Scripts[name];

			else if (typeof(T) == typeof(ScriptModel) && Models.ContainsKey(name))
				return Models[name];

			else if (typeof(T) == typeof(AudioSample) && Audios.ContainsKey(name))
					return Audios[name];

			else if (typeof(T) == typeof(InputScheme) && Schemes.ContainsKey(name))
				return Schemes[name];

			else if (typeof(T) == typeof(Layout) && Layouts.ContainsKey(name))
				return Layouts[name];

			return null;
		}


		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <typeparam name="T">Type of teh asset</typeparam>
		/// <param name="name">Name</param>
		public override void Remove<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(T) == typeof(TileSet) && TileSets.ContainsKey(name))
				TileSets.Remove(name);

			else if (typeof(T) == typeof(StringTable) && Strings.ContainsKey(name))
				Strings.Remove(name);

			else if (typeof(T) == typeof(Animation) && Animations.ContainsKey(name))
				Animations.Remove(name);

			else if (typeof(T) == typeof(Shader) && Shaders.ContainsKey(name))
				Shaders.Remove(name);

			else if (typeof(T) == typeof(Scene) && Scenes.ContainsKey(name))
				Scenes.Remove(name);

			else if (typeof(T) == typeof(BitmapFont) && Fonts.ContainsKey(name))
				Fonts.Remove(name);

			else if (typeof(T) == typeof(Script) && Scripts.ContainsKey(name))
				Scripts.Remove(name);

			else if (typeof(T) == typeof(ScriptModel) && Models.ContainsKey(name))
				Models.Remove(name);

			else if (typeof(T) == typeof(AudioSample) && Audios.ContainsKey(name))
				Audios.Remove(name);

			else if (typeof(T) == typeof(InputScheme) && Schemes.ContainsKey(name))
				Schemes.Remove(name);

			else if (typeof(T) == typeof(Layout) && Layouts.ContainsKey(name))
				Layouts.Remove(name);

		}


		/// <summary>
		/// Removes assets of a specific type
		/// </summary>
		/// <typeparam name="T">Type of asset</typeparam>
		public override void Remove<T>()
		{
			if (typeof(T) == typeof(TileSet))
				TileSets.Clear();

			else if (typeof(T) == typeof(StringTable))
				Strings.Clear();

			else if (typeof(T) == typeof(Animation))
				Animations.Clear();

			else if (typeof(T) == typeof(Scene))
				Scenes.Clear();

			else if (typeof(T) == typeof(BitmapFont))
				Fonts.Clear();

			else if (typeof(T) == typeof(Shader))
				Shaders.Clear();

			else if (typeof(T) == typeof(Script))
				Scripts.Clear();

			else if (typeof(T) == typeof(ScriptModel))
				Models.Clear();

			else if (typeof(T) == typeof(AudioSample))
				Audios.Clear();

			else if (typeof(T) == typeof(InputScheme))
				Schemes.Clear();

			else if (typeof(T) == typeof(Layout))
				Layouts.Clear();

		}


		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			TileSets.Clear();
			Strings.Clear();
			Animations.Clear();
			Scenes.Clear();
			Fonts.Clear();
			Scripts.Clear();
			Models.Clear();
			Audios.Clear();
			Schemes.Clear();
			Layouts.Clear();
			Shaders.Clear();
		}


		/// <summary>
		/// Returns the number of known assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Number of available asset</returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(TileSet))
				return TileSets.Count;

			else if (typeof(T) == typeof(StringTable))
				return Strings.Count;

			else if (typeof(T) == typeof(Animation))
				return Animations.Count;

			else if (typeof(T) == typeof(Shader))
				return Shaders.Count;

			else if (typeof(T) == typeof(Scene))
				return Scenes.Count;

			else if (typeof(T) == typeof(BitmapFont))
				return Fonts.Count;

			else if (typeof(T) == typeof(Script))
				return Scripts.Count;

			else if (typeof(T) == typeof(ScriptModel))
				return Models.Count;

			else if (typeof(T) == typeof(AudioSample))
				return Audios.Count;

			else if (typeof(T) == typeof(InputScheme))
				return Schemes.Count;

			else if (typeof(T) == typeof(Layout))
				return Layouts.Count;

			return 0;
		}

		#endregion


		#region Shared assets

		/// <summary>
		/// Adds an asset as Shared
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset to register</param>
		/// <param name="asset">Asset's handle</param>
		public override void AddShared<T>(string name, IAsset asset)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (typeof(T) == typeof(TileSet))
				SharedTileSets[name] = asset as TileSet;

			else if (typeof(T) == typeof(StringTable))
				SharedStrings[name] = asset as StringTable;

			else if (typeof(T) == typeof(Shader))
				SharedShaders[name] = asset as Shader;

			else if (typeof(T) == typeof(Animation))
				SharedAnimations[name] = asset as Animation;

			else if (typeof(T) == typeof(Scene))
				SharedScenes[name] = asset as Scene;

			else if (typeof(T) == typeof(BitmapFont))
				SharedFonts[name] = asset as BitmapFont;

			else if (typeof(T) == typeof(Script))
				SharedScripts[name] = asset as Script;

			else if (typeof(T) == typeof(AudioSample))
				SharedAudios[name] = asset as AudioSample;

			else if (typeof(T) == typeof(InputScheme))
				SharedSchemes[name] = asset as InputScheme;

			else if (typeof(T) == typeof(Layout))
				SharedLayouts[name] = asset as Layout;

		}


		/// <summary>
		/// Gets a shared asset
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Shared name</param>
		/// <returns>Asset handle or null</returns>
		public override T GetShared<T>(string name)
		{
			
			if (typeof(T) == typeof(TileSet))
			{
				if (SharedTileSets.ContainsKey(name))
					return (T)(object)SharedTileSets[name];
			}

			else if (typeof(T) == typeof(StringTable))
			{
				if (SharedStrings.ContainsKey(name))
					return (T)(object)SharedStrings[name];
			}

			else if (typeof(T) == typeof(Animation))
			{
				if (SharedAnimations.ContainsKey(name))
					return (T)(object)SharedAnimations[name];
			}

			else if (typeof(T) == typeof(Shader))
			{
				if (SharedShaders.ContainsKey(name))
					return (T)(object)SharedShaders[name];
			}

			else if (typeof(T) == typeof(Scene))
			{
				if (SharedScenes.ContainsKey(name))
					return (T)(object)SharedScenes[name];
			}

			else if (typeof(T) == typeof(BitmapFont))
			{
				if (SharedFonts.ContainsKey(name))
					return (T)(object)SharedFonts[name];
			}

			else if (typeof(T) == typeof(Script))
			{
				if (SharedScripts.ContainsKey(name))
					return (T)(object)SharedScripts[name];
			}

			else if (typeof(T) == typeof(AudioSample))
			{
				if (SharedAudios.ContainsKey(name))
					return (T)(object)SharedAudios[name];
			}

			else if (typeof(T) == typeof(InputScheme))
			{
				if (SharedSchemes.ContainsKey(name))
					return (T)(object)SharedSchemes[name];
			}

			else if (typeof(T) == typeof(Layout))
			{
				if (SharedLayouts.ContainsKey(name))
					return (T)(object)SharedLayouts[name];
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
			if (string.IsNullOrEmpty(name))
				return;

			if (typeof(T) == typeof(TileSet))
				SharedTileSets.Remove(name);

			else if (typeof(T) == typeof(StringTable))
				SharedStrings.Remove(name);

			else if (typeof(T) == typeof(Shader))
				SharedShaders.Remove(name);

			else if (typeof(T) == typeof(Scene))
				SharedScenes.Remove(name);

			else if (typeof(T) == typeof(Animation))
				SharedAnimations.Remove(name);

			else if (typeof(T) == typeof(BitmapFont))
				SharedFonts.Remove(name);

			else if (typeof(T) == typeof(Script))
				SharedScripts.Remove(name);

			else if (typeof(T) == typeof(AudioSample))
				SharedAudios.Remove(name);

			else if (typeof(T) == typeof(InputScheme))
				SharedSchemes.Remove(name);

			else if (typeof(T) == typeof(Layout))
				SharedLayouts.Remove(name);

		}


		/// <summary>
		/// Removes a specific type of shared assets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(StringTable))
				SharedTileSets.Clear();

			else if (typeof(T) == typeof(StringTable))
				SharedStrings.Clear();

			else if (typeof(T) == typeof(Animation))
				SharedAnimations.Clear();

			else if (typeof(T) == typeof(Shader))
				SharedShaders.Clear();

			else if (typeof(T) == typeof(Scene))
				SharedScenes.Clear();

			else if (typeof(T) == typeof(BitmapFont))
				SharedFonts.Clear();

			else if (typeof(T) == typeof(Script))
				SharedScripts.Clear();

			else if (typeof(T) == typeof(AudioSample))
				SharedAudios.Clear();

			else if (typeof(T) == typeof(InputScheme))
				SharedSchemes.Clear();

			else if (typeof(T) == typeof(Layout))
				SharedLayouts.Clear();


		}


		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			foreach (TileSet ts in SharedTileSets.Values)
				ts.Dispose();
			SharedTileSets.Clear();

			SharedStrings.Clear();
			SharedSchemes.Clear();
			SharedScripts.Clear();

			foreach (Animation anim in SharedAnimations.Values)
				anim.Dispose();
			SharedAnimations.Clear();

			foreach (Scene scene in SharedScenes.Values)
				scene.Dispose();
			SharedScenes.Clear();

			foreach (BitmapFont font in SharedFonts.Values)
				font.Dispose();
			SharedFonts.Clear();

			foreach (AudioSample audio in SharedAudios.Values)
				audio.Dispose();
			SharedAudios.Clear();

			foreach (Layout layout in SharedLayouts.Values)
				layout.Dispose();
			SharedLayouts.Clear();

			foreach (Shader shader in SharedShaders.Values)
				shader.Dispose();
			SharedShaders.Clear();
		}



		#endregion


		#region Properties


		/// <summary>
		/// Scriptmodels
		/// </summary>
		public Dictionary<string, XmlNode> Models;


		/// <summary>
		/// Assets
		/// </summary>
		Dictionary<string, XmlNode> TileSets;
		Dictionary<string, XmlNode> Strings;
		Dictionary<string, XmlNode> Animations;
		Dictionary<string, XmlNode> Scenes;
		Dictionary<string, XmlNode> Fonts;
		Dictionary<string, XmlNode> Scripts;
		Dictionary<string, XmlNode> Audios;
		Dictionary<string, XmlNode> Schemes;
		Dictionary<string, XmlNode> Layouts;
		Dictionary<string, XmlNode> Shaders;






		/// <summary>
		/// Shared assets
		/// </summary>
		Dictionary<string, Layout> SharedLayouts;
		Dictionary<string, InputScheme> SharedSchemes;
		Dictionary<string, BitmapFont> SharedFonts;
		Dictionary<string, TileSet> SharedTileSets;
		Dictionary<string, StringTable> SharedStrings;
		Dictionary<string, Animation> SharedAnimations;
		Dictionary<string, AudioSample> SharedAudios;
		Dictionary<string, Scene> SharedScenes;
		Dictionary<string, Script> SharedScripts;
		Dictionary<string, Shader> SharedShaders;

		#endregion
	}
}
